using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using BellLib.Class;
using BellLib.Data;
using Debug = BellLib.Class.Debug;
using Bell_Smart_Tools.Source.BSL;

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

            TextBoxFormCreator.Enter += new EventHandler(TextBoxFormCreator_Enter);
            TextBoxFormCreator.Leave += new EventHandler(TextBoxFormCreator_Leave);
            ButtonFormCreator.Click += new EventHandler(ButtonFormCreator_Click);
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

            if (notice == null) return; // notice가 있다가 오류로 안받아져서 공지가 없어지면 좀 그렇잖아요?

            if (txt_Notice.Text != notice || txt_Notice.Text == null)
                txt_Notice.Text = notice;
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

        private void button1_Click(object sender, EventArgs e)
        {
            Source.BSL.BSL_Preferences MCP = new BSL.BSL_Preferences();
            MCP.ShowDialog();
        }

        private void mi_DebugTool_Click(object sender, EventArgs e)
        {
            BST_Debug BSTD = new BST_Debug();
            BSTD.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Test_WPF TWPF = new Test_WPF();
            TWPF.Show();
            Common.CreateFormAndShow("Test_WPF", Assembly.GetExecutingAssembly());
        }

        private void mi_Help_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            BSL_Main BSLM = new BSL_Main();
            BSLM.Show();
        }

        private void TextBoxFormCreator_Enter(object sender, EventArgs e)
        {
            if (TextBoxFormCreator.ForeColor == System.Drawing.Color.Black)
                return;
            TextBoxFormCreator.Text = "";
            TextBoxFormCreator.ForeColor = System.Drawing.Color.Black;
        }

        private void TextBoxFormCreator_Leave(object sender, EventArgs e)
        {
            if (TextBoxFormCreator.Text.Trim() == "")
                TextBoxFormCreator_SetDefault();
        }

        private void TextBoxFormCreator_SetDefault()
        {
            this.TextBoxFormCreator.Text = "폼 클래스 이름...";
            TextBoxFormCreator.ForeColor = System.Drawing.Color.Gray;
        }

        private void ButtonFormCreator_Click(object sender, EventArgs e)
        {
            if (TextBoxFormCreator.Text == "폼 클래스 이름...")
            {
                MessageBox.Show("입력이 없당");
                return;
            }

            Common.CreateFormAndShow(TextBoxFormCreator.Text, Assembly.GetExecutingAssembly());
        }
    }
}