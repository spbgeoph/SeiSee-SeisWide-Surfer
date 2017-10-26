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

namespace SeisWide_Surfer
{
    public partial class UncertaintiesForm : Form
    {
        private static string format_record = "{0,10} {1,9} {2,9:F3} {3,9} {4,10}";
        
        AnotherManipulator man;

        public UncertaintiesForm(AnotherManipulator _man)
        {
            InitializeComponent();
            man = _man;
        }

        private void buttonSubstitute_Click(object sender, EventArgs e)
        {
            
            Dictionary<int, double> uncertainties;
            
            if (!extractUncs(out uncertainties))
                return;
            
            string dir = man.SourcePicking;
            string[] files = Directory.GetFiles(dir, "*.in");            

            foreach (string file in files)
            {
                substitute(file, uncertainties);
            }
        }

        private bool extractUncs(out Dictionary<int, double> uncs)
        {
            uncs = new Dictionary<int, double>();

            string uncText = textBoxUnc.Text;
            string[] lines = uncText.Split('\n');
            
            int wave;
            double uncertainty;
            
            foreach (string line in lines)
            {
                string[] record = line.Split(new char[] {' ', '\t' }, 2, StringSplitOptions.RemoveEmptyEntries);
                if (record.Length < 2)
                {
                    MessageBox.Show("Не хватает параметров.\nСтрока: " + line, "Ошибка.");
                    return false;
                }
                if (!int.TryParse(record[0], out wave))
                {
                    MessageBox.Show("Не удалось распознать номер волны.\nСтрока: " + line, "Ошибка.");
                    return false;
                }
                
                if (!double.TryParse(record[1], out uncertainty))
                {
                    MessageBox.Show("Не удалось распознать значение неопределённости.\nCтрока: " + line, "Ошибка.");
                    return false;
                }

                if (uncs.ContainsKey(wave))
                {
                    MessageBox.Show(string.Format("Для волны №{0} значение неопределённости было задано несколько раз.", wave), "Ошибка.");
                    return false;
                }

                if (wave <= 0)
                {
                    MessageBox.Show("Номер волны задан отрицательным.\nСтрока: " + line, "Предупреждение.");
                    continue;
                }

                if (uncertainty <= 0)
                {
                    MessageBox.Show("Значение неопределённости не может быть неположительным.\nСтрока: " + line, "Ошибка.");
                    return false;
                }
                uncs.Add(wave, uncertainty);
            }

            return true;
        }

        private void substitute(string file, Dictionary<int, double> uncs)
        {
            StringBuilder sb = new StringBuilder();
            string[] lines = File.ReadAllLines(file);
            foreach (string line in lines)
            {
                string[] rec = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int wave;
                if (rec.Length < 3)
                {
                    MessageBox.Show(string.Format("Файл:{0}\nСтрока: {1}\nНеверный формат записи.", Path.GetFileName(file), line), "Ошибка.");
                    return;
                }
                if (!int.TryParse(rec[3], out wave))
                {
                    MessageBox.Show(string.Format("Файл:{0}\nСтрока: {1}\nНе удалось прочесть номер волны.", Path.GetFileName(file), line), "Ошибка.");
                    return;
                }
                if ((wave <= 0) || (!uncs.ContainsKey(wave)))
                {
                    sb.Append(line).AppendLine();
                }
                else
                {
                    sb.AppendFormat(format_record, rec[0], rec[1], uncs[wave], rec[3], rec[4]).AppendLine();
                }
            }
            File.WriteAllText(file, sb.ToString());
        }
    }
}
