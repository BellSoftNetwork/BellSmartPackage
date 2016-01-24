using BellLib.Class;
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

namespace Bell_Smart_Launcher.Source.Frame
{
    /// <summary>
    /// BSL_Main.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Main : Window
    {
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


            //NEWS


            //MODPACKS
            foreach (string value in BSN_BSL.LoadPackList(BSN_BSL.PACK.modpack))
                mod_lstPackList.Items.Add(Common.getElement(value, "name"));
            mod_lstPackList.SelectedIndex = 0; // 마지막에 선택했던 팩 자동선택
            Mod_Expand(mod_expanderDetail.IsExpanded); // 모드탭 익스펜더 설정
            mod_cbProfile.Items.Add("Select Profile");
            mod_cbProfile.Items.Add("Create Profile");
            mod_cbProfile.SelectedIndex = 0;
            /*mod_cbVersion.Items.Add("Recommended");
            mod_cbVersion.Items.Add("Latest");
            mod_cbVersion.SelectedIndex = 0;*/
            ProfileLoad(); // 프로필 리스트 로드
            SettingLoad(); // 셋팅값 로드!

            //MAPS


            //RESOURCES


            //SETTING

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
            mod_cbProfile.SelectedIndex = 0; // 일단 프로필 선택으로 맞춰둠 (기본값)
            string DefaultPath = User.BSL_Root + "Data\\BSL\\Profile\\"; // 프로필파일 기본 경로
            try
            {
                string[] ProfileList = Directory.GetFiles(DefaultPath, "*.bdx"); // .bd 파일 리스트를 불러옴.
                foreach (string tmp in ProfileList)
                    mod_cbProfile.Items.Add(tmp.Replace(DefaultPath, string.Empty).Replace(".bdx", string.Empty)); // 프로필 파일을 전부 로드함.
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(DefaultPath);
            }
        }

        /// <summary>
        /// 클라이언트 설정값을 전부 로드 후, BSL을 초기화합니다.
        /// </summary>
        private void SettingLoad()
        {
            string[] DataList;
            try
            {
                DataList = Protection.ReadBDXFile(User.BSL_Root + "DATA\\BSL\\Client.bdx");
            }
            catch
            {
                return; // 로드실패. 셋팅 로드 중단
            }
            foreach (string Data in DataList)
            {
                string[] Value = Data.Split('|');
                switch (Value[0])
                {
                    case "PROFILE":
                        if (Value[1] != string.Empty)
                            mod_cbProfile.SelectedItem = Value[1];
                        break;

                    case "MODPACK":
                        mod_lstPackList.SelectedItem = Value[1];
                        break;
                }
            }
        }

        /// <summary>
        /// 클라이언트 설정값을 저장합니다.
        /// </summary>
        private void SaveSetting()
        {
            List<string> list = new List<string>();
            if (mod_cbProfile.SelectedIndex == 0)
            {
                list.Add("PROFILE|" + string.Empty);
            }
            else
            {
                list.Add("PROFILE|" + (string)mod_cbProfile.SelectedItem);
            }
            list.Add("MODPACK|" + (string)mod_lstPackList.SelectedItem);

            Protection.WriteBDXFile(User.BSL_Root + "DATA\\BSL\\Client.bdx", list.ToArray()); // 모든 값 저장
        }

        private void Mod_Expand(bool expand)
        {
            if (expand)
            { // 활성화
                mod_lstDetailList.Visibility = Visibility.Visible;
                mod_wbNotice.Height = 284;
            }
            else
            { // 비활성화
                mod_lstDetailList.Visibility = Visibility.Hidden;
                mod_wbNotice.Height = 347;
            }
        }

        /// <summary>
        /// 모드팩을 실행합니다.
        /// </summary>
        /// <param name="PathBase">베이스팩 경로</param>
        /// <param name="PathPack">모드팩 경로</param>
        private void Launch(string Version, string PathBase, string PathPack, string PathJAVA, string Parameter, string NickName, string UUID, string AccessToken)
        {
            if (Version == string.Empty || PathBase == string.Empty || PathPack == string.Empty || UUID == string.Empty || AccessToken == string.Empty)
            {
                WinCom.Message("게임 실행 중 매개변수값이 정상적으로 전달되지 않아 실행을 중단합니다.");
                return;
            }
            string strTemp;
            StringBuilder sb = new StringBuilder(1024); //기본 문자열을 JAVA 변수, 기본 캐피시터를 1024로 하여 StringBuilder 선언.
            //PackAnalysisRead MAR = new PackAnalysisRead(PackAnalysisRead.PackType.Mod, MUID); // 선택된 팩정보로 인스턴스 생성

            sb.Append(Parameter);

            sb.Append(" -Djava.library.path=");
            sb.Append(PathBase);
            sb.Append("natives");

            sb.Append(" -cp ");
            sb.Append(PathBase);
            sb.Append("*");

            sb.Append(" net.minecraft.launchwrapper.Launch");

            sb.Append(" --username ");
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

            sb.Append(" --userProperties {} --userType mojang --tweakClass cpw.mods.fml.common.launcher.FMLTweaker");

            strTemp = sb.ToString();
            try
            {
                Directory.SetCurrentDirectory(PathPack); //BST 실행경로를 방울크래프트 클라이언트 경로로 수정.
                Process.Start(PathJAVA, strTemp);
                //BC_PID = Shell(strTemp, AppWinStyle.NormalFocus);
            }
            catch (FileNotFoundException fnf)
            {
                BellLib.Class.Debug.Message(BellLib.Class.Debug.Level.High, fnf.Message);
                /*BC_PID = -1;
                if (BST_Manager.Message("자바 경로가 비 정상적으로 설정되었습니다." + Constants.vbCrLf + "자바 경로 설정 화면으로 이동하시겠습니까?", , MessageBoxButtons.YesNo) == Windows.Forms.DialogResult.Yes) {
                    BC_Preferences.ShowDialog();
                }
                BC_Button(false);*/
            }
            catch (Exception ex)
            {
                WPFCom.Message(ex.Message);
                /*BC_PID = -1;
                BST_Manager.Message("방울크래프트 실행 중 문제가 발생하였습니다." + Constants.vbCrLf + "자바 경로가 정상적으로 설정되어있는지 확인하시기 바랍니다." + Constants.vbCrLf + Constants.vbCrLf + ex.Message);
                BC_Button(false);*/
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
                SettingLoad(); // 셋팅값 로드!
                if (Pro.getData(Profile.Data.Name) != null) // 프로필 이름이 null이 아니라면, (프로필을 정상적으로 생성했다면,
                    mod_cbProfile.SelectedItem = Pro.getData(Profile.Data.Name); // 방금 생성한 따끈따끈한 프로필파일을 선택
                SaveSetting(); // 선택 프로필이 바뀌었으므로 설정값 저장!
            }
        }

        private void mod_btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (mod_cbProfile.SelectedIndex < 2)
                return;
            Profile pro = new Profile((string)mod_cbProfile.SelectedItem);
            pro.ShowDialog();
            ProfileLoad(); // 값이 바뀌었을테니 프로필 다시 로드!
            SettingLoad(); // 셋팅값 로드!
            SaveSetting(); // 선택 프로필이 바뀌었으므로 설정값 저장!
        }

        private void mod_lstPackList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mod_lstPackList.SelectedIndex < 0)
                return;
            mod_cbVersion.Items.Clear();
            mod_cbVersion.Items.Add("Latest");
            mod_cbVersion.Items.Add("Recommended");
            foreach (string value in BSN_BSL.LoadPackVersionList(BSN_BSL.PACK.modpack, (string)mod_lstPackList.SelectedItem))
                mod_cbVersion.Items.Add(Common.getElement(value, "version"));

            mod_cbVersion.SelectedIndex = 1;
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
            string basePath = User.BSL_Root + "Base\\" + baseVerid + "\\";
            string modPath = User.BSL_Root + "ModPack\\" + modVerid + "\\";

            /// 클라이언트 유효성 검증
            // 베이스팩 설치유무확인
            if (!Directory.Exists(basePath))
                installBase = true;

            // 모드팩 설치유무확인
            if (!Directory.Exists(modPath))
                installMod = true;

            // 미설치시 설치시작
            if (installBase && installMod)
            {
                Installer install = new Installer(modName, modVer, modVerid, baseVerid); // 설치기 초기화
                install.Show(); // 설치기 실행
                install.Install(installBase, installMod, basePath, modPath); // 설치 시작
            }

            /// 게임 실행
            // 계정 로그인
            Profile profile = new Profile((string)mod_cbProfile.SelectedItem); // 선택한 프로필로 데이터를 초기화함.
            User.MC_ID = profile.getData(Profile.Data.ID);
            User.MC_PW = profile.getData(Profile.Data.PW);
            string Password = User.MC_PW;

            if (User.MC_ID != string.Empty) // && User.MC_PW != null) // 레지스트리에 MC 계정정보가 저장되어있으면 로그인 실행
            {
                if (Password == string.Empty)
                {
                    Password pass = new Password();
                    pass.ShowDialog();
                    Password = pass.getPassword();
                }

                //SetState("마인크래프트 계정 로그인 시도중");
                if (MCLogin.Login(User.MC_ID, Password, MCLogin.LoginType.Authenticate))
                {
                    //SetState("마인크래프트 계정 로그인 성공");
                }
                else
                {
                    //SetState("마인크래프트 계정 로그인에 실패하였습니다. 아이디 또는 비밀번호를 확인해주세요.");
                    //btn_Launch.Enabled = true;
                    return;
                }
            }
            else
            {
                //SetState("마인크래프트 계정 로그인 실패. 프로필 파일 설정을 확인하세요.");
                //btn_Launch.Enabled = true;
                return;
            }
            Launch(modVer, basePath, modPath, @"C:\BSN\Runtime\JAVA\x64\bin\java.exe", "", User.MC_NickName, User.MC_UUID, User.MC_AccessToken); // 게임 실행
        }

        private void mod_btnPackSetting_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            WPFCom.End();
        }

        // News 버튼 위로 커서가 올라갔을 시id NewsButton_MouseEnteTabItemeMouseEntereEventArgs e)
        {
            var uriSUri(@"/WpfApplication1;component/changeimage.png", UriKind.Relative);
            ImageName.Source = new BitmapImage(uriSource);
        }
    }
}
