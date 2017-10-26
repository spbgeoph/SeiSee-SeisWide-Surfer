using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeisWide_Surfer
{
    public class AnotherManipulator
    {
        private AnotherModel model = new AnotherModel();

        private Dictionary<int, Tuple<int, int>> traceToDist = new Dictionary<int, Tuple<int, int>>();
        private Dictionary<int, double> distToProj = new Dictionary<int, double>();


        public TextWriter Writer { get; private set; }

        public static readonly string format_txin_separator = "{0,10:F3} {1,9:F3} {2,9:F3} {3,9}";
        public static readonly string format_txin_record    = "{0,10:F3} {1,9:F3} {2,9:F3} {3,9} {4,10}";
        public static readonly string format_hodo_record    = "{0,10:F3} {1,8:F3} {2,5} {3,5} {4,10:F3} {5,10:F3}";
        public static readonly string format_refl_squared_times = "{0,10:F3} {1,10:F3} {2,10:F3} {3,5} {4,10:F3} {5,10:F3}";

        /// <summary>
        /// Gets name of current workspace folder from settings.
        /// </summary>
        public string Folder { get { return Properties.Settings.Default.Folder; } } // return pathToFolder.Text// 

        /// <summary>
        /// Gets name of folder where SeiWide headers are stored.
        /// </summary>
        public string SourceHeaders { get { return Path.Combine(Folder, "in_headers"); } }

        /// <summary>
        /// Gets name of folder where ' tx.in' files are stored.
        /// </summary>
        public string SourcePicking { get { return Path.Combine(Folder, "in_picking"); } }

        /// <summary>
        /// Gets name of folder where supplementary files are stored.
        /// </summary>
        public string SourceBinding { get { return Path.Combine(Folder, "out_binding"); } }

        /// <summary>
        /// </summary>
        public string SourceVisualize { get { return Path.Combine(Folder, "out_for_surfer"); } }

        /// <summary>
        /// 
        /// </summary>
        public string TotalBinding { get { return Path.Combine(SourceBinding, "total.in"); } }

        public string ReflectedRev { get { return Path.Combine(SourceVisualize, "reflected_rev.txt"); } }

        public string ReflectedDirect { get { return Path.Combine(SourceVisualize, "reflected_dir.txt"); } }

        public string ReflectedTotal { get { return Path.Combine(SourceVisualize, "reflected_total.txt"); } }

        public string HodoDirect { get; set; }  // { return Path.Combine(SourceVisualize, "direct.txt"); } 

        public string HodoRev { get; set; }  //{ return Path.Combine(SourceVisualize, "reversed.txt"); }

        public string InterTotal { get; set; }

        public string InterDirect { get; set; }

        public string InterRev { get; set; }

        public string PrefixProj { get { return "proj_"; } }

        public string PrefixReal { get { return "real_"; } }

        /// <summary>
        /// Creates new instance of file-and-directory manipulator. 
        /// </summary>
        /// <param name="_writer">Object that deals with all technical information about work process of program.</param>
        public AnotherManipulator(TextWriter _writer)
        {
            Writer = _writer;
        }

        /// <summary>
        /// Creates new instance of file-and-directory manipulator. Output of all techincal info is redirected into console.
        /// </summary>
        public AnotherManipulator() :
            this(Console.Out)
        {
        }

        /// <summary>
        /// Deploys workspace in the given folder. Creates all of missing subdirectories required for work.
        /// </summary>
        /// <param name="folder">Path to folder that has been selected.</param>
        public void SetWorkspace(string folder)
        {
            Writer.WriteLine(folder);

            Writer.WriteLine("Does in_headers folder exist? {0}", Directory.Exists(SourceHeaders));
            Writer.WriteLine("Does in_picking folder exist? {0}", Directory.Exists(SourcePicking));
            Writer.WriteLine("Does out_binding folder exist? {0}", Directory.Exists(SourceBinding));
            Writer.WriteLine("Does out_for_surfer folder exist? {0}", Directory.Exists(SourceVisualize));

            string[] dirs = { SourceHeaders, SourcePicking, SourceBinding, SourceVisualize };
            if (dirs.Any(dir => !Directory.Exists(dir)))
            {
                if (migrate(folder))
                    return;

                MessageBox.Show(Properties.Resources.msg_new_workspace,
                    "Дополнительные каталоги",
                    MessageBoxButtons.OK);

                foreach (string dir in dirs)
                    Directory.CreateDirectory(dir);
            }

            Program.ReadProfileData(Folder);
        }

        internal bool migrate(string folder)
        {
            bool result = false;

            string hss = "Header_SeiSee";
            string hsw = "Header_SeisWide";
            string intrp = "Interpolation";
            string txin = "tx.in";
            string obj = "obj";
            string[] oldDirs = { hss, hsw, intrp, txin, obj };


            string[] subDirs = Directory.GetDirectories(folder);
            var temp = subDirs.Select(dir => dir.Remove(0, folder.Length + 1));
            if (oldDirs.All(old => temp.Contains(old)))
            {
                result = true;
                Writer.WriteLine("Yay, we can restructure this folder!");

                Directory.Move(Path.Combine(folder, txin), SourcePicking);
                Directory.Move(Path.Combine(folder, hsw), SourceHeaders);
                Directory.Move(Path.Combine(folder, obj), SourceBinding);
                Directory.Move(Path.Combine(folder, intrp), SourceVisualize);

                // extra manipulations to move seisee headers
                string[] files = Directory.GetFiles(Path.Combine(folder, hss));
                foreach (string file in files)
                {
                    File.Move(file, Path.Combine(SourceHeaders, Path.GetFileName(file)));
                }
                Directory.Delete(Path.Combine(folder, hss));
            }
            return result;
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

        public void ProcessFirstEntry(ProcessingFlag options, double delta)
        {
            cleanSubdir(SourceBinding);
            string prefix = options.HasFlag(ProcessingFlag.Project) ? PrefixProj : PrefixReal;

            List<string> toBeErased = new List<string>();
            if (options.HasFlag(ProcessingFlag.SplitHodographs))
            {
                HodoRev = Path.Combine(SourceVisualize, prefix + "hodograph_rev.txt");
                HodoDirect = Path.Combine(SourceVisualize, prefix + "hodograph_dir.txt");
                File.Delete(HodoRev);
                File.Delete(HodoDirect);
                string header = string.Format(format_hodo_record + "\n", "XCenter", "Time", "Wave", "Trace", "Source", "X");
                File.AppendAllText(HodoRev, header);
                File.AppendAllText(HodoDirect, header);

            }
            if (options.HasFlag(ProcessingFlag.Interpolate))
            {
                InterTotal = Path.Combine(SourceVisualize, prefix + "inter_total_" + delta + ".txt");
                InterRev = Path.Combine(SourceVisualize, prefix + "inter_rev_" + delta + ".txt");
                InterDirect = Path.Combine(SourceVisualize, prefix + "inter_dir_" + delta + ".txt");
                File.Delete(InterTotal);
                File.Delete(InterRev);
                File.Delete(InterDirect);
                string header = string.Format(AnotherModel.format_interp + "\n",
                    "XCenter", "Time", "Offset", "SOffset", "Source");
                File.AppendAllText(InterTotal, header);
                File.AppendAllText(InterRev, header);
                File.AppendAllText(InterDirect, header);
            }

            string[] txinFiles = Directory.GetFiles(SourcePicking, "*.in");
            Profile p = new Profile();
            if (options.HasFlag(ProcessingFlag.Project))
                Profile.ExtractInstance(out p);

            foreach (string txin in txinFiles)
            {
                string file = Path.GetFileNameWithoutExtension(txin);
                if (!checkInput(options, txin))
                {
                    string msg = string.Format("Файл {0} обрабатывать не будем. Продолжить обработку?", file);
                    DialogResult res = MessageBox.Show(msg, "Error" , MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                    {
                        continue;
                    } else 
                    {
                        Writer.WriteLine("...Calculation was aborted.");
                        return;
                    }
                }
                Writer.WriteLine("...file {0} was checked.", file);

                if (options.HasFlag(ProcessingFlag.Project))
                    readSeiSeeHeader(Path.Combine(SourceHeaders, file), p);

                readSeisWideHeader(Path.Combine(SourceHeaders, file + ".txt"));
                processTxin(txin, options);

                if (options.HasFlag(ProcessingFlag.Interpolate))
                {
                    model.Interpolate(delta);
                    model.WriteInterpolation(InterTotal, InterRev, InterDirect);
                }
            }

            string last = string.Format(format_txin_separator,
                    0.0, 0.0, 0.0, -1);
            File.AppendAllText(TotalBinding, last);
            Writer.WriteLine("...File {0} was assembled.", Path.GetFileName(TotalBinding));

            if (options.HasFlag(ProcessingFlag.SplitHodographs))
            {
                Writer.WriteLine("...File {0} was made.", Path.GetFileName(HodoRev));
                Writer.WriteLine("...File {0} was made.", Path.GetFileName(HodoDirect));
            }

            if (options.HasFlag(ProcessingFlag.Interpolate))
            {
                Writer.WriteLine("Interpolation complete.");
            }
        }


        private bool checkInput(ProcessingFlag options, string txin)
        {
            string file = Path.GetFileNameWithoutExtension(txin);
            if (options.HasFlag(ProcessingFlag.Project) && !File.Exists(Path.Combine(SourceHeaders, file)))
            {
                Writer.WriteLine("Missing SeiSee header for {0}", file);
                return false;
            }

            if (!File.Exists(Path.Combine(SourceHeaders, Path.ChangeExtension(file, ".txt"))))
            {
                Writer.WriteLine("Missing SeisWide header for {0}", file);
                return false;
            }

            // SeisWide may bind traces incorrectly. There are may be two lines in 'tx.in' file with 
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
        /// Finds projection of every trace from SeiSee header.
        /// </summary>
        /// <param name="headerSeiSee">Path to SeiSee header.</param>
        private void readSeiSeeHeader(string headerSeiSee, Profile p)
        {
            distToProj.Clear();

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
            int x = int.Parse(recordWithSource[sourceXColumn]);
            int y = int.Parse(recordWithSource[sourceYColumn]);
            distToProj.Add(-1, p.getProjection(x, y) / 1000.0);

            while (++i < lines.Length)
            {
                string[] record = lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                int d = int.Parse(record[dist]);
                x = int.Parse(record[groupXColumn]);
                y = int.Parse(record[groupYColumn]);

                if (!distToProj.ContainsKey(d))
                    distToProj.Add(d, p.getProjection(x, y) / 1000.0);
            }
        }

        private void readSeisWideHeader(string headerSeisWide)
        {
            traceToDist.Clear();
            string[] lines = File.ReadAllLines(headerSeisWide);

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
                double offsetInKM = double.Parse(record[4]);
                int offsetInM = (int)Math.Round(offsetInKM * 1000);

                traceToDist.Add(trace, new Tuple<int, int>(cdp, offsetInM));
                i++;
            }
        }

        private void processTxin(string txin, ProcessingFlag options, int reflWave = 3)
        {
            string[] lines = File.ReadAllLines(txin);
            string outFile = Path.Combine(SourceBinding, Path.GetFileName(txin));

            StringBuilder sbTotalBind = new StringBuilder();
            StringBuilder sbRev = new StringBuilder();
            StringBuilder sbDir = new StringBuilder();
            StringBuilder sbReflTotal = new StringBuilder();
            StringBuilder sbReflRev = new StringBuilder();
            StringBuilder sbReflDir = new StringBuilder();

            if (options.HasFlag(ProcessingFlag.Interpolate))
            {
                model.Clear();
            }

            using (StreamWriter file = new StreamWriter(outFile))
            {
                Writer.WriteLine("...creating file:\t{0}", Path.GetFileName(outFile));
                string result;
                double pv = 0;
                bool isLeft = true;

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    string[] record = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (record.Length < 4)
                    {
                        string msg = string.Format(
                            "Файл: {0}{1}Не удалось прочесть следующую строку:{1}>{2}<{1}" +
                            "Продолжить обработку, пропустив данную строку?",
                            Path.GetFileName(txin), Environment.NewLine, line);
                        if (MessageBox.Show(msg, "Предупреждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            continue;
                        else
                        {
                            Writer.WriteLine("......Processing aborted for file {0}", Path.GetFileName(txin));
                            return;
                        }
                    }

                    double x = double.Parse(record[0]);
                    double time = double.Parse(record[1]);
                    double unc = double.Parse(record[2]);       // uncertainty value
                    int wave = int.Parse(record[3]);

                    if (record.Length == 4)
                    {
                        result = string.Format(format_txin_separator,
                                x, time, unc, wave);
                        file.WriteLine(result);

                        if (wave != -1)
                        {
                            isLeft = (time < -0);
                            pv = x;
                            //currPV = (options.HasFlag(ProcessingFlag.Project)) ? distToProj[-1] : pv;

                            sbTotalBind.Append(result).AppendLine();

                            if (options.HasFlag(ProcessingFlag.Project))
                                model.Initialize(txin, pv, distToProj[-1]);
                            else
                                model.Initialize(txin, pv);
                        }
                        continue;
                    }

                    int trace = int.Parse(record[4]);
                    if (traceToDist.ContainsKey(trace))
                        x = ((traceToDist[trace].Item1) / 1000.0);
                    result = string.Format(format_txin_record,
                            x, time, unc, wave, trace);

                    file.WriteLine(result);
                    sbTotalBind.Append(result).AppendLine();

                    if (options.HasFlag(ProcessingFlag.SquaredTimes) && wave == reflWave)
                    {
                        double xc = (pv + x) / 2;
                        double l = x - pv;
                        string reflResult = string.Format(format_refl_squared_times,
                            xc, time * time, l * l, trace, pv, x);
                        sbReflTotal.AppendLine(reflResult);
                        (isLeft ? sbReflRev : sbReflDir).AppendLine(reflResult);
                    }

                    double currX =
                        (options.HasFlag(ProcessingFlag.Project)) ? distToProj[traceToDist[trace].Item2] : x;

                    if (wave == 1 || wave == 2)
                    {
                        if (options.HasFlag(ProcessingFlag.SplitHodographs))
                        {

                            double xCenter = (pv + currX) / 2;
                            result = string.Format(format_hodo_record,
                                xCenter, time, wave, trace, pv, currX);

                            (isLeft ? sbRev : sbDir).AppendLine(result);
                        }

                        if (options.HasFlag(ProcessingFlag.Interpolate))
                        {
                            model.AddRecord(x, time, wave, trace, currX);
                        }
                    }
                }
            }

            File.AppendAllText(TotalBinding, sbTotalBind.ToString());
            if (options.HasFlag(ProcessingFlag.SplitHodographs))
            {
                File.AppendAllText(HodoRev, sbRev.ToString());
                File.AppendAllText(HodoDirect, sbDir.ToString());
            }
            if (options.HasFlag(ProcessingFlag.SquaredTimes))
            {
                File.AppendAllText(ReflectedTotal, sbReflTotal.ToString());
                File.AppendAllText(ReflectedRev, sbReflRev.ToString());
                File.AppendAllText(ReflectedDirect, sbReflDir.ToString());
            }
        }

        public void ProcessReflectedWave(ProcessingFlag options, int wave)
        {
            cleanSubdir(SourceBinding);
            if (options.HasFlag(ProcessingFlag.SquaredTimes))
            {
                File.Delete(ReflectedRev);
                File.Delete(ReflectedDirect);
                File.Delete(ReflectedTotal);
                string header = string.Format(format_refl_squared_times + "\n",
                    "XCenter", "SqrTime", "SqrOffset", "Trace", "Source", "X");
                File.AppendAllText(ReflectedTotal, header);
                File.AppendAllText(ReflectedRev, header);
                File.AppendAllText(ReflectedDirect, header);
            }

            string[] txinFiles = Directory.GetFiles(SourcePicking, "*.in");
            foreach (string txin in txinFiles)
            {
                string file = Path.GetFileNameWithoutExtension(txin);
                if (!checkInput(options, txin))
                {
                    MessageBox.Show(string.Format("Файл {0} обрабатывать не будем.", file), "Error");
                    continue;
                }
                Writer.WriteLine("...file {0} was checked.", file);

                processTxin(txin, options, wave);
            }

            string last = string.Format(format_txin_separator,
                    0.0, 0.0, 0.0, -1);
            File.AppendAllText(TotalBinding, last);
            Writer.WriteLine("...File {0} was assembled.", Path.GetFileName(TotalBinding));

            if (options.HasFlag(ProcessingFlag.SquaredTimes))
            {
                Writer.WriteLine("...File {0} was assembled.", Path.GetFileName(ReflectedTotal));
                Writer.WriteLine("...File {0} was assembled.", Path.GetFileName(ReflectedRev));
                Writer.WriteLine("...File {0} was assembled.", Path.GetFileName(ReflectedDirect));
            }
        }

        public void ProcessContours(ProcessingFlag options, string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            int deltaKm;
            double sqrTime;
            string sqrOffset_string = string.Empty;
            int sqrOffset = 0;

            SortedList<int, List<double>> pyramid = new SortedList<int, List<double>>();
            List<int> offsets = new List<int>();

            foreach (string line in lines)
            {
                // rec here consists of 'deltaKm' - 'squared time' - 'value on contour in squared deltaKm'
                string[] rec = line.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (!int.TryParse(rec[0], out deltaKm))      // we need only integer values of deltaKm
                    continue;

                sqrTime = double.Parse(rec[1]);

                if (sqrOffset_string != rec[2])
                {
                    sqrOffset_string = rec[2];
                    sqrOffset = int.Parse(sqrOffset_string);
                    offsets.Add(sqrOffset);
                }

                if (!pyramid.ContainsKey(deltaKm))
                    pyramid.Add(deltaKm, new List<double>());

                var slice = pyramid[deltaKm];
                int diff = offsets.Count - slice.Count;
                if (diff > 0)
                {
                    while (--diff > 0)
                        slice.Add(0);

                    slice.Add(sqrTime);    
                }
                else
                {
                    string msg = string.Format("Что-то пошло не так. Километр {0} встретился еще раз. \n{1}", deltaKm, line);
                    MessageBox.Show(msg, "Ошибка");
                }
            }

            string template = Path.Combine(Folder, Path.GetFileNameWithoutExtension(filename));
            string fileVelocities = Path.ChangeExtension(filename, "velocities.txt");
            bool printEveryVelocity = options.HasFlag(ProcessingFlag.ShowPyramid);
            var meanVels = calculateMeanVelocities(pyramid, offsets, fileVelocities, printEveryVelocity);

            if (options.HasFlag(ProcessingFlag.ShowPyramid))
            {
                printPyramid(pyramid, offsets, Path.ChangeExtension(filename, "cleaned_up.txt"));
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0,5} {1,6}\n", "km", "depth");
            foreach (var pair in meanVels)
            {
                int km = pair.Key;
                var slice = pyramid[km];
                int index = slice.Count - 1;
                double time_sqr = slice[index];
                int offset_sqr = offsets[index];
                double vel = pair.Value;

                double depth = -Math.Sqrt(vel*vel * time_sqr - offset_sqr) / 2;

                sb.AppendFormat("{0,5} {1:F3}", km, depth).AppendLine();
            }

            File.WriteAllText(Path.ChangeExtension(filename, "depth.txt"), sb.ToString());
        }


        private SortedList<int, double> calculateMeanVelocities
                (SortedList<int, List<double>> pyramid, IList<int> offsets, string output, bool printEverything)
        {

            SortedList<int, List<double>> velPyramid = new SortedList<int, List<double>>();

            foreach (var pair in pyramid)
            {
                if (pair.Value.Count <= 1)
                    continue;

                velPyramid.Add(pair.Key, new List<double>(offsets.Count));
                for (int i = 0; i < pair.Value.Count - 1; i++)
                {
                    double velocity = Math.Sqrt((offsets[i + 1] - offsets[i]) / (pair.Value[i + 1] - pair.Value[i]));
                    velPyramid[pair.Key].Add(velocity);
                }
            }

            SortedList<int, double> meanVelocities = new SortedList<int, double>();

            StringBuilder sb = new StringBuilder();

            string ffst = " {0,6}";
            string intgr = " {0,4}";
            string dbl = " {0,6:F2}";
            sb.Append("  km ").Append("  mean ");

            if (printEverything)
            {
                foreach (int offset in offsets)
                    sb.Append(string.Format(ffst, offset));
            }
            sb.AppendLine();

            foreach (var pair in velPyramid)
            {
                sb.Append(string.Format(intgr, pair.Key));
                double mean_vel = pair.Value.Sum() / pair.Value.Count;
                meanVelocities.Add(pair.Key, mean_vel);
                sb.AppendFormat(dbl, mean_vel);

                if (printEverything)
                {
                    foreach (double sqr_time in pair.Value)
                        sb.Append(string.Format(dbl, sqr_time));
                }
                sb.AppendLine();
            }

            File.WriteAllText(output, sb.ToString());
            Writer.WriteLine("...File " + output + " was made.");

            return meanVelocities;
        }

        private void printPyramid(SortedList<int, List<double>> pyramid, IList<int> offsets, string output)
        {
            string ffst = " {0,15}";
            string intgr = " {0,4}";
            string dbl = " {0,15:F8}";

            StringBuilder sb = new StringBuilder();
            sb.Append("  km ");

            foreach (int offset in offsets)
                sb.Append(string.Format(ffst, offset));
            sb.AppendLine();

            foreach (var pair in pyramid)
            {
                sb.Append(string.Format(intgr, pair.Key));
                foreach (double sqr_time in pair.Value)
                    sb.Append(string.Format(dbl, sqr_time));
                sb.AppendLine();
            }

            File.WriteAllText(output, sb.ToString());
            Writer.WriteLine("...File " + output + " was made.");
        }
    }
}
