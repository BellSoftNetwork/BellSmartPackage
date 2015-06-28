namespace Bell_Smart_Server.Source.BSU
{
    partial class BSU_RuntimeManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BSU_RuntimeManager));
            this.tc_Runtime = new System.Windows.Forms.TabControl();
            this.tp_64 = new System.Windows.Forms.TabPage();
            this.tp_86 = new System.Windows.Forms.TabPage();
            this.tc_Runtime.SuspendLayout();
            this.SuspendLayout();
            // 
            // tc_Runtime
            // 
            this.tc_Runtime.Controls.Add(this.tp_64);
            this.tc_Runtime.Controls.Add(this.tp_86);
            this.tc_Runtime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_Runtime.Location = new System.Drawing.Point(0, 0);
            this.tc_Runtime.Name = "tc_Runtime";
            this.tc_Runtime.SelectedIndex = 0;
            this.tc_Runtime.Size = new System.Drawing.Size(526, 302);
            this.tc_Runtime.TabIndex = 0;
            // 
            // tp_64
            // 
            this.tp_64.Location = new System.Drawing.Point(4, 22);
            this.tp_64.Name = "tp_64";
            this.tp_64.Padding = new System.Windows.Forms.Padding(3);
            this.tp_64.Size = new System.Drawing.Size(518, 276);
            this.tp_64.TabIndex = 0;
            this.tp_64.Text = "x64";
            this.tp_64.UseVisualStyleBackColor = true;
            // 
            // tp_86
            // 
            this.tp_86.Location = new System.Drawing.Point(4, 22);
            this.tp_86.Name = "tp_86";
            this.tp_86.Padding = new System.Windows.Forms.Padding(3);
            this.tp_86.Size = new System.Drawing.Size(383, 211);
            this.tp_86.TabIndex = 1;
            this.tp_86.Text = "x86";
            this.tp_86.UseVisualStyleBackColor = true;
            // 
            // BSU_RuntimeManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(526, 302);
            this.Controls.Add(this.tc_Runtime);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BSU_RuntimeManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "런타임 관리";
            this.tc_Runtime.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tc_Runtime;
        private System.Windows.Forms.TabPage tp_64;
        private System.Windows.Forms.TabPage tp_86;
    }
}