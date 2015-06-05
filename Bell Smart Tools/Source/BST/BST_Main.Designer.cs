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
            this.SuspendLayout();
            // 
            // txt_Notice
            // 
            this.txt_Notice.Location = new System.Drawing.Point(0, 0);
            this.txt_Notice.Multiline = true;
            this.txt_Notice.Name = "txt_Notice";
            this.txt_Notice.ReadOnly = true;
            this.txt_Notice.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Notice.Size = new System.Drawing.Size(316, 251);
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
            this.cb_PackList.Location = new System.Drawing.Point(322, 208);
            this.cb_PackList.Name = "cb_PackList";
            this.cb_PackList.Size = new System.Drawing.Size(216, 20);
            this.cb_PackList.TabIndex = 1;
            this.cb_PackList.Text = "플레이하실 모드팩을 선택해주세요.";
            // 
            // btn_GameStart
            // 
            this.btn_GameStart.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_GameStart.Location = new System.Drawing.Point(322, 228);
            this.btn_GameStart.Name = "btn_GameStart";
            this.btn_GameStart.Size = new System.Drawing.Size(216, 23);
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
            // BST_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(538, 251);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_GameStart);
            this.Controls.Add(this.cb_PackList);
            this.Controls.Add(this.txt_Notice);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BST_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BST 메인";
            this.Load += new System.EventHandler(this.BST_Main_Load);
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

    }
}