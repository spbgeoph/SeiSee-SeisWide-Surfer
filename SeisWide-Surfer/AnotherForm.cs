using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeisWide_Surfer
{
    public partial class AnotherForm : Form
    {

        private AnotherManipulator man;
        private Dictionary<CheckBox, ProcessingFlag> flags = new Dictionary<CheckBox, ProcessingFlag>();

        public AnotherForm()
        {
            InitializeComponent();
            man = new AnotherManipulator(new TextBoxWriter(log));
            log.Visible = !Console.Out.Equals(man.Writer);

            flags.Add(check_fe_Bind, ProcessingFlag.Bind);
            flags.Add(check_fe_Project, ProcessingFlag.Project);
            flags.Add(check_fe_SplitHodographs, ProcessingFlag.SplitHodographs);
            flags.Add(check_fe_Interpolate, ProcessingFlag.Interpolate);
        }

        private void buttonSetWorkspace_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlgF = new FolderBrowserDialog();
            if (!string.IsNullOrEmpty(pathToFolder.Text))
                dlgF.SelectedPath = pathToFolder.Text;

            if (dlgF.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(dlgF.SelectedPath))
            {
                if (Directory.Exists(pathToFolder.Text))
                    Program.WriteProfileData(pathToFolder.Text);

                pathToFolder.Text = dlgF.SelectedPath;
                man.SetWorkspace(dlgF.SelectedPath);
                button_fe_Process.Enabled = true;
                button_refl_FirstStage.Enabled = true;
            }
        }


        private void check_fe_Bind_CheckedChanged(object sender, EventArgs e)
        {
            if (!check_fe_Bind.Checked)
            {
                check_fe_Project.Checked = false;
                check_fe_SplitHodographs.Checked = false;
                check_fe_Interpolate.Checked = false;
            }
        }


        private void check_fe_Project_CheckedChanged(object sender, EventArgs e)
        {
            if (check_fe_Project.Checked)
            {
                check_fe_Bind.Checked = true;
            }
        }


        private void check_fe_SplitHodographs_CheckedChanged(object sender, EventArgs e)
        {
            if (check_fe_SplitHodographs.Checked)
            {
                check_fe_Bind.Checked = true;
            }
        }

        private void check_fe_Interpolate_CheckedChanged(object sender, EventArgs e)
        {
            if (check_fe_Interpolate.Checked)
            {
                check_fe_Bind.Checked = true;
            }
        }

        private void check_refl_Bind_CheckedChanged(object sender, EventArgs e)
        {
            if (!check_refl_Bind.Checked)
                check_refl_Square.Checked = false;
        }

        private void check_refl_Square_CheckedChanged(object sender, EventArgs e)
        {
            if (check_refl_Square.Checked)
                check_refl_Bind.Checked = true;
        }

        private void check_refl_MeanVelocity_CheckedChanged(object sender, EventArgs e)
        {
            if (!check_refl_MeanVelocity.Checked)
            {
                check_refl_Pyramid.Checked = false;
                check_refl_SoundCenters.Checked = false;
            }
        }

        private void check_refl_Pyramid_CheckedChanged(object sender, EventArgs e)
        {
            if (check_refl_Pyramid.Checked)
                check_refl_MeanVelocity.Checked = true;
        }

        private void check_refl_SoundCenters_CheckedChanged(object sender, EventArgs e)
        {
            if (check_refl_SoundCenters.Checked)
                check_refl_MeanVelocity.Checked = true;
        }


        /// <summary>
        /// Tries to parse timeDelta from deltaTextBox, shows some message boxes in case of incorrect input.
        /// </summary>
        /// <param name="timeDelta"></param>
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

        private void buttonShowParameters_Click(object sender, EventArgs e)
        {
            (new ProfileParameters()).Show();
        }

        private void buttonProcessFirstEntry_Click(object sender, EventArgs e)
        {
            ProcessingFlag options = ProcessingFlag.None;
            foreach (CheckBox box in flags.Keys)
                if (box.Checked)
                    options |= flags[box];

            double delta = 0;
            if (check_fe_Interpolate.Checked && !extractDelta(out delta))
                return;

            if (options == ProcessingFlag.None)
                return;

            man.ProcessFirstEntry(options, delta);
        }

        private void AnotherForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SeisWide_Surfer.Properties.Settings.Default.Save();
            Program.WriteProfileData(SeisWide_Surfer.Properties.Settings.Default.Folder);
        }

        private void AnotherForm_Load(object sender, EventArgs e)
        {
            string workspace = SeisWide_Surfer.Properties.Settings.Default.Folder;
            if (Directory.Exists(workspace))
            {
                man.migrate(workspace);
            } 
            else
            {
                pathToFolder.Text = SeisWide_Surfer.Properties.Resources.default_folder;
                button_fe_Process.Enabled = false;
                button_refl_FirstStage.Enabled = false;
            }
        }

        private void button_fe_BindWaves_Click(object sender, EventArgs e)
        {
            (new WaveBinder(man)).Show();
        }


        private void button_refl_FirstStage_Click(object sender, EventArgs e)
        {
            ProcessingFlag options = ProcessingFlag.None;
            if (check_refl_Bind.Checked)
                options |= ProcessingFlag.Bind;
            if (check_refl_Square.Checked)
                options |= ProcessingFlag.SquaredTimes;

            if (options == ProcessingFlag.None)
                return;

            int wave;
            if (!int.TryParse(textBox_refl_Wave.Text, out wave))
            {
                MessageBox.Show("Не удалось распознать параметр волны.", "Ошибка");
                return;
            }

            man.ProcessReflectedWave(options, wave);
        }

        private void button_refl_SecondStage_Click(object sender, EventArgs e)
        {
            if (!check_refl_MeanVelocity.Checked)
                return;

            if (string.IsNullOrWhiteSpace(textBox_refl_Contours.Text) || !File.Exists(textBox_refl_Contours.Text))
                return;

            ProcessingFlag options = ProcessingFlag.None;
            if (check_refl_MeanVelocity.Checked)
                options |= ProcessingFlag.MeanVelocities;
            if (check_refl_Pyramid.Checked)
                options |= ProcessingFlag.ShowPyramid;
            if (check_refl_SoundCenters.Checked)
                options |= ProcessingFlag.DepthsOnXCenters;

            if (options == ProcessingFlag.None)
                return;

            man.ProcessContours(options, textBox_refl_Contours.Text);
        }

        private void button_refl_SelectContours_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            if (File.Exists(textBox_refl_Contours.Text))
                dlg.InitialDirectory = Path.GetDirectoryName(textBox_refl_Contours.Text);
            else if (Directory.Exists(man.Folder))
                dlg.InitialDirectory = man.Folder;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string file = dlg.FileName;
                textBox_refl_Contours.Text = file;
            }
        }

        private void button_fe_BindUncertainy_Click(object sender, EventArgs e)
        {
            (new UncertaintiesForm(man)).Show();
        }
    }
}
