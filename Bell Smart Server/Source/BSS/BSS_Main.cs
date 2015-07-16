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

        private void btn_ModManager_Click(object sender, EventArgs e)
        {
            if (Common.Feasibility("BSU_ModManager"))
            {
                BSU.BSU_ModManager MM = new BSU.BSU_ModManager();
                MM.Show();
            }
        }

        private void btn_RuntimeManager_Click(object sender, EventArgs e)
        {
            if (Common.Feasibility("BSU_RuntimeManager"))
            {
                BSU.BSU_RuntimeManager RM = new BSU.BSU_RuntimeManager();
                RM.Show();
            }
        }

        private void btn_PackMaker_Click(object sender, EventArgs e)
        {
            if (Common.Feasibility("BSU_PackMaker"))
            {
                BSU.BSU_PackMaker PM = new BSU.BSU_PackMaker();
                PM.Show();
            }
        }
    }
}
