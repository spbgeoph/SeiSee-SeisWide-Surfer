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

        /// <summary>
        /// Updates all titles on buttons according to state of useProjectionsBox.
        /// </summary>
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

        /// <summary>
        /// Function attached to buttonOpenFolder Click event.
        /// Opens FolderBrowserDialog for folder selection. Saves selected folder in settings; shows it on the main form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Function attached to MainForm object FormClosing event.
        /// Saves user settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SeisWide_Surfer.Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Function attached to buttonOpenTXIN Click event.
        /// Shows OpenFileDialog for 'tx.in' file selection; shows selected file in PathToTXIN textbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Function attached to buttonOpenSW Click event.
        /// Shows OpenFileDialog for SeisWide header selection; shows selected file in PathToSW textbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Function attached to buttonOpenSeiSee Click event.
        /// Shows OpenFileDialog for SeiSee header selection; shows selected file in PathToSeiSee textbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Tries to parse delta from deltaTextBox, shows some message boxes in case of incorrect input.
        /// </summary>
        /// <param name="delta"></param>
        /// <returns></returns>
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
        
        /// <summary> 
        /// Function attached to buttonCorrectAll Click event.
        /// Binds or calculates projections for every pair / triplet of files; then splits hodographs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void correctAllButton_Click(object sender, EventArgs e)
        {
            man.CorrectAllTXIN(useProjectionsBox.Checked);
            man.SplitHodographs(useProjectionsBox.Checked);
        }

        /// <summary>
        /// Function attached to buttonCorrect Click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void correct_Click(object sender, EventArgs e)
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

            if (!useProjectionsBox.Checked)
            {
                man.Correct(pathToSW.Text, pathToTXIN.Text);
            }
            else
            {
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

        /// <summary>
        /// Function attached to buttonInterpolate Click event.
        /// Interpolates all 'tx.in' files with bound traces or 'tx.in'-like files with additional projection column.
        /// Checkboxes on the form affect the output.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void interpolateButton_Click(object sender, EventArgs e)
        {
            double delta;
            if (!extractDelta(out delta))
                return;

            man.ProcessInterpolation(delta, useProjectionsBox.Checked, singleCheckBox.Checked);
        }

        /// <summary>
        /// Function attached to buttonShowParameters Click event.
        /// Shows ProfileParameters form with profile parameters.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showParameters_Click(object sender, EventArgs e)
        {
            (new ProfileParameters()).Show();
        }

        /// <summary>
        /// Function attached to useProjectionsBox CheckedChanged event.
        /// Updates text on all buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void useSeiSeeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            updateTitles();
        }

    }
}