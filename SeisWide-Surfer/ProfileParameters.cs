using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeisWide_Surfer
{
    /// <summary>
    /// Form which provides opportunity to set profile parameters for calculating projections.
    /// </summary>
    public partial class ProfileParameters : Form
    {
        public ProfileParameters()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
