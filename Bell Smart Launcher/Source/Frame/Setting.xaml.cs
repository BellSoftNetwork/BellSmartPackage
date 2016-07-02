using Bell_Smart_Launcher.Source.Data;
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
using BellLib.Class;

namespace Bell_Smart_Launcher.Source.Frame
{
    /// <summary>
    /// Setting.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Setting : Window
    {
        private string GameSettingPath = User.BSN_Path + "DATA\\BSL\\Game.bdx";
        private string GeneralSettingPath = User.BSN_Path + "DATA\\BSL\\General.bdx";
        private string DebugSettingPath = User.BSN_Path + "DATA\\BSL\\Debug.bdx";

        public Setting()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            SystemInfo.MemoryInfo mi = SystemInfo.GetMemoryInfo();

            gen_cbLanguage.Items.Clear();
            gen_cbLanguage.Items.Add("한국어");
            gen_cbLanguage.Items.Add("English");
            gen_cbLanguage.SelectedIndex = 0;

            game_sdJAVA.Maximum = mi.Total_Physical_GB;

            SettingLoad();
        }

        /// <summary>
        /// 세팅값을 로드합니다.
        /// </summary>
        private void SettingLoad(int tab = 7) // 0111
        {
            if ((tab & 1) == 1) // 0001
            {
                gen_txtInstall.Text = Game.BSL_Root;
                if (Game.Language != null)
                    gen_cbLanguage.SelectedItem = Game.Language;
                gen_cbConsole.IsChecked = Game.ConsoleRun;
                gen_cbKeep.IsChecked = Game.KeepOpen;
                gen_cbAutoControl.IsChecked = Game.AutoControl;
                gen_cbDebugMode.IsChecked = Game.DebugMode;
            }

            if ((tab & 2) == 2) // 0010
            {
                game_sdJAVA.Value = Game.Memory_Allocate;
                game_txtJAVAPath.Text = Game.JAVA_Path;
                game_txtParameter.Text = Game.JAVA_Parameter;
                game_cbMultipleExe.IsChecked = Game.MultipleExe;

                if (game_txtJAVAPath.Text == string.Empty)
                    game_txtJAVAPath.Text = User.BSN_Path + @"Runtime\Java\x64"; // 임시 기본값 설정
            }

            if ((tab & 4) == 4) // 0100
            {
                deb_cbPWV.IsChecked = DebugCategory.PWV;
            }

            // Default
            if (Game.DebugMode)
                tiDebug.Visibility = Visibility.Visible;
            else
                tiDebug.Visibility = Visibility.Collapsed;
        }
        
        /// <summary>
        /// 일반설정을 저장합니다.
        /// </summary>
        /// <returns>저장 성공여부</returns>
        public bool SaveGeneral()
        {
            Game.BSL_Root = gen_txtInstall.Text;
            Game.Language = (string)gen_cbLanguage.SelectedItem;
            Game.ConsoleRun = (bool)gen_cbConsole.IsChecked;
            Game.KeepOpen = (bool)gen_cbKeep.IsChecked;
            Game.AutoControl = (bool)gen_cbAutoControl.IsChecked;
            Game.DebugMode = (bool)gen_cbDebugMode.IsChecked;

            BD.Data.DataSave(GeneralSettingPath, "BSL_Root", Game.BSL_Root);
            BD.Data.DataSave(GeneralSettingPath, "Laungage", Game.Language);
            BD.Data.DataSave(GeneralSettingPath, "ConsoleRun", Game.ConsoleRun.ToString());
            BD.Data.DataSave(GeneralSettingPath, "KeepOpen", Game.KeepOpen.ToString());
            BD.Data.DataSave(GeneralSettingPath, "AutoControl", Game.AutoControl.ToString());
            BD.Data.DataSave(GeneralSettingPath, "DebugMode", Game.DebugMode.ToString());

            SettingLoad(5);

            return true;
        }

        /// <summary>
        /// 게임설정을 저장합니다.
        /// </summary>
        /// <returns>저장 성공여부</returns>
        public bool SaveGame()
        {
            bool Java32bit = false;
            if (Environment.Is64BitOperatingSystem)
            { // 64비트 운영체제일경우,
                if (game_txtJAVAPath.Text.Contains("x86")) // 자바경로에 32비트 문구가 있을경우
                    Java32bit = true; // 32비트 자바로 판정
            }
            else // 32비트 운영체제일경우,
                Java32bit = true; // 필요없고 그냥 32비트

            if (Java32bit)
                if (game_sdJAVA.Value > 1)
                    if (WPFCom.Message("32비트 자바에서는 메모리 할당량 1GB 초과시 에러가 발생하며 실행되지 않습니다." + Environment.NewLine + "그래도 저장하시겠습니까?", "Bell Smart Launcher", MessageBoxButton.YesNo) == MessageBoxResult.No)
                        return false;

            Game.Memory_Allocate = game_sdJAVA.Value;
            Game.JAVA_Path = game_txtJAVAPath.Text;
            Game.JAVA_Parameter = game_txtParameter.Text;
            Game.MultipleExe = (bool)game_cbMultipleExe.IsChecked;

            BD.Data.DataSave(GameSettingPath, "Memory_Allocate", Game.Memory_Allocate.ToString());
            BD.Data.DataSave(GameSettingPath, "JAVA_Path", Game.JAVA_Path);
            BD.Data.DataSave(GameSettingPath, "JAVA_Parameter", Game.JAVA_Parameter);
            BD.Data.DataSave(GameSettingPath, "MultipleExe", Game.MultipleExe.ToString());

            SettingLoad(2);

            return true;
        }

        public bool SaveDebug()
        {
            DebugCategory.PWV = (bool)deb_cbPWV.IsChecked;

            BD.Data.DataSave(DebugSettingPath, "PWV", DebugCategory.PWV.ToString());

            SettingLoad(4);

            return true;
        }

        private void game_sdJAVA_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!game_sdJAVA.IsInitialized)
                return;
            game_lbRAM.Content = game_sdJAVA.Value + " GB";
        }

        private void gen_btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveGeneral();
            WPFCom.Message("일반설정 저장에 성공하였습니다.");
        }

        private void game_btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveGame();
            WPFCom.Message("게임설정 저장에 성공하였습니다.");
        }

        private void deb_btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveDebug();
            WPFCom.Message("디버그설정 저장에 성공하였습니다.");
        }
    }
}
