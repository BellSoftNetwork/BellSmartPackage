namespace Bell_Smart_Server.Source.BST
{
    partial class BSS_Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BSS_Login));
            this.cb_PWSave = new System.Windows.Forms.CheckBox();
            this.cb_AutoLogin = new System.Windows.Forms.CheckBox();
            this.cb_EmailSave = new System.Windows.Forms.CheckBox();
            this.btn_Login = new System.Windows.Forms.Button();
            this.txt_PW = new System.Windows.Forms.TextBox();
            this.txt_Email = new System.Windows.Forms.TextBox();
            this.lb_PW = new System.Windows.Forms.Label();
            this.lb_Email = new System.Windows.Forms.Label();
            this.llb_text = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // cb_PWSave
            // 
            this.cb_PWSave.AutoSize = true;
            this.cb_PWSave.Location = new System.Drawing.Point(99, 76);
            this.cb_PWSave.Name = "cb_PWSave";
            this.cb_PWSave.Size = new System.Drawing.Size(100, 16);
            this.cb_PWSave.TabIndex = 110;
            this.cb_PWSave.Text = "비밀번호 저장";
            this.cb_PWSave.UseVisualStyleBackColor = true;
            this.cb_PWSave.CheckedChanged += new System.EventHandler(this.cb_PWSave_CheckedChanged);
            // 
            // cb_AutoLogin
            // 
            this.cb_AutoLogin.AutoSize = true;
            this.cb_AutoLogin.Location = new System.Drawing.Point(268, 76);
            this.cb_AutoLogin.Name = "cb_AutoLogin";
            this.cb_AutoLogin.Size = new System.Drawing.Size(88, 16);
            this.cb_AutoLogin.TabIndex = 106;
            this.cb_AutoLogin.Text = "자동 로그인";
            this.cb_AutoLogin.UseVisualStyleBackColor = true;
            this.cb_AutoLogin.CheckedChanged += new System.EventHandler(this.cb_AutoLogin_CheckedChanged);
            // 
            // cb_EmailSave
            // 
            this.cb_EmailSave.AutoSize = true;
            this.cb_EmailSave.Location = new System.Drawing.Point(5, 76);
            this.cb_EmailSave.Name = "cb_EmailSave";
            this.cb_EmailSave.Size = new System.Drawing.Size(88, 16);
            this.cb_EmailSave.TabIndex = 105;
            this.cb_EmailSave.Text = "이메일 저장";
            this.cb_EmailSave.UseVisualStyleBackColor = true;
            this.cb_EmailSave.CheckedChanged += new System.EventHandler(this.cb_EmailSave_CheckedChanged);
            // 
            // btn_Login
            // 
            this.btn_Login.Location = new System.Drawing.Point(317, 27);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(52, 43);
            this.btn_Login.TabIndex = 104;
            this.btn_Login.Text = "로그인";
            this.btn_Login.UseVisualStyleBackColor = true;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // txt_PW
            // 
            this.txt_PW.Font = new System.Drawing.Font("Wingdings", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.txt_PW.Location = new System.Drawing.Point(66, 49);
            this.txt_PW.MaxLength = 30;
            this.txt_PW.Name = "txt_PW";
            this.txt_PW.PasswordChar = 'l';
            this.txt_PW.Size = new System.Drawing.Size(248, 21);
            this.txt_PW.TabIndex = 103;
            // 
            // txt_Email
            // 
            this.txt_Email.Location = new System.Drawing.Point(66, 27);
            this.txt_Email.MaxLength = 30;
            this.txt_Email.Name = "txt_Email";
            this.txt_Email.Size = new System.Drawing.Size(248, 21);
            this.txt_Email.TabIndex = 102;
            // 
            // lb_PW
            // 
            this.lb_PW.AutoSize = true;
            this.lb_PW.Location = new System.Drawing.Point(-1, 53);
            this.lb_PW.Name = "lb_PW";
            this.lb_PW.Size = new System.Drawing.Size(61, 12);
            this.lb_PW.TabIndex = 107;
            this.lb_PW.Text = "비밀번호 :";
            this.lb_PW.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lb_Email
            // 
            this.lb_Email.AutoSize = true;
            this.lb_Email.Location = new System.Drawing.Point(11, 30);
            this.lb_Email.Name = "lb_Email";
            this.lb_Email.Size = new System.Drawing.Size(49, 12);
            this.lb_Email.TabIndex = 108;
            this.lb_Email.Text = "이메일 :";
            this.lb_Email.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // llb_text
            // 
            this.llb_text.AutoSize = true;
            this.llb_text.Location = new System.Drawing.Point(5, 2);
            this.llb_text.Name = "llb_text";
            this.llb_text.Size = new System.Drawing.Size(350, 12);
            this.llb_text.TabIndex = 109;
            this.llb_text.TabStop = true;
            this.llb_text.Text = "방울소프트네트워크 회원인증을위해 BSN 로그인이 필요합니다.";
            this.llb_text.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.llb_text.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llb_text_LinkClicked);
            // 
            // BSS_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(368, 91);
            this.Controls.Add(this.cb_PWSave);
            this.Controls.Add(this.cb_AutoLogin);
            this.Controls.Add(this.cb_EmailSave);
            this.Controls.Add(this.btn_Login);
            this.Controls.Add(this.txt_PW);
            this.Controls.Add(this.txt_Email);
            this.Controls.Add(this.lb_PW);
            this.Controls.Add(this.lb_Email);
            this.Controls.Add(this.llb_text);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BSS_Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bell Soft Network 회원 인증";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BSP_Login_FormClosing);
            this.Load += new System.EventHandler(this.BST_Login_Load);
            this.Shown += new System.EventHandler(this.BSP_Login_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.CheckBox cb_PWSave;
        internal System.Windows.Forms.CheckBox cb_AutoLogin;
        internal System.Windows.Forms.CheckBox cb_EmailSave;
        internal System.Windows.Forms.Button btn_Login;
        internal System.Windows.Forms.TextBox txt_PW;
        internal System.Windows.Forms.TextBox txt_Email;
        internal System.Windows.Forms.Label lb_PW;
        internal System.Windows.Forms.Label lb_Email;
        internal System.Windows.Forms.LinkLabel llb_text;
    }
}