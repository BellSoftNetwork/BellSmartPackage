namespace Bell_Smart_Server.Source.BSL
{
    partial class BSL_Password
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BSL_Password));
            this.txt_Password = new System.Windows.Forms.TextBox();
            this.btn_Apply = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_Password
            // 
            this.txt_Password.Dock = System.Windows.Forms.DockStyle.Left;
            this.txt_Password.Location = new System.Drawing.Point(0, 0);
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.PasswordChar = '*';
            this.txt_Password.Size = new System.Drawing.Size(346, 21);
            this.txt_Password.TabIndex = 0;
            this.txt_Password.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Password_KeyDown);
            // 
            // btn_Apply
            // 
            this.btn_Apply.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Apply.Location = new System.Drawing.Point(352, 0);
            this.btn_Apply.Name = "btn_Apply";
            this.btn_Apply.Size = new System.Drawing.Size(54, 21);
            this.btn_Apply.TabIndex = 1;
            this.btn_Apply.Text = "확인";
            this.btn_Apply.UseVisualStyleBackColor = true;
            this.btn_Apply.Click += new System.EventHandler(this.btn_Apply_Click);
            // 
            // BSL_Password
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(406, 21);
            this.Controls.Add(this.btn_Apply);
            this.Controls.Add(this.txt_Password);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BSL_Password";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "패스워드 입력";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Password;
        private System.Windows.Forms.Button btn_Apply;
    }
}