using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bell_Smart_Server.Source.BSS;
using Bell_Smart_Tools.Source.BST;

namespace Bell_Smart_Package.Source.BSP
{
    public partial class BSP_Selector : Form
    {
        public BSP_Selector()
        {
            InitializeComponent();
        }

        private void btn_BST_Click(object sender, EventArgs e)
        {
            BST_Loader BST = new BST_Loader();
            BST.Show();
            this.Close();
        }

        private void btn_BSS_Click(object sender, EventArgs e)
        {
            BSS_Loader BSS = new BSS_Loader();
            BSS.Show();
            this.Close();
        }

        private void btn_Updater_Click(object sender, EventArgs e)
        {
            BSP_Updater BSPU = new BSP_Updater();
            BSPU.Show();
            this.Close();
        }
    }
}
