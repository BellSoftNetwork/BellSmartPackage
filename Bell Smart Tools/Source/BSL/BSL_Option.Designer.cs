namespace Bell_Smart_Server.Source.BSL
{
    partial class BSL_Option
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BSL_Option));
            this.lst_Enable = new System.Windows.Forms.ListBox();
            this.lst_Disable = new System.Windows.Forms.ListBox();
            this.btn_Enable = new System.Windows.Forms.Button();
            this.btn_Disable = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lst_Enable
            // 
            this.lst_Enable.Dock = System.Windows.Forms.DockStyle.Left;
            this.lst_Enable.FormattingEnabled = true;
            this.lst_Enable.ItemHeight = 12;
            this.lst_Enable.Location = new System.Drawing.Point(0, 0);
            this.lst_Enable.Name = "lst_Enable";
            this.lst_Enable.Size = new System.Drawing.Size(242, 411);
            this.lst_Enable.TabIndex = 0;
            // 
            // lst_Disable
            // 
            this.lst_Disable.Dock = System.Windows.Forms.DockStyle.Right;
            this.lst_Disable.FormattingEnabled = true;
            this.lst_Disable.ItemHeight = 12;
            this.lst_Disable.Location = new System.Drawing.Point(336, 0);
            this.lst_Disable.Name = "lst_Disable";
            this.lst_Disable.Size = new System.Drawing.Size(258, 411);
            this.lst_Disable.TabIndex = 1;
            // 
            // btn_Enable
            // 
            this.btn_Enable.Location = new System.Drawing.Point(248, 156);
            this.btn_Enable.Name = "btn_Enable";
            this.btn_Enable.Size = new System.Drawing.Size(82, 23);
            this.btn_Enable.TabIndex = 2;
            this.btn_Enable.Text = "<< 활성화";
            this.btn_Enable.UseVisualStyleBackColor = true;
            // 
            // btn_Disable
            // 
            this.btn_Disable.Location = new System.Drawing.Point(248, 185);
            this.btn_Disable.Name = "btn_Disable";
            this.btn_Disable.Size = new System.Drawing.Size(82, 23);
            this.btn_Disable.TabIndex = 3;
            this.btn_Disable.Text = "비활성화 >>";
            this.btn_Disable.UseVisualStyleBackColor = true;
            // 
            // BSL_Option
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(594, 411);
            this.Controls.Add(this.btn_Disable);
            this.Controls.Add(this.btn_Enable);
            this.Controls.Add(this.lst_Disable);
            this.Controls.Add(this.lst_Enable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BSL_Option";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "**크래프트 설치옵션";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lst_Enable;
        private System.Windows.Forms.ListBox lst_Disable;
        private System.Windows.Forms.Button btn_Enable;
        private System.Windows.Forms.Button btn_Disable;
    }
}