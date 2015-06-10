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
            this.lb_ModPack = new System.Windows.Forms.ListBox();
            this.wb_PackNews = new System.Windows.Forms.WebBrowser();
            this.btn_Launch = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.도구ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.정보ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mC환경설정ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_ModPack
            // 
            this.lb_ModPack.FormattingEnabled = true;
            this.lb_ModPack.ItemHeight = 12;
            this.lb_ModPack.Items.AddRange(new object[] {
            "BellCraft8",
            "TestPack"});
            this.lb_ModPack.Location = new System.Drawing.Point(455, 84);
            this.lb_ModPack.Name = "lb_ModPack";
            this.lb_ModPack.Size = new System.Drawing.Size(181, 100);
            this.lb_ModPack.TabIndex = 0;
            this.lb_ModPack.SelectedIndexChanged += new System.EventHandler(this.lb_ModPack_SelectedIndexChanged);
            // 
            // wb_PackNews
            // 
            this.wb_PackNews.Location = new System.Drawing.Point(12, 84);
            this.wb_PackNews.MinimumSize = new System.Drawing.Size(20, 20);
            this.wb_PackNews.Name = "wb_PackNews";
            this.wb_PackNews.Size = new System.Drawing.Size(280, 173);
            this.wb_PackNews.TabIndex = 1;
            this.wb_PackNews.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wb_PackNews_DocumentCompleted);
            // 
            // btn_Launch
            // 
            this.btn_Launch.Location = new System.Drawing.Point(535, 293);
            this.btn_Launch.Name = "btn_Launch";
            this.btn_Launch.Size = new System.Drawing.Size(112, 23);
            this.btn_Launch.TabIndex = 2;
            this.btn_Launch.Text = "Launch";
            this.btn_Launch.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Latest",
            "Recommended"});
            this.comboBox1.Location = new System.Drawing.Point(455, 237);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(181, 20);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.cb_SelectedIndexChanged);
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
            // 정보ToolStripMenuItem
            // 
            this.정보ToolStripMenuItem.Name = "정보ToolStripMenuItem";
            this.정보ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.정보ToolStripMenuItem.Text = "정보";
            // 
            // mC환경설정ToolStripMenuItem
            // 
            this.mC환경설정ToolStripMenuItem.Name = "mC환경설정ToolStripMenuItem";
            this.mC환경설정ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mC환경설정ToolStripMenuItem.Text = "MC환경설정";
            this.mC환경설정ToolStripMenuItem.Click += new System.EventHandler(this.ms_BSL_PreferenceToolStripMenuItem_Click);
            // 
            // BSL_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(648, 317);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btn_Launch);
            this.Controls.Add(this.wb_PackNews);
            this.Controls.Add(this.lb_ModPack);
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

        private System.Windows.Forms.ListBox lb_ModPack;
        private System.Windows.Forms.WebBrowser wb_PackNews;
        private System.Windows.Forms.Button btn_Launch;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 도구ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 정보ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mC환경설정ToolStripMenuItem;
    }
}