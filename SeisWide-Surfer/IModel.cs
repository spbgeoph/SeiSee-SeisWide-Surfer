using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SeisWide_Surfer
{
    public abstract class IModel
    {
        protected string origin;
        protected double x0;
        protected List<double> distances = new List<double>();
        protected List<double> times = new List<double>();
        protected List<int> waveNum = new List<int>();
        protected List<int> stations = new List<int>();
        protected List<double> offsets = new List<double>();

        protected List<OutRecord> interpolation = new List<OutRecord>();

        protected bool OffsetEnabled { get { return offsets.Count > 0; } }

        public IModel()
        {
        }

        public void Clear()
        {
            origin = string.Empty;
            x0 = 0;
            distances.Clear();
            times.Clear();
            waveNum.Clear();
            stations.Clear();
            offsets.Clear();
            interpolation.Clear();
        }

        public void Initialize(string textFile, bool withProjections)
        {
            this.Clear();
            origin = textFile;
            string[] lines = File.ReadAllLines(textFile);

            bool directHodographIncoming = false;
            foreach (string line in lines)
            {
                string[] record = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if ("-1".Equals(record[3]))
                {
                    return;
                }
                else if ("0".Equals(record[3]))
                {
                    if (!directHodographIncoming)
                    {
                        directHodographIncoming = true;
                        continue;
                    }
                    else
                    {
                        x0 = double.Parse(record[0]);
                        distances.Add(x0);
                        times.Add(0);
                        waveNum.Add(int.Parse(record[3]));
                        stations.Add(0);
                        if (withProjections)
                            offsets.Add(double.Parse(record[4]));
                        directHodographIncoming = false;
                    }
                }
                else 
                {
                    distances.Add(double.Parse(record[0]));
                    times.Add(double.Parse(record[1]));
                    waveNum.Add(int.Parse(record[3]));
                    stations.Add(int.Parse(record[4]));
                    if (withProjections)
                        offsets.Add(double.Parse(record[5]));
                }
            }
        }

        public void CheckInterpolation(double delta)
        {
            for (int i = 1; i < interpolation.Count; i++)
            {
                if (Math.Abs(interpolation[i].Time - interpolation[i - 1].Time) >= delta + 0.00001)
                {
                    MessageBox.Show(string.Format("{0}\n{1} - {2}\n{3}\n{4}\n{5}\n{6} - {7}",
                                    "Осторожно: длина шага временной сетки при интерполяции не осталась неизменной.",
                                    "Значение шага", 
                                    delta,
                                    "Узлы интерполяции:",
                                    interpolation[i - 1], 
                                    interpolation[i],
                                    "Наблюдаемая разность",
                                    Math.Abs(interpolation[i].Time - interpolation[i - 1].Time)),
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

        public string InterpolationResult()
        {
            StringBuilder sb = new StringBuilder();
            
            foreach (OutRecord r in interpolation)
            {
                string str = string.Format("{0,9:F3} {1,9:F3} {2,9:F3} {3,9:F3} {4,9:F3}", 
                                            r.XCenter, 
                                            r.Time, 
                                            Math.Abs(r.Offset), 
                                            r.Offset, 
                                            x0);

                sb.AppendFormat(str).AppendLine();
            }
            return sb.ToString();
        }

        public string FirstEntryHeader 
        {
            get
            {
                return string.Format("{0,10} {1,8} {2,9} {3,8} {4,8} {5,8}\n",
                    "X", "XCenter", "Time", "Wave", "Trace", OffsetEnabled ? "Projection" : "Offset");
            }
        }


        public string InterpolationLabel { get {return string.Format("Количество узлов интерполяции - {0}", interpolation.Count);} }
        

        public abstract void ExtractFirstEntry();
        public abstract string FirstEntry();
        public abstract void Interpolate(double timeDelta);

        public abstract string IntermediateLabel();
    }

    public class Record
    {
        public double Distance  { get; set; }
        public double XCenter   { get; set; }
        public double Time      { get; set; }
        public int Wave         { get; set; }
        public int Station      { get; set; }
        public double Offset    { get; set; }


        private Record(bool offsetEnabled, double _time, int _wave, int _station, double x0, double _dist, double _offset)
        {
            Distance = offsetEnabled ? x0 + _offset : _dist;
            XCenter = offsetEnabled ? x0 + _offset / 2 : (_dist + x0) / 2;
            Time = _time;
            Wave = _wave;
            Station = _station;
            Offset = offsetEnabled ? _offset : (_station == 0 ? 0 : _dist - x0);
        }

        public Record(double _dist, double _time, int _wave, int _station, double x0)
        {
            Distance = _dist;
            XCenter = (_dist + x0) / 2;
            Time = _time;
            Wave = _wave;
            Station = _station;
            Offset = _station == 0 ? 0 : _dist - x0;
        }

        public Record(double _dist, double _time, int _wave, int _station, double x0, double _offset)
        {
            Distance = _dist;
            XCenter = (x0 + _offset) / 2;
            Time = _time;
            Wave = _wave;
            Station = _station;
            Offset = _station == 0 ? 0 : _dist - x0;
            //Offset = _offset;
        }

        public override string ToString()
        {
            return string.Format("{0,10:F3} {1,8:F3} {2,9:F3} {3,8} {4,8} {5,8:F3}",
                Distance, XCenter, Time, Wave, Station, Offset);
        }

        public class DistanceComparer : IComparer<Record>
        {

            public int Compare(Record r1, Record r2)
            {
                return (int)Math.Sign(r1.Distance - r2.Distance);
            }
        }

        public class TimeComparer : IComparer<Record>
        {

            public int Compare(Record r1, Record r2)
            {
                return (int)Math.Sign(r1.Time - r2.Time);
            }
        }
    }

    public class OutRecord
    {
        public double XCenter { get; set; }
        public double Time { get; set; }
        public double Offset { get; set; }

        public override string ToString()
        {
            return string.Format("{0,10:F4}\t{1:F3}\t{2:F3}", XCenter, Time, Offset);
        }
    }
}
