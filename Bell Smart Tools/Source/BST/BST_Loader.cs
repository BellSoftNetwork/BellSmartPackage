using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bell_Smart_Tools.Source.BST
{
    public partial class BST_Loader : Form
    {
        public BST_Loader()
        {
            InitializeComponent();
        }

        private void BST_Loader_Shown(object sender, EventArgs e)
        {
            pb_Load.Value = pb_Load.Maximum;
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            BST_Login BST = new BST_Login();
            BST.Show();
        }
    }
}
