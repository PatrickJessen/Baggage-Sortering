using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Baggage_Sortering
{
    class Counter : IManager
    {
        public event EventHandler OnPassengerCheckedIn;
        public event EventHandler OnLuggageSortedIn;
        public event EventHandler OnOpenCloseEvent;
        public Passenger Passenger { get; set; }
        public static Luggage[] LuggageBuffer { get; set; }
        private bool isOpen;
        public bool IsOpen
        {
            get { return isOpen; }
            set
            {
                if (isOpen != value)
                {
                    isOpen = value;
                    TriggerOnOpenCloseEvent();
                }
            }
        }

        public int Number { get; set; }
        private IBufferManager counterManager;
        private ReservationSystem reservationSystem;

        public Counter(int number)
        {
            this.Number = number;
            counterManager = new CounterManager(this);
            reservationSystem = new ReservationSystem();
        }

        public void Update(object locker)
        {
            while (true)
            {
                DateTime startTime = DateTime.Now;
                while ((DateTime.Now - startTime).Seconds < 10)
                {
                    if (IsOpen)
                        try
                        {
                            if (Terminal.LuggageBuffer[Terminal.LuggageBuffer.Length] != null)
                                Monitor.Wait(locker);
                            else
                            {
                                Monitor.Enter(locker);
                                CheckIn();
                                counterManager.AddToBuffer(this.Passenger.Luggage);
                                TriggerOnLuggageSortedIn();
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

        private void CheckIn()
        {
            this.Passenger = reservationSystem.MakeNewReservation();
            TriggerOnPassengerCheckedIn();
            Thread.Sleep(2000);
        }

        private void TriggerOnPassengerCheckedIn()
        {
            EventHandler handler = OnPassengerCheckedIn;
            if (handler != null)
            {
                OnPassengerCheckedIn($"{this.Passenger.Name} Just checked in.", EventArgs.Empty);
            }
        }

        private void TriggerOnLuggageSortedIn()
        {
            EventHandler handler = OnLuggageSortedIn;
            if (handler != null)
            {
                OnLuggageSortedIn($"{this.Passenger.Name}'s luggage was sorted in at {this.Passenger.Luggage.TimeStampIn}. And going to {Passenger.FlightPlan.Country}", EventArgs.Empty);
            }
        }

        public void TriggerOnOpenCloseEvent()
        {
            EventHandler handler = OnOpenCloseEvent;
            if (handler != null)
            {
                if (IsOpen)
                    OnOpenCloseEvent($"\nCounter number {Number} was opened", EventArgs.Empty);
                else
                    OnOpenCloseEvent($"\nCounter number {Number} was closed", EventArgs.Empty);
            }
        }
    }
}
