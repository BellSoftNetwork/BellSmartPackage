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
    public class BellSmartController
    {
        private const int BSC_PORT = 3642;
        private string ProcessID;
        private bool BSC_Use;
        private bool Server_Stop;

        private TcpListener Listener;


        /// <summary>
        /// Bell Smart Controller를 초기화합니다.
        /// </summary>
        public BellSmartController()
        {
            Server_Stop = false;
        }

        /// <summary>
        /// BSC에 전달할 정보를 설정합니다.
        /// </summary>
        /// <param name="PID">프로세스 ID</param>
        public void BSC_Set(string PID)
        {
            ProcessID = PID;
        }

        /// <summary>
        /// BSC 시스템을 시작합니다.
        /// </summary>
        public bool BSC_Start()
        {
            // BSC를 사용하지 않으면 시작안함
            if (!BSC_Use)
                return true;

            // 필드
            NetworkStream NS = null;

            StreamReader SR = null;
            StreamWriter SW = null;
            TcpClient client = null;

            string GetMessage = string.Empty;
            string SendData = string.Empty;
            bool Success = false;
            long StartTime = DateTime.Now.Ticks; // 시작시간

            // 스타트
            try
            {
                // 연결 요청이 있을때까지 무한루프
                while (!Server_Stop && !Listener.Pending())
                {
                    long NowTime = DateTime.Now.Ticks; // 현재시간
                    long ProgressTime = (NowTime - StartTime) / 10000000;
                    if (ProgressTime > 180) // 연결 대기상태로 180초를 경과하면,
                        // BSC_Stop(); // finally 에서 실행됨.
                        return false; // 연동 실패로 간주
                    else
                        Common.DoEvents(); // 아니면 게임이 실행중일 수 있으니 UI 스레드를 돌려줌
                }

                if (Server_Stop)
                    return false;

                client = Listener.AcceptTcpClient();

                NS = client.GetStream(); // 소켓에서 메시지를 가져오는 스트림
                SR = new StreamReader(NS, Encoding.UTF8); // Get message
                SW = new StreamWriter(NS, Encoding.UTF8); // Send message

                while (client.Connected == true) //클라이언트 메시지받기
                {
                    // 수신
                    GetMessage = SR.ReadLine();
                    if (GetMessage.Contains("BSC Initialize Complete"))
                        break;

                    // 분석
                    if (GetMessage.Contains("BSC 1.0.0"))
                        SendData = "PID=" + ProcessID;

                    // 전송
                    if (!string.IsNullOrEmpty(SendData))
                    {
                        SW.WriteLine(SendData);
                        SW.Flush();
                    }
                }

                Success = true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            finally
            {
                BSC_Stop();
            }

            try
            {
                // 초기화 되지 않았을때 뛰쳐나올 수도 있으니 예외처리.
                SW.Close();
                SR.Close();
                client.Close();
                NS.Close();
            }
            catch { }

            return Success;
        }

        private void BSC_Connect()
        {
            try
            {
                // 필드
                IPEndPoint ipep = new IPEndPoint(IPAddress.Loopback, BSC_PORT);
                Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                string BSC_Ver = string.Empty, sendData = null;
                byte[] _data;

                // 연결 준비
                server.Bind(ipep);
                server.Listen(20);

                // 연결 대기
                Socket client = server.Accept();
                IPEndPoint ip = (IPEndPoint)client.RemoteEndPoint;

                // 클라이언트 접속됨
                _data = new byte[1024];
                sendData = "Connect";
                _data = Encoding.UTF8.GetBytes(sendData);
                client.Send(_data);

                _data = new byte[1024];
                client.Receive(_data);
                BSC_Ver = Encoding.UTF8.GetString(_data);

                // BSC 버전 받아옴
                while (!BSC_Ver.Contains("BSC Initialize Complete"))
                {
                    // 버전별 데이터 분류
                    if (!BSC_Ver.Contains("BSC Initialize Complete"))
                    {
                        if (BSC_Ver.Contains("BSC 1.0.0"))
                            sendData = "PID=" + ProcessID;

                        // 데이터 전송
                        if (!string.IsNullOrEmpty(sendData))
                        {
                            _data = Encoding.UTF8.GetBytes(sendData + Environment.NewLine);
                            client.Send(_data);
                        }

                        // 다시 입력받음
                        _data = new byte[1024];
                        client.Receive(_data);
                        BSC_Ver = Encoding.UTF8.GetString(_data);
                    }
                }

                // 연결처리 완료시 전부 닫아줌.
                client.Shutdown(SocketShutdown.Both);
                client.Close();
                server.Shutdown(SocketShutdown.Both);
                server.Close();
            }
            catch { }
        }

        /// <summary>
        /// BSC를 연결 시스템을 초기화합니다.
        /// </summary>
        /// <returns>초기화 성공여부</returns>
        public bool BSC_Init(string modsDir)
        {
            BSC_Use = false;
            try
            {
                // 모드리스트 로드
                string[] modsList = Directory.GetFiles(modsDir, "*.jar");
                foreach (string mod in modsList)
                    if (mod.Contains("BellSmartController"))
                    {
                        // Bell Smart Controller 모드가 있으면 BSC 구동
                        BSC_Use = true;

                        break;
                    }
            }
            catch { }

            // BSC 모드가 없으면 초기화 끝.
            if (!BSC_Use)
                return true;

            // BSC 모드가 있으면 TCP 서버 개방
            try
            {
                Listener = new TcpListener(IPAddress.Loopback, BSC_PORT);
                Listener.Start(); // Listener 동작 시작

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// BSC 시스템을 종료합니다.
        /// </summary>
        public void BSC_Stop()
        {
            Server_Stop = true;

            try
            {
                Listener.Stop();
            }
            catch { }
        }
    }
}
