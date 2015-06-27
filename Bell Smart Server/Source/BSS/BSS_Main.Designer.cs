namespace Bell_Smart_Server.Source.BSS
{
    partial class BSS_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BSS_Main));
            this.btn_ModManager = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_ModManager
            // 
            this.btn_ModManager.Location = new System.Drawing.Point(274, 77);
            this.btn_ModManager.Name = "btn_ModManager";
            this.btn_ModManager.Size = new System.Drawing.Size(187, 86);
            this.btn_ModManager.TabIndex = 0;
            this.btn_ModManager.Text = "모드 매니저";
            this.btn_ModManager.UseVisualStyleBackColor = true;
            this.btn_ModManager.Click += new System.EventHandler(this.btn_ModManager_Click);
            // 
            // BSS_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(473, 175);
            this.Controls.Add(this.btn_ModManager);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BSS_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BSS 메인";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BSS_Main_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_ModManager;
    }
}