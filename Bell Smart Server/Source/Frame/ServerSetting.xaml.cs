using Bell_Smart_Server.Source.Class;
using BellLib.Class;
using System;
using System.Collections.Generic;
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

namespace Bell_Smart_Server.Source.Frame
{
    /// <summary>
    /// ServerSetting.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ServerSetting : Window
    {
        // 필드
        private ServerDetail detail;
        
        /// <summary>
        /// 서버 이름으로 초기화합니다.
        /// </summary>
        /// <param name="ServerName">서버이름</param>
        public ServerSetting(string ServerName)
        {
            InitializeComponent();
            detail = new ServerDetail(ServerName);
            Initalize();
        }

        /// <summary>
        /// 상세설정을 로드합니다.
        /// </summary>
        private void Initalize()
        {
            if (detail.GetDataString(ServerDetail.Data.AutoRestart) == "True")
            {
                cbAutoRestart.IsChecked = true;
                txtRestartDelay.IsEnabled = true;
            }
            else
            {
                cbAutoRestart.IsChecked = false;
                txtRestartDelay.IsEnabled = false;
            }
            txtRestartDelay.Text = detail.GetDataString(ServerDetail.Data.AutoRestartTime);
        }

        /// <summary>
        /// 상세설정을 저장합니다.
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // 필드
            int Delay;

            // 유효성 검사
            try
            {
                Delay = Convert.ToInt32(txtRestartDelay.Text);

                if (Delay < 1)
                {
                    WPFCom.Message("자동 재시작 대기시간은 1초 이상으로 설정하실 수 있습니다.", BellLib.Data.Base.PROJECT.Bell_Smart_Server);
                    return;
                }
            }
            catch
            {
                WPFCom.Message("자동 재시작 대기시간은 정수만 입력할 수 있습니다.", BellLib.Data.Base.PROJECT.Bell_Smart_Server);
                return;
            }
            
            // 저장
            detail.SetData((bool)cbAutoRestart.IsChecked, Delay);

            detail.Save();
            this.Close();
        }

        /// <summary>
        /// 상세설정 변경을 취소합니다.
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
