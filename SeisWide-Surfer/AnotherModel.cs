using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeisWide_Surfer
{
    class AnotherModel
    {
        public static readonly string format_interp = "{0,9:F3} {1,9:F3} {2,9:F3} {3,9:F3} {4,9:F3}";

        private double x0;

        private SortedList<double, Record> sortedRecords = new SortedList<double, Record>();
        private List<Record> records = new List<Record>();
        private List<OutRecord> interpolation = new List<OutRecord>();

        private TextWriter Writer { get; set; }

        /// <summary>
        /// Name of file model is working with right now.
        /// </summary>
        private string SourceFile { get; set; }

        /// <summary>
        /// Value which tells, if model is going to make calculations taking projections data into account.
        /// </summary>
        private bool ProjectionsEnabled { get; set; }

        public void Clear()
        {
            SourceFile = string.Empty;
            x0 = 0;
            sortedRecords.Clear();
            records.Clear();
            interpolation.Clear();
        }

        public void Initialize(string textFile, double pv, double pvProj)
        {
            if (!string.IsNullOrWhiteSpace(SourceFile))
                return;

            SourceFile = textFile;
            x0 = pv;
            ProjectionsEnabled = true;
            sortedRecords.Add(x0, new Record(x0, 0, 0, 0, x0, pvProj));
        }

        public void Initialize(string textFile, double pv)
        {
            if (!string.IsNullOrWhiteSpace(SourceFile))
                return;

            SourceFile = textFile;
            x0 = pv;
            ProjectionsEnabled = false;
            sortedRecords.Add(x0, new Record(x0, 0, 0, 0, x0));
        }

        public void AddRecord(double x, double time, int wave, int trace, double currX)
        {
            double key = ProjectionsEnabled ? currX : x;
            if (sortedRecords.ContainsKey(key))
            {
                bool shouldSwap = time < sortedRecords[key].Time;
                if (shouldSwap)
                {
                    sortedRecords[key].Time = time;
                    sortedRecords[key].Wave = wave;
                }
            }
            else
            {
                Record rec = ProjectionsEnabled ?
                    new Record(x, time, wave, trace, x0, currX) :
                    new Record(x, time, wave, trace, x0);
                sortedRecords.Add(key, rec);
            }
        }

        public void Interpolate(double timeDelta)
        {
            records.Clear();
            records.AddRange(sortedRecords.Values);

            if (records.Count <= 1)
            {
                MessageBox.Show(string.Format("{0}\n\t{1}\n{2}",
                    "Не было выделено записей при обработке файла", SourceFile, "Проверьте, штатная ли это ситуация."),
                "Предупреждение");
                return;
            }

            int sign;
            double temp, delta, xCenter, offset;
            bool flag = false;
            for (int k = 1; k < records.Count; k++)
            {
                Record r = records[k - 1];
                Record rNext = records[k];
                sign = Math.Sign(rNext.Time - r.Time);
                delta = timeDelta * sign;

                // okay, we got to mess here with dem 'flags' and 'signs'.
                //      'flag' equalling 'TRUE' here means that 'r.Time' value is a multiple of 'timeDelta' value
                // AND has been already saved in 'interpolation' list, so we need to make one step further (i.e. add 'timeDelta')
                // in order to not duplicate an interpolation node.
                //      'sign' 
                temp = flag ?
                    (r.Time + delta) :
                    timeDelta * (Math.Floor(r.Time / timeDelta) + (sign > 0 ? 1 : 0));
                flag = false;

                while (true)
                {
                    OutRecord outR;
                    if (Math.Abs(rNext.Time - temp) <= 0.00001)
                    {
                        outR = new OutRecord() { XCenter = rNext.XCenter, Time = rNext.Time, Offset = rNext.Projection };
                        interpolation.Add(outR);
                        flag = true;
                        break;
                    }
                    if (sign * (rNext.Time - temp) < 0)
                        break;
                    else
                    {
                        xCenter = getDist(temp, r.Time, rNext.Time, r.XCenter, rNext.XCenter);
                        offset = getDist(temp, r.Time, rNext.Time, r.Projection, rNext.Projection);
                        outR = new OutRecord() { XCenter = xCenter, Time = temp, Offset = offset };
                        interpolation.Add(outR);
                        temp += delta;
                    }
                }
            }
        }

        /// <summary>
        /// Checks interpolated data if difference between 'time' values of two adjacent records equals to predefined 'timeDelta'.
        /// If this difference is not constant, message box then appears.
        /// </summary>
        /// <param name="timeDelta">Interpolation step.</param>
        public void CheckInterpolation(double timeDelta)
        {
            for (int i = 1; i < interpolation.Count; i++)
            {
                if (Math.Abs(interpolation[i].Time - interpolation[i - 1].Time) >= timeDelta + 0.00001)
                {
                    MessageBox.Show(string.Format("\t{8}\n{0}\n{1} - {2}\n{3}\n{4}\n{5}\n{6} - {7}",
                                    "Осторожно: длина шага временной сетки при интерполяции не осталась неизменной.",
                                    "Значение шага",
                                    timeDelta,
                                    "Узлы интерполяции:",
                                    interpolation[i - 1],
                                    interpolation[i],
                                    "Наблюдаемая разность",
                                    Math.Abs(interpolation[i].Time - interpolation[i - 1].Time),
                                    SourceFile),
                        "Предупреждение");
                    break;
                }
            }
        }

        /// <summary>
        /// Determines distance value corresponding 'time' parameter through using linear interpolation.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="t1">time value on the left interval</param>
        /// <param name="t2">time value on the right interval</param>
        /// <param name="x1">distance value on the left interval</param>
        /// <param name="x2">distance value on the right interval</param>
        /// <returns>distance value where signal is supposed to be registered in exactly 'time' after shot.</returns>
        protected double getDist(double time, double t1, double t2, double x1, double x2)
        {
            return x1 + (x2 - x1) * (time - t1) / (t2 - t1);
        }

        /// <summary>
        /// Returns string representation of the interpolation.
        /// </summary>
        /// <returns></returns>
        public string InterpolationResult()
        {
            StringBuilder sb = new StringBuilder();

            foreach (OutRecord r in interpolation)
            {
                string str = string.Format(format_interp,
                                            r.XCenter,
                                            r.Time,
                                            Math.Abs(r.Offset),
                                            r.Offset,
                                            x0);

                sb.AppendFormat(str).AppendLine();
            }
            return sb.ToString();
        }

        public void WriteInterpolation(string outTotal, string outRev, string outDirect)
        {
            StringBuilder sb = new StringBuilder();
            bool isReverse = (interpolation[1].Time - interpolation[0].Time) < 0;
            for (int i = 0; i < interpolation.Count; i++)
            {
                OutRecord r = interpolation[i];
                string str = string.Format(format_interp,
                    r.XCenter, r.Time, Math.Abs(r.Offset), r.Offset, x0);
                sb.Append(str).AppendLine();

                if (r.Time == 0 )
                {
                    if (isReverse)
                    {
                        string result = sb.ToString();
                        File.AppendAllText(outTotal, result);
                        File.AppendAllText(outRev, result);
                        sb.Clear();
                    }
                    else if (i > 0)
                    {
                        throw new InvalidDataException(
                            string.Format("File: {0}.\nRecord {1}\nGetting zero time in the DIRECT hodographs",
                            SourceFile, r));
                    }
                }
            }
            File.AppendAllText(outDirect, sb.ToString());
            File.AppendAllText(outTotal, sb.ToString());
        }


        public string FirstEntry()
        {
            StringBuilder sb = new StringBuilder();

            foreach (Record rec in records)
                sb.AppendLine(rec.ToString());

            return sb.ToString();
        }
    }
}
