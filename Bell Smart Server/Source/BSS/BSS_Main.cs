using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BellLib.Class;

namespace Bell_Smart_Server.Source.BSS
{
    public partial class BSS_Main : Form
    {
        public BSS_Main()
        {
            InitializeComponent();
        }

        private void BSS_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Common.End();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BSU.BSU_ModManager MM = new BSU.BSU_ModManager();
            MM.Show();
        }
    }
}
