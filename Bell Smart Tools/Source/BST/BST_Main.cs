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
            string Temp = Class.Common.GetStringFromWeb(Data.Base.TOTAL_WEB_URL + "BST/Integration Notice.BSN");

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

        private void btn_GameStart_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Class.Common.RegSave("Email", textBox1.Text);
            Class.BSN.LoginStatus = true;
            
        }

        private void mi_TopMost_Click(object sender, EventArgs e)
        {
            this.TopMost = mi_TopMost.Checked;
        }

        private void mi_Laboratory_Click(object sender, EventArgs e)
        {
            BST_Laboratory Lab = new BST_Laboratory();
            Lab.Show();
        }

        private void mi_AutoTray_Click(object sender, EventArgs e)
        {

        }

        private void cb_PackList_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)Keys.None;
        }
    }
}
