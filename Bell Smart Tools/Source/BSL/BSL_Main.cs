using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BellLib.Data;

namespace Bell_Smart_Tools.Source.BSL
{
    public partial class BSL_Main : Form
    {
        public BSL_Main()
        {
            InitializeComponent();
        }

        private void wb_PackNews_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void lb_ModPack_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BSL_Main_Load(object sender, EventArgs e)
        {

        }

        private void cb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ms_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void ms_BSL_PreferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BSL_Preferences BSLP = new BSL_Preferences();
            BSLP.ShowDialog();
        }

    }
}
