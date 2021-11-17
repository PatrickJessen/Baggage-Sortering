using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Central_Server
{
    class Server
    {
        public event EventHandler OnMessageReceived;

        TcpListener server = null;
        NetworkStream netStream = null;
        TcpClient client = null;

        public void StartServer()
        {
            IPAddress localAddr = IPAddress.Parse(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString());
            server = new TcpListener(localAddr, 80);
            server.Start();
            Thread t = new Thread(StartListener);
            t.Start();
            //StartListener();

        }

        private void StartListener()
        {
            try
            {
                while (true)
                {
                    client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                    netStream = client.GetStream();
                    Thread t = new Thread(ReceiveMessage);
                    t.Start();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
                server.Stop();
            }
        }

        private void ReceiveMessage()
        {
            while (client.Connected)
            {
                int bytesRead = 0;
                byte[] msgBuffer = new byte[1024];
                bytesRead = netStream.Read(msgBuffer, 0, msgBuffer.Length);
                OnMessageReceivedTrigger(Encoding.ASCII.GetString(msgBuffer, 0, bytesRead));
            }
        }

        public void SendMessage(string message)
        {
            netStream = client.GetStream();
            byte[] buffer = ASCIIEncoding.ASCII.GetBytes(message);
            netStream.Write(buffer, 0, buffer.Length);
            netStream.Flush();
        }


        private void OnMessageReceivedTrigger(string message)
        {
            EventHandler handler = OnMessageReceived;
            if (handler != null)
            {
                OnMessageReceived(message, EventArgs.Empty);
            }
        }
    }
}
