using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Baggage_Sortering
{
    class Counter
    {
        public event EventHandler OnPassengerCheckedIn;
        public event EventHandler OnLuggageSortedIn;
        public event EventHandler OnOpenCloseEvent;
        public Passenger Passenger { get; set; }
        public Belt CounterBelt { get; set; }
        public Country Country { get; set; }
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

        public int CounterNumber { get; set; }

        public Counter(Country country, int counterNumber, int maxBeltSlots)
        {
            this.Country = country;
            this.CounterNumber = counterNumber;
            IsOpen = true;
            CounterBelt = new Belt(maxBeltSlots);
        }
        public void CheckIn(Passenger passenger)
        {
            if (IsOpen)
            {
                this.Passenger = passenger;
                TriggerOnPassengerCheckedIn();
                Thread.Sleep(1500);
                AddToBelt();
            }
        }

        private void AddToBelt()
        {
            if (!CounterBelt.IsFull)
            {
                CounterBelt.Add(this.Passenger.Luggage);
                TriggerOnLuggageSortedIn();
            }
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
                    OnOpenCloseEvent($"\nCounter number {CounterNumber} was opened", EventArgs.Empty);
                else
                    OnOpenCloseEvent($"\nCounter number {CounterNumber} was closed", EventArgs.Empty);
            }
        }
    }
}
