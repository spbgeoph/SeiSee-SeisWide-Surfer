using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace SeisWide_Surfer
{
    class Manipulator
    {
        private Dictionary<int, int> dict = new Dictionary<int, int>();
        Dictionary<int, Tuple<int, int>> coordsFromSeisee = new Dictionary<int, Tuple<int, int>>();
        Dictionary<int, Tuple<int, int>> joinedTraceWithCoords = new Dictionary<int, Tuple<int, int>>();

        IModel model = new SortedArrayModel();

        public static string SuffixTotal { get { return "-total"; } }
        public static string SuffixReversed { get { return "-reversed"; } }
        public static string SuffixDirect { get { return "-direct"; } }
        public static string ExtIn { get { return ".in"; } }
        public static string ExtOut { get { return ".out"; } }

        public string Folder { get { return Properties.Settings.Default.Folder; } } // return pathToFolder.Text// 
        public string SourceSeisWideHeader { get { return Path.Combine(Folder, "Header_SeisWide"); } }
        public string SourceSeiSeeHeader { get { return Path.Combine(Folder, "Header_SeiSee"); } }
        public string SourceTXIN { get { return Path.Combine(Folder, "tx.in"); } }
        public string SourceBoundTXIN { get { return Path.Combine(Folder, "obj"); } }
        public string SourceInterpolation { get { return Path.Combine(Folder, "Interpolation"); } }

        public void SelectWorkspace(string folder)
        {

            Console.WriteLine(folder);

            Console.WriteLine("header_seiswide exists? {0}", Directory.Exists(SourceSeiSeeHeader));
            Console.WriteLine("header_seisee exsts? {0}", Directory.Exists(SourceSeiSeeHeader));
            Console.WriteLine("txin source folder exists? {0}", Directory.Exists(SourceTXIN));
            Console.WriteLine("bound txin folder exists? {0}", Directory.Exists(SourceBoundTXIN));
            Console.WriteLine("folder for interpolation exists? {0}", Directory.Exists(SourceInterpolation));

            string[] dirs = { SourceSeiSeeHeader, SourceSeisWideHeader, SourceTXIN, SourceBoundTXIN, SourceInterpolation };
            if (dirs.Any(dir => !Directory.Exists(dir)))
            {
                string msg = "В выбранном каталоге были созданы подкаталоги для заголовков SeisWide, заголовков SeiSee и " +
                    "файлов корелляции формата \'tx.in\', а также каталог для промежуточных результатов и " +
                    "каталог для выходных файлов интерполяции.";
                MessageBox.Show(msg, "Дополнительные каталоги", MessageBoxButtons.OK);

                foreach (string dir in dirs)
                    Directory.CreateDirectory(dir);
            }
        }

        private void cleanSubdir(string subdir)
        {
            DirectoryInfo di = new DirectoryInfo(subdir);
            foreach (FileInfo file in di.GetFiles())
                file.Delete();

            foreach (DirectoryInfo dir in di.GetDirectories())
                dir.Delete(true);
        }

        public bool CheckTXIN(string txin)
        {
            // SeisWide may bind traces incorrectly. There are may be two records with the same trace and the same
            // wave number. With such an inconsistency in tx.in this program will seem work normally, though its
            // results are not supposed to be relevant.

            // We should track this mistake of binding in tx.in and note an user about it.

            Dictionary<string, string> txinDict = new Dictionary<string, string>();
            string[] lines = File.ReadAllLines(txin);
            bool result = true;
            foreach (string line in lines)
            {
                string[] record = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (record.Length < 5)
                    continue;

                if (txinDict.ContainsKey(record[4]))
                {
                    if (record[3].Equals(txinDict[record[4]]))
                    {
                        string msg = string.Format("File: {0}\nTrace {1}, wave {2}: Multiple occurences.\n",
                            txin, record[4], record[3]);

                        Console.WriteLine(msg);
                        //MessageBox.Show(msg, "Error");
                        result = false;
                    }
                    else
                        txinDict[record[4]] = record[3];
                }
                else
                    txinDict.Add(record[4], record[3]);
            }

            return result;
        }

        public bool CheckTXIN()
        {
            string[] txinFiles = Directory.GetFiles(SourceTXIN, "*.in");
            bool allTxinConsistent = true;
            foreach (string txin in txinFiles)
            {
                allTxinConsistent = CheckTXIN(txin) && allTxinConsistent;
            }
            return allTxinConsistent;
        }

        private void parseSWHeader(string header)
        {
            dict.Clear();
            string[] lines = File.ReadAllLines(header);

            int i;
            for (i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("Trace "))
                    break;
            }
            i++;

            while (i < lines.Length)
            {
                string[] record = lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int trace = int.Parse(record[0]);
                int cdp = int.Parse(record[3]);

                dict.Add(trace, cdp);
                i++;
            }
        }

        private void parseTXIN_Bind(string txinFile)
        {
            string[] lines = File.ReadAllLines(txinFile);

            string outFile = Path.Combine(this.SourceBoundTXIN, Path.GetFileNameWithoutExtension(txinFile) + SuffixTotal + ".in");
            using (StreamWriter file = new StreamWriter(outFile))
            {
                Console.WriteLine("opening file:\t{0}", Path.GetFileName(outFile));
                string result;
                foreach (string line in lines)
                {
                    string[] record = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (record.Length <= 4)
                    {
                        result = string.Format("{0,10:F3} {1,8:F3} {2,9:F3} {3,8}",
                            double.Parse(record[0]),
                            double.Parse(record[1]),
                            double.Parse(record[2]),
                            int.Parse(record[3]));
                        file.WriteLine(result);
                        continue;
                    }
                    //Console.WriteLine(line);

                    double x = double.Parse(record[0]);
                    int trace = int.Parse(record[4]);
                    if (dict.ContainsKey(trace))
                        x = ((dict[trace]) / 1000.0);

                    double err = double.Parse(record[2]);
                    result = string.Format("{0,10:F3} {1,8:F3} {2,9:F3} {3,8} {4,7}",
                            x,
                            double.Parse(record[1]),    // this is time
                            double.Parse(record[2]),    // this is error (0.050 value)
                            record[3],
                            trace);
                    //Console.WriteLine(result);
                    file.WriteLine(result);
                }
            }
        }

        public void Correct(string swHeader, string txin)
        {
            cleanSubdir(SourceBoundTXIN);

            if (!CheckTXIN(txin))
            {
                MessageBox.Show("Работа программы была завершена преждевременно.\nУстраните ошибки привязки SeisWide: " +
                    "записи одного номера волны с одинаковым номером трассы.", "Обнаружены ошибки привязки трассы.");
                return;
            }
            parseSWHeader(swHeader);
            parseTXIN_Bind(txin);
        }

        private void correctAll()
        {
            if (!Directory.Exists(Folder))
            {
                MessageBox.Show("Не указан или указан неправильно каталог.", "Ошибка");
                return;
            }

            if (!CheckTXIN())
            {
                MessageBox.Show("Работа программы была завершена преждевременно.\nУстраните ошибки привязки SeisWide: " +
                    "записи одного номера волны с одинаковым номером трассы.", "Обнаружены ошибки привязки трассы.");
                return;
            }
            string[] seisWideHeaderFiles = Directory.GetFiles(SourceSeisWideHeader, "*.txt");
            foreach (string h in seisWideHeaderFiles)
            {
                string txin = Path.Combine(SourceTXIN, Path.ChangeExtension(Path.GetFileName(h), ".in"));
                if (File.Exists(txin))
                    continue;

                string error = string.Format("Для заголовка\n{0}\nне был найден .in файл c именем\n{1}",
                    h.Remove(0, Folder.Length),
                    txin.Remove(0, Folder.Length));
                MessageBox.Show(error, "Ошибка!");
                return;
            }

            foreach (string swHeader in seisWideHeaderFiles)
            {
                string f = Path.GetFileName(swHeader);
                string txin = Path.Combine(SourceTXIN, Path.ChangeExtension(f, ".in"));
                Console.WriteLine("Parsing header:\t\t{0}", f);

                parseSWHeader(swHeader);
                parseTXIN_Bind(txin);
            }
        }

        private void readSeiSeeHeader(string headerSeiSee)
        {
            string[] lines = File.ReadAllLines(headerSeiSee);
            int i, dist = -1, groupXColumn = -1, groupYColumn = -1, sourceXColumn = -1, sourceYColumn = -1;

            for (i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("Distance from source point"))
                    dist = i;
                else if (lines[i].Contains("Group  X coordinate"))
                    groupXColumn = i;
                else if (lines[i].Contains("Group  Y coordinate"))
                    groupYColumn = i;
                else if (lines[i].Contains("Source X coordinate"))
                    sourceXColumn = i;
                else if (lines[i].Contains("Source Y coordinate"))
                    sourceYColumn = i;
                else if (lines[i].StartsWith("+-"))
                    break;
            }
            //Console.WriteLine("distance {0}\nx {1}\ny {2}\ndata starts from {3}", dist, groupXColumn, groupYColumn, i);

            string[] recordWithSource = lines[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            coordsFromSeisee.Add(-1, new Tuple<int, int>(int.Parse(recordWithSource[sourceXColumn]),
                                                           int.Parse(recordWithSource[sourceYColumn])));

            while (++i < lines.Length)
            {
                string[] record = lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                int d = int.Parse(record[dist]);
                int x = int.Parse(record[groupXColumn]);
                int y = int.Parse(record[groupYColumn]);

                if (!coordsFromSeisee.ContainsKey(d))
                    coordsFromSeisee.Add(d, new Tuple<int, int>(x, y));
            }
        }

        private void readSeisWideHeader(string hsw)
        {
            string[] lines = File.ReadAllLines(hsw);

            int i = 0;
            while (i++ < lines.Length)
                if (lines[i].Contains("Trace "))
                    break;

            while (++i < lines.Length)
            {
                string[] record = lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int trace = int.Parse(record[0]);
                double offsetInKM = double.Parse(record[4]);

                int offsetInM = (int)Math.Round(offsetInKM * 1000);
                if (coordsFromSeisee.ContainsKey(offsetInM))
                {
                    joinedTraceWithCoords.Add(trace, coordsFromSeisee[offsetInM]);
                }
                else
                {
                    string ohcrap = string.Format("\tOh no! Seiswide header: \n{0}\n," +
                        "could not find any info about offset {1}\nMaybe rounding sucks. " +
                        "Please check corresponding SeiSee header.", offsetInKM, hsw);
                    MessageBox.Show(ohcrap, "Error!!!");
                }
            }
            joinedTraceWithCoords.Add(-1, coordsFromSeisee[-1]);
        }

        private void createOut(string txin, Profile p)
        {
            string[] lines = File.ReadAllLines(txin);

            string outFile = Path.Combine(this.SourceBoundTXIN, Path.GetFileNameWithoutExtension(txin) + SuffixTotal + ".out");

            using (StreamWriter file = new StreamWriter(outFile))
            {
                Console.WriteLine("Creating file:\t{0}", Path.GetFileName(outFile));
                string result;
                foreach (string line in lines)
                {
                    string[] record = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (record.Length <= 4)
                    {
                        var tuple = joinedTraceWithCoords[-1];
                        result = string.Format("{0,10:F3} {1,8:F3} {2,9:F3} {3,8} {4,7} {5,9:F3}",
                            double.Parse(record[0]),
                            double.Parse(record[1]),
                            double.Parse(record[2]),
                            int.Parse(record[3]),
                            "",
                            p.getProjection(tuple) / 1000);
                        file.WriteLine(result);
                        continue;
                    }

                    int trace = int.Parse(record[4]);
                    double projection = 0;
                    if (joinedTraceWithCoords.ContainsKey(trace))
                    {
                        var tuple = joinedTraceWithCoords[trace];
                        projection = p.getProjection(tuple);
                    }

                    double err = double.Parse(record[2]);
                    result = string.Format("{0,10:F3} {1,8:F3} {2,9:F3} {3,8} {4,7} {5,9:F3}",
                            double.Parse(record[0]),
                            double.Parse(record[1]),    // this is time
                            double.Parse(record[2]),    // this is error (0.050 value)
                            record[3],
                            trace,
                            projection / 1000);
                    file.WriteLine(result);
                }
            }
        }

        private void calculateProjection(string txin, string hsw, string hss, Profile p)
        {
            coordsFromSeisee.Clear();
            joinedTraceWithCoords.Clear();

            Console.WriteLine("Reading seisee {0}", hss);
            readSeiSeeHeader(hss);

            Console.WriteLine("Reading seiswide {0}", hsw);
            readSeisWideHeader(hsw);

            Console.WriteLine("Calculating projections of tx.in {0}", txin);
            createOut(txin, p);
        }

        private void correctAllWithProjections()
        {
            if (!Directory.Exists(Folder))
            {
                MessageBox.Show("Не указан или указан неправильно каталог.", "Ошибка");
                return;
            }

            Console.WriteLine(Folder);

            if (!CheckTXIN())
            {
                MessageBox.Show("Работа программы была завершена преждевременно.\nУстраните ошибки привязки SeisWide: " +
                    "записи одного номера волны с одинаковым номером трассы.", "Обнаружены ошибки привязки трассы.");
                return;
            }

            string[] swhFiles = Directory.GetFiles(SourceSeisWideHeader, "*.txt");
            string[] sshFiles = Directory.GetFiles(SourceSeiSeeHeader, "*.");

            if (swhFiles.Length != sshFiles.Length)
            {
                string error = string.Format("Количество заголовков SeiSee: {0}\nКоличество заголовков SeisWide: {1}",
                    sshFiles.Length,
                    swhFiles.Length);
                MessageBox.Show("Количество заголовков не совпадает!\n" + error, "Ошибка!");
                return;
            }

            foreach (string h in swhFiles)
            {
                string txin = Path.Combine(SourceTXIN, Path.ChangeExtension(Path.GetFileName(h), ".in"));
                string ssh = Path.Combine(SourceSeiSeeHeader, Path.GetFileNameWithoutExtension(h));
                if (File.Exists(txin) && File.Exists(ssh))
                    continue;

                string error = string.Format("Для заголовка\n\t{0}\nне был найден .in файл c именем\n\t{1}\nили " +
                    "заголовок SeiSee с именем\n\t{2}",
                    h.Remove(0, Folder.Length),
                    txin.Remove(0, Folder.Length),
                    ssh.Remove(0, Folder.Length));
                MessageBox.Show(error, "Ошибка!");
                return;
            }

            Profile p = Profile.ExtractInstance;
            Console.WriteLine(p);
            foreach (string ssh in sshFiles)
            {
                string f = Path.GetFileName(ssh);
                string txin = Path.Combine(SourceTXIN, Path.ChangeExtension(f, ".in"));
                string swh = Path.Combine(SourceSeisWideHeader, Path.ChangeExtension(f, ".txt"));

                calculateProjection(txin, swh, ssh, p);
            }
        }

        public void CorrectWithProjections(string txin, string hsw, string hss)
        {
            if (!CheckTXIN(txin))
            {
                MessageBox.Show("Работа программы была завершена преждевременно.\nУстраните ошибки привязки SeisWide: " +
                    "записи одного номера волны с одинаковым номером трассы.", "Обнаружены ошибки привязки трассы.");
                return;
            }
            cleanSubdir(SourceBoundTXIN);

            Profile p = Profile.ExtractInstance;
            Console.WriteLine(p);
            calculateProjection(txin, hsw, hss, p);
        }

        public void CorrectAllTXIN(bool useProjections)
        {
            cleanSubdir(SourceBoundTXIN);
            if (useProjections)
                correctAllWithProjections();
            else
                correctAll();
        }

        private void splitHodographs(string file, string suffix, string ext)
        {
            string[] lines = File.ReadAllLines(file);
            if (lines.Length < 1)
                return;

            string ending = suffix + ext;
            string rev = file.EndsWith(ending) ? file.Replace(ending, SuffixReversed + ext) : "";
            string dir = file.EndsWith(ending) ? file.Replace(ending, SuffixDirect + ext) : "";


            string blastPoint = lines[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
            int i = 0;
            using (StreamWriter stream = new StreamWriter(rev))
            {
                stream.WriteLine(lines[0]);
                while (++i < lines.Length)
                {
                    stream.WriteLine(lines[i]);

                    if (lines[i].Contains(blastPoint) &&
                        "0".Equals(lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[3]))
                        break;
                }
                stream.WriteLine(lines[lines.Length - 1]);
            }

            using (StreamWriter stream = new StreamWriter(dir))
            {
                stream.WriteLine(lines[0]);
                stream.WriteLine(lines[i]);

                while (++i < lines.Length)
                    stream.WriteLine(lines[i]);
            }
        }

        public void SplitHodographs(bool useProjections)
        {
            string ext = useProjections ? ExtOut : ExtIn;
            string[] outFiles = Directory.GetFiles(SourceBoundTXIN, "*" + SuffixTotal + ext);
            foreach (string file in outFiles)
                splitHodographs(file, SuffixTotal, ext);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="suffix">Can be "-total", "-reversed", "-direct".</param>
        /// <param name="ext">Extension of the textFile, either ".in" or ".out".</param>>
        private void processInterpolation(string suffix, string ext, double delta, bool useProjections, bool ifSingle)
        {
            string[] outFiles = Directory.GetFiles(SourceBoundTXIN, "*" + suffix + ext);

            string feExtension = ".fentry";
            string ipExtension = ".txt";

            string resultingFile = ifSingle ?
                Path.Combine(SourceInterpolation, string.Format("Result-{0}{1}{2}", delta, suffix, ipExtension)) :
                "";
            if (ifSingle)
                File.Delete(resultingFile);

            string fileFE = ifSingle ? Path.ChangeExtension(resultingFile, feExtension) : "";

            foreach (string f in outFiles)
            {
                Console.WriteLine("Interpolating file: {0}", Path.GetFileName(f));
                // extracting first entry 
                model.Initialize(f, useProjections);
                model.ExtractFirstEntry();
                if (ifSingle)
                {
                    File.AppendAllText(fileFE, model.FirstEntryHeader);
                    File.AppendAllText(fileFE, model.FirstEntry());
                }
                else
                {
                    fileFE = Path.Combine(SourceInterpolation, Path.ChangeExtension(Path.GetFileName(f), feExtension));
                    File.WriteAllText(fileFE, model.FirstEntryHeader);
                    File.AppendAllText(fileFE, model.FirstEntry());
                }
                Console.WriteLine("Writing first entry into {0}", fileFE);

                // interpolating stuff
                model.Interpolate(delta);
                model.CheckInterpolation(delta);
                if (ifSingle)
                    File.AppendAllText(resultingFile, model.InterpolationResult());
                else
                {
                    resultingFile = Path.Combine(SourceInterpolation, Path.ChangeExtension(Path.GetFileName(f), ipExtension));
                    File.WriteAllText(resultingFile, model.InterpolationResult());
                }
            }
        }

        public void ProcessInterpolation(double delta, bool useProjections, bool intoSingleOutput)
        {
            cleanSubdir(SourceInterpolation);
            string ext = useProjections ? ".out" : ".in";

            processInterpolation(SuffixTotal, ext, delta, useProjections, intoSingleOutput);
            processInterpolation(SuffixReversed, ext, delta, useProjections, intoSingleOutput);
            processInterpolation(SuffixDirect, ext, delta, useProjections, intoSingleOutput);
        }
    }
}
