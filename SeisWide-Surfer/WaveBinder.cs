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
using System.Text.RegularExpressions;

namespace SeisWide_Surfer
{
    partial class WaveBinder : Form
    {
        AnotherManipulator man;
        ListViewItem last;
        private static readonly string WaveBindingSource = "_wavebinding";
        private static readonly string separator = "||";

        Dictionary<string, string> waveBinding = new Dictionary<string, string>();

        public WaveBinder(AnotherManipulator _man)
        {
            InitializeComponent();
            man = _man;
            loadTxinList();
            loadWaveBinding();
        }

        private void loadTxinList()
        {
            string dir = man.SourcePicking;
            string[] txins = Directory.GetFiles(dir, "*.in");
            
            foreach (string txin in txins)
            {
                string temp = Path.GetFileName(txin);
                ListViewItem item = new ListViewItem(temp);
                waveBinding.Add(temp, string.Empty);
                listViewTxins.Items.Add(item);
            }
        }

        private void loadTxinInternals(string txin)
        {
            string path = Path.Combine(man.SourcePicking, txin);
            textBoxInternals.Text = File.ReadAllText(path);
            labelCurrentTxin.Text = txin;
        }


        private void loadWaveBinding()
        {
            string source = Path.Combine(man.Folder, WaveBindingSource);
            if (!File.Exists(source))
                return;

            string[] lines = File.ReadAllLines(source);
            foreach (string line in lines)
            {
                int sep = line.IndexOf(separator);
                if (sep < 0)
                    continue;
                string txin = line.Substring(0, sep);
                string waves = line.Substring(sep + separator.Length);
                textBoxInternals.Text += txin + Environment.NewLine;

                if (waveBinding.ContainsKey(txin))
                {
                    waveBinding[txin] = waves;
                }
                else
                {
                    waveBinding.Add(txin, waves);
                    MessageBox.Show("wtf k nvm record with non-existing file " + txin , "Error");
                }
            }
        }

        private void WaveBinder_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (last != null)
                waveBinding[last.Text] = textBoxWaves.Text;

            string filename = Path.Combine(man.Folder, WaveBindingSource);

            StringBuilder sb = new StringBuilder();
            foreach (var pair in waveBinding)
                sb.Append(pair.Key).Append(separator).Append(pair.Value).AppendLine();

            File.WriteAllText(filename, sb.ToString());
        }

        private void listViewTxins_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listViewTxins.SelectedItems.Count <= 0)
                return;

            var item = listViewTxins.SelectedItems[0];
            loadTxinInternals(item.Text);
            
            if (last != null)
                waveBinding[last.Text] = textBoxWaves.Text;
            
            textBoxWaves.Text = waveBinding[item.Text];
            last = item;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSubstitute_Click(object sender, EventArgs e)
        {
            if (last != null)
                waveBinding[last.Text] = textBoxWaves.Text;

            foreach (var pair in waveBinding)
            {
                if (string.IsNullOrWhiteSpace(pair.Value))
                    continue;

                string[] waves = pair.Value.Split(',');

                StringBuilder sb = new StringBuilder();
                int i = -1;
                string current = string.Empty;
                string temp = string.Empty;
                string txin = Path.Combine(man.SourcePicking, pair.Key);
                string[] lines = File.ReadAllLines(txin);
                
                foreach(string line in lines)
                {
                    string[] record = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (record[3] == "0" || record[3] == "-1")
                    {
                        sb.AppendLine(line);
                        if (i >= 0)
                        {
                            temp = string.Empty;
                        }
                        continue;
                    }
                    if (record[3] != temp)
                    {
                        if (++i >= waves.Length)
                        {
                            MessageBox.Show("Ошибка: волн в изменяемом файле больше, чем дано для замещения.", pair.Key);
                            return;
                        }
                        temp = string.Empty;
                        current = " " + waves[i] + " ";
                    }

                    if (string.IsNullOrEmpty(temp))
                        temp = record[3];

                    if (record[3] == temp)
                    {
                        Regex regexp = new Regex(Regex.Escape(" " + record[3] + " "));
                        string newLine = regexp.Replace(line, current, 1);

                        sb.AppendLine(newLine);
                    }
                }
                File.WriteAllText(txin, sb.ToString());
            }

            if (listViewTxins.SelectedItems.Count > 0)
                loadTxinInternals(listViewTxins.SelectedItems[0].Text);
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            if (listViewTxins.SelectedItems.Count <= 0)
                return;

            var item = listViewTxins.SelectedItems[0];

            string txin = Path.Combine(man.SourcePicking, item.Text);
            string[] lines = File.ReadAllLines(txin);

            string revHodoStarts = string.Empty;
            string dirHodoStarts = string.Empty;
            string endOfTxin = string.Empty;

            SortedList<int, List<string>> revHodo = new SortedList<int, List<string>>();
            SortedList<int, List<string>> dirHodo = new SortedList<int, List<string>>();

            bool isReversedYet = true;

            foreach (string line in lines)
            {
                string[] record = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (record.Length >= 4)
                {
                    if (record[1] == "-1.000")
                    {
                        revHodoStarts = line;
                    }
                    else if (record[1] == "1.000")
                    {
                        dirHodoStarts = line;
                        isReversedYet = false;
                    }
                    else if (record[3] == "-1")
                    {
                        endOfTxin = line;
                    }
                    else 
                    {
                        var hodo = isReversedYet ? revHodo : dirHodo;
                        int wave = int.Parse(record[3]);
                        if (!hodo.ContainsKey(wave))
                            hodo.Add(wave, new List<string>());
                        hodo[wave].Add(line);
                    }
                }
            }

            List<string> rearranged = new List<string>(lines.Length);
            rearranged.Add(revHodoStarts);

            for (int i = revHodo.Count - 1; i >= 0; i--)
                rearranged.AddRange(revHodo.Values[i]);

            rearranged.Add(dirHodoStarts);

            for (int i = 0; i < dirHodo.Count; i++)
                rearranged.AddRange(dirHodo.Values[i]);

            rearranged.Add(endOfTxin);

            File.WriteAllLines(txin, rearranged);

            loadTxinInternals(item.Text);
        }

        private void textBoxWaves_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
        }
    }
}
