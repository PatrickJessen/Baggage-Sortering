using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Baggage_Sortering
{
    class Simulator
    {
        private Counter[] counter;
        private Terminal[] terminal;
        private CentralServer server;
        private int bufferSize;
        private int gateSize;

        private Thread[] threads;

        public Simulator(int gateSize = 3)
        {
            this.gateSize = gateSize;

            counter = new Counter[gateSize];
            terminal = new Terminal[gateSize];
            bufferSize = gateSize * 3;
            Initialize();

            server = new CentralServer(counter, terminal);
            threads = new Thread[] { new Thread(SimulateCheckIn), new Thread(SimulateSorting), new Thread(server.OpenCloseCounters), new Thread(server.OpenCloseTerminals) };
        }

        public void StartSimulator()
        {
            for (int i = 0; i < threads.Length; i++)
                threads[i].Start();
        }

        private void Initialize()
        {
            for (int i = 0; i < gateSize; i++)
            {
                counter[i] = new Counter((Country)i, i, bufferSize);
                terminal[i] = new Terminal((Country)i, i, bufferSize);
            }
        }

        private void SimulateCheckIn()
        {
            CheckInManager checkIn = new CheckInManager();
            while (true)
            {
                checkIn.StartCheckIn(this, counter);
            }
        }

        private void SimulateSorting()
        {
            SortingManager sorting = new SortingManager(gateSize);
            while (true)
            {
                sorting.StartSorting(this, counter, terminal);
            }
        }
    }
}
