using Bell_Smart_Server.Source.Class;
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
            if (detail.GetData(ServerDetail.Data.AutoRestart) == "True")
                cbAutoRestart.IsChecked = true;
            else
                cbAutoRestart.IsChecked = false;
        }

        /// <summary>
        /// 상세설정을 저장합니다.
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            detail.SetData((bool)cbAutoRestart.IsChecked);

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
