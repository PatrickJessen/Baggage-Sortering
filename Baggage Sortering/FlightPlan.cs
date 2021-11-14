using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baggage_Sortering
{
    enum Country
    {
        DK, UK, America
    }
    class FlightPlan
    {
        public DateTime TakeOff { get; private set; }
        public Country Country { get; private set; }
        public int TerminalNumber { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="country"></param>
        public FlightPlan(Country country)
        {
            TakeOff = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            TakeOff.AddSeconds(10);
            this.Country = country;
            TerminalNumber = (int)country;
        }
    }
}
