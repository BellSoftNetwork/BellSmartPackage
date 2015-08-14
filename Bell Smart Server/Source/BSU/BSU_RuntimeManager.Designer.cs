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
            this.tp_JAVA = new System.Windows.Forms.TabPage();
            this.btn_JAVA_Upload = new System.Windows.Forms.Button();
            this.gb_Architecture = new System.Windows.Forms.GroupBox();
            this.rb_JAVA_86 = new System.Windows.Forms.RadioButton();
            this.rb_JAVA_64 = new System.Windows.Forms.RadioButton();
            this.btn_JAVA_Load = new System.Windows.Forms.Button();
            this.llb_JAVA_Upload = new System.Windows.Forms.LinkLabel();
            this.pb_JAVA_Upload = new System.Windows.Forms.ProgressBar();
            this.lst_JAVA_File = new System.Windows.Forms.ListBox();
            this.tc_Runtime.SuspendLayout();
            this.tp_JAVA.SuspendLayout();
            this.gb_Architecture.SuspendLayout();
            this.SuspendLayout();
            // 
            // tc_Runtime
            // 
            this.tc_Runtime.Controls.Add(this.tp_JAVA);
            this.tc_Runtime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_Runtime.Location = new System.Drawing.Point(0, 0);
            this.tc_Runtime.Name = "tc_Runtime";
            this.tc_Runtime.SelectedIndex = 0;
            this.tc_Runtime.Size = new System.Drawing.Size(681, 253);
            this.tc_Runtime.TabIndex = 0;
            // 
            // tp_JAVA
            // 
            this.tp_JAVA.Controls.Add(this.lst_JAVA_File);
            this.tp_JAVA.Controls.Add(this.pb_JAVA_Upload);
            this.tp_JAVA.Controls.Add(this.btn_JAVA_Upload);
            this.tp_JAVA.Controls.Add(this.gb_Architecture);
            this.tp_JAVA.Controls.Add(this.btn_JAVA_Load);
            this.tp_JAVA.Controls.Add(this.llb_JAVA_Upload);
            this.tp_JAVA.Location = new System.Drawing.Point(4, 22);
            this.tp_JAVA.Name = "tp_JAVA";
            this.tp_JAVA.Padding = new System.Windows.Forms.Padding(3);
            this.tp_JAVA.Size = new System.Drawing.Size(673, 227);
            this.tp_JAVA.TabIndex = 0;
            this.tp_JAVA.Text = "JAVA";
            this.tp_JAVA.UseVisualStyleBackColor = true;
            // 
            // btn_JAVA_Upload
            // 
            this.btn_JAVA_Upload.Location = new System.Drawing.Point(595, 51);
            this.btn_JAVA_Upload.Name = "btn_JAVA_Upload";
            this.btn_JAVA_Upload.Size = new System.Drawing.Size(75, 23);
            this.btn_JAVA_Upload.TabIndex = 31;
            this.btn_JAVA_Upload.Text = "업로드";
            this.btn_JAVA_Upload.UseVisualStyleBackColor = true;
            this.btn_JAVA_Upload.Click += new System.EventHandler(this.btn_JAVA_Upload_Click);
            // 
            // gb_Architecture
            // 
            this.gb_Architecture.Controls.Add(this.rb_JAVA_86);
            this.gb_Architecture.Controls.Add(this.rb_JAVA_64);
            this.gb_Architecture.Dock = System.Windows.Forms.DockStyle.Top;
            this.gb_Architecture.Location = new System.Drawing.Point(3, 3);
            this.gb_Architecture.Name = "gb_Architecture";
            this.gb_Architecture.Size = new System.Drawing.Size(667, 42);
            this.gb_Architecture.TabIndex = 30;
            this.gb_Architecture.TabStop = false;
            this.gb_Architecture.Text = "OS Architecture";
            // 
            // rb_JAVA_86
            // 
            this.rb_JAVA_86.AutoSize = true;
            this.rb_JAVA_86.Location = new System.Drawing.Point(54, 20);
            this.rb_JAVA_86.Name = "rb_JAVA_86";
            this.rb_JAVA_86.Size = new System.Drawing.Size(42, 16);
            this.rb_JAVA_86.TabIndex = 1;
            this.rb_JAVA_86.Text = "x86";
            this.rb_JAVA_86.UseVisualStyleBackColor = true;
            // 
            // rb_JAVA_64
            // 
            this.rb_JAVA_64.AutoSize = true;
            this.rb_JAVA_64.Checked = true;
            this.rb_JAVA_64.Location = new System.Drawing.Point(6, 20);
            this.rb_JAVA_64.Name = "rb_JAVA_64";
            this.rb_JAVA_64.Size = new System.Drawing.Size(42, 16);
            this.rb_JAVA_64.TabIndex = 0;
            this.rb_JAVA_64.TabStop = true;
            this.rb_JAVA_64.Text = "x64";
            this.rb_JAVA_64.UseVisualStyleBackColor = true;
            // 
            // btn_JAVA_Load
            // 
            this.btn_JAVA_Load.Location = new System.Drawing.Point(483, 51);
            this.btn_JAVA_Load.Name = "btn_JAVA_Load";
            this.btn_JAVA_Load.Size = new System.Drawing.Size(106, 23);
            this.btn_JAVA_Load.TabIndex = 29;
            this.btn_JAVA_Load.Text = "업로드 폴더 로드";
            this.btn_JAVA_Load.UseVisualStyleBackColor = true;
            this.btn_JAVA_Load.Click += new System.EventHandler(this.btn_JAVA_Load_Click);
            // 
            // llb_JAVA_Upload
            // 
            this.llb_JAVA_Upload.AutoSize = true;
            this.llb_JAVA_Upload.Location = new System.Drawing.Point(3, 48);
            this.llb_JAVA_Upload.Name = "llb_JAVA_Upload";
            this.llb_JAVA_Upload.Size = new System.Drawing.Size(81, 12);
            this.llb_JAVA_Upload.TabIndex = 28;
            this.llb_JAVA_Upload.TabStop = true;
            this.llb_JAVA_Upload.Text = "업로드 폴더 : ";
            this.llb_JAVA_Upload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llb_JAVA_Upload_LinkClicked);
            // 
            // pb_JAVA_Upload
            // 
            this.pb_JAVA_Upload.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pb_JAVA_Upload.Location = new System.Drawing.Point(3, 209);
            this.pb_JAVA_Upload.Name = "pb_JAVA_Upload";
            this.pb_JAVA_Upload.Size = new System.Drawing.Size(667, 15);
            this.pb_JAVA_Upload.Step = 1;
            this.pb_JAVA_Upload.TabIndex = 27;
            // 
            // lst_JAVA_File
            // 
            this.lst_JAVA_File.AllowDrop = true;
            this.lst_JAVA_File.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lst_JAVA_File.FormattingEnabled = true;
            this.lst_JAVA_File.ItemHeight = 12;
            this.lst_JAVA_File.Location = new System.Drawing.Point(3, 73);
            this.lst_JAVA_File.Name = "lst_JAVA_File";
            this.lst_JAVA_File.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lst_JAVA_File.Size = new System.Drawing.Size(667, 136);
            this.lst_JAVA_File.TabIndex = 32;
            // 
            // BSU_RuntimeManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(681, 253);
            this.Controls.Add(this.tc_Runtime);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BSU_RuntimeManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "런타임 관리";
            this.Load += new System.EventHandler(this.BSU_RuntimeManager_Load);
            this.tc_Runtime.ResumeLayout(false);
            this.tp_JAVA.ResumeLayout(false);
            this.tp_JAVA.PerformLayout();
            this.gb_Architecture.ResumeLayout(false);
            this.gb_Architecture.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tc_Runtime;
        private System.Windows.Forms.TabPage tp_JAVA;
        private System.Windows.Forms.Button btn_JAVA_Load;
        private System.Windows.Forms.LinkLabel llb_JAVA_Upload;
        private System.Windows.Forms.GroupBox gb_Architecture;
        private System.Windows.Forms.RadioButton rb_JAVA_86;
        private System.Windows.Forms.RadioButton rb_JAVA_64;
        private System.Windows.Forms.Button btn_JAVA_Upload;
        private System.Windows.Forms.ProgressBar pb_JAVA_Upload;
        private System.Windows.Forms.ListBox lst_JAVA_File;
    }
}