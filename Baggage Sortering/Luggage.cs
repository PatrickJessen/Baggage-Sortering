using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baggage_Sortering
{
    class Luggage
    {
        public Country Destination { get; set; }
        public string Owner { get; set; }
        public DateTime TimeStampIn { get; set; }
        public DateTime TimeStampOut { get; set; }

        public Luggage(Country destination, string owner)
        {
            this.Destination = destination;
            this.Owner = owner;
            TimeStampIn = CreateTimeStamp();
        }

        private DateTime CreateTimeStamp()
        {
            return DateTime.UtcNow;
        }
    }
}
