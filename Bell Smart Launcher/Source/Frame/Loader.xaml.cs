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
            if (Game.BSL_Root == null)
                Game.BSL_Root = User.BSN_Path;
            Game.Language = BD.Data.DataLoad(SettingFilePath, "Language");
            try
            {
                if (BD.Data.DataLoad(SettingFilePath, "ConsoleRun").ToUpper() == "TRUE")
                    Game.ConsoleRun = true;
                else
                    Game.ConsoleRun = false;
            }
            catch
            {
                Game.ConsoleRun = true;
            }

            try
            {
                if (BD.Data.DataLoad(SettingFilePath, "KeepOpen").ToUpper() == "TRUE")
                    Game.KeepOpen = true;
                else
                    Game.KeepOpen = false;
            }
            catch
            {
                Game.KeepOpen = true;
            }

            try
            {
                if (BD.Data.DataLoad(SettingFilePath, "AutoControl").ToUpper() == "TRUE")
                    Game.AutoControl = true;
                else
                    Game.AutoControl = false;
            }
            catch
            {
                Game.AutoControl = true;
            }
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
            try
            {
                if (BD.Data.DataLoad(SettingFilePath, "MultipleExe").ToUpper() == "TRUE")
                    Game.MultipleExe = true;
                else
                    Game.MultipleExe = false;
            }
            catch
            {
                Game.MultipleExe = false;
            }
        }
    }
}
