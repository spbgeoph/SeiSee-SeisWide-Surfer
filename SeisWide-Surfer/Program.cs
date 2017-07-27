using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Resources;
using System.IO;

namespace SeisWide_Surfer
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // This line is supposed to fight against commas.
            // In our application we use DOT as a decimal separator.
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            // Here we try to get settings saved in the earlier versions of program.
            if (Properties.Settings.Default.UpgradeRequired)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UpgradeRequired = false;
                Properties.Settings.Default.Save();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AnotherForm());
        }

        private static readonly string profileData = "_profile"; 

        public static void ReadProfileData(string folder)
        {
            string file = Path.Combine(folder, profileData);
            if (!File.Exists(file))
            {
                return;
            }

            var lines = File.ReadLines(file);

            string[] record = lines.First().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string errorMsg = "Не удалось прочесть параметры створа профиля. Возможно, файл повреждён. ";
            if (record.Length < 7)
            {
                MessageBox.Show(errorMsg + "Координат меньше, чем положено.", "Предупреждение");
                return;
            }

            // Should we ensure that these 'records' are actually numbers?
            Properties.Settings.Default.S1_X = record[0];
            Properties.Settings.Default.S1_Y = record[1];
            Properties.Settings.Default.S2_X = record[2];
            Properties.Settings.Default.S2_Y = record[3];
            Properties.Settings.Default.N1_X = record[4];
            Properties.Settings.Default.N1_Y = record[5];
            Properties.Settings.Default.L = record[6];
        }

        public static void WriteProfileData(string folder)
        {
            if (!Directory.Exists(folder))
                return;

            string file = Path.Combine(folder, profileData);
            string result = string.Format("{0} {1} {2} {3} {4} {5} {6}",
                Properties.Settings.Default.S1_X,
                Properties.Settings.Default.S1_Y,
                Properties.Settings.Default.S2_X,
                Properties.Settings.Default.S2_Y,
                Properties.Settings.Default.N1_X,
                Properties.Settings.Default.N1_Y,
                Properties.Settings.Default.L
                );
            File.WriteAllText(file, result);
        }
    }
}
