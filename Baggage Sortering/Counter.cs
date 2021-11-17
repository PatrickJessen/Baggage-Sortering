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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="counterNumber"></param>
        public Counter(int counterNumber)
        {
            this.CounterNumber = counterNumber;
            IsOpen = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="passenger"></param>
        public void CheckInPassenger(Passenger passenger, Server.ServerHandler server)
        {
            this.Passenger = passenger;
            server.SendMessageToServer(SendCheckInInformation());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="belt"></param>
        public void AddLuggageToBelt(Belt belt, Server.ServerHandler server)
        {
            belt.Add(this.Passenger.Luggage);
            server.SendMessageToServer(SendLuggageInformation());
        }

        /// <summary>
        /// 
        /// </summary>
        private void TriggerOnPassengerCheckedIn()
        {
            EventHandler handler = OnPassengerCheckedIn;
            if (handler != null)
            {
                OnPassengerCheckedIn($"{this.Passenger.Name} {this.Passenger.LastName} Just checked in.", EventArgs.Empty);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void TriggerOnLuggageSortedIn()
        {
            EventHandler handler = OnLuggageSortedIn;
            if (handler != null)
            {
                OnLuggageSortedIn($"{this.Passenger.Name} {this.Passenger.LastName}'s luggage was added to the belt at {this.Passenger.Luggage.TimeStampIn}. And going to {Passenger.FlightPlan.Country}", EventArgs.Empty);
            }
        }

        public string SendCheckInInformation()
        {
            return $"{this.Passenger.Name} {this.Passenger.LastName} Just checked in.";
        }

        private string SendLuggageInformation()
        {
            return $"{this.Passenger.Name} {this.Passenger.LastName}'s luggage was added to the belt at {this.Passenger.Luggage.TimeStampIn}. And going to {Passenger.FlightPlan.Country}";
        }

        /// <summary>
        /// 
        /// </summary>
        public void TriggerOnOpenCloseEvent()
        {
            EventHandler handler = OnOpenCloseEvent;
            if (handler != null)
            {
                ForwardMessage();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ForwardMessage()
        {
            if (IsOpen)
            {
                OnOpenCloseEvent($"\nCounter number {CounterNumber} was opened", EventArgs.Empty);
                return;
            }

            OnOpenCloseEvent($"\nCounter number {CounterNumber} was closed", EventArgs.Empty);
        }
    }
}
