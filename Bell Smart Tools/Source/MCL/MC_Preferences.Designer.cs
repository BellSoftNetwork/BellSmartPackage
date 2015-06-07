namespace Bell_Smart_Tools.Source.MCL
{
    partial class MC_Preferences
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MC_Preferences));
            this.gb_MCAccount = new System.Windows.Forms.GroupBox();
            this.btn_MCLogout = new System.Windows.Forms.Button();
            this.btn_MCLogin = new System.Windows.Forms.Button();
            this.txt_PW = new System.Windows.Forms.TextBox();
            this.txt_ID = new System.Windows.Forms.TextBox();
            this.lb_PW = new System.Windows.Forms.Label();
            this.lb_ID = new System.Windows.Forms.Label();
            this.gb_MCSetting = new System.Windows.Forms.GroupBox();
            this.txt_ClientURL = new System.Windows.Forms.TextBox();
            this.lb_JVM = new System.Windows.Forms.Label();
            this.txt_JAVAURL = new System.Windows.Forms.TextBox();
            this.lb_Java = new System.Windows.Forms.Label();
            this.btn_JavaSearch = new System.Windows.Forms.Button();
            this.lb_Client = new System.Windows.Forms.Label();
            this.cb_Console = new System.Windows.Forms.CheckBox();
            this.txt_Parameter = new System.Windows.Forms.TextBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.gb_MCAccount.SuspendLayout();
            this.gb_MCSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_MCAccount
            // 
            this.gb_MCAccount.Controls.Add(this.btn_MCLogout);
            this.gb_MCAccount.Controls.Add(this.btn_MCLogin);
            this.gb_MCAccount.Controls.Add(this.txt_PW);
            this.gb_MCAccount.Controls.Add(this.txt_ID);
            this.gb_MCAccount.Controls.Add(this.lb_PW);
            this.gb_MCAccount.Controls.Add(this.lb_ID);
            this.gb_MCAccount.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gb_MCAccount.ForeColor = System.Drawing.Color.Red;
            this.gb_MCAccount.Location = new System.Drawing.Point(0, 0);
            this.gb_MCAccount.Name = "gb_MCAccount";
            this.gb_MCAccount.Size = new System.Drawing.Size(585, 91);
            this.gb_MCAccount.TabIndex = 70;
            this.gb_MCAccount.TabStop = false;
            this.gb_MCAccount.Text = "마인크래프트 계정인증";
            // 
            // btn_MCLogout
            // 
            this.btn_MCLogout.Enabled = false;
            this.btn_MCLogout.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_MCLogout.ForeColor = System.Drawing.Color.Black;
            this.btn_MCLogout.Location = new System.Drawing.Point(424, 67);
            this.btn_MCLogout.Name = "btn_MCLogout";
            this.btn_MCLogout.Size = new System.Drawing.Size(155, 23);
            this.btn_MCLogout.TabIndex = 3;
            this.btn_MCLogout.Text = "로그아웃";
            this.btn_MCLogout.UseVisualStyleBackColor = true;
            this.btn_MCLogout.Click += new System.EventHandler(this.btn_MCLogout_Click);
            // 
            // btn_MCLogin
            // 
            this.btn_MCLogin.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_MCLogin.ForeColor = System.Drawing.Color.Black;
            this.btn_MCLogin.Location = new System.Drawing.Point(252, 67);
            this.btn_MCLogin.Name = "btn_MCLogin";
            this.btn_MCLogin.Size = new System.Drawing.Size(166, 23);
            this.btn_MCLogin.TabIndex = 2;
            this.btn_MCLogin.Text = "로그인";
            this.btn_MCLogin.UseVisualStyleBackColor = true;
            this.btn_MCLogin.Click += new System.EventHandler(this.btn_MCLogin_Click);
            // 
            // txt_PW
            // 
            this.txt_PW.Location = new System.Drawing.Point(123, 39);
            this.txt_PW.Name = "txt_PW";
            this.txt_PW.Size = new System.Drawing.Size(456, 22);
            this.txt_PW.TabIndex = 1;
            this.txt_PW.UseSystemPasswordChar = true;
            // 
            // txt_ID
            // 
            this.txt_ID.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_ID.Location = new System.Drawing.Point(123, 18);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Size = new System.Drawing.Size(456, 21);
            this.txt_ID.TabIndex = 0;
            // 
            // lb_PW
            // 
            this.lb_PW.AutoSize = true;
            this.lb_PW.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_PW.ForeColor = System.Drawing.Color.Black;
            this.lb_PW.Location = new System.Drawing.Point(0, 43);
            this.lb_PW.Name = "lb_PW";
            this.lb_PW.Size = new System.Drawing.Size(65, 12);
            this.lb_PW.TabIndex = 0;
            this.lb_PW.Text = "비밀번호 : ";
            // 
            // lb_ID
            // 
            this.lb_ID.AutoSize = true;
            this.lb_ID.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_ID.ForeColor = System.Drawing.Color.Black;
            this.lb_ID.Location = new System.Drawing.Point(0, 21);
            this.lb_ID.Name = "lb_ID";
            this.lb_ID.Size = new System.Drawing.Size(121, 12);
            this.lb_ID.TabIndex = 0;
            this.lb_ID.Text = "이메일 또는 닉네임 : ";
            // 
            // gb_MCSetting
            // 
            this.gb_MCSetting.Controls.Add(this.txt_ClientURL);
            this.gb_MCSetting.Controls.Add(this.lb_JVM);
            this.gb_MCSetting.Controls.Add(this.txt_JAVAURL);
            this.gb_MCSetting.Controls.Add(this.lb_Java);
            this.gb_MCSetting.Controls.Add(this.btn_JavaSearch);
            this.gb_MCSetting.Controls.Add(this.lb_Client);
            this.gb_MCSetting.Controls.Add(this.cb_Console);
            this.gb_MCSetting.Controls.Add(this.txt_Parameter);
            this.gb_MCSetting.Enabled = false;
            this.gb_MCSetting.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gb_MCSetting.ForeColor = System.Drawing.Color.Blue;
            this.gb_MCSetting.Location = new System.Drawing.Point(0, 97);
            this.gb_MCSetting.Name = "gb_MCSetting";
            this.gb_MCSetting.Size = new System.Drawing.Size(585, 111);
            this.gb_MCSetting.TabIndex = 71;
            this.gb_MCSetting.TabStop = false;
            this.gb_MCSetting.Text = "마인크래프트 설정";
            // 
            // txt_ClientURL
            // 
            this.txt_ClientURL.Enabled = false;
            this.txt_ClientURL.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_ClientURL.Location = new System.Drawing.Point(101, 40);
            this.txt_ClientURL.Name = "txt_ClientURL";
            this.txt_ClientURL.ReadOnly = true;
            this.txt_ClientURL.Size = new System.Drawing.Size(417, 21);
            this.txt_ClientURL.TabIndex = 63;
            // 
            // lb_JVM
            // 
            this.lb_JVM.AutoSize = true;
            this.lb_JVM.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_JVM.ForeColor = System.Drawing.Color.Black;
            this.lb_JVM.Location = new System.Drawing.Point(-1, 70);
            this.lb_JVM.Name = "lb_JVM";
            this.lb_JVM.Size = new System.Drawing.Size(100, 12);
            this.lb_JVM.TabIndex = 67;
            this.lb_JVM.Text = "JVM Parameter :";
            // 
            // txt_JAVAURL
            // 
            this.txt_JAVAURL.Enabled = false;
            this.txt_JAVAURL.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_JAVAURL.Location = new System.Drawing.Point(101, 19);
            this.txt_JAVAURL.Name = "txt_JAVAURL";
            this.txt_JAVAURL.ReadOnly = true;
            this.txt_JAVAURL.Size = new System.Drawing.Size(417, 21);
            this.txt_JAVAURL.TabIndex = 53;
            // 
            // lb_Java
            // 
            this.lb_Java.AutoSize = true;
            this.lb_Java.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_Java.ForeColor = System.Drawing.Color.Black;
            this.lb_Java.Location = new System.Drawing.Point(-1, 23);
            this.lb_Java.Name = "lb_Java";
            this.lb_Java.Size = new System.Drawing.Size(65, 12);
            this.lb_Java.TabIndex = 55;
            this.lb_Java.Text = "자바 경로 :";
            // 
            // btn_JavaSearch
            // 
            this.btn_JavaSearch.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_JavaSearch.ForeColor = System.Drawing.Color.Black;
            this.btn_JavaSearch.Location = new System.Drawing.Point(524, 19);
            this.btn_JavaSearch.Name = "btn_JavaSearch";
            this.btn_JavaSearch.Size = new System.Drawing.Size(53, 21);
            this.btn_JavaSearch.TabIndex = 4;
            this.btn_JavaSearch.Text = "찾기";
            this.btn_JavaSearch.UseVisualStyleBackColor = true;
            // 
            // lb_Client
            // 
            this.lb_Client.AutoSize = true;
            this.lb_Client.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_Client.ForeColor = System.Drawing.Color.Black;
            this.lb_Client.Location = new System.Drawing.Point(-2, 44);
            this.lb_Client.Name = "lb_Client";
            this.lb_Client.Size = new System.Drawing.Size(105, 12);
            this.lb_Client.TabIndex = 62;
            this.lb_Client.Text = "클라이언트 경로 : ";
            // 
            // cb_Console
            // 
            this.cb_Console.AutoSize = true;
            this.cb_Console.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_Console.ForeColor = System.Drawing.Color.Black;
            this.cb_Console.Location = new System.Drawing.Point(101, 94);
            this.cb_Console.Name = "cb_Console";
            this.cb_Console.Size = new System.Drawing.Size(117, 16);
            this.cb_Console.TabIndex = 6;
            this.cb_Console.Text = "Console running";
            this.cb_Console.UseVisualStyleBackColor = true;
            // 
            // txt_Parameter
            // 
            this.txt_Parameter.Location = new System.Drawing.Point(101, 67);
            this.txt_Parameter.Name = "txt_Parameter";
            this.txt_Parameter.Size = new System.Drawing.Size(417, 22);
            this.txt_Parameter.TabIndex = 5;
            this.txt_Parameter.Text = "-Xmx2G";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(476, 214);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(109, 23);
            this.btn_Cancel.TabIndex = 73;
            this.btn_Cancel.Text = "취소";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Enabled = false;
            this.btn_Save.Location = new System.Drawing.Point(356, 214);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(114, 23);
            this.btn_Save.TabIndex = 72;
            this.btn_Save.Text = "저장";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // MC_Preferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(585, 236);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.gb_MCSetting);
            this.Controls.Add(this.gb_MCAccount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MC_Preferences";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "마인크래프트 환경설정";
            this.Load += new System.EventHandler(this.MC_Preferences_Load);
            this.gb_MCAccount.ResumeLayout(false);
            this.gb_MCAccount.PerformLayout();
            this.gb_MCSetting.ResumeLayout(false);
            this.gb_MCSetting.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox gb_MCAccount;
        internal System.Windows.Forms.Button btn_MCLogout;
        internal System.Windows.Forms.Button btn_MCLogin;
        internal System.Windows.Forms.TextBox txt_PW;
        internal System.Windows.Forms.TextBox txt_ID;
        internal System.Windows.Forms.Label lb_PW;
        internal System.Windows.Forms.Label lb_ID;
        internal System.Windows.Forms.GroupBox gb_MCSetting;
        internal System.Windows.Forms.TextBox txt_ClientURL;
        internal System.Windows.Forms.Label lb_JVM;
        internal System.Windows.Forms.TextBox txt_JAVAURL;
        internal System.Windows.Forms.Label lb_Java;
        internal System.Windows.Forms.Button btn_JavaSearch;
        internal System.Windows.Forms.Label lb_Client;
        internal System.Windows.Forms.CheckBox cb_Console;
        internal System.Windows.Forms.TextBox txt_Parameter;
        internal System.Windows.Forms.Button btn_Cancel;
        internal System.Windows.Forms.Button btn_Save;
    }
}