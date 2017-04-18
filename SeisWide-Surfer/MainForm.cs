using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Resources;

namespace SeisWide_Surfer
{
    public partial class MainForm : Form
    {

        Manipulator man = new Manipulator();


        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            updateTitles();
        }

        private void openFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlgF = new FolderBrowserDialog();
            if (!string.IsNullOrEmpty(pathToFolder.Text))
                dlgF.SelectedPath = pathToFolder.Text;
            
            if (dlgF.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(dlgF.SelectedPath))
            {
                pathToFolder.Text = dlgF.SelectedPath;
                man.SelectWorkspace(dlgF.SelectedPath);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SeisWide_Surfer.Properties.Settings.Default.Save();
        }

        private void openInButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "tx.in file (.in)|*.in";
            dialog.FilterIndex = 1;
            if (Directory.Exists(man.SourceTXIN))
                dialog.InitialDirectory = man.SourceTXIN;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string file = dialog.FileName;
                pathToTXIN.Text = file;
            }
        }

        private void openSWButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "SeisWide header file (.txt)|*.txt|All files|*.*";
            dialog.FilterIndex = 1;
            if (Directory.Exists(man.SourceSeisWideHeader))
                dialog.InitialDirectory = man.SourceSeisWideHeader;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string file = dialog.FileName;
                pathToSW.Text = file;
            }
        }

        private void openSeiSeeButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "SeiSee header file|";
            dialog.FilterIndex = 1;
            if (Directory.Exists(man.SourceSeiSeeHeader))
                dialog.InitialDirectory = man.SourceSeiSeeHeader;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string file = dialog.FileName;
                this.pathToSeiSee.Text = file;
            }
        }

        private void interpolateButton_Click(object sender, EventArgs e)
        {
            double delta;
            if (!extractDelta(out delta))
                return;

            man.ProcessInterpolation(delta, useProjectionsBox.Checked, singleCheckBox.Checked);
        }

        private bool extractDelta(out double delta)
        {
            if (!Double.TryParse(deltaTextBox.Text, out delta))
            {
                MessageBox.Show("Ой. Не удалось распознать шаг дискретизации. Проверьте, что Вы используете точку в качестве разделителя.",
                    "Ошибка");
                return false;
            }

            else if (delta <= 0.00001 || delta >= 10)
            {
                MessageBox.Show("Шаг дискретизации задан очень большим (больше 10) или очень маленьким (меньше 0.00001).", "Ошибка");
                return false;
            }
            return true;
        }
        
        private void correctAllButton_Click(object sender, EventArgs e)
        {
            man.CorrectAllTXIN(useProjectionsBox.Checked);
            man.SplitHodographs(useProjectionsBox.Checked);
        }

        private void correct_Click(object sender, EventArgs e)
        {

            if (!useProjectionsBox.Checked)
            {
                if (!File.Exists(pathToSW.Text))
                {
                    MessageBox.Show("Не указан или указан неправильно путь к файлу заголовка.", "Ошибка");
                    return;
                }
                if (!File.Exists(pathToTXIN.Text))
                {
                    MessageBox.Show("Не указан или указан неправильно путь к файлу корелляции (.in)", "Ошибка");
                    return;
                }

                man.Correct(pathToSW.Text, pathToTXIN.Text);
            }
            else
            {
                if (!File.Exists(pathToSW.Text))
                {
                    MessageBox.Show("Не указан или указан неправильно путь к файлу заголовка SeisWide.", "Ошибка");
                    return;
                }
                if (!File.Exists(pathToTXIN.Text))
                {
                    MessageBox.Show("Не указан или указан неправильно путь к файлу корелляции (.in)", "Ошибка");
                    return;
                }
                if (!File.Exists(pathToSeiSee.Text))
                {
                    MessageBox.Show("Не указан или указан неправильно путь к файлу заголовка SeiSee.\n" +
                        "Недостаточно данных для вычисления проекций.", "Ошибка");
                    return;
                }

                man.CorrectWithProjections(pathToTXIN.Text, pathToSW.Text, pathToSeiSee.Text);
            }

            man.SplitHodographs(useProjectionsBox.Checked);
        }

        private void showParameters_Click(object sender, EventArgs e)
        {
            (new ProfileParameters()).Show();
        }

        private void printParams_Click(object sender, EventArgs e)
        {
            var properties = Properties.Settings.Default;
            double S1_x = double.Parse(properties.S1_X);
            double S1_y = double.Parse(properties.S1_Y);
            double S2_x = double.Parse(properties.S2_X);
            double S2_y = double.Parse(properties.S2_Y);
            double N1_x = double.Parse(properties.N1_X);
            double N1_y = double.Parse(properties.N1_Y);
            double L = double.Parse(properties.L);

            Console.WriteLine("S1: ({0}, {1})\nS2: ({2}, {3})\nN1: ({4}, {5})\nL: {6}",
                S1_x, S1_y, S2_x, S2_y, N1_x, N1_y, L);
        }

        private void useSeiSeeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            updateTitles();
        }

        private void updateTitles()
        {
            if (useProjectionsBox.Checked)
            {
                buttonCorrect.Text = Properties.Resources.with;
                buttonCorrectAll.Text = Properties.Resources.with_all;
                buttonInterpolate.Text = Properties.Resources.with_ip;
            }
            else
            {
                buttonCorrect.Text = Properties.Resources.without;
                buttonCorrectAll.Text = Properties.Resources.without_all;
                buttonInterpolate.Text = Properties.Resources.without_ip;
            }
        }
    }
}