﻿using Bell_Smart_Server.Source.Class;
using Bell_Smart_Server.Source.Management;
using BellLib.Class;
using BellLib.Class.Protection;
using BellLib.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Threading;
using Xceed.Wpf.Toolkit;

namespace Bell_Smart_Server.Source.Frame
{
    /// <summary>
    /// Main.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Main : Window
    {
        #region *** FIELD ***

        private DispatcherTimer tmr_OperatingTime;
        private DispatcherTimer tmr_ServerControl;
        private Process ServerProc;
        private long StartTime;

        /// <summary>
        /// 로그 기록 타입 열거형
        /// </summary>
        private enum LOG
        {
            INFO,
            WARN,
            ERROR
        }

        #endregion

        #region *** INITIALIZE ***

        /// <summary>
        /// 서버 메인화면을 초기화합니다.
        /// </summary>
        public Main()
        {
            InitializeComponent();
            PreInitialize();
        }

        /// <summary>
        /// 서버창을 보여주기 전 1회 초기화합니다.
        /// </summary>
        private void PreInitialize()
        {
            this.MinHeight = 400;
            this.MinWidth = 900;

            tmr_OperatingTime = new DispatcherTimer(); // 가동시간 타이머 초기화
            tmr_ServerControl = new DispatcherTimer(); // 서버 제어 타이머 초기화
        }

        /// <summary>
        /// 서버를 초기화합니다.
        /// </summary>
        private void Initialize()
        {
            tmr_OperatingTime.Interval = TimeSpan.FromSeconds(1); // 1초간격
            tmr_OperatingTime.Tick += new EventHandler(OperatingTime_Tick);

            tmr_ServerControl.Interval = TimeSpan.FromSeconds(60); // 60초간격
            tmr_ServerControl.Tick += new EventHandler(ServerControl_Tick);

            InitServer();
            InitSetting();
        }

        /// <summary>
        /// 서버리스트를 초기화합니다.
        /// </summary>
        private void InitServer()
        {
            ServerLoad();
        }

        /// <summary>
        /// 세팅탭을 초기화합니다.
        /// </summary>
        private void InitSetting()
        {
            set_lbVersion.Content = "프로그램 버전 : " + Deploy.CurrentVersion;
        }

        /// <summary>
        /// 서버 프로필 데이터를 로드합니다.
        /// </summary>
        private void ServerLoad()
        {
            // 초기화
            cbServer.Items.Clear();

            // 로드
            foreach (string value in ServerProfile.GetProfileList(true))
                cbServer.Items.Add(value);

            // 프로필 선택
            cbServer.SelectedItem = ServerProfile.GetLastProfile();

            if (cbServer.SelectedIndex == -1)
                cbServer.SelectedIndex = 0;
        }

        #endregion

        #region *** CONTROL ***

        /// <summary>
        /// 서버를 가동합니다.
        /// </summary>
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            SetState("서버 초기화 시작");
            ServerProfile profile = new ServerProfile((string)cbServer.SelectedItem); // 선택한 서버로 데이터 초기화

            string ServerPath = profile.GetData(ServerProfile.Data.ServerPath);
            string ServerFile = profile.GetData(ServerProfile.Data.ServerFile);
            string JavaPath = profile.GetData(ServerProfile.Data.JavaPath);
            string Parameter = profile.GetData(ServerProfile.Data.Parameter);

            SetState("서버 가동 준비");
            var startInfo = new ProcessStartInfo(JavaPath, Parameter + " -jar " + ServerFile + " nogui");
            startInfo.WorkingDirectory = ServerPath;
            startInfo.RedirectStandardOutput = startInfo.RedirectStandardInput = startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            ServerProc = new Process();
            ServerProc.StartInfo = startInfo;
            ServerProc.EnableRaisingEvents = true;

            ServerProc.OutputDataReceived += new DataReceivedEventHandler(DataReceived);
            ServerProc.ErrorDataReceived += new DataReceivedEventHandler(ServerProc_ErrorDataReceived);
            ServerProc.Exited += new EventHandler(ServerProc_Exited);

            SetState("서버 가동 시작");
            ServerProc.Start();
            ServerProc.BeginOutputReadLine();
            ServerProc.BeginErrorReadLine();

            // 마무리
            SetControl(true);
            SetStartTime();
            tmr_OperatingTime.Start();
            SetState("서버 가동 완료");
            Controller.SetLockFlag(Controller.LockBit.Running_Server); // 업데이트 잠금
        }

        /// <summary>
        /// 서버를 종료합니다.
        /// </summary>
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            SetState("서버 종료 요청 전송");
            SendCommand("stop");
            btnForceStop.IsEnabled = true;
        }

        /// <summary>
        /// 서버를 강제종료합니다.
        /// </summary>
        private void btnForceStop_Click(object sender, RoutedEventArgs e)
        {
            if (WPFCom.Message("이 기능은 일반적인 종료요청에 서버가 응답하지 않을때 사용하는 기능입니다." + Environment.NewLine + "서버를 강제 종료하실경우 서버 중요파일에 문제가 생길 수 있습니다." + Environment.NewLine + Environment.NewLine + "정말로 강제종료 하시겠습니까?", "Bell Smart Server", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.No)
                return;

            SetState("서버 강제종료 요청");
            if (!ServerProc.HasExited)
                ServerProc.Kill();
        }

        /// <summary>
        /// 서버 저장 명령어를 전송합니다.
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SendCommand("save-all");
            SetState("서버 저장 요청 전송");
        }

        /// <summary>
        /// 서버를 재시작합니다.
        /// </summary>
        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            SetState("서버 중단");

            SetState("서버 가동");
        }

        #region ** EVENT **

        /// <summary>
        /// 서버 종료 이벤트
        /// </summary>
        private void ServerProc_Exited(object sender, EventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                SetControl(false);
                tmr_OperatingTime.Stop(); // 가동시간 계산 타이머 중단
                SetState("서버 종료");
                lbPlayers.Content = "접속자 : 0/?";
                lbTPS.Content = "TPS : ?";
                Controller.SetLockFlag(Controller.LockBit.Running_Server, false); // 업데이트 잠금해제
            }));
        }

        /// <summary>
        /// 서버 에러 데이터 수신 이벤트
        /// </summary>
        private void ServerProc_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                AnalysisOutput(e.Data, true);
            }));
        }

        /// <summary>
        /// 서버 데이터 수신 이벤트
        /// </summary>
        public void DataReceived(object sender, DataReceivedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                // e.Data is the line which was written to standard output
                AnalysisOutput(e.Data);
            }));
        }

        #endregion

        /// <summary>
        /// 명령어를 서버에 전송합니다.
        /// </summary>
        public void SendCommand(string input)
        {
            try
            {
                ServerProc.StandardInput.WriteLine(input);
            }
            catch
            {
                AddLog("[BSS] 명령어 입력 실패 (" + input + ")", LOG.ERROR);
            }
        }

        /// <summary>
        /// 서버 상태에 따라 주요 컨트롤을 활성화/비활성화 합니다.
        /// </summary>
        /// <param name="State">서버 작동 상태</param>
        private void SetControl(bool State)
        {
            btnForceStop.IsEnabled = false;

            cbServer.IsEnabled = !State;
            btnEdit.IsEnabled = !State;
            btnStart.IsEnabled = !State;

            btnStop.IsEnabled = State;
            btnSave.IsEnabled = State;
            tcAdditional.IsEnabled = State;
        }

        /// <summary>
        /// 상황 그룹에 상태 레이블 컨텐츠를 설정합니다.
        /// </summary>
        /// <param name="state">상태 텍스트</param>
        private void SetState(string state)
        {
            lbState.Content = "상태 : " + state;
        }

        /// <summary>
        /// 서버 시작시간을 설정합니다.
        /// </summary>
        private void SetStartTime()
        {
            lbStartTime.Content = "시작 시간 : " + DateTime.Now.ToString();
            StartTime = DateTime.Now.Ticks; // 시작시간 설정
        }
        
        /// <summary>
        /// 타이머를 통해 시작시간을 기준으로 가동시간을 계산합니다.
        /// </summary>
        private void OperatingTime_Tick(object sender, EventArgs e)
        {
            // 필드
            long NowTime = DateTime.Now.Ticks;
            long OperatingTime = (NowTime - StartTime) / 10000000;
            int day, hour, min, sec;

            // 로드
            sec = (int)(OperatingTime % 60); // 60으로 나눈 나머지 (초)
            min = (int)((OperatingTime / 60) % 60); // 60초로 나눈 값(분)에서 60으로 나눈 값의 나머지 (60분 이상 버림)
            hour = (int)((OperatingTime / (60 * 60)) % 24); // 60 * 60초로 나눈 값(시간)에서 60으로 나눈 값의 나머지 (24시간 이상 버림)
            day = (int)(OperatingTime / (60 * 60 * 24)); // 60 * 60 * 24초로 나눈 값(일)

            // 출력
            lbOperatingTime.Content = "가동 시간 : " + day + "일 " + hour + "시간 " + min + "분 " + sec + "초";
        }

        /// <summary>
        /// 타이머를 통해 서버를 제어합니다.
        /// </summary>
        private void ServerControl_Tick(object sender, EventArgs e)
        {
            SendCommand("tps"); // tps 정보 로드
        }

        /// <summary>
        /// 서버 출력 로그를 분석합니다.
        /// </summary>
        /// <param name="output">출력 텍스트</param>
        /// <param name="errorData">에러 데이터 여부</param>
        private void AnalysisOutput(string output, bool errorData = false)
        {
            // 유효성 검사
            if (output == null)
                return;

            // 출력
            if (errorData)
            {
                AddLog(output, LOG.ERROR);
                return;
            }

            if (output.Contains(" INFO]"))
            {
                // 정보 분석 함수
                if (output.Contains("TPS from last"))
                    CheckForTPS(output);
                else if (output.Contains(" players online:") || output.Contains("명의 플레이어가 접속중입니다."))
                    CheckForPlayerList(output);
                else if (output.Contains(" INFO]: Done ("))
                    CheckForDone(output);

                AddLog(output, LOG.INFO);
            }
            else if (output.Contains(" WARN]"))
            {
                AddLog(output, LOG.WARN);
            }
            else
            {
                if (output.Contains("WARN Unable to instantiate org.fusesource.jansi.WindowsAnsiOutputStream"))
                {
                    AddLog(output, LOG.WARN);

                    return;
                }

                AddLog(output, LOG.INFO);
            }
        }

        /// <summary>
        /// 서버 가동 완료메시지를 확인합니다.
        /// </summary>
        /// <param name="output">로그 출력 데이터</param>
        private void CheckForDone(string output)
        {
            // [22:10:58 INFO]: Done (1.679s)! For help, type "help" or "?"
            if (!output.Contains(" INFO]: Done ("))
                AddLog("서버 가동 확인 실패 (" + output + ")", LOG.ERROR);
            else
                SetState("서버 정상 가동중 (접속 가능)");

            SendCommand("tps");
            SendCommand("list");
        }

        /// <summary>
        /// 플레이어 목록을 확인합니다.
        /// </summary>
        /// <param name="output">로그 출력 데이터</param>
        private void CheckForPlayerList(string output)
        {
            // [22:26:44 INFO]: There are 0/20 players online:
            // [22:24:18 INFO]: 최대 40명이 접속 가능하고, 2명의 플레이어가 접속중입니다.
            string[] temp;
            string Now = "0", Max = "?";

            if (output.Contains(" players online:"))
            {
                temp = Common.stringSplit(output, " INFO]: There are ");
                temp = Common.stringSplit(temp[1], " players online:");
                temp = temp[0].Split('/');

                Now = temp[0];
                Max = temp[1];
            }
            else if(output.Contains("명의 플레이어가 접속중입니다."))
            {
                temp = Common.stringSplit(output, " INFO]: 최대 ");
                temp = Common.stringSplit(temp[1], "명이 접속 가능하고, ");
                Now = temp[0];

                temp = Common.stringSplit(temp[1], "명의 플레이어가 접속중입니다.");
                Max = temp[0];
            }

            lbPlayers.Content = "접속자 : " + Now + "/" + Max;
        }

        /// <summary>
        /// 서버 TPS를 확인합니다.
        /// </summary>
        /// <param name="output">로그 출력 데이터</param>
        private void CheckForTPS(string output)
        {
            // [22:01:14 INFO]: TPS from last 1m, 5m, 15m: 19.98, 19.99, 19.99
            // 필드
            string[] temp;
            string tps;

            // 로드
            temp = output.Split(':');
            temp = temp[temp.Length - 1].Split(',');
            tps = temp[0];

            // 출력
            lbTPS.Content = "TPS : " + tps;
        }

        /// <summary>
        /// 서버 로그를 기록합니다.
        /// </summary>
        /// <param name="Data">로그</param>
        /// <param name="type">로그 기록 타입</param>
        private void AddLog(string Data, LOG type)
        {
            switch (type)
            {
                case LOG.INFO:
                    txtInfo.Text += Data + Environment.NewLine;
                    txtInfo.ScrollToEnd();
                    txtInfo.CaretIndex = txtInfo.Text.Length;

                    break;

                case LOG.WARN:
                    txtWarn.Text += Data + Environment.NewLine;
                    txtWarn.ScrollToEnd();
                    txtWarn.CaretIndex = txtWarn.Text.Length;

                    break;

                case LOG.ERROR:
                    txtError.Text += Data + Environment.NewLine;
                    txtError.ScrollToEnd();
                    txtError.CaretIndex = txtError.Text.Length;

                    break;
            }
        }

        #endregion

        #region *** ADD-ON ***

        /// <summary>
        /// 명령어를 서버에 전송합니다.
        /// </summary>
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            // 필드
            string Command = txtCommand.Text;

            // 유효성 검증
            if (Command == string.Empty)
                return;

            // 추가 옵션
            if (cbSay.IsChecked == true)
                Command = "say " + Command;

            // 명령어 전송
            SendCommand(Command);

            // 마무으리
            txtCommand.Clear();
            btnSend.IsEnabled = false;
        }

        /// <summary>
        /// 명령어 입력시 키보드를 통한 간편명령기능을 지원합니다.
        /// </summary>
        private void txtCommand_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    btnSend_Click(sender, e);

                    break;

                case Key.Up:
                    // 이전에 입력한 명령어 로드

                    break;

                case Key.Down:
                    // 이후에 입력한 명령어 로드

                    break;
            }
        }

        /// <summary>
        /// 서버 로그를 지웁니다.
        /// </summary>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtInfo.Clear();
            txtWarn.Clear();
            txtError.Clear();
        }

        /// <summary>
        /// 명령어 입력값을 확인합니다.
        /// </summary>
        private void txtCommand_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtCommand.Text == string.Empty)
                btnSend.IsEnabled = false;
            else
                btnSend.IsEnabled = true;
        }

        #endregion

        #region *** PROFILE ***

        /// <summary>
        /// 프로필 수정모드로 엽니다.
        /// </summary>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (cbServer.SelectedIndex < 2)
                return;
            ServerEditor pro = new ServerEditor((string)cbServer.SelectedItem);
            pro.ShowDialog();
            ServerLoad(); // 값이 바뀌었을테니 프로필 다시 로드!
        }

        /// <summary>
        /// 프로필 리스트 변경을 검사 후 선택값을 저장합니다.
        /// </summary>
        private void cbServer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 유효성 검증
            if (cbServer.IsInitialized)
                if (cbServer.SelectedIndex > 1)
                    btnStart.IsEnabled = true;
                else
                    btnStart.IsEnabled = false;

            // 프로필 제어
            if (cbServer.SelectedIndex == 1)
            {
                cbServer.SelectedIndex = 0;
                ServerEditor Pro = new ServerEditor();
                Pro.ShowDialog();
                ServerLoad(); // 값이 바뀌었을테니 프로필 다시 로드!

                if (Pro.GetSaveName() != null) // 프로필 이름이 null이 아니라면, (프로필을 정상적으로 생성했다면,
                    cbServer.SelectedItem = Pro.GetSaveName(); // 방금 생성한 따끈따끈한 프로필파일을 선택
            }

            // 선택값 저장
            if (cbServer.IsInitialized && cbServer.SelectedIndex > -1)
                ServerProfile.SetLastProfile((string)cbServer.SelectedItem); // 선택 프로필이 바뀌었으므로 설정값 저장!
        }

        #endregion
        

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            Initialize();
        }

        /// <summary>
        /// 서버 설정창을 실행합니다.
        /// </summary>
        private void btnServerSetting_Click(object sender, RoutedEventArgs e)
        {
            ServerSetting ss = new ServerSetting();
            ss.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ServerProc != null && !ServerProc.HasExited)
            {
                if (WPFCom.Message("현재 서버가 가동중입니다." + Environment.NewLine + "정말로 서버를 종료하시겠습니까?", "Bell Smart Server", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.No)
                {
                    e.Cancel = true;
                    return;
                }

                try
                {
                    SendCommand("stop");
                    ServerProc.WaitForExit(10000);
                    if (!ServerProc.HasExited)
                    {
                        WPFCom.Message("서버가 종료되지 않았습니다." + Environment.NewLine + "서버를 종료하신 후 다시 시도해 주세요.", "Bell Smart Server");
                        e.Cancel = true;
                    }
                }
                catch { }
            }
        }
    }
}