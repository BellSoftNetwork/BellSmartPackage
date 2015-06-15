namespace Bell_Smart_Tools.Source.BSL
{
    partial class BSL_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BSL_Main));
            this.lst_ModPack = new System.Windows.Forms.ListBox();
            this.wb_PackNews = new System.Windows.Forms.WebBrowser();
            this.btn_Launch = new System.Windows.Forms.Button();
            this.cb_Version = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.도구ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mC환경설정ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.정보ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pb_Load = new System.Windows.Forms.ProgressBar();
            this.lb_Status = new System.Windows.Forms.Label();
            this.cb_AutoUpdate = new System.Windows.Forms.CheckBox();
            this.cb_Profile = new System.Windows.Forms.ComboBox();
            this.btn_Preferences = new System.Windows.Forms.Button();
            this.txt_Detail = new System.Windows.Forms.TextBox();
            this.btn_Option = new System.Windows.Forms.Button();
            this.btn_Edit = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lst_ModPack
            // 
            this.lst_ModPack.FormattingEnabled = true;
            this.lst_ModPack.ItemHeight = 12;
            this.lst_ModPack.Items.AddRange(new object[] {
            "BellCraft8",
            "TestPack"});
            this.lst_ModPack.Location = new System.Drawing.Point(297, 53);
            this.lst_ModPack.Name = "lst_ModPack";
            this.lst_ModPack.Size = new System.Drawing.Size(351, 160);
            this.lst_ModPack.TabIndex = 0;
            this.lst_ModPack.SelectedIndexChanged += new System.EventHandler(this.lst_ModPack_SelectedIndexChanged);
            // 
            // wb_PackNews
            // 
            this.wb_PackNews.Location = new System.Drawing.Point(0, 27);
            this.wb_PackNews.MinimumSize = new System.Drawing.Size(20, 20);
            this.wb_PackNews.Name = "wb_PackNews";
            this.wb_PackNews.Size = new System.Drawing.Size(297, 186);
            this.wb_PackNews.TabIndex = 1;
            // 
            // btn_Launch
            // 
            this.btn_Launch.Location = new System.Drawing.Point(536, 281);
            this.btn_Launch.Name = "btn_Launch";
            this.btn_Launch.Size = new System.Drawing.Size(112, 23);
            this.btn_Launch.TabIndex = 2;
            this.btn_Launch.Text = "Enjoy!";
            this.btn_Launch.UseVisualStyleBackColor = true;
            this.btn_Launch.Click += new System.EventHandler(this.btn_Launch_Click);
            // 
            // cb_Version
            // 
            this.cb_Version.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Version.Enabled = false;
            this.cb_Version.FormattingEnabled = true;
            this.cb_Version.Location = new System.Drawing.Point(467, 27);
            this.cb_Version.Name = "cb_Version";
            this.cb_Version.Size = new System.Drawing.Size(181, 20);
            this.cb_Version.TabIndex = 3;
            this.cb_Version.SelectedIndexChanged += new System.EventHandler(this.cb_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mCToolStripMenuItem,
            this.도구ToolStripMenuItem,
            this.정보ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(648, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ms_ItemClicked);
            // 
            // mCToolStripMenuItem
            // 
            this.mCToolStripMenuItem.Name = "mCToolStripMenuItem";
            this.mCToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.mCToolStripMenuItem.Text = "MC";
            // 
            // 도구ToolStripMenuItem
            // 
            this.도구ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mC환경설정ToolStripMenuItem});
            this.도구ToolStripMenuItem.Name = "도구ToolStripMenuItem";
            this.도구ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.도구ToolStripMenuItem.Text = "도구";
            // 
            // mC환경설정ToolStripMenuItem
            // 
            this.mC환경설정ToolStripMenuItem.Name = "mC환경설정ToolStripMenuItem";
            this.mC환경설정ToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.mC환경설정ToolStripMenuItem.Text = "MC환경설정";
            this.mC환경설정ToolStripMenuItem.Click += new System.EventHandler(this.ms_BSL_PreferenceToolStripMenuItem_Click);
            // 
            // 정보ToolStripMenuItem
            // 
            this.정보ToolStripMenuItem.Name = "정보ToolStripMenuItem";
            this.정보ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.정보ToolStripMenuItem.Text = "정보";
            // 
            // pb_Load
            // 
            this.pb_Load.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pb_Load.Location = new System.Drawing.Point(0, 303);
            this.pb_Load.Name = "pb_Load";
            this.pb_Load.Size = new System.Drawing.Size(648, 14);
            this.pb_Load.Step = 1;
            this.pb_Load.TabIndex = 6;
            // 
            // lb_Status
            // 
            this.lb_Status.AutoSize = true;
            this.lb_Status.Location = new System.Drawing.Point(295, 286);
            this.lb_Status.Name = "lb_Status";
            this.lb_Status.Size = new System.Drawing.Size(49, 12);
            this.lb_Status.TabIndex = 7;
            this.lb_Status.Text = "대기중..";
            // 
            // cb_AutoUpdate
            // 
            this.cb_AutoUpdate.AutoSize = true;
            this.cb_AutoUpdate.Enabled = false;
            this.cb_AutoUpdate.Location = new System.Drawing.Point(297, 236);
            this.cb_AutoUpdate.Name = "cb_AutoUpdate";
            this.cb_AutoUpdate.Size = new System.Drawing.Size(100, 16);
            this.cb_AutoUpdate.TabIndex = 8;
            this.cb_AutoUpdate.Text = "자동 업데이트";
            this.cb_AutoUpdate.UseVisualStyleBackColor = true;
            // 
            // cb_Profile
            // 
            this.cb_Profile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Profile.FormattingEnabled = true;
            this.cb_Profile.Items.AddRange(new object[] {
            "선택하세요",
            "=================="});
            this.cb_Profile.Location = new System.Drawing.Point(350, 255);
            this.cb_Profile.Name = "cb_Profile";
            this.cb_Profile.Size = new System.Drawing.Size(298, 20);
            this.cb_Profile.TabIndex = 9;
            this.cb_Profile.SelectedIndexChanged += new System.EventHandler(this.cb_Profile_SelectedIndexChanged);
            // 
            // btn_Preferences
            // 
            this.btn_Preferences.Location = new System.Drawing.Point(297, 213);
            this.btn_Preferences.Name = "btn_Preferences";
            this.btn_Preferences.Size = new System.Drawing.Size(94, 22);
            this.btn_Preferences.TabIndex = 10;
            this.btn_Preferences.Text = "런처 환경설정";
            this.btn_Preferences.UseVisualStyleBackColor = true;
            this.btn_Preferences.Click += new System.EventHandler(this.btn_Preferences_Click);
            // 
            // txt_Detail
            // 
            this.txt_Detail.Location = new System.Drawing.Point(0, 215);
            this.txt_Detail.Multiline = true;
            this.txt_Detail.Name = "txt_Detail";
            this.txt_Detail.ReadOnly = true;
            this.txt_Detail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Detail.Size = new System.Drawing.Size(297, 88);
            this.txt_Detail.TabIndex = 11;
            // 
            // btn_Option
            // 
            this.btn_Option.Location = new System.Drawing.Point(297, 27);
            this.btn_Option.Name = "btn_Option";
            this.btn_Option.Size = new System.Drawing.Size(75, 23);
            this.btn_Option.TabIndex = 12;
            this.btn_Option.Text = "옵션 모드";
            this.btn_Option.UseVisualStyleBackColor = true;
            this.btn_Option.Click += new System.EventHandler(this.btn_Option_Click);
            // 
            // btn_Edit
            // 
            this.btn_Edit.Enabled = false;
            this.btn_Edit.Location = new System.Drawing.Point(297, 253);
            this.btn_Edit.Name = "btn_Edit";
            this.btn_Edit.Size = new System.Drawing.Size(47, 23);
            this.btn_Edit.TabIndex = 13;
            this.btn_Edit.Text = "수정";
            this.btn_Edit.UseVisualStyleBackColor = true;
            this.btn_Edit.Click += new System.EventHandler(this.btn_Edit_Click);
            // 
            // BSL_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(648, 317);
            this.Controls.Add(this.btn_Edit);
            this.Controls.Add(this.btn_Option);
            this.Controls.Add(this.txt_Detail);
            this.Controls.Add(this.btn_Preferences);
            this.Controls.Add(this.cb_Profile);
            this.Controls.Add(this.cb_AutoUpdate);
            this.Controls.Add(this.lb_Status);
            this.Controls.Add(this.pb_Load);
            this.Controls.Add(this.cb_Version);
            this.Controls.Add(this.btn_Launch);
            this.Controls.Add(this.wb_PackNews);
            this.Controls.Add(this.lst_ModPack);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "BSL_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bell Smart Launcher";
            this.Load += new System.EventHandler(this.BSL_Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       

        #endregion

        private System.Windows.Forms.ListBox lst_ModPack;
        private System.Windows.Forms.WebBrowser wb_PackNews;
        private System.Windows.Forms.Button btn_Launch;
        private System.Windows.Forms.ComboBox cb_Version;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 도구ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 정보ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mC환경설정ToolStripMenuItem;
        private System.Windows.Forms.ProgressBar pb_Load;
        private System.Windows.Forms.Label lb_Status;
        private System.Windows.Forms.CheckBox cb_AutoUpdate;
        private System.Windows.Forms.ComboBox cb_Profile;
        private System.Windows.Forms.Button btn_Preferences;
        private System.Windows.Forms.TextBox txt_Detail;
        private System.Windows.Forms.Button btn_Option;
        private System.Windows.Forms.Button btn_Edit;
    }
}