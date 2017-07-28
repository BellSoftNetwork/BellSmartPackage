using Bell_Smart_Server.Source.Class;
using BellLib.Class;
using BellLib.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Threading;

namespace Bell_Smart_Server.Source.Frame
{
    /// <summary>
    /// Main.xaml에 대한 상호 작용 논리
    /// 소스는 분류에 따라 Source\Class\Partial\Main 폴더 안애 정의하였음.
    /// 메인창에 기능이 많으므로 조금이나마 가독성을 높이기 위해 각 기능에만 해당하는 메소드는 해당 파일에 정의하고,
    /// 공통적으로 사용하는 메소드만 이 파일에 정의하기 바람.
    /// </summary>
    public partial class Main : Window
    {
        #region *** FIELD ***

        private Process ServerProc;

        /// <summary>
        /// 플레이어 데이터
        /// </summary>
        public class Player
        {
            public bool select { get; set; }
            public string nickname { get; set; }
            public string ip { get; set; }
            public string jointime { get; set; }
            public string suspects { get; set; }
        }
        
        #endregion

        #region *** INITIALIZE ***

        /// <summary>
        /// 서버 메인화면을 초기화합니다.
        /// </summary>
        public Main()
        {
            InitializeComponent();
            PreInitialize();
            Initialize();
        }

        /// <summary>
        /// 서버창을 보여주기 전 1회 초기화합니다.
        /// </summary>
        private void PreInitialize()
        {
            this.MinHeight = 450;
            this.MinWidth = 1000;

            SetControl(false);

            tmr_SecondControl = new DispatcherTimer(); // 초 제어 타이머 초기화
            tmr_Sync = new DispatcherTimer(); // 싱크 타이머 초기화
            tmr_OperatingTime = new DispatcherTimer(); // 가동시간 타이머 초기화
            tmr_ServerControl = new DispatcherTimer(); // 서버 제어 타이머 초기화
        }

        /// <summary>
        /// 서버를 초기화합니다.
        /// </summary>
        private void Initialize()
        {
            tmr_SecondControl.Interval = TimeSpan.FromSeconds(1); // 1초 간격
            tmr_SecondControl.Tick += new EventHandler(SecondControl_Tick);

            tmr_Sync.Interval = TimeSpan.FromSeconds(5); // 5초 간격
            tmr_Sync.Tick += new EventHandler(Sync_Tick);

            tmr_OperatingTime.Interval = TimeSpan.FromSeconds(1); // 1초 간격
            tmr_OperatingTime.Tick += new EventHandler(OperatingTime_Tick);

            tmr_ServerControl.Interval = TimeSpan.FromSeconds(300); // 300초 간격
            tmr_ServerControl.Tick += new EventHandler(ServerControl_Tick);

            tmr_SecondControl.Start(); // 1초 타이머 시작
            SecondControl_Tick(null, null);

            ServerLoad();
            InitSetting();
        }
        
        /// <summary>
        /// 서버 프로필 데이터를 로드합니다.
        /// </summary>
        private void ServerLoad()
        {
            // 초기화
            cbServer.Items.Clear();

            // 로드
            foreach (string value in ServerProfile.GetProfileList(true))
                cbServer.Items.Add(value);

            // 프로필 선택
            cbServer.SelectedItem = ServerProfile.GetLastProfile();

            if (cbServer.SelectedIndex == -1)
                cbServer.SelectedIndex = 0;
        }

        #endregion

        #region *** CONTROL ***

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (ServerProc != null && !ServerProc.HasExited)
                {
                    if (WPFCom.Message("현재 서버가 가동중입니다." + Environment.NewLine + "정말로 서버를 종료하시겠습니까?", Basic.PROJECT.Bell_Smart_Server, MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.No)
                    {
                        e.Cancel = true;
                        return;
                    }

                    try
                    {
                        SendCommand("stop");
                        ServerProc.WaitForExit(10000);
                        if (!ServerProc.HasExited)
                        {
                            WPFCom.Message("서버가 종료되지 않았습니다." + Environment.NewLine + "서버를 종료하신 후 다시 시도해 주세요.", Basic.PROJECT.Bell_Smart_Server);
                            e.Cancel = true;
                        }
                        bsc.Stop();
                    }
                    catch { }
                }
            }
            catch { }
        }

        #endregion
    }
}
