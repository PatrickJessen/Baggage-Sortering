using System;

namespace Central_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.OnMessageReceived += Server_OnMessageReceived;
            server.StartServer();
            while (true)
            {
                server.SendMessage(Console.ReadLine());
            }
        }

        private static void Server_OnMessageReceived(object sender, EventArgs e)
        {
            Console.WriteLine(sender);
        }
    }
}
