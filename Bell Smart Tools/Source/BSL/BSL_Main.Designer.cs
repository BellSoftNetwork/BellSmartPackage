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
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
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
            this.lst_ModPack.Location = new System.Drawing.Point(455, 46);
            this.lst_ModPack.Name = "lst_ModPack";
            this.lst_ModPack.Size = new System.Drawing.Size(181, 100);
            this.lst_ModPack.TabIndex = 0;
            this.lst_ModPack.SelectedIndexChanged += new System.EventHandler(this.lst_ModPack_SelectedIndexChanged);
            // 
            // wb_PackNews
            // 
            this.wb_PackNews.Location = new System.Drawing.Point(17, 46);
            this.wb_PackNews.MinimumSize = new System.Drawing.Size(20, 20);
            this.wb_PackNews.Name = "wb_PackNews";
            this.wb_PackNews.Size = new System.Drawing.Size(280, 157);
            this.wb_PackNews.TabIndex = 1;
            this.wb_PackNews.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wb_PackNews_DocumentCompleted);
            // 
            // btn_Launch
            // 
            this.btn_Launch.Location = new System.Drawing.Point(524, 277);
            this.btn_Launch.Name = "btn_Launch";
            this.btn_Launch.Size = new System.Drawing.Size(112, 23);
            this.btn_Launch.TabIndex = 2;
            this.btn_Launch.Text = "START";
            this.btn_Launch.UseVisualStyleBackColor = true;
            this.btn_Launch.Click += new System.EventHandler(this.btn_Launch_Click);
            // 
            // cb_Version
            // 
            this.cb_Version.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Version.FormattingEnabled = true;
            this.cb_Version.Items.AddRange(new object[] {
            "선택하세요",
            "==================",
            "Latest",
            "Recommended"});
            this.cb_Version.Location = new System.Drawing.Point(455, 152);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 206);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Modpack Memo";
            this.label1.Click += new System.EventHandler(this.lb_modlist_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(3, 306);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(644, 14);
            this.progressBar1.TabIndex = 6;
            this.progressBar1.Click += new System.EventHandler(this.pb_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(453, 242);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "Status Log Lable";
            this.label2.Click += new System.EventHandler(this.lb_status_log_lable_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(432, 281);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(96, 16);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "자동업데이트";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.cb_auto_update_CheckedChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "선택하세요",
            "=================="});
            this.comboBox2.Location = new System.Drawing.Point(455, 191);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(180, 20);
            this.comboBox2.TabIndex = 9;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.cb_profile_list_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(338, 70);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 22);
            this.button1.TabIndex = 10;
            this.button1.Text = "MC환경설정";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.bt_MCpreference_Click);
            // 
            // BSL_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(648, 317);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button button1;
    }
}