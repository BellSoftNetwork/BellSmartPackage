using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Net;
using BellLib.Class;

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
            ReflectionExample.Example();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SocketServer SS = new SocketServer();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SocketClient SC = new SocketClient("127.0.0.1");
            SC.Send(textBox1.Text);
            textBox1.Text = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SocketClient SC = new SocketClient("127.0.0.1");
            SC.Send(textBox3.Text);
            textBox3.Text = null;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Protection pt = new Protection();
            textBox5.Text = pt.Base64(textBox4.Text, Protection.ProtectionType.PROTECTION_ENCODE);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Protection pt = new Protection();
            textBox4.Text = pt.Base64(textBox5.Text, Protection.ProtectionType.PROTECTION_DECODE);
        }
    }
}
