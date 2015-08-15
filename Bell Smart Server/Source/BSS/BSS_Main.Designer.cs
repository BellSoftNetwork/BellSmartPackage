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
            this.btn_RuntimeManager = new System.Windows.Forms.Button();
            this.btn_PackMaker = new System.Windows.Forms.Button();
            this.btn_ServerManager = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_ModManager
            // 
            this.btn_ModManager.Location = new System.Drawing.Point(12, 77);
            this.btn_ModManager.Name = "btn_ModManager";
            this.btn_ModManager.Size = new System.Drawing.Size(135, 86);
            this.btn_ModManager.TabIndex = 0;
            this.btn_ModManager.Text = "모드 매니저";
            this.btn_ModManager.UseVisualStyleBackColor = true;
            this.btn_ModManager.Click += new System.EventHandler(this.btn_ModManager_Click);
            // 
            // btn_RuntimeManager
            // 
            this.btn_RuntimeManager.Location = new System.Drawing.Point(153, 77);
            this.btn_RuntimeManager.Name = "btn_RuntimeManager";
            this.btn_RuntimeManager.Size = new System.Drawing.Size(125, 86);
            this.btn_RuntimeManager.TabIndex = 1;
            this.btn_RuntimeManager.Text = "런타임 매니저";
            this.btn_RuntimeManager.UseVisualStyleBackColor = true;
            this.btn_RuntimeManager.Click += new System.EventHandler(this.btn_RuntimeManager_Click);
            // 
            // btn_PackMaker
            // 
            this.btn_PackMaker.Location = new System.Drawing.Point(284, 77);
            this.btn_PackMaker.Name = "btn_PackMaker";
            this.btn_PackMaker.Size = new System.Drawing.Size(141, 86);
            this.btn_PackMaker.TabIndex = 2;
            this.btn_PackMaker.Text = "팩 메이커";
            this.btn_PackMaker.UseVisualStyleBackColor = true;
            this.btn_PackMaker.Click += new System.EventHandler(this.btn_PackMaker_Click);
            // 
            // btn_ServerManager
            // 
            this.btn_ServerManager.Location = new System.Drawing.Point(311, 9);
            this.btn_ServerManager.Name = "btn_ServerManager";
            this.btn_ServerManager.Size = new System.Drawing.Size(95, 62);
            this.btn_ServerManager.TabIndex = 3;
            this.btn_ServerManager.Text = "서버 매니저";
            this.btn_ServerManager.UseVisualStyleBackColor = true;
            this.btn_ServerManager.Click += new System.EventHandler(this.btn_ServerManager_Click);
            // 
            // BSS_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(473, 175);
            this.Controls.Add(this.btn_ServerManager);
            this.Controls.Add(this.btn_PackMaker);
            this.Controls.Add(this.btn_RuntimeManager);
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
        private System.Windows.Forms.Button btn_RuntimeManager;
        private System.Windows.Forms.Button btn_PackMaker;
        private System.Windows.Forms.Button btn_ServerManager;
    }
}