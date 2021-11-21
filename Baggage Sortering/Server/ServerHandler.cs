using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Baggage_Sortering.Server
{
    class ServerHandler
    {
        public event EventHandler OnCannotConnect;
        TcpClient client;
        NetworkStream stream;

        public void Connect()
        {
            client = new TcpClient();
            try
            {
                client.Connect(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString(), 80);
                stream = client.GetStream();
            }
            catch
            {
                KeepReconnecting();
            }
        }

        public void SendMessageToServer(string message)
        {
            if (client.Connected)
            {
                byte[] buffer = ASCIIEncoding.ASCII.GetBytes(message);
                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();
            }
        }

        public string ReceiveMessage()
        {
            if (client.Connected)
            {
                int bytesRead = 0;
                byte[] msgBuffer = new byte[1024];
                bytesRead = stream.Read(msgBuffer, 0, msgBuffer.Length);
                return Encoding.ASCII.GetString(msgBuffer, 0, bytesRead);
            }
            return null;
        }

        private void KeepReconnecting()
        {
            while (!client.Connected)
            {
                try
                {
                    client.Connect(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString(), 80);
                    stream = client.GetStream();
                }
                catch
                {
                    TriggerOnCannotConnect();
                    Thread.Sleep(5000);
                }
            }
        }

        private void TriggerOnCannotConnect()
        {
            EventHandler handler = OnCannotConnect;
            if (handler != null)
            {
                OnCannotConnect("Cannot connect to server. Trying to reconnect...", EventArgs.Empty);
            }
        }
    }
}
