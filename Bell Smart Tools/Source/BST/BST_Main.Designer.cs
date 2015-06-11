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
            this.mi_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_Info = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_FeedBack = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_DebugMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_DebugLevel_Disable = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_SS1 = new System.Windows.Forms.ToolStripSeparator();
            this.mi_DebugLevel_Low = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_DebugLevel_Middle = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_DebugLevel_High = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_DebugLevel_Log = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_SS2 = new System.Windows.Forms.ToolStripSeparator();
            this.mi_DebugTool = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.TextBoxFormCreator = new System.Windows.Forms.TextBox();
            this.ButtonFormCreator = new System.Windows.Forms.Button();
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
            // ms_MainMenu
            // 
            this.ms_MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_BST,
            this.mi_Tool,
            this.mi_Help});
            this.ms_MainMenu.Location = new System.Drawing.Point(0, 0);
            this.ms_MainMenu.Name = "ms_MainMenu";
            this.ms_MainMenu.Size = new System.Drawing.Size(538, 24);
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
            this.mi_Logout.Size = new System.Drawing.Size(146, 22);
            this.mi_Logout.Text = "BST 로그아웃";
            // 
            // mi_Restart
            // 
            this.mi_Restart.Name = "mi_Restart";
            this.mi_Restart.Size = new System.Drawing.Size(146, 22);
            this.mi_Restart.Text = "BST 재시작";
            this.mi_Restart.Click += new System.EventHandler(this.mi_Restart_Click);
            // 
            // mi_End
            // 
            this.mi_End.Name = "mi_End";
            this.mi_End.Size = new System.Drawing.Size(146, 22);
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
            this.mi_BST_Preferences});
            this.mi_Tool.Name = "mi_Tool";
            this.mi_Tool.Size = new System.Drawing.Size(41, 20);
            this.mi_Tool.Text = "도구";
            // 
            // mi_TopMost
            // 
            this.mi_TopMost.CheckOnClick = true;
            this.mi_TopMost.Name = "mi_TopMost";
            this.mi_TopMost.Size = new System.Drawing.Size(146, 22);
            this.mi_TopMost.Text = "BST 항상위";
            this.mi_TopMost.Click += new System.EventHandler(this.mi_TopMost_Click);
            // 
            // mi_AutoTray
            // 
            this.mi_AutoTray.Name = "mi_AutoTray";
            this.mi_AutoTray.Size = new System.Drawing.Size(146, 22);
            this.mi_AutoTray.Text = "자동 트레이";
            this.mi_AutoTray.Click += new System.EventHandler(this.mi_AutoTray_Click);
            // 
            // ss_1
            // 
            this.ss_1.Name = "ss_1";
            this.ss_1.Size = new System.Drawing.Size(143, 6);
            // 
            // mi_Laboratory
            // 
            this.mi_Laboratory.Name = "mi_Laboratory";
            this.mi_Laboratory.Size = new System.Drawing.Size(146, 22);
            this.mi_Laboratory.Text = "실험실";
            this.mi_Laboratory.Click += new System.EventHandler(this.mi_Laboratory_Click);
            // 
            // mi_BST_Preferences
            // 
            this.mi_BST_Preferences.Name = "mi_BST_Preferences";
            this.mi_BST_Preferences.Size = new System.Drawing.Size(146, 22);
            this.mi_BST_Preferences.Text = "BST 환경설정";
            this.mi_BST_Preferences.Click += new System.EventHandler(this.mi_BST_Preferences_Click);
            // 
            // mi_Help
            // 
            this.mi_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_Info,
            this.mi_FeedBack,
            this.mi_DebugMenu});
            this.mi_Help.Name = "mi_Help";
            this.mi_Help.Size = new System.Drawing.Size(41, 20);
            this.mi_Help.Text = "정보";
            this.mi_Help.Click += new System.EventHandler(this.mi_Help_Click);
            // 
            // mi_Info
            // 
            this.mi_Info.Name = "mi_Info";
            this.mi_Info.Size = new System.Drawing.Size(122, 22);
            this.mi_Info.Text = "BST 소개";
            // 
            // mi_FeedBack
            // 
            this.mi_FeedBack.Name = "mi_FeedBack";
            this.mi_FeedBack.Size = new System.Drawing.Size(122, 22);
            this.mi_FeedBack.Text = "피드백";
            // 
            // mi_DebugMenu
            // 
            this.mi_DebugMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_DebugLevel_Disable,
            this.mi_SS1,
            this.mi_DebugLevel_Low,
            this.mi_DebugLevel_Middle,
            this.mi_DebugLevel_High,
            this.mi_DebugLevel_Log,
            this.mi_SS2,
            this.mi_DebugTool});
            this.mi_DebugMenu.Name = "mi_DebugMenu";
            this.mi_DebugMenu.Size = new System.Drawing.Size(122, 22);
            this.mi_DebugMenu.Text = "Debug";
            // 
            // mi_DebugLevel_Disable
            // 
            this.mi_DebugLevel_Disable.Name = "mi_DebugLevel_Disable";
            this.mi_DebugLevel_Disable.Size = new System.Drawing.Size(204, 22);
            this.mi_DebugLevel_Disable.Text = "Disable";
            // 
            // mi_SS1
            // 
            this.mi_SS1.Name = "mi_SS1";
            this.mi_SS1.Size = new System.Drawing.Size(201, 6);
            // 
            // mi_DebugLevel_Low
            // 
            this.mi_DebugLevel_Low.Name = "mi_DebugLevel_Low";
            this.mi_DebugLevel_Low.Size = new System.Drawing.Size(204, 22);
            this.mi_DebugLevel_Low.Text = "Low Level";
            // 
            // mi_DebugLevel_Middle
            // 
            this.mi_DebugLevel_Middle.Name = "mi_DebugLevel_Middle";
            this.mi_DebugLevel_Middle.Size = new System.Drawing.Size(204, 22);
            this.mi_DebugLevel_Middle.Text = "Middle Level";
            // 
            // mi_DebugLevel_High
            // 
            this.mi_DebugLevel_High.Name = "mi_DebugLevel_High";
            this.mi_DebugLevel_High.Size = new System.Drawing.Size(204, 22);
            this.mi_DebugLevel_High.Text = "High Level";
            // 
            // mi_DebugLevel_Log
            // 
            this.mi_DebugLevel_Log.Name = "mi_DebugLevel_Log";
            this.mi_DebugLevel_Log.Size = new System.Drawing.Size(204, 22);
            this.mi_DebugLevel_Log.Text = "Log Level";
            // 
            // mi_SS2
            // 
            this.mi_SS2.Name = "mi_SS2";
            this.mi_SS2.Size = new System.Drawing.Size(201, 6);
            // 
            // mi_DebugTool
            // 
            this.mi_DebugTool.Name = "mi_DebugTool";
            this.mi_DebugTool.Size = new System.Drawing.Size(204, 22);
            this.mi_DebugTool.Text = "Bell Smart Debug Tools";
            this.mi_DebugTool.Click += new System.EventHandler(this.mi_DebugTool_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(406, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "MC 환경설정 (임시)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(406, 69);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(406, 120);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // TextBoxFormCreator
            // 
            this.TextBoxFormCreator.Location = new System.Drawing.Point(326, 213);
            this.TextBoxFormCreator.Name = "TextBoxFormCreator";
            this.TextBoxFormCreator.Size = new System.Drawing.Size(200, 21);
            this.TextBoxFormCreator.TabIndex = 9;
            // 
            // ButtonFormCreator
            // 
            this.ButtonFormCreator.Location = new System.Drawing.Point(326, 240);
            this.ButtonFormCreator.Name = "ButtonFormCreator";
            this.ButtonFormCreator.Size = new System.Drawing.Size(200, 23);
            this.ButtonFormCreator.TabIndex = 10;
            this.ButtonFormCreator.Text = "Create Form And Show";
            this.ButtonFormCreator.UseVisualStyleBackColor = true;
            // 
            // BST_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(538, 275);
            this.Controls.Add(this.ButtonFormCreator);
            this.Controls.Add(this.TextBoxFormCreator);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_Notice);
            this.Controls.Add(this.ms_MainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.ms_MainMenu;
            this.MaximizeBox = false;
            this.Name = "BST_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bell Smart Tools";
            this.Load += new System.EventHandler(this.BST_Main_Load);
            this.ms_MainMenu.ResumeLayout(false);
            this.ms_MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Notice;
        private System.Windows.Forms.Timer tmr_NoticeLoader;
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
        private System.Windows.Forms.ToolStripMenuItem mi_Info;
        private System.Windows.Forms.ToolStripMenuItem mi_FeedBack;
        private System.Windows.Forms.ToolStripMenuItem mi_DebugMenu;
        private System.Windows.Forms.ToolStripMenuItem mi_DebugLevel_Disable;
        private System.Windows.Forms.ToolStripSeparator mi_SS1;
        private System.Windows.Forms.ToolStripMenuItem mi_DebugLevel_Low;
        private System.Windows.Forms.ToolStripMenuItem mi_DebugLevel_Middle;
        private System.Windows.Forms.ToolStripMenuItem mi_DebugLevel_High;
        private System.Windows.Forms.ToolStripMenuItem mi_DebugLevel_Log;
        private System.Windows.Forms.ToolStripSeparator mi_SS2;
        private System.Windows.Forms.ToolStripMenuItem mi_DebugTool;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox TextBoxFormCreator;
        private System.Windows.Forms.Button ButtonFormCreator;

    }
}