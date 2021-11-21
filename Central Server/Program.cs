using System;
using System.Threading;

namespace Central_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.OnMessageReceived += Server_OnMessageReceived;
            server.StartServer();
            GateController controller = new GateController(server);
            controller.OpenCloseGate(Console.ReadLine());
        }

        private static void Server_OnMessageReceived(object sender, EventArgs e)
        {
            ChooseColor(sender.ToString());
            Console.WriteLine(sender);
        }

        private static void ChooseColor(string sender)
        {
            if (sender.Contains("checked in"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                return;
            }
            else if (sender.Contains("added"))
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                return;
            }
            else if (sender.Contains("transfered"))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                return;
            }
            else if (sender.Contains("Open"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                return;
            }
            else if (sender.Contains("Closed"))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                return;
            }
        }
    }
}
