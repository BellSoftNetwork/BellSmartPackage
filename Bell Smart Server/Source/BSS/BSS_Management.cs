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

namespace Bell_Smart_Server.Source.BSS
{
    public partial class BSS_Management : Form
    {
        public BSS_Management()
        {
            InitializeComponent();
        }

        private void BSP_Management_Load(object sender, EventArgs e)
        {
            this.Opacity = 0; // 폼 투명도를 0으로 설정하여 안보이게함.
        }

        private int errCount;
        private void tmr_AutoUpdate_Tick(object sender, EventArgs e)
        {
            if (User.BSP_AutoUpdate)
            {
                try
                {
                    if (Deploy.UpdateAvailable())
                    {
                        BSS_Updater BSPU = new BSS_Updater();
                        BSPU.Show();

                        tmr_AutoUpdate.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    if (errCount > 2)
                    {
                        Common.Message("자동 업데이트 시스템 동작 중 문제가 발생하였습니다." + Environment.NewLine + "이 에러메시지가 자주 발생한다면 BSN 홈페이지에 피드백을 올려주시기 바랍니다." + Environment.NewLine + "errCount = " + errCount + Environment.NewLine + ex.Message + Environment.NewLine + "StackTrace : " + ex.StackTrace);
                        errCount = 0;
                    }
                    else
                    {
                        errCount += 1;
                    }

                    return;
                }
                errCount = 0;
            }
        }

        private void BSP_Management_Shown(object sender, EventArgs e)
        {
            this.Hide(); // ALT + TAB으로 관리 폼 모양이 나타나는걸 방지하기 위해 폼이 로드된 후 하이드.
        }
    }
}
