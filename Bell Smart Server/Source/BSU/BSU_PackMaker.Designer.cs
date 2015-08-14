namespace Bell_Smart_Tools.Source.BSU
{
    partial class BSU_PackMaker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BSU_PackMaker));
            this.tc_Maker = new System.Windows.Forms.TabControl();
            this.tp_ModPack = new System.Windows.Forms.TabPage();
            this.gb_ModPack = new System.Windows.Forms.GroupBox();
            this.txt_Mod_Down = new System.Windows.Forms.TextBox();
            this.lb_Mod_Down = new System.Windows.Forms.Label();
            this.txt_Mod_News = new System.Windows.Forms.TextBox();
            this.lb_Mod_News = new System.Windows.Forms.Label();
            this.cb_Mod_Option = new System.Windows.Forms.ComboBox();
            this.cb_Mod_Base = new System.Windows.Forms.ComboBox();
            this.lb_Mod_Option = new System.Windows.Forms.Label();
            this.lb_Mod_Base = new System.Windows.Forms.Label();
            this.txt_Mod_Name = new System.Windows.Forms.TextBox();
            this.lb_Mod_Name = new System.Windows.Forms.Label();
            this.lb_MUID = new System.Windows.Forms.Label();
            this.txt_MUID = new System.Windows.Forms.TextBox();
            this.btn_Mod_Upload = new System.Windows.Forms.Button();
            this.tp_BasePack = new System.Windows.Forms.TabPage();
            this.gb_BasePack = new System.Windows.Forms.GroupBox();
            this.btn_Base_Upload = new System.Windows.Forms.Button();
            this.txt_Base_Down = new System.Windows.Forms.TextBox();
            this.lb_Base_Down = new System.Windows.Forms.Label();
            this.lb_BUID = new System.Windows.Forms.Label();
            this.txt_BUID = new System.Windows.Forms.TextBox();
            this.tp_OptionPack = new System.Windows.Forms.TabPage();
            this.gb_OptionPack = new System.Windows.Forms.GroupBox();
            this.btn_Option_Upload = new System.Windows.Forms.Button();
            this.txt_Option_Down = new System.Windows.Forms.TextBox();
            this.lb_Option_Down = new System.Windows.Forms.Label();
            this.lb_OUID = new System.Windows.Forms.Label();
            this.txt_OUID = new System.Windows.Forms.TextBox();
            this.tc_Maker.SuspendLayout();
            this.tp_ModPack.SuspendLayout();
            this.gb_ModPack.SuspendLayout();
            this.tp_BasePack.SuspendLayout();
            this.gb_BasePack.SuspendLayout();
            this.tp_OptionPack.SuspendLayout();
            this.gb_OptionPack.SuspendLayout();
            this.SuspendLayout();
            // 
            // tc_Maker
            // 
            this.tc_Maker.Controls.Add(this.tp_ModPack);
            this.tc_Maker.Controls.Add(this.tp_BasePack);
            this.tc_Maker.Controls.Add(this.tp_OptionPack);
            this.tc_Maker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_Maker.Location = new System.Drawing.Point(0, 0);
            this.tc_Maker.Name = "tc_Maker";
            this.tc_Maker.SelectedIndex = 0;
            this.tc_Maker.Size = new System.Drawing.Size(402, 215);
            this.tc_Maker.TabIndex = 0;
            // 
            // tp_ModPack
            // 
            this.tp_ModPack.Controls.Add(this.gb_ModPack);
            this.tp_ModPack.Location = new System.Drawing.Point(4, 22);
            this.tp_ModPack.Name = "tp_ModPack";
            this.tp_ModPack.Padding = new System.Windows.Forms.Padding(3);
            this.tp_ModPack.Size = new System.Drawing.Size(394, 189);
            this.tp_ModPack.TabIndex = 0;
            this.tp_ModPack.Text = "모드팩";
            this.tp_ModPack.UseVisualStyleBackColor = true;
            // 
            // gb_ModPack
            // 
            this.gb_ModPack.Controls.Add(this.txt_Mod_Down);
            this.gb_ModPack.Controls.Add(this.lb_Mod_Down);
            this.gb_ModPack.Controls.Add(this.txt_Mod_News);
            this.gb_ModPack.Controls.Add(this.lb_Mod_News);
            this.gb_ModPack.Controls.Add(this.cb_Mod_Option);
            this.gb_ModPack.Controls.Add(this.cb_Mod_Base);
            this.gb_ModPack.Controls.Add(this.lb_Mod_Option);
            this.gb_ModPack.Controls.Add(this.lb_Mod_Base);
            this.gb_ModPack.Controls.Add(this.txt_Mod_Name);
            this.gb_ModPack.Controls.Add(this.lb_Mod_Name);
            this.gb_ModPack.Controls.Add(this.lb_MUID);
            this.gb_ModPack.Controls.Add(this.txt_MUID);
            this.gb_ModPack.Controls.Add(this.btn_Mod_Upload);
            this.gb_ModPack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_ModPack.Location = new System.Drawing.Point(3, 3);
            this.gb_ModPack.Name = "gb_ModPack";
            this.gb_ModPack.Size = new System.Drawing.Size(388, 183);
            this.gb_ModPack.TabIndex = 2;
            this.gb_ModPack.TabStop = false;
            // 
            // txt_Mod_Down
            // 
            this.txt_Mod_Down.Location = new System.Drawing.Point(97, 132);
            this.txt_Mod_Down.Name = "txt_Mod_Down";
            this.txt_Mod_Down.ReadOnly = true;
            this.txt_Mod_Down.Size = new System.Drawing.Size(291, 21);
            this.txt_Mod_Down.TabIndex = 41;
            // 
            // lb_Mod_Down
            // 
            this.lb_Mod_Down.AutoSize = true;
            this.lb_Mod_Down.Location = new System.Drawing.Point(2, 135);
            this.lb_Mod_Down.Name = "lb_Mod_Down";
            this.lb_Mod_Down.Size = new System.Drawing.Size(89, 12);
            this.lb_Mod_Down.TabIndex = 40;
            this.lb_Mod_Down.Text = "파일서버 주소 :";
            // 
            // txt_Mod_News
            // 
            this.txt_Mod_News.Location = new System.Drawing.Point(97, 111);
            this.txt_Mod_News.Name = "txt_Mod_News";
            this.txt_Mod_News.ReadOnly = true;
            this.txt_Mod_News.Size = new System.Drawing.Size(291, 21);
            this.txt_Mod_News.TabIndex = 39;
            // 
            // lb_Mod_News
            // 
            this.lb_Mod_News.AutoSize = true;
            this.lb_Mod_News.Location = new System.Drawing.Point(17, 114);
            this.lb_Mod_News.Name = "lb_Mod_News";
            this.lb_Mod_News.Size = new System.Drawing.Size(74, 12);
            this.lb_Mod_News.TabIndex = 38;
            this.lb_Mod_News.Text = "News 주소 :";
            // 
            // cb_Mod_Option
            // 
            this.cb_Mod_Option.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Mod_Option.FormattingEnabled = true;
            this.cb_Mod_Option.Location = new System.Drawing.Point(97, 90);
            this.cb_Mod_Option.Name = "cb_Mod_Option";
            this.cb_Mod_Option.Size = new System.Drawing.Size(291, 20);
            this.cb_Mod_Option.TabIndex = 37;
            // 
            // cb_Mod_Base
            // 
            this.cb_Mod_Base.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Mod_Base.FormattingEnabled = true;
            this.cb_Mod_Base.Location = new System.Drawing.Point(97, 69);
            this.cb_Mod_Base.Name = "cb_Mod_Base";
            this.cb_Mod_Base.Size = new System.Drawing.Size(291, 20);
            this.cb_Mod_Base.TabIndex = 36;
            // 
            // lb_Mod_Option
            // 
            this.lb_Mod_Option.AutoSize = true;
            this.lb_Mod_Option.Location = new System.Drawing.Point(42, 93);
            this.lb_Mod_Option.Name = "lb_Mod_Option";
            this.lb_Mod_Option.Size = new System.Drawing.Size(49, 12);
            this.lb_Mod_Option.TabIndex = 35;
            this.lb_Mod_Option.Text = "옵션팩 :";
            // 
            // lb_Mod_Base
            // 
            this.lb_Mod_Base.AutoSize = true;
            this.lb_Mod_Base.Location = new System.Drawing.Point(30, 72);
            this.lb_Mod_Base.Name = "lb_Mod_Base";
            this.lb_Mod_Base.Size = new System.Drawing.Size(61, 12);
            this.lb_Mod_Base.TabIndex = 34;
            this.lb_Mod_Base.Text = "베이스팩 :";
            // 
            // txt_Mod_Name
            // 
            this.txt_Mod_Name.Location = new System.Drawing.Point(97, 47);
            this.txt_Mod_Name.Name = "txt_Mod_Name";
            this.txt_Mod_Name.Size = new System.Drawing.Size(291, 21);
            this.txt_Mod_Name.TabIndex = 32;
            // 
            // lb_Mod_Name
            // 
            this.lb_Mod_Name.AutoSize = true;
            this.lb_Mod_Name.Location = new System.Drawing.Point(14, 50);
            this.lb_Mod_Name.Name = "lb_Mod_Name";
            this.lb_Mod_Name.Size = new System.Drawing.Size(77, 12);
            this.lb_Mod_Name.TabIndex = 30;
            this.lb_Mod_Name.Text = "모드팩 이름 :";
            // 
            // lb_MUID
            // 
            this.lb_MUID.AutoSize = true;
            this.lb_MUID.Location = new System.Drawing.Point(48, 23);
            this.lb_MUID.Name = "lb_MUID";
            this.lb_MUID.Size = new System.Drawing.Size(43, 12);
            this.lb_MUID.TabIndex = 33;
            this.lb_MUID.Text = "MUID :";
            // 
            // txt_MUID
            // 
            this.txt_MUID.Location = new System.Drawing.Point(97, 20);
            this.txt_MUID.Name = "txt_MUID";
            this.txt_MUID.Size = new System.Drawing.Size(291, 21);
            this.txt_MUID.TabIndex = 31;
            this.txt_MUID.TextChanged += new System.EventHandler(this.txt_MUID_TextChanged);
            this.txt_MUID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_MUID_KeyPress);
            // 
            // btn_Mod_Upload
            // 
            this.btn_Mod_Upload.Location = new System.Drawing.Point(290, 159);
            this.btn_Mod_Upload.Name = "btn_Mod_Upload";
            this.btn_Mod_Upload.Size = new System.Drawing.Size(98, 23);
            this.btn_Mod_Upload.TabIndex = 1;
            this.btn_Mod_Upload.Text = "모드팩 등록!";
            this.btn_Mod_Upload.UseVisualStyleBackColor = true;
            this.btn_Mod_Upload.Click += new System.EventHandler(this.btn_Mod_Upload_Click);
            // 
            // tp_BasePack
            // 
            this.tp_BasePack.Controls.Add(this.gb_BasePack);
            this.tp_BasePack.Location = new System.Drawing.Point(4, 22);
            this.tp_BasePack.Name = "tp_BasePack";
            this.tp_BasePack.Padding = new System.Windows.Forms.Padding(3);
            this.tp_BasePack.Size = new System.Drawing.Size(394, 189);
            this.tp_BasePack.TabIndex = 1;
            this.tp_BasePack.Text = "베이스팩";
            this.tp_BasePack.UseVisualStyleBackColor = true;
            // 
            // gb_BasePack
            // 
            this.gb_BasePack.Controls.Add(this.btn_Base_Upload);
            this.gb_BasePack.Controls.Add(this.txt_Base_Down);
            this.gb_BasePack.Controls.Add(this.lb_Base_Down);
            this.gb_BasePack.Controls.Add(this.lb_BUID);
            this.gb_BasePack.Controls.Add(this.txt_BUID);
            this.gb_BasePack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_BasePack.Location = new System.Drawing.Point(3, 3);
            this.gb_BasePack.Name = "gb_BasePack";
            this.gb_BasePack.Size = new System.Drawing.Size(388, 183);
            this.gb_BasePack.TabIndex = 0;
            this.gb_BasePack.TabStop = false;
            // 
            // btn_Base_Upload
            // 
            this.btn_Base_Upload.Location = new System.Drawing.Point(290, 74);
            this.btn_Base_Upload.Name = "btn_Base_Upload";
            this.btn_Base_Upload.Size = new System.Drawing.Size(98, 23);
            this.btn_Base_Upload.TabIndex = 27;
            this.btn_Base_Upload.Text = "베이스팩 등록!";
            this.btn_Base_Upload.UseVisualStyleBackColor = true;
            this.btn_Base_Upload.Click += new System.EventHandler(this.btn_Base_Upload_Click);
            // 
            // txt_Base_Down
            // 
            this.txt_Base_Down.Location = new System.Drawing.Point(97, 47);
            this.txt_Base_Down.Name = "txt_Base_Down";
            this.txt_Base_Down.ReadOnly = true;
            this.txt_Base_Down.Size = new System.Drawing.Size(291, 21);
            this.txt_Base_Down.TabIndex = 26;
            // 
            // lb_Base_Down
            // 
            this.lb_Base_Down.AutoSize = true;
            this.lb_Base_Down.Location = new System.Drawing.Point(2, 50);
            this.lb_Base_Down.Name = "lb_Base_Down";
            this.lb_Base_Down.Size = new System.Drawing.Size(89, 12);
            this.lb_Base_Down.TabIndex = 25;
            this.lb_Base_Down.Text = "파일서버 주소 :";
            // 
            // lb_BUID
            // 
            this.lb_BUID.AutoSize = true;
            this.lb_BUID.Location = new System.Drawing.Point(48, 23);
            this.lb_BUID.Name = "lb_BUID";
            this.lb_BUID.Size = new System.Drawing.Size(40, 12);
            this.lb_BUID.TabIndex = 24;
            this.lb_BUID.Text = "BUID :";
            // 
            // txt_BUID
            // 
            this.txt_BUID.Location = new System.Drawing.Point(97, 20);
            this.txt_BUID.Name = "txt_BUID";
            this.txt_BUID.Size = new System.Drawing.Size(291, 21);
            this.txt_BUID.TabIndex = 23;
            this.txt_BUID.TextChanged += new System.EventHandler(this.txt_BUID_TextChanged);
            // 
            // tp_OptionPack
            // 
            this.tp_OptionPack.Controls.Add(this.gb_OptionPack);
            this.tp_OptionPack.Location = new System.Drawing.Point(4, 22);
            this.tp_OptionPack.Name = "tp_OptionPack";
            this.tp_OptionPack.Size = new System.Drawing.Size(394, 189);
            this.tp_OptionPack.TabIndex = 2;
            this.tp_OptionPack.Text = "옵션팩";
            this.tp_OptionPack.UseVisualStyleBackColor = true;
            // 
            // gb_OptionPack
            // 
            this.gb_OptionPack.Controls.Add(this.btn_Option_Upload);
            this.gb_OptionPack.Controls.Add(this.txt_Option_Down);
            this.gb_OptionPack.Controls.Add(this.lb_Option_Down);
            this.gb_OptionPack.Controls.Add(this.lb_OUID);
            this.gb_OptionPack.Controls.Add(this.txt_OUID);
            this.gb_OptionPack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_OptionPack.Location = new System.Drawing.Point(0, 0);
            this.gb_OptionPack.Name = "gb_OptionPack";
            this.gb_OptionPack.Size = new System.Drawing.Size(394, 189);
            this.gb_OptionPack.TabIndex = 0;
            this.gb_OptionPack.TabStop = false;
            // 
            // btn_Option_Upload
            // 
            this.btn_Option_Upload.Location = new System.Drawing.Point(299, 74);
            this.btn_Option_Upload.Name = "btn_Option_Upload";
            this.btn_Option_Upload.Size = new System.Drawing.Size(92, 23);
            this.btn_Option_Upload.TabIndex = 27;
            this.btn_Option_Upload.Text = "옵션팩 등록!";
            this.btn_Option_Upload.UseVisualStyleBackColor = true;
            this.btn_Option_Upload.Click += new System.EventHandler(this.btn_Option_Upload_Click);
            // 
            // txt_Option_Down
            // 
            this.txt_Option_Down.Location = new System.Drawing.Point(100, 47);
            this.txt_Option_Down.Name = "txt_Option_Down";
            this.txt_Option_Down.ReadOnly = true;
            this.txt_Option_Down.Size = new System.Drawing.Size(291, 21);
            this.txt_Option_Down.TabIndex = 26;
            // 
            // lb_Option_Down
            // 
            this.lb_Option_Down.AutoSize = true;
            this.lb_Option_Down.Location = new System.Drawing.Point(5, 50);
            this.lb_Option_Down.Name = "lb_Option_Down";
            this.lb_Option_Down.Size = new System.Drawing.Size(89, 12);
            this.lb_Option_Down.TabIndex = 25;
            this.lb_Option_Down.Text = "파일서버 주소 :";
            // 
            // lb_OUID
            // 
            this.lb_OUID.AutoSize = true;
            this.lb_OUID.Location = new System.Drawing.Point(51, 23);
            this.lb_OUID.Name = "lb_OUID";
            this.lb_OUID.Size = new System.Drawing.Size(33, 12);
            this.lb_OUID.TabIndex = 24;
            this.lb_OUID.Text = "OUID";
            // 
            // txt_OUID
            // 
            this.txt_OUID.Location = new System.Drawing.Point(100, 20);
            this.txt_OUID.Name = "txt_OUID";
            this.txt_OUID.Size = new System.Drawing.Size(291, 21);
            this.txt_OUID.TabIndex = 23;
            this.txt_OUID.TextChanged += new System.EventHandler(this.txt_OUID_TextChanged);
            // 
            // BSU_PackMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(402, 215);
            this.Controls.Add(this.tc_Maker);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BSU_PackMaker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "팩 메이커";
            this.tc_Maker.ResumeLayout(false);
            this.tp_ModPack.ResumeLayout(false);
            this.gb_ModPack.ResumeLayout(false);
            this.gb_ModPack.PerformLayout();
            this.tp_BasePack.ResumeLayout(false);
            this.gb_BasePack.ResumeLayout(false);
            this.gb_BasePack.PerformLayout();
            this.tp_OptionPack.ResumeLayout(false);
            this.gb_OptionPack.ResumeLayout(false);
            this.gb_OptionPack.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tc_Maker;
        private System.Windows.Forms.TabPage tp_ModPack;
        private System.Windows.Forms.TabPage tp_BasePack;
        private System.Windows.Forms.TabPage tp_OptionPack;
        private System.Windows.Forms.Button btn_Mod_Upload;
        private System.Windows.Forms.GroupBox gb_ModPack;
        private System.Windows.Forms.TextBox txt_Mod_Down;
        private System.Windows.Forms.Label lb_Mod_Down;
        private System.Windows.Forms.TextBox txt_Mod_News;
        private System.Windows.Forms.Label lb_Mod_News;
        private System.Windows.Forms.ComboBox cb_Mod_Option;
        private System.Windows.Forms.ComboBox cb_Mod_Base;
        private System.Windows.Forms.Label lb_Mod_Option;
        private System.Windows.Forms.Label lb_Mod_Base;
        private System.Windows.Forms.TextBox txt_Mod_Name;
        private System.Windows.Forms.Label lb_Mod_Name;
        private System.Windows.Forms.Label lb_MUID;
        private System.Windows.Forms.TextBox txt_MUID;
        private System.Windows.Forms.GroupBox gb_BasePack;
        private System.Windows.Forms.Button btn_Base_Upload;
        private System.Windows.Forms.TextBox txt_Base_Down;
        private System.Windows.Forms.Label lb_Base_Down;
        private System.Windows.Forms.Label lb_BUID;
        private System.Windows.Forms.TextBox txt_BUID;
        private System.Windows.Forms.GroupBox gb_OptionPack;
        private System.Windows.Forms.Button btn_Option_Upload;
        private System.Windows.Forms.TextBox txt_Option_Down;
        private System.Windows.Forms.Label lb_Option_Down;
        private System.Windows.Forms.Label lb_OUID;
        private System.Windows.Forms.TextBox txt_OUID;
    }
}