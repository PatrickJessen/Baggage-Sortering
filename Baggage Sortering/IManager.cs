using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baggage_Sortering
{
    interface IManager
    {
        event EventHandler OnOpenCloseEvent;
        static Luggage[] LuggageBuffer { get; set; }
        Passenger Passenger { get; set; }
        int Number { get; set; }
        bool IsOpen
        {
            get { return IsOpen; }
            set
            {
                if (IsOpen != value)
                {
                    IsOpen = value;
                    TriggerOnOpenCloseEvent();
                }
            }
        }

        void TriggerOnOpenCloseEvent();
    }
}
