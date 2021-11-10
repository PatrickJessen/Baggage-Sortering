using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Baggage_Sortering
{
    class SortingManager
    {
        private Counter[] counter;
        private Terminal[] terminal;
        private int bufferSize;
        private int gateSize;
        private ReservationSystem reservationSystem;

        public SortingManager(int gateSize = 3)
        {
            this.gateSize = gateSize;

            counter = new Counter[gateSize];
            terminal = new Terminal[gateSize];
            bufferSize = gateSize * 3;

            Initialize();
        }

        private void Initialize()
        {
            for (int i = 0; i < gateSize; i++)
            {
                if (counter[i] == null)
                {
                    counter[i] = new Counter((Country)i, i, bufferSize);
                }

                if (terminal[i] == null)
                {
                    terminal[i] = new Terminal(i, bufferSize);
                }
            }
        }

        public void StartSorting()
        {
            while (true)
            {
                DateTime startTime = DateTime.Now;
                while ((DateTime.Now - startTime).Seconds < 5)
                {
                    ReservationSystem reservationSystem = new ReservationSystem();
                    Passenger passenger = reservationSystem.MakeNewReservation();
                    int index = (int)passenger.FlightPlan.Country;
                    if (IsBeltFull(index))
                        Monitor.Wait(this);
                    try
                    {
                        Monitor.Enter(this);
                        counter[index].CheckIn(passenger);
                        Thread.Sleep(2000);
                        terminal[index].TransferLuggageToTerminal(counter[index].CounterBelt.Luggage[0]);
                    }
                    finally
                    {
                        Monitor.PulseAll(this);
                        Monitor.Exit(this);
                    }
                }
            }
        }

        private bool IsBeltFull(int index)
        {
            if (counter[index].CounterBelt.IsFull)
                return true;
            return false;
        }
    }
}
