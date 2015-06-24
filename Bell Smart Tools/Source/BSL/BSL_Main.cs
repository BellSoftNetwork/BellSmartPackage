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

namespace Bell_Smart_Tools.Source.BSL
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

            ModAnalysisRead MAR = new ModAnalysisRead();
            string[] PackList = MAR.GetList(ModAnalysisRead.PackType.Mod);
            List<string> PackNameList = new List<string>();

            foreach (string tmp in PackList)
            {
                MAR = new ModAnalysisRead(ModAnalysisRead.PackType.Mod, tmp); // 얻어온 MUID로 팩 정보 분석
                PackNameList.Add(MAR.GetInfo(ModAnalysisRead.PackType.Mod, "Name")); // 팩 이름 리스트 추가
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
        private void Enjoy(string MUID, string PathBase, string PathPack, string PathJAVA, string Parameter, string NickName, string UUID, string AccessToken)
        {
            string strTemp;
            StringBuilder sb = new StringBuilder(1024); //기본 문자열을 JAVA 변수, 기본 캐피시터를 1024로 하여 StringBuilder 선언.
            ModAnalysisRead MAR = new ModAnalysisRead(ModAnalysisRead.PackType.Mod, MUID); // 선택된 팩정보로 인스턴스 생성

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
            sb.Append(MUID);

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
                //Shell(strTemp, AppWinStyle.NormalFocus);
                /*BC_PID = Interaction.Shell(strTemp, AppWinStyle.NormalFocus);
                //방울크래프트 실행
                SetStatusTxt("방울크래프트 실행완료");
                RegSave("LastGame", DateString);
                this.Invoke(() => BST_Main.BC_Progress.Value == BST_Main.BC_Progress.Maximum);
                this.Invoke(() => BST_Main.BC_Status.BackColor == Color.SpringGreen);
                this.Invoke(() => BST_Main.btn_BCLaunch.Text == "방울크래프트 강제종료");
                this.Invoke(() => BST_Main.btn_BCLaunch.BackColor == Color.Red);
                this.Invoke(() => BST_Main.btn_BCLaunch.Enabled == true);
                BGW_BCC.RunWorkerAsync();
                if (DATA_USER.BST_AutoTray)
                    BST_Manager.BST_Visible = false;*/
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
        private void CheckInstall()
        {

        }

        /// <summary>
        /// 베이스팩을 설치 및 업데이트합니다.
        /// </summary>
        private void InstallBase()
        {

        }

        /// <summary>
        /// 모드팩을 설치 및 업데이트합니다.
        /// </summary>
        private void InstallMod(string MUID, string Version)
        {
            ModAnalysisRead MAR = new ModAnalysisRead(ModAnalysisRead.PackType.Mod, MUID); // 선택된 팩정보로 인스턴스 생성
            MAR.LoadMod(Version); // 모드팩 설치 데이터 로드
            string[] Dir = MAR.GetInstallData(ModAnalysisRead.PackType.Mod, "Directory"); // 디렉토리 배열 받아옴
            string[] Hash = MAR.GetInstallData(ModAnalysisRead.PackType.Mod, "Hash"); // 해시 배열 받아옴
            Protection Pro = new Protection();
            string PackPath = User.BSL_Root + "ModPack\\" + MUID + "\\"; // 모드팩 기본 경로
            string FileServer = MAR.GetInfo(ModAnalysisRead.PackType.Mod, "Down");

            foreach (string tmp in Dir)
            {
                Directory.CreateDirectory(PackPath + tmp); // 디렉토리 생성
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
            }

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
                
                ModAnalysisRead MAR = new ModAnalysisRead(ModAnalysisRead.PackType.Mod, MUID); // 선택된 팩정보로 인스턴스 생성
                try
                {
                    cb_Version.Items.AddRange(MAR.GetVersion(ModAnalysisRead.PackType.Mod)); // 선택 모드팩 버전정보 삽입!
                }
                catch
                {
                    Common.Message("버전정보 로드중 문제가 발생하였습니다.");
                    return;
                }
                wb_PackNews.AllowNavigation = true; // 뉴스페이지를 바꿔야되니 잠시 페이지 이동 허용해주고!
                string News = MAR.GetInfo(ModAnalysisRead.PackType.Mod, "News");
                Uri URI = new Uri(News);
                wb_PackNews.Url = URI; // 선택 모드팩 뉴스페이지 로드!
                while (wb_PackNews.ReadyState != WebBrowserReadyState.Complete)
                { // 아직 로딩중일 때 페이지 변경 비허용을 시키면 페이지 로드가 안되니 로드가 완료될때까지 대기
                    Application.DoEvents(); // 무한 루프만 돌리면 UI가 렉먹으니 UI 메시지 큐 처리.
                }
                wb_PackNews.AllowNavigation = false; // 다시 페이지 변경 비허용!

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
        }

        private void cb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ms_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void ms_BSL_PreferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BSL_Preferences BSLP = new BSL_Preferences();
            BSLP.ShowDialog();
        }

        private void btn_Launch_Click(object sender, EventArgs e)
        {
            btn_Launch.Enabled = false;
            string MUID = lst_ModPack.Tag.ToString().Split('|')[lst_ModPack.SelectedIndex]; SetState("MUID 로드 성공");
            ModAnalysisRead MAR = new ModAnalysisRead(ModAnalysisRead.PackType.Mod, MUID); // 선택된 팩정보로 인스턴스 생성
            InstallMod(MUID, "8.6.0"); SetState("버전 설치정보 로드 성공");
            
            CheckInstall(); // 클라이언트 설치 & 업데이트
            BSL_Profile BSLP = new BSL_Profile((string)cb_Profile.SelectedItem); // 선택한 프로필로 데이터를 초기화함.
            Enjoy(MUID, User.BSL_Root + "Base\\BCP_1.7.10\\", User.BSL_Root + "ModPack\\BellCraft8\\", BSLP.getData(BSL_Profile.Data.JAVA), BSLP.getData(BSL_Profile.Data.Parameter), User.MC_NickName, User.MC_UUID, User.MC_AccessToken); SetState("클라이언트 실행 성공");
            SaveSetting(); SetState("클라이언트 설정정보 저장 성공"); // 클라이언트 설정 저장.
            SetState("정상적으로 실행되었습니다.");
            btn_Launch.Enabled = true;
        }

        private void btn_Option_Click(object sender, EventArgs e)
        {
            BSL_Option BSLO = new BSL_Option();
            BSLO.ShowDialog();
        }

        private void cb_Profile_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_Edit.Enabled = false;
            if (cb_Profile.SelectedItem == null)
                return;
            if (cb_Profile.SelectedIndex == 1)
            {
                cb_Profile.SelectedIndex = 0;
                BSL_Profile BSLP = new BSL_Profile();
                BSLP.ShowDialog();
                ProfileLoad(); // 값이 바뀌었을테니 프로필 다시 로드!
            }
            if (cb_Profile.SelectedIndex != 0) // 인덱스 1은 위에서 0으로 바뀌므로 0이 아닐경우 (프로필을 선택했을경우)
            {
                btn_Edit.Enabled = true; // 수정 버튼 활성화
            }
            if (Initialization)
                SaveSetting(); // 설정값 저장
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            // 프로필 에디터에 선택 프로필 값 전달해줌.
            BSL_Profile BSLP = new BSL_Profile((string)cb_Profile.SelectedItem);
            BSLP.ShowDialog();
            ProfileLoad(); // 값이 바뀌었을테니 프로필 다시 로드!
        }
    }
}
