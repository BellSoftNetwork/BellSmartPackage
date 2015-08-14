using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BellLib.Data;
using BellLib.Class;
using System.IO;
using System.Diagnostics;
using System.Net;

namespace Bell_Smart_Server.Source.BSL
{
    public partial class BSL_Main : Form
    {
        private bool Initialization = false; // 초기화 상태
        public BSL_Main()
        {
            InitializeComponent();
        }
        /// <summary>
        /// BSL을 초기화합니다.
        /// </summary>
        private void Initialize()
        {
            ListLoad(); // 팩 리스트 로드
            ProfileLoad(); // 프로필 로드
            SettingLoad(); // 클라이언트 셋팅 로드
            Initialization = true;
        }

        private void SetState(string Log)
        {
            lb_State.Text = Log;
            Application.DoEvents();
        }

        /// <summary>
        /// 팩 리스트를 로드합니다.
        /// </summary>
        private void ListLoad()
        {
            lst_ModPack.Items.Clear(); // 리스트 전체 초기화!
            lst_ModPack.Tag = string.Empty;

            PackAnalysisRead MAR = new PackAnalysisRead();
            string[] PackList = PackAnalysisRead.LoadPackList(PackAnalysisRead.PackType.Mod);
            List<string> PackNameList = new List<string>();

            foreach (string tmp in PackList)
            {
                MAR = new PackAnalysisRead(PackAnalysisRead.PackType.Mod, tmp); // 얻어온 MUID로 팩 정보 분석
                PackNameList.Add(MAR.GetInfo(PackAnalysisRead.PackType.Mod, "Name")); // 팩 이름 리스트 추가
                if (tmp == PackList[PackList.Length - 1])
                {
                    lst_ModPack.Tag += tmp; // tmp값이 팩 리스트의 마지막값이면 파싱문자를 추가하지않음.
                }
                else
                {
                    lst_ModPack.Tag += tmp + "|";
                }
            }
            lst_ModPack.Items.AddRange(PackNameList.ToArray()); // 팩 리스트 로드!
            lst_ModPack.SelectedIndex = 0; // 첫번째 모드팩 기본 선택
            

            cb_Version.Items.Clear(); // 버전정보 리스트 초기화!
            string[] Default = { "Latest", "Recommended" }; // 기본값 임시 저장
            cb_Version.Items.AddRange(Default); // 기본값 삽입!
            cb_Version.SelectedItem = "Recommended"; // 선택값을 권장버전으로 설정!

            
            // 클라 셋팅에서 프로필 리스트 불러온 뒤 추가함.
            // 마지막 설정한 프로필 선택.
        }
        /// <summary>
        /// 프로필 데이터를 로드합니다.
        /// </summary>
        private void ProfileLoad()
        {
            cb_Profile.Items.Clear(); // 프로필 리스트 초기화
            string[] Default = { "프로필 선택", "프로필 생성" };
            cb_Profile.Items.AddRange(Default); // 기본값 추가
            cb_Profile.SelectedIndex = 0; // 일단 프로필 선택으로 맞춰둠 (기본값)
            string DefaultPath = User.BSL_Root + "Data\\BSL\\Profile\\"; // 프로필파일 기본 경로
            string[] ProfileList = Directory.GetFiles(DefaultPath,"*.bdx"); // .bd 파일 리스트를 불러옴.
            foreach (string tmp in ProfileList)
            {
                cb_Profile.Items.Add(tmp.Replace(DefaultPath, string.Empty).Replace(".bdx", string.Empty)); // 프로필 파일을 전부 로드함.
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
                DataList = Common.ReadBDXFile(User.BSL_Root + "DATA\\BSL\\Client.bdx");
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
                            cb_Profile.SelectedItem = Value[1];
                        break;
                        
                    case "MODPACK":
                        lst_ModPack.SelectedItem = Value[1];
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
            if (cb_Profile.SelectedIndex == 0)
            {
                list.Add("PROFILE|" + string.Empty);
            }
            else
            {
                list.Add("PROFILE|" + (string)cb_Profile.SelectedItem);
            }
            list.Add("MODPACK|" + (string)lst_ModPack.SelectedItem);

            Common.WriteBDXFile(User.BSL_Root + "DATA\\BSL\\Client.bdx", list.ToArray()); // 모든 값 저장
        }

        /// <summary>
        /// 모드팩을 실행합니다.
        /// </summary>
        /// <param name="PathBase">베이스팩 경로</param>
        /// <param name="PathPack">모드팩 경로</param>
        private void Enjoy(string MUID, string BUID, string PathBase, string PathPack, string PathJAVA, string Parameter, string NickName, string UUID, string AccessToken)
        {
            if (MUID == string.Empty || PathBase == string.Empty || PathPack == string.Empty || UUID == string.Empty || AccessToken == string.Empty)
            {
                Common.Message("게임 실행 중 매개변수값이 정상적으로 전달되지 않아 실행을 중단합니다.");
                return;
            }
            string strTemp;
            StringBuilder sb = new StringBuilder(1024); //기본 문자열을 JAVA 변수, 기본 캐피시터를 1024로 하여 StringBuilder 선언.
            PackAnalysisRead MAR = new PackAnalysisRead(PackAnalysisRead.PackType.Mod, MUID); // 선택된 팩정보로 인스턴스 생성

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
            sb.Append(MUID);

            sb.Append(" --gameDir ");
            sb.Append(PathPack);

            sb.Append(" --assetsDir ");
            sb.Append(PathBase);
            sb.Append("assets");

            sb.Append(" --assetIndex ");
            sb.Append(BUID);

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
                Common.Message(ex.Message);
                /*BC_PID = -1;
                BST_Manager.Message("방울크래프트 실행 중 문제가 발생하였습니다." + Constants.vbCrLf + "자바 경로가 정상적으로 설정되어있는지 확인하시기 바랍니다." + Constants.vbCrLf + Constants.vbCrLf + ex.Message);
                BC_Button(false);*/
            }
        }

        /// <summary>
        /// 선택된 클라이언트 설치여부를 확인한 후, 새 버전이 있을경우 업데이트합니다.
        /// </summary>
        /// <param name="RootPath">클라이언트 로컬 루트 경로</param>
        /// <param name="MUID">MUID값</param>
        /// <param name="SelectMod">모드팩 설치 희망 버전</param>
        /// <returns>BUID|요구버전</returns>
        private string CheckInstall(string RootPath, string MUID, string SelectMod)
        {
            // 주 데이터
            string BUID;
            string ModVersion = string.Empty; // 모드팩 현재 버전
            string RelativeMod = SelectMod; // 모드팩 상대 버전
            string BaseVersion = string.Empty; // 베이스팩 현재 버전
            string SelectBase; // 베이스팩 선택 버전
            int LengthMod = 0; // 모드팩 설치 길이
            int LengthBase = 0; // 베이스팩 설치 길이

            // 1. 기초 데이터 로드
            SetState("설치 및 업데이트 필요여부 검사 시작");

            // 서버에서 데이터 가져옴 - 모드팩 부분
            PackAnalysisRead MAR = new PackAnalysisRead(PackAnalysisRead.PackType.Mod, MUID); // 인스턴스 생성
            BUID = MAR.GetInfo(PackAnalysisRead.PackType.Mod, "Base"); SetState("필요 베이스팩 고유이름 로드 완료");
            switch (SelectMod) // 유지 희망 버전이 상대적일경우,
            {
                case "Recommended":
                    SelectMod = MAR.GetInfo(PackAnalysisRead.PackType.Mod, "Recommended"); // 모드팩 권장버전의 절대버전
                    break;

                case "Latest":
                    SelectMod = MAR.GetInfo(PackAnalysisRead.PackType.Mod, "Latest"); // 모드팩 최신버전의 절대버전
                    break;
            }
            SetState("모드팩 상대버전 분석 완료");
            MAR.LoadMod(SelectMod); SetState("베이스팩 정보 로드 완료"); // 모드팩 선택버전의 데이터 로드
            SetState("모드팩 진행바 최대값 계산중");
            LengthMod += MAR.GetInstallData(PackAnalysisRead.PackType.Mod, "Directory").Length; // 생성이 필요한 디렉토리 개수만큼 진행바 최대값 증가
            LengthMod += MAR.GetInstallData(PackAnalysisRead.PackType.Mod, "Hash").Length; // 생성이 필요한 파일 개수만큼
            SetState("모드팩 진행바 최대값 계산완료");
            SelectBase = MAR.GetInstallInfo(PackAnalysisRead.PackType.Mod, "Base"); SetState("베이스팩 필요버전 로드 완료"); // 베이스팩 필요 버전 로드 (상대 버전일 수도 있음)

            // 서버에서 데이터 가져옴 - 베이스팩 부분
            MAR = new PackAnalysisRead(PackAnalysisRead.PackType.Base, BUID); // 인스턴스 생성
            switch (SelectBase)
            {
                case "Recommended":
                    SelectBase = MAR.GetInfo(PackAnalysisRead.PackType.Base, "Recommended"); // 베이스팩 권장버전의 절대버전
                    break;

                case "Latest":
                    SelectBase = MAR.GetInfo(PackAnalysisRead.PackType.Base, "Latest"); // 베이스팩 최신버전의 절대버전
                    break;
            }
            SetState("베이스팩 상대버전 분석 완료");
            MAR.LoadBase(SelectBase);
            SetState("베이스팩 진행바 최대값 계산중");
            LengthBase += MAR.GetInstallData(PackAnalysisRead.PackType.Base, "Directory").Length;
            LengthBase += MAR.GetInstallData(PackAnalysisRead.PackType.Base, "Hash").Length;
            SetState("베이스팩 진행바 최대값 계산완료");

            // 클라이언트 모드팩 데이터 가져옴
            try
            {
                string[] modData = Common.ReadBDXFile(RootPath + "\\ModPack\\" + MUID + "\\" + SelectMod + "\\data.bdx"); // 클라이언트 모드팩 데이터 로드
                foreach (string tmp in modData)
                {
                    string[] Value = tmp.Split('|');
                    if (Value[0] == "Current Version") // 데이터 집합 중 현재 버전 데이터일경우
                    {
                        if (SelectMod == Value[1]) // 희망버전과 로컬 버전이 같으면
                        {
                            ModVersion = Value[1];
                            break; // 필요한거 얻었응께 나머지 버리고 나와브러~
                        }
                    }
                }
                SetState("모드팩 로컬데이터 분석 완료");
            }
            catch // 데이터 로드중 예외 오류가 날 경우, 신규 설치로 간주
            {
                // Mod Current Version이 이미 null로 초기화 되어있으므로 그냥 진행.
                // Mod Select Version이 이미 SelectVer로 초기화 되어있으므로 그냥 진행.
                SetState("모드팩 신규설치");
            }

            // 클라이언트 베이스팩 데이터 가져옴
            try
            {
                string[] baseData = Common.ReadBDXFile(RootPath + "\\Base\\" + BUID + "\\" + SelectBase + "\\data.bdx");
                foreach (string tmp in baseData)
                {
                    string[] Value = tmp.Split('|');
                    if (Value[0] == "Current Version") // 데이터 집합 중 현재버전만.
                    {
                        if (SelectBase == Value[1]) // 베이스팩 요구버전과 베이스팩 현재버전이 같으면
                        {
                            BaseVersion = Value[1];
                            SetState("베이스팩 로컬 데이터 분석 완료");
                            break; // 찾을거 다 찾았으면 가차없이 반복문 탈출 해부러~~
                        }
                    }
                }
            }
            catch // 데이터 로드중 문제 발생, 신규 설치로 간주
            {
                // BaseVersion이 이미 null로 초기화 되어있으므로 그냥 진행.
                SetState("베이스팩 신규설치");
            }

            // 2. 설치 & 업데이트
            // 로드한 데이터를 바탕으로 설치 또는 업데이트를 시행함.
            SetState("진행바 최대값 설정");
            if (ModVersion != SelectMod) // 모드팩 희망버전과 로컬버전이 다를경우,
                pb_Load.Maximum += LengthMod;
            if (BaseVersion != SelectBase) // 베이스팩 희망버전과 로컬버전이 다를경우,
                pb_Load.Maximum += LengthBase;

            SetState("설치 및 업데이트 진행");
            if (ModVersion != SelectMod) // 모드팩 희망버전과 로컬버전이 다를경우,
            {
                InstallMod(MUID, SelectMod, RelativeMod); // 모드팩 설치 및 업데이트 진행
            }
            if (BaseVersion != SelectBase) // 베이스팩 희망버전과 로컬버전이 다를경우,
            {
                InstallBase(BUID, SelectBase); // 베이스팩 설치 및 업데이트 진행
            }
            SetState("정상적으로 설치되었습니다.");
            return BUID + "|" + SelectBase + "|" + SelectMod; // 'BUID|요구버전' 반환
        }

        /// <summary>
        /// 베이스팩을 설치 및 업데이트합니다.
        /// </summary>
        /// <param name="BUID">BUID 값</param>
        /// <param name="Version">설치버전</param>
        private void InstallBase(string BUID, string Version)
        {
            PackAnalysisRead MAR = new PackAnalysisRead(PackAnalysisRead.PackType.Base, BUID); // 선택된 팩정보로 인스턴스 생성
            MAR.LoadBase(Version); SetState("모드팩 설치 데이터 로드 성공"); // 모드팩 설치 데이터 로드
            string[] Dir = MAR.GetInstallData(PackAnalysisRead.PackType.Base, "Directory"); // 디렉토리 배열 받아옴
            string[] Hash = MAR.GetInstallData(PackAnalysisRead.PackType.Base, "Hash"); // 해시 배열 받아옴
            Protection Pro = new Protection();
            string BasePath = User.BSL_Root + "Base\\" + BUID + "\\" + Version + "\\"; // 모드팩 기본 경로
            string FileServer = MAR.GetInfo(PackAnalysisRead.PackType.Base, "Down");

            foreach (string tmp in Dir)
            {
                Directory.CreateDirectory(BasePath + tmp); // 디렉토리 생성
                pb_Load.PerformStep(); // 진행
                SetState("디렉토리 생성 : " + tmp);
            }

            foreach (string tmp in Hash)
            {
                string[] Data = tmp.Split('|');

                if (Pro.MD5Hash(BasePath + Data[0]) != Data[1])
                {
                    WebClient WC = new WebClient();
                    try
                    {
                        WC.DownloadFile(FileServer + Version + "\\" + Data[0], BasePath + Data[0]); // 파일 다운로드
                        SetState("다운로드 : " + Data[0]);
                    }
                    catch
                    {
                        SetState("다운로드 실패 : " + Data[0]);
                        Common.Delay(1000); // 뭐가 실패인지 사용자에게 알려주기 위해 잠시 멈춤
                    }
                }
                else
                {
                    SetState("최신버전 : " + Data[0]);
                }
                pb_Load.PerformStep(); // 진행
            }
            string[] versionData = { "Current Version|" + Version }; // 신규설치일때 기본값을 그대로 작성

            Common.WriteBDXFile(BasePath + "data.bdx", versionData); // 모드팩 버전 데이터 저장
            SetState("베이스팩 설치 완료");
        }

        /// <summary>
        /// 모드팩을 설치 및 업데이트합니다.
        /// </summary>
        /// <param name="MUID">MUID값</param>
        /// <param name="Version">설치 절대버전</param>
        /// <param name="RelativeVersion">설치 상대버전</param>
        private void InstallMod(string MUID, string Version, string RelativeVersion)
        {
            PackAnalysisRead MAR = new PackAnalysisRead(PackAnalysisRead.PackType.Mod, MUID); // 선택된 팩정보로 인스턴스 생성
            MAR.LoadMod(Version); SetState("모드팩 설치 데이터 로드 성공"); // 모드팩 설치 데이터 로드
            string[] Dir = MAR.GetInstallData(PackAnalysisRead.PackType.Mod, "Directory"); // 디렉토리 배열 받아옴
            string[] Hash = MAR.GetInstallData(PackAnalysisRead.PackType.Mod, "Hash"); // 해시 배열 받아옴
            Protection Pro = new Protection();
            string RootPath = User.BSL_Root + "ModPack\\" + MUID + "\\"; // 모드팩 루트 경로
            string PackPath = RootPath + Version + "\\"; // 모드팩 기본 경로
            string FileServer = MAR.GetInfo(PackAnalysisRead.PackType.Mod, "Down");

            foreach (string tmp in Dir)
            {
                Directory.CreateDirectory(PackPath + tmp); // 디렉토리 생성
                pb_Load.PerformStep(); // 진행
                SetState("디렉토리 생성 : " + tmp);
            }

            foreach (string tmp in Hash)
            {
                string[] Data = tmp.Split('|');

                if (Pro.MD5Hash(PackPath + Data[0]) != Data[1])
                {
                    WebClient WC = new WebClient();
                    try
                    {
                        WC.DownloadFile(FileServer + Version + "\\" + Data[0], PackPath + Data[0]); // 파일 다운로드
                        SetState("다운로드 : " + Data[0]);
                    }
                    catch
                    {
                        SetState("다운로드 실패 : " + Data[0]);
                        Common.Delay(1000); // 뭐가 실패인지 사용자에게 알려주기 위해 잠시 멈춤
                    }
                }
                else
                {
                    SetState("최신버전 : " + Data[0]);
                }
                pb_Load.PerformStep(); // 진행
            }

            string[] baseData = { "Select Version|" + RelativeVersion }; // 신규설치일때 기본값을 그대로 작성
            string[] versionData = { "Current Version|" + Version }; // 신규설치일때 기본값을 그대로 작성
            string[] localData = null;
            try
            {
                List<string> list = new List<string>();
                localData = Common.ReadBDXFile(PackPath + "data.bdx"); // 저장되있는 클라이언트 데이터를 불러옴
                foreach (string tmp in localData)
                {
                    string[] Value = tmp.Split('|');
                    switch (Value[0])
                    {
                        case "Select Version":
                            list.Add("Select Version|" + Version);
                            break;

                        default:
                            list.Add(tmp); // 현재 데이터 그대로 추가
                            break;
                    }
                }
                baseData = list.ToArray();
                SetState("모드팩 업데이트 완료");
            }
            catch
            {
                // 실패 시 신규설치로 간주
                SetState("모드팩 신규설치 완료");
            }

            Common.WriteBDXFile(RootPath + "data.bdx", baseData); // 모드팩 전체 데이터 저장
            Common.WriteBDXFile(PackPath + "data.bdx", versionData); // 모드팩 버전 데이터 저장
            SetState("모드팩 데이터 작성 완료");
        }

        /// <summary>
        /// 필요한 런타임 파일을 설치합니다.
        /// </summary>
        private void InstallRuntime()
        {
            string LocalPathTag = "x86\\";
            if (Environment.Is64BitOperatingSystem)
                LocalPathTag = "x64\\";
            try
            {
                string[] Data = Common.ReadBDXFile(User.BSN_Path + "Runtime\\JAVA\\" + LocalPathTag + "data.bdx");
                if (Data[0] == "Runtime|JAVA")
                { // 런타임 자바가 설치되어 있는경우,
                    return;
                }
            } catch { }

            // 런타임 자바가 설치되어 있지 않은경우,
            BST.BST_Runtime BSTR = new BST.BST_Runtime(BST.BST_Runtime.RunType.JAVA, FormStartPosition.CenterParent);
            if (Environment.Is64BitOperatingSystem)
                BSTR.SetJAVA(RuntimeAnalysis.JAVAType.x64);
            else
                BSTR.SetJAVA(RuntimeAnalysis.JAVAType.x86);
            BSTR.ShowDialog();
            BSTR.Dispose();
        }

        private void lst_ModPack_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_ModPack.SelectedItem != null)
            {
                cb_Version.Items.Clear(); // 반복 클릭하면 값이 중복되니 초기화!

                string[] Default = { "Latest", "Recommended" }; // 기본값 임시 저장
                string PackName = (string)lst_ModPack.SelectedItem;
                string MUID = lst_ModPack.Tag.ToString().Split('|')[lst_ModPack.SelectedIndex];
                cb_Version.Items.AddRange(Default); // 기본값 삽입!
                cb_Version.SelectedItem = "Recommended"; // 선택값을 권장버전으로 설정!
                cb_Version.Enabled = true; // 버전정보 선택 활성화!
                
                PackAnalysisRead MAR = new PackAnalysisRead(PackAnalysisRead.PackType.Mod, MUID); // 선택된 팩정보로 인스턴스 생성
                try
                {
                    // 선택 모드팩 버전 리스트 로드
                    cb_Version.Items.AddRange(MAR.GetVersion(PackAnalysisRead.PackType.Mod)); // 선택 모드팩 버전정보 삽입!
                }
                catch
                {
                    Common.Message("버전정보 로드중 문제가 발생하였습니다.");
                    return;
                }
                // 마지막에 실행했던 버전 로드
                try
                {
                    string[] modData = Common.ReadBDXFile(User.BSL_Root + "\\ModPack\\" + MUID + "\\data.bdx"); // 클라이언트 모드팩 데이터 로드
                    foreach (string tmp in modData)
                    {
                        string[] Value = tmp.Split('|');
                        if (Value[0] == "Select Version") // 데이터 집합 중 현재 버전 데이터일경우
                        {
                            cb_Version.SelectedItem = Value[1]; // 마지막 실행했던 버전으로 선택
                            break; // 필요한거 얻었응께 나머지 버리고 나와브러~
                        }
                    }
                }
                catch // 데이터 로드중 예외 오류가 날 경우 미설치
                {
                }

                wb_PackNews.AllowNavigation = true; // 뉴스페이지를 바꿔야되니 잠시 페이지 이동 허용해주고!
                string News = null;// MAR.GetInfo(ModAnalysisRead.PackType.Mod, "News");
                try
                {
                    Uri URI = new Uri(News);
                    wb_PackNews.Url = URI; // 선택 모드팩 뉴스페이지 로드!
                    while (wb_PackNews.ReadyState != WebBrowserReadyState.Complete)
                    { // 아직 로딩중일 때 페이지 변경 비허용을 시키면 페이지 로드가 안되니 로드가 완료될때까지 대기
                        Application.DoEvents(); // 무한 루프만 돌리면 UI가 렉먹으니 UI 메시지 큐 처리.
                    }
                    wb_PackNews.AllowNavigation = false; // 다시 페이지 변경 비허용!
                }
                catch
                {
                    wb_PackNews.Url = null;
                }
                if (Initialization)
                    SaveSetting(); // 설정값 저장

                cb_AutoUpdate.Enabled = true;
                cb_AutoUpdate.Checked = false;
                cb_AutoUpdate.Text = PackName + " 자동 업데이트";
                cb_AutoUpdate.Tag = MUID; // 자동업데이트 체크박스 태그를 MUID로 설정
            }
            else
            {
                cb_Version.Enabled = false; // 암것도 선택 안됬으면 버전 선택해야 의미없음!
                cb_AutoUpdate.Enabled = false;
            }
        }
        
        private void BSL_Main_Load(object sender, EventArgs e)
        {
            Initialize(); // 초기화
            lst_ModPack_SelectedIndexChanged(sender, e); // 처음 런처를 키면 버전 리스트가 제대로 로드가 안되서 직접 실행
        }
        
        private void btn_Launch_Click(object sender, EventArgs e)
        {
            // 기본값 설정 유무 확인
            if (cb_Profile.SelectedIndex == 0)
            {
                Common.Message("사용하실 프로필파일을 선택해주세요.");
                return;
            }

            // 본격적인 런칭 시작
            btn_Launch.Enabled = false;
            string MUID = lst_ModPack.Tag.ToString().Split('|')[lst_ModPack.SelectedIndex]; SetState("MUID 로드 성공");
            string SelectMod = (string)cb_Version.SelectedItem; // 모드팩 선택 버전
            string AbsoluteMod; // 모드팩 선택 절대버전
            string BUID;
            string SelectBase;
            string[] Data;

            pb_Load.Value = 0;
            pb_Load.Maximum = 0;
            pb_Load.Maximum += 5;

            // 클라이언트 설치 & 업데이트
            Data = CheckInstall(User.BSL_Root, MUID, SelectMod).Split('|');
            BUID = Data[0];
            SelectBase = Data[1];
            AbsoluteMod = Data[2];
            pb_Load.PerformStep(); // 진행
            
            BSL_Profile BSLP = new BSL_Profile((string)cb_Profile.SelectedItem); // 선택한 프로필로 데이터를 초기화함.
            string PathBase = User.BSL_Root + "Base\\" + BUID + "\\" + SelectBase + "\\";
            string PathPack = User.BSL_Root + "ModPack\\" + MUID + "\\" + AbsoluteMod + "\\";
            pb_Load.PerformStep(); // 진행
            // 계정 로그인
            User.MC_ID = BSLP.getData(BSL_Profile.Data.ID);
            User.MC_PW = BSLP.getData(BSL_Profile.Data.PW);
            string Password = User.MC_PW;

            if (User.MC_ID != string.Empty) // && User.MC_PW != null) // 레지스트리에 MC 계정정보가 저장되어있으면 로그인 실행
            {
                if (Password == string.Empty)
                {
                    BSL_Password BSLPw = new BSL_Password();
                    BSLPw.ShowDialog();
                    Password = BSLPw.getPassword();
                }

                SetState("마인크래프트 계정 로그인 시도중");
                if (MCLogin.Login(User.MC_ID, Password, MCLogin.LoginType.Authenticate))
                {
                    SetState("마인크래프트 계정 로그인 성공");
                }
                else
                {
                    SetState("마인크래프트 계정 로그인에 실패하였습니다. 아이디 또는 비밀번호를 확인해주세요.");
                    btn_Launch.Enabled = true;
                    return;
                }
            }
            else
            {
                SetState("마인크래프트 계정 로그인 실패. 프로필 파일 설정을 확인하세요.");
                btn_Launch.Enabled = true;
                return;
            }

            Enjoy(MUID, BUID, PathBase, PathPack, BSLP.getData(BSL_Profile.Data.JAVA), BSLP.getData(BSL_Profile.Data.Parameter), User.MC_NickName, User.MC_UUID, User.MC_AccessToken); SetState("클라이언트 실행 성공");
            pb_Load.PerformStep(); // 진행
            string[] tmp = {"Select Version|" + SelectMod};
            Common.WriteBDXFile(Path.Combine(User.BSL_Root, "ModPack", MUID, "data.bdx"), tmp); // 현재 실행한 버전을 저장함.
            pb_Load.PerformStep(); // 진행

            SaveSetting(); SetState("클라이언트 설정정보 저장 성공"); // 클라이언트 설정 저장.
            pb_Load.Value = pb_Load.Maximum; // 진행
            btn_Launch.Enabled = true; SetState("정상적으로 실행되었습니다.");
        }

        private void btn_Option_Click(object sender, EventArgs e)
        {
            BSL_Option BSLO = new BSL_Option();
            BSLO.ShowDialog();
        }

        private void cb_Profile_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_Edit.Enabled = false;
            btn_Launch.Enabled = false;
            if (cb_Profile.SelectedItem == null)
                return;
            if (cb_Profile.SelectedIndex == 1)
            { // 프로필 생성
                cb_Profile.SelectedIndex = 0;
                BSL_Profile BSLP = new BSL_Profile();
                BSLP.ShowDialog();
                ProfileLoad(); // 값이 바뀌었을테니 프로필 다시 로드!
                SettingLoad(); // 셋팅값 로드!
                if (BSLP.getData(BSL_Profile.Data.Name) != null) // 프로필 이름이 null이 아니라면, (프로필을 정상적으로 생성했다면,
                    cb_Profile.SelectedItem = BSLP.getData(BSL_Profile.Data.Name); // 방금 생성한 따끈따끈한 프로필파일을 선택
                SaveSetting(); // 선택 프로필이 바뀌었으므로 설정값 저장!
            }
            if (cb_Profile.SelectedIndex != 0) // 인덱스 1은 위에서 0으로 바뀌므로 0이 아닐경우 (프로필을 선택했을경우)
            {
                btn_Edit.Enabled = true; // 수정 버튼 활성화
                btn_Launch.Enabled = true;
            }
            if (Initialization && cb_Profile.SelectedIndex != 0)
                SaveSetting(); // 설정값 저장
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            // 프로필 에디터에 선택 프로필 값 전달해줌.
            BSL_Profile BSLP = new BSL_Profile((string)cb_Profile.SelectedItem);
            BSLP.ShowDialog();
            ProfileLoad(); // 값이 바뀌었을테니 프로필 다시 로드!
            SettingLoad(); // 데이터가 다시 로드됬으니 다시 셋팅!
        }

        private void BSL_Main_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            InstallRuntime();
        }
    }
}
