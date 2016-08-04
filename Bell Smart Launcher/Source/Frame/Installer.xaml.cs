using Bell_Smart_Launcher.Class;
using BellLib.Class;
using BellLib.Class.BSN;
using BellLib.Class.Control;
using BellLib.Class.Protection;
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

        Modpack.InstallData install; // 모드팩 설치정보

        // 런타임
        string runtimeName;
        string runtimeVerid; // 설치할 런타임 버전 id
        string[] runtimeServerList; // 런타임이 업로드되어있는 서버리스트

                
        /// <summary>
        /// 모드팩 설치정보로 설치를 준비합니다.
        /// </summary>
        /// <param name="install">설치정보</param>
        public Installer(Modpack.InstallData install)
        {
            InitializeComponent();

            this.install = install;
            this.Title = install.Name + " " + install.Version + " 설치";
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
        private void SetStatus(string Log)
        {
            lbLog.Content = Log;
            Common.DoEvents();
        }

        /// <summary>
        /// 클라이언트 설치 진행
        /// </summary>
        public void Install(bool Overwrite = false)
        {
            if (installing)
                return;
            installing = true;
            while (UpdateControl.GetLockFlag().Contains(UpdateControl.LockBit.Install_Game))
            {
                SetStatus("현재 다른 모드팩이 설치중입니다.");
                Common.Delay(1000);
            }
            UpdateControl.SetLockFlag(UpdateControl.LockBit.Install_Game); // 업데이트 잠금

            // 기본 필드
            long startTime = DateTime.Now.Ticks; // 시작시간
            int failSize = 0;
            List<string> failList = new List<string>();
            Protect pro = new Protect();

            // 진행바 초기화
            pbTotal.Value = 0;
            pbTotal.Maximum = install.FullCapacity;

            SetStatus("설치준비 완료");

            // 팩 정보 저장
            DataProtect.DataSave(install.PathPack + "data.bdx", "Name", install.Name);
            DataProtect.DataSave(install.PathPack + "data.bdx", "State", "Setup");
            DataProtect.DataSave(install.PathVersion + "config.bdx", "Version", install.Version);
            DataProtect.DataSave(install.PathVersion + "config.bdx", "State", "Setup");

            // 파일 다운로드
            SetStatus("설치 시작");
            foreach (BSN_BSL.Install value in install.File)
            {
                WebClient WC = new WebClient();

                try
                {
                    string serverURL = "http://" + "cloud." + install.Server.address + "/" + "BSL/" + install.pack.ToString() + "/" + install.VersionID + "/client/" + value.url;
                    string createPath = install.PathVersion;
                    if (value.url.Contains("\\")) // 파일 경로에 폴더가 존재하면,
                    {
                        string[] temp = value.url.Split('\\');
                        foreach (string dir in temp)
                            if (dir != temp[temp.Length - 1]) // 맨 마지막 파일명이 아닐경우
                                createPath += dir + "\\"; // 디렉토리 경로만 추가
                    }
                    if (!Directory.Exists(createPath)) // 파일명을 제외한 경로가 존재하지 않으면,
                        Directory.CreateDirectory(createPath); // 디렉토리 생성

                    if (Overwrite || !File.Exists(install.PathVersion + value.url)) // 덮어쓰기를 허용했거나 로컬디스크에 파일이 없을때
                    {
                        WC.DownloadFile(serverURL, install.PathVersion + value.url); // 파일 다운로드
                        SetStatus("다운로드 : " + value.url);
                    }
                    else // 파일이 있을때
                        if (pro.MD5Hash(install.PathVersion + value.url) != value.hash) // 서버에 등록된 MD5해시와 이미 설치된 파일의 해시가 다르면,
                        {
                            WC.DownloadFile(serverURL, install.PathVersion + value.url); // 파일 다운로드
                            SetStatus("변경된 파일 다운로드 : " + value.url);
                        }
                        else // 같으면 이미 설치된걸로 판단, 재 다운로드를 하지 않음으로 네트워크 사용량을 줄임
                            SetStatus("이미 설치됨 : " + value.url);
                    pbTotal.Value += Convert.ToDouble(value.size); // 진행바 설정
                }
                catch
                {
                    SetStatus("다운로드 실패 : " + value.url);
                    failList.Add(value.url);
                    failSize += Convert.ToInt32(value.size);
                    Common.Delay(1000); // 뭐가 실패인지 사용자에게 알려주기 위해 잠시 멈춤
                }
            }

            // 팩 상태 저장
            DataProtect.DataSave(install.PathPack + "data.bdx", "State", "Installed");
            DataProtect.DataSave(install.PathVersion + "config.bdx", "State", "Installed");
            UpdateControl.SetLockFlag(UpdateControl.LockBit.Install_Game, false); // 업데이트 잠금해제

            SetStatus("설치완료");

            // 소요시간 표시
            long endTime = DateTime.Now.Ticks; // 종료시간
            long installTime = (endTime - startTime) / 10000000; // 1틱은 천만분의 1초

            SetStatus("설치 소요시간 : " + (installTime / 60) + "분 " + (installTime % 60) + "초");

            // 다운로드 실패 알림
            if (failSize > 0)
            {
                StringBuilder sb = new StringBuilder(1024);

                WPFCom.Message("파일 다운로드 중 문제가 발생하여 " + failSize + "byte 만큼 설치하지 못했습니다." + Environment.NewLine + Environment.NewLine + install.Name + " 모드팩에서 지속적으로 설치문제가 발생할 시 해당 모드팩 관리자에게 문의 해 주시기 바랍니다.", Base.PROJECT.Bell_Smart_Launcher);
                if (failList.Count > 0)
                {
                    sb.Clear();
                    foreach (string file in failList)
                        sb.Append(file + ", ");
                    sb.Remove(sb.Length - 2, 2);
                    WPFCom.Message("다운로드 실패한 파일 리스트 : " + sb.ToString(), Base.PROJECT.Bell_Smart_Launcher);
                }
            }

            Common.Delay(10000); // 소요시간 확인할 시간좀 주고
            for (int i = 10; i > 0; i--)
            {
                SetStatus(i + "초 후 설치기가 자동으로 닫힙니다.");
                Common.Delay(1000);
            }

            this.Close();
        }
        
        /// <summary>
        /// 런타임 설치 진행
        /// </summary>
        public void Install(string basePath)
        {
            if (runtimeVerid == string.Empty)
            {
                WPFCom.Message(runtimeName + " 런타임이 존재하지 않습니다." + Environment.NewLine + "이 에러가 계속 발생한다면, 관리자에게 문의하시기 바랍니다.", Base.PROJECT.Bell_Smart_Launcher);
                this.Close();
                return;
            }
            if (installing)
                return;
            installing = true;
            UpdateControl.SetLockFlag(UpdateControl.LockBit.Install_Runtime); // 업데이트 잠금

            // 기본 필드
            long startTime = DateTime.Now.Ticks; // 시작시간

            BSN_BSL.Install[] runtimeInstall = BSN_BSL.LoadVersionFiles(BSN_BSL.PACK.runtime, BSN_BSL.KIND.client, runtimeVerid);

            /// 진행바 초기화
            pbTotal.Value = 0;
            pbTotal.Maximum = 0;

            /// 런타임 설치유무확인
            foreach (BSN_BSL.Install value in runtimeInstall)
                pbTotal.Maximum += Convert.ToDouble(value.size);
            SetStatus("런타임 설치준비 완료");
            
            // 원활한 파일서버 탐색
            BSN_BSL.Server runtimeServer = null; // 파일서버정보
            foreach (string serverid in runtimeServerList)
            { // 루프돌면서 최적의 서버 탐색 (추후 개발예정)
                BSN_BSL.Server server = BSN_BSL.LoadServerDetail(serverid);
                runtimeServer = server;
            }
            SetStatus("최적의 런타임 파일 서버 탐색완료");

            // 파일 다운로드
            SetStatus("런타임 설치 시작");
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
                    SetStatus("다운로드 : " + value.url);
                }
                catch
                {
                    SetStatus("다운로드 실패 : " + value.url);
                    Common.Delay(1000); // 뭐가 실패인지 사용자에게 알려주기 위해 잠시 멈춤
                }
            }
            SetStatus("런타임 설치완료");
            UpdateControl.SetLockFlag(UpdateControl.LockBit.Install_Runtime, false); // 업데이트 잠금해제

            long endTime = DateTime.Now.Ticks; // 종료시간
            long installTime = (endTime - startTime) / 10000000; // 1틱은 천만분의 1초

            SetStatus("설치 소요시간 : " + (installTime / 60) + "분 " + (installTime % 60) + "초");
        }
    }
}
