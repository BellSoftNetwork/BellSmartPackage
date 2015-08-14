using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bell_Smart_Tools.Source.BST;
using Bell_Smart_Tools.Source.BST;
using System.IO;
using BellLib.Class;
using BellLib.Data;

namespace Bell_Smart_Tools.Source.BST
{
    public partial class BSP_Selector : Form
    {
        private static bool Sel = false;
        private static bool Run = false;
        public BSP_Selector()
        {
            if (!Run)
            {
                InitializeComponent();
                Run = true;
            }
            else
            {
                this.Close();
            }
        }

        private void btn_BST_Click(object sender, EventArgs e)
        {
            if (!Sel)
            {
                BST_Loader BST = new BST_Loader();
                BST.Show();
                this.Close();
                Sel = true;
            }
        }

        private void btn_BSS_Click(object sender, EventArgs e)
        {
            if (!Sel)
            {
                BSS_Loader BSS = new BSS_Loader(User.BSN_Email);
                BSS.Show();
                this.Close();
                Sel = true;
            }
        }

        /// <summary>
        /// 지금 한창 BST만 개발중인데 개발할때 계속 BST 선택 눌러서 들어가기 귀찮으니까 개발중에만 임시로 BST 자동선택!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BSP_Selector_Shown(object sender, EventArgs e)
        {
            if ((!File.Exists(Application.StartupPath + "\\Bell Smart Server.exe") && !Sel) || User.BSN_Email != "bell04204@gmail.com") // BSS 파일이 없으면,
            {
                btn_BST_Click(sender, e); // 그냥 바로 BST 실행
                this.Close();
            }
        }
    }
}
