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
using BellLib.Data;

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
    }
}
