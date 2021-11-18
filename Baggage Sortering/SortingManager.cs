using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Baggage_Sortering.Server;

namespace Baggage_Sortering
{
    class SortingManager
    {
        private readonly Counter[] counter;
        private readonly Terminal[] terminal;
        private readonly Belt belt;
        private readonly ServerHandler server;

        private int gateSize;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="counter"></param>
        /// <param name="terminal"></param>
        /// <param name="belt"></param>
        /// <param name="gateSize"></param>
        public SortingManager(Counter[] counter, Terminal[] terminal, Belt belt, ServerHandler server, int gateSize)
        {
            this.gateSize = gateSize;
            this.counter = counter;
            this.terminal = terminal;
            this.belt = belt;
            this.server = server;
        }

        /// <summary>
        /// Starts sorting luggage
        /// </summary>
        /// <param name="locker">The object to monitor</param>
        public void StartSorting(object locker)
        {
            Random rand = new Random();
            Thread.Sleep(rand.Next(1500, 3000));
            try
            {
                Monitor.Enter(locker);
                if (CanBeSorted())
                {
                    SortLuggage();
                }
                Monitor.Wait(locker);    
            }
            finally
            {
                Monitor.PulseAll(locker);
                Monitor.Exit(locker);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SortLuggage()
        {
            for (int j = 0; j < gateSize; j++)
            {
                terminal[j].TakeInLuggage(belt.GetFirst(), server);
                belt.RemoveFirst();
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="j"></param>
        /// <returns></returns>
        private bool IsDestinationSame(int j)
        {
            if (terminal[j].Destination == belt.GetFirst().Destination)
                return true;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CanBeSorted()
        {
            //DateTime time = belt.GetFirst().TimeStampIn.AddSeconds(5);
            if (belt.GetFirst() != null && DateTime.UtcNow > belt.GetFirst().TimeStampIn.AddSeconds(15))
                return true;
            return false;
        }
    }
}
