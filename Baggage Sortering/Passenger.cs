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
        public Luggage Luggage { get; set; }
        public FlightPlan FlightPlan { get; set; }

        public Passenger(string name, Luggage luggage, FlightPlan flightPlan)
        {
            this.Name = name;
            this.Luggage = luggage;
            this.FlightPlan = flightPlan;
        }
    }
}
