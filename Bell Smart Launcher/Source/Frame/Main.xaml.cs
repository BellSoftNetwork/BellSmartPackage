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

namespace Bell_Smart_Launcher.Source.Frame
{
    /// <summary>
    /// BSL_Main.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Main : Window
    {
        #region *** FIELD ***

        // 설정 파일 경로
        //private Process GameProcess = null;
        private Modpack GameInfo;
        private bool noticeLock = true;
        
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
                tc_Main.SelectedIndex = Convert.ToInt32(DataProtect.DataLoad(DataPath.BSL.General , "LastSelectedTab"));
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
            Dictionary<string, int> BasicPlan = new Dictionary<string, int>();
            Dictionary<string, int> PremiumPlan = new Dictionary<string, int>();
            Dictionary<string, int> PartnerPlan = new Dictionary<string, int>();
            Dictionary<string, int> BSN_SpecialPlan = new Dictionary<string, int>();

            //초기화
            mod_lstPackList.Items.Clear(); // 팩 리스트 초기화!
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
                        BasicPlan.Add(value, Convert.ToInt32(Common.getElement(value, "like")));
                        break;

                    case "1":
                        PremiumPlan.Add(value, Convert.ToInt32(Common.getElement(value, "like")));
                        break;

                    case "2":
                        PartnerPlan.Add(value, Convert.ToInt32(Common.getElement(value, "like")));
                        break;

                    case "10":
                        BSN_SpecialPlan.Add(value, Convert.ToInt32(Common.getElement(value, "like")));
                        break;

                    default:
                        BasicPlan.Add(value, Convert.ToInt32(Common.getElement(value, "like")));
                        break;
                }
            }

            // 최종 리스트 출력
            object[] plans;

            switch ((string)mod_cbFilter.SelectedItem)
            {
                case "All":
                    plans = new object[] { BSN_SpecialPlan, PartnerPlan, PremiumPlan, BasicPlan };
                    break;

                case "Standard":
                    plans = new object[] { BSN_SpecialPlan, PartnerPlan, PremiumPlan };
                    break;

                case "BSN_Special":
                    plans = new object[] { BSN_SpecialPlan };
                    break;

                case "Partner":
                    plans = new object[] { PartnerPlan };
                    break;

                case "Premium":
                    plans = new object[] { PremiumPlan };
                    break;

                case "Basic":
                    plans = new object[] { BasicPlan };
                    break;

                default:
                    plans = new object[] { BSN_SpecialPlan, PartnerPlan, PremiumPlan };
                    break;
            }
            
            foreach (Dictionary<string, int> plan in plans)
            {
                var plan_desc = from pack in plan orderby pack.Value descending select pack;
                foreach (var plan_value in plan_desc)
                    mod_lstPackList.Items.Add(Common.getElement(plan_value.Key, "name"));
            }

            mod_lstPackList.SelectedItem = DataProtect.DataLoad(DataPath.BSL.Modpacks, "Modpack"); // 마지막에 선택했던 팩 자동선택
            if (mod_lstPackList.SelectedIndex == -1)
                mod_lstPackList.SelectedIndex = 0;
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
            mod_cbFilter.Items.Add("All");
            mod_cbFilter.Items.Add("Standard");
            /*mod_cbFilter.Items.Add("Installed");
            mod_cbFilter.Items.Add("Not Install");*/
            /*mod_cbFilter.Items.Add("BSN_Special");
            mod_cbFilter.Items.Add("Partner");
            mod_cbFilter.Items.Add("Premium");
            mod_cbFilter.Items.Add("Basic");*/

            mod_cbFilter.SelectedItem = DataProtect.DataLoad(DataPath.BSL.Modpacks, "Filter"); // 마지막에 선택한 필터 자동선택
            if (mod_cbFilter.SelectedIndex == -1)
                mod_cbFilter.SelectedIndex = 1; // 기본값 Standard로 설정

            // 모드팩 리스트 로드
            InitListModpack();
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
        
        private void mod_btnEnjoy_Click(object sender, RoutedEventArgs e)
        {
            // 필드
            Modpack.ERR_LOAD loadResult;
            Modpack.ERR_PATH pathResult;
            Modpack.ERR_LOGIN loginResult;
            Modpack.ERR_LAUNCH launchResult;
            Profile profile = new Profile((string)mod_cbProfile.SelectedItem); // 선택한 프로필로 데이터를 초기화함.

            string Name = (string)mod_lstPackList.SelectedItem;
            string Version = (string)mod_cbVersion.SelectedItem;
            string MC_ID = profile.getData(Profile.Data.ID);
            string MC_PW = profile.getData(Profile.Data.PW);
            bool installModpack;
            bool installBase;

            GameInfo = new Modpack(Name, Version);
            
            // 상세정보 로드
            loadResult = GameInfo.LoadModpackDetail();
            switch (loadResult)
            {
                case Modpack.ERR_LOAD.Version_Load_Fail:
                    WPFCom.Message("버전정보를 불러오는중 문제가 발생했습니다.");

                    return;
            }

            // 경로 설정
            pathResult = GameInfo.SetPath(Game.BSL_Root, Game.JAVA_Path);
            switch (pathResult)
            {
                case Modpack.ERR_PATH.Not_Load_Data:
                    WPFCom.Message("데이터가 로드되지 않아 경로를 설정할 수 없습니다.");

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

                return;
            }

            // 계정 정보 설정
            if (!GameInfo.SetAccount(MC_ID, MC_PW))
            {
                WPFCom.Message("계정정보 설정에 실패했습니다.");

                return;
            }

            if (MC_PW == string.Empty)
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

                    return;

                case Modpack.ERR_LOGIN.No_Input_PW:

                    return;
            }

            // 실행
            launchResult = GameInfo.Launch();
            switch(launchResult)
            {
                case Modpack.ERR_LAUNCH.Already_Running:
                    WPFCom.Message("게임이 이미 실행중 입니다.");

                    return;

                case Modpack.ERR_LAUNCH.No_Input_Data:
                    WPFCom.Message("실행에 필요한 데이터가 정상적으로 수집되지 않아 실행할 수 없습니다.");

                    return;

                case Modpack.ERR_LAUNCH.Java_Not_Found:
                    if (WPFCom.Message("자바 경로가 비 정상적으로 설정되었습니다." + Environment.NewLine + "자바 경로 설정화면으로 이동하시겠습니까?", "Bell Smart Launcher", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        tc_Main.SelectedIndex = 4;
                        game_txtJAVAPath.Focus();
                    }

                    return;

                case Modpack.ERR_LAUNCH.Not_Installed:
                    WPFCom.Message("모드팩이 정상적으로 설치되지 않아, 실행할 수 없습니다.");

                    return;

                case Modpack.ERR_LAUNCH.Error:
                    WPFCom.Message("예상하지 못한 문제가 발생하여 실행하지 못했습니다.");

                    return;
            }
        }

        private void mod_btnForceKill_Click(object sender, RoutedEventArgs e)
        {
            if (!GameInfo.Feasibility())
            {
                GameInfo.Kill();
                WPFCom.Message("성공적으로 강제종료되었습니다.");
            }
            else
                WPFCom.Message("실행중인 게임이 없습니다.");
        }

        #endregion

        #region ** PROFILE **

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
            try
            {
                string[] ProfileList = Directory.GetFiles(DataPath.BSL.Profiles, "*.bdx"); // .bd 파일 리스트를 불러옴.
                foreach (string tmp in ProfileList)
                    mod_cbProfile.Items.Add(tmp.Replace(DataPath.BSL.Profiles, string.Empty).Replace(".bdx", string.Empty)); // 프로필 파일을 전부 로드함.
                mod_cbProfile.SelectedItem = DataProtect.DataLoad(DataPath.BSL.Modpacks, "Profile");
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(DataPath.BSL.Profiles);
            }
            if (mod_cbProfile.SelectedIndex == -1)
                mod_cbProfile.SelectedIndex = 0;
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
                DataProtect.DataSave(DataPath.BSL.Modpacks, "Profile", (string)mod_cbProfile.SelectedItem); // 선택 프로필이 바뀌었으므로 설정값 저장!
        }

        private void mod_btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (mod_cbProfile.SelectedIndex < 2)
                return;
            Profile pro = new Profile((string)mod_cbProfile.SelectedItem);
            pro.ShowDialog();
            ProfileLoad(); // 값이 바뀌었을테니 프로필 다시 로드!
        }

        #endregion

        /// <summary>
        /// 상세정보에 값을 추가합니다.
        /// </summary>
        /// <param name="key">정보 이름</param>
        /// <param name="value">정보 값</param>
        /// <param name="InputAfterClear">초기화 후 값 입력 여부</param>
        private void InputDetail(string key, string value, bool InputAfterClear = false)
        {
            if (InputAfterClear)
                mod_lstDetailList.Items.Clear();
            mod_lstDetailList.Items.Add(key + " : " + value);
        }

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

            // 초기화
            mod_cbVersion.Items.Clear();
            mod_cbVersion.Items.Add("Latest");
            mod_cbVersion.Items.Add("Recommended");
            noticeLock = false;

            // 데이터 로드
            BSN_BSL.ModPack mp = BSN_BSL.LoadModPackDetail((string)mod_lstPackList.SelectedItem);

            // 상세정보 출력
            InputDetail("Detail", mp.detail, true); // 상세정보
            InputDetail("Like", mp.like.ToString()); // 좋아요 개수 표시
            InputDetail("Recommended", mp.recommended); // 권장버전
            InputDetail("Latest", mp.latest); // 최신버전
            InputDetail("Basepack", mp.BaseName); // 베이스팩 이름
            foreach (BSN_BSL.Manager member in BSN_BSL.LoadPackManager(BSN_BSL.PACK.modpack, (string)mod_lstPackList.SelectedItem))
                if (member.permission == "4")
                    InputDetail("Producer", member.email); // 팩 제작자
            InputDetail("Made", mp.made); // 생성일
            InputDetail("Modification", mp.modification); // 마지막 수정일
            try
            {
                InputDetail("Plan", BSN_BSL.GetPlanName((BSN_BSL.PLAN)Convert.ToInt32(mp.plan))); // 요금제
            }
            catch
            {
                InputDetail("Plan", BSN_BSL.GetPlanName(BSN_BSL.PLAN.Basic));
            }

            // 버전 리스트 로드
            foreach (string value in BSN_BSL.LoadPackVersionList(BSN_BSL.PACK.modpack, (string)mod_lstPackList.SelectedItem))
                mod_cbVersion.Items.Add(Common.getElement(value, "version"));

            // 버전 설정
            if ((string)mod_lstPackList.SelectedItem == DataProtect.DataLoad(DataPath.BSL.Modpacks, "Modpack")) // 마지막에 선택했던 팩 자동선택)
                mod_cbVersion.SelectedItem = DataProtect.DataLoad(DataPath.BSL.Modpacks, "Version"); // 마지막에 실행했던 버전 자동선택
            if (mod_cbVersion.SelectedIndex == -1)
                mod_cbVersion.SelectedIndex = 1;

            // 공지사항 출력
            try
            {
                string strNotice = Common.getStringFromWeb(mp.notice, Encoding.UTF8);
                string[] strTemp = Common.stringSplit(strNotice, "<article");
                strNotice = strTemp[1];
                strTemp = Common.stringSplit(strNotice, "article>");
                strNotice = "<meta charset=\"utf-8\"><article" + strTemp[0].Replace("target=\"_blank\"", "") + "article >";

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

        private void mod_btnPackSetting_Click(object sender, RoutedEventArgs e)
        {
            PackSetting packSet = new PackSetting();
            packSet.ShowDialog(); // 세팅탭 오픈
        }

        private void mod_btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            PreInitialize();
            Initialize();
            WPFCom.Message("런처 초기화에 성공하였습니다.");
        }

        private void mod_cbVersion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!mod_cbVersion.IsInitialized) // 초기화 되지 않았으면 중단
                return;

            string modName = (string)mod_lstPackList.SelectedItem;
            string modVer = (string)mod_cbVersion.SelectedItem;
            string modVerid = null;
            string baseVerid = null;
            string baseVer = null;

            // 필드 검사
            if (modName == string.Empty || modVer == string.Empty)
                return;

            // 상세 정보 로드
            string[] verList = BSN_BSL.LoadPackVersionList(BSN_BSL.PACK.modpack, modName, BSN_BSL.STATE.ACTIVATE); // 모드팩 버전 리스트
            BSN_BSL.ModPack mp = BSN_BSL.LoadModPackDetail(modName); // 모드팩 정보 로드

            // 버전정보 검증
            if (modVer == "Recommended") // 권장버전을 선택했을경우,
                modVer = mp.recommended; // 공식 권장버전을 대입
            try
            {
                foreach (string verData in verList)
                {
                    if (modVer == "Latest") // 선택한 버전이 최신버전일경우,
                        if (modVerid == null) // 버전id 설정이 안되어있을경우 (foreach 처음 진입일경우)
                            modVer = Common.getElement(verData, "version"); // 최신버전값을 넣어준다.
                    if (modVer == Common.getElement(verData, "version")) // 루프를 돌다가 선택버전과 서버버전이 일치할경우,
                        modVerid = Common.getElement(verData, ("id")); // 해당 버전 id를 로드한다.
                }
            }
            catch
            {
                return;
            }

            // 데이터 유효성 검증
            if (modVerid == null) // 예상치 못한 오류로 모드 버전 id를 받지 못하였을경우 실행 중단
                return;

            // 선행 로드가 끝난 후 추가정보 로드
            string modVerData = BSN_BSL.LoadVersionDetail(BSN_BSL.PACK.modpack, modVerid); // 모드팩 버전 상세정보 로드
            baseVerid = Common.getElement(modVerData, "basevid"); // 베이스팩 버전id
            verList = BSN_BSL.LoadPackVersionList(BSN_BSL.PACK.basepack, mp.BaseName, BSN_BSL.STATE.ACTIVATE);
            foreach (string verData in verList)
                if (baseVerid == Common.getElement(verData, "id")) // 루프를 돌다가 선택버전과 서버버전이 일치할경우,
                    baseVer = Common.getElement(verData, "version"); // 해당 버전 id를 로드한다.
            
            // 출력
            foreach (string value in mod_lstDetailList.Items)
                if (value.Contains("Basepack : "))
                {
                    int index = mod_lstDetailList.Items.IndexOf(value) + 1;
                    mod_lstDetailList.Items.Insert(index, "Basepack Version : " + baseVer);

                    break;
                }
        }

        private void mod_cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!mod_cbFilter.IsInitialized || mod_cbFilter.SelectedIndex == -1)
                return;
            DataProtect.DataSave(DataPath.BSL.Modpacks, "Filter", (string)mod_cbFilter.SelectedItem); // 필터 설정값 저장
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

        private void game_btnJAVAIntegrity_Click(object sender, RoutedEventArgs e)
        {
            if (game_txtJAVAPath.Text.ToUpper().Contains((Game.BSL_Root + "Runtime\\Java\\").ToUpper()))
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
                    string localURL = game_txtJAVAPath.Text.Remove(game_txtJAVAPath.Text.Length - 3) + value.url;
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
                    WPFCom.Message("런타임팩에 문제가 발생하였습니다." + Environment.NewLine + "설치되지 않았거나 변경된 파일 : " + Environment.NewLine + sb);
                    if (WPFCom.Message("런타임 자바를 재설치 하시겠습니까?", "Bell Smart Launcher", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        InstallJava(game_txtJAVAPath.Text);
                }
                else
                    WPFCom.Message("런타임팩이 정상적으로 설치되어있습니다.");
            }
            else
                WPFCom.Message("런타임 자바팩이 아닌 외부 자바는 무결성 체크를 할 수 없습니다.");
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Initialize();
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