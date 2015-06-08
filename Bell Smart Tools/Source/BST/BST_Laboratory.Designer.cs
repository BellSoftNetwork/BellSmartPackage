namespace Bell_Smart_Tools.Source.BST
{
    partial class BST_Laboratory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BST_Laboratory));
            this.Button_TicTacToe = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Button_TicTacToe
            // 
            this.Button_TicTacToe.Location = new System.Drawing.Point(13, 13);
            this.Button_TicTacToe.Name = "Button_TicTacToe";
            this.Button_TicTacToe.Size = new System.Drawing.Size(246, 23);
            this.Button_TicTacToe.TabIndex = 0;
            this.Button_TicTacToe.Text = "Tic Tac Toe 게임 하러가기!";
            this.Button_TicTacToe.UseVisualStyleBackColor = true;
            this.Button_TicTacToe.Click += new System.EventHandler(this.Button_TicTacToe_Click);
            // 
            // BST_Laboratory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(271, 174);
            this.Controls.Add(this.Button_TicTacToe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BST_Laboratory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BST 실험실";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Button_TicTacToe;
    }
}