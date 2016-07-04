using BellLib.Class;
using BD = BellLib.Class.Analysis;
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
using BellLib.Class.BSN;
using BellLib.Data;
using System.IO;
using System.Net;
using System.Diagnostics;
using Bell_Smart_Launcher.Source.Data;
using System.Reflection;

namespace Bell_Smart_Launcher.Source.Frame
{
    /// <summary>
    /// BSL_Main.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Main : Window
    {
        // 필드
        //private int LastSelectedTab;
        private string ModpacksDataPath = User.BSN_Path + "DATA\\BSL\\Modpacks.bdx";
        private string ResourcesDataPath = User.BSN_Path + "DATA\\BSL\\Resources.bdx";
        private Process GameProcess = null;
        private bool noticeLock = true;

        public Main()
        {
            InitializeComponent();
            PreInitialize();
        }

        /// <summary>
        /// 런처창을 보여주기 전에 먼저 1회 초기화합니다.
        /// </summary>
        private void PreInitialize()
        {
            //Common
            tc_Main.SelectedIndex = 1; // 마지막에 열었던 탭 활성화

            //NEWS


            //MODPACKS
            mod_lstPackList.Items.Clear(); // 팩 리스트 초기화!
            mod_lstDetailList.Items.Clear(); // 팩 상세정보 초기화
            mod_cbProfile.Items.Clear(); // 프로필 리스트 초기화
            mod_cbVersion.Items.Clear(); // 팩 버전 리스트 초기화
            mod_expanderDetail.IsExpanded = false;

            //MAPS


            //RESOURCES


            //SETTING

        }

        /// <summary>
        /// 런처창이 로드된 후 사용할 수 있게 초기화합니다.
        /// </summary>
        private void Initialize()
        {
            //Common
            List<string> BasicPlan = new List<string>();
            List<string> PremiumPlan = new List<string>();
            List<string> PartnerPlan = new List<string>();
            List<string> BSN_SpecialPlan = new List<string>();


            //NEWS


            //MODPACKS
            //초기화
            BasicPlan.Clear();
            PremiumPlan.Clear();
            PartnerPlan.Clear();
            BSN_SpecialPlan.Clear();

            // 요금제별 분류
            foreach (string value in BSN_BSL.LoadPackList(BSN_BSL.PACK.modpack))
            {
                switch (Common.getElement(value, "plan"))
                {
                    case "0":
                        BasicPlan.Add(value);
                        break;

                    case "1":
                        PremiumPlan.Add(value);
                        break;

                    case "2":
                        PartnerPlan.Add(value);
                        break;

                    case "10":
                        BSN_SpecialPlan.Add(value);
                        break;

                    default:
                        BasicPlan.Add(value);
                        break;
                }
            }

            // like별 분류

            // 최종 리스트 출력
            object[] plans = { BSN_SpecialPlan, PartnerPlan, PremiumPlan, BasicPlan };
            foreach (List<string> plan in plans)
                foreach (string value in plan)
                    mod_lstPackList.Items.Add(Common.getElement(value, "name"));

            mod_lstPackList.SelectedItem = BD.Data.DataLoad(ModpacksDataPath, "Modpack"); // 마지막에 선택했던 팩 자동선택
            if (mod_lstPackList.SelectedIndex == -1)
                mod_lstPackList.SelectedIndex = 0;
            Mod_Expand(mod_expanderDetail.IsExpanded); // 모드탭 익스펜더 설정
            mod_cbProfile.Items.Add("Select Profile");
            mod_cbProfile.Items.Add("Create Profile");
            //mod_cbProfile.SelectedIndex = 0;
            /*mod_cbVersion.Items.Add("Recommended");
            mod_cbVersion.Items.Add("Latest");
            mod_cbVersion.SelectedIndex = 0;*/
            ProfileLoad(); // 프로필 리스트 로드

            //MAPS


            //RESOURCES


            //SETTING
            AutoControl(); // 첫 실행시 자동 세팅
        }

        /// <summary>
        /// 런처 처음실행시 사용자 환경에 맞게 세팅 진행
        /// </summary>
        private void AutoControl()
        {
            /*if (File.Exists(User.BSN_Path + "DATA\\BSL\\General.bdx"))
                return;*/
            string newbie = BD.Data.DataLoad(User.BSN_Path + "DATA\\BSL\\General.bdx", "AutoControl");
            if (newbie != null)
                return;

            string runtimePath = Game.BSL_Root + "Runtime\\Java\\";

            if (WPFCom.Message("자동제어기능을 이용하면 복잡한 설정 없이 간편하게 이용할 수 있습니다." + Environment.NewLine + "런처 자동제어기능을 이용하시겠습니까?", "Bell Smart Launcher", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
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
                                
                Setting set = new Setting();
                set.SaveGeneral(); // 일반설정 저장
                set.SaveGame(); // 게임설정 저장
                set.Close(); // 끝났으면 닫아야지.

                if (!Directory.Exists(runtimePath))
                    InstallJava(runtimePath);
            }
            else
            { // 수동제어 사용
                Game.AutoControl = false;
                if (!Directory.Exists(runtimePath))
                    if (WPFCom.Message("자바 런타임팩을 설치하면 간편하게 게임제어가 가능합니다." + Environment.NewLine + "설치하시겠습니까?", "Bell Smart Launcher", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        InstallJava(runtimePath);
                    else
                    {
                        if (WPFCom.Message("런타임자바 미 설치시 자바경로를 수동으로 설정해야합니다." + Environment.NewLine + "자바 경로설정창으로 이동하시겠습니까?", "Bell Smart Launcher", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            if (WPFCom.Feasibility("Bell_Smart_Launcher.Source.Frame.Setting"))
                            {
                                Setting set = new Setting();
                                set.tc_Setting.SelectedIndex = 1;
                                set.Show(); // 세팅탭 오픈
                            }
                    }
            }

            BD.Data.DataSave(User.BSN_Path + "DATA\\BSL\\General.bdx", "AutoControl", Game.AutoControl.ToString());
        }

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
        /// 프로필 데이터를 로드합니다.
        /// </summary>
        private void ProfileLoad()
        {
            mod_cbProfile.Items.Clear(); // 프로필 리스트 초기화
            string[] Default = { "프로필 선택", "프로필 생성" };
            foreach (string value in Default)
                mod_cbProfile.Items.Add(value); // 기본값 추가
            //mod_cbProfile.SelectedIndex = 0; // 일단 프로필 선택으로 맞춰둠 (기본값)
            string DefaultPath = User.BSN_Path + "Data\\BSL\\Profile\\"; // 프로필파일 기본 경로
            try
            {
                string[] ProfileList = Directory.GetFiles(DefaultPath, "*.bdx"); // .bd 파일 리스트를 불러옴.
                foreach (string tmp in ProfileList)
                    mod_cbProfile.Items.Add(tmp.Replace(DefaultPath, string.Empty).Replace(".bdx", string.Empty)); // 프로필 파일을 전부 로드함.
                mod_cbProfile.SelectedItem = BD.Data.DataLoad(ModpacksDataPath, "Profile");
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(DefaultPath);
            }
            if (mod_cbProfile.SelectedIndex == -1)
                mod_cbProfile.SelectedIndex = 0;
        }

        private void Mod_Expand(bool expand)
        {
            if (expand)
            { // 활성화
                mod_lstDetailList.Visibility = Visibility.Visible;
                mod_wbNotice.Height = 280;
            }
            else
            { // 비활성화
                mod_lstDetailList.Visibility = Visibility.Hidden;
                mod_wbNotice.Height = 350;
            }
        }

        /// <summary>
        /// 모드팩을 실행합니다.
        /// </summary>
        /// <param name="PathBase">베이스팩 경로</param>
        /// <param name="PathPack">모드팩 경로</param>
        private void Launch(string Version, string PathBase, string PathPack, string PathJAVA, string Parameter, string NickName, string UUID, string AccessToken, string UserParameter)
        {
            bool javaNotFound = false;
            if (Version == string.Empty || PathBase == string.Empty || PathPack == string.Empty || UUID == string.Empty || AccessToken == string.Empty)
            {
                WinCom.Message("게임 실행 중 매개변수값이 정상적으로 전달되지 않아 실행을 중단합니다.");
                return;
            }
            string strTemp;
            StringBuilder sb = new StringBuilder(1024); //기본 문자열을 JAVA 변수, 기본 캐피시터를 1024로 하여 StringBuilder 선언.

            sb.Append(UserParameter);

            sb.Append(" -Djava.library.path=");
            sb.Append(PathBase);
            sb.Append("natives");

            sb.Append(" -cp ");
            sb.Append(PathBase);
            sb.Append("*");

            sb.Append(" net.minecraft.launchwrapper.Launch ");

            sb.Append(BSN_BSL.ReplaceParameter(Parameter, NickName, Version, PathPack, PathBase, UUID, AccessToken));
            /*sb.Append(" --username ");
            sb.Append(NickName);

            sb.Append(" --version ");
            sb.Append(Version);

            sb.Append(" --gameDir ");
            sb.Append(PathPack);

            sb.Append(" --assetsDir ");
            sb.Append(PathBase);
            sb.Append("assets");

            sb.Append(" --assetIndex ");
            sb.Append("BSN");

            sb.Append(" --uuid ");
            sb.Append(UUID);

            sb.Append(" --accessToken ");
            sb.Append(AccessToken);

            sb.Append(" --userProperties {} --userType mojang --tweakClass cpw.mods.fml.common.launcher.FMLTweaker");*/

            strTemp = sb.ToString();
            try
            {
                Directory.SetCurrentDirectory(PathPack); //런처 실행경로를 방울크래프트 클라이언트 경로로 수정.
                GameProcess = new Process();
                if (!Game.ConsoleRun)
                    GameProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                else
                    GameProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                //GameProcess = Process.Start(PathJAVA, strTemp);
                GameProcess.StartInfo.FileName = PathJAVA;
                GameProcess.StartInfo.Arguments = strTemp;
                GameProcess.StartInfo.WorkingDirectory = PathPack;
                GameProcess.Start();
            }
            catch (FileNotFoundException fnf)
            {
                BellLib.Class.Debug.Message(BellLib.Class.Debug.Level.High, fnf.Message);
                javaNotFound = true;
            }
            catch (System.ComponentModel.Win32Exception we)
            {
                BellLib.Class.Debug.Message(BellLib.Class.Debug.Level.High, we.Message);
                javaNotFound = true;
            }
            catch (Exception ex)
            {
                WPFCom.Message(ex.Message);
                /*BC_PID = -1;
                BST_Manager.Message("방울크래프트 실행 중 문제가 발생하였습니다." + Constants.vbCrLf + "자바 경로가 정상적으로 설정되어있는지 확인하시기 바랍니다." + Constants.vbCrLf + Constants.vbCrLf + ex.Message);
                BC_Button(false);*/
            }

            if (javaNotFound)
                if (WPFCom.Message("자바 경로가 비 정상적으로 설정되었습니다." + Environment.NewLine + "자바 경로 설정화면으로 이동하시겠습니까?", "Bell Smart Launcher", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    if (WPFCom.Feasibility("Bell_Smart_Launcher.Source.Frame.Setting"))
                    {
                        Setting set = new Setting();
                        set.tc_Setting.SelectedIndex = 1;
                        set.Show(); // 세팅탭 오픈
                    }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Initialize();
        }
        private void mod_expanderDetail_Expanded(object sender, RoutedEventArgs e)
        { // 익스펜더 열음
            Mod_Expand(true);
        }

        private void mod_expanderDetail_Collapsed(object sender, RoutedEventArgs e)
        { // 익스펜더 접음
            Mod_Expand(false);
        }

        private void mod_cbProfile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mod_cbProfile.SelectedIndex == 1)
            {
                mod_cbProfile.SelectedIndex = 0;
                Profile Pro = new Profile();
                Pro.ShowDialog();
                ProfileLoad(); // 값이 바뀌었을테니 프로필 다시 로드!
                if (Pro.getData(Profile.Data.Name) != null) // 프로필 이름이 null이 아니라면, (프로필을 정상적으로 생성했다면,
                    mod_cbProfile.SelectedItem = Pro.getData(Profile.Data.Name); // 방금 생성한 따끈따끈한 프로필파일을 선택
            }
            if (mod_cbProfile.IsInitialized && mod_cbProfile.SelectedIndex > -1)
                BD.Data.DataSave(ModpacksDataPath, "Profile", (string)mod_cbProfile.SelectedItem); // 선택 프로필이 바뀌었으므로 설정값 저장!
        }

        private void mod_btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (mod_cbProfile.SelectedIndex < 2)
                return;
            Profile pro = new Profile((string)mod_cbProfile.SelectedItem);
            pro.ShowDialog();
            ProfileLoad(); // 값이 바뀌었을테니 프로필 다시 로드!
        }

        private void mod_lstPackList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 예외처리
            if (mod_lstPackList.SelectedIndex < 0)
                return;

            // 초기화
            mod_cbVersion.Items.Clear();
            mod_cbVersion.Items.Add("Latest");
            mod_cbVersion.Items.Add("Recommended");

            // 데이터 로드
            foreach (string value in BSN_BSL.LoadPackVersionList(BSN_BSL.PACK.modpack, (string)mod_lstPackList.SelectedItem))
                mod_cbVersion.Items.Add(Common.getElement(value, "version"));

            // 데이터 설정
            mod_cbVersion.SelectedIndex = 1;
            BD.Data.DataSave(ModpacksDataPath, "Modpack", (string)mod_lstPackList.SelectedItem); // 선택 모드팩이 바뀌었으므로 설정값 저장!

            // 공지사항 출력
            BSN_BSL.ModPack mp = BSN_BSL.LoadModPackDetail((string)mod_lstPackList.SelectedItem);
            noticeLock = false;
            try
            {
                string strNotice = Common.getStringFromWeb(mp.notice, Encoding.UTF8);
                string[] strTemp = Common.stringSplit(strNotice, "<article");
                strNotice = strTemp[1];
                strTemp = Common.stringSplit(strNotice, "article>");
                strNotice = "<meta charset=\"utf-8\"><article" + strTemp[0] + "article>";
                
                mod_wbNotice.NavigateToString(strNotice);
            }
            catch
            {
                // 공지사항 로드 에러
                mod_wbNotice.NavigateToString("<meta charset=\"utf-8\"><strong abp=\"4668\"><span style=\"font-family: 돋움; \"abp=\"4670\"><font color=\"#ff0000\">공지사항이 존재하지 않거나 불러오는 중 문제가 발생하였습니다.</font></span></strong>");
            }

            // 좋아요 정보 로드
            mod_lstPackList.Tag = mp.id; // 태그에 팩 id 저장
            mod_LoadLike();
        }

        /// <summary>
        /// 좋아요 정보를 로드합니다.
        /// </summary>
        private void mod_LoadLike()
        {
            if (BSN_BSL.isLikedPack(BSN_BSL.PACK.modpack, (string)mod_lstPackList.Tag))
                mod_btnLike.Content = "♥";
            else
                mod_btnLike.Content = "♡";
        }

        private void mod_btnEnjoy_Click(object sender, RoutedEventArgs e)
        {
            // 필드
            string modName = (string)mod_lstPackList.SelectedItem;
            string modVer = (string)mod_cbVersion.SelectedItem;
            string modVerid = null;
            string baseVerid = null;
            bool installBase = false;
            bool installMod = false;

            // 필드 유효성 검증
            if (mod_cbProfile.SelectedIndex < 2)
            {
                WPFCom.Message("실행할 프로필을 선택해주세요.");
                return;
            }
            if (mod_lstPackList.SelectedIndex < 0)
            {
                WPFCom.Message("실행할 모드팩을 선택해주세요.");
                return;
            }

            /// 선택한 모드팩 상세정보 로드
            // 상세 정보 로드
            string[] verList = BSN_BSL.LoadPackVersionList(BSN_BSL.PACK.modpack, modName, BSN_BSL.STATE.ACTIVATE); // 모드팩 버전 리스트
            BSN_BSL.ModPack mp = BSN_BSL.LoadModPackDetail(modName); // 모드팩 정보 로드

            // 버전정보 검증
            if (modVer == "Recommended") // 권장버전을 선택했을경우,
                modVer = mp.recommended; // 공식 권장버전을 대입
            foreach (string verData in verList)
            {
                if (modVer == "Lastest") // 선택한 버전이 최신버전일경우,
                    if (modVerid == null) // 버전id 설정이 안되어있을경우 (foreach 처음 진입일경우)
                        modVer = Common.getElement(verData, "version"); // 최신버전값을 넣어준다.
                if (modVer == Common.getElement(verData, "version")) // 루프를 돌다가 선택버전과 서버버전이 일치할경우,
                    modVerid = Common.getElement(verData, ("id")); // 해당 버전 id를 로드한다.
            }

            // 데이터 유효성 검증
            if (modVerid == null) // 예상치 못한 오류로 모드 버전 id를 받지 못하였을경우 실행 중단
            {
                WPFCom.Message("버전정보를 가져오는데 실패하였습니다.");
                return;
            }

            // 선행 로드가 끝난 후 추가정보 로드
            string modVerData = BSN_BSL.LoadVersionDetail(BSN_BSL.PACK.modpack, modVerid); // 모드팩 버전 상세정보 로드
            baseVerid = Common.getElement(modVerData, "basevid"); // 베이스팩 버전id
            string basePath = Game.BSL_Root + "Base\\" + baseVerid + "\\";
            string modPath = Game.BSL_Root + "ModPack\\" + modVerid + "\\";
            string baseVerData = BSN_BSL.LoadVersionDetail(BSN_BSL.PACK.basepack, baseVerid); // 베이스팩 버전 상세정보 로드
            string parameter = Common.getElement(baseVerData, "parameter"); // 베이스팩 파라메터 로드

            /// 클라이언트 유효성 검증
            // 베이스팩 설치유무확인
            if (!Directory.Exists(basePath))
                installBase = true;

            // 모드팩 설치유무확인
            if (!Directory.Exists(modPath))
                installMod = true;

            // 미설치시 설치시작
            if (installBase || installMod)
            {
                Installer install = new Installer(modName, modVer, modVerid, baseVerid); // 설치기 초기화
                install.Show(); // 설치기 실행
                install.Install(installBase, installMod, basePath, modPath); // 설치 시작
            }

            /// 게임 실행
            // 다중실행 허용여부 검사
            if (!Game.MultipleExe)
            { // 다중실행 비허용
                if (GameProcess != null)
                    if (!GameProcess.HasExited)
                    { // 게임이 실행중이라면,
                        WPFCom.Message("이미 게임이 실행중입니다.");
                        return;
                    }
            }

            // 계정 로그인
            Profile profile = new Profile((string)mod_cbProfile.SelectedItem); // 선택한 프로필로 데이터를 초기화함.
            string MC_ID = profile.getData(Profile.Data.ID);
            string MC_PW = profile.getData(Profile.Data.PW);

            if (MC_ID != string.Empty) // && User.MC_PW != null) // 레지스트리에 MC 계정정보가 저장되어있으면 로그인 실행
            {
                if (MC_PW == string.Empty)
                {
                    Password pass = new Password();
                    pass.ShowDialog();
                    MC_PW = pass.getPassword();
                }

                //SetState("마인크래프트 계정 로그인 시도중");
                MCLogin MCL = new MCLogin();
                if (MCL.Login(MC_ID, MC_PW, MCLogin.LoginType.Authenticate))
                {
                    //SetState("마인크래프트 계정 로그인 성공");
                }
                else
                {
                    WPFCom.Message("마인크래프트 계정 로그인에 실패하였습니다. 아이디 또는 비밀번호를 확인해주세요.");
                    //SetState("마인크래프트 계정 로그인에 실패하였습니다. 아이디 또는 비밀번호를 확인해주세요.");
                    //btn_Launch.Enabled = true;
                    return;
                }
                MCLogin.MC_Account MCA = MCL.GetLoginData();
                string MemoryParameter = "-Xmx" + (Game.Memory_Allocate * 1024) + "M ";
                Launch(modVer, basePath, modPath, Game.JAVA_Path + @"\bin\java.exe", parameter, MCA.MC_NickName, MCA.MC_UUID, MCA.MC_AccessToken, MemoryParameter + Game.JAVA_Parameter); // 게임 실행
            }
            else
            {
                //SetState("마인크래프트 계정 로그인 실패. 프로필 파일 설정을 확인하세요.");
                //btn_Launch.Enabled = true;
                return;
            }
        }

        private void mod_btnPackSetting_Click(object sender, RoutedEventArgs e)
        {
            /*if (WPFCom.Feasibility("Bell_Smart_Launcher.Source.Frame.PackSetting"))
            {
                PackSetting packSet = new PackSetting();
                packSet.Show(); // 세팅탭 오픈
            }*/
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            WPFCom.End();
        }

        private void tc_Main_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*if (LastSelectedTab == -1)
                return;
            if (LastSelectedTab == 3)
            {
                // 예외 에러발생
                LastSelectedTab = 1; // 일단 강제로 설정 변경
                WPFCom.Message("예상치못한 탭 컨트롤 에러가 발생하여 자동으로 수정하었습니다.");
            }

            if (tc_Main.SelectedIndex == 3)
            {
                int tempTab = LastSelectedTab;
                LastSelectedTab = -1;
                tc_Main.SelectedIndex = tempTab;
                if (WPFCom.Feasibility("Bell_Smart_Launcher.Source.Frame.Setting"))
                {
                    Setting set = new Setting();
                    set.Show(); // 세팅탭 오픈
                }
            }

            if (tc_Main.SelectedIndex != 3 && tc_Main.SelectedIndex != -1) // 탭컨트롤 인덱스가 3이 아닐경우
                LastSelectedTab = tc_Main.SelectedIndex; // 마지막으로 선택된 탭 인덱스*/
        }

        private void set_detailSetting_Click(object sender, RoutedEventArgs e)
        {
            if (WPFCom.Feasibility("Bell_Smart_Launcher.Source.Frame.Setting"))
            {
                Setting set = new Setting();
                set.Show(); // 세팅탭 오픈
            }
        }

        private void mod_btnForceKill_Click(object sender, RoutedEventArgs e)
        {
            if (GameProcess != null && !GameProcess.HasExited)
            {
                GameProcess.Kill();
                WPFCom.Message("성공적으로 강제종료되었습니다.");
            }
            else
                WPFCom.Message("실행중인 게임이 없습니다.");
        }

        private void mod_btnLike_Click(object sender, RoutedEventArgs e)
        {
            if (BSN_BSL.isLikedPack(BSN_BSL.PACK.modpack, (string)mod_lstPackList.Tag))
            {
                // unlike
                if (!BSN_BSL.delLikePack(BSN_BSL.PACK.modpack, (string)mod_lstPackList.Tag))
                    WPFCom.Message("좋아요 취소에 실패했습니다.");
            }
            else
            {
                // like
                if (!BSN_BSL.setLikePack(BSN_BSL.PACK.modpack, (string)mod_lstPackList.Tag))
                    WPFCom.Message("좋아요 등록에 실패했습니다.");
            }

            mod_LoadLike(); // 새로고침
        }

        private void mod_wbNotice_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            if (noticeLock)
                e.Cancel = true;
        }

        private void mod_wbNotice_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            HideJsScriptErrors((WebBrowser)sender);
            noticeLock = true;
        }

        public void HideJsScriptErrors(WebBrowser wb)
        {
            // IWebBrowser2 interface
            // Exposes methods that are implemented by the WebBrowser control  
            // Searches for the specified field, using the specified binding constraints.

            FieldInfo fld = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fld == null)
                return;
            object obj = fld.GetValue(wb);
            if (obj == null)
                return;
            // Silent: Sets or gets a value that indicates whether the object can display dialog boxes.
            // HRESULT IWebBrowser2::get_Silent(VARIANT_BOOL *pbSilent);HRESULT IWebBrowser2::put_Silent(VARIANT_BOOL bSilent);
            obj.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, obj, new object[] { true });
        }
    }
}
