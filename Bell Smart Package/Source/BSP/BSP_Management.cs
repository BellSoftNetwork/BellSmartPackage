using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BellLib.Class;
using BellLib.Data;

namespace Bell_Smart_Package.Source.BSP
{
    public partial class BSP_Management : Form
    {
        public BSP_Management()
        {
            InitializeComponent();
        }

        private void BSP_Management_Load(object sender, EventArgs e)
        {
            this.Opacity = 0; // 폼 투명도를 0으로 설정하여 안보이게함.
        }

        private void tmr_AutoUpdate_Tick(object sender, EventArgs e)
        {
            if (User.BSP_AutoUpdate)
            {
                if (Deployment.UpdateAvailable())
                {
                    BSP_Updater BSPU = new BSP_Updater();
                    BSPU.Show();

                    tmr_AutoUpdate.Enabled = false;
                }
            }
        }

        private void BSP_Management_Shown(object sender, EventArgs e)
        {
            this.Hide(); // ALT + TAB으로 관리 폼 모양이 나타나는걸 방지하기 위해 폼이 로드된 후 하이드.
        }
    }
}
