namespace Bell_Smart_Tools.Source.BST
{
    partial class BST_Runtime
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BST_Runtime));
            this.pb_Down = new System.Windows.Forms.ProgressBar();
            this.lb_Name = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pb_Down
            // 
            this.pb_Down.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pb_Down.Location = new System.Drawing.Point(0, 143);
            this.pb_Down.Name = "pb_Down";
            this.pb_Down.Size = new System.Drawing.Size(352, 55);
            this.pb_Down.Step = 1;
            this.pb_Down.TabIndex = 0;
            // 
            // lb_Name
            // 
            this.lb_Name.BackColor = System.Drawing.Color.Transparent;
            this.lb_Name.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_Name.ForeColor = System.Drawing.Color.White;
            this.lb_Name.Location = new System.Drawing.Point(0, 0);
            this.lb_Name.Name = "lb_Name";
            this.lb_Name.Size = new System.Drawing.Size(352, 64);
            this.lb_Name.TabIndex = 1;
            this.lb_Name.Text = "Loading...";
            this.lb_Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BST_Runtime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Bell_Smart_Tools.Properties.Resources.Logo_BSN3;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(352, 198);
            this.Controls.Add(this.lb_Name);
            this.Controls.Add(this.pb_Down);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BST_Runtime";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "런타임 설치";
            this.Shown += new System.EventHandler(this.BST_Runtime_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar pb_Down;
        private System.Windows.Forms.Label lb_Name;
    }
}