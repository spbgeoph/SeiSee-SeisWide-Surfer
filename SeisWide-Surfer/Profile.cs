using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeisWide_Surfer
{
    /// <summary>
    /// Class Profile allows to calculate projection of a point on profile line 
    /// </summary>
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

        /// <summary>
        /// Gets instance of Profile object taking values from settings.
        /// </summary>
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

        /// <summary>
        /// Sets up all intermediate constants required for calculation of projections.
        /// </summary>
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

        /// <summary>
        /// Calculates projection of the point on the line of profile.
        /// </summary>
        /// <param name="x">X coordinate of the point.</param>
        /// <param name="y">Y coordinate of the point.</param>
        /// <returns>Value of projection.</returns>
        public double getProjection(double x, double y)
        {
            return (-sign * (A1 * x + B1 * y + C1) / R1 + L0);
        }

        public double getProjection(Tuple<int, int> point)
        {
            return getProjection(point.Item1, point.Item2);
        }

        /// <summary>
        /// Returns string representation of the profile parameters.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string basic = string.Format("S1: ({0}, {1}){7}S2: ({2}, {3}){7}N1: ({4}, {5}){7}L: {6}{7}",
                S1_X, S1_Y, S2_X, S2_Y, N1_X, N1_Y, L0, Environment.NewLine);

            string aux1 = string.Format("A1: {0}{4}B1: {1}{4}C1: {2}{4}R1 {3}",
                A1, B1, C1, R1, Environment.NewLine);

            return basic + aux1;
        }
    }
}
