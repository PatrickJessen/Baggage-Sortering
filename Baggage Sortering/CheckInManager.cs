using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Baggage_Sortering
{
    class CheckInManager
    {
        private ReservationSystem reservation;
        private Passenger passenger;
        private readonly Counter[] counter;
        private readonly Belt belt;

        public CheckInManager(Counter[] counter, Belt belt)
        {
            reservation = new ReservationSystem();
            this.counter = counter;
            this.belt = belt;
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
                passenger = reservation.MakeNewReservation();

                if (!IsBeltFull())
                {
                    CheckIn();
                }
                //Monitor.Wait(locker);
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
            counter[randNum].CheckInPassenger(passenger);
            Thread.Sleep(1000);
            counter[randNum].AddLuggageToBelt(belt);
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
    }
}
