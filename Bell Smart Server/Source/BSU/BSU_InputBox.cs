using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bell_Smart_Server.Source.BSU
{
    public partial class BSU_InputBox : Form
    {
        public BSU_InputBox(string Text, string Caption = "Bell Smart Server")
        {
            InitializeComponent();
            lb_Text.Text = Text;
            this.Text = Caption;
        }

        public string getInput()
        {
            return txt_Input.Text;
        }
        private void txt_Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btn_OK_Click(sender, e);
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            txt_Input.Text = null;
            this.Close();
        }
    }
}
