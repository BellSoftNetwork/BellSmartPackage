using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using BellLib.Class;
using BellLib.Data;
using Debug = BellLib.Class.Debug;
using Bell_Smart_Tools.Source.BSL;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Bell_Smart_Tools.Source.BST
{
    public partial class BST_Main : Form
    {
        public BST_Main()
        {
            InitializeComponent();
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            // Register mi_DebugLevel_*.CheckedChanged on mi_DebugLevel_CheckedChanged
            // Register mi_DebugLevel_*.Click on mi_DebugLevel_Click
            foreach (var debugItem in typeof(BST_Main).GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (debugItem.Name.IndexOf("mi_DebugLevel_") >= 0)
                {
                    var menuItem = (ToolStripMenuItem)debugItem.GetValue(this);
                    menuItem.CheckedChanged += new EventHandler(mi_DebugLevel_CheckedChanged);
                    menuItem.Click += new EventHandler(mi_DebugLevel_Click);
                }
            }
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
            try
            {
                string original = Common.GetStringFromWeb("http://www.softbell.net/Notice", Encoding.UTF8);
                original = original.Substring(original.IndexOf("<tbody>"));
                original = original.Substring(original.IndexOf("<tbody>"), original.IndexOf("</tbody>"));
                string[] data = original.Split('\n');
                List<string> listNotice = new List<string>();
                foreach (string tmp in data)
                {
                    if (tmp.Contains("<a href=\"/Notice/") && !tmp.Contains("</td>"))
                    {
                        string temp = tmp.Substring(tmp.IndexOf("<a href=\"/"));
                        temp = temp.Substring(temp.IndexOf("<a href=\"/"), temp.IndexOf("</a"));
                        string Notice = temp.Substring(temp.IndexOf("\">") + 2);

                        string URL = temp.Substring(temp.IndexOf("\"/Notice/") + 9);
                        URL = URL.Substring(0, URL.IndexOf("\">"));
                        if (Notice.Contains("<span style="))
                        { // 제목 굵음 처리 되어있을시
                            Notice = "[중요] " + Notice.Replace("<span style=\"font-weight:bold;\">", null).Replace("</span>", null);
                        }
                        else
                        {
                            Notice = "------ " + Notice;
                        }

                        listNotice.Add(Notice);
                        lstNotice.Tag += Servers.Bell_Soft_Network.WEB_BSN_ROOT + "Notice/" + URL + "|";
                    }
                }

                string tempSelected = (string)lstNotice.SelectedItem;
                lstNotice.Items.Clear();
                lstNotice.Items.AddRange(listNotice.ToArray());
                lstNotice.SelectedItem = tempSelected;
            }
            catch (Exception ex)
            {
                Debug.Message(Debug.Level.Middle, "NoticeLoad" + Environment.NewLine + ex.Message);
            }
        }

        private void mi_TopMost_Click(object sender, EventArgs e)
        {
            this.TopMost = mi_TopMost.Checked;
        }

        private void mi_Laboratory_Click(object sender, EventArgs e)
        {
            if (Common.Feasibility("BST_Laboratory"))
            {
                BST_Laboratory Lab = new BST_Laboratory();
                Lab.Show();
            }
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
            Type t = typeof(BST_Main);
            foreach (var fieldMenu in t.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (fieldMenu.Name.IndexOf("mi_DebugLevel_") >= 0) // 만약 디버그 레벨 메뉴면..
                {
                    var MenuItem = (ToolStripMenuItem)fieldMenu.GetValue(this); // 일단 mi_DebugLevel 메뉴를.. 안전하게 캐스팅하자..
                    MenuItem.Checked = false; // 이걸 다 false로.. 바꾸고..

                    foreach (var fieldLevel in typeof(Debug.Level).GetFields()) // Debug.Level 필드들을 구해서..
                        if (fieldLevel.Name == fieldMenu.Name.Replace("mi_DebugLevel_", String.Empty) && Debug.DebuggerMode == (Debug.Level)fieldLevel.GetValue(this))
                            // Debug.Level.*가 mi_DebugLevel_*와 같고.. Debug.DebuggerMode가 Debug.Level.*와 같으면?
                            MenuItem.Checked = true; // mi_DebugLevel_*.Checked를 true로 바꾼다!
                }
            }
        }

        private void mi_DebugLevel_CheckedChanged(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;
            if (item.Checked)
            {
                var fieldValue = typeof(Debug.Level).GetField(item.Name.Replace("mi_DebugLevel_", String.Empty)).GetValue(this);
                Debug.DebuggerMode = (Debug.Level)fieldValue;
            }
        }

        private void mi_DebugLevel_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;
            var fieldValue = typeof(Debug.Level).GetField(item.Name.Replace("mi_DebugLevel_", String.Empty)).GetValue(null);
            Debug.DebuggerMode = (Debug.Level)fieldValue;
            DebugModeLoad();
        }

        private void mi_DebugTool_Click(object sender, EventArgs e)
        {
            if (Common.Feasibility("BST_Debug"))
            {
                BST_Debug BSTD = new BST_Debug();
                BSTD.Show();
            }
        }

        private void mi_Help_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Common.Feasibility("BSL_Main"))
            {
                BSL_Main BSLM = new BSL_Main();
                BSLM.Show();
            }
        }

        private void mi_Reader_Click(object sender, EventArgs e)
        {
            BST_Reader BSTR = new BST_Reader();
            BSTR.Show();
        }

        private void mi_Info_Click(object sender, EventArgs e)
        {
            BST_Info BSTI = new BST_Info();
            BSTI.ShowDialog();
        }

        private void lstNotice_DoubleClick(object sender, EventArgs e)
        {
            if (lstNotice.SelectedIndex != -1)
                Process.Start(lstNotice.Tag.ToString().Split('|')[lstNotice.SelectedIndex]);
        }
    }
}