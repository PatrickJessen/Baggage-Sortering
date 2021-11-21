using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Baggage_Sortering.FileManagement;
using Baggage_Sortering.Server;

namespace Baggage_Sortering
{
    class CheckInManager
    {
        private Passenger passenger;
        private readonly Counter[] counter;
        private readonly Belt belt;
        private readonly FileHandler file;
        private readonly ServerHandler server;

        public CheckInManager(Counter[] counter, Belt belt, ServerHandler server)
        {
            file = new FileHandler();
            this.counter = counter;
            this.belt = belt;
            this.server = server;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locker"></param>
        public void StartCheckIn(object locker)
        {
            try
            {
                Monitor.Enter(locker);
                WaitForReservation(locker);
                passenger = file.GetPassengerFromFile("../../../../Assets/Reservation.txt");
                if (IsBeltFull())
                    Monitor.Wait(locker);

                CheckIn();
            }
            finally
            {
                Monitor.PulseAll(locker);
                Monitor.Exit(locker);
            }
            Random rand = new Random();
            Thread.Sleep(rand.Next(1000, 5000));
        }

        /// <summary>
        /// 
        /// </summary>
        private void CheckIn()
        {
            Random rand = new Random();
            int randNum = rand.Next(0, counter.Length);

            counter[randNum].CheckInPassenger(passenger, server);
            Thread.Sleep(1000);
            counter[randNum].AddLuggageToBelt(belt, server);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsBeltFull()
        {
            if (belt.IsFull)
                return true;

            return false;
        }

        private void WaitForReservation(object locker)
        {
            if (file.IsReservationEmpty("../../../../Assets/Reservation.txt"))
            {
                Monitor.Wait(locker);
            }
        }
    }
}
