namespace Bell_Smart_Tools.Source.BST
{
    partial class BST_Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BST_Main));
            this.txt_Notice = new System.Windows.Forms.TextBox();
            this.tmr_NoticeLoader = new System.Windows.Forms.Timer(this.components);
            this.cb_PackList = new System.Windows.Forms.ComboBox();
            this.btn_GameStart = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ms_MainMenu = new System.Windows.Forms.MenuStrip();
            this.mi_BST = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_Logout = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_Restart = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_End = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_Tool = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_TopMost = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_AutoTray = new System.Windows.Forms.ToolStripMenuItem();
            this.ss_1 = new System.Windows.Forms.ToolStripSeparator();
            this.mi_Laboratory = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_BST_Preferences = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_BC_Preferences = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_Info = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_FeedBack = new System.Windows.Forms.ToolStripMenuItem();
            this.ms_MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_Notice
            // 
            this.txt_Notice.Location = new System.Drawing.Point(0, 25);
            this.txt_Notice.Multiline = true;
            this.txt_Notice.Name = "txt_Notice";
            this.txt_Notice.ReadOnly = true;
            this.txt_Notice.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Notice.Size = new System.Drawing.Size(320, 250);
            this.txt_Notice.TabIndex = 0;
            // 
            // tmr_NoticeLoader
            // 
            this.tmr_NoticeLoader.Enabled = true;
            this.tmr_NoticeLoader.Interval = 1000;
            this.tmr_NoticeLoader.Tick += new System.EventHandler(this.tmr_NoticeLoader_Tick);
            // 
            // cb_PackList
            // 
            this.cb_PackList.FormattingEnabled = true;
            this.cb_PackList.Items.AddRange(new object[] {
            "방울크래프트8",
            "섹시크래프트"});
            this.cb_PackList.Location = new System.Drawing.Point(320, 235);
            this.cb_PackList.Name = "cb_PackList";
            this.cb_PackList.Size = new System.Drawing.Size(220, 20);
            this.cb_PackList.TabIndex = 1;
            this.cb_PackList.Text = "플레이하실 모드팩을 선택해주세요.";
            this.cb_PackList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cb_PackList_KeyPress);
            // 
            // btn_GameStart
            // 
            this.btn_GameStart.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_GameStart.Location = new System.Drawing.Point(320, 255);
            this.btn_GameStart.Name = "btn_GameStart";
            this.btn_GameStart.Size = new System.Drawing.Size(220, 20);
            this.btn_GameStart.TabIndex = 2;
            this.btn_GameStart.Text = "방울크래프트 시작!";
            this.btn_GameStart.UseVisualStyleBackColor = true;
            this.btn_GameStart.Click += new System.EventHandler(this.btn_GameStart_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(450, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(344, 33);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 4;
            // 
            // ms_MainMenu
            // 
            this.ms_MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_BST,
            this.mi_Tool,
            this.mi_Help});
            this.ms_MainMenu.Location = new System.Drawing.Point(0, 0);
            this.ms_MainMenu.Name = "ms_MainMenu";
            this.ms_MainMenu.Size = new System.Drawing.Size(539, 24);
            this.ms_MainMenu.TabIndex = 5;
            this.ms_MainMenu.Text = "메인 메뉴";
            // 
            // mi_BST
            // 
            this.mi_BST.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_Logout,
            this.mi_Restart,
            this.mi_End});
            this.mi_BST.Name = "mi_BST";
            this.mi_BST.Size = new System.Drawing.Size(41, 20);
            this.mi_BST.Text = "BST";
            // 
            // mi_Logout
            // 
            this.mi_Logout.Name = "mi_Logout";
            this.mi_Logout.Size = new System.Drawing.Size(152, 22);
            this.mi_Logout.Text = "BST 로그아웃";
            // 
            // mi_Restart
            // 
            this.mi_Restart.Name = "mi_Restart";
            this.mi_Restart.Size = new System.Drawing.Size(152, 22);
            this.mi_Restart.Text = "BST 재시작";
            // 
            // mi_End
            // 
            this.mi_End.Name = "mi_End";
            this.mi_End.Size = new System.Drawing.Size(152, 22);
            this.mi_End.Text = "BST 종료";
            this.mi_End.Click += new System.EventHandler(this.mi_End_Click);
            // 
            // mi_Tool
            // 
            this.mi_Tool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_TopMost,
            this.mi_AutoTray,
            this.ss_1,
            this.mi_Laboratory,
            this.mi_BST_Preferences,
            this.mi_BC_Preferences});
            this.mi_Tool.Name = "mi_Tool";
            this.mi_Tool.Size = new System.Drawing.Size(41, 20);
            this.mi_Tool.Text = "도구";
            // 
            // mi_TopMost
            // 
            this.mi_TopMost.CheckOnClick = true;
            this.mi_TopMost.Name = "mi_TopMost";
            this.mi_TopMost.Size = new System.Drawing.Size(152, 22);
            this.mi_TopMost.Text = "BST 항상위";
            this.mi_TopMost.Click += new System.EventHandler(this.mi_TopMost_Click);
            // 
            // mi_AutoTray
            // 
            this.mi_AutoTray.Name = "mi_AutoTray";
            this.mi_AutoTray.Size = new System.Drawing.Size(152, 22);
            this.mi_AutoTray.Text = "자동 트레이";
            this.mi_AutoTray.Click += new System.EventHandler(this.mi_AutoTray_Click);
            // 
            // ss_1
            // 
            this.ss_1.Name = "ss_1";
            this.ss_1.Size = new System.Drawing.Size(149, 6);
            // 
            // mi_Laboratory
            // 
            this.mi_Laboratory.Name = "mi_Laboratory";
            this.mi_Laboratory.Size = new System.Drawing.Size(152, 22);
            this.mi_Laboratory.Text = "실험실";
            this.mi_Laboratory.Click += new System.EventHandler(this.mi_Laboratory_Click);
            // 
            // mi_BST_Preferences
            // 
            this.mi_BST_Preferences.Name = "mi_BST_Preferences";
            this.mi_BST_Preferences.Size = new System.Drawing.Size(152, 22);
            this.mi_BST_Preferences.Text = "BST 환경설정";
            // 
            // mi_BC_Preferences
            // 
            this.mi_BC_Preferences.Name = "mi_BC_Preferences";
            this.mi_BC_Preferences.Size = new System.Drawing.Size(152, 22);
            this.mi_BC_Preferences.Text = "BC 환경설정";
            // 
            // mi_Help
            // 
            this.mi_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_Info,
            this.mi_FeedBack});
            this.mi_Help.Name = "mi_Help";
            this.mi_Help.Size = new System.Drawing.Size(53, 20);
            this.mi_Help.Text = "도움말";
            // 
            // mi_Info
            // 
            this.mi_Info.Name = "mi_Info";
            this.mi_Info.Size = new System.Drawing.Size(152, 22);
            this.mi_Info.Text = "BST 소개";
            // 
            // mi_FeedBack
            // 
            this.mi_FeedBack.Name = "mi_FeedBack";
            this.mi_FeedBack.Size = new System.Drawing.Size(152, 22);
            this.mi_FeedBack.Text = "피드백";
            // 
            // BST_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(539, 330);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_GameStart);
            this.Controls.Add(this.cb_PackList);
            this.Controls.Add(this.txt_Notice);
            this.Controls.Add(this.ms_MainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.ms_MainMenu;
            this.MaximizeBox = false;
            this.Name = "BST_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BST 메인";
            this.Load += new System.EventHandler(this.BST_Main_Load);
            this.ms_MainMenu.ResumeLayout(false);
            this.ms_MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Notice;
        private System.Windows.Forms.Timer tmr_NoticeLoader;
        private System.Windows.Forms.ComboBox cb_PackList;
        private System.Windows.Forms.Button btn_GameStart;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.MenuStrip ms_MainMenu;
        private System.Windows.Forms.ToolStripMenuItem mi_BST;
        private System.Windows.Forms.ToolStripMenuItem mi_Tool;
        private System.Windows.Forms.ToolStripMenuItem mi_TopMost;
        private System.Windows.Forms.ToolStripMenuItem mi_AutoTray;
        private System.Windows.Forms.ToolStripMenuItem mi_Help;
        private System.Windows.Forms.ToolStripSeparator ss_1;
        private System.Windows.Forms.ToolStripMenuItem mi_Laboratory;
        private System.Windows.Forms.ToolStripMenuItem mi_Logout;
        private System.Windows.Forms.ToolStripMenuItem mi_Restart;
        private System.Windows.Forms.ToolStripMenuItem mi_End;
        private System.Windows.Forms.ToolStripMenuItem mi_BST_Preferences;
        private System.Windows.Forms.ToolStripMenuItem mi_BC_Preferences;
        private System.Windows.Forms.ToolStripMenuItem mi_Info;
        private System.Windows.Forms.ToolStripMenuItem mi_FeedBack;

    }
}