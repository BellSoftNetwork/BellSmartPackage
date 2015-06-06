using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bell_Smart_Package.Source.BSP
{
    public partial class BSP_Loader : Form
    {
        public BSP_Loader()
        {
            InitializeComponent();
        }

        private void BSP_Loader_Shown(object sender, EventArgs e)
        {
            Bell_Smart_Tools.Source.BST.BST_Loader BST = new Bell_Smart_Tools.Source.BST.BST_Loader(); // 존나기네;;
            BST.Show();

            this.Hide();
        }
    }
}
