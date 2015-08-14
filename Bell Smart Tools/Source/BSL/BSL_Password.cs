using BellLib.Class;
using BellLib.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bell_Smart_Server.Source.BSL
{
    public partial class BSL_Password : Form
    {
        public BSL_Password()
        {
            InitializeComponent();
        }

        public string getPassword() {
            return txt_Password.Text;
        }
        private void txt_Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btn_Apply_Click(sender, e);
        }

        private void btn_Apply_Click(object sender, EventArgs e)
        {
            if (txt_Password.Text == string.Empty)
                return;
            this.Close();
        }
    }
}
