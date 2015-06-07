using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using BellLib.Class;
using BellLib.Data;
using Debug = BellLib.Class.Debug;

namespace Bell_Smart_Tools.Source.BST
{
    public partial class BST_Main : Form
    {
        public BST_Main()
        {
            InitializeComponent();
        }

        private void Initialize() // 폼 초기화
        {
            NoticeLoad();
            DebugModeLoad(); // 디버그 모드 로드
            
        }
        private void BST_Main_Load(object sender, EventArgs e)
        {
            Initialize();
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
            string notice = Common.GetStringFromWeb(Base.TOTAL_WEB_URL + "BST/Integration Notice.BSN");

            if (txt_Notice.Text != notice || txt_Notice.Text == null)
                txt_Notice.Text = notice;

                /*else
            {
                    txt_Notice.Text = notice;
                    //BST_Manager.Sound(My.Resources.Sound_error);
                    //F_BASE.FlashWindow(this.Handle, true);
                }*/ // 무슨 의도인..
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

        private void mi_BST_Preferences_Click(object sender, EventArgs e)
        {
            BST_Preferences BSTP = new BST_Preferences();
            BSTP.ShowDialog();
        }

        private void DebugModeLoad()
        {
            mi_Disable.Checked = false;
            mi_Low.Checked = false;
            mi_Middle.Checked = false;
            mi_High.Checked = false;
            mi_Log.Checked = false;

            switch (Debug.DebuggerMode)
            {
                case Debug.Level.Disable:
                    mi_Disable.Checked = true;
                    break;
                case Debug.Level.Low:
                    mi_Low.Checked = true;
                    break;
                case Debug.Level.Middle:
                    mi_Middle.Checked = true;
                    break;
                case Debug.Level.High:
                    mi_High.Checked = true;
                    break;
                case Debug.Level.Log:
                    mi_Log.Checked = true;
                    break;
            }
        }

        private void mi_Disable_CheckedChanged(object sender, EventArgs e)
        {
            if (mi_Disable.Checked)
                Debug.DebuggerMode = Debug.Level.Disable;
        }

        private void mi_Low_CheckedChanged(object sender, EventArgs e)
        {
            if (mi_Low.Checked)
                Debug.DebuggerMode = Debug.Level.Low;
        }

        private void mi_Middle_CheckedChanged(object sender, EventArgs e)
        {
            if (mi_Middle.Checked)
                Debug.DebuggerMode = Debug.Level.Middle;
        }

        private void mi_High_CheckedChanged(object sender, EventArgs e)
        {
            if (mi_High.Checked)
                Debug.DebuggerMode = Debug.Level.High;
        }

        private void mi_Log_CheckedChanged(object sender, EventArgs e)
        {
            if (mi_Log.Checked)
                Debug.DebuggerMode = Debug.Level.Log;
        }

        private void mi_Disable_Click(object sender, EventArgs e)
        {
            Debug.DebuggerMode = Debug.Level.Disable;
            DebugModeLoad();
        }

        private void mi_Low_Click(object sender, EventArgs e)
        {
            Debug.DebuggerMode = Debug.Level.Low;
            DebugModeLoad();
        }

        private void mi_Middle_Click(object sender, EventArgs e)
        {
            Debug.DebuggerMode = Debug.Level.Middle;
            DebugModeLoad();
        }

        private void mi_High_Click(object sender, EventArgs e)
        {
            Debug.DebuggerMode = Debug.Level.High;
            DebugModeLoad();
        }

        private void mi_Log_Click(object sender, EventArgs e)
        {
            Debug.DebuggerMode = Debug.Level.Log;
            DebugModeLoad();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Source.MCL.BSL_Preferences MCP = new MCL.BSL_Preferences();
            MCP.ShowDialog();
        }

        private void mi_DebugTool_Click(object sender, EventArgs e)
        {
            BST_Debug BSTD = new BST_Debug();
            BSTD.Show();
        }
    }
}
