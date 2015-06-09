﻿namespace Bell_Smart_Server.Source.BSU
{
    partial class BSU_ModManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BSU_ModManager));
            this.tc_Pack = new System.Windows.Forms.TabControl();
            this.tp_ModPack = new System.Windows.Forms.TabPage();
            this.gb_Mod_Upload = new System.Windows.Forms.GroupBox();
            this.lst_Mod_File = new System.Windows.Forms.ListBox();
            this.cms_File = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mi_Exclusion = new System.Windows.Forms.ToolStripMenuItem();
            this.lb_Mod_Version = new System.Windows.Forms.Label();
            this.txt_Version = new System.Windows.Forms.TextBox();
            this.gb_Mod_Info = new System.Windows.Forms.GroupBox();
            this.btn_Mod_Set = new System.Windows.Forms.Button();
            this.lb_MUID = new System.Windows.Forms.Label();
            this.txt_MUID = new System.Windows.Forms.TextBox();
            this.gb_Mod_Setting = new System.Windows.Forms.GroupBox();
            this.btn_Mod_Save = new System.Windows.Forms.Button();
            this.lst_Mod_Version = new System.Windows.Forms.ListBox();
            this.cms_Version = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mi_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.txt_Mod_Down = new System.Windows.Forms.TextBox();
            this.lb_Mod_Down = new System.Windows.Forms.Label();
            this.txt_Mod_News = new System.Windows.Forms.TextBox();
            this.lb_Mod_News = new System.Windows.Forms.Label();
            this.cb_Mod_Option = new System.Windows.Forms.ComboBox();
            this.cb_Mod_Base = new System.Windows.Forms.ComboBox();
            this.lb_Mod_Option = new System.Windows.Forms.Label();
            this.lb_Mod_Base = new System.Windows.Forms.Label();
            this.lb_Mod_Recommended = new System.Windows.Forms.Label();
            this.txt_Mod_Recommended = new System.Windows.Forms.TextBox();
            this.txt_Mod_Latest = new System.Windows.Forms.TextBox();
            this.lb_Mod_Latest = new System.Windows.Forms.Label();
            this.txt_Mod_Name = new System.Windows.Forms.TextBox();
            this.lb_Mod_Name = new System.Windows.Forms.Label();
            this.tp_BasePack = new System.Windows.Forms.TabPage();
            this.gb_Base_Upload = new System.Windows.Forms.GroupBox();
            this.gb_Base_Info = new System.Windows.Forms.GroupBox();
            this.btn_Base_Set = new System.Windows.Forms.Button();
            this.lb_BUID = new System.Windows.Forms.Label();
            this.txt_BUID = new System.Windows.Forms.TextBox();
            this.gb_Base_Setting = new System.Windows.Forms.GroupBox();
            this.btn_Base_Save = new System.Windows.Forms.Button();
            this.lst_Base_Version = new System.Windows.Forms.ListBox();
            this.txt_Base_Down = new System.Windows.Forms.TextBox();
            this.lb_Base_Down = new System.Windows.Forms.Label();
            this.lb_Base_Recommended = new System.Windows.Forms.Label();
            this.txt_Base_Recommended = new System.Windows.Forms.TextBox();
            this.txt_Base_Latest = new System.Windows.Forms.TextBox();
            this.lb_Base_Latest = new System.Windows.Forms.Label();
            this.txt_Base_Name = new System.Windows.Forms.TextBox();
            this.lb_Base_Name = new System.Windows.Forms.Label();
            this.tp_OptionPack = new System.Windows.Forms.TabPage();
            this.gb_Option_Upload = new System.Windows.Forms.GroupBox();
            this.gb_Option_Info = new System.Windows.Forms.GroupBox();
            this.btn_Option_Set = new System.Windows.Forms.Button();
            this.lb_OUID = new System.Windows.Forms.Label();
            this.txt_OUID = new System.Windows.Forms.TextBox();
            this.gb_Option_Setting = new System.Windows.Forms.GroupBox();
            this.btn_Option_Save = new System.Windows.Forms.Button();
            this.lst_Option_Version = new System.Windows.Forms.ListBox();
            this.txt_Option_Down = new System.Windows.Forms.TextBox();
            this.lb_Option_Down = new System.Windows.Forms.Label();
            this.lb_Option_Recommended = new System.Windows.Forms.Label();
            this.txt_Option_Recommended = new System.Windows.Forms.TextBox();
            this.txt_Option_Latest = new System.Windows.Forms.TextBox();
            this.lb_Option_Latest = new System.Windows.Forms.Label();
            this.txt_Option_Name = new System.Windows.Forms.TextBox();
            this.lb_Option_Name = new System.Windows.Forms.Label();
            this.tc_Pack.SuspendLayout();
            this.tp_ModPack.SuspendLayout();
            this.gb_Mod_Upload.SuspendLayout();
            this.cms_File.SuspendLayout();
            this.gb_Mod_Info.SuspendLayout();
            this.gb_Mod_Setting.SuspendLayout();
            this.cms_Version.SuspendLayout();
            this.tp_BasePack.SuspendLayout();
            this.gb_Base_Info.SuspendLayout();
            this.gb_Base_Setting.SuspendLayout();
            this.tp_OptionPack.SuspendLayout();
            this.gb_Option_Info.SuspendLayout();
            this.gb_Option_Setting.SuspendLayout();
            this.SuspendLayout();
            // 
            // tc_Pack
            // 
            this.tc_Pack.Controls.Add(this.tp_ModPack);
            this.tc_Pack.Controls.Add(this.tp_BasePack);
            this.tc_Pack.Controls.Add(this.tp_OptionPack);
            this.tc_Pack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_Pack.Location = new System.Drawing.Point(0, 0);
            this.tc_Pack.Name = "tc_Pack";
            this.tc_Pack.SelectedIndex = 0;
            this.tc_Pack.Size = new System.Drawing.Size(826, 435);
            this.tc_Pack.TabIndex = 3;
            // 
            // tp_ModPack
            // 
            this.tp_ModPack.Controls.Add(this.gb_Mod_Upload);
            this.tp_ModPack.Controls.Add(this.gb_Mod_Info);
            this.tp_ModPack.Controls.Add(this.gb_Mod_Setting);
            this.tp_ModPack.Location = new System.Drawing.Point(4, 22);
            this.tp_ModPack.Name = "tp_ModPack";
            this.tp_ModPack.Padding = new System.Windows.Forms.Padding(3);
            this.tp_ModPack.Size = new System.Drawing.Size(818, 409);
            this.tp_ModPack.TabIndex = 0;
            this.tp_ModPack.Text = "모드팩";
            this.tp_ModPack.UseVisualStyleBackColor = true;
            // 
            // gb_Mod_Upload
            // 
            this.gb_Mod_Upload.Controls.Add(this.lst_Mod_File);
            this.gb_Mod_Upload.Controls.Add(this.lb_Mod_Version);
            this.gb_Mod_Upload.Controls.Add(this.txt_Version);
            this.gb_Mod_Upload.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gb_Mod_Upload.Enabled = false;
            this.gb_Mod_Upload.Location = new System.Drawing.Point(3, 230);
            this.gb_Mod_Upload.Name = "gb_Mod_Upload";
            this.gb_Mod_Upload.Size = new System.Drawing.Size(812, 176);
            this.gb_Mod_Upload.TabIndex = 2;
            this.gb_Mod_Upload.TabStop = false;
            this.gb_Mod_Upload.Text = "업로드";
            // 
            // lst_Mod_File
            // 
            this.lst_Mod_File.AllowDrop = true;
            this.lst_Mod_File.ContextMenuStrip = this.cms_File;
            this.lst_Mod_File.FormattingEnabled = true;
            this.lst_Mod_File.ItemHeight = 12;
            this.lst_Mod_File.Items.AddRange(new object[] {
            "슈밤",
            "테스트"});
            this.lst_Mod_File.Location = new System.Drawing.Point(240, 14);
            this.lst_Mod_File.Name = "lst_Mod_File";
            this.lst_Mod_File.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lst_Mod_File.Size = new System.Drawing.Size(566, 160);
            this.lst_Mod_File.TabIndex = 2;
            this.lst_Mod_File.DragDrop += new System.Windows.Forms.DragEventHandler(this.lst_Mod_File_DragDrop);
            this.lst_Mod_File.DragOver += new System.Windows.Forms.DragEventHandler(this.lst_Mod_File_DragOver);
            // 
            // cms_File
            // 
            this.cms_File.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_Exclusion});
            this.cms_File.Name = "cms_File";
            this.cms_File.Size = new System.Drawing.Size(153, 48);
            // 
            // mi_Exclusion
            // 
            this.mi_Exclusion.Name = "mi_Exclusion";
            this.mi_Exclusion.Size = new System.Drawing.Size(152, 22);
            this.mi_Exclusion.Text = "제외";
            this.mi_Exclusion.Click += new System.EventHandler(this.mi_Exclusion_Click);
            // 
            // lb_Mod_Version
            // 
            this.lb_Mod_Version.AutoSize = true;
            this.lb_Mod_Version.Location = new System.Drawing.Point(6, 17);
            this.lb_Mod_Version.Name = "lb_Mod_Version";
            this.lb_Mod_Version.Size = new System.Drawing.Size(41, 12);
            this.lb_Mod_Version.TabIndex = 1;
            this.lb_Mod_Version.Text = "버전 : ";
            // 
            // txt_Version
            // 
            this.txt_Version.Location = new System.Drawing.Point(47, 14);
            this.txt_Version.Name = "txt_Version";
            this.txt_Version.Size = new System.Drawing.Size(187, 21);
            this.txt_Version.TabIndex = 0;
            // 
            // gb_Mod_Info
            // 
            this.gb_Mod_Info.Controls.Add(this.btn_Mod_Set);
            this.gb_Mod_Info.Controls.Add(this.lb_MUID);
            this.gb_Mod_Info.Controls.Add(this.txt_MUID);
            this.gb_Mod_Info.Dock = System.Windows.Forms.DockStyle.Top;
            this.gb_Mod_Info.Location = new System.Drawing.Point(3, 3);
            this.gb_Mod_Info.Name = "gb_Mod_Info";
            this.gb_Mod_Info.Size = new System.Drawing.Size(812, 43);
            this.gb_Mod_Info.TabIndex = 1;
            this.gb_Mod_Info.TabStop = false;
            this.gb_Mod_Info.Text = "정보";
            // 
            // btn_Mod_Set
            // 
            this.btn_Mod_Set.Location = new System.Drawing.Point(743, 12);
            this.btn_Mod_Set.Name = "btn_Mod_Set";
            this.btn_Mod_Set.Size = new System.Drawing.Size(63, 23);
            this.btn_Mod_Set.TabIndex = 2;
            this.btn_Mod_Set.Text = "불러오기";
            this.btn_Mod_Set.UseVisualStyleBackColor = true;
            this.btn_Mod_Set.Click += new System.EventHandler(this.btn_Set_Click);
            // 
            // lb_MUID
            // 
            this.lb_MUID.AutoSize = true;
            this.lb_MUID.Location = new System.Drawing.Point(0, 17);
            this.lb_MUID.Name = "lb_MUID";
            this.lb_MUID.Size = new System.Drawing.Size(43, 12);
            this.lb_MUID.TabIndex = 1;
            this.lb_MUID.Text = "MUID :";
            // 
            // txt_MUID
            // 
            this.txt_MUID.Location = new System.Drawing.Point(49, 14);
            this.txt_MUID.Name = "txt_MUID";
            this.txt_MUID.Size = new System.Drawing.Size(688, 21);
            this.txt_MUID.TabIndex = 0;
            // 
            // gb_Mod_Setting
            // 
            this.gb_Mod_Setting.Controls.Add(this.btn_Mod_Save);
            this.gb_Mod_Setting.Controls.Add(this.lst_Mod_Version);
            this.gb_Mod_Setting.Controls.Add(this.txt_Mod_Down);
            this.gb_Mod_Setting.Controls.Add(this.lb_Mod_Down);
            this.gb_Mod_Setting.Controls.Add(this.txt_Mod_News);
            this.gb_Mod_Setting.Controls.Add(this.lb_Mod_News);
            this.gb_Mod_Setting.Controls.Add(this.cb_Mod_Option);
            this.gb_Mod_Setting.Controls.Add(this.cb_Mod_Base);
            this.gb_Mod_Setting.Controls.Add(this.lb_Mod_Option);
            this.gb_Mod_Setting.Controls.Add(this.lb_Mod_Base);
            this.gb_Mod_Setting.Controls.Add(this.lb_Mod_Recommended);
            this.gb_Mod_Setting.Controls.Add(this.txt_Mod_Recommended);
            this.gb_Mod_Setting.Controls.Add(this.txt_Mod_Latest);
            this.gb_Mod_Setting.Controls.Add(this.lb_Mod_Latest);
            this.gb_Mod_Setting.Controls.Add(this.txt_Mod_Name);
            this.gb_Mod_Setting.Controls.Add(this.lb_Mod_Name);
            this.gb_Mod_Setting.Enabled = false;
            this.gb_Mod_Setting.Location = new System.Drawing.Point(3, 52);
            this.gb_Mod_Setting.Name = "gb_Mod_Setting";
            this.gb_Mod_Setting.Size = new System.Drawing.Size(812, 172);
            this.gb_Mod_Setting.TabIndex = 0;
            this.gb_Mod_Setting.TabStop = false;
            this.gb_Mod_Setting.Text = "설정";
            // 
            // btn_Mod_Save
            // 
            this.btn_Mod_Save.Location = new System.Drawing.Point(731, 143);
            this.btn_Mod_Save.Name = "btn_Mod_Save";
            this.btn_Mod_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Mod_Save.TabIndex = 15;
            this.btn_Mod_Save.Text = "저장";
            this.btn_Mod_Save.UseVisualStyleBackColor = true;
            // 
            // lst_Mod_Version
            // 
            this.lst_Mod_Version.ContextMenuStrip = this.cms_Version;
            this.lst_Mod_Version.FormattingEnabled = true;
            this.lst_Mod_Version.ItemHeight = 12;
            this.lst_Mod_Version.Location = new System.Drawing.Point(397, 14);
            this.lst_Mod_Version.Name = "lst_Mod_Version";
            this.lst_Mod_Version.Size = new System.Drawing.Size(409, 124);
            this.lst_Mod_Version.TabIndex = 14;
            // 
            // cms_Version
            // 
            this.cms_Version.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_Delete});
            this.cms_Version.Name = "cms_Version";
            this.cms_Version.Size = new System.Drawing.Size(155, 26);
            // 
            // mi_Delete
            // 
            this.mi_Delete.Name = "mi_Delete";
            this.mi_Delete.Size = new System.Drawing.Size(154, 22);
            this.mi_Delete.Text = "해당 버전 삭제";
            // 
            // txt_Mod_Down
            // 
            this.txt_Mod_Down.Location = new System.Drawing.Point(100, 140);
            this.txt_Mod_Down.Name = "txt_Mod_Down";
            this.txt_Mod_Down.Size = new System.Drawing.Size(291, 21);
            this.txt_Mod_Down.TabIndex = 13;
            // 
            // lb_Mod_Down
            // 
            this.lb_Mod_Down.AutoSize = true;
            this.lb_Mod_Down.Location = new System.Drawing.Point(5, 143);
            this.lb_Mod_Down.Name = "lb_Mod_Down";
            this.lb_Mod_Down.Size = new System.Drawing.Size(89, 12);
            this.lb_Mod_Down.TabIndex = 12;
            this.lb_Mod_Down.Text = "파일서버 주소 :";
            // 
            // txt_Mod_News
            // 
            this.txt_Mod_News.Location = new System.Drawing.Point(100, 119);
            this.txt_Mod_News.Name = "txt_Mod_News";
            this.txt_Mod_News.Size = new System.Drawing.Size(291, 21);
            this.txt_Mod_News.TabIndex = 11;
            // 
            // lb_Mod_News
            // 
            this.lb_Mod_News.AutoSize = true;
            this.lb_Mod_News.Location = new System.Drawing.Point(20, 122);
            this.lb_Mod_News.Name = "lb_Mod_News";
            this.lb_Mod_News.Size = new System.Drawing.Size(74, 12);
            this.lb_Mod_News.TabIndex = 10;
            this.lb_Mod_News.Text = "News 주소 :";
            // 
            // cb_Mod_Option
            // 
            this.cb_Mod_Option.FormattingEnabled = true;
            this.cb_Mod_Option.Location = new System.Drawing.Point(100, 98);
            this.cb_Mod_Option.Name = "cb_Mod_Option";
            this.cb_Mod_Option.Size = new System.Drawing.Size(291, 20);
            this.cb_Mod_Option.TabIndex = 9;
            // 
            // cb_Mod_Base
            // 
            this.cb_Mod_Base.FormattingEnabled = true;
            this.cb_Mod_Base.Location = new System.Drawing.Point(100, 77);
            this.cb_Mod_Base.Name = "cb_Mod_Base";
            this.cb_Mod_Base.Size = new System.Drawing.Size(291, 20);
            this.cb_Mod_Base.TabIndex = 8;
            // 
            // lb_Mod_Option
            // 
            this.lb_Mod_Option.AutoSize = true;
            this.lb_Mod_Option.Location = new System.Drawing.Point(45, 101);
            this.lb_Mod_Option.Name = "lb_Mod_Option";
            this.lb_Mod_Option.Size = new System.Drawing.Size(49, 12);
            this.lb_Mod_Option.TabIndex = 7;
            this.lb_Mod_Option.Text = "옵션팩 :";
            // 
            // lb_Mod_Base
            // 
            this.lb_Mod_Base.AutoSize = true;
            this.lb_Mod_Base.Location = new System.Drawing.Point(33, 80);
            this.lb_Mod_Base.Name = "lb_Mod_Base";
            this.lb_Mod_Base.Size = new System.Drawing.Size(61, 12);
            this.lb_Mod_Base.TabIndex = 6;
            this.lb_Mod_Base.Text = "베이스팩 :";
            // 
            // lb_Mod_Recommended
            // 
            this.lb_Mod_Recommended.AutoSize = true;
            this.lb_Mod_Recommended.Location = new System.Drawing.Point(33, 59);
            this.lb_Mod_Recommended.Name = "lb_Mod_Recommended";
            this.lb_Mod_Recommended.Size = new System.Drawing.Size(61, 12);
            this.lb_Mod_Recommended.TabIndex = 5;
            this.lb_Mod_Recommended.Text = "권장버전 :";
            // 
            // txt_Mod_Recommended
            // 
            this.txt_Mod_Recommended.Location = new System.Drawing.Point(100, 56);
            this.txt_Mod_Recommended.Name = "txt_Mod_Recommended";
            this.txt_Mod_Recommended.Size = new System.Drawing.Size(291, 21);
            this.txt_Mod_Recommended.TabIndex = 4;
            // 
            // txt_Mod_Latest
            // 
            this.txt_Mod_Latest.Location = new System.Drawing.Point(100, 35);
            this.txt_Mod_Latest.Name = "txt_Mod_Latest";
            this.txt_Mod_Latest.Size = new System.Drawing.Size(291, 21);
            this.txt_Mod_Latest.TabIndex = 3;
            // 
            // lb_Mod_Latest
            // 
            this.lb_Mod_Latest.AutoSize = true;
            this.lb_Mod_Latest.Location = new System.Drawing.Point(33, 38);
            this.lb_Mod_Latest.Name = "lb_Mod_Latest";
            this.lb_Mod_Latest.Size = new System.Drawing.Size(61, 12);
            this.lb_Mod_Latest.TabIndex = 2;
            this.lb_Mod_Latest.Text = "최신버전 :";
            // 
            // txt_Mod_Name
            // 
            this.txt_Mod_Name.Location = new System.Drawing.Point(100, 14);
            this.txt_Mod_Name.Name = "txt_Mod_Name";
            this.txt_Mod_Name.Size = new System.Drawing.Size(291, 21);
            this.txt_Mod_Name.TabIndex = 1;
            // 
            // lb_Mod_Name
            // 
            this.lb_Mod_Name.AutoSize = true;
            this.lb_Mod_Name.Location = new System.Drawing.Point(17, 17);
            this.lb_Mod_Name.Name = "lb_Mod_Name";
            this.lb_Mod_Name.Size = new System.Drawing.Size(77, 12);
            this.lb_Mod_Name.TabIndex = 0;
            this.lb_Mod_Name.Text = "모드팩 이름 :";
            // 
            // tp_BasePack
            // 
            this.tp_BasePack.Controls.Add(this.gb_Base_Upload);
            this.tp_BasePack.Controls.Add(this.gb_Base_Info);
            this.tp_BasePack.Controls.Add(this.gb_Base_Setting);
            this.tp_BasePack.Location = new System.Drawing.Point(4, 22);
            this.tp_BasePack.Name = "tp_BasePack";
            this.tp_BasePack.Padding = new System.Windows.Forms.Padding(3);
            this.tp_BasePack.Size = new System.Drawing.Size(818, 409);
            this.tp_BasePack.TabIndex = 1;
            this.tp_BasePack.Text = "베이스팩";
            this.tp_BasePack.UseVisualStyleBackColor = true;
            // 
            // gb_Base_Upload
            // 
            this.gb_Base_Upload.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gb_Base_Upload.Enabled = false;
            this.gb_Base_Upload.Location = new System.Drawing.Point(3, 170);
            this.gb_Base_Upload.Name = "gb_Base_Upload";
            this.gb_Base_Upload.Size = new System.Drawing.Size(812, 236);
            this.gb_Base_Upload.TabIndex = 5;
            this.gb_Base_Upload.TabStop = false;
            this.gb_Base_Upload.Text = "업로드";
            // 
            // gb_Base_Info
            // 
            this.gb_Base_Info.Controls.Add(this.btn_Base_Set);
            this.gb_Base_Info.Controls.Add(this.lb_BUID);
            this.gb_Base_Info.Controls.Add(this.txt_BUID);
            this.gb_Base_Info.Dock = System.Windows.Forms.DockStyle.Top;
            this.gb_Base_Info.Location = new System.Drawing.Point(3, 3);
            this.gb_Base_Info.Name = "gb_Base_Info";
            this.gb_Base_Info.Size = new System.Drawing.Size(812, 43);
            this.gb_Base_Info.TabIndex = 4;
            this.gb_Base_Info.TabStop = false;
            this.gb_Base_Info.Text = "정보";
            // 
            // btn_Base_Set
            // 
            this.btn_Base_Set.Location = new System.Drawing.Point(743, 12);
            this.btn_Base_Set.Name = "btn_Base_Set";
            this.btn_Base_Set.Size = new System.Drawing.Size(63, 23);
            this.btn_Base_Set.TabIndex = 2;
            this.btn_Base_Set.Text = "불러오기";
            this.btn_Base_Set.UseVisualStyleBackColor = true;
            // 
            // lb_BUID
            // 
            this.lb_BUID.AutoSize = true;
            this.lb_BUID.Location = new System.Drawing.Point(0, 17);
            this.lb_BUID.Name = "lb_BUID";
            this.lb_BUID.Size = new System.Drawing.Size(40, 12);
            this.lb_BUID.TabIndex = 1;
            this.lb_BUID.Text = "BUID :";
            // 
            // txt_BUID
            // 
            this.txt_BUID.Location = new System.Drawing.Point(49, 14);
            this.txt_BUID.Name = "txt_BUID";
            this.txt_BUID.Size = new System.Drawing.Size(688, 21);
            this.txt_BUID.TabIndex = 0;
            // 
            // gb_Base_Setting
            // 
            this.gb_Base_Setting.Controls.Add(this.btn_Base_Save);
            this.gb_Base_Setting.Controls.Add(this.lst_Base_Version);
            this.gb_Base_Setting.Controls.Add(this.txt_Base_Down);
            this.gb_Base_Setting.Controls.Add(this.lb_Base_Down);
            this.gb_Base_Setting.Controls.Add(this.lb_Base_Recommended);
            this.gb_Base_Setting.Controls.Add(this.txt_Base_Recommended);
            this.gb_Base_Setting.Controls.Add(this.txt_Base_Latest);
            this.gb_Base_Setting.Controls.Add(this.lb_Base_Latest);
            this.gb_Base_Setting.Controls.Add(this.txt_Base_Name);
            this.gb_Base_Setting.Controls.Add(this.lb_Base_Name);
            this.gb_Base_Setting.Enabled = false;
            this.gb_Base_Setting.Location = new System.Drawing.Point(3, 52);
            this.gb_Base_Setting.Name = "gb_Base_Setting";
            this.gb_Base_Setting.Size = new System.Drawing.Size(812, 112);
            this.gb_Base_Setting.TabIndex = 3;
            this.gb_Base_Setting.TabStop = false;
            this.gb_Base_Setting.Text = "설정";
            // 
            // btn_Base_Save
            // 
            this.btn_Base_Save.Location = new System.Drawing.Point(731, 84);
            this.btn_Base_Save.Name = "btn_Base_Save";
            this.btn_Base_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Base_Save.TabIndex = 15;
            this.btn_Base_Save.Text = "저장";
            this.btn_Base_Save.UseVisualStyleBackColor = true;
            // 
            // lst_Base_Version
            // 
            this.lst_Base_Version.FormattingEnabled = true;
            this.lst_Base_Version.ItemHeight = 12;
            this.lst_Base_Version.Location = new System.Drawing.Point(397, 14);
            this.lst_Base_Version.Name = "lst_Base_Version";
            this.lst_Base_Version.Size = new System.Drawing.Size(409, 64);
            this.lst_Base_Version.TabIndex = 14;
            // 
            // txt_Base_Down
            // 
            this.txt_Base_Down.Location = new System.Drawing.Point(100, 77);
            this.txt_Base_Down.Name = "txt_Base_Down";
            this.txt_Base_Down.Size = new System.Drawing.Size(291, 21);
            this.txt_Base_Down.TabIndex = 13;
            // 
            // lb_Base_Down
            // 
            this.lb_Base_Down.AutoSize = true;
            this.lb_Base_Down.Location = new System.Drawing.Point(5, 80);
            this.lb_Base_Down.Name = "lb_Base_Down";
            this.lb_Base_Down.Size = new System.Drawing.Size(89, 12);
            this.lb_Base_Down.TabIndex = 12;
            this.lb_Base_Down.Text = "파일서버 주소 :";
            // 
            // lb_Base_Recommended
            // 
            this.lb_Base_Recommended.AutoSize = true;
            this.lb_Base_Recommended.Location = new System.Drawing.Point(33, 59);
            this.lb_Base_Recommended.Name = "lb_Base_Recommended";
            this.lb_Base_Recommended.Size = new System.Drawing.Size(61, 12);
            this.lb_Base_Recommended.TabIndex = 5;
            this.lb_Base_Recommended.Text = "권장버전 :";
            // 
            // txt_Base_Recommended
            // 
            this.txt_Base_Recommended.Location = new System.Drawing.Point(100, 56);
            this.txt_Base_Recommended.Name = "txt_Base_Recommended";
            this.txt_Base_Recommended.Size = new System.Drawing.Size(291, 21);
            this.txt_Base_Recommended.TabIndex = 4;
            // 
            // txt_Base_Latest
            // 
            this.txt_Base_Latest.Location = new System.Drawing.Point(100, 35);
            this.txt_Base_Latest.Name = "txt_Base_Latest";
            this.txt_Base_Latest.Size = new System.Drawing.Size(291, 21);
            this.txt_Base_Latest.TabIndex = 3;
            // 
            // lb_Base_Latest
            // 
            this.lb_Base_Latest.AutoSize = true;
            this.lb_Base_Latest.Location = new System.Drawing.Point(33, 38);
            this.lb_Base_Latest.Name = "lb_Base_Latest";
            this.lb_Base_Latest.Size = new System.Drawing.Size(61, 12);
            this.lb_Base_Latest.TabIndex = 2;
            this.lb_Base_Latest.Text = "최신버전 :";
            // 
            // txt_Base_Name
            // 
            this.txt_Base_Name.Location = new System.Drawing.Point(100, 14);
            this.txt_Base_Name.Name = "txt_Base_Name";
            this.txt_Base_Name.Size = new System.Drawing.Size(291, 21);
            this.txt_Base_Name.TabIndex = 1;
            // 
            // lb_Base_Name
            // 
            this.lb_Base_Name.AutoSize = true;
            this.lb_Base_Name.Location = new System.Drawing.Point(5, 17);
            this.lb_Base_Name.Name = "lb_Base_Name";
            this.lb_Base_Name.Size = new System.Drawing.Size(89, 12);
            this.lb_Base_Name.TabIndex = 0;
            this.lb_Base_Name.Text = "베이스팩 이름 :";
            // 
            // tp_OptionPack
            // 
            this.tp_OptionPack.Controls.Add(this.gb_Option_Upload);
            this.tp_OptionPack.Controls.Add(this.gb_Option_Info);
            this.tp_OptionPack.Controls.Add(this.gb_Option_Setting);
            this.tp_OptionPack.Location = new System.Drawing.Point(4, 22);
            this.tp_OptionPack.Name = "tp_OptionPack";
            this.tp_OptionPack.Size = new System.Drawing.Size(818, 409);
            this.tp_OptionPack.TabIndex = 2;
            this.tp_OptionPack.Text = "옵션팩";
            this.tp_OptionPack.UseVisualStyleBackColor = true;
            // 
            // gb_Option_Upload
            // 
            this.gb_Option_Upload.Enabled = false;
            this.gb_Option_Upload.Location = new System.Drawing.Point(3, 170);
            this.gb_Option_Upload.Name = "gb_Option_Upload";
            this.gb_Option_Upload.Size = new System.Drawing.Size(812, 236);
            this.gb_Option_Upload.TabIndex = 8;
            this.gb_Option_Upload.TabStop = false;
            this.gb_Option_Upload.Text = "업로드";
            // 
            // gb_Option_Info
            // 
            this.gb_Option_Info.Controls.Add(this.btn_Option_Set);
            this.gb_Option_Info.Controls.Add(this.lb_OUID);
            this.gb_Option_Info.Controls.Add(this.txt_OUID);
            this.gb_Option_Info.Location = new System.Drawing.Point(3, 3);
            this.gb_Option_Info.Name = "gb_Option_Info";
            this.gb_Option_Info.Size = new System.Drawing.Size(812, 43);
            this.gb_Option_Info.TabIndex = 7;
            this.gb_Option_Info.TabStop = false;
            this.gb_Option_Info.Text = "정보";
            // 
            // btn_Option_Set
            // 
            this.btn_Option_Set.Location = new System.Drawing.Point(743, 12);
            this.btn_Option_Set.Name = "btn_Option_Set";
            this.btn_Option_Set.Size = new System.Drawing.Size(63, 23);
            this.btn_Option_Set.TabIndex = 2;
            this.btn_Option_Set.Text = "불러오기";
            this.btn_Option_Set.UseVisualStyleBackColor = true;
            // 
            // lb_OUID
            // 
            this.lb_OUID.AutoSize = true;
            this.lb_OUID.Location = new System.Drawing.Point(0, 17);
            this.lb_OUID.Name = "lb_OUID";
            this.lb_OUID.Size = new System.Drawing.Size(33, 12);
            this.lb_OUID.TabIndex = 1;
            this.lb_OUID.Text = "OUID";
            // 
            // txt_OUID
            // 
            this.txt_OUID.Location = new System.Drawing.Point(49, 14);
            this.txt_OUID.Name = "txt_OUID";
            this.txt_OUID.Size = new System.Drawing.Size(688, 21);
            this.txt_OUID.TabIndex = 0;
            // 
            // gb_Option_Setting
            // 
            this.gb_Option_Setting.Controls.Add(this.btn_Option_Save);
            this.gb_Option_Setting.Controls.Add(this.lst_Option_Version);
            this.gb_Option_Setting.Controls.Add(this.txt_Option_Down);
            this.gb_Option_Setting.Controls.Add(this.lb_Option_Down);
            this.gb_Option_Setting.Controls.Add(this.lb_Option_Recommended);
            this.gb_Option_Setting.Controls.Add(this.txt_Option_Recommended);
            this.gb_Option_Setting.Controls.Add(this.txt_Option_Latest);
            this.gb_Option_Setting.Controls.Add(this.lb_Option_Latest);
            this.gb_Option_Setting.Controls.Add(this.txt_Option_Name);
            this.gb_Option_Setting.Controls.Add(this.lb_Option_Name);
            this.gb_Option_Setting.Enabled = false;
            this.gb_Option_Setting.Location = new System.Drawing.Point(3, 52);
            this.gb_Option_Setting.Name = "gb_Option_Setting";
            this.gb_Option_Setting.Size = new System.Drawing.Size(812, 112);
            this.gb_Option_Setting.TabIndex = 6;
            this.gb_Option_Setting.TabStop = false;
            this.gb_Option_Setting.Text = "설정";
            // 
            // btn_Option_Save
            // 
            this.btn_Option_Save.Location = new System.Drawing.Point(731, 84);
            this.btn_Option_Save.Name = "btn_Option_Save";
            this.btn_Option_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Option_Save.TabIndex = 15;
            this.btn_Option_Save.Text = "저장";
            this.btn_Option_Save.UseVisualStyleBackColor = true;
            // 
            // lst_Option_Version
            // 
            this.lst_Option_Version.FormattingEnabled = true;
            this.lst_Option_Version.ItemHeight = 12;
            this.lst_Option_Version.Location = new System.Drawing.Point(397, 14);
            this.lst_Option_Version.Name = "lst_Option_Version";
            this.lst_Option_Version.Size = new System.Drawing.Size(409, 64);
            this.lst_Option_Version.TabIndex = 14;
            // 
            // txt_Option_Down
            // 
            this.txt_Option_Down.Location = new System.Drawing.Point(100, 77);
            this.txt_Option_Down.Name = "txt_Option_Down";
            this.txt_Option_Down.Size = new System.Drawing.Size(291, 21);
            this.txt_Option_Down.TabIndex = 13;
            // 
            // lb_Option_Down
            // 
            this.lb_Option_Down.AutoSize = true;
            this.lb_Option_Down.Location = new System.Drawing.Point(5, 80);
            this.lb_Option_Down.Name = "lb_Option_Down";
            this.lb_Option_Down.Size = new System.Drawing.Size(89, 12);
            this.lb_Option_Down.TabIndex = 12;
            this.lb_Option_Down.Text = "파일서버 주소 :";
            // 
            // lb_Option_Recommended
            // 
            this.lb_Option_Recommended.AutoSize = true;
            this.lb_Option_Recommended.Location = new System.Drawing.Point(33, 59);
            this.lb_Option_Recommended.Name = "lb_Option_Recommended";
            this.lb_Option_Recommended.Size = new System.Drawing.Size(61, 12);
            this.lb_Option_Recommended.TabIndex = 5;
            this.lb_Option_Recommended.Text = "권장버전 :";
            // 
            // txt_Option_Recommended
            // 
            this.txt_Option_Recommended.Location = new System.Drawing.Point(100, 56);
            this.txt_Option_Recommended.Name = "txt_Option_Recommended";
            this.txt_Option_Recommended.Size = new System.Drawing.Size(291, 21);
            this.txt_Option_Recommended.TabIndex = 4;
            // 
            // txt_Option_Latest
            // 
            this.txt_Option_Latest.Location = new System.Drawing.Point(100, 35);
            this.txt_Option_Latest.Name = "txt_Option_Latest";
            this.txt_Option_Latest.Size = new System.Drawing.Size(291, 21);
            this.txt_Option_Latest.TabIndex = 3;
            // 
            // lb_Option_Latest
            // 
            this.lb_Option_Latest.AutoSize = true;
            this.lb_Option_Latest.Location = new System.Drawing.Point(33, 38);
            this.lb_Option_Latest.Name = "lb_Option_Latest";
            this.lb_Option_Latest.Size = new System.Drawing.Size(61, 12);
            this.lb_Option_Latest.TabIndex = 2;
            this.lb_Option_Latest.Text = "최신버전 :";
            // 
            // txt_Option_Name
            // 
            this.txt_Option_Name.Location = new System.Drawing.Point(100, 14);
            this.txt_Option_Name.Name = "txt_Option_Name";
            this.txt_Option_Name.Size = new System.Drawing.Size(291, 21);
            this.txt_Option_Name.TabIndex = 1;
            // 
            // lb_Option_Name
            // 
            this.lb_Option_Name.AutoSize = true;
            this.lb_Option_Name.Location = new System.Drawing.Point(17, 17);
            this.lb_Option_Name.Name = "lb_Option_Name";
            this.lb_Option_Name.Size = new System.Drawing.Size(77, 12);
            this.lb_Option_Name.TabIndex = 0;
            this.lb_Option_Name.Text = "옵션팩 이름 :";
            // 
            // BSU_ModManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(826, 435);
            this.Controls.Add(this.tc_Pack);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BSU_ModManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "모드팩 관리";
            this.tc_Pack.ResumeLayout(false);
            this.tp_ModPack.ResumeLayout(false);
            this.gb_Mod_Upload.ResumeLayout(false);
            this.gb_Mod_Upload.PerformLayout();
            this.cms_File.ResumeLayout(false);
            this.gb_Mod_Info.ResumeLayout(false);
            this.gb_Mod_Info.PerformLayout();
            this.gb_Mod_Setting.ResumeLayout(false);
            this.gb_Mod_Setting.PerformLayout();
            this.cms_Version.ResumeLayout(false);
            this.tp_BasePack.ResumeLayout(false);
            this.gb_Base_Info.ResumeLayout(false);
            this.gb_Base_Info.PerformLayout();
            this.gb_Base_Setting.ResumeLayout(false);
            this.gb_Base_Setting.PerformLayout();
            this.tp_OptionPack.ResumeLayout(false);
            this.gb_Option_Info.ResumeLayout(false);
            this.gb_Option_Info.PerformLayout();
            this.gb_Option_Setting.ResumeLayout(false);
            this.gb_Option_Setting.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tc_Pack;
        private System.Windows.Forms.TabPage tp_ModPack;
        private System.Windows.Forms.TabPage tp_BasePack;
        private System.Windows.Forms.TabPage tp_OptionPack;
        private System.Windows.Forms.GroupBox gb_Mod_Setting;
        private System.Windows.Forms.GroupBox gb_Mod_Info;
        private System.Windows.Forms.TextBox txt_MUID;
        private System.Windows.Forms.Label lb_MUID;
        private System.Windows.Forms.Button btn_Mod_Set;
        private System.Windows.Forms.Label lb_Mod_Name;
        private System.Windows.Forms.TextBox txt_Mod_Name;
        private System.Windows.Forms.Label lb_Mod_Latest;
        private System.Windows.Forms.TextBox txt_Mod_Latest;
        private System.Windows.Forms.Label lb_Mod_Recommended;
        private System.Windows.Forms.TextBox txt_Mod_Recommended;
        private System.Windows.Forms.Label lb_Mod_Option;
        private System.Windows.Forms.Label lb_Mod_Base;
        private System.Windows.Forms.ComboBox cb_Mod_Base;
        private System.Windows.Forms.ComboBox cb_Mod_Option;
        private System.Windows.Forms.Label lb_Mod_News;
        private System.Windows.Forms.TextBox txt_Mod_News;
        private System.Windows.Forms.Label lb_Mod_Down;
        private System.Windows.Forms.TextBox txt_Mod_Down;
        private System.Windows.Forms.ListBox lst_Mod_Version;
        private System.Windows.Forms.Button btn_Mod_Save;
        private System.Windows.Forms.GroupBox gb_Mod_Upload;
        private System.Windows.Forms.GroupBox gb_Base_Upload;
        private System.Windows.Forms.GroupBox gb_Base_Info;
        private System.Windows.Forms.Button btn_Base_Set;
        private System.Windows.Forms.Label lb_BUID;
        private System.Windows.Forms.TextBox txt_BUID;
        private System.Windows.Forms.GroupBox gb_Base_Setting;
        private System.Windows.Forms.Button btn_Base_Save;
        private System.Windows.Forms.ListBox lst_Base_Version;
        private System.Windows.Forms.TextBox txt_Base_Down;
        private System.Windows.Forms.Label lb_Base_Down;
        private System.Windows.Forms.Label lb_Base_Recommended;
        private System.Windows.Forms.TextBox txt_Base_Recommended;
        private System.Windows.Forms.TextBox txt_Base_Latest;
        private System.Windows.Forms.Label lb_Base_Latest;
        private System.Windows.Forms.TextBox txt_Base_Name;
        private System.Windows.Forms.Label lb_Base_Name;
        private System.Windows.Forms.GroupBox gb_Option_Upload;
        private System.Windows.Forms.GroupBox gb_Option_Info;
        private System.Windows.Forms.Button btn_Option_Set;
        private System.Windows.Forms.Label lb_OUID;
        private System.Windows.Forms.TextBox txt_OUID;
        private System.Windows.Forms.GroupBox gb_Option_Setting;
        private System.Windows.Forms.Button btn_Option_Save;
        private System.Windows.Forms.ListBox lst_Option_Version;
        private System.Windows.Forms.TextBox txt_Option_Down;
        private System.Windows.Forms.Label lb_Option_Down;
        private System.Windows.Forms.Label lb_Option_Recommended;
        private System.Windows.Forms.TextBox txt_Option_Recommended;
        private System.Windows.Forms.TextBox txt_Option_Latest;
        private System.Windows.Forms.Label lb_Option_Latest;
        private System.Windows.Forms.TextBox txt_Option_Name;
        private System.Windows.Forms.Label lb_Option_Name;
        private System.Windows.Forms.ContextMenuStrip cms_Version;
        private System.Windows.Forms.ToolStripMenuItem mi_Delete;
        private System.Windows.Forms.Label lb_Mod_Version;
        private System.Windows.Forms.TextBox txt_Version;
        private System.Windows.Forms.ListBox lst_Mod_File;
        private System.Windows.Forms.ContextMenuStrip cms_File;
        private System.Windows.Forms.ToolStripMenuItem mi_Exclusion;

    }
}