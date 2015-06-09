﻿namespace Bell_Smart_Tools.Source.BSL
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
            "BellCraft8",
            "TestPack"});
            this.comboBox1.Location = new System.Drawing.Point(455, 237);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(181, 20);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.cb_SelectedIndexChanged);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BSL_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bell Smart Launcher";
            this.Load += new System.EventHandler(this.BSL_Main_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lb_ModPack;
        private System.Windows.Forms.WebBrowser wb_PackNews;
        private System.Windows.Forms.Button btn_Launch;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}