using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeisWide_Surfer
{
    class LinkedListModel : IModel
    {
        private LinkedList<Record> records = new LinkedList<Record>();

        public LinkedListModel()
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


        private void convertToLinkedList()
        {
            // skipping zero since there is a delimiter record
            for (int i = 1; i < stations.Count; i++)
            {
                if (waveNum[i] == -1)
                    break;
                if (waveNum[i] == 0)
                    times[i] = 0;

                Record rec = ProjectionsEnabled ?
                        new Record(distances[i], times[i], waveNum[i], stations[i], x0, offsets[i]) :
                        new Record(distances[i], times[i], waveNum[i], stations[i], x0);
                records.AddLast(rec);
            }
        }

        public override void ExtractFirstEntry()
        {
            convertToLinkedList();

            Dictionary<int, LinkedListNode<Record>> waveBuffer = new Dictionary<int, LinkedListNode<Record>>();

            // on the reversed distance-time curve
            LinkedListNode<Record> temp = records.First;
            while (temp.Value.Station != 0)
            {
                switch (temp.Value.Wave)
                {
                    case 3:
                        temp = temp.Next;
                        records.Remove(temp.Previous);
                        continue;

                    case 2:
                        if (waveBuffer.ContainsKey(temp.Value.Station))
                        {
                            MessageBox.Show(string.Format("Номер трассы {0} встретился еще раз.", temp.Value.Station), "Ошибка");
                            return;
                        }
                        waveBuffer.Add(temp.Value.Station, temp);
                        temp = temp.Next;
                        break;

                    case 1:
                        if (waveBuffer.ContainsKey(temp.Value.Station))
                        {
                            bool isSecond = waveBuffer[temp.Value.Station].Value.Time < temp.Value.Time;
                            temp = temp.Next;

                            records.Remove(isSecond ? temp.Previous : waveBuffer[temp.Previous.Value.Station]);
                        }
                        else
                            temp = temp.Next;
                        break;
                }
            }

            waveBuffer.Clear();

            // On the direct distance-time curve.
            // Here we move from the end towards blast point.
            temp = records.Last;
            while (temp.Value.Station != 0)
            {
                switch (temp.Value.Wave)
                {
                    case 3:
                        temp = temp.Previous;
                        records.Remove(temp.Next);
                        continue;

                    case 2:
                        if (waveBuffer.ContainsKey(temp.Value.Station))
                        {
                            MessageBox.Show(string.Format("Номер трассы {0} встретился еще раз.", temp.Value.Station), "Ошибка");
                            return;
                        }
                        waveBuffer.Add(temp.Value.Station, temp);
                        temp = temp.Previous;
                        break;

                    case 1:
                        if (waveBuffer.ContainsKey(temp.Value.Station))
                        {
                            bool isSecond = waveBuffer[temp.Value.Station].Value.Time < temp.Value.Time;
                            temp = temp.Previous;

                            records.Remove(isSecond ? temp.Next : waveBuffer[temp.Value.Station]);
                        }
                        else
                            temp = temp.Previous;
                        break;
                }
            }
        }

        public override void Interpolate(double timeDelta)
        {
            if (records.Count <= 1)
            {
                MessageBox.Show("Не было выделено записей! Проверьте, всё ли настроено правильно.");
                return;
            }
            interpolation.Clear();

            // determining the time point 'tempTime' we are going to start interpolating from
            LinkedListNode<Record> temp = records.First;
            int sign = Math.Sign(temp.Next.Value.Time - temp.Value.Time);
            double N = Math.Floor(temp.Value.Time / timeDelta) + (sign > 0 ? 1 : 0);
            double tempTime = timeDelta * N;

            double delta, xCenter, offset;

            // while-loop iteraring through all extracted records
            do
            {
                Record r = temp.Value;
                Record rNext = temp.Next.Value;
                sign = Math.Sign(rNext.Time - r.Time);
                delta = timeDelta * sign;
                while (sign * (rNext.Time - tempTime) > 0)
                {
                    OutRecord rOut;
                    if (0 <= tempTime && tempTime <= 0.00001)
                    {
                        rOut = new OutRecord() { XCenter = r.Distance, Time = r.Time };
                        interpolation.Add(rOut);
                    }
                    else if (0 <= tempTime)
                    {
                        xCenter = getDist(tempTime, r.Time, rNext.Time, r.XCenter, rNext.XCenter);
                        offset = getDist(tempTime, r.Time, rNext.Time, r.Projection, rNext.Projection);

                        rOut = new OutRecord() { XCenter = xCenter, Time = tempTime, Offset = offset };
                        interpolation.Add(rOut);
                    }
                    tempTime += delta;
                }
                temp = temp.Next;

            } while (temp.Next != null);
        }
    }
}