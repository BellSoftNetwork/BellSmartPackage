namespace Bell_Smart_Package.Source.BSP
{
    partial class BSP_Loader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BSP_Loader));
            this.pb_Load = new System.Windows.Forms.ProgressBar();
            this.lb_Log = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pb_Load
            // 
            this.pb_Load.Dock = System.Windows.Forms.DockStyle.Top;
            this.pb_Load.Location = new System.Drawing.Point(0, 0);
            this.pb_Load.Name = "pb_Load";
            this.pb_Load.Size = new System.Drawing.Size(365, 34);
            this.pb_Load.TabIndex = 0;
            // 
            // lb_Log
            // 
            this.lb_Log.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lb_Log.Location = new System.Drawing.Point(0, 37);
            this.lb_Log.Name = "lb_Log";
            this.lb_Log.Size = new System.Drawing.Size(365, 25);
            this.lb_Log.TabIndex = 1;
            this.lb_Log.Text = "진행상황 : ";
            // 
            // BSP_Loader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(365, 62);
            this.Controls.Add(this.lb_Log);
            this.Controls.Add(this.pb_Load);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BSP_Loader";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BSP 로더";
            this.Shown += new System.EventHandler(this.BSP_Loader_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar pb_Load;
        private System.Windows.Forms.Label lb_Log;
    }
}