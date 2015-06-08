namespace Bell_Smart_Tools.Source.BST
{
    partial class BST_Debug
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BST_Debug));
            this.btn_OpenLog = new System.Windows.Forms.Button();
            this.btn_DeleteDL = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_OpenLog
            // 
            this.btn_OpenLog.Location = new System.Drawing.Point(0, 0);
            this.btn_OpenLog.Name = "btn_OpenLog";
            this.btn_OpenLog.Size = new System.Drawing.Size(142, 23);
            this.btn_OpenLog.TabIndex = 3;
            this.btn_OpenLog.Text = "현재 디버그 로그 열기";
            this.btn_OpenLog.UseVisualStyleBackColor = true;
            this.btn_OpenLog.Click += new System.EventHandler(this.btn_OpenLog_Click);
            // 
            // btn_DeleteDL
            // 
            this.btn_DeleteDL.Location = new System.Drawing.Point(0, 21);
            this.btn_DeleteDL.Name = "btn_DeleteDL";
            this.btn_DeleteDL.Size = new System.Drawing.Size(142, 23);
            this.btn_DeleteDL.TabIndex = 2;
            this.btn_DeleteDL.Text = "디버그 로그 전체 삭제";
            this.btn_DeleteDL.UseVisualStyleBackColor = true;
            this.btn_DeleteDL.Click += new System.EventHandler(this.btn_DeleteDL_Click);
            // 
            // BST_Debug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(298, 126);
            this.Controls.Add(this.btn_OpenLog);
            this.Controls.Add(this.btn_DeleteDL);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BST_Debug";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bell Smart Debug Tools";
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button btn_OpenLog;
        internal System.Windows.Forms.Button btn_DeleteDL;
    }
}