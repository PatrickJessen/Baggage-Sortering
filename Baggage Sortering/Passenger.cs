using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baggage_Sortering
{
    class Passenger
    {
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public Luggage Luggage { get; set; }
        public FlightPlan FlightPlan { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lastName"></param>
        /// <param name="luggage"></param>
        /// <param name="flightPlan"></param>
        public Passenger(string name, string lastName, Luggage luggage, FlightPlan flightPlan)
        {
            this.Name = name;
            this.LastName = lastName;
            this.Luggage = luggage;
            this.FlightPlan = flightPlan;
        }
    }
}
