using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BellLib.Class;

namespace Bell_Smart_Tools.Source.BST
{
    public partial class BST_Updater : Form
    {
        public BST_Updater()
        {
            InitializeComponent();
        }

        private void BSP_Updater_Load(object sender, EventArgs e)
        {
            BeginUpdate();
        }

        private void BeginUpdate()
        {
            ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
            ad.UpdateCompleted += new AsyncCompletedEventHandler(ad_UpdateCompleted);

            // Indicate progress in the application's status bar.
            ad.UpdateProgressChanged += new DeploymentProgressChangedEventHandler(ad_UpdateProgressChanged);
            ad.UpdateAsync();
        }

        void ad_UpdateProgressChanged(object sender, DeploymentProgressChangedEventArgs e)
        {
            String progressText = String.Format("{0:D}MB out of {1:D}MB downloaded - {2:D}% complete", e.BytesCompleted / (1024 << 10), e.BytesTotal / (1024 << 10), e.ProgressPercentage);
            pb_Down.Maximum = (int)e.BytesTotal;
            pb_Down.Value = (int)e.BytesCompleted;
            downloadStatus.Text = progressText;
        }

        void ad_UpdateCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Common.Message("응용 프로그램의 최신 버전의 업데이트가 취소되었습니다.");
                Common.End();
                return;
            }
            else if (e.Error != null)
            {
                Common.Message("오류 : 응용 프로그램의 최신 버전을 설치 할 수 없습니다. 이유: \n" + e.Error.Message + "\n시스템 관리자에게 이 오류를 보고하십시오.");
                Common.End();
                return;
            }

            Common.End(true);
        }
    }
}
