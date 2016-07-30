using BellLib.Class;
using BellLib.Class.Minecraft;
using BellLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bell_Smart_Tools.Source.Frame
{
    /// <summary>
    /// BSC.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class BSC : Window
    {
        private BellSmartController bsc;
        public BSC()
        {
            InitializeComponent();
        }

        private void btnInit_Click(object sender, RoutedEventArgs e)
        {
            if (bsc.Initialize())
                WPFCom.Message("BSC 시스템 초기화 성공", Base.PROJECT.Bell_Smart_Tools);
            else
                WPFCom.Message("BSC 시스템 초기화에 실패하였습니다.", Base.PROJECT.Bell_Smart_Tools);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            bsc.Set_ConnectTimeout(true);
            bsc.Set_CommunicationTimeout(true, 3);

            if (bsc.Start())
                WPFCom.Message("BSC 시스템 시작 성공", Base.PROJECT.Bell_Smart_Tools);
            else
                WPFCom.Message("BSC 시스템 시작에 실패하였습니다.", Base.PROJECT.Bell_Smart_Tools);
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            bsc.Stop();
            WPFCom.Message("BSC 시스템을 종료하였습니다.", Base.PROJECT.Bell_Smart_Tools);
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            bsc = new BellSmartController(true);

            WPFCom.Message("BSC 인스턴스 생성 성공", Base.PROJECT.Bell_Smart_Tools);
        }
    }
}
