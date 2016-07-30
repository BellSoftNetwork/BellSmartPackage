using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Threading;

namespace BellLib.Class.Minecraft
{
    /// <summary>
    /// 마인크래프트 Bell Smart Controller 모드를 제어하는 클래스
    /// </summary>
    public class BellSmartController
    {
        private const int BSC_PORT = 3642;
        private string ProcessID;
        private bool Init;
        private bool Server_Stop;
        private bool ExceptionThrow;

        private bool ConnectTimeout;
        private bool CommunicationTimeout;

        private int ConnectTimeoutSec;
        private int CommunicationTimeoutSec;

        private TcpListener Listener;


        /// <summary>
        /// Bell Smart Controller를 초기화합니다.
        /// </summary>
        public BellSmartController(bool ExceptionThrow = false)
        {
            this.ExceptionThrow = ExceptionThrow;

            ProcessID = null;
            Init = false;
            Server_Stop = false;

            Listener = new TcpListener(IPAddress.Loopback, BSC_PORT);
        }

        /// <summary>
        /// BSC와 연결 제한시간을 설정합니다.
        /// </summary>
        /// <param name="Timeout">타임아웃 사용여부</param>
        /// <param name="TimeoutSec">타임아웃 시간 (초)</param>
        public void Set_ConnectTimeout(bool Timeout, int TimeoutSec = 300)
        {
            this.ConnectTimeout = Timeout;
            this.ConnectTimeoutSec = TimeoutSec;
        }

        /// <summary>
        /// BSC와 통신 제한시간을 설정합니다.
        /// </summary>
        /// <param name="Timeout">타임아웃 사용여부</param>
        /// <param name="TimeoutSec">통신 유휴상태 제한시간 (초)</param>
        public void Set_CommunicationTimeout(bool Timeout, int TimeoutSec = 5)
        {
            this.CommunicationTimeout = Timeout;
            this.CommunicationTimeoutSec = TimeoutSec;
        }

        /// <summary>
        /// BSC에 전달할 정보를 설정합니다.
        /// </summary>
        /// <param name="PID">프로세스 ID</param>
        public void Set_PID(string PID)
        {
            ProcessID = PID;
        }
        
        /// <summary>
        /// BSC 시스템 연동을 시작합니다.
        /// </summary>
        /// <returns>BSC 시스템 연동 성공여부</returns>
        public bool Start()
        {
            // 초기화 되지 않았으면 시작안함.
            if (!Init)
                return false;

            // 필드
            NetworkStream NS = null;

            StreamReader SR = null;
            StreamWriter SW = null;
            TcpClient client = null;

            string GetMessage = string.Empty;
            string SendData = string.Empty;
            bool Success = false;
            long StartTime = DateTime.Now.Ticks; // 연결 시작시간

            // 스타트
            try
            {
                // 연결 요청이 있을때까지 무한루프
                while (!Server_Stop && !Listener.Pending())
                {
                    if (ConnectTimeout)
                    {
                        long NowTime = DateTime.Now.Ticks; // 현재시간
                        long ProgressTime = (NowTime - StartTime) / 10000000; // 초 단위로 계산

                        if (ProgressTime > ConnectTimeoutSec) // 연결 대기상태로 타임아웃 시간을 경과하면,
                            return false; // 연동 실패로 간주
                    }

                    Common.DoEvents(); // 아니면 게임이 실행중일 수 있으니 UI 스레드를 돌려줌
                }

                // 서버 종료 요청이 들어왔을경우 바로 종료
                if (Server_Stop)
                    return false;

                client = Listener.AcceptTcpClient();

                NS = client.GetStream(); // 소켓에서 메시지를 가져오는 스트림
                SR = new StreamReader(NS, Encoding.UTF8); // Get message
                SW = new StreamWriter(NS, Encoding.UTF8); // Send message

                StartTime = DateTime.Now.Ticks; // 통신 시작시간
                while (client.Connected) //클라이언트 메시지받기
                {
                    // 수신
                    GetMessage = SR.ReadLine();
                    if (GetMessage.Contains("BSC Initialize Complete"))
                        break;

                    // 분석
                    if (GetMessage.Contains("BSC 1.0.0"))
                    {
                        if (!string.IsNullOrEmpty(ProcessID))
                            SendData = "PID=" + ProcessID;
                        else
                        {
                            if (ExceptionThrow)
                                throw new Exception("BSC 1.0.0 버전 연동하고싶으면 PID값 설정해라");

                            return false;
                        }
                    }
                    // 전송
                    if (!string.IsNullOrEmpty(SendData))
                    {
                        SW.WriteLine(SendData);
                        SW.Flush();

                        SendData = null;
                        StartTime = DateTime.Now.Ticks; // 마지막 통신시간
                    }

                    // 서버 종료 요청이 들어왔을경우 바로 종료
                    if (Server_Stop)
                        return false;

                    // 타임아웃
                    if (CommunicationTimeout)
                    {
                        long NowTime = DateTime.Now.Ticks; // 현재시간
                        long ProgressTime = (NowTime - StartTime) / 10000000; // 초 단위로 계산

                        if (ProgressTime > CommunicationTimeoutSec) // 통신 대기상태로 타임아웃 시간을 경과하면,
                            return false; // 연동 종료로 간주
                    }
                }

                Success = true;
            }
            catch (Exception e)
            {
                string Message = e.Message;

                if (ExceptionThrow)
                    throw;
            }
            finally
            {
                Stop();

                try
                {
                    // 초기화 되지 않았을때 뛰쳐나올 수도 있으니 예외처리.
                    if (SW != null)
                        SW.Close();

                    if (SR != null)
                        SR.Close();

                    if (client != null)
                        client.Close();

                    if (NS != null)
                        NS.Close();
                }
                catch (Exception e)
                {
                    string Message = e.Message;

                    if (ExceptionThrow)
                        throw;
                }
            }

            return Success;
        }

        /// <summary>
        /// BSC 시스템 이용가능 여부를 확인합니다.
        /// </summary>
        /// <param name="modsDir">mods 폴더 경로</param>
        /// <returns>BSC 시스템 이용가능여부</returns>
        public bool Feasibility(string modsDir)
        {
            try
            {
                // 모드리스트 로드
                string[] modsList = Directory.GetFiles(modsDir, "*.jar");
                foreach (string mod in modsList)
                    if (mod.Contains("BellSmartController") || mod.Contains("Bell Smart Controller"))
                        return true;
            }
            catch (Exception ex)
            {
                string Message = ex.Message;

                if (ExceptionThrow)
                    throw;
            }

            return false;
        }

        /// <summary>
        /// BSC 연결 시스템을 초기화합니다.
        /// </summary>
        /// <returns>초기화 성공여부</returns>
        public bool Initialize()
        {
            Server_Stop = false;
            Init = false;
            
            // TCP 서버 개방
            try
            {
                Listener.Start(); // Listener 동작 시작
                Init = true;
            }
            catch (Exception ex)
            {
                string temp = ex.Message;

                if (ExceptionThrow)
                    throw;
            }

            return Init;
        }
        
        /// <summary>
        /// BSC 시스템을 종료합니다.
        /// </summary>
        public void Stop()
        {
            Server_Stop = true;
            Init = false;

            try
            {
                if (Listener != null)
                    Listener.Stop();
            }
            catch (Exception e)
            {
                string Message = e.Message;

                if (ExceptionThrow)
                    throw;
            }
        }
    }
}
