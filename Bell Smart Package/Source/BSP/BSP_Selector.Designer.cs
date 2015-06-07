namespace Bell_Smart_Package.Source.BSP
{
    partial class BSP_Selector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BSP_Selector));
            this.btn_BSS = new System.Windows.Forms.Button();
            this.btn_BST = new System.Windows.Forms.Button();
            this.btn_Updater = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_BSS
            // 
            this.btn_BSS.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_BSS.Location = new System.Drawing.Point(0, 49);
            this.btn_BSS.Name = "btn_BSS";
            this.btn_BSS.Size = new System.Drawing.Size(206, 23);
            this.btn_BSS.TabIndex = 0;
            this.btn_BSS.Text = "Bell Smart Server";
            this.btn_BSS.UseVisualStyleBackColor = true;
            this.btn_BSS.Click += new System.EventHandler(this.btn_BSS_Click);
            // 
            // btn_BST
            // 
            this.btn_BST.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_BST.Location = new System.Drawing.Point(0, 0);
            this.btn_BST.Name = "btn_BST";
            this.btn_BST.Size = new System.Drawing.Size(206, 23);
            this.btn_BST.TabIndex = 1;
            this.btn_BST.Text = "Bell Smart Tools";
            this.btn_BST.UseVisualStyleBackColor = true;
            this.btn_BST.Click += new System.EventHandler(this.btn_BST_Click);
            // 
            // btn_Updater
            // 
            this.btn_Updater.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Updater.Location = new System.Drawing.Point(0, 23);
            this.btn_Updater.Name = "btn_Updater";
            this.btn_Updater.Size = new System.Drawing.Size(206, 26);
            this.btn_Updater.TabIndex = 2;
            this.btn_Updater.Text = "Bell Smart Package Updater";
            this.btn_Updater.UseVisualStyleBackColor = true;
            this.btn_Updater.Click += new System.EventHandler(this.btn_Updater_Click);
            // 
            // BSP_Selector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(206, 72);
            this.ControlBox = false;
            this.Controls.Add(this.btn_Updater);
            this.Controls.Add(this.btn_BST);
            this.Controls.Add(this.btn_BSS);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BSP_Selector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BSP 선택";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_BSS;
        private System.Windows.Forms.Button btn_BST;
        private System.Windows.Forms.Button btn_Updater;
    }
}