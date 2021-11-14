using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baggage_Sortering
{
    class ReservationSystem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Passenger MakeNewReservation()
        {
            FlightPlan flightPlan = new FlightPlan(GenerateRandomCountry());
            NameGenerator names = new NameGenerator();
            string name = names.GenerateName();
            string lastName = names.GenerateLastName();

            return new Passenger(name, lastName, new Luggage(flightPlan.Country, name + " " + lastName), flightPlan);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Country GenerateRandomCountry()
        {
            Random rand = new Random();
            int enumeLength = Enum.GetNames(typeof(Country)).Length;
            return (Country)rand.Next(0, enumeLength);
        }
    }
}
