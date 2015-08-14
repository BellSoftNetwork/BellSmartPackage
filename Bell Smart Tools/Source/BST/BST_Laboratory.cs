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
using System.IO;
using System.Collections.Specialized;

namespace Bell_Smart_Server.Source.BST
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

        private void btn_DataUD_Click(object sender, EventArgs e)
        {
            string URL = "http://info.softbell.net/data.php";
            WebClient webClient = new WebClient();

            NameValueCollection formData = new NameValueCollection();
            formData["Data"] = "T1";
            formData["Data"] = "T2";
            formData["TEST3"] = "T3";

            byte[] responseBytes = webClient.UploadValues(URL, "POST", formData);
            string responsefromserver = Encoding.UTF8.GetString(responseBytes);
            Common.Message(responsefromserver);
            webClient.Dispose();
        }
    }
}
