using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace Bell_Smart_Tools.Source.BST
{
    public partial class BST_Laboratory : Form
    {
        public BST_Laboratory()
        {
            InitializeComponent();
        }

        private void Button_TicTacToe_Click(object sender, EventArgs e)
        {
            //var game = new BST_TicTacToe();
            //game.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlTextReader XTR = new XmlTextReader(textBox1.Text);
            string Temp = null;
            while (XTR.Read())
            {
                Temp += XTR.Value;
            }
            textBox2.Text = Temp;
            XTR.Close();
        }
    }
}
