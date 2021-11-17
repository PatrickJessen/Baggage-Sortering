using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baggage_Sortering
{
    class Luggage
    {
        public string Destination { get; private set; }
        public string Owner { get; private set; }
        public DateTime TimeStampIn { get; private set; }
        public DateTime TimeStampOut { get { return CreateTimeStamp(); } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="owner"></param>
        public Luggage(string destination, string owner, DateTime timeStampIn)
        {
            this.Destination = destination;
            this.Owner = owner;
            TimeStampIn = timeStampIn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DateTime CreateTimeStamp()
        {
            return DateTime.UtcNow;
        }
    }
}
