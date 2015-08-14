namespace Bell_Smart_Tools.Source.BST
{
    partial class BSP_Installer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BSP_Installer));
            this.btn_Manager = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Manager
            // 
            this.btn_Manager.Location = new System.Drawing.Point(153, 44);
            this.btn_Manager.Name = "btn_Manager";
            this.btn_Manager.Size = new System.Drawing.Size(75, 23);
            this.btn_Manager.TabIndex = 0;
            this.btn_Manager.Text = "Manager";
            this.btn_Manager.UseVisualStyleBackColor = true;
            this.btn_Manager.Click += new System.EventHandler(this.btn_Manager_Click);
            // 
            // BSP_Installer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(668, 225);
            this.Controls.Add(this.btn_Manager);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BSP_Installer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BSP 추가 설치";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Manager;
    }
}