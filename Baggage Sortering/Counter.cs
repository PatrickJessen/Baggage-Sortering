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
        public Passenger Passenger { get; set; }
        public bool IsOpen { get; set; }

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

        public string SendCheckInInformation()
        {
            return $"{this.Passenger.Name} {this.Passenger.LastName} Just checked in.";
        }

        private string SendLuggageInformation()
        {
            return $"{this.Passenger.Name} {this.Passenger.LastName}'s luggage was added to the belt at {this.Passenger.Luggage.TimeStampIn}. And going to {Passenger.FlightPlan.Country}";
        }
    }
}
