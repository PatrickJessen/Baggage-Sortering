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

        public FlightPlan(Country country)
        {
            TakeOff = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            this.Country = country;
            TerminalNumber = SetTerminalNumber();
        }

        private int SetTerminalNumber()
        {
            switch (Country)
            {
                case Country.DK:
                    return 0;
                case Country.UK:
                    return 1;
                case Country.America:
                    return 2;
                default:
                    return -1;
            }
        }
    }
}
