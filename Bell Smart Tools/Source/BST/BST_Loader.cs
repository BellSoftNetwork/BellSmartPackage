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

        private void BST_Loader_Load(object sender, EventArgs e)
        {
            pb_Load.Value = pb_Load.Maximum;
            this.Hide();
            BST_Main BST = new BST_Main();
            BST.Show();
        }
    }
}
