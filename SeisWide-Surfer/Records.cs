using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeisWide_Surfer
{

    /// <summary>
    /// Class which contains information about one record from 'tx.in' or 'tx.in'-like file,
    /// such as distance, calculated XCenter, time, number of wave, number of trace and projection
    /// </summary>
    public class Record
    {
        public double Distance { get; set; }
        public double XCenter { get; set; }
        public double Time { get; set; }
        public int Wave { get; set; }
        public int Trace { get; set; }
        public double Projection { get; set; }


        /// <summary>
        /// Creates new record from 'tx.in' file.
        /// </summary>
        /// <param name="_dist">Distance.</param>
        /// <param name="_time">Time.</param>
        /// <param name="_wave">Number of wave.</param>
        /// <param name="_trace">Number of trace.</param>
        /// <param name="x0">PV, blast point, пункт взрыва.</param>
        public Record(double _dist, double _time, int _wave, int _trace, double x0)
        {
            Distance = _dist;
            XCenter = (x0 + _dist) / 2;
            Time = _time;
            Wave = _wave;
            Trace = _trace;
            Projection = _trace == 0 ? 0 : _dist - x0;
        }

        /// <summary>
        /// Creates new record from 'tx.in'-like file with additional projection column.
        /// </summary>
        /// <param name="_dist">Distance.</param>
        /// <param name="_time">Time.</param>
        /// <param name="_wave">Number of wave.</param>
        /// <param name="_trace">Number of trace.</param>
        /// <param name="x0">PV, blast point, пункт взрыва.</param>
        /// <param name="_proj">Projection. Used in calculation of XCenter.</param>
        public Record(double _dist, double _time, int _wave, int _trace, double x0, double _proj)
        {
            Distance = _dist;
            XCenter = (x0 + _proj) / 2;
            Time = _time;
            Wave = _wave;
            Trace = _trace;
            Projection = _trace == 0 ? 0 : _dist - x0;
            //Projection = _proj;
        }

        /// <summary>
        /// Returns string representation of Record object (overriden from object).
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0,10:F3} {1,8:F3} {2,9:F3} {3,8} {4,8} {5,8:F3}",
                Distance, XCenter, Time, Wave, Trace, Projection);
        }

        /// <summary>
        /// Record comparer which orders lines using Distance property.
        /// </summary>
        public class DistanceComparer : IComparer<Record>
        {

            public int Compare(Record r1, Record r2)
            {
                return (int)Math.Sign(r1.Distance - r2.Distance);
            }
        }

        /// <summary>
        /// Record comparer which orders lines using Time property.
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
