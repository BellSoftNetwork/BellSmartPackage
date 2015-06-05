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
    public partial class BST_Main : Form
    {
        public BST_Main()
        {
            InitializeComponent();
        }

        private void BST_Main_Load(object sender, EventArgs e)
        {
            NoticeLoad();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            Class.Common.End();
        }

        private void tmr_NoticeLoader_Tick(object sender, EventArgs e)
        {
            NoticeLoad();
        }
        private void NoticeLoad()
        {
            string Temp = Class.Common.WebHTML(Data.Base.TOTAL_WEB_URL + "BST/Integration Notice.BSN");

            if (Temp != txt_Notice.Text)
            {
                if (txt_Notice.Text == null)
                {
                    txt_Notice.Text = Temp;
                }
                else
                {
                    txt_Notice.Text = Temp;
                    //BST_Manager.Sound(My.Resources.Sound_error);
                    //F_BASE.FlashWindow(this.Handle, true);
                }
            }
        }
    }
}
