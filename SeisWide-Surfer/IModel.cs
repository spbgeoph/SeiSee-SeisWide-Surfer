using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SeisWide_Surfer
{

    /// <summary>
    /// Descendants of this abstract class can perform such calculations as first entry extraction and subsequent interpolation.
    /// This class can load data from 'tx.in' file or 'tx.in'-like file where additional column of projections is recorded.
    /// </summary>
    public abstract class IModel
    {
        protected double x0;
        protected List<double> distances = new List<double>();
        protected List<double> times = new List<double>();
        protected List<int> waveNum = new List<int>();
        protected List<int> stations = new List<int>();
        protected List<double> offsets = new List<double>();

        protected List<OutRecord> interpolation = new List<OutRecord>();

        /// <summary>
        /// Name of file model is working with right now.
        /// </summary>
        protected string Source { get; private set; }

        /// <summary>
        /// Value which tells, if model is going to make calculations taking projections data into account.
        /// </summary>
        protected bool ProjectionsEnabled { get { return offsets.Count > 0; } }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public IModel()
        {
        }

        /// <summary>
        /// Clears all the buffers.
        /// </summary>
        public void Clear()
        {
            Source = string.Empty;
            x0 = 0;
            distances.Clear();
            times.Clear();
            waveNum.Clear();
            stations.Clear();
            offsets.Clear();
            interpolation.Clear();
        }

        /// <summary>
        /// Fills buffers from provided text file. Flag 'withProjections' shows if there is an additional column 
        /// with projetions.
        /// </summary>
        /// <param name="textFile">Full name of 'tx.in' (or 'tx.in'-like) file with data.</param>
        /// <param name="withProjections">If it is set to true, method will try to read projection values from additional  
        ///     column in file. For regular 'tx.in' file use false.</param>
        public void Initialize(string textFile, bool withProjections)
        {
            this.Clear();
            Source = textFile;
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
                                    Source),
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
                    "X", "XCenter", "Time", "Wave", "Trace", ProjectionsEnabled ? "Projection" : "Offset");
            }
        }

        public string InterpolationLabel { get {return string.Format("Количество узлов интерполяции - {0}", interpolation.Count);} }
        
        /// <summary>
        /// From the loaded data extracts first entry (i.e. the 1st and the 2nd wave).
        /// </summary>
        public abstract void ExtractFirstEntry();

        /// <summary>
        /// Returns string representation of the first entry.
        /// </summary>
        /// <returns></returns>
        public abstract string FirstEntry();

        /// <summary>
        /// Interpolates first entry in 'time' domain with given step of interpolaton.
        /// </summary>
        /// <param name="timeDelta">Step of interpolation.</param>
        public abstract void Interpolate(double timeDelta);

        /// <summary>
        /// Some string label, information about first-entry-extraction phase i.e. number of extracted records
        /// </summary>
        /// <returns></returns>
        public abstract string IntermediateLabel();
    }

    /// <summary>
    /// Class which contains information about one record from 'tx.in' or 'tx.in'-like file,
    /// such as distance, calculated XCenter, time, number of wave, number of trace and projection
    /// </summary>
    public class Record
    {
        public double Distance      { get; set; }
        public double XCenter       { get; set; }
        public double Time          { get; set; }
        public int Wave             { get; set; }
        public int Station          { get; set; }
        public double Projection    { get; set; }

        /// <summary>
        /// Creates new record.
        /// </summary>
        /// <param name="projectionEnabled"> If this flag is set to true, Distance and XCenter are going to be 
        ///     calculated from _proj</param>
        /// <param name="_time">Time.</param>
        /// <param name="_wave">Number of wave.</param>
        /// <param name="_station">Number of trace.</param>
        /// <param name="x0">PV, blast point, пункт взрыва.</param>
        /// <param name="_dist">Distance.</param>
        /// <param name="_proj">Projection.</param>
        private Record(bool projectionEnabled, double _time, int _wave, int _station, double x0, double _dist, double _proj)
        {
            Distance = projectionEnabled ? x0 + _proj : _dist;
            XCenter = projectionEnabled ? x0 + _proj / 2 : (_dist + x0) / 2;
            Time = _time;
            Wave = _wave;
            Station = _station;
            Projection = projectionEnabled ? _proj : (_station == 0 ? 0 : _dist - x0);
        }

        /// <summary>
        /// Creates new record from 'tx.in' file.
        /// </summary>
        /// <param name="_dist">Distance.</param>
        /// <param name="_time">Time.</param>
        /// <param name="_wave">Number of wave.</param>
        /// <param name="_station">Number of trace.</param>
        /// <param name="x0">PV, blast point, пункт взрыва.</param>
        public Record(double _dist, double _time, int _wave, int _station, double x0)
        {
            Distance = _dist;
            XCenter = (x0 + _dist) / 2;
            Time = _time;
            Wave = _wave;
            Station = _station;
            Projection = _station == 0 ? 0 : _dist - x0;
        }

        /// <summary>
        /// Creates new record from 'tx.in'-like file with additional projection column.
        /// </summary>
        /// <param name="_dist">Distance.</param>
        /// <param name="_time">Time.</param>
        /// <param name="_wave">Number of wave.</param>
        /// <param name="_station">Number of trace.</param>
        /// <param name="x0">PV, blast point, пункт взрыва.</param>
        /// <param name="_proj">Projection. Used in calculation of XCenter.</param>
        public Record(double _dist, double _time, int _wave, int _station, double x0, double _proj)
        {
            Distance = _dist;
            XCenter = (x0 + _proj) / 2;
            Time = _time;
            Wave = _wave;
            Station = _station;
            Projection = _station == 0 ? 0 : _dist - x0;
            //Projection = _proj;
        }

        /// <summary>
        /// Returns string representation of Record object (overriden from object).
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0,10:F3} {1,8:F3} {2,9:F3} {3,8} {4,8} {5,8:F3}",
                Distance, XCenter, Time, Wave, Station, Projection);
        }

        /// <summary>
        /// Record comparer which orders records using Distance property.
        /// </summary>
        public class DistanceComparer : IComparer<Record>
        {

            public int Compare(Record r1, Record r2)
            {
                return (int)Math.Sign(r1.Distance - r2.Distance);
            }
        }

        /// <summary>
        /// Record comparer which orders records using Time property.
        /// </summary>
        public class TimeComparer : IComparer<Record>
        {

            public int Compare(Record r1, Record r2)
            {
                return (int)Math.Sign(r1.Time - r2.Time);
            }
        }
    }

    /// <summary>
    /// Class representing one record of interpolation.
    /// </summary>
    public class OutRecord
    {
        public double XCenter { get; set; }
        public double Time { get; set; }
        public double Offset { get; set; }

        /// <summary>
        /// Returns string representaion of OutRecord. (Overriden from object method).
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0,10:F4}\t{1:F3}\t{2:F3}", XCenter, Time, Offset);
        }
    }
}
