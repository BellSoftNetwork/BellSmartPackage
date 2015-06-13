namespace Bell_Smart_Tools.Source.BSL
{
    partial class BSL_Profile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BSL_Profile));
            this.lb_ID = new System.Windows.Forms.Label();
            this.lb_PW = new System.Windows.Forms.Label();
            this.lb_Name = new System.Windows.Forms.Label();
            this.txt_ID = new System.Windows.Forms.TextBox();
            this.txt_PW = new System.Windows.Forms.TextBox();
            this.txt_Name = new System.Windows.Forms.TextBox();
            this.cb_SavePW = new System.Windows.Forms.CheckBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.gb_Info = new System.Windows.Forms.GroupBox();
            this.gb_Java = new System.Windows.Forms.GroupBox();
            this.txt_Parameter = new System.Windows.Forms.TextBox();
            this.txt_Java = new System.Windows.Forms.TextBox();
            this.cb_Parameter = new System.Windows.Forms.CheckBox();
            this.cb_Java = new System.Windows.Forms.CheckBox();
            this.gb_Info.SuspendLayout();
            this.gb_Java.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_ID
            // 
            this.lb_ID.AutoSize = true;
            this.lb_ID.Location = new System.Drawing.Point(6, 50);
            this.lb_ID.Name = "lb_ID";
            this.lb_ID.Size = new System.Drawing.Size(115, 12);
            this.lb_ID.TabIndex = 0;
            this.lb_ID.Text = "닉네임/이메일주소 :";
            // 
            // lb_PW
            // 
            this.lb_PW.AutoSize = true;
            this.lb_PW.Location = new System.Drawing.Point(6, 77);
            this.lb_PW.Name = "lb_PW";
            this.lb_PW.Size = new System.Drawing.Size(61, 12);
            this.lb_PW.TabIndex = 1;
            this.lb_PW.Text = "비밀번호 :";
            // 
            // lb_Name
            // 
            this.lb_Name.AutoSize = true;
            this.lb_Name.Location = new System.Drawing.Point(6, 23);
            this.lb_Name.Name = "lb_Name";
            this.lb_Name.Size = new System.Drawing.Size(77, 12);
            this.lb_Name.TabIndex = 2;
            this.lb_Name.Text = "프로필 이름 :";
            // 
            // txt_ID
            // 
            this.txt_ID.Location = new System.Drawing.Point(127, 47);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Size = new System.Drawing.Size(253, 21);
            this.txt_ID.TabIndex = 3;
            // 
            // txt_PW
            // 
            this.txt_PW.Location = new System.Drawing.Point(127, 74);
            this.txt_PW.Name = "txt_PW";
            this.txt_PW.Size = new System.Drawing.Size(253, 21);
            this.txt_PW.TabIndex = 4;
            // 
            // txt_Name
            // 
            this.txt_Name.Location = new System.Drawing.Point(127, 20);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.Size = new System.Drawing.Size(253, 21);
            this.txt_Name.TabIndex = 5;
            // 
            // cb_SavePW
            // 
            this.cb_SavePW.AutoSize = true;
            this.cb_SavePW.Checked = true;
            this.cb_SavePW.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_SavePW.Location = new System.Drawing.Point(127, 101);
            this.cb_SavePW.Name = "cb_SavePW";
            this.cb_SavePW.Size = new System.Drawing.Size(100, 16);
            this.cb_SavePW.TabIndex = 6;
            this.cb_SavePW.Text = "비밀번호 저장";
            this.cb_SavePW.UseVisualStyleBackColor = true;
            this.cb_SavePW.CheckedChanged += new System.EventHandler(this.cb_SavePW_CheckedChanged);
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(119, 217);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 7;
            this.btn_Save.Text = "저장";
            this.btn_Save.UseVisualStyleBackColor = true;
            // 
            // btn_Delete
            // 
            this.btn_Delete.Location = new System.Drawing.Point(200, 217);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(75, 23);
            this.btn_Delete.TabIndex = 8;
            this.btn_Delete.Text = "삭제";
            this.btn_Delete.UseVisualStyleBackColor = true;
            // 
            // gb_Info
            // 
            this.gb_Info.Controls.Add(this.txt_ID);
            this.gb_Info.Controls.Add(this.lb_ID);
            this.gb_Info.Controls.Add(this.lb_PW);
            this.gb_Info.Controls.Add(this.lb_Name);
            this.gb_Info.Controls.Add(this.cb_SavePW);
            this.gb_Info.Controls.Add(this.txt_Name);
            this.gb_Info.Controls.Add(this.txt_PW);
            this.gb_Info.Location = new System.Drawing.Point(12, 12);
            this.gb_Info.Name = "gb_Info";
            this.gb_Info.Size = new System.Drawing.Size(391, 123);
            this.gb_Info.TabIndex = 9;
            this.gb_Info.TabStop = false;
            this.gb_Info.Text = "프로필 정보";
            // 
            // gb_Java
            // 
            this.gb_Java.Controls.Add(this.txt_Parameter);
            this.gb_Java.Controls.Add(this.txt_Java);
            this.gb_Java.Controls.Add(this.cb_Parameter);
            this.gb_Java.Controls.Add(this.cb_Java);
            this.gb_Java.Location = new System.Drawing.Point(12, 141);
            this.gb_Java.Name = "gb_Java";
            this.gb_Java.Size = new System.Drawing.Size(391, 70);
            this.gb_Java.TabIndex = 10;
            this.gb_Java.TabStop = false;
            this.gb_Java.Text = "자바 세부설정";
            // 
            // txt_Parameter
            // 
            this.txt_Parameter.Location = new System.Drawing.Point(68, 42);
            this.txt_Parameter.Name = "txt_Parameter";
            this.txt_Parameter.ReadOnly = true;
            this.txt_Parameter.Size = new System.Drawing.Size(312, 21);
            this.txt_Parameter.TabIndex = 3;
            // 
            // txt_Java
            // 
            this.txt_Java.Location = new System.Drawing.Point(68, 18);
            this.txt_Java.Name = "txt_Java";
            this.txt_Java.ReadOnly = true;
            this.txt_Java.Size = new System.Drawing.Size(312, 21);
            this.txt_Java.TabIndex = 2;
            // 
            // cb_Parameter
            // 
            this.cb_Parameter.AutoSize = true;
            this.cb_Parameter.Location = new System.Drawing.Point(6, 44);
            this.cb_Parameter.Name = "cb_Parameter";
            this.cb_Parameter.Size = new System.Drawing.Size(56, 16);
            this.cb_Parameter.TabIndex = 1;
            this.cb_Parameter.Text = "인자 :";
            this.cb_Parameter.UseVisualStyleBackColor = true;
            this.cb_Parameter.CheckedChanged += new System.EventHandler(this.cb_Parameter_CheckedChanged);
            // 
            // cb_Java
            // 
            this.cb_Java.AutoSize = true;
            this.cb_Java.Location = new System.Drawing.Point(6, 20);
            this.cb_Java.Name = "cb_Java";
            this.cb_Java.Size = new System.Drawing.Size(56, 16);
            this.cb_Java.TabIndex = 0;
            this.cb_Java.Text = "실행 :";
            this.cb_Java.UseVisualStyleBackColor = true;
            this.cb_Java.CheckedChanged += new System.EventHandler(this.cb_Java_CheckedChanged);
            // 
            // BSL_Profile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(408, 242);
            this.Controls.Add(this.gb_Java);
            this.Controls.Add(this.gb_Info);
            this.Controls.Add(this.btn_Delete);
            this.Controls.Add(this.btn_Save);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BSL_Profile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "프로필 편집";
            this.gb_Info.ResumeLayout(false);
            this.gb_Info.PerformLayout();
            this.gb_Java.ResumeLayout(false);
            this.gb_Java.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lb_ID;
        private System.Windows.Forms.Label lb_PW;
        private System.Windows.Forms.Label lb_Name;
        private System.Windows.Forms.TextBox txt_ID;
        private System.Windows.Forms.TextBox txt_PW;
        private System.Windows.Forms.TextBox txt_Name;
        private System.Windows.Forms.CheckBox cb_SavePW;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.GroupBox gb_Info;
        private System.Windows.Forms.GroupBox gb_Java;
        private System.Windows.Forms.CheckBox cb_Parameter;
        private System.Windows.Forms.CheckBox cb_Java;
        private System.Windows.Forms.TextBox txt_Java;
        private System.Windows.Forms.TextBox txt_Parameter;
    }
}