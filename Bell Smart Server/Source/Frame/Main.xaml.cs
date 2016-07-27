using Bell_Smart_Server.Source.Class;
using Bell_Smart_Server.Source.Data;
using Bell_Smart_Server.Source.Management;
using BellLib.Class;
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

        private DispatcherTimer tmr_SecondControl; // 빠른 업데이트 주기로 계속 제어
        private DispatcherTimer tmr_Sync; // 온라인과 연결하여 싱크 제어
        private DispatcherTimer tmr_OperatingTime; // 가동시간 제어
        private DispatcherTimer tmr_ServerControl; // 서버 제어
        private Process ServerProc;
        private BellSmartController bsc;
        private long StartTime;
        //private bool listLoading;

        /// <summary>
        /// 로그 기록 타입 열거형
        /// </summary>
        private enum LOG
        {
            NOTIFY,
            INFO,
            WARN,
            ERROR,
            LOG,
            OTHER
        }

        /// <summary>
        /// 플레이어 데이터
        /// </summary>
        public class Player
        {
            public bool select { get; set; }
            public string nickname { get; set; }
            public string ip { get; set; }
            public string jointime { get; set; }
            public string suspects { get; set; }
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
            this.MinHeight = 450;
            this.MinWidth = 1000;

            tmr_SecondControl = new DispatcherTimer(); // 초 제어 타이머 초기화
            tmr_Sync = new DispatcherTimer(); // 싱크 타이머 초기화
            tmr_OperatingTime = new DispatcherTimer(); // 가동시간 타이머 초기화
            tmr_ServerControl = new DispatcherTimer(); // 서버 제어 타이머 초기화
        }

        /// <summary>
        /// 서버를 초기화합니다.
        /// </summary>
        private void Initialize()
        {
            tmr_SecondControl.Interval = TimeSpan.FromSeconds(1); // 1초 간격
            tmr_SecondControl.Tick += new EventHandler(SecondControl_Tick);

            tmr_Sync.Interval = TimeSpan.FromSeconds(5); // 5초 간격
            tmr_Sync.Tick += new EventHandler(Sync_Tick);

            tmr_OperatingTime.Interval = TimeSpan.FromSeconds(1); // 1초 간격
            tmr_OperatingTime.Tick += new EventHandler(OperatingTime_Tick);

            tmr_ServerControl.Interval = TimeSpan.FromSeconds(60); // 60초 간격
            tmr_ServerControl.Tick += new EventHandler(ServerControl_Tick);

            tmr_SecondControl.Start(); // 1초 타이머 시작
            SecondControl_Tick(null, null);

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
        private void InitSetting(int flag = 0x03)
        {
            // 정보 (0x01)
            if ((flag & 0x01) != 0)
            {
                set_lbCurrentVersion.Content = "현재버전 : " + Deploy.CurrentVersion;
                set_lbLatestVersion.Content = "최신버전 : " + Deploy.LatestVersion;

                tmr_Sync.Start();
                Sync_Tick(null, null);
            }

            // 일반 (0x02)
            if ((flag & 0x02) != 0)
            {
                cbRemoveOldLog.IsChecked = true;
                cbLimitLogLine.IsChecked = true;
                
                try
                {
                    Server.LimitLogLine = Convert.ToInt32(DataProtect.DataLoad(DataPath.BSS.General, "LimitLogLine"));
                    txtLimitLogLine.Text = Server.LimitLogLine.ToString();
                }
                catch { }
                finally
                {
                    if (string.IsNullOrWhiteSpace(txtLimitLogLine.Text))
                        Server.LimitLogLine = 1000;
                }

                if (DataProtect.DataLoad(DataPath.BSS.General, "StartLogClear") == "True")
                    Server.StartLogClear = true;
                else
                    Server.StartLogClear = false;
                cbStartLogClear.IsChecked = Server.StartLogClear;
            }
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

            // 로그 초기화
            if (Server.StartLogClear)
                foreach (LOG log in LogList())
                    GetLogBox(log).Clear();

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

            SetState("서버 상세 설정 로드");
            ServerDetail sd = new ServerDetail((string)cbServer.SelectedItem);
            if (sd.GetData(ServerDetail.Data.AutoRestart) == "True")
                Server.AutoRestart = true;
            else
                Server.AutoRestart = false;

            SetState("BSC 시스템 초기화");
            bsc = new BellSmartController();
            if (!bsc.BSC_Init(ServerPath + "\\mods\\"))
            {
                SetState("BSC 시스템 초기화 실패");

                return;
            }

            SetState("서버 가동 시작");
            ServerProc.Start();
            ServerProc.BeginOutputReadLine();
            ServerProc.BeginErrorReadLine();

            // 마무리
            SetControl(true);
            SetStartTime();
            tmr_OperatingTime.Start();
            SetState("서버 가동 완료");

            // BSC 가동
            SetState("BSC 시스템 가동 시작");
            bsc.BSC_Set(ServerProc.Id.ToString());
            if (bsc.BSC_Start())
                SetState("BSC 시스템 연동 성공");
            else
                SetState("BSC 시스템 연동 실패");

            Controller.SetLockFlag(Controller.LockBit.Running_Server); // 업데이트 잠금
        }

        /// <summary>
        /// 서버를 종료합니다
        /// </summary>
        /// <returns>종료 성공여부</returns>
        private bool ServerStop(bool restart = false)
        {
            SendCommand("list");

            string players = lbPlayers.Content.ToString().Remove(0, 6).Split('/')[0];
            if (players != "0")
                if (WPFCom.Message("현재 접속중인 플레이어가 있습니다." + Environment.NewLine + "정말로 종료하시겠습니까?", Base.PROJECT.Bell_Smart_Server, MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.No)
                    return false;

            Server.AutoRestart = restart;
            SetState("서버 종료 요청 전송");
            SendCommand("stop");
            btnForceStop.IsEnabled = true;

            return true;
        }

        /// <summary>
        /// 서버를 재시작합니다.
        /// </summary>
        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            ServerStop(true);
        }

        /// <summary>
        /// 서버를 종료합니다.
        /// </summary>
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            ServerStop();
        }

        /// <summary>
        /// 서버를 강제종료합니다.
        /// </summary>
        private void btnForceStop_Click(object sender, RoutedEventArgs e)
        {
            if (WPFCom.Message("이 기능은 일반적인 종료요청에 서버가 응답하지 않을때 사용하는 기능입니다." + Environment.NewLine + "서버를 강제 종료하실경우 서버 중요파일에 문제가 생길 수 있습니다." + Environment.NewLine + Environment.NewLine + "정말로 강제종료 하시겠습니까?", Base.PROJECT.Bell_Smart_Server, MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.No)
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
                //bsc.BSC_Stop(); // BSC 시스템이 가동중일 수 있으므로 종료시킴
                SetState("서버 종료");
                lbPlayers.Content = "접속자 : 0/?";
                lbTPS.Content = "TPS : ?";
                Controller.SetLockFlag(Controller.LockBit.Running_Server, false); // 업데이트 잠금해제

                if (Server.AutoRestart)
                {
                    SetState("서버 재시작 대기");
                    Common.DoEvents();
                    Common.Delay(3000); // 3초 딜레이
                    btnStart_Click(sender, null);
                }
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
            
            // 서버 구동중 사용 불가 컨트롤
            cbServer.IsEnabled = !State;
            btnEdit.IsEnabled = !State;
            btnStart.IsEnabled = !State;
            btnServerSetting.IsEnabled = !State;

            // 서버 구동중 사용 가능 컨트롤
            btnPlayerRefresh.IsEnabled = State;
            btnRestart.IsEnabled = State;
            btnStop.IsEnabled = State;
            btnSave.IsEnabled = State;
            tcAdditional.IsEnabled = State;
            btnKick.IsEnabled = State;
            btnBan.IsEnabled = State;
            btnWhispers.IsEnabled = State;
            btnWarn.IsEnabled = State;
            btnGive.IsEnabled = State;
            btnPlayerRefresh.IsEnabled = State;
            btnSelectAll.IsEnabled = State;
            btnSelectCancelAll.IsEnabled = State;
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
        /// 선택된 로그 탭을 반환합니다.
        /// </summary>
        /// <returns>로그 탭</returns>
        private LOG GetCurrentLogType()
        {
            try
            {
                int LogIndex = tcLog.SelectedIndex;

                switch (LogIndex)
                {
                    case 0: // 알림
                        return LOG.NOTIFY;

                    case 1: // 정보
                        return LOG.INFO;

                    case 2: // 경고
                        return LOG.WARN;

                    case 3: // 에러
                        return LOG.ERROR;

                    case 4: // 기타
                        return LOG.OTHER;

                    case 5: // 로그
                        return LOG.LOG;
                }
            }
            catch (Exception ex)
            {
                AddLog("현재 로그탭을 반환하는 중 오류가 발생하였습니다." + Environment.NewLine + "에러내용 : " + ex.Message, LOG.ERROR);
            }

            return LOG.OTHER;
        }

        /// <summary>
        /// 로그타입에 맞는 로그 텍스트박스를 반환합니다.
        /// </summary>
        /// <returns>텍스트박스</returns>
        private TextBox GetLogBox(LOG log)
        {
            try
            {
                switch (log)
                {
                    case LOG.NOTIFY:
                        return txtNotify;

                    case LOG.INFO:
                        return txtInfo;

                    case LOG.WARN:
                        return txtWarn;

                    case LOG.ERROR:
                        return txtError;

                    case LOG.LOG:
                        return txtLog;

                    case LOG.OTHER:
                        return txtOther;
                }
            }
            catch (Exception ex)
            {
                AddLog("현재 로그박스를 반환하는 중 오류가 발생하였습니다." + Environment.NewLine + "에러내용 : " + ex.Message, LOG.ERROR);
            }

            return txtOther;
        }

        /// <summary>
        /// 오래된 로그를 삭제합니다.
        /// </summary>
        /// <param name="log">로그 타입</param>
        /// <param name="Limit">제한 줄 수</param>
        /// <returns>성공여부</returns>
        private bool RemoveOldLog(LOG log, int Limit)
        {
            try
            {
                TextBox tb = GetLogBox(log);
                int length = 0;
                int removeLine = tb.LineCount - Limit;
                
                if (removeLine > 0)
                    for (int i = 0; i < removeLine; i++)
                    {
                        length += tb.GetLineLength(i);
                        Common.DoEvents(); // 로그가 많으면 삭제하는데 오래걸리므로 UI 스레드 렉으로 인한 셧다운 방지
                    }

                if (length >= 0)
                    tb.Text = tb.Text.Remove(0, length);
                
                return true;
            }
            catch (Exception ex)
            {
                AddLog("오래된 로그 삭제중 에러 발생" + Environment.NewLine + "에러 내용 : " + ex.Message, LOG.ERROR);

                return false;
            }
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
        /// 1초간격으로 제어합니다.
        /// </summary>
        private void SecondControl_Tick(object sender, EventArgs e)
        {
            lbNowTime.Content = "현재 시간 : " + DateTime.Now.ToString();
        }

        /// <summary>
        /// 서버 출력 로그를 분석합니다.
        /// </summary>
        /// <param name="output">출력 텍스트</param>
        /// <param name="errorData">에러 데이터 여부</param>
        private void AnalysisOutput(string output, bool errorData = false)
        {
            // 유효성 검사
            if (string.IsNullOrWhiteSpace(output))
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
                /*if (listLoading)
                {
                    // 기본 : [02:19:49 INFO]: Usage: /tell <player> <message>
                    // 에센셜 : [02:19:51 INFO]: /tell <to> <message>
                    if (output.Contains("Usage: /tell <player> <message>") || output.Contains("/tell <to> <message>"))
                    {

                    }

                    return;
                }*/


                if (output.Contains("TPS from last"))
                    CheckForTPS(output);
                else if (output.Contains(" players online:") || output.Contains("명의 플레이어가 접속중입니다."))
                    CheckForPlayerList(output);
                else if (output.Contains("] logged in with entity id "))
                    ConnectPlayer(output);
                else if (output.Contains(" lost connection: "))
                    DisconnectPlayer(output);
                else if (output.Contains(" INFO]: Done ("))
                    CheckForDone(output);

                if (output.Contains(" Client attempting to join with "))
                {
                    // [20:44:34 INFO]: Client attempting to join with 132 mods : BuildCraft|
                    // 긴 출력문인 서버접속시 플레이어 모드리스트는 로그탭에 기록함.
                    AddLog(output, LOG.LOG);
                    return;
                }

                AddLog(output, LOG.INFO);
            }
            else if (output.Contains(" WARN]"))
            {
                AddLog(output, LOG.WARN);
            }
            else if (output.Contains(" ERROR]"))
                AddLog(output, LOG.ERROR);
            else
            {
                if (output.Contains("WARN Unable to instantiate org.fusesource.jansi.WindowsAnsiOutputStream"))
                {
                    AddLog(output, LOG.WARN);

                    return;
                }

                AddLog(output, LOG.OTHER);
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
                Max = temp[0];

                temp = Common.stringSplit(temp[1], "명의 플레이어가 접속중입니다.");
                Now = temp[0];
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
        /// 플레이어 접속을 분석하여 리스트를 정리합니다.
        /// </summary>
        /// <param name="output">로그 출력 데이터</param>
        private void ConnectPlayer(string output)
        {
            // [23:57:59 INFO]: Bell_[/127.0.0.1:14409] logged in with entity id 146 at ([world] -93.5, 64.0, 251.5)
            // [00:40:55 INFO]: SeA_13[/222.233.12.212:49496] logged in with entity id 1866579 at ([world] 579.9313052379114, 66.0, 953.5839232426342)
            string[] temp;
            Player player = new Player();

            try
            {
                temp = Common.stringSplit(output, " INFO]: ");
                temp = Common.stringSplit(temp[1], "] logged in with entity id ");
                temp = temp[0].Split('[');

                player.nickname = temp[0];
                player.ip = temp[1].Split(':')[0].Remove(0, 1);
                player.jointime = DateTime.Now.ToString();
                player.suspects = "0"; // 의심수치
                
                lstPlayers.Items.Add(player);
            }
            catch
            {
                AddLog("플레이어 접속 리스트 추가 실패 (" + output + ")", LOG.ERROR);
            }

            try
            {
                string nowPlayer = lbPlayers.Content.ToString().Remove(0, 6).Split('/')[0];
                int convertPlayer = Convert.ToInt32(nowPlayer); // 숫자가 아니면 catch로 이동됨
                lbPlayers.Content = lbPlayers.Content.ToString().Replace(nowPlayer + "/", (convertPlayer + 1).ToString() + "/");
            }
            catch
            {
                AddLog("접속자 추가 실패 (" + output + ")", LOG.ERROR);
            }

            lstPlayers.Items.Refresh();
        }

        /// <summary>
        /// 플레이어 접속종료를 분석하여 리스트를 정리합니다.
        /// </summary>
        /// <param name="output">로그 출력 데이터</param>
        private void DisconnectPlayer(string output)
        {
            // [23:59:12 INFO]: Bell_ lost connection: Server closed
            // [00:52:33 INFO]: abnavv lost connection: Disconnected
            // [00:51:51 INFO]: SeA_13 lost connection: Internal Exception: java.io.IOException: 현재 연결은 사용자의 호스트 시스템의 소프트웨어의 의해 중단되었습니다
            // [04:15:01 INFO]: Soft_Bell lost connection: Mod rejections [FMLMod:Bell Smart Controller{1.0.0}]
            string[] temp;
            string reason;
            Player player = new Player();

            try
            {
                temp = Common.stringSplit(output, " INFO]: ");
                temp = Common.stringSplit(temp[1], " lost connection: ");

                player.nickname = temp[0];
                reason = temp[1];

                if (reason.Contains("Mod rejections")) // 모드문제로 접속제한시 접속되지 않은 상태이므로 플레이어 제어 필요없음.
                {
                    AddLog(player.nickname + "님이 비공식 모드팩으로 접속을 시도했습니다.", LOG.NOTIFY, true);
                    try
                    {
                        string[] mods = reason.Split('[');
                        mods = mods[1].Split(']');

                        AddLog(player.nickname + "님의 거부된 모드 : " + mods[0], LOG.NOTIFY);
                    }
                    catch { }
                    
                    return;
                }

                foreach (Player pr in lstPlayers.Items)
                    if (pr.nickname == player.nickname)
                    {
                        player = pr;
                        break;
                    }
                lstPlayers.Items.Remove(player);
            }
            catch
            {
                AddLog("플레이어 접속종료 분석 실패 (" + output + ")", LOG.ERROR);
            }

            try
            {
                string nowPlayer = lbPlayers.Content.ToString().Remove(0, 6).Split('/')[0];
                int convertPlayer = Convert.ToInt32(nowPlayer); // 숫자가 아니면 catch로 이동됨
                if (convertPlayer > 0) // 버그로 인해 접속자가 음수가 되는 상황 방지
                    lbPlayers.Content = lbPlayers.Content.ToString().Replace(nowPlayer + "/", (convertPlayer - 1).ToString() + "/");
            }
            catch
            {
                AddLog("접속자 제거 실패 (" + output + ")", LOG.ERROR);
            }

            lstPlayers.Items.Refresh();
        }

        /// <summary>
        /// 서버 로그를 기록합니다.
        /// </summary>
        /// <param name="Data">로그</param>
        /// <param name="type">로그 기록 타입</param>
        private void AddLog(string Data, LOG type, bool nowTimeShow = false)
        {
            // 유효성 검증
            if (string.IsNullOrWhiteSpace(Data))
                return;

            // 텍스트박스 가져옴
            TextBox tb = GetLogBox(type);

            // 시간 출력
            if (nowTimeShow)
                Data = "[" + DateTime.Now.ToString("hh:mm:ss") + "]: " + Data;

            // 출력
            this.Dispatcher.BeginInvoke(DispatcherPriority.Send, (ThreadStart)delegate ()
            {
                tb.AppendText(Data + Environment.NewLine);
                //tb.Text = Data + Environment.NewLine;
                this.InvalidateVisual();
            });

            // 스크롤
            if ((bool)cbAutoScroll.IsChecked)
            {
                tb.ScrollToEnd();
                tb.CaretIndex = tb.Text.Length;
            }

            // 오래된 로그 삭제
            RemoveOldLog(type, Server.LimitLogLine);
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
            
            // 명령어 유지기능
            try
            {
                string[] temp = Command.Split(' ');

                // 귓속말 유지기능
                if (temp[0] == "tell")
                    txtCommand.Text = "tell " + temp[1] + " ";

                // 마지막 위치로 입력 설정
                txtCommand.CaretIndex = txtCommand.Text.Length;
            }
            catch { }
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
        /// 로그 타입 배열을 반환합니다.
        /// </summary>
        /// <returns>로그 타입</returns>
        private LOG[] LogList()
        {
            List<LOG> list = new List<LOG>();

            list.Add(LOG.NOTIFY);
            list.Add(LOG.INFO);
            list.Add(LOG.WARN);
            list.Add(LOG.ERROR);
            list.Add(LOG.OTHER);
            list.Add(LOG.LOG);

            return list.ToArray();
        }

        /// <summary>
        /// 서버 로그를 지웁니다.
        /// </summary>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(GetLogBox(GetCurrentLogType()).Text))
                if (WPFCom.Message("정말로 모든탭의 로그를 초기화하시겠습니까?", Base.PROJECT.Bell_Smart_Server, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    foreach (LOG log in LogList())
                        GetLogBox(log).Clear();

                    WPFCom.Message("모든 로그 초기화에 성공했습니다.", Base.PROJECT.Bell_Smart_Server);
                }

            try
            {
                GetLogBox(GetCurrentLogType()).Clear();
            }
            catch (Exception ex)
            {
                AddLog("로그 초기화중 오류가 발생하였습니다." + Environment.NewLine + "에러내용 : " + ex.Message, LOG.ERROR);
            }
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

        private void btnPlayerRefresh_Click(object sender, RoutedEventArgs e)
        {
            SendCommand("list");
            lstPlayers.Items.Refresh();
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
            if (cbServer.SelectedIndex < 2)
                return;

            ServerSetting ss = new ServerSetting((string)cbServer.SelectedItem);
            ss.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (ServerProc != null && !ServerProc.HasExited)
                {
                    if (WPFCom.Message("현재 서버가 가동중입니다." + Environment.NewLine + "정말로 서버를 종료하시겠습니까?", Base.PROJECT.Bell_Smart_Server, MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.No)
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
                            WPFCom.Message("서버가 종료되지 않았습니다." + Environment.NewLine + "서버를 종료하신 후 다시 시도해 주세요.", Base.PROJECT.Bell_Smart_Server);
                            e.Cancel = true;
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }

        #region *** SETTING ***

        /// <summary>
        /// 온라인 데이터를 로드하여 서버와 싱크를 맞춥니다.
        /// </summary>
        private void Sync_Tick(object sender, EventArgs e)
        {
            try
            {
                set_lbLatestVersion.Content = "최신버전 : " + Deploy.LatestVersion;
                set_lbUpdateLock.Content = "업데이트 잠금 : " + Controller.GetLockFlag()[0];

                if (Deploy.UpdateAvailable())
                {
                    set_lbUpdateLock.ToolTip = "최신버전이 발견되었습니다. 업데이트 잠금이 해제되면 업데이트 할 수 있습니다.";
                    btnUpdate.IsEnabled = false;
                }
            }
            catch
            {
                set_lbUpdateLock.Content = "업데이트 잠금 : 잠금해제";
                
                if (Deploy.UpdateAvailable())
                    btnUpdate.IsEnabled = true;
            }
        }

        #endregion

        /// <summary>
        /// 선택한 플레이어를 추방합니다.
        /// </summary>
        private void btnKick_Click(object sender, RoutedEventArgs e)
        {
            List<string> list = new List<string>();
            StringBuilder sb = new StringBuilder();

            foreach (Player player in lstPlayers.Items)
                if (player.select)
                {
                    list.Add(player.nickname);
                    sb.Append(player.nickname + ", ");
                }
            try
            {
                sb.Remove(sb.Length - 2, 2);
            }
            catch { }

            if (list.Count > 0)
            {
                if (WPFCom.Message(sb.ToString() + " 플레이어를 추방하시겠습니까?", Base.PROJECT.Bell_Smart_Server, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                    return;

                foreach (string nickname in list)
                    SendCommand("kick " + nickname);
            }
            else
                WPFCom.Message("선택된 플레이어가 없습니다.", Base.PROJECT.Bell_Smart_Server);
        }

        /// <summary>
        /// 선택한 플레이어를 영구정지합니다.
        /// </summary>
        private void btnBan_Click(object sender, RoutedEventArgs e)
        {
            List<string> list = new List<string>();
            StringBuilder sb = new StringBuilder();

            foreach (Player player in lstPlayers.Items)
                if (player.select)
                {
                    list.Add(player.nickname);
                    sb.Append(player.nickname + ", ");
                }
            try
            {
                sb.Remove(sb.Length - 2, 2);
            }
            catch { }

            if (list.Count > 0)
            {
                if (WPFCom.Message(sb.ToString() + " 플레이어를 영구정지 하시겠습니까?", Base.PROJECT.Bell_Smart_Server, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                    return;

                foreach (string nickname in list)
                    SendCommand("ban " + nickname);
            }
            else
                WPFCom.Message("선택된 플레이어가 없습니다.", Base.PROJECT.Bell_Smart_Server);
        }

        /// <summary>
        /// 선택한 플레이어에게 귓속말합니다.
        /// </summary>
        private void btnWhispers_Click(object sender, RoutedEventArgs e)
        {
            List<string> list = new List<string>();

            foreach (Player player in lstPlayers.Items)
                if (player.select)
                {
                    list.Add(player.nickname);
                }

            if (list.Count != 1)
            {
                WPFCom.Message("귓속말은 한명에게만 할 수 있습니다.", Base.PROJECT.Bell_Smart_Server);
                return;
            }

            cbSay.IsChecked = false;
            txtCommand.Text = "tell " + list[0] + " ";
        }

        /// <summary>
        /// 선택한 플레이어에게 경고를 부여합니다.
        /// </summary>
        private void btnWarn_Click(object sender, RoutedEventArgs e)
        {
            List<string> list = new List<string>();
            StringBuilder sb = new StringBuilder();

            foreach (Player player in lstPlayers.Items)
                if (player.select)
                {
                    list.Add(player.nickname);
                    sb.Append(player.nickname + ", ");
                }
            try
            {
                sb.Remove(sb.Length - 2, 2);
            }
            catch { }

            if (list.Count > 0)
            {
                if (WPFCom.Message(sb.ToString() + " 플레이어에게 경고하시겠습니까?", Base.PROJECT.Bell_Smart_Server, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                    return;
                ItemCollection playerList = lstPlayers.Items;
                foreach (string nickname in list)
                    try
                    {
                        foreach (Player player in playerList)
                        {
                            if (player.nickname == nickname)
                            {
                                int index = lstPlayers.Items.IndexOf(player);
                                lstPlayers.Items.Remove(player);
                                player.suspects = (Convert.ToInt32(player.suspects) + 1).ToString();
                                lstPlayers.Items.Insert(index, player);
                                SendCommand("say " + nickname + " 경고 누적");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        AddLog("경고 누적중 에러 발생" + Environment.NewLine + ex.Message, LOG.ERROR);
                    }
            }
            else
                WPFCom.Message("선택된 플레이어가 없습니다.", Base.PROJECT.Bell_Smart_Server);
        }

        /// <summary>
        /// 선택한 플레이어에게 아이템을 증정합니다.
        /// </summary>
        private void btnGive_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Convert.ToInt32(txtItemAmount.Text);
            }
            catch
            {
                WPFCom.Message("아이템 수량은 숫자만 입력할 수 있습니다.", Base.PROJECT.Bell_Smart_Server);
                return;
            }

            List<string> list = new List<string>();
            StringBuilder sb = new StringBuilder();

            foreach (Player player in lstPlayers.Items)
                if (player.select)
                {
                    list.Add(player.nickname);
                    sb.Append(player.nickname + ", ");
                }
            try
            {
                sb.Remove(sb.Length - 2, 2);
            }
            catch { }

            if (list.Count > 0)
            {
                if (WPFCom.Message(sb.ToString() + " 플레이어에게 " + txtItemID.Text + " 아이템을 " + txtItemAmount.Text + "개 지급하시겠습니까?", Base.PROJECT.Bell_Smart_Server, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                    return;

                foreach (string nickname in list)
                    SendCommand("give " + nickname + " " + txtItemID.Text + " " + txtItemAmount.Text);
            }
            else
                WPFCom.Message("선택된 플레이어가 없습니다.", Base.PROJECT.Bell_Smart_Server);
        }

        /// <summary>
        /// 리스트에서 모든 플레이어를 선택합니다.
        /// </summary>
        private void btnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (Player player in lstPlayers.Items)
                player.select = true;
            lstPlayers.Items.Refresh();
        }

        /// <summary>
        /// 리스트에서 모든 플레이어를 선택해제합니다.
        /// </summary>
        private void btnSelectCancelAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (Player player in lstPlayers.Items)
                player.select = false;
            lstPlayers.Items.Refresh();
        }

        /// <summary>
        /// 프로그램 업데이트를 진행합니다.
        /// </summary>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Controller.UpdateCheck())
                    btnUpdate.IsEnabled = false;
            }
            catch (Exception ex)
            {
                WPFCom.Message("업데이트 시도 중 문제가 발생하였습니다." + Environment.NewLine + "이 에러메시지가 자주 발생한다면 BSN 홈페이지 이슈트래커 게시판에 이슈를 등록 해 주시기 바랍니다." + Environment.NewLine + ex.Message + Environment.NewLine + "StackTrace : " + ex.StackTrace, Base.PROJECT.Bell_Smart_Server);
            }
        }

        /// <summary>
        /// Log Limit에 따라 오래된 로그를 삭제합니다.
        /// </summary>
        private void btnOldLogRemove_Click(object sender, RoutedEventArgs e)
        {
            btnOldLogRemove.IsEnabled = false; // 중복클릭 방지

            int OldCriteria = 1000;

            foreach (LOG log in LogList())
                RemoveOldLog(log, OldCriteria);

            btnOldLogRemove.IsEnabled = true;
            WPFCom.Message("모든탭의 오래된로그를 전부 삭제했습니다!", Base.PROJECT.Bell_Smart_Server);
        }

        /// <summary>
        /// 설정값을 저장합니다.
        /// </summary>
        private void set_btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //LogLimit = Convert.ToInt32(txtLimitLogLine.Text);
                Server.LimitLogLine = Convert.ToInt32(txtLimitLogLine.Text);
                DataProtect.DataSave(DataPath.BSS.General, "LimitLogLine", txtLimitLogLine.Text);
            }
            catch
            {
                WPFCom.Message("로그 제한 줄 수는 정수만 입력할 수 있습니다.", Base.PROJECT.Bell_Smart_Server);
                return;
            }

            Server.StartLogClear = (bool)cbStartLogClear.IsChecked;
            DataProtect.DataSave(DataPath.BSS.General, "StartLogClear", Server.StartLogClear.ToString());

            WPFCom.Message("저장이 완료되었습니다.", Base.PROJECT.Bell_Smart_Server);
        }

        /// <summary>
        /// 설정값 변경을 취소합니다.
        /// </summary>
        private void set_btnCancel_Click(object sender, RoutedEventArgs e)
        {
            InitSetting(0x02);

            WPFCom.Message("취소되었습니다.", Base.PROJECT.Bell_Smart_Server);
        }
    }
}
