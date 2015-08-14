namespace Bell_Smart_Server.Source.BST
{
    partial class BSS_Updater
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BSS_Updater));
            this.pb_Down = new System.Windows.Forms.ProgressBar();
            this.downloadStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pb_Down
            // 
            this.pb_Down.Dock = System.Windows.Forms.DockStyle.Top;
            this.pb_Down.Location = new System.Drawing.Point(0, 0);
            this.pb_Down.Name = "pb_Down";
            this.pb_Down.Size = new System.Drawing.Size(435, 51);
            this.pb_Down.TabIndex = 0;
            // 
            // downloadStatus
            // 
            this.downloadStatus.Location = new System.Drawing.Point(-2, 52);
            this.downloadStatus.Name = "downloadStatus";
            this.downloadStatus.Size = new System.Drawing.Size(437, 39);
            this.downloadStatus.TabIndex = 3;
            this.downloadStatus.Text = "초기화중..";
            // 
            // BSS_Updater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(435, 94);
            this.Controls.Add(this.downloadStatus);
            this.Controls.Add(this.pb_Down);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BSS_Updater";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BSS 업데이터";
            this.Load += new System.EventHandler(this.BSP_Updater_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label downloadStatus;
        private System.Windows.Forms.ProgressBar pb_Down;
    }
}