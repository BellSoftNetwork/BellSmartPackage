using Bell_Smart_Server.Source.Class;
using Bell_Smart_Server.Source.Data;
using BellLib.Class;
using BellLib.Class.Control;
using BellLib.Class.Minecraft;
using BellLib.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Bell_Smart_Server.Source.Frame
{
    /// <summary>
    /// 서버 메인의 서버제어부 분할 소스파일
    /// </summary>
    public partial class Main
    {
        #region *** FIELD ***

        private bool running; // 서버 가동 여부
        private Queue<string>[] logsQue; // 콘솔 출력 대기열
        private Task SyncConsole; // 콘솔 동기화 태스크

        private DispatcherTimer tmr_SecondControl; // 빠른 업데이트 주기로 계속 제어
        private DispatcherTimer tmr_Sync; // 온라인과 연결하여 싱크 제어
        private DispatcherTimer tmr_OperatingTime; // 가동시간 제어
        private DispatcherTimer tmr_ServerControl; // 서버 제어
        
        private BellSmartController bsc;
        private long StartTime;

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

        #endregion

        #region *** THREAD ***

        /// <summary>
        /// 서버 다중실행을 위해 멀티스레드로 BSC 시스템 연동을 시도합니다.
        /// </summary>
        private void DoStart_BSC()
        {
            SetState("BSC 시스템 사용여부 검사시작");
            bsc = new BellSmartController();

            SetState("BSC 시스템 초기화 시작");
            bsc.Set_ConnectTimeout(true);
            bsc.Set_CommunicationTimeout(true);

            if (!bsc.Initialize())
            {
                SetState("BSC 시스템 초기화 실패");
                UpdateControl.SetLockFlag(UpdateControl.LockBit.Running_Server, false); // 업데이트 잠금해제
                SetControl(false);

                return;
            }

            // BSC 가동
            SetState("BSC 시스템 가동 시작");
            // PID 값 설정
            bsc.Set_PID(ServerProc.Id.ToString());

            if (bsc.Start())
                SetState("BSC 시스템 연동 성공");
            else
                SetState("BSC 시스템 연동 실패");
        }

        /// <summary>
        /// 콘솔 로그를 동기화합니다.
        /// </summary>
        private void SyncLogTask()
        {
            while (running)
            {
                const int MAX_LOG = 5;

                // 모든 로그탭 출력
                for (int log = 0; log <= MAX_LOG; log++)
                {
                    // 초기화
                    string Data = null;

                    // 밀린 로그 수집
                    for (int i = 0; i < logsQue[log].Count; i++)
                        Data += logsQue[log].Dequeue() + Environment.NewLine;

                    // 로그 출력
                    TextBox tb = GetLogBox((LOG)log);
                    this.Dispatcher.BeginInvoke(DispatcherPriority.Send, (ThreadStart)delegate ()
                    {
                        tb.AppendText(Data);
                        this.InvalidateVisual();

                        // 오래된 로그 삭제
                        RemoveOldLog((LOG)log, Server.LimitLogLine);

                        // 스크롤
                        if (!string.IsNullOrEmpty(Data) && (bool)cbAutoScroll.IsChecked)
                        {
                            tb.ScrollToEnd();
                            tb.CaretIndex = tb.Text.Length;
                        }
                    });
                }

                Thread.Sleep(100); // 0.1초에 한번씩 동기화
            }
        }

        #region ** TIMER **

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
            SendCommand("list"); // list 정보 로드
        }

        /// <summary>
        /// 1초간격으로 제어합니다.
        /// </summary>
        private void SecondControl_Tick(object sender, EventArgs e)
        {
            lbNowTime.Content = "현재 시간 : " + DateTime.Now.ToString();
        }

        #endregion

        #endregion

        #region *** EVENT ***

        /// <summary>
        /// 서버 종료 이벤트
        /// </summary>
        private void ServerProc_Exited(object sender, EventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                running = false;
                SetControl(false);
                tmr_OperatingTime.Stop(); // 가동시간 계산 타이머 중단
                tmr_ServerControl.Stop(); // 서버제어 타이머 중단
                bsc.Stop(); // BSC 시스템이 가동중일 수 있으므로 종료시킴
                SetState("서버 종료");
                lbPlayers.Content = "접속자 : 0/?";
                lbTPS.Content = "TPS : ?";
                UpdateControl.SetLockFlag(UpdateControl.LockBit.Running_Server, false); // 업데이트 잠금해제

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

        #region *** LOG ***

        #region ** LOG CONTROL **

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
            else if (output.Contains("명의 플레이어가 접속중입니다."))
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

        #endregion

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

            // 시간 출력
            if (nowTimeShow)
                Data = "[" + DateTime.Now.ToString("hh:mm:ss") + "]: " + Data;

            // 대기열에 추가
            logsQue[(int)type].Enqueue(Data);
        }

        /// <summary>
        /// 상황 그룹에 상태 레이블 컨텐츠를 설정합니다.
        /// </summary>
        /// <param name="state">상태 텍스트</param>
        private void SetState(string state)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                lbState.Content = "상태 : " + state;
            }));
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

        #endregion

        #region *** CONTROL ***

        /// <summary>
        /// 서버를 가동합니다.
        /// </summary>
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            // 사전 초기화
            SetState("서버 초기화 시작");
            UpdateControl.SetLockFlag(UpdateControl.LockBit.Running_Server); // 업데이트 잠금
            SetControl(true);

            // 필드
            ServerProfile profile = new ServerProfile((string)cbServer.SelectedItem); // 선택한 서버로 데이터 초기화

            string ServerPath = profile.GetData(ServerProfile.Data.ServerPath);
            string ServerFile = profile.GetData(ServerProfile.Data.ServerFile);
            string JavaPath = profile.GetData(ServerProfile.Data.JavaPath);
            string Parameter = profile.GetData(ServerProfile.Data.Parameter);
            bool BSC_Use = false;

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

            SetState("BSC 시스템 사용여부 검사시작");
            if (bsc != null)
                bsc.Stop();
            bsc = new BellSmartController();
            BSC_Use = bsc.Feasibility(ServerPath + "\\mods\\");

            if (BSC_Use)
            {
                SetState("BSC 시스템 초기화 시작");
                bsc.Set_ConnectTimeout(true);
                bsc.Set_CommunicationTimeout(true);

                if (!bsc.Initialize())
                {
                    SetState("BSC 시스템 초기화 실패");
                    UpdateControl.SetLockFlag(UpdateControl.LockBit.Running_Server, false); // 업데이트 잠금해제
                    SetControl(false);

                    return;
                }

                // 초기화 시도하면서 서버가 열려있으므로 서버 닫아줌.
                bsc.Stop();
            }

            SetState("서버 가동 시작");
            ServerProc.Start();
            ServerProc.BeginOutputReadLine();
            ServerProc.BeginErrorReadLine();

            // 마무리
            running = true;
            SetStartTime();
            tmr_OperatingTime.Start(); // 가동시간 타이머
            tmr_ServerControl.Start(); // 서버제어 타이머
            logsQue = new Queue<string>[6];
            for (int log = 0; log <= 5; log++)
                logsQue[log] = new Queue<string>(); // 5개의 로그탭 대기열
            SyncConsole = new Task(SyncLogTask);
            SyncConsole.Start();
            SetState("서버 가동 완료");

            // BSC 가동
            if (BSC_Use)
            {
                Thread workerThread = new Thread(DoStart_BSC);
                workerThread.Start();
            }
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
            bsc.Stop();
        }
        
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

        #endregion

        #region *** METHOD ***

        /// <summary>
        /// 서버 상태에 따라 주요 컨트롤을 활성화/비활성화 합니다.
        /// </summary>
        /// <param name="State">서버 작동 상태</param>
        private void SetControl(bool State)
        {
            Dispatcher.Invoke(new Action(() =>
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
            }));
        }

        /// <summary>
        /// 서버 시작시간을 설정합니다.
        /// </summary>
        private void SetStartTime()
        {
            lbStartTime.Content = "시작 시간 : " + DateTime.Now.ToString();
            StartTime = DateTime.Now.Ticks; // 시작시간 설정
        }

        #endregion
    }
}
