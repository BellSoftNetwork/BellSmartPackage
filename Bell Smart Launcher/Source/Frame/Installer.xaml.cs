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
        /// 설치시 필요한 공통 필드
        // 모드팩
        bool installing = false; // 설치 진행 상황
        string modName; // 현재 선택한 모드팩 이름
        string modVer; // 현재 선택한 모드팩 버전
        string modVerid; // 모드 버전 id
        string baseVerid; // 베이스팩 버전id
        string[] baseServerList; // 베이스팩이 업로드되어있는 서버리스트
        string[] modServerList; // 모드팩이 업로드되어있는 서버리스트

        // 런타임
        string runtimeName;
        string runtimeVerid; // 설치할 런타임 버전 id
        string[] runtimeServerList; // 런타임이 업로드되어있는 서버리스트


        /// <summary>
        /// 마인크래프트 모드팩 설치를 준비합니다.
        /// </summary>
        /// <param name="modName">설치할 모드팩 이름</param>
        /// <param name="modVer">설치할 모드팩 버전</param>
        /// <param name="modVerid">설치할 모드팩 버전id</param>
        /// <param name="baseVerid">설치할 베이스팩 버전id</param>
        public Installer(string modName, string modVer, string modVerid, string baseVerid)
        {
            InitializeComponent();
            this.modName = modName;
            this.modVer = modVer;
            this.modVerid = modVerid;
            this.baseVerid = baseVerid;

            baseServerList = BSN_BSL.LoadVersionServer(BSN_BSL.PACK.basepack, BSN_BSL.KIND.client, baseVerid); // 베이스팩이 업로드되어있는 서버리스트
            modServerList = BSN_BSL.LoadVersionServer(BSN_BSL.PACK.modpack, BSN_BSL.KIND.client, modVerid); // 모드팩이 업로드되어있는 서버리스트

            this.Title = modName + " 설치";
        }

        /// <summary>
        /// 이름에 맞는 런타임 설치를 준비합니다.
        /// </summary>
        /// <param name="runtimeName">런타임 이름</param>
        public Installer(string runtimeName)
        {
            InitializeComponent();
            this.runtimeName = runtimeName;
            this.runtimeVerid = null;

            BSN_BSL.Runtime runtime = BSN_BSL.LoadRuntimeDetail(runtimeName); // 런타임 이름으로 상세정보 검색
            foreach (string value in BSN_BSL.LoadPackVersionList(BSN_BSL.PACK.runtime, runtimeName))
            { // 팩 버전리스트 검색
                if (runtime.recommended == Common.getElement(value, "version")) // 루프를 돌다가 권장버전이 나오면
                    runtimeVerid = Common.getElement(value, "id"); // 런타임 버전 id를 권장버전 id로 설정.
            }

            runtimeServerList = BSN_BSL.LoadVersionServer(BSN_BSL.PACK.runtime, BSN_BSL.KIND.client, runtimeVerid); // 런타임이 업로드되어있는 서버 탐색

            this.Title = runtimeName + " 설치";
        }

        /// <summary>
        /// 진행상황 설명을 설정합니다.
        /// </summary>
        /// <param name="Log">로그</param>
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
            int failSize = 0;
            List<string> failListBase = new List<string>();
            List<string> failListModpack = new List<string>();
            
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
                        failListBase.Add(value.url);
                        failSize += Convert.ToInt32(value.size);
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
                        failListModpack.Add(value.url);
                        failSize += Convert.ToInt32(value.size);
                        Common.Delay(1000); // 뭐가 실패인지 사용자에게 알려주기 위해 잠시 멈춤
                    }
                }
                SetState("모드팩 설치완료");
            }
            long endTime = DateTime.Now.Ticks; // 종료시간
            long installTime = (endTime - startTime) / 10000000; // 1틱은 천만분의 1초

            SetState("설치 소요시간 : " + (installTime / 60) + "분 " + (installTime % 60) + "초");
            if (failSize > 0)
            {
                StringBuilder sb = new StringBuilder(1024);

                WPFCom.Message("파일 다운로드 중 문제가 발생하여 " + failSize + "byte 만큼 설치하지 못했습니다." + Environment.NewLine + Environment.NewLine + modName + " 모드팩에서 지속적으로 설치문제가 발생할 시 해당 모드팩 관리자에게 문의 해 주시기 바랍니다.");
                if (failListBase.Count > 0)
                {
                    sb.Clear();
                    foreach (string file in failListBase)
                        sb.Append(file + ", ");
                    sb.Remove(sb.Length - 2, 2);
                    WPFCom.Message("다운로드 실패한 베이스팩 파일 리스트 : " + sb.ToString());
                }

                if (failListModpack.Count > 0)
                {
                    sb.Clear();
                    foreach (string file in failListModpack)
                        sb.Append(file + ", ");
                    sb.Remove(sb.Length - 2, 2);
                    WPFCom.Message("다운로드 실패한 모드팩 파일 리스트 : " + sb.ToString());
                }
            }
        }

        /// <summary>
        /// 런타임 설치 진행
        /// </summary>
        public void Install(string basePath)
        {
            if (runtimeVerid == string.Empty)
            {
                WPFCom.Message(runtimeName + " 런타임이 존재하지 않습니다." + Environment.NewLine + "이 에러가 계속 발생한다면, 관리자에게 문의하시기 바랍니다.");
                this.Close();
                return;
            }
            if (installing)
                return;
            installing = true;

            // 기본 필드
            long startTime = DateTime.Now.Ticks; // 시작시간

            BSN_BSL.Install[] runtimeInstall = BSN_BSL.LoadVersionFiles(BSN_BSL.PACK.runtime, BSN_BSL.KIND.client, runtimeVerid);

            /// 진행바 초기화
            pbTotal.Value = 0;
            pbTotal.Maximum = 0;

            /// 런타임 설치유무확인
            foreach (BSN_BSL.Install value in runtimeInstall)
                pbTotal.Maximum += Convert.ToDouble(value.size);
            SetState("런타임 설치준비 완료");
            
            /// 베이스팩 설치
            // 원활한 파일서버 탐색
            BSN_BSL.Server runtimeServer = null; // 파일서버정보
            foreach (string serverid in runtimeServerList)
            { // 루프돌면서 최적의 서버 탐색 (추후 개발예정)
                BSN_BSL.Server server = BSN_BSL.LoadServerDetail(serverid);
                runtimeServer = server;
            }
            SetState("최적의 런타임 파일 서버 탐색완료");

            // 파일 다운로드
            SetState("런타임 설치 시작");
            foreach (BSN_BSL.Install value in runtimeInstall)
            {
                WebClient WC = new WebClient();
                try
                {
                    string serverURL = "http://" + "cloud." + runtimeServer.address + "/" + "BSL/runtime/" + runtimeVerid + "/" + value.url;
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
            SetState("런타임 설치완료");
            
            long endTime = DateTime.Now.Ticks; // 종료시간
            long installTime = (endTime - startTime) / 10000000; // 1틱은 천만분의 1초

            SetState("설치 소요시간 : " + (installTime / 60) + "분 " + (installTime % 60) + "초");
        }
    }
}
