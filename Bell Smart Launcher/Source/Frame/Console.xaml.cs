using BellLib.Class;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
    delegate void DataRecived(object sender, DataReceivedEventArgs e);

    /// <summary>
    /// Console.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Console : Window
    {
        // 필드
        private bool useConsole;
        private bool running;
        private Queue<string> logsQue;
        private Task SyncConsole;

        public Console(bool useConsole)
        {
            InitializeComponent();
            this.useConsole = useConsole;
            running = true;
            logsQue = new Queue<string>();
            SyncConsole = new Task(SyncLogTask);
            if (useConsole)
                SyncConsole.Start();
        }

        /// <summary>
        /// 서버 종료 이벤트
        /// </summary>
        public void Game_Exited(object sender, EventArgs e)
        {
            AddLog("게임이 종료되었습니다.", true);
            running = false;

            Dispatcher.Invoke(new Action(() =>
            {
                this.Close();
            }));
        }

        /// <summary>
        /// 서버 에러 데이터 수신 이벤트
        /// </summary>
        public void Game_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (useConsole)
                AddLog(e.Data);
        }

        /// <summary>
        /// 서버 데이터 수신 이벤트
        /// </summary>
        public void Game_DataReceived(object sender, DataReceivedEventArgs e)
        {
            if (useConsole)
                AddLog(e.Data);
        }

        /// <summary>
        /// 출력 할 콘솔 로그를 대기열에 추가합니다.
        /// </summary>
        /// <param name="Data">로그</param>
        private void AddLog(string Data, bool nowTimeShow = false)
        {
            // 유효성 검증
            if (string.IsNullOrWhiteSpace(Data))
                return;
            
            // 시간 출력
            if (nowTimeShow)
                Data = "[" + DateTime.Now.ToString("hh:mm:ss") + "]: " + Data;

            // 대기열에 추가
            logsQue.Enqueue(Data);
        }

        /// <summary>
        /// 콘솔 로그를 동기화합니다.
        /// </summary>
        private void SyncLogTask()
        {
            while (running)
            {
                string Data = null;

                // 밀린 로그 수집
                for (int i = 0; i < logsQue.Count; i++)
                    Data += logsQue.Dequeue() + Environment.NewLine;

                // 데이터 검사
                if (!string.IsNullOrWhiteSpace(Data))
                    this.Dispatcher.BeginInvoke(DispatcherPriority.Send, (ThreadStart)delegate ()
                    {
                        // 밀린 로그 한번에 출력
                        txtLog.AppendText(Data);
                        this.InvalidateVisual();

                        // 오래된 로그 삭제
                        RemoveOldLog(500);

                        // 스크롤
                        txtLog.ScrollToEnd();
                        txtLog.CaretIndex = txtLog.Text.Length;
                    });

                Thread.Sleep(100); // 0.1초에 한번씩 동기화
            }
        }

        /// <summary>
        /// 오래된 로그를 삭제합니다.
        /// </summary>
        /// <param name="Limit">제한 줄 수</param>
        /// <returns>성공여부</returns>
        private bool RemoveOldLog(int Limit)
        {
            try
            {
                int length = 0;
                int removeLine = txtLog.LineCount - Limit;

                if (removeLine > 0)
                    for (int i = 0; i < removeLine; i++)
                        length += txtLog.GetLineLength(i);

                if (length >= 0)
                    txtLog.Text = txtLog.Text.Remove(0, length);

                return true;
            }
            catch (Exception ex)
            {
                AddLog("오래된 로그 삭제중 에러 발생" + Environment.NewLine + "에러 내용 : " + ex.Message);

                return false;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (running)
                e.Cancel = true;
        }
    }
}
