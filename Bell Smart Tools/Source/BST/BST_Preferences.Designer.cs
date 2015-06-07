namespace Bell_Smart_Tools.Source.BST
{
    partial class BST_Preferences
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BST_Preferences));
            this.btn_DisAuto = new System.Windows.Forms.Button();
            this.lb_email = new System.Windows.Forms.Label();
            this.lb_PW = new System.Windows.Forms.Label();
            this.cb_AutoUpdate = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btn_DisAuto
            // 
            this.btn_DisAuto.Enabled = false;
            this.btn_DisAuto.Location = new System.Drawing.Point(210, 0);
            this.btn_DisAuto.Name = "btn_DisAuto";
            this.btn_DisAuto.Size = new System.Drawing.Size(128, 24);
            this.btn_DisAuto.TabIndex = 0;
            this.btn_DisAuto.Text = "자동로그인 해제";
            this.btn_DisAuto.UseVisualStyleBackColor = true;
            this.btn_DisAuto.Click += new System.EventHandler(this.btn_DisAuto_Click);
            // 
            // lb_email
            // 
            this.lb_email.Location = new System.Drawing.Point(0, 0);
            this.lb_email.Name = "lb_email";
            this.lb_email.Size = new System.Drawing.Size(202, 12);
            this.lb_email.TabIndex = 1;
            this.lb_email.Text = "이메일 저장 : Loading..";
            // 
            // lb_PW
            // 
            this.lb_PW.Location = new System.Drawing.Point(-2, 12);
            this.lb_PW.Name = "lb_PW";
            this.lb_PW.Size = new System.Drawing.Size(204, 12);
            this.lb_PW.TabIndex = 2;
            this.lb_PW.Text = "비밀번호 저장 : Loading..";
            // 
            // cb_AutoUpdate
            // 
            this.cb_AutoUpdate.AutoSize = true;
            this.cb_AutoUpdate.Location = new System.Drawing.Point(1, 31);
            this.cb_AutoUpdate.Name = "cb_AutoUpdate";
            this.cb_AutoUpdate.Size = new System.Drawing.Size(128, 16);
            this.cb_AutoUpdate.TabIndex = 3;
            this.cb_AutoUpdate.Text = "BSP 자동 업데이트";
            this.cb_AutoUpdate.UseVisualStyleBackColor = true;
            this.cb_AutoUpdate.CheckedChanged += new System.EventHandler(this.cb_AutoUpdate_CheckedChanged);
            // 
            // BST_Preferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(338, 180);
            this.Controls.Add(this.cb_AutoUpdate);
            this.Controls.Add(this.lb_PW);
            this.Controls.Add(this.lb_email);
            this.Controls.Add(this.btn_DisAuto);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BST_Preferences";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BST 환경설정";
            this.Load += new System.EventHandler(this.BST_Preferences_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_DisAuto;
        private System.Windows.Forms.Label lb_email;
        private System.Windows.Forms.Label lb_PW;
        private System.Windows.Forms.CheckBox cb_AutoUpdate;
    }
}