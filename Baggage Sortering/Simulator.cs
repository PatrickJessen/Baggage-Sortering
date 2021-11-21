using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Baggage_Sortering.Server;

namespace Baggage_Sortering
{
    class Simulator
    {
        private readonly Counter[] counter;
        private readonly Terminal[] terminal;
        //private readonly CentralServer server;
        private readonly Belt belt;
        private readonly ServerHandler server;

        private int bufferSize;
        private int gateSize;

        private Thread[] threads;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gateSize"></param>
        public Simulator(int gateSize = 3)
        {
            this.gateSize = gateSize;

            server = new ServerHandler();
            server.OnCannotConnect += Server_OnCannotConnect;
            server.Connect();
            counter = new Counter[gateSize];
            terminal = new Terminal[gateSize];
            bufferSize = gateSize * 3;
            belt = new Belt(10);
            Initialize();

            //server = new CentralServer(counter, terminal);
            threads = new Thread[] { new Thread(SimulateCheckIn), new Thread(SimulateSorting), new Thread(OpenCloseGates) };//, new Thread(server.OpenCloseCounters), new Thread(server.OpenCloseTerminals) };
        }

        private void Server_OnCannotConnect(object sender, EventArgs e)
        {
            Console.WriteLine(sender);
        }

        /// <summary>
        /// 
        /// </summary>
        public void StartSimulator()
        {
            for (int i = 0; i < threads.Length; i++)
                threads[i].Start();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Initialize()
        {
            for (int i = 0; i < gateSize; i++)
            {
                counter[i] = new Counter(i);
                terminal[i] = new Terminal("", i, bufferSize);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SimulateCheckIn()
        {
            CheckInManager checkIn = new CheckInManager(counter, belt, server);
            while (true)
                checkIn.StartCheckIn(this);
        }

        /// <summary>
        /// 
        /// </summary>
        private void SimulateSorting()
        {
            SortingManager sorting = new SortingManager(counter, terminal, belt, server, gateSize);
            while (true)
                sorting.StartSorting(this);
        }

        //todo: FIX THIS SHIT CODE
        private void OpenCloseGates()
        {
            while (true)
            {
                for (int i = 0; i < gateSize; i++)
                {
                    if (server.ReceiveMessage() == "c" + i.ToString())
                    {
                        counter[i].IsOpen = !counter[i].IsOpen;
                        if (counter[i].IsOpen == true)
                        {
                            server.SendMessageToServer($"Counter {i} is Open");
                        }
                        else
                            server.SendMessageToServer($"Counter {i} is Closed");

                    }
                    if (server.ReceiveMessage() == "t" + i.ToString())
                    {
                        terminal[i].IsOpen = !terminal[i].IsOpen;
                        if (terminal[i].IsOpen == true)
                        {
                            server.SendMessageToServer($"Terminal {i} is Open");
                        }
                        else
                            server.SendMessageToServer($"Terminal {i} is Closed");
                    }
                }
            }
        }
    }
}
