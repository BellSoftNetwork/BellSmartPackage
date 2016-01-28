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

        public Setting()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            gen_cbLanguage.Items.Clear();
            gen_cbLanguage.Items.Add("한국어");
            gen_cbLanguage.Items.Add("English");
            gen_cbLanguage.SelectedIndex = 0;

            SettingLoad();
        }

        /// <summary>
        /// 세팅값을 로드합니다.
        /// </summary>
        private void SettingLoad()
        {
            gen_txtInstall.Text = Game.BSL_Root;
            if (Game.Language != null)
                gen_cbLanguage.SelectedItem = Game.Language;
            gen_cbConsole.IsChecked = Game.ConsoleRun;
            gen_cbKeep.IsChecked = Game.KeepOpen;

            game_sdJAVA.Value = Game.Memory_Allocate;
            game_txtJAVAPath.Text = Game.JAVA_Path;
            game_txtParameter.Text = Game.JAVA_Parameter;

            if (game_txtJAVAPath.Text == string.Empty)
                game_txtJAVAPath.Text = User.BSN_Path + @"Runtime\JAVA\x64"; // 임시 기본값 설정
        }
        
        private void game_sdJAVA_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!game_sdJAVA.IsInitialized)
                return;
            game_lbRAM.Content = game_sdJAVA.Value + " GB";
        }

        private void gen_btnSave_Click(object sender, RoutedEventArgs e)
        {
            Game.BSL_Root = gen_txtInstall.Text;
            Game.Language = (string)gen_cbLanguage.SelectedItem;
            Game.ConsoleRun = (bool)gen_cbConsole.IsChecked;
            Game.KeepOpen = (bool)gen_cbKeep.IsChecked;

            BD.Data.DataSave(GeneralSettingPath, "BSL_Root", Game.BSL_Root);
            BD.Data.DataSave(GeneralSettingPath, "Laungage", Game.Language);
            BD.Data.DataSave(GeneralSettingPath, "ConsoleRun", Game.ConsoleRun.ToString());
            BD.Data.DataSave(GeneralSettingPath, "KeepOpen", Game.KeepOpen.ToString());
            WPFCom.Message("일반설정 저장에 성공하였습니다.");
        }

        private void game_btnSave_Click(object sender, RoutedEventArgs e)
        {
            Game.Memory_Allocate = game_sdJAVA.Value;
            Game.JAVA_Path = game_txtJAVAPath.Text;
            Game.JAVA_Parameter = game_txtParameter.Text;

            BD.Data.DataSave(GameSettingPath, "Memory_Allocate", Game.Memory_Allocate.ToString());
            BD.Data.DataSave(GameSettingPath, "JAVA_Path", Game.JAVA_Path);
            BD.Data.DataSave(GameSettingPath, "JAVA_Parameter", Game.JAVA_Parameter);
            WPFCom.Message("게임설정 저장에 성공하였습니다.");
        }
    }
}
