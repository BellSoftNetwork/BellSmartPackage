using BellLib.Class;
using BellLib.Class.BSN;
using BellLib.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Bell_Smart_Launcher.Source.Frame
{
    /// <summary>
    /// Installer.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Installer : Window
    {
        // 설치시 필요한 공통 필드
        bool installing = false; // 설치 진행 상황
        string modName; // 현재 선택한 모드팩 이름
        string modVer; // 현재 선택한 모드팩 버전
        string modVerid; // 모드 버전 id
        string baseVerid; // 베이스팩 버전id
        string[] baseServerList; // 베이스팩이 업로드되어있는 서버리스트
        string[] modServerList; // 모드팩이 업로드되어있는 서버리스트


        public Installer(string modName, string modVer, string modVerid, string baseVerid)
        {
            InitializeComponent();
            this.modName = modName;
            this.modVer = modVer;
            this.modVerid = modVerid;
            this.baseVerid = baseVerid;
            Initialize();
        }

        /// <summary>
        /// 데이터 초기화
        /// </summary>
        private void Initialize()
        {
            /*/// 선택한 모드팩 상세정보 로드

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
            baseVerid = Common.getElement(modVerData, "basevid"); // 베이스팩 버전id*/
            baseServerList = BSN_BSL.LoadVersionServer(BSN_BSL.PACK.basepack, BSN_BSL.KIND.client, baseVerid); // 베이스팩이 업로드되어있는 서버리스트
            modServerList = BSN_BSL.LoadVersionServer(BSN_BSL.PACK.modpack, BSN_BSL.KIND.client, modVerid); // 모드팩이 업로드되어있는 서버리스트
        }

        private void SetState(string Log)
        {
            lbLog.Content = Log;
            WPFCom.DoEvents();
        }

        /// <summary>
        /// 클라이언트 설치 진행
        /// </summary>
        /// <param name="forceUpdate">강제 업데이트 여부</param>
        public void Install(bool installBase, bool installMod, string basePath, string modPath)
        {
            if (installing)
                return;
            installing = true;

            // 기본 필드
            long startTime = DateTime.Now.Ticks; // 시작시간
            
            BSN_BSL.Install[] baseInstall = BSN_BSL.LoadVersionFiles(BSN_BSL.PACK.basepack, BSN_BSL.KIND.client, baseVerid);
            BSN_BSL.Install[] modInstall = BSN_BSL.LoadVersionFiles(BSN_BSL.PACK.modpack, BSN_BSL.KIND.client, modVerid);

            /// 진행바 초기화
            pbTotal.Value = 0;
            pbTotal.Maximum = 0;
            
            /// 베이스팩 설치유무확인
            if (installBase)
            {
                foreach (BSN_BSL.Install value in baseInstall)
                    pbTotal.Maximum += Convert.ToDouble(value.size);
                SetState("베이스팩 설치준비 완료");
            }

            /// 모드팩 설치유무확인
            if (installMod)
            {
                foreach (BSN_BSL.Install value in modInstall)
                    pbTotal.Maximum += Convert.ToDouble(value.size);
                SetState("모드팩 설치준비 완료");
            }

            if (installBase) // 베이스팩 설치가 필요할시,
            {
                /// 베이스팩 설치
                // 원활한 파일서버 탐색
                BSN_BSL.Server baseServer = null; // 파일서버정보
                foreach (string serverid in baseServerList)
                { // 루프돌면서 최적의 서버 탐색 (추후 개발예정)
                    BSN_BSL.Server server = BSN_BSL.LoadServerDetail(serverid);
                    baseServer = server;
                }
                SetState("최적의 베이스팩 파일 서버 탐색완료");

                // 파일 다운로드
                SetState("베이스팩 설치 시작");
                foreach (BSN_BSL.Install value in baseInstall)
                {
                    WebClient WC = new WebClient();
                    try
                    {
                        string serverURL = "http://" + "cloud." + baseServer.address + "/" + "BSL/basepack/" + baseVerid + "/client/" + value.url;
                        string createPath = basePath;
                        if (value.url.Contains("\\")) // 파일 경로에 폴더가 존재하면,
                        {
                            string[] temp = value.url.Split('\\');
                            foreach (string dir in temp)
                                if (dir != temp[temp.Length - 1]) // 맨 마지막 파일명이 아닐경우
                                    createPath += dir + "\\"; // 디렉토리 경로만 추가
                        }
                        if (!Directory.Exists(createPath)) // 파일명을 제외한 경로가 존재하지 않으면,
                            Directory.CreateDirectory(createPath); // 디렉토리 생성
                        WC.DownloadFile(serverURL, basePath + value.url); // 파일 다운로드
                        pbTotal.Value += Convert.ToDouble(value.size); // 진행바 설정
                        SetState("다운로드 : " + value.url);
                    }
                    catch
                    {
                        SetState("다운로드 실패 : " + value.url);
                        Common.Delay(1000); // 뭐가 실패인지 사용자에게 알려주기 위해 잠시 멈춤
                    }
                }
                SetState("베이스팩 설치완료");
            }

            if (installMod) // 모드팩 설치가 필요할시,
            {
                /// 모드팩 설치
                // 원활한 파일서버 탐색
                BSN_BSL.Server modServer = null; // 파일서버정보
                foreach (string serverid in modServerList)
                { // 루프돌면서 최적의 서버 탐색 (추후 개발예정)
                    BSN_BSL.Server server = BSN_BSL.LoadServerDetail(serverid);
                    modServer = server;
                }
                SetState("최적의 모드팩 파일 서버 탐색완료");

                // 파일 다운로드
                SetState("모드팩 설치시작");
                foreach (BSN_BSL.Install value in modInstall)
                {
                    WebClient WC = new WebClient();
                    try
                    {
                        string serverURL = "http://" + "cloud." + modServer.address + "/" + "BSL/modpack/" + modVerid + "/client/" + value.url;
                        string createPath = modPath;
                        if (value.url.Contains("\\")) // 파일 경로에 폴더가 존재하면,
                        {
                            string[] temp = value.url.Split('\\');
                            foreach (string dir in temp)
                                if (dir != temp[temp.Length - 1]) // 맨 마지막 파일명이 아닐경우
                                    createPath += dir + "\\"; // 디렉토리 경로만 추가
                        }
                        if (!Directory.Exists(createPath)) // 파일명을 제외한 경로가 존재하지 않으면,
                            Directory.CreateDirectory(createPath); // 디렉토리 생성
                        WC.DownloadFile(serverURL, modPath + value.url); // 파일 다운로드
                        pbTotal.Value += Convert.ToDouble(value.size); // 진행바 설정
                        SetState("다운로드 : " + value.url);
                    }
                    catch
                    {
                        SetState("다운로드 실패 : " + value.url);
                        Common.Delay(1000); // 뭐가 실패인지 사용자에게 알려주기 위해 잠시 멈춤
                    }
                }
                SetState("모드팩 설치완료");
            }
            long endTime = DateTime.Now.Ticks; // 종료시간
            long installTime = (endTime - startTime) / 10000000; // 1틱은 천만분의 1초

            SetState("설치 소요시간 : " + (installTime / 60) + "분 " + (installTime % 60) + "초");
        }
    }
}
