using Bell_Smart_Launcher.Source.Data;
using Bell_Smart_Launcher.Source.Management;
using BD = BellLib.Class.Analysis;
using BellLib.Data;
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
            if (Initialize())
            {
                Main Main = new Main();
                Main.Show();
            }
            this.Close();
        }

        private bool Initialize()
        {
            // 주요정보 로드
            GeneralSettingLoad();
            GameSettingLoad();
            DebugSettingLoad();

            // 컨트롤러 실행
            Controller Cont = new Controller();
            Cont.Initialize();

            return true;
        }

        /// <summary>
        /// 일반설정을 로드합니다.
        /// </summary>
        private static void GeneralSettingLoad()
        {
            string SettingFilePath = User.BSN_Path + "DATA\\BSL\\General.bdx";

            Game.BSL_Root = BD.Data.DataLoad(SettingFilePath, "BSL_Root"); // BSL 루트경로 로드
            /*if (Game.BSL_Root == null)
                Game.BSL_Root = User.BSN_Path;*/
            Game.BSL_Root = Game.BSL_Root == null ? User.BSN_Path : Game.BSL_Root;
            Game.Language = BD.Data.DataLoad(SettingFilePath, "Language");
            Game.ConsoleRun = boolDataLoad(SettingFilePath, "ConsoleRun", true);
            Game.KeepOpen = boolDataLoad(SettingFilePath, "KeepOpen", true);
            Game.AutoControl = boolDataLoad(SettingFilePath, "AutoControl", true);
            Game.DebugMode = boolDataLoad(SettingFilePath, "DebugMode");
        }

        /// <summary>
        /// 게임 설정을 로드합니다.
        /// </summary>
        private static void GameSettingLoad()
        {
            string SettingFilePath = User.BSN_Path + "DATA\\BSL\\Game.bdx";

            Game.Memory_Allocate = Convert.ToDouble(BD.Data.DataLoad(SettingFilePath, "Memory_Allocate"));
            Game.JAVA_Path = BD.Data.DataLoad(SettingFilePath, "JAVA_Path");
            Game.JAVA_Parameter = BD.Data.DataLoad(SettingFilePath, "JAVA_Parameter");
            Game.MultipleExe = boolDataLoad(SettingFilePath, "MultipleExe");
        }

        /// <summary>
        /// 디버그 설정을 로드합니다.
        /// </summary>
        private static void DebugSettingLoad()
        {
            string SettingFilePath = User.BSN_Path + "DATA\\BSL\\Debug.bdx";

            DebugCategory.PWV = boolDataLoad(SettingFilePath, "PWV");
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
                if (BD.Data.DataLoad(Path, Name).ToUpper() == "TRUE")
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
