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

        public CheckInManager()
        {
            reservation = new ReservationSystem();
        }

        public void StartCheckIn(object locker, Counter[] counter)
        {

            try
            {
                Monitor.Enter(locker);
                passenger = reservation.MakeNewReservation();
                for (int i = 0; i < counter.Length; i++)
                {
                    if (counter[i].CounterBelt.IsFull)
                    {
                        Monitor.Wait(locker);
                        break;
                    }
                    else if (passenger.FlightPlan.Country == counter[i].Country)
                    {
                        counter[i].CheckIn(passenger);
                        break;
                    }
                    Thread.Sleep(1000);
                }
            }
            finally
            {
                Monitor.PulseAll(locker);
                Monitor.Exit(locker);
            }
        }
    }
}
