﻿using Bell_Smart_Launcher.Class;
using Bell_Smart_Launcher.Source.Data;
using BellLib.Class;
using BellLib.Class.BSN;
using BellLib.Class.Control;
using BellLib.Class.Minecraft;
using BellLib.Class.Protection;
using BellLib.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Bell_Smart_Launcher.Source.Frame
{
    /// <summary>
    /// Main 창의 Modpacks 탭 분할클래스 입니다.
    /// </summary>
    public partial class Main
    {
        #region *** INITIALIZE ***

        /// <summary>
        /// 모드팩 리스트를 초기화합니다.
        /// </summary>
        public void InitListModpack()
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
        public void InitModpacks()
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

        #endregion


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
                        Task<bool> JavaCheck = Task<bool>.Factory.StartNew(() => JavaIntegrityCheck(true));
                        if (!JavaCheck.Result) // 자바 무결성 체크 자동진행
                            WPFCom.Message("게임 실행중 예상치 못한 문제가 발생하여 종료된 것을 탐지하였습니다." + Environment.NewLine + "자바 경로 혹은 자바 파일에 문제가 있는지 확인하시기 바랍니다.", Basic.PROJECT.Bell_Smart_Launcher);
                        else
                            WPFCom.Message("자바 무결성 체크결과 문제가 없는 것을 확인하였습니다." + Environment.NewLine + "이 에러메시지가 계속 발생한다면 해당 모드팩 관리자에게 문의하시기 바랍니다.", Basic.PROJECT.Bell_Smart_Launcher);
                    }
                    else
                    {
                        if (WPFCom.Message("게임 실행중 예상치 못한 문제가 발생하여 종료된 것을 탐지하였습니다." + Environment.NewLine + "유력한 해결방안인 자바 무결성 체크를 진행하시겠습니까?", Basic.PROJECT.Bell_Smart_Launcher, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
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
                            if (WPFCom.Message("게임 실행중 충돌이 발생하였습니다." + Environment.NewLine + "충돌 보고서를 확인하시겠습니까?" + Environment.NewLine + "지속적으로 충돌이 일어날경우 해당 모드팩 관리자에게 문의하시기 바랍니다.", Basic.PROJECT.Bell_Smart_Launcher, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            {
                                try
                                {
                                    Process.Start(ReportFile);
                                }
                                catch
                                {
                                    WPFCom.Message("충돌 보고서 파일을 실행하는 중 문제가 발생하였습니다." + Environment.NewLine + "보고서 파일 위치 : " + ReportFile, Basic.PROJECT.Bell_Smart_Launcher);
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

                UpdateControl.SetLockFlag(UpdateControl.LockBit.Running_Game, false); // 업데이트 잠금해제
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
            bool BSC_Use;
            bool Exit = false;

            // 중복실행 방지
            mod_btnEnjoy.IsEnabled = false;
            UpdateControl.SetLockFlag(UpdateControl.LockBit.Running_Game); // 업데이트 잠금

            // 경로 설정
            pathResult = GameInfo.SetPath(Game.BSL_Root, Game.JAVA_Path);
            switch (pathResult)
            {
                case Modpack.ERR_PATH.Not_Load_Data:
                    WPFCom.Message("데이터가 로드되지 않아 경로를 설정할 수 없습니다.", Basic.PROJECT.Bell_Smart_Launcher);
                    UpdateControl.SetLockFlag(UpdateControl.LockBit.Running_Game, false); // 업데이트 잠금해제
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
                WPFCom.Message("옵션 설정에 실패했습니다.", Basic.PROJECT.Bell_Smart_Launcher);
                UpdateControl.SetLockFlag(UpdateControl.LockBit.Running_Game, false); // 업데이트 잠금해제
                mod_btnEnjoy.IsEnabled = true;

                return;
            }

            // BSC 초기화
            bsc = new BellSmartController();
            BSC_Use = bsc.Feasibility(GameInfo.GetPath() + "\\mods\\");

            if (BSC_Use)
            {
                bsc.Set_ConnectTimeout(true);
                bsc.Set_CommunicationTimeout(true);

                if (!bsc.Initialize())
                {
                    WPFCom.Message("BSC 시스템 초기화에 실패하였습니다.", Basic.PROJECT.Bell_Smart_Launcher);
                    mod_btnEnjoy.IsEnabled = true;

                    return;
                }
            }

            // 계정 정보 설정
            if (!GameInfo.SetAccount(MC_ID, MC_PW))
            {
                WPFCom.Message("계정정보 설정에 실패했습니다.", Basic.PROJECT.Bell_Smart_Launcher);

                Exit = true;
            }
            else
            {

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
                        WPFCom.Message("마인크래프트 계정 ID가 설정되지 않아 로그인할 수 없습니다.", Basic.PROJECT.Bell_Smart_Launcher);
                        Exit = true;

                        break;

                    case Modpack.ERR_LOGIN.No_Input_PW:
                        WPFCom.Message("마인크래프트 계정 비밀번호가 설정되지 않아 로그인할 수 없습니다.", Basic.PROJECT.Bell_Smart_Launcher);
                        Exit = true;

                        break;

                    case Modpack.ERR_LOGIN.Login_Fail:
                        WPFCom.Message("마인크래프트 로그인에 실패했습니다." + Environment.NewLine + "프로필에 아이디 또는 비밀번호를 정상적으로 저장했는지 확인 해 보시기 바랍니다." + Environment.NewLine + "혹은 짧은 시간 내에 잦은 로그인 요청으로 일정시간 접속제한을 받았을 수도 있습니다." + Environment.NewLine + "이 경우, 약 5분간 기다린 후 다시 실행 해 보십시오.", Basic.PROJECT.Bell_Smart_Launcher);
                        Exit = true;

                        break;
                }
            }

            // 마인크래프트 계정 제어 중 문제 발생시 중단
            if (Exit)
            {
                UpdateControl.SetLockFlag(UpdateControl.LockBit.Running_Game, false); // 업데이트 잠금해제
                mod_btnEnjoy.IsEnabled = true;

                // BSC 초기화 후 종료할시 BSC 시스템 종료 필요.
                bsc.Stop();
                return;
            }

            // 실행
            Console console = new Console(Game.ConsoleRun);
            launchResult = GameInfo.Launch(console.Game_DataReceived, console.Game_ErrorDataReceived, console.Game_Exited);
            switch (launchResult)
            {
                case Modpack.ERR_LAUNCH.Already_Running:
                    WPFCom.Message("게임이 이미 실행중 입니다.", Basic.PROJECT.Bell_Smart_Launcher);
                    // BSC 초기화 후 종료할시 BSC 시스템 종료 필요.
                    bsc.Stop();

                    return;

                case Modpack.ERR_LAUNCH.No_Input_Data:
                    WPFCom.Message("실행에 필요한 데이터가 정상적으로 수집되지 않아 실행할 수 없습니다.", Basic.PROJECT.Bell_Smart_Launcher);
                    Exit = true;

                    break;

                case Modpack.ERR_LAUNCH.Java_Not_Found:
                    if (WPFCom.Message("자바 경로가 비 정상적으로 설정되었습니다." + Environment.NewLine + "자바 경로 설정화면으로 이동하시겠습니까?", Basic.PROJECT.Bell_Smart_Launcher, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        tc_Main.SelectedIndex = 4;
                        game_txtJAVAPath.Focus();
                    }
                    Exit = true;

                    break;

                case Modpack.ERR_LAUNCH.Not_Installed:
                    WPFCom.Message("모드팩이 정상적으로 설치되지 않아, 실행할 수 없습니다.", Basic.PROJECT.Bell_Smart_Launcher);
                    Exit = true;

                    break;

                case Modpack.ERR_LAUNCH.Error:
                    WPFCom.Message("예상하지 못한 문제가 발생하여 실행하지 못했습니다.", Basic.PROJECT.Bell_Smart_Launcher);
                    Exit = true;

                    break;
            }

            // 게임 실행중 문제 발생시 중단
            if (Exit)
            {
                UpdateControl.SetLockFlag(UpdateControl.LockBit.Running_Game, false); // 업데이트 잠금해제
                mod_btnEnjoy.IsEnabled = true;

                // BSC 초기화 후 종료할시 BSC 시스템 종료 필요.
                bsc.Stop();
                return;
            }

            // 실행 설정
            mod_btnForceKill.IsEnabled = true;

            // 콘솔 실행
            if (Game.ConsoleRun)
                console.Show();

            // 정보 저장
            GameInfo.SetLastVersion();

            // 게임 관리 프로세스 시작
            GameNormal = false;
            tmr_GameCheck.Start();

            // BSC 구동
            if (BSC_Use)
            {
                // PID 값 설정
                bsc.Set_PID(GameInfo.GetProcessID().ToString());

                if (!bsc.Start())
                    WPFCom.Message("BSC 시스템 연동에 실패하였습니다.", Basic.PROJECT.Bell_Smart_Launcher);
            }
        }

        /// <summary>
        /// 실행중인 게임을 강제종료합니다.
        /// </summary>
        private void mod_btnForceKill_Click(object sender, RoutedEventArgs e)
        {
            if (!GameInfo.Feasibility())
            {
                GameInfo.Kill(); // 게임 종료
                bsc.Stop(); // BSC 시스템이 가동중일 수 있으므로 중단시킴
                tmr_GameCheck.Stop(); // 게임 관리 프로세스 중단
                WPFCom.Message("성공적으로 강제종료되었습니다.", Basic.PROJECT.Bell_Smart_Launcher);
            }
            else
                WPFCom.Message("실행중인 게임이 없습니다.", Basic.PROJECT.Bell_Smart_Launcher);
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
                    WPFCom.Message("데이터가 로드되지 않아 경로를 설정할 수 없습니다.", Basic.PROJECT.Bell_Smart_Launcher);

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
            mod_wbNotice.NavigateToString("<meta charset=\"utf-8\"><p>모드팩 공지사항을 가져오는중이에요!</p><p>정리되면 보여드릴게요.</p>");
            Common.DoEvents();

            noticeLock = false;
            Task loadNews = new Task(LoadMPNotice);
            loadNews.Start();

            // 좋아요 정보 로드
            mod_LoadLike();
        }

        /// <summary>
        /// 모드팩 공지사항을 출력합니다.
        /// </summary>
        private void LoadMPNotice()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                mod_wbNotice.NavigateToString(GameInfo.GetNotice());
            }));
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
                WPFCom.Message("좋아요 설정에 실패했습니다.", Basic.PROJECT.Bell_Smart_Launcher);

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
                    WPFCom.Message("버전정보를 불러오는중 문제가 발생했습니다.", Basic.PROJECT.Bell_Smart_Launcher);

                    return;

                case Modpack.ERR_LOAD.Not_Input_Version:
                    WPFCom.Message("버전정보가 설정되지 않았습니다.", Basic.PROJECT.Bell_Smart_Launcher);

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
    }
}
