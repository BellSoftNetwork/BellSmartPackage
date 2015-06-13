using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bell_Smart_Tools.Source.BSL
{
    public partial class BSL_Profile : Form
    {
        public BSL_Profile()
        {
            InitializeComponent();
        }

        private void cb_Java_CheckedChanged(object sender, EventArgs e)
        {
            txt_Java.ReadOnly = !cb_Java.Checked;
        }

        private void cb_Parameter_CheckedChanged(object sender, EventArgs e)
        {
            txt_Parameter.ReadOnly = !cb_Parameter.Checked;
        }

        private void cb_SavePW_CheckedChanged(object sender, EventArgs e)
        {
            txt_PW.ReadOnly = !cb_SavePW.Checked;
            txt_PW.Text = string.Empty;
        }
    }
}
