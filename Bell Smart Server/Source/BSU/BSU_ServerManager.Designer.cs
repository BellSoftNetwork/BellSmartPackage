namespace Bell_Smart_Server.Source.BSU
{
    partial class BSU_ServerManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BSU_ServerManager));
            this.gb_Info = new System.Windows.Forms.GroupBox();
            this.gb_Cloud = new System.Windows.Forms.GroupBox();
            this.lst_Info_Servers = new System.Windows.Forms.ListBox();
            this.txt_Info_URL = new System.Windows.Forms.TextBox();
            this.lb_Info_URL = new System.Windows.Forms.Label();
            this.lb_Info_Name = new System.Windows.Forms.Label();
            this.txt_Info_Name = new System.Windows.Forms.TextBox();
            this.btn_Info_Save = new System.Windows.Forms.Button();
            this.btn_Info_Add = new System.Windows.Forms.Button();
            this.btn_Info_Delete = new System.Windows.Forms.Button();
            this.lst_Cloud_Servers = new System.Windows.Forms.ListBox();
            this.lb_Cloud_Name = new System.Windows.Forms.Label();
            this.lb_Cloud_URL = new System.Windows.Forms.Label();
            this.txt_Cloud_Name = new System.Windows.Forms.TextBox();
            this.txt_Cloud_URL = new System.Windows.Forms.TextBox();
            this.btn_Cloud_Add = new System.Windows.Forms.Button();
            this.btn_Cloud_Delete = new System.Windows.Forms.Button();
            this.btn_Cloud_Save = new System.Windows.Forms.Button();
            this.gb_Info.SuspendLayout();
            this.gb_Cloud.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_Info
            // 
            this.gb_Info.BackColor = System.Drawing.Color.Transparent;
            this.gb_Info.Controls.Add(this.btn_Info_Delete);
            this.gb_Info.Controls.Add(this.btn_Info_Add);
            this.gb_Info.Controls.Add(this.btn_Info_Save);
            this.gb_Info.Controls.Add(this.txt_Info_Name);
            this.gb_Info.Controls.Add(this.lb_Info_Name);
            this.gb_Info.Controls.Add(this.lb_Info_URL);
            this.gb_Info.Controls.Add(this.txt_Info_URL);
            this.gb_Info.Controls.Add(this.lst_Info_Servers);
            this.gb_Info.Dock = System.Windows.Forms.DockStyle.Top;
            this.gb_Info.ForeColor = System.Drawing.Color.White;
            this.gb_Info.Location = new System.Drawing.Point(0, 0);
            this.gb_Info.Name = "gb_Info";
            this.gb_Info.Size = new System.Drawing.Size(572, 141);
            this.gb_Info.TabIndex = 0;
            this.gb_Info.TabStop = false;
            this.gb_Info.Text = "정보";
            // 
            // gb_Cloud
            // 
            this.gb_Cloud.BackColor = System.Drawing.Color.Transparent;
            this.gb_Cloud.Controls.Add(this.btn_Cloud_Save);
            this.gb_Cloud.Controls.Add(this.btn_Cloud_Delete);
            this.gb_Cloud.Controls.Add(this.btn_Cloud_Add);
            this.gb_Cloud.Controls.Add(this.txt_Cloud_URL);
            this.gb_Cloud.Controls.Add(this.txt_Cloud_Name);
            this.gb_Cloud.Controls.Add(this.lb_Cloud_URL);
            this.gb_Cloud.Controls.Add(this.lb_Cloud_Name);
            this.gb_Cloud.Controls.Add(this.lst_Cloud_Servers);
            this.gb_Cloud.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gb_Cloud.ForeColor = System.Drawing.Color.White;
            this.gb_Cloud.Location = new System.Drawing.Point(0, 147);
            this.gb_Cloud.Name = "gb_Cloud";
            this.gb_Cloud.Size = new System.Drawing.Size(572, 175);
            this.gb_Cloud.TabIndex = 1;
            this.gb_Cloud.TabStop = false;
            this.gb_Cloud.Text = "클라우드";
            // 
            // lst_Info_Servers
            // 
            this.lst_Info_Servers.FormattingEnabled = true;
            this.lst_Info_Servers.ItemHeight = 12;
            this.lst_Info_Servers.Location = new System.Drawing.Point(6, 12);
            this.lst_Info_Servers.Name = "lst_Info_Servers";
            this.lst_Info_Servers.Size = new System.Drawing.Size(167, 124);
            this.lst_Info_Servers.TabIndex = 0;
            this.lst_Info_Servers.SelectedIndexChanged += new System.EventHandler(this.lst_Info_Servers_SelectedIndexChanged);
            // 
            // txt_Info_URL
            // 
            this.txt_Info_URL.Location = new System.Drawing.Point(262, 37);
            this.txt_Info_URL.Name = "txt_Info_URL";
            this.txt_Info_URL.Size = new System.Drawing.Size(304, 21);
            this.txt_Info_URL.TabIndex = 1;
            // 
            // lb_Info_URL
            // 
            this.lb_Info_URL.AutoSize = true;
            this.lb_Info_URL.Location = new System.Drawing.Point(179, 40);
            this.lb_Info_URL.Name = "lb_Info_URL";
            this.lb_Info_URL.Size = new System.Drawing.Size(77, 12);
            this.lb_Info_URL.TabIndex = 2;
            this.lb_Info_URL.Text = "최상위 주소 :";
            // 
            // lb_Info_Name
            // 
            this.lb_Info_Name.AutoSize = true;
            this.lb_Info_Name.Location = new System.Drawing.Point(179, 17);
            this.lb_Info_Name.Name = "lb_Info_Name";
            this.lb_Info_Name.Size = new System.Drawing.Size(65, 12);
            this.lb_Info_Name.TabIndex = 3;
            this.lb_Info_Name.Text = "서버 이름 :";
            // 
            // txt_Info_Name
            // 
            this.txt_Info_Name.Location = new System.Drawing.Point(262, 14);
            this.txt_Info_Name.Name = "txt_Info_Name";
            this.txt_Info_Name.Size = new System.Drawing.Size(304, 21);
            this.txt_Info_Name.TabIndex = 4;
            // 
            // btn_Info_Save
            // 
            this.btn_Info_Save.ForeColor = System.Drawing.Color.Black;
            this.btn_Info_Save.Location = new System.Drawing.Point(491, 64);
            this.btn_Info_Save.Name = "btn_Info_Save";
            this.btn_Info_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Info_Save.TabIndex = 5;
            this.btn_Info_Save.Text = "저장";
            this.btn_Info_Save.UseVisualStyleBackColor = true;
            // 
            // btn_Info_Add
            // 
            this.btn_Info_Add.ForeColor = System.Drawing.Color.Black;
            this.btn_Info_Add.Location = new System.Drawing.Point(329, 64);
            this.btn_Info_Add.Name = "btn_Info_Add";
            this.btn_Info_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Info_Add.TabIndex = 6;
            this.btn_Info_Add.Text = "서버 추가";
            this.btn_Info_Add.UseVisualStyleBackColor = true;
            // 
            // btn_Info_Delete
            // 
            this.btn_Info_Delete.ForeColor = System.Drawing.Color.Black;
            this.btn_Info_Delete.Location = new System.Drawing.Point(410, 64);
            this.btn_Info_Delete.Name = "btn_Info_Delete";
            this.btn_Info_Delete.Size = new System.Drawing.Size(75, 23);
            this.btn_Info_Delete.TabIndex = 7;
            this.btn_Info_Delete.Text = "서버 삭제";
            this.btn_Info_Delete.UseVisualStyleBackColor = true;
            // 
            // lst_Cloud_Servers
            // 
            this.lst_Cloud_Servers.FormattingEnabled = true;
            this.lst_Cloud_Servers.ItemHeight = 12;
            this.lst_Cloud_Servers.Location = new System.Drawing.Point(6, 20);
            this.lst_Cloud_Servers.Name = "lst_Cloud_Servers";
            this.lst_Cloud_Servers.Size = new System.Drawing.Size(167, 148);
            this.lst_Cloud_Servers.TabIndex = 0;
            this.lst_Cloud_Servers.SelectedIndexChanged += new System.EventHandler(this.lst_Cloud_Servers_SelectedIndexChanged);
            // 
            // lb_Cloud_Name
            // 
            this.lb_Cloud_Name.AutoSize = true;
            this.lb_Cloud_Name.Location = new System.Drawing.Point(179, 98);
            this.lb_Cloud_Name.Name = "lb_Cloud_Name";
            this.lb_Cloud_Name.Size = new System.Drawing.Size(65, 12);
            this.lb_Cloud_Name.TabIndex = 1;
            this.lb_Cloud_Name.Text = "서버 이름 :";
            // 
            // lb_Cloud_URL
            // 
            this.lb_Cloud_URL.AutoSize = true;
            this.lb_Cloud_URL.Location = new System.Drawing.Point(179, 121);
            this.lb_Cloud_URL.Name = "lb_Cloud_URL";
            this.lb_Cloud_URL.Size = new System.Drawing.Size(77, 12);
            this.lb_Cloud_URL.TabIndex = 2;
            this.lb_Cloud_URL.Text = "최상위 주소 :";
            // 
            // txt_Cloud_Name
            // 
            this.txt_Cloud_Name.Location = new System.Drawing.Point(262, 95);
            this.txt_Cloud_Name.Name = "txt_Cloud_Name";
            this.txt_Cloud_Name.Size = new System.Drawing.Size(304, 21);
            this.txt_Cloud_Name.TabIndex = 3;
            // 
            // txt_Cloud_URL
            // 
            this.txt_Cloud_URL.Location = new System.Drawing.Point(262, 118);
            this.txt_Cloud_URL.Name = "txt_Cloud_URL";
            this.txt_Cloud_URL.Size = new System.Drawing.Size(304, 21);
            this.txt_Cloud_URL.TabIndex = 4;
            // 
            // btn_Cloud_Add
            // 
            this.btn_Cloud_Add.ForeColor = System.Drawing.Color.Black;
            this.btn_Cloud_Add.Location = new System.Drawing.Point(329, 145);
            this.btn_Cloud_Add.Name = "btn_Cloud_Add";
            this.btn_Cloud_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Cloud_Add.TabIndex = 5;
            this.btn_Cloud_Add.Text = "서버 추가";
            this.btn_Cloud_Add.UseVisualStyleBackColor = true;
            // 
            // btn_Cloud_Delete
            // 
            this.btn_Cloud_Delete.ForeColor = System.Drawing.Color.Black;
            this.btn_Cloud_Delete.Location = new System.Drawing.Point(410, 145);
            this.btn_Cloud_Delete.Name = "btn_Cloud_Delete";
            this.btn_Cloud_Delete.Size = new System.Drawing.Size(75, 23);
            this.btn_Cloud_Delete.TabIndex = 6;
            this.btn_Cloud_Delete.Text = "서버 삭제";
            this.btn_Cloud_Delete.UseVisualStyleBackColor = true;
            // 
            // btn_Cloud_Save
            // 
            this.btn_Cloud_Save.ForeColor = System.Drawing.Color.Black;
            this.btn_Cloud_Save.Location = new System.Drawing.Point(491, 145);
            this.btn_Cloud_Save.Name = "btn_Cloud_Save";
            this.btn_Cloud_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Cloud_Save.TabIndex = 7;
            this.btn_Cloud_Save.Text = "저장";
            this.btn_Cloud_Save.UseVisualStyleBackColor = true;
            // 
            // BSU_ServerManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Bell_Smart_Server.Properties.Resources.Logo_BSN2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(572, 322);
            this.Controls.Add(this.gb_Cloud);
            this.Controls.Add(this.gb_Info);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BSU_ServerManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "서버 관리";
            this.gb_Info.ResumeLayout(false);
            this.gb_Info.PerformLayout();
            this.gb_Cloud.ResumeLayout(false);
            this.gb_Cloud.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_Info;
        private System.Windows.Forms.GroupBox gb_Cloud;
        private System.Windows.Forms.Button btn_Info_Delete;
        private System.Windows.Forms.Button btn_Info_Add;
        private System.Windows.Forms.Button btn_Info_Save;
        private System.Windows.Forms.TextBox txt_Info_Name;
        private System.Windows.Forms.Label lb_Info_Name;
        private System.Windows.Forms.Label lb_Info_URL;
        private System.Windows.Forms.TextBox txt_Info_URL;
        private System.Windows.Forms.ListBox lst_Info_Servers;
        private System.Windows.Forms.ListBox lst_Cloud_Servers;
        private System.Windows.Forms.Button btn_Cloud_Save;
        private System.Windows.Forms.Button btn_Cloud_Delete;
        private System.Windows.Forms.Button btn_Cloud_Add;
        private System.Windows.Forms.TextBox txt_Cloud_URL;
        private System.Windows.Forms.TextBox txt_Cloud_Name;
        private System.Windows.Forms.Label lb_Cloud_URL;
        private System.Windows.Forms.Label lb_Cloud_Name;

    }
}