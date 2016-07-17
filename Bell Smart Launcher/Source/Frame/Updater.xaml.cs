using BellLib.Class;
using BellLib.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Deployment.Application;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bell_Smart_Launcher.Source.Frame
{
    /// <summary>
    /// BSL_Updater.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Updater : Window
    {
        public Updater()
        {
            InitializeComponent();
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
            String progressText = String.Format("총 {0:D}MB 중 {1:D}MB 다운로드됨 - {2:D}% 업데이트 완료", e.BytesTotal / (1024 << 10), e.BytesCompleted / (1024 << 10), e.ProgressPercentage);
            pb_Down.Maximum = (int)e.BytesTotal;
            pb_Down.Value = (int)e.BytesCompleted;
            downloadStatus.Content = progressText;
        }

        void ad_UpdateCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                WPFCom.Message("응용 프로그램의 최신 버전의 업데이트가 취소되었습니다.", Base.PROJECT.Bell_Smart_Launcher);
                WPFCom.End();
                return;
            }
            else if (e.Error != null)
            {
                WPFCom.Message("오류 : 응용 프로그램의 최신 버전을 설치 할 수 없습니다. 이유: \n" + e.Error.Message + "\n시스템 관리자에게 이 오류를 보고하십시오.", Base.PROJECT.Bell_Smart_Launcher);
                WPFCom.End();
                return;
            }

            WPFCom.End(true);
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            lbUpdateStatus.Content = Deploy.CurrentVersion + " -> " + Deploy.LatestVersion + " 업데이트 진행중";
            BeginUpdate();
        }
    }
}
