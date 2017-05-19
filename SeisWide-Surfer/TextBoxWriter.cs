using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace SeisWide_Surfer
{
    class TextBoxWriter : TextWriter 
    {
        private TextBox textbox;
        private int counter;

        public TextBoxWriter(TextBox _textbox)
        {
            textbox = _textbox;
            counter = 0;
        }

        public override Encoding Encoding
        {
            get { throw new NotImplementedException(); }
        }

        public override void WriteLine(object value)
        {
            counter++;
            textbox.Text = value.ToString() + Environment.NewLine + textbox.Text;
        }

        public override void WriteLine(string value)
        {
            counter++;
            textbox.Text = value + Environment.NewLine + textbox.Text;
        }

        public override void WriteLine(string format, params object[] arg)
        {
            counter++;
            string result = string.Format(format, arg);
            textbox.Text = result + Environment.NewLine + textbox.Text;
        }
    }
}
