namespace Bell_Smart_Package.Source.BSP
{
    partial class BSP_Updater
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BSP_Updater));
            this.pb_Down = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.lb_Info = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pb_Down
            // 
            this.pb_Down.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pb_Down.Location = new System.Drawing.Point(0, 86);
            this.pb_Down.Name = "pb_Down";
            this.pb_Down.Size = new System.Drawing.Size(435, 51);
            this.pb_Down.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(360, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lb_Info
            // 
            this.lb_Info.AutoSize = true;
            this.lb_Info.Location = new System.Drawing.Point(12, 49);
            this.lb_Info.Name = "lb_Info";
            this.lb_Info.Size = new System.Drawing.Size(38, 12);
            this.lb_Info.TabIndex = 2;
            this.lb_Info.Text = "label1";
            // 
            // BSP_Updater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(435, 137);
            this.Controls.Add(this.lb_Info);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pb_Down);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BSP_Updater";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BSP 업데이터";
            this.Load += new System.EventHandler(this.BSP_Updater_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pb_Down;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lb_Info;
    }
}