using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bell_Smart_Tools.Class;
using System.Diagnostics;

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
            Common.End();
        }

        private void tmr_NoticeLoader_Tick(object sender, EventArgs e)
        {
            NoticeLoad();
        }

        private void NoticeLoad()
        {
            string notice = Class.Common.GetStringFromWeb(Data.Base.TOTAL_WEB_URL + "BST/Integration Notice.BSN");

            if (txt_Notice.Text != notice || txt_Notice.Text == null)
                txt_Notice.Text = notice;

                /*else
            {
                    txt_Notice.Text = notice;
                    //BST_Manager.Sound(My.Resources.Sound_error);
                    //F_BASE.FlashWindow(this.Handle, true);
                }*/ // 무슨 의도인..
        }

        private void btn_GameStart_Click(object sender, EventArgs e)
        {

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

        private void mi_End_Click(object sender, EventArgs e)
        {
            Common.End();
        }

        private void mi_Restart_Click(object sender, EventArgs e)
        {
            Common.End(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath);
        }
    }
}
