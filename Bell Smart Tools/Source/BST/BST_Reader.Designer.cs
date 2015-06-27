namespace Bell_Smart_Tools.Source.BST
{
    partial class BST_Reader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BST_Reader));
            this.txt_Content = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txt_Content
            // 
            this.txt_Content.AllowDrop = true;
            this.txt_Content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Content.Location = new System.Drawing.Point(0, 0);
            this.txt_Content.Multiline = true;
            this.txt_Content.Name = "txt_Content";
            this.txt_Content.ReadOnly = true;
            this.txt_Content.Size = new System.Drawing.Size(364, 250);
            this.txt_Content.TabIndex = 0;
            this.txt_Content.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_Content_DragDrop);
            this.txt_Content.DragOver += new System.Windows.Forms.DragEventHandler(this.txt_Content_DragOver);
            // 
            // BST_Reader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(364, 250);
            this.Controls.Add(this.txt_Content);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BST_Reader";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "전용 텍스트 파일 리더";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Content;
    }
}