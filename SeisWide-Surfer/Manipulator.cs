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
        private Dictionary<int, Tuple<int, int>> coordsFromSeisee = new Dictionary<int, Tuple<int, int>>();
        private Dictionary<int, Tuple<int, int>> joinedTraceWithCoords = new Dictionary<int, Tuple<int, int>>();

        private IModel model = new SortedArrayModel();

        /// <summary>
        /// 
        /// </summary>
        public TextWriter Writer { get; private set; }

        /// <summary>
        /// Suffix added to files with information about both hodographs.
        /// </summary>
        public static string SuffixTotal { get { return "-total"; } }

        /// <summary>
        /// Suffix added to files with information about reversed hodograph.
        /// </summary>
        public static string SuffixReversed { get { return "-reversed"; } }

        /// <summary>
        /// Suffix added to files with information about direct hodograph.
        /// </summary>
        public static string SuffixDirect { get { return "-direct"; } }

        /// <summary>
        /// Extension of classical 'tx.in' file. 
        /// </summary>
        public static string ExtIn { get { return ".in"; } }

        /// <summary>
        /// Extension of 'tx.in'-like file where also projections are stored.
        /// </summary>
        public static string ExtOut { get { return ".out"; } }

        /// <summary>
        /// Gets name of current workspace folder from settings.
        /// </summary>
        public string Folder { get { return Properties.Settings.Default.Folder; } } // return pathToFolder.Text// 

        /// <summary>
        /// Gets name of folder where SeiWide headers are stored.
        /// </summary>
        public string SourceSeisWideHeader { get { return Path.Combine(Folder, "Header_SeisWide"); } }

        /// <summary>
        /// Gets name of folder where SeiSee headers are stored.
        /// </summary>
        public string SourceSeiSeeHeader { get { return Path.Combine(Folder, "Header_SeiSee"); } }

        /// <summary>
        /// Gets name of folder where ' tx.in' files are stored.
        /// </summary>
        public string SourceTXIN { get { return Path.Combine(Folder, "tx.in"); } }

        /// <summary>
        /// Gets name of folder where supplementary files are stored.
        /// </summary>
        public string SourceBoundTXIN { get { return Path.Combine(Folder, "obj"); } }

        /// <summary>
        /// Gets name of folder where interpolation records are stored.
        /// </summary>
        public string SourceInterpolation { get { return Path.Combine(Folder, "Interpolation"); } }


        /// <summary>
        /// Creates new instance of file-and-directory manipulator. 
        /// </summary>
        /// <param name="_writer">Object that deals with all technical information about work process of program.</param>
        public Manipulator(TextWriter _writer)
        {
            Writer = _writer;
        }

        /// <summary>
        /// Creates new instance of file-and-directory manipulator. Output of all techincal info is redirected into console.
        /// </summary>
        public Manipulator() :
            this(Console.Out)
        {
        }

        /// <summary>
        /// Deploys workspace in the given folder. Creates all of missing subdirectories required for work.
        /// </summary>
        /// <param name="folder">Path to folder that has been selected.</param>
        public void SelectWorkspace(string folder)
        {
            Writer.WriteLine(folder);

            Writer.WriteLine("header_seiswide exists? {0}", Directory.Exists(SourceSeiSeeHeader));
            Writer.WriteLine("header_seisee exsts? {0}", Directory.Exists(SourceSeiSeeHeader));
            Writer.WriteLine("txin source folder exists? {0}", Directory.Exists(SourceTXIN));
            Writer.WriteLine("bound txin folder exists? {0}", Directory.Exists(SourceBoundTXIN));
            Writer.WriteLine("folder for interpolation exists? {0}", Directory.Exists(SourceInterpolation));

            string[] dirs = { SourceSeiSeeHeader, SourceSeisWideHeader, SourceTXIN, SourceBoundTXIN, SourceInterpolation };
            if (dirs.Any(dir => !Directory.Exists(dir)))
            {
                MessageBox.Show(Properties.Resources.msg_new_workspace,
                    "Дополнительные каталоги",
                    MessageBoxButtons.OK);

                foreach (string dir in dirs)
                    Directory.CreateDirectory(dir);
            }
        }

        /// <summary>
        /// Deletes all files and directories from given directory.
        /// </summary>
        /// <param name="subdir">Full path of directory you want to clean.</param>
        private void cleanSubdir(string subdir)
        {
            Writer.WriteLine("...Cleaning directory {0}", subdir);
            DirectoryInfo di = new DirectoryInfo(subdir);
            foreach (FileInfo file in di.GetFiles())
                file.Delete();

            foreach (DirectoryInfo dir in di.GetDirectories())
                dir.Delete(true);
        }

        /// <summary>
        /// Checks given 'tx.in' file for incorrect state.
        /// </summary>
        /// <param name="txin">Path to the 'tx.in' file.</param>
        /// <returns>True, if file is consistent, and false otherwise.</returns>
        public bool CheckTXIN(string txin)
        {
            // SeisWide may bind traces incorrectly. There are may be two records in 'tx.in' file with 
            // the same trace and the same wave number. With such an inconsistency in 'tx.in' this program 
            // will seem work normally, though its results are not supposed to be relevant.

            // We should track this mistake of binding in tx.in and note user about it.

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
                        string msg = string.Format("File: {0}{1}Trace {2}, wave {3}: Multiple occurences.",
                            Path.GetFileName(txin), Environment.NewLine, record[4], record[3]);

                        Writer.WriteLine(msg);
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

        /// <summary>
        /// Checks all 'tx.in' files in SourceTXIN directory for consistency
        /// </summary>
        /// <returns>True, if every file in directory is consistent, and false otherwise.</returns>
        public bool CheckTXIN()
        {
            Writer.WriteLine(@"...Checking input 'tx.in' files...");
            string[] txinFiles = Directory.GetFiles(SourceTXIN, "*.in");
            bool allTxinConsistent = true;
            foreach (string txin in txinFiles)
            {
                allTxinConsistent = CheckTXIN(txin) && allTxinConsistent;
            }
            Writer.WriteLine("...Checking finished.");
            return allTxinConsistent;
        }

        /// <summary>
        /// Used in binding procedure. Parses SeisWide header and extracts traces and distances.
        /// </summary>
        /// <param name="header">Path to SeisWide header.</param>
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

        /// <summary>
        /// Used in binding procedure. Binds appropriate distance values to traces using information
        /// gathered during parseSWHeader phase.        
        /// </summary>
        /// <param name="txinFile">Path to 'tx.in' file</param>
        private void parseTXIN_Bind(string txinFile)
        {
            string[] lines = File.ReadAllLines(txinFile);

            string outFile = Path.Combine(this.SourceBoundTXIN, Path.GetFileNameWithoutExtension(txinFile) + SuffixTotal + ".in");
            using (StreamWriter file = new StreamWriter(outFile))
            {
                Writer.WriteLine("...Creating  file:\t{0}", Path.GetFileName(outFile));
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
                    //Writer.WriteLine(line);

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
                    //Writer.WriteLine(result);
                    file.WriteLine(result);
                }
            }
        }

        /// <summary>
        /// Binds distance to corresponding trace in the 'tx.in' file, using info from SeisWide header. 
        /// </summary>
        /// <param name="swHeader"> Path to SeisWide header info about distances should be taken from.</param>
        /// <param name="txin"> Path to 'tx.in' file. </param>
        public void Correct(string swHeader, string txin)
        {
            cleanSubdir(SourceBoundTXIN);

            if (!CheckTXIN(txin))
            {
                MessageBox.Show(Properties.Resources.msg_incorrect_txin, "Обнаружены ошибки привязки трассы.");
                return;
            }
            parseSWHeader(swHeader);
            parseTXIN_Bind(txin);
        }

        /// <summary>
        /// Checks every 'tx.in' file in SourceTXIN directory for consistency. Checks 1-to-1 mapping of 
        /// SeisWide headers and 'tx.in' files in corresponding subdirectories. If everything is fine,
        /// for every pair of SeisWide header and 'tx.in' file binds distance to corresponding trace in the 
        /// 'tx.in' file, using information from SeisWide header. 
        /// </summary>
        private void correctAll()
        {
            if (!Directory.Exists(Folder))
            {
                MessageBox.Show("Не указан или указан неправильно каталог.", "Ошибка");
                return;
            }
            if (!CheckTXIN())
            {
                MessageBox.Show(Properties.Resources.msg_incorrect_txin, "Обнаружены ошибки привязки трассы.");
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

            Writer.WriteLine("...Binding distances to traces...");
            foreach (string swHeader in seisWideHeaderFiles)
            {
                string f = Path.GetFileName(swHeader);
                string txin = Path.Combine(SourceTXIN, Path.ChangeExtension(f, ".in"));
                
                parseSWHeader(swHeader);
                parseTXIN_Bind(txin);
            }
            Writer.WriteLine("...Binding finished.");
        }

        /// <summary>
        /// Extracts coordinates of every trace from SeiSee header.
        /// </summary>
        /// <param name="headerSeiSee">Path to SeiSee header.</param>
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
            //Writer.WriteLine("distance {0}\nx {1}\ny {2}\ndata starts from {3}", dist, groupXColumn, groupYColumn, i);

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

        /// <summary>
        /// Matches coordinates extracted from SeiSee header in readSeiSeeHeader phase 
        /// to traces contained in the given SeisWide header.
        /// </summary>
        /// <param name="hsw">Path to SeisWide header.</param>
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

        /// <summary>
        /// Creates 'tx.in'-like file where every record also contains value of projection.
        /// </summary>
        /// <param name="txin">Path to 'tx.in' file for every trace in consists of we need to calculate projection.</param>
        /// <param name="p"> Profile parameters used in calculation of projection.</param>
        private void createOut(string txin, Profile p)
        {
            string[] lines = File.ReadAllLines(txin);

            string outFile = Path.Combine(this.SourceBoundTXIN, Path.GetFileNameWithoutExtension(txin) + SuffixTotal + ".out");

            using (StreamWriter file = new StreamWriter(outFile))
            {
                Writer.WriteLine("...creating file:\t{0}", Path.GetFileName(outFile));
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

                    double x = double.Parse(record[0]);
                    int trace = int.Parse(record[4]);
                    if (dict.ContainsKey(trace))
                        x = ((dict[trace]) / 1000.0);

                    double projection = 0;
                    if (joinedTraceWithCoords.ContainsKey(trace))
                    {
                        var tuple = joinedTraceWithCoords[trace];
                        projection = p.getProjection(tuple);
                    }

                    double err = double.Parse(record[2]);
                    result = string.Format("{0,10:F3} {1,8:F3} {2,9:F3} {3,8} {4,7} {5,9:F3}",
                            x,
                            double.Parse(record[1]),    // this is time
                            double.Parse(record[2]),    // this is error (0.050 value)
                            record[3],
                            trace,
                            projection / 1000);
                    file.WriteLine(result);
                }
            }
        }

        /// <summary>
        /// Binds traces and calculates projection for every trace in 'tx.in' file using SeiSee header, 
        /// SeisWide header and profile parameters. Result is written in 'tx.in'-like file 
        /// in the SourceBoundTXIN directory.
        /// </summary>
        /// <param name="txin">Path to 'tx.in' file.</param>
        /// <param name="hsw">Path to SeisWide header.</param>
        /// <param name="hss">Path to SeiSee header.</param>
        /// <param name="p">Profile parameters used in calculation of projections.</param>
        private void calculateProjection(string txin, string hsw, string hss, Profile p)
        {
            coordsFromSeisee.Clear();
            joinedTraceWithCoords.Clear();

            readSeiSeeHeader(hss);
            parseSWHeader(hsw);
            readSeisWideHeader(hsw);
            createOut(txin, p);
        }

        /// <summary>
        /// Checks if everything is fine and in this case calculates projections for every 'tx.in' file 
        /// using coordinates obtained from corresponding SeiSee and SeisWide headers.
        /// </summary>
        private void correctAllWithProjections()
        {
            if (!Directory.Exists(Folder))
            {
                MessageBox.Show("Не указан или указан неправильно каталог.", "Ошибка");
                return;
            }

            if (!CheckTXIN())
            {
                MessageBox.Show(Properties.Resources.msg_incorrect_txin, "Обнаружены ошибки привязки трассы.");
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

            Writer.WriteLine("...Calculating projections...");
            Profile p = Profile.ExtractInstance;
            Writer.WriteLine("...Here are profile parameters:{0}{1}", Environment.NewLine, p);
            foreach (string ssh in sshFiles)
            {
                string f = Path.GetFileName(ssh);
                string txin = Path.Combine(SourceTXIN, Path.ChangeExtension(f, ".in"));
                string swh = Path.Combine(SourceSeisWideHeader, Path.ChangeExtension(f, ".txt"));

                calculateProjection(txin, swh, ssh, p);
            }
            Writer.WriteLine("...Projections calculated.");
        }

        /// <summary>
        /// Checks 'tx.in' file for consistency. If it is fine, calculates projections for it
        /// using given SeiSee and SeisWide headers and writes result in the 'tx.in'-like file.
        /// </summary>
        /// <param name="txin">Path to 'tx.in' file.</param>
        /// <param name="hsw">Path to SeisWide header. </param>
        /// <param name="hss">Path to SeiSee header.</param>
        public void CorrectWithProjections(string txin, string hsw, string hss)
        {
            if (!CheckTXIN(txin))
            {
                MessageBox.Show(Properties.Resources.msg_incorrect_txin, "Обнаружены ошибки привязки трассы.");
                return;
            }
            cleanSubdir(SourceBoundTXIN);

            Profile p = Profile.ExtractInstance;
            Writer.WriteLine(p);
            calculateProjection(txin, hsw, hss, p);
        }

        /// <summary>
        /// Corrects every 'tx.in' file in SourceTXIN directory.
        /// </summary>
        /// <param name="useProjections"> True, if you want to works with projections.
        ///     False, if you need just to bind correct values of distances.</param>
        public void CorrectAllTXIN(bool useProjections)
        {
            cleanSubdir(SourceBoundTXIN);
            if (useProjections)
                correctAllWithProjections();
            else
                correctAll();
        }

        /// <summary>
        /// Splits 'tx.in' or 'tx.in'-like file into two, which contain info about only one hodograph, either direct or reverse.
        /// </summary>
        /// <param name="file">Path to 'tx.in' or 'tx.in'-like file </param>
        /// <param name="suffix">Suffix in the name of file which should be replaced for both hodograph branches.</param>
        private void splitHodographs(string file, string suffix)
        {
            string[] lines = File.ReadAllLines(file);
            if (lines.Length < 1)
                return;

            string ext = Path.GetExtension(file);
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

        /// <summary>
        /// Splits every 'tx.in' or 'tx.in'-like file in SourceBoundTXIN directory into separate hodographs.
        /// </summary>
        /// <param name="useProjections">Flag meaning if we need to work with either 'tx.in' files or
        ///     files with additional projection column.</param>
        public void SplitHodographs(bool useProjections)
        {
            string ext = useProjections ? ExtOut : ExtIn;
            string[] outFiles = Directory.GetFiles(SourceBoundTXIN, "*" + SuffixTotal + ext);
            foreach (string file in outFiles)
                splitHodographs(file, SuffixTotal);
        }

        /// <summary>
        /// Takes all 'tx.in' or 'tx.in'-like files and every group of files with the same suffix unites into the one.
        /// </summary>
        /// <param name="useProjections">Flag meaning if we need to work with either 'tx.in' files or
        ///     files with additional projection column.</param>
        public void ConsolidateTXIN(bool useProjections)
        {
            string ext = useProjections ? ExtOut : ExtIn;
            string[] outFiles = Directory.GetFiles(SourceBoundTXIN, "*" + ext);
            string lastLine = string.Empty;

            using (StreamWriter total = new StreamWriter(Path.Combine(Folder, SuffixTotal + ext)),
                             reversed = new StreamWriter(Path.Combine(Folder, SuffixReversed + ext)),
                               direct = new StreamWriter(Path.Combine(Folder, SuffixDirect + ext)))
            {
                foreach (string file in outFiles)
                {
                    if (Path.GetFileNameWithoutExtension(file).EndsWith(SuffixTotal))
                        continue;

                    bool isDirect = file.IndexOf(SuffixDirect) > -1;
                    string[] lines = File.ReadAllLines(file);

                    int first = isDirect ? 1 : 0;
                    int last = lines.Length - (isDirect ? 1 : 2);
                    for (int i = first; i < last; i++)
                    {
                        total.WriteLine(lines[i]);
                        (isDirect ? direct : reversed).WriteLine(lines[i]);
                    }
                    lastLine = lines[lines.Length - 1];
                }

                total.WriteLine(lastLine);
                reversed.WriteLine(lastLine);
                direct.WriteLine(lastLine);
            }
        }

        /// <summary>
        /// Interpolates every 'tx.in' or 'tx.in'-like file in SourceBountTXIN directory with the given filename suffix
        /// and step of interpolation in time domain.
        /// </summary>
        /// <param name="suffix">Can be "-total", "-reversed", "-direct".</param>
        /// <param name="ext">Extension of the textFile, either ".in" or ".out".</param>>
        /// <param name="timeDelta">Value of interpolation step in the time domain.</param>
        /// <param name="useProjections">Flag meaning if we going to use projections in calculation or not.</param>
        /// <param name="intoSingleOutput">Flag meaning if function should gather all results in one file 
        ///     or write it in separates files instead. </param>
        private void processInterpolation(string suffix, string ext, double timeDelta, bool useProjections, bool intoSingleOutput)
        {
            string[] outFiles = Directory.GetFiles(SourceBoundTXIN, "*" + suffix + ext);

            string feExtension = ".fentry";
            string ipExtension = ".txt";

            string resultingFile = intoSingleOutput ?
                Path.Combine(SourceInterpolation, string.Format("Result-{0}{1}{2}", timeDelta, suffix, ipExtension)) :
                "";
            if (intoSingleOutput)
                File.Delete(resultingFile);

            string fileFE = intoSingleOutput ? Path.ChangeExtension(resultingFile, feExtension) : "";

            foreach (string f in outFiles)
            {
                Writer.WriteLine("...Interpolating file: {0}", Path.GetFileName(f));
                // extracting first entry 
                model.Initialize(f, useProjections);

                model.ExtractFirstEntry();
                if (intoSingleOutput)
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

                // interpolating stuff
                model.Interpolate(timeDelta);
                model.CheckInterpolation(timeDelta);
                if (intoSingleOutput)
                    File.AppendAllText(resultingFile, model.InterpolationResult());
                else
                {
                    resultingFile = Path.Combine(SourceInterpolation, Path.ChangeExtension(Path.GetFileName(f), ipExtension));
                    File.WriteAllText(resultingFile, model.InterpolationResult());
                }
            }
        }

        /// <summary>
        /// Processes interpolation for every 'tx.in' or 'tx.in'-like file in SourceBountTXIN directory with given 
        /// step of intepolation.
        /// </summary>
        /// <param name="delta">Value of interpolation step in the time domain.</param>
        /// <param name="useProjections">Flag meaning if we going to use projections in calculation or not.</param>
        /// <param name="intoSingleOutput">Flag meaning if function should gather all results in one file 
        ///     or write it in separates files instead. </param>
        public void ProcessInterpolation(double delta, bool useProjections, bool intoSingleOutput)
        {
            cleanSubdir(SourceInterpolation);
            string ext = useProjections ? ".out" : ".in";

            Writer.WriteLine("...Starting interpolation  procedure...");
            processInterpolation(SuffixTotal, ext, delta, useProjections, intoSingleOutput);
            processInterpolation(SuffixReversed, ext, delta, useProjections, intoSingleOutput);
            processInterpolation(SuffixDirect, ext, delta, useProjections, intoSingleOutput);
            Writer.WriteLine("...Interpolation  procedure  finished.");
        }
    }
}
