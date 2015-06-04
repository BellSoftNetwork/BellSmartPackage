using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bell_Smart_Tools.Source.BST
{
    public partial class BST_Main : Form
    {
        public BST_Main()
        {
            InitializeComponent();
        }

        private void BST_Main_Load(object sender, EventArgs e)
        {

        }
        protected override void OnClosing(CancelEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Data.Common.test);
            Class.Common.Message(Data.Common.test);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Data.Common.test = textBox1.Text;
        }
    }
}
