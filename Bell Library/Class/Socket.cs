using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace BellLib.Class
{
    public class SocketServer
    {
        private Socket m_ServerSocket;
        private List<Socket> m_ClientSocket;
        private byte[] szData;

        public SocketServer(int Port = 1515, int backlog = 20)
        {
            m_ClientSocket = new List<Socket>();

            m_ServerSocket = new Socket(
                                AddressFamily.InterNetwork,
                                SocketType.Stream,
                                ProtocolType.Tcp);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, Port);
            m_ServerSocket.Bind(ipep);
            m_ServerSocket.Listen(backlog);

            SocketAsyncEventArgs args = new SocketAsyncEventArgs();
            args.Completed
                += new EventHandler<SocketAsyncEventArgs>(Accept_Completed);
            m_ServerSocket.AcceptAsync(args);
        }
        private void Accept_Completed(object sender, SocketAsyncEventArgs e)
        {
            Socket ClientSocket = e.AcceptSocket;
            m_ClientSocket.Add(ClientSocket);

            if (m_ClientSocket != null)
            {
                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                szData = new byte[1024];
                args.SetBuffer(szData, 0, 1024);
                args.UserToken = m_ClientSocket;
                args.Completed
                    += new EventHandler<SocketAsyncEventArgs>(Receive_Completed);
                ClientSocket.ReceiveAsync(args);
            }
            e.AcceptSocket = null;
            m_ServerSocket.AcceptAsync(e);
        }
        private void Receive_Completed(object sender, SocketAsyncEventArgs e)
        {
            Socket ClientSocket = (Socket)sender;
            if (ClientSocket.Connected && e.BytesTransferred > 0)
            {
                byte[] szData = e.Buffer;    // 데이터 수신
                string sData = Encoding.Unicode.GetString(szData);

                string Test = sData.Replace("\0", "").Trim();
                //SetText(Test);
                for (int i = 0; i < szData.Length; i++)
                {
                    szData[i] = 0;
                }
                e.SetBuffer(szData, 0, 1024);
                ClientSocket.ReceiveAsync(e);
            }
            else
            {
                ClientSocket.Disconnect(false);
                ClientSocket.Dispose();
                m_ClientSocket.Remove(ClientSocket);
            }
        }
    }

    public class SocketClient
    {
        private Socket m_ClientSocket;

        public SocketClient(string IP, int Port = 1515)
        {
            m_ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(IP), Port); //포트 대기 설정

            SocketAsyncEventArgs args = new SocketAsyncEventArgs();
            args.RemoteEndPoint = ipep;

            m_ClientSocket.ConnectAsync(args);
        }

        public void Send(string Text)
        {
            SocketAsyncEventArgs args = new SocketAsyncEventArgs();
            byte[] szData = Encoding.Unicode.GetBytes(Text);
            args.SetBuffer(szData, 0, szData.Length);
            m_ClientSocket.SendAsync(args);
        }
    }
}