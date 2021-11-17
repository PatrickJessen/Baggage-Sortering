using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Baggage_Sortering.Server
{
    class ServerHandler
    {
        TcpClient client;
        NetworkStream stream;

        public void Connect(string address, int port)
        {
            client = new TcpClient();
            client.Connect(address, port);
        }

        public void SendMessageToServer(string message)
        {
            stream = client.GetStream();
            byte[] buffer = ASCIIEncoding.ASCII.GetBytes(message);
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();
        }
    }
}
