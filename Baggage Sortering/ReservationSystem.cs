using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baggage_Sortering
{
    class ReservationSystem
    {
        public Passenger MakeNewReservation()
        {
            FlightPlan flightPlan = new FlightPlan(GenerateRandomCountry());
            NameGenerator names = new NameGenerator();
            string name = names.GenerateName();

            return new Passenger(name, new Luggage(flightPlan.Country, name), flightPlan);
        }

        private Country GenerateRandomCountry()
        {
            Random rand = new Random();
            int enumeLength = Enum.GetNames(typeof(Country)).Length;
            return (Country)rand.Next(0, enumeLength);
        }
    }
}
