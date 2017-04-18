using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeisWide_Surfer
{
    class SortedArrayModel : IModel
    {

        private List<Record> records = new List<Record>();
        private SortedList<double, Record> sortedRecords = new SortedList<double, Record>();

        public SortedArrayModel()
            : base()
        {
        }

        public override string IntermediateLabel() 
        { 
            return string.Format("Количество записей - {0}", records.Count); 
        }

        public override string FirstEntry()
        {
            StringBuilder sb = new StringBuilder();

            foreach (Record rec in records)
                sb.AppendLine(rec.ToString());

            return sb.ToString();
        }

        public override void ExtractFirstEntry()
        {
            sortedRecords.Clear();

            for (int i = 0; i < stations.Count; i++)
            {
                if (waveNum[i] == -1)
                    break;
                if (waveNum[i] >= 3)
                    continue;
                if (waveNum[i] == 0)
                    times[i] = 0;

                
                double key = ProjectionsEnabled ? offsets[i] : distances[i];
                if (sortedRecords.ContainsKey(key))
                {
                    bool shouldSwap = times[i] < sortedRecords[key].Time;
                    if (shouldSwap)
                    {
                        sortedRecords[key].Time = times[i];
                        sortedRecords[key].Wave = waveNum[i];
                    }
                }
                else
                {
                    Record rec = ProjectionsEnabled ?
                        new Record(distances[i], times[i], waveNum[i], stations[i], x0, offsets[i]) :
                        new Record(distances[i], times[i], waveNum[i], stations[i], x0);
                    sortedRecords.Add(key, rec);
                }
            }

            // at this point we still could add extra records bc of precision issues of 'double' type 
            // and therefore not that strict relation 'channel'->'distance'
            // i.e. records corresponding to one channel with various wave numbers 
            // may have slightly different 'distance' value, which shouldn't be the case.

            // UPD: ^ irrelevant because now two functions exist who control over 
            // strictness of relation 'channel'->'distance'.

            records.Clear();
            records.AddRange(sortedRecords.Values);
        }

        public override void Interpolate(double timeDelta)
        {
            if (records.Count <= 1)
            {
                MessageBox.Show(string.Format("{0}\n\t{1}\n{2}",
                    "Не было выделено записей при обработке файла", Source, "Проверьте, штатная ли это ситуация."),
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
    }
}