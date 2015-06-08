using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace Bell_Smart_Tools.Source.BST
{
    public partial class BST_TicTacToe : MetroForm
    {
        bool turn = false;

        public BST_TicTacToe()
        {
            InitializeComponent();

            /* Initialize Tic Button */
            TicButton1.Click += new EventHandler(this.TicButton_Click);
            TicButton2.Click += new EventHandler(this.TicButton_Click);
            TicButton3.Click += new EventHandler(this.TicButton_Click);
            TicButton4.Click += new EventHandler(this.TicButton_Click);
            TicButton5.Click += new EventHandler(this.TicButton_Click);
            TicButton6.Click += new EventHandler(this.TicButton_Click);
            TicButton7.Click += new EventHandler(this.TicButton_Click);
            TicButton8.Click += new EventHandler(this.TicButton_Click);
            TicButton9.Click += new EventHandler(this.TicButton_Click);

            /* Initialize Info Button */
            InfoButton.Click += new EventHandler(this.InfoButton_Click);
        }

        private void TicButton_Click(object sender, EventArgs e)
        {
            var btn = (MetroFramework.Controls.MetroButton)sender;

            if (turn)
                btn.Text = "O";
            else
                btn.Text = "X";

            turn = !turn;
        }

        private void InfoButton_Click(object sender, EventArgs e)
        {
            MetroFramework.MetroMessageBox.Show(this, "Work in Progress" + Environment.NewLine +
                                                      "개발중인 기능입니다.");
        }
    }
}
