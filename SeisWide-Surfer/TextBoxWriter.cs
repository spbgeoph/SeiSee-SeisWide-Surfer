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

        public TextBoxWriter(TextBox _textbox)
        {
            textbox = _textbox;
        }

        public override Encoding Encoding
        {
            get { throw new NotImplementedException(); }
        }

        public override void WriteLine(object value)
        {
            textbox.Text = value.ToString() + Environment.NewLine + textbox.Text;
        }

        public override void WriteLine(string value)
        {
            textbox.Text = value + Environment.NewLine + textbox.Text;
        }

        public override void WriteLine(string format, params object[] arg)
        {
            string result = string.Format(format, arg);
            textbox.Text = result + Environment.NewLine + textbox.Text;
        }
    }
}
