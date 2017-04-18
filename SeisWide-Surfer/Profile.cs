using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeisWide_Surfer
{
    class Profile
    {
        public double S1_X { get; set; }
        public double S1_Y { get; set; }
        public double S2_X { get; set; }
        public double S2_Y { get; set; }
        public double N1_X { get; set; }
        public double N1_Y { get; set; }
        public double L0 { get; set; }

        private double sign, A1, B1, C1, R1;

        public static Profile ExtractInstance
        {
            get
            {
                var properties = Properties.Settings.Default;
                Profile p = new Profile()
                {
                    S1_X = double.Parse(properties.S1_X),
                    S1_Y = double.Parse(properties.S1_Y),
                    S2_X = double.Parse(properties.S2_X),
                    S2_Y = double.Parse(properties.S2_Y),
                    N1_X = double.Parse(properties.N1_X),
                    N1_Y = double.Parse(properties.N1_Y),
                    L0 = double.Parse(properties.L)
                };
                p.Init();

                return p;
            }
        }

        private void Init()
        {
            double dx = S2_X - S1_X;
            double dy = S2_Y - S1_Y;
            sign = Math.Sign(dx / dy);

            A1 = -1.0 / dy;
            B1 = -1.0 / dx;
            C1 = N1_X / dy + N1_Y / dx;
            R1 = Math.Sqrt(A1 * A1 + B1 * B1);
        }

        public double getProjection(double x, double y)
        {
            return (-sign * (A1 * x + B1 * y + C1) / R1 + L0);
        }

        public double getProjection(Tuple<int, int> tuple)
        {
            return getProjection(tuple.Item1, tuple.Item2);
        }

        public override string ToString()
        {
            string basic = string.Format("S1: ({0}, {1})\nS2: ({2}, {3})\nN1: ({4}, {5})\nL: {6}\n",
                S1_X, S1_Y, S2_X, S2_Y, N1_X, N1_Y, L0);

            string aux1 = string.Format("A1: {0}\nB1: {1}\nC1: {2}\nR1 {3}",
                A1, B1, C1, R1);

            return basic + aux1;
        }
    }
}
