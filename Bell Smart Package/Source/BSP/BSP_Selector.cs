using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bell_Smart_Server.Source.BSS;
using Bell_Smart_Tools.Source.BST;

namespace Bell_Smart_Package.Source.BSP
{
    public partial class BSP_Selector : Form
    {
        public BSP_Selector()
        {
            InitializeComponent();
        }

        private void btn_BST_Click(object sender, EventArgs e)
        {
            BST_Loader BST = new BST_Loader();
            BST.Show();
            this.Close();
        }

        private void btn_BSS_Click(object sender, EventArgs e)
        {
            BSS_Loader BSS = new BSS_Loader();
            BSS.Show();
            this.Close();
        }

        /// <summary>
        /// 지금 한창 BST만 개발중인데 개발할때 계속 BST 선택 눌러서 들어가기 귀찮으니까 개발중에만 임시로 BST 자동선택!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BSP_Selector_Shown(object sender, EventArgs e)
        {
            BST_Loader BST = new BST_Loader();
            BST.Show();
            this.Close();
        }
    }
}
