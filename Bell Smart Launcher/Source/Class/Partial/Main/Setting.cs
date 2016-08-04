using Bell_Smart_Launcher.Source.Data;
using BellLib.Class;
using BellLib.Class.BSN;
using BellLib.Class.Protection;
using BellLib.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Bell_Smart_Launcher.Source.Frame
{
    /// <summary>
    /// Main 창의 Setting 탭 분할클래스 입니다.
    /// </summary>
    public partial class Main
    {
        #region *** INITIALIZE ***

        /// <summary>
        /// 세팅탭 관련 기능을 초기화합니다.
        /// </summary>
        public void InitSetting()
        {
            SystemInfo.MemoryInfo mi = SystemInfo.GetMemoryInfo();

            gen_cbLanguage.Items.Clear();
            gen_cbLanguage.Items.Add("한국어");
            gen_cbLanguage.Items.Add("English");
            gen_cbLanguage.SelectedIndex = 0;

            game_sdJAVA.Maximum = mi.Total_Physical_GB;

            gen_lbBSLVer.Content = Deploy.CurrentVersion.ToString();

            gen_cbSkin.Items.Clear();
            gen_cbSkin.Items.Add("밝은 파랑");
            gen_cbSkin.Items.Add("파랑");
            gen_cbSkin.Items.Add("검정 큐브");
            gen_cbSkin.Items.Add("무광 파랑");
            gen_cbSkin.Items.Add("광 파랑");
            gen_cbSkin.Items.Add("파란 유리");
            gen_cbSkin.SelectedIndex = 0;

            SettingLoad(); // 세팅값 로드
            AutoControl(); // 첫 실행시 자동 세팅
        }

        /// <summary>
        /// 세팅값을 불러옵니다.
        /// </summary>
        /// <param name="tab">불러올 세팅 탭</param>
        private void SettingLoad(int tab = 7) // 0111
        {
            if ((tab & 1) == 1) // 0001 일반
            {
                gen_txtInstall.Text = Game.BSL_Root;
                if (Game.Language != null)
                    gen_cbLanguage.SelectedItem = Game.Language;
                gen_cbConsole.IsChecked = Game.ConsoleRun;
                gen_cbKeep.IsChecked = Game.KeepOpen;
                gen_cbAutoControl.IsChecked = Game.AutoControl;
                gen_cbDebugMode.IsChecked = Game.DebugMode;
                if (Game.Skin != null)
                    gen_cbSkin.SelectedItem = Game.Skin;

                string skinFile = "Tile_WhiteBlue.png";
                SolidColorBrush color = new SolidColorBrush(Colors.Black);
                switch (Game.Skin)
                {
                    case "밝은 파랑":
                        skinFile = "Tile_WhiteBlue.png";
                        break;

                    case "파랑":
                        skinFile = "Tile_Blue.png";
                        break;

                    case "검정 큐브":
                        skinFile = "Tile_BlackCube.jpg";
                        color = new SolidColorBrush(Colors.Magenta);
                        break;

                    case "무광 파랑":
                        skinFile = "Tile_NoLightBlue.png";
                        break;

                    case "광 파랑":
                        skinFile = "Tile_LightBlue.png";
                        break;
                        
                    case "파란 유리":
                        skinFile = "Tile_BlueGlass.png";
                        break;
                }
                this.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Bell Smart Launcher;component/Resource/Photo/" + skinFile)));
                ti_NewsFeed.Foreground = color;
                ti_Modpacks.Foreground = color;
                ti_Resources.Foreground = color;
                ti_Maps.Foreground = color;
                ti_Setting.Foreground = color;
            }

            if ((tab & 2) == 2) // 0010 게임
            {
                game_sdJAVA.Value = Game.Memory_Allocate;
                game_txtJAVAPath.Text = Game.JAVA_Path;
                game_txtParameter.Text = Game.JAVA_Parameter;
                game_cbMultipleExe.IsChecked = Game.MultipleExe;

                if (game_txtJAVAPath.Text == string.Empty)
                    game_txtJAVAPath.Text = User.BSN_Path + @"Runtime\Java\x64"; // 임시 기본값 설정
            }

            // Default
            if (Game.DebugMode)
                gen_btnDebugger.Visibility = Visibility.Visible;
            else
                gen_btnDebugger.Visibility = Visibility.Collapsed;
        }

        #endregion

        #region *** CONTROL ***

        /// <summary>
        /// 런처 처음실행시 사용자 환경에 맞게 세팅 진행
        /// </summary>
        private void AutoControl()
        {
            string newbie = DataProtect.DataLoad(DataPath.BSL.General, "AutoControl");
            if (newbie != null)
                return;

            string runtimePath = Game.BSL_Root + "Runtime\\Java\\";

            if (WPFCom.Message("자동제어기능을 이용하면 복잡한 설정 없이 간편하게 이용할 수 있습니다." + Environment.NewLine + "런처 자동제어기능을 이용하시겠습니까?", Base.PROJECT.Bell_Smart_Launcher, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            { // 자동제어 사용
                // 기본값 자동설정을 위해
                SystemInfo.MemoryInfo mi = SystemInfo.GetMemoryInfo();
                int Allocate = 1; // 메모리 기본 할당량 1기가
                if (mi.Total_Physical_GB >= 2) // 전체 메모리가 2기가 이상일경우,
                    Allocate = 2; // 일단 메모리 2기가 할당
                if (mi.Total_Physical_GB > 4) // 전체 메모리가 4기가 초과일경우,
                    if (mi.Free_Physical_GB > mi.Total_Physical_GB / 2) // 사용가능한 메모리가 전체 메모리의 절반보다 크다면,
                        Allocate = mi.Free_Physical_GB; // 사용가능한 메모리만큼 할당
                    else
                        Allocate = mi.Total_Physical_GB / 2; // 아니면 전체메모리의 절반만 할당
                if (mi.Free_Physical_GB >= 6) // 사용가능 메모리가 8기가 이상일경우,
                    Allocate = 4;
                if (mi.Free_Physical_GB >= 14) // 사용가능 메모리가 14기가 이상일경우,
                    Allocate = 8;

                Game.AutoControl = true;
                Game.Memory_Allocate = Allocate;

                SaveGeneral(); // 일반설정 저장
                SaveGame(); // 게임설정 저장

                if (!Directory.Exists(runtimePath))
                    InstallJava(runtimePath);
            }
            else
            { // 수동제어 사용
                Game.AutoControl = false;
                if (!Directory.Exists(runtimePath))
                    if (WPFCom.Message("자바 런타임팩을 설치하면 간편하게 게임제어가 가능합니다." + Environment.NewLine + "설치하시겠습니까?", Base.PROJECT.Bell_Smart_Launcher, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        InstallJava(runtimePath);
                    else
                    {
                        if (WPFCom.Message("런타임자바 미 설치시 자바경로를 수동으로 설정해야합니다." + Environment.NewLine + "자바 경로설정창으로 이동하시겠습니까?", Base.PROJECT.Bell_Smart_Launcher, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            if (WPFCom.Feasibility("Bell_Smart_Launcher.Source.Frame.Setting"))
                                tc_Main.SelectedIndex = 4;
                    }
            }

            DataProtect.DataSave(DataPath.BSL.General, "AutoControl", Game.AutoControl.ToString());
        }

        /// <summary>
        /// 런타임 자바팩을 설치합니다.
        /// </summary>
        /// <param name="runtimePath">런타임 자바팩 설치경로</param>
        private void InstallJava(string runtimePath)
        {
            Installer install = new Installer("Java"); // 설치기 초기화
            install.Show(); // 설치기 실행
            install.Install(runtimePath); // 설치 시작

            if (Environment.Is64BitOperatingSystem)
                Game.JAVA_Path = runtimePath + "x64";
            else
                Game.JAVA_Path = runtimePath + "x86";
        }

        /// <summary>
        /// 런타임 자바팩 무결성검증을 시행합니다.
        /// </summary>
        /// <param name="AutoControl">자동제어 사용여부 (사용시 메시지박스 출력 안함)</param>
        /// <returns>정상설치 여부</returns>
        private bool JavaIntegrityCheck(bool AutoControl = false)
        {
            if (Game.JAVA_Path.ToUpper().Contains((Game.BSL_Root + "Runtime\\Java\\").ToUpper()))
            {
                string runtimeVerid = null;
                string runtimeName = "Java";
                Protect pro = new Protect();
                List<string> failFile = new List<string>();
                BSN_BSL.Runtime runtime = BSN_BSL.LoadRuntimeDetail(runtimeName); // 런타임 이름으로 상세정보 검색

                foreach (string value in BSN_BSL.LoadPackVersionList(BSN_BSL.PACK.runtime, runtimeName))
                { // 팩 버전리스트 검색
                    if (runtime.recommended == Common.getElement(value, "version")) // 루프를 돌다가 권장버전이 나오면
                        runtimeVerid = Common.getElement(value, "id"); // 런타임 버전 id를 권장버전 id로 설정.
                }
                BSN_BSL.Install[] runtimeInstall = BSN_BSL.LoadVersionFiles(BSN_BSL.PACK.runtime, BSN_BSL.KIND.client, runtimeVerid);


                foreach (BSN_BSL.Install value in runtimeInstall)
                {
                    string localURL = Game.JAVA_Path.Remove(Game.JAVA_Path.Length - 3) + value.url;
                    if (value.hash != pro.MD5Hash(localURL))
                        failFile.Add(value.url);
                }

                if (failFile.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (string value in failFile)
                    {
                        if (value == failFile[failFile.Count - 1])
                            sb.Append(value);
                        else
                            sb.Append(value + ", ");
                    }
                    if (!AutoControl)
                    {
                        WPFCom.Message("런타임팩에 문제가 발생하였습니다." + Environment.NewLine + "설치되지 않았거나 변경된 파일 : " + Environment.NewLine + sb, Base.PROJECT.Bell_Smart_Launcher);
                        if (WPFCom.Message("런타임 자바를 재설치 하시겠습니까?", Base.PROJECT.Bell_Smart_Launcher, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            InstallJava(Game.JAVA_Path);
                    }
                    else
                        InstallJava(Game.JAVA_Path);

                    return false; // 결점 확인 비정상 설치로 반환
                }

                return true; // 결점 없음 정상설치
            }
            else
                return false; // 런타임팩 아님
        }

        #endregion

        #region *** DATA ***

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
            Game.Skin = (string)gen_cbSkin.SelectedItem;

            DataProtect.DataSave(DataPath.BSL.General, "BSL_Root", Game.BSL_Root);
            DataProtect.DataSave(DataPath.BSL.General, "Laungage", Game.Language);
            DataProtect.DataSave(DataPath.BSL.General, "ConsoleRun", Game.ConsoleRun.ToString());
            DataProtect.DataSave(DataPath.BSL.General, "KeepOpen", Game.KeepOpen.ToString());
            DataProtect.DataSave(DataPath.BSL.General, "AutoControl", Game.AutoControl.ToString());
            DataProtect.DataSave(DataPath.BSL.General, "DebugMode", Game.DebugMode.ToString());
            DataProtect.DataSave(DataPath.BSL.General, "Skin", Game.Skin);

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
                    if (WPFCom.Message("32비트 자바에서는 메모리 할당량 1GB 초과시 에러가 발생하며 실행되지 않습니다." + Environment.NewLine + "그래도 저장하시겠습니까?", Base.PROJECT.Bell_Smart_Launcher, MessageBoxButton.YesNo) == MessageBoxResult.No)
                        return false;

            Game.Memory_Allocate = game_sdJAVA.Value;
            Game.JAVA_Path = game_txtJAVAPath.Text;
            Game.JAVA_Parameter = game_txtParameter.Text;
            Game.MultipleExe = (bool)game_cbMultipleExe.IsChecked;

            DataProtect.DataSave(DataPath.BSL.Game_Setting, "Memory_Allocate", Game.Memory_Allocate.ToString());
            DataProtect.DataSave(DataPath.BSL.Game_Setting, "JAVA_Path", Game.JAVA_Path);
            DataProtect.DataSave(DataPath.BSL.Game_Setting, "JAVA_Parameter", Game.JAVA_Parameter);
            DataProtect.DataSave(DataPath.BSL.Game_Setting, "MultipleExe", Game.MultipleExe.ToString());

            SettingLoad(2);

            return true;
        }

        #endregion

        #region *** FORM ***

        /// <summary>
        /// 할당한 자바 용량이 몇GB인지 보여줍니다.
        /// </summary>
        private void game_sdJAVA_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!game_sdJAVA.IsInitialized)
                return;
            game_lbRAM.Content = game_sdJAVA.Value + " GB";
        }

        /// <summary>
        /// 일반 설정 저장
        /// </summary>
        private void gen_btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveGeneral();
            WPFCom.Message("일반설정 저장에 성공하였습니다.", Base.PROJECT.Bell_Smart_Launcher);
        }

        /// <summary>
        /// 게임 설정 저장
        /// </summary>
        private void game_btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveGame();
            WPFCom.Message("게임설정 저장에 성공하였습니다.", Base.PROJECT.Bell_Smart_Launcher);
        }

        /// <summary>
        /// 자바 유효성 검증을 시행합니다.
        /// </summary>
        private void game_btnJAVAIntegrity_Click(object sender, RoutedEventArgs e)
        {
            if (Game.JAVA_Path.ToUpper().Contains((Game.BSL_Root + "Runtime\\Java\\").ToUpper()))
            {
                Task<bool> JavaCheck = Task<bool>.Factory.StartNew(() => JavaIntegrityCheck());
                while (!JavaCheck.Wait(1))
                    Common.DoEvents();

                if (JavaCheck.Result)
                    WPFCom.Message("런타임팩이 정상적으로 설치되어있습니다.", Base.PROJECT.Bell_Smart_Launcher);
            }
            else
                WPFCom.Message("런타임 자바팩이 아닌 외부 자바는 무결성 체크를 할 수 없습니다.", Base.PROJECT.Bell_Smart_Launcher);
        }

        private void gen_btnDebugger_Click(object sender, RoutedEventArgs e)
        {
            if (WPFCom.Feasibility("Bell_Smart_Launcher.Source.Frame.Debugger"))
            {
                Debugger deb = new Debugger();
                deb.Show(); // 세팅탭 오픈
            }
        }

        private void gen_btnCancel_Click(object sender, RoutedEventArgs e)
        {
            SettingLoad(1);
        }

        private void game_btnCancel_Click(object sender, RoutedEventArgs e)
        {
            SettingLoad(2);
        }

        private void gen_btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "게임 설치폴더 검색";
            dialog.SelectedPath = Game.BSL_Root;

            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result != System.Windows.Forms.DialogResult.OK)
                return;
            
            gen_txtInstall.Text = dialog.SelectedPath + "\\";
        }

        private void gen_btnSkinRemove_Click(object sender, RoutedEventArgs e)
        {

        }

        private void gen_cbSkin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!gen_cbSkin.IsInitialized)
                return;

            if (gen_cbSkin.SelectedIndex < 7) // 기본스킨 삭제불가
                gen_btnSkinRemove.IsEnabled = false;
            else
                gen_btnSkinRemove.IsEnabled = true;
        }

        #endregion
    }
}
