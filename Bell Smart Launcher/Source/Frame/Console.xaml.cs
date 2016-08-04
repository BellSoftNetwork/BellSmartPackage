using BellLib.Class;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Bell_Smart_Launcher.Source.Frame
{
    delegate void DataRecived(object sender, DataReceivedEventArgs e);

    /// <summary>
    /// Console.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Console : Window
    {
        private bool running = true;
        public Console()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 서버 종료 이벤트
        /// </summary>
        public void Game_Exited(object sender, EventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                AddLog("게임이 종료되었습니다.", true);
                running = false;
                
                this.Close();
            }));
        }

        /// <summary>
        /// 서버 에러 데이터 수신 이벤트
        /// </summary>
        public void Game_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                AddLog(e.Data);
            }));
        }

        /// <summary>
        /// 서버 데이터 수신 이벤트
        /// </summary>
        public void Game_DataReceived(object sender, DataReceivedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                // e.Data is the line which was written to standard output
                AddLog(e.Data);
            }));
        }

        /// <summary>
        /// 콘솔 로그를 기록합니다.
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

            // 출력
            this.Dispatcher.BeginInvoke(DispatcherPriority.Send, (ThreadStart)delegate ()
            {
                txtLog.AppendText(Data + Environment.NewLine);
                //tb.Text = Data + Environment.NewLine;
                this.InvalidateVisual();
            });

            // 스크롤
            if (true)
            {
                txtLog.ScrollToEnd();
                txtLog.CaretIndex = txtLog.Text.Length;
            }

            // 오래된 로그 삭제
            RemoveOldLog(1000);
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
                    {
                        length += txtLog.GetLineLength(i);
                        Common.DoEvents(); // 로그가 많으면 삭제하는데 오래걸리므로 UI 스레드 렉으로 인한 셧다운 방지
                    }

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
