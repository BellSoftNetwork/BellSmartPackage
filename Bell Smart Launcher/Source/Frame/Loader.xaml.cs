using Bell_Smart_Launcher.Source.Data;
using BellLib.Class;
using BellLib.Class.Control;
using BellLib.Class.Protection;
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

namespace Bell_Smart_Launcher.Source.Frame
{
    /// <summary>
    /// BSL_Loader.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Loader : Window
    {
        public Loader()
        {
            InitializeComponent();
            pbLoad.Maximum = 48;
            pbLoad.Value = 0;

            SetStatus("Initialize Component");
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            // 진행률 20 추가

            SetStatus("로더 실행 완료", 1);

            SetStatus("런처 최신버전 체크", 1);
            if (UpdateControl.UpdateCheck(false))
            {
                SetStatus("업데이트 시작", 1);
                Updater updater = new Updater();
                updater.Show();

                this.Close();

                return;
            }

            SetStatus("로더 초기화 시작", 1);
            if (Initialize())
            {
                SetStatus("로더 초기화 완료", 1);

                SetStatus("런처 메인 초기화 시작", 1);
                Main Main = new Main();
                SetStatus("런처 메인 초기화 완료", 2);

                SetStatus("런처 메인 기본값 초기화 시작", 1);
                Main.PreInitialize();
                SetStatus("런처 메인 기본값 초기화 완료", 2);

                SetStatus("런처 뉴스피드 초기화 시작", 1);
                Main.InitNews();
                SetStatus("런처 뉴스피드 초기화 완료", 5);

                SetStatus("런처 모드팩 초기화 시작", 1);
                Main.InitModpacks();
                SetStatus("런처 모드팩 초기화 완료", 10);

                SetStatus("런처 리소스 초기화 시작", 1);
                Main.InitResources();
                SetStatus("런처 리소스 초기화 완료", 2);

                SetStatus("런처 맵 초기화 시작", 1);
                Main.InitMaps();
                SetStatus("런처 맵 초기화 완료", 2);

                SetStatus("런처 세팅 초기화 시작", 1);
                Main.InitSetting();
                SetStatus("런처 세팅 초기화 완료", 2);

                SetStatus("런처 실행", 1);
                Main.Show();
                SetStatus("런처 실행 완료", 1);
            }

            this.Close();
        }

        private void SetStatus(string value, double addProgress = 0)
        {
            lbStatus.Content = value;
            pbLoad.Value += addProgress;
            Common.DoEvents();
        }

        private bool Initialize()
        {
            // 진행률 10 추가

            // 주요정보 로드
            SetStatus("일반 설정을 불러오는중", 1);
            GeneralSettingLoad();
            SetStatus("일반 설정 로드 완료", 1);

            SetStatus("게임 설정을 불러오는중", 1);
            GameSettingLoad();
            SetStatus("게임 설정 로드 완료", 1);

            SetStatus("디버그 설정을 불러오는중", 1);
            DebugSettingLoad();
            SetStatus("디버그 설정 로드 완료", 1);

            // 오토업데이트 시스템 실행
            SetStatus("런처 자동업데이트 시스템 생성 시작", 1);
            Task upSys = new Task(UpdateSystem);
            SetStatus("런처 자동업데이트 시스템 생성 완료", 1);

            SetStatus("런처 자동업데이트 시스템 가동 시작", 1);
            upSys.Start();
            SetStatus("런처 자동업데이트 시스템 가동 완료", 1);

            return true;
        }

        /// <summary>
        /// 업데이트 시스템을 비동기 시작합니다.
        /// </summary>
        private static void UpdateSystem()
        {
            if (UpdateControl.UpdateCheck())
            {
                Updater updater = new Updater();
                updater.Show();
            }
        }

        /// <summary>
        /// 일반설정을 로드합니다.
        /// </summary>
        private static void GeneralSettingLoad()
        {
            Game.BSL_Root = DataProtect.DataLoad(DataPath.BSL.General, "BSL_Root"); // BSL 루트경로 로드
            /*if (Game.BSL_Root == null)
                Game.BSL_Root = User.BSN_Path;*/
            Game.BSL_Root = Game.BSL_Root == null ? User.BSN_Path : Game.BSL_Root;
            Game.Language = DataProtect.DataLoad(DataPath.BSL.General, "Language");
            Game.ConsoleRun = boolDataLoad(DataPath.BSL.General, "ConsoleRun", true);
            Game.KeepOpen = boolDataLoad(DataPath.BSL.General, "KeepOpen", true);
            Game.AutoControl = boolDataLoad(DataPath.BSL.General, "AutoControl", true);
            Game.DebugMode = boolDataLoad(DataPath.BSL.General, "DebugMode");
        }

        /// <summary>
        /// 게임 설정을 로드합니다.
        /// </summary>
        private static void GameSettingLoad()
        {
            Game.Memory_Allocate = Convert.ToDouble(DataProtect.DataLoad(DataPath.BSL.Game_Setting, "Memory_Allocate"));
            Game.JAVA_Path = DataProtect.DataLoad(DataPath.BSL.Game_Setting, "JAVA_Path");
            Game.JAVA_Parameter = DataProtect.DataLoad(DataPath.BSL.Game_Setting, "JAVA_Parameter");
            Game.MultipleExe = boolDataLoad(DataPath.BSL.Game_Setting, "MultipleExe");
        }

        /// <summary>
        /// 디버그 설정을 로드합니다.
        /// </summary>
        private static void DebugSettingLoad()
        {
            DebugCategory.PWV = boolDataLoad(DataPath.BSL.Debug_Setting, "PWV");
        }

        /// <summary>
        /// bool 타입 설정값을 불러옵니다.
        /// </summary>
        /// <param name="Path">bdx 파일 경로</param>
        /// <param name="Name">요소 이름</param>
        /// <param name="DefaultReturn">항목이 없을시 반환값</param>
        /// <returns>요소 bool 값</returns>
        private static bool boolDataLoad(string Path, string Name, bool DefaultReturn = false)
        {
            try
            {
                if (DataProtect.DataLoad(Path, Name).ToUpper() == "TRUE")
                    return true;
                else
                    return false;
            }
            catch
            {
                return DefaultReturn;
            }
        }
    }
}
