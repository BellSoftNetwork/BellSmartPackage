using Bell_Smart_Launcher.Source.Data;
using Bell_Smart_Launcher.Source.Management;
using BellLib.Class.Protection;
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
