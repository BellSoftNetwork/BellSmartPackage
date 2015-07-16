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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BSP_Loader));
            this.ni_BSP = new System.Windows.Forms.NotifyIcon(this.components);
            this.ms_BSP = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mi_Restart = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_End = new System.Windows.Forms.ToolStripMenuItem();
            this.pb_Load = new System.Windows.Forms.ProgressBar();
            this.lb_Log = new System.Windows.Forms.Label();
            this.ms_BSP.SuspendLayout();
            this.SuspendLayout();
            // 
            // ni_BSP
            // 
            this.ni_BSP.ContextMenuStrip = this.ms_BSP;
            this.ni_BSP.Icon = ((System.Drawing.Icon)(resources.GetObject("ni_BSP.Icon")));
            this.ni_BSP.Text = "Bell Smart Package";
            this.ni_BSP.Visible = true;
            // 
            // ms_BSP
            // 
            this.ms_BSP.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_Restart,
            this.mi_End});
            this.ms_BSP.Name = "ms_BSP";
            this.ms_BSP.Size = new System.Drawing.Size(136, 48);
            // 
            // mi_Restart
            // 
            this.mi_Restart.Name = "mi_Restart";
            this.mi_Restart.Size = new System.Drawing.Size(135, 22);
            this.mi_Restart.Text = "BSP 재시작";
            this.mi_Restart.Click += new System.EventHandler(this.mi_Restart_Click);
            // 
            // mi_End
            // 
            this.mi_End.Name = "mi_End";
            this.mi_End.Size = new System.Drawing.Size(135, 22);
            this.mi_End.Text = "BSP 종료";
            this.mi_End.Click += new System.EventHandler(this.mi_End_Click);
            // 
            // pb_Load
            // 
            this.pb_Load.BackColor = System.Drawing.Color.Black;
            this.pb_Load.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pb_Load.Location = new System.Drawing.Point(0, 196);
            this.pb_Load.Name = "pb_Load";
            this.pb_Load.Size = new System.Drawing.Size(365, 10);
            this.pb_Load.Step = 1;
            this.pb_Load.TabIndex = 4;
            // 
            // lb_Log
            // 
            this.lb_Log.BackColor = System.Drawing.Color.Transparent;
            this.lb_Log.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lb_Log.ForeColor = System.Drawing.Color.White;
            this.lb_Log.Location = new System.Drawing.Point(0, 180);
            this.lb_Log.Name = "lb_Log";
            this.lb_Log.Size = new System.Drawing.Size(365, 16);
            this.lb_Log.TabIndex = 5;
            this.lb_Log.Text = "진행상황 : ";
            // 
            // BSP_Loader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Bell_Smart_Package.Properties.Resources.Logo_BSN1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(365, 206);
            this.Controls.Add(this.lb_Log);
            this.Controls.Add(this.pb_Load);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BSP_Loader";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BSP 로더";
            this.Shown += new System.EventHandler(this.BSP_Loader_Shown);
            this.ms_BSP.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon ni_BSP;
        private System.Windows.Forms.ContextMenuStrip ms_BSP;
        private System.Windows.Forms.ToolStripMenuItem mi_Restart;
        private System.Windows.Forms.ToolStripMenuItem mi_End;
        private System.Windows.Forms.ProgressBar pb_Load;
        private System.Windows.Forms.Label lb_Log;
    }
}