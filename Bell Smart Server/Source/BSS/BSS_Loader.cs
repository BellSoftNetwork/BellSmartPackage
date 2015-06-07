using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bell_Smart_Server.Source.BSS
{
    public partial class BSS_Loader : Form
    {
        public BSS_Loader()
        {
            InitializeComponent();
        }

        private void BSS_Loader_Shown(object sender, EventArgs e)
        {
            BSS_Main Main = new BSS_Main();
            Main.Show();
            //this.Hide();
            this.Close();
        }
    }
}
