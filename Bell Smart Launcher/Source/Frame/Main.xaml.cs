using BellLib.Class;
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
using BellLib.Class.BSN;
using BellLib.Class.Minecraft;
using System.IO;
using System.Net;
using System.Diagnostics;
using Bell_Smart_Launcher.Source.Data;
using System.Reflection;
using BellLib.Class.Protection;
using System.Windows.Threading;

namespace Bell_Smart_Launcher.Source.Frame
{
    /// <summary>
    /// BSL_Main.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Main : Window
    {
        #region *** FIELD ***

        private DispatcherTimer tmr_GameCheck;
        private bool GameNormal;

        private Modpack GameInfo;
        private bool noticeLock = true;
        private bool isAllInit;

        #endregion

        #region *** INITIALIZE ***

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
            // 마지막에 열었던 탭 활성화
            try
            {
                tc_Main.SelectedIndex = Convert.ToInt32(DataProtect.DataLoad(DataPath.BSL.General, "LastSelectedTab"));
            }
            catch { }
            ti_Resources.Visibility = Visibility.Collapsed; // 아직 개발되지 않은 영역이므로 임시로 가려둠
            ti_Maps.Visibility = Visibility.Collapsed; // 아직 개발되지 않은 영역이므로 임시로 가려둠

            //NEWS


            //MODPACKS
            mod_lstPackList.Items.Clear(); // 팩 리스트 초기화!
            mod_lstDetailList.Items.Clear(); // 팩 상세정보 초기화
            mod_cbProfile.Items.Clear(); // 프로필 리스트 초기화
            mod_cbVersion.Items.Clear(); // 팩 버전 리스트 초기화
            mod_cbFilter.Items.Clear(); // 필터 리스트 초기화
            mod_btnForceKill.IsEnabled = false;

            tmr_GameCheck = new DispatcherTimer(); // 게임 체크 타이머 초기화

            //MAPS


            //RESOURCES


            //SETTING

        }

        /// <summary>
        /// 런처창이 로드된 후 사용할 수 있게 모든 기능을 초기화합니다.
        /// </summary>
        public void Initialize(bool forceInit = false)
        {
            if (!forceInit && isAllInit) // 이미 한번 전체 초기화했을때는 다시 초기화하지 않음.
                return;
            //Common


            //Individual
            InitNews(); // 뉴스탭 초기화
            InitModpacks(); // 모드팩탭 초기화
            InitResources(); // 리소스탭 초기화
            InitMaps(); // 맵탭 초기화
            InitSetting(); // 세팅탭 초기화
        }

        /// <summary>
        /// 뉴스탭 관련 기능을 초기화합니다.
        /// </summary>
        private void InitNews()
        {
            //NEWS

            // 필드
            List<string> NewsList = new List<string>();
            StringBuilder Newsfeed = new StringBuilder();
            string strData;
            string[] strUnprocessdList;


            try
            {
                // 데이터 로드
                // Bell Smart Launcher 공지 분류에 해당하는 공지만 받아옴
                strData = Common.getStringFromWeb("http://www.softbell.net/index.php?mid=notice&category=1312", Encoding.UTF8);
                // 문서 srl을 얻기 위한 문구로 파싱
                strUnprocessdList = Common.stringSplit(strData, "mid=notice&amp;category=1312&amp;document_srl=");


                // 뉴스 리스트 로드
                for (int i = 1; i < strUnprocessdList.Length - 1; i++) // 제일 첫 집합은 관련없는 값이므로 버리고 두번째 집합부터 돌림
                {
                    string strDocsrl = Common.stringSplit(strUnprocessdList[i], "\"")[0]; // 뒤에 잡 내용은 버리고 앞에 숫자만 받아옴
                    try
                    {
                        int intDoc = Convert.ToInt32(strDocsrl);
                        NewsList.Add(strDocsrl); // 문서 번호를 리스트에 등록함
                    }
                    catch { }
                }
            }
            catch
            {
                // 뉴스 리스트 로드 에러
                mod_wbNotice.NavigateToString("<meta charset=\"utf-8\"><strong abp=\"4668\"><span style=\"font-family: 돋움; \"abp=\"4670\"><font color=\"#ff0000\">뉴스 리스트를 불러오는 중 문제가 발생하였습니다.</font></span></strong>");

                return;
            }

            try
            {
                // 뉴스 내용 로드
                foreach (string doc in NewsList)
                {
                    try
                    {
                        string strNotice = Common.getStringFromWeb(Servers.Bell_Soft_Network.WEB_BSN_ROOT + "Notice/" + doc, Encoding.UTF8);
                        string[] strTemp = Common.stringSplit(strNotice, "<article");
                        // 제목 추가
                        strNotice = Common.stringSplit(strTemp[0], "<title>")[1];
                        strNotice = Common.stringSplit(strNotice, " - 공지사항 - 방울소프트네트워크</title>")[0];
                        Newsfeed.Append("<p><a href=\"" + Servers.Bell_Soft_Network.WEB_BSN_ROOT + "Notice/" + doc + "\" target=\"_blank\"><strong><span style=\"font-size: 12pt;\"><font color=\"#009e25\">" + "- " + strNotice + "</font></span></strong></a></p>");

                        // 내용 추가
                        strNotice = strTemp[1];
                        strTemp = Common.stringSplit(strNotice, "article>");
                        Newsfeed.Append("<article" + strTemp[0] + "article>");
                        if (doc != NewsList[NewsList.Count - 1])
                            Newsfeed.Append("<hr style=''border:5px; color:green; width:1024px;''>"); // 공지를 구분하는 라인 html 추가필요
                    }
                    catch { }
                }

                // 출력
                strData = "<meta charset=\"utf-8\">" + Newsfeed.ToString();

                news_wbNews.NavigateToString(strData);
            }
            catch
            {
                // 공지사항 로드 에러
                mod_wbNotice.NavigateToString("<meta charset=\"utf-8\"><strong abp=\"4668\"><span style=\"font-family: 돋움; \"abp=\"4670\"><font color=\"#ff0000\">뉴스 내용을 출력중에 문제가 발생하였습니다.</font></span></strong>");
            }
        }

        /// <summary>
        /// 모드팩 리스트를 초기화합니다.
        /// </summary>
        private void InitListModpack()
        {
            //초기화
            mod_lstPackList.Items.Clear(); // 팩 리스트 초기화!
            GameInfo = new Modpack();

            // 팩 리스트 로드
            GameInfo.LoadModpackList();

            // 필터 설정
            GameInfo.SetFilter((Modpack.FILTER)mod_cbFilter.SelectedIndex);

            // 모드팩 리스트 출력
            foreach (string value in GameInfo.GetModpackList())
                mod_lstPackList.Items.Add(value);

            // 마지막 팩 선택
            mod_lstPackList.SelectedItem = Modpack.GetLastModpack(); // 마지막에 선택했던 팩 자동선택
            if (mod_lstPackList.SelectedIndex == -1)
                mod_lstPackList.SelectedIndex = 0;

            // 게임 체크 타이머 설정
            tmr_GameCheck.Interval = TimeSpan.FromSeconds(5); // 5초간격
            tmr_GameCheck.Tick += new EventHandler(GameCheck_Tick); // 매 틱마다 GameCheck_Tick 메서드 실행
        }

        /// <summary>
        /// 모드팩탭 관련 기능을 초기화합니다.
        /// </summary>
        private void InitModpacks()
        {
            //MODPACKS
            // 프로필 로드
            mod_cbProfile.Items.Add("Select Profile");
            mod_cbProfile.Items.Add("Create Profile");

            ProfileLoad(); // 프로필 리스트 로드

            // 익스팬더 설정
            if (DataProtect.DataLoad(DataPath.BSL.Modpacks, "Expander") == "ACTIVATE")
                mod_expanderDetail.IsExpanded = true;
            else
                mod_expanderDetail.IsExpanded = false;

            // 필터 추가

            mod_cbFilter.Items.Add(Modpack.FILTER.All.ToString());
            mod_cbFilter.Items.Add(Modpack.FILTER.Standard.ToString());
            /*mod_cbFilter.Items.Add("Installed");
            mod_cbFilter.Items.Add("Not Install");*/

            mod_cbFilter.SelectedItem = Modpack.GetLastFilter(); // 마지막에 선택한 필터 자동선택
            if (mod_cbFilter.SelectedIndex == -1)
                mod_cbFilter.SelectedIndex = 1; // 기본값 Standard로 설정

            // 모드팩 리스트 로드
            //InitListModpack(); // 필터 초기화하면서 이미 모드팩리스트 로드됐음. 중복 로드
        }

        /// <summary>
        /// 리소스탭 관련 기능을 초기화합니다.
        /// </summary>
        private void InitResources()
        {

        }

        /// <summary>
        /// 맵탭 관련 기능을 초기화합니다.
        /// </summary>
        private void InitMaps()
        {

        }

        /// <summary>
        /// 세팅탭 관련 기능을 초기화합니다.
        /// </summary>
        private void InitSetting()
        {
            SystemInfo.MemoryInfo mi = SystemInfo.GetMemoryInfo();

            gen_cbLanguage.Items.Clear();
            gen_cbLanguage.Items.Add("한국어");
            gen_cbLanguage.Items.Add("English");
            gen_cbLanguage.SelectedIndex = 0;

            game_sdJAVA.Maximum = mi.Total_Physical_GB;

            gen_lbBSLVer.Content = Deploy.CurrentVersion.ToString();

            SettingLoad();

            AutoControl(); // 첫 실행시 자동 세팅
        }

        #endregion

        #region *** METHOD ***

        /// <summary>
        /// 자바 스크립트 에러를 숨겨줍니다.
        /// </summary>
        /// <param name="wb"></param>
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

        #endregion


        #region *** NEWS ***


        #endregion

        #region *** MODPACKS ***

        #region ** GAME **

        /// <summary>
        /// 게임 관리 시스템
        /// </summary>
        private void GameCheck_Tick(object sender, EventArgs e)
        {
            if (GameInfo.Feasibility())
            {
                // 게임 실행가능
                // 게임 실행하자마자 첫 타이머틱에 자바 프로세스가 종료되어있으면 결점 확인 진행
                tmr_GameCheck.Stop(); // 타이머 중단 안하면 계속 오류나니까 중단
                mod_btnForceKill.IsEnabled = false; // 게임이 이미 종료됐으므로 강제종료 비활성화

                if (!GameNormal)
                {
                    // 게임 실행 직후 종료됨. 자바 경로에 문제있는것으로 판단.
                    // *** 결점 확인 진행 ***
                    if (Game.AutoControl && Game.JAVA_Path.ToUpper().Contains((Game.BSL_Root + "Runtime\\Java\\").ToUpper()))
                    {
                        // 오토컨트롤 이용중, 런타임 자바팩을 이용중일경우
                        if (!JavaIntegrityCheck(true)) // 자바 무결성 체크 자동진행
                            WPFCom.Message("게임 실행중 예상치 못한 문제가 발생하여 종료된 것을 탐지하였습니다." + Environment.NewLine + "자바 경로 혹은 자바 파일에 문제가 있는지 확인하시기 바랍니다.");
                        else
                            WPFCom.Message("자바 무결성 체크결과 문제가 없는 것을 확인하였습니다." + Environment.NewLine + "이 에러메시지가 계속 발생한다면 해당 모드팩 관리자에게 문의하시기 바랍니다.");
                    }
                    else
                    {
                        if (WPFCom.Message("게임 실행중 예상치 못한 문제가 발생하여 종료된 것을 탐지하였습니다." + Environment.NewLine + "유력한 해결방안인 자바 무결성 체크를 진행하시겠습니까?", "Bell Smart Launcher", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            game_btnJAVAIntegrity_Click(sender, null); // 자바 무결성 체크 반자동 진행
                    }
                }
                else
                {
                    // 게임 플레이 후 크래시발생 또는 사용자가 직접 종료한것으로 판단.
                    
                    // 필드
                    string[] CrashReports;
                    string[] Screenshots;

                    // *** 크래시 리포트 폴더 검사 ***
                    try
                    {
                        CrashReports = Directory.GetFiles(GameInfo.GetPath() + "crash-reports\\", "crash-*-client.txt");

                        if (CrashReports.Length > 0)
                        {
                            // 크래시 리포트 발견
                            // 필드
                            string ReportFile = null;

                            // 크래시 리포트 서버에 전송


                            // 크래시 리포트 전송 확인
                            foreach (string value in CrashReports)
                            {
                                try
                                {
                                    if (value == CrashReports[CrashReports.Length - 1])
                                    {
                                        ReportFile = value.Replace("client.txt", "client-confirm.txt");
                                        File.Move(value, ReportFile);
                                    }
                                    else
                                        File.Move(value, value.Replace("client.txt", "client-ignore.txt"));
                                }
                                catch
                                {
                                    // 파일 이동중 문제 발생
                                }
                            }

                            // 크래시 알림
                            if (WPFCom.Message("게임 실행중 충돌이 발생하였습니다." + Environment.NewLine + "충돌 보고서를 확인하시겠습니까?" + Environment.NewLine + "지속적으로 충돌이 일어날경우 해당 모드팩 관리자에게 문의하시기 바랍니다.", "Bell Smart Launcher", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            {
                                try
                                {
                                    Process.Start(ReportFile);
                                }
                                catch
                                {
                                    WPFCom.Message("충돌 보고서 파일을 실행하는 중 문제가 발생하였습니다." + Environment.NewLine + "보고서 파일 위치 : " + ReportFile);
                                }
                            }
                        }
                    }
                    catch
                    {
                        // 크래시 리포트 없음
                    }

                    // *** 스크린샷 폴더 검사 ***
                    try
                    {
                        Screenshots = Directory.GetFiles(GameInfo.GetPath() + "screenshots\\", "*.png");

                        if (Screenshots.Length > 0)
                        {
                            // 사용자 직접 종료
                            // 스크린샷 파일 이동

                            // 스크린샷 파일 이동 알림
                            //WPFCom.Message("게임 플레이 스크린샷이 발견되었습니다.");
                        }
                    }
                    catch
                    {
                        // 스크린샷 없음
                    }
                }

                mod_btnEnjoy.IsEnabled = true; // 에러 처리가 끝났으니 실행가능
            }
            else
                GameNormal = true; // 게임 정상 실행중
        }

        /// <summary>
        /// 게임 자동설치 후 실행합니다.
        /// </summary>
        private void mod_btnEnjoy_Click(object sender, RoutedEventArgs e)
        {
            // 필드
            Modpack.ERR_PATH pathResult;
            Modpack.ERR_LOGIN loginResult;
            Modpack.ERR_LAUNCH launchResult;
            Profile profile = new Profile((string)mod_cbProfile.SelectedItem); // 선택한 프로필로 데이터를 초기화함.

            string Name = (string)mod_lstPackList.SelectedItem;
            string Version = (string)mod_cbVersion.SelectedItem;
            string MC_ID = profile.GetData(Profile.Data.ID);
            string MC_PW = profile.GetData(Profile.Data.PW);
            bool installModpack;
            bool installBase;

            // 중복실행 방지
            mod_btnEnjoy.IsEnabled = false;

            // 경로 설정
            pathResult = GameInfo.SetPath(Game.BSL_Root, Game.JAVA_Path);
            switch (pathResult)
            {
                case Modpack.ERR_PATH.Not_Load_Data:
                    WPFCom.Message("데이터가 로드되지 않아 경로를 설정할 수 없습니다.");
                    mod_btnEnjoy.IsEnabled = true;

                    return;
            }
            
            // 설치여부 검사
            installBase = !GameInfo.GetInstalled(BSN_BSL.PACK.basepack);
            installModpack = !GameInfo.GetInstalled(BSN_BSL.PACK.modpack);

            // 미설치시 설치시작
            if (installBase)
            {
                GameInfo.LoadInstallData(BSN_BSL.PACK.basepack); // 베이스팩 설치 정보 로드

                Installer install = new Installer(GameInfo.GetInstallData(BSN_BSL.PACK.basepack)); // 설치기 초기화
                install.Show(); // 설치기 실행
                install.Install(); // 설치 시작
            }

            if (installModpack)
            {
                GameInfo.LoadInstallData(BSN_BSL.PACK.modpack); // 모드팩 설치 정보 로드

                Installer install = new Installer(GameInfo.GetInstallData(BSN_BSL.PACK.modpack)); // 설치기 초기화
                install.Show(); // 설치기 실행
                install.Install(); // 설치 시작
            }

            // 옵션 설정
            if (!GameInfo.SetOption(Game.Memory_Allocate, Game.JAVA_Parameter, Game.ConsoleRun))
            {
                WPFCom.Message("옵션 설정에 실패했습니다.");
                mod_btnEnjoy.IsEnabled = true;

                return;
            }

            // 계정 정보 설정
            if (!GameInfo.SetAccount(MC_ID, MC_PW))
            {
                WPFCom.Message("계정정보 설정에 실패했습니다.");
                mod_btnEnjoy.IsEnabled = true;

                return;
            }

            if (MC_PW == null)
            {
                Password pass = new Password();
                pass.ShowDialog();
                MC_PW = pass.getPassword();
                loginResult = GameInfo.Login(MC_PW);
            }
            else
                loginResult = GameInfo.Login();

            switch (loginResult)
            {
                case Modpack.ERR_LOGIN.No_Input_ID:
                    WPFCom.Message("마인크래프트 계정 ID가 설정되지 않아 로그인할 수 없습니다.");
                    mod_btnEnjoy.IsEnabled = true;

                    return;

                case Modpack.ERR_LOGIN.No_Input_PW:
                    WPFCom.Message("마인크래프트 계정 비밀번호가 설정되지 않아 로그인할 수 없습니다.");
                    mod_btnEnjoy.IsEnabled = true;

                    return;

                case Modpack.ERR_LOGIN.Login_Fail:
                    WPFCom.Message("마인크래프트 로그인에 실패했습니다." + Environment.NewLine + "프로필에 아이디 또는 비밀번호를 정상적으로 저장했는지 확인 해 보시기 바랍니다.");
                    mod_btnEnjoy.IsEnabled = true;

                    return;
            }

            // 실행
            launchResult = GameInfo.Launch();
            switch (launchResult)
            {
                case Modpack.ERR_LAUNCH.Already_Running:
                    WPFCom.Message("게임이 이미 실행중 입니다.");

                    return;

                case Modpack.ERR_LAUNCH.No_Input_Data:
                    WPFCom.Message("실행에 필요한 데이터가 정상적으로 수집되지 않아 실행할 수 없습니다.");
                    mod_btnEnjoy.IsEnabled = true;

                    return;

                case Modpack.ERR_LAUNCH.Java_Not_Found:
                    if (WPFCom.Message("자바 경로가 비 정상적으로 설정되었습니다." + Environment.NewLine + "자바 경로 설정화면으로 이동하시겠습니까?", "Bell Smart Launcher", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        tc_Main.SelectedIndex = 4;
                        game_txtJAVAPath.Focus();
                    }
                    mod_btnEnjoy.IsEnabled = true;

                    return;

                case Modpack.ERR_LAUNCH.Not_Installed:
                    WPFCom.Message("모드팩이 정상적으로 설치되지 않아, 실행할 수 없습니다.");
                    mod_btnEnjoy.IsEnabled = true;

                    return;

                case Modpack.ERR_LAUNCH.Error:
                    WPFCom.Message("예상하지 못한 문제가 발생하여 실행하지 못했습니다.");
                    mod_btnEnjoy.IsEnabled = true;

                    return;
            }

            mod_btnForceKill.IsEnabled = true;

            // 정보 저장
            GameInfo.SetLastVersion();

            // 게임 관리 프로세스 시작
            GameNormal = false;
            tmr_GameCheck.Start();
        }

        /// <summary>
        /// 실행중인 게임을 강제종료합니다.
        /// </summary>
        private void mod_btnForceKill_Click(object sender, RoutedEventArgs e)
        {
            if (!GameInfo.Feasibility())
            {
                GameInfo.Kill(); // 게임 종료
                tmr_GameCheck.Stop(); // 게임 관리 프로세스 중단
                WPFCom.Message("성공적으로 강제종료되었습니다.");
            }
            else
                WPFCom.Message("실행중인 게임이 없습니다.");
            mod_btnForceKill.IsEnabled = false;
            mod_btnEnjoy.IsEnabled = true;
        }

        #endregion

        #region ** PROFILE **

        /// <summary>
        /// 프로필 데이터를 로드합니다.
        /// </summary>
        private void ProfileLoad()
        {
            // 초기화
            mod_cbProfile.Items.Clear();

            // 로드
            foreach (string value in Profile.GetProfileList(true))
                mod_cbProfile.Items.Add(value);

            // 프로필 선택
            mod_cbProfile.SelectedItem = Profile.GetLastProfile();

            if (mod_cbProfile.SelectedIndex == -1)
                mod_cbProfile.SelectedIndex = 0;
        }

        /// <summary>
        /// 프로필 리스트 변경을 검사 후 선택값을 저장합니다.
        /// </summary>
        private void mod_cbProfile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mod_cbProfile.SelectedIndex == 1)
            {
                mod_cbProfile.SelectedIndex = 0;
                ProfileEditor Pro = new ProfileEditor();
                Pro.ShowDialog();
                ProfileLoad(); // 값이 바뀌었을테니 프로필 다시 로드!

                if (Pro.GetSaveName() != null) // 프로필 이름이 null이 아니라면, (프로필을 정상적으로 생성했다면,
                    mod_cbProfile.SelectedItem = Pro.GetSaveName(); // 방금 생성한 따끈따끈한 프로필파일을 선택
            }

            if (mod_cbProfile.IsInitialized && mod_cbProfile.SelectedIndex > -1)
                Profile.SetLastProfile((string)mod_cbProfile.SelectedItem); // 선택 프로필이 바뀌었으므로 설정값 저장!
        }

        /// <summary>
        /// 프로필 수정모드로 엽니다.
        /// </summary>
        private void mod_btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (mod_cbProfile.SelectedIndex < 2)
                return;
            ProfileEditor pro = new ProfileEditor((string)mod_cbProfile.SelectedItem);
            pro.ShowDialog();
            ProfileLoad(); // 값이 바뀌었을테니 프로필 다시 로드!
        }

        #endregion

        /// <summary>
        /// 상세정보를 새로고칩니다.
        /// </summary>
        private void RefreshDetail()
        {
            // 초기화
            mod_lstDetailList.Items.Clear();

            // 출력
            foreach (string value in GameInfo.GetDetailInfo())
                mod_lstDetailList.Items.Add(value);
        }

        /// <summary>
        /// 상세정보 리스트를 보여주거나 감춥니다.
        /// </summary>
        private void Mod_Expand(object sender, RoutedEventArgs e)
        {
            if (!mod_expanderDetail.IsInitialized)
                return;

            if (mod_expanderDetail.IsExpanded)
            {
                // 활성화
                mod_lstDetailList.Visibility = Visibility.Visible;
                mod_wbNotice.Height = 250;
                DataProtect.DataSave(DataPath.BSL.Modpacks, "Expander", "ACTIVATE");
            }
            else
            {
                // 비활성화
                mod_lstDetailList.Visibility = Visibility.Hidden;
                mod_wbNotice.Height = 350;
                DataProtect.DataSave(DataPath.BSL.Modpacks, "Expander", "DISABLE");
            }
        }

        /// <summary>
        /// 팩 리스트 선택 항목이 변경되었을때
        /// </summary>
        private void mod_lstPackList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 예외처리
            if (mod_lstPackList.SelectedIndex < 0)
                return;

            // 필드
            Modpack.ERR_PATH pathResult;

            // 초기화
            mod_cbVersion.Items.Clear();
            mod_cbVersion.Items.Add("Latest");
            mod_cbVersion.Items.Add("Recommended");
            mod_lstDetailList.Items.Clear();
            noticeLock = false;

            // 데이터 로드
            GameInfo = new Modpack((string)mod_lstPackList.SelectedItem);
            GameInfo.LoadModpackBase();

            // 경로 설정
            pathResult = GameInfo.SetPath(Game.BSL_Root);
            switch (pathResult)
            {
                case Modpack.ERR_PATH.Not_Load_Data:
                    WPFCom.Message("데이터가 로드되지 않아 경로를 설정할 수 없습니다.");

                    return;
            }

            // 버전 리스트 로드
            foreach (string value in GameInfo.GetVersionList())
                mod_cbVersion.Items.Add(Common.getElement(value, "version"));

            // 버전 설정
            mod_cbVersion.SelectedItem = GameInfo.GetLastVersion(); // 마지막에 실행했던 버전 자동선택
            if (mod_cbVersion.SelectedIndex == -1)
                mod_cbVersion.SelectedIndex = 1;

            // 공지사항 출력
            mod_wbNotice.NavigateToString(GameInfo.GetNotice());

            // 좋아요 정보 로드
            mod_LoadLike();
        }

        /// <summary>
        /// 공지사항 컨트롤에서 페이지가 이동되는것을 막습니다.
        /// </summary>
        private void mod_wbNotice_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            if (noticeLock)
                e.Cancel = true;
        }

        /// <summary>
        /// 공지사항 로드가 완료되면 페이지 이동을 방지합니다.
        /// </summary>
        private void mod_wbNotice_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            HideJsScriptErrors((WebBrowser)sender);
            noticeLock = true;
        }

        /// <summary>
        /// 좋아요 정보를 로드합니다.
        /// </summary>
        private void mod_LoadLike()
        {
            if (GameInfo.GetLike())
                mod_btnLike.Content = "♥";
            else
                mod_btnLike.Content = "♡";
        }

        /// <summary>
        /// 모드팩 좋아요 정보를 설정합니다.
        /// </summary>
        private void mod_btnLike_Click(object sender, RoutedEventArgs e)
        {
            if (!GameInfo.SetLike(!GameInfo.GetLike())) // 좋아요 설정 반전
                WPFCom.Message("좋아요 설정에 실패했습니다.");

            mod_LoadLike(); // 새로고침
            RefreshDetail(); // 상세정보 로드
        }

        /// <summary>
        /// 모드팩 설정창을 엽니다.
        /// </summary>
        private void mod_btnPackSetting_Click(object sender, RoutedEventArgs e)
        {
            PackSetting packSet = new PackSetting();
            packSet.ShowDialog(); // 세팅탭 오픈
        }

        /// <summary>
        /// 선택한 버전으로 나머지 상세정보를 로드합니다.
        /// </summary>
        private void mod_cbVersion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!mod_cbVersion.IsInitialized) // 초기화 되지 않았으면 중단
                return;

            string modName = (string)mod_lstPackList.SelectedItem;
            string modVer = (string)mod_cbVersion.SelectedItem;
            Modpack.ERR_LOAD loadResult;

            // 필드 검사
            if (modName == null || modVer == null)
                return;

            // 초기화
            mod_lstDetailList.Items.Clear();

            // 로드
            GameInfo.SetVersion(modVer); // 선택한 버전 설정
            loadResult = GameInfo.LoadModpackDetail(false); // 상세정보 로드

            switch (loadResult)
            {
                case Modpack.ERR_LOAD.Version_Load_Fail:
                    WPFCom.Message("버전정보를 불러오는중 문제가 발생했습니다.");

                    return;

                case Modpack.ERR_LOAD.Not_Input_Version:
                    WPFCom.Message("버전정보가 설정되지 않았습니다.");

                    return;
            }

            RefreshDetail(); // 상세정보 로드
        }

        /// <summary>
        /// 필터값을 적용합니다.
        /// </summary>
        private void mod_cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!mod_cbFilter.IsInitialized || mod_cbFilter.SelectedIndex == -1)
                return;
            Modpack.SetLastFilter((string)mod_cbFilter.SelectedItem); // 필터 설정값 저장
            InitListModpack(); // 모드팩 리스트 다시 로드
        }

        #endregion

        #region *** RESOURCES ***


        #endregion

        #region *** MAPS ***


        #endregion

        #region *** SETTING ***

        /// <summary>
        /// 런처 처음실행시 사용자 환경에 맞게 세팅 진행
        /// </summary>
        private void AutoControl()
        {
            string newbie = DataProtect.DataLoad(DataPath.BSL.General, "AutoControl");
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

                SaveGeneral(); // 일반설정 저장
                SaveGame(); // 게임설정 저장

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

            // Default
            if (Game.DebugMode)
                gen_btnDebugger.Visibility = Visibility.Visible;
            else
                gen_btnDebugger.Visibility = Visibility.Collapsed;
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

            DataProtect.DataSave(DataPath.BSL.General, "BSL_Root", Game.BSL_Root);
            DataProtect.DataSave(DataPath.BSL.General, "Laungage", Game.Language);
            DataProtect.DataSave(DataPath.BSL.General, "ConsoleRun", Game.ConsoleRun.ToString());
            DataProtect.DataSave(DataPath.BSL.General, "KeepOpen", Game.KeepOpen.ToString());
            DataProtect.DataSave(DataPath.BSL.General, "AutoControl", Game.AutoControl.ToString());
            DataProtect.DataSave(DataPath.BSL.General, "DebugMode", Game.DebugMode.ToString());

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

            DataProtect.DataSave(DataPath.BSL.Game_Setting, "Memory_Allocate", Game.Memory_Allocate.ToString());
            DataProtect.DataSave(DataPath.BSL.Game_Setting, "JAVA_Path", Game.JAVA_Path);
            DataProtect.DataSave(DataPath.BSL.Game_Setting, "JAVA_Parameter", Game.JAVA_Parameter);
            DataProtect.DataSave(DataPath.BSL.Game_Setting, "MultipleExe", Game.MultipleExe.ToString());

            SettingLoad(2);

            return true;
        }

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
            WPFCom.Message("일반설정 저장에 성공하였습니다.");
        }

        /// <summary>
        /// 게임 설정 저장
        /// </summary>
        private void game_btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveGame();
            WPFCom.Message("게임설정 저장에 성공하였습니다.");
        }

        /// <summary>
        /// 자바 유효성 검증을 시행합니다.
        /// </summary>
        private void game_btnJAVAIntegrity_Click(object sender, RoutedEventArgs e)
        {
            if (Game.JAVA_Path.ToUpper().Contains((Game.BSL_Root + "Runtime\\Java\\").ToUpper()))
            {
                if (!JavaIntegrityCheck())
                    WPFCom.Message("런타임팩이 정상적으로 설치되어있습니다.");
            }
            else
                WPFCom.Message("런타임 자바팩이 아닌 외부 자바는 무결성 체크를 할 수 없습니다.");
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
                        WPFCom.Message("런타임팩에 문제가 발생하였습니다." + Environment.NewLine + "설치되지 않았거나 변경된 파일 : " + Environment.NewLine + sb);
                        if (WPFCom.Message("런타임 자바를 재설치 하시겠습니까?", "Bell Smart Launcher", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
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

        #endregion


        #region *** MAIN ***

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            //Initialize();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            WPFCom.End();
        }

        private void tc_Main_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tc_Main.SelectedIndex != -1)
                DataProtect.DataSave(DataPath.BSL.General, "LastSelectedTab", tc_Main.SelectedIndex.ToString()); // 현재 선택한 탭 인덱스 저장
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!GameInfo.Feasibility())
                if (WPFCom.Message("현재 게임이 실행중입니다." + Environment.NewLine + "런처에 종속성을 가진 게임은 런처종료 후 문제가 발생할 수 있습니다." + Environment.NewLine + "정말로 종료하시겠습니까?", "Bell Smart Launcher", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.No)
                    e.Cancel = true;
        }

        #endregion
    }
}