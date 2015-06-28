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
            this.btn_JAVA_Load = new System.Windows.Forms.Button();
            this.llb_JAVA_Upload = new System.Windows.Forms.LinkLabel();
            this.lst_JAVA_File = new System.Windows.Forms.ListBox();
            this.gb_Architecture = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
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
            this.tc_Runtime.Size = new System.Drawing.Size(681, 290);
            this.tc_Runtime.TabIndex = 0;
            // 
            // tp_JAVA
            // 
            this.tp_JAVA.Controls.Add(this.gb_Architecture);
            this.tp_JAVA.Controls.Add(this.btn_JAVA_Load);
            this.tp_JAVA.Controls.Add(this.llb_JAVA_Upload);
            this.tp_JAVA.Controls.Add(this.lst_JAVA_File);
            this.tp_JAVA.Location = new System.Drawing.Point(4, 22);
            this.tp_JAVA.Name = "tp_JAVA";
            this.tp_JAVA.Padding = new System.Windows.Forms.Padding(3);
            this.tp_JAVA.Size = new System.Drawing.Size(673, 264);
            this.tp_JAVA.TabIndex = 0;
            this.tp_JAVA.Text = "JAVA";
            this.tp_JAVA.UseVisualStyleBackColor = true;
            // 
            // btn_JAVA_Load
            // 
            this.btn_JAVA_Load.Location = new System.Drawing.Point(564, 122);
            this.btn_JAVA_Load.Name = "btn_JAVA_Load";
            this.btn_JAVA_Load.Size = new System.Drawing.Size(106, 23);
            this.btn_JAVA_Load.TabIndex = 29;
            this.btn_JAVA_Load.Text = "업로드 폴더 로드";
            this.btn_JAVA_Load.UseVisualStyleBackColor = true;
            // 
            // llb_JAVA_Upload
            // 
            this.llb_JAVA_Upload.AutoSize = true;
            this.llb_JAVA_Upload.Location = new System.Drawing.Point(104, 125);
            this.llb_JAVA_Upload.Name = "llb_JAVA_Upload";
            this.llb_JAVA_Upload.Size = new System.Drawing.Size(81, 12);
            this.llb_JAVA_Upload.TabIndex = 28;
            this.llb_JAVA_Upload.TabStop = true;
            this.llb_JAVA_Upload.Text = "업로드 폴더 : ";
            // 
            // lst_JAVA_File
            // 
            this.lst_JAVA_File.AllowDrop = true;
            this.lst_JAVA_File.FormattingEnabled = true;
            this.lst_JAVA_File.ItemHeight = 12;
            this.lst_JAVA_File.Location = new System.Drawing.Point(104, 146);
            this.lst_JAVA_File.Name = "lst_JAVA_File";
            this.lst_JAVA_File.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lst_JAVA_File.Size = new System.Drawing.Size(566, 112);
            this.lst_JAVA_File.TabIndex = 27;
            // 
            // gb_Architecture
            // 
            this.gb_Architecture.Controls.Add(this.radioButton2);
            this.gb_Architecture.Controls.Add(this.radioButton1);
            this.gb_Architecture.Dock = System.Windows.Forms.DockStyle.Top;
            this.gb_Architecture.Location = new System.Drawing.Point(3, 3);
            this.gb_Architecture.Name = "gb_Architecture";
            this.gb_Architecture.Size = new System.Drawing.Size(667, 42);
            this.gb_Architecture.TabIndex = 30;
            this.gb_Architecture.TabStop = false;
            this.gb_Architecture.Text = "OS Architecture";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 20);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(42, 16);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "x64";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(54, 20);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(42, 16);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "x86";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // BSU_RuntimeManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(681, 290);
            this.Controls.Add(this.tc_Runtime);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BSU_RuntimeManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "런타임 관리";
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
        private System.Windows.Forms.ListBox lst_JAVA_File;
        private System.Windows.Forms.GroupBox gb_Architecture;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}