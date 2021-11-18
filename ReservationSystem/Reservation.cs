using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationSystem
{
    class Reservation
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Countries { get; set; }

        public Reservation(string firstName, string lastName, string countries)
        {
            FirstName = firstName;
            LastName = lastName;
            Countries = countries;
        }
    }
}