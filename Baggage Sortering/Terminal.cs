using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baggage_Sortering
{
    class Terminal : IManager
    {
        public event EventHandler OnLuggageSortedOut;
        public event EventHandler OnOpenCloseEvent;
        public static Luggage[] LuggageBuffer { get; set; }
        public Passenger Passenger { get; set; }
        public int Number { get; set; }
        private bool isOpen;
        public bool IsOpen
        {
            get { return isOpen; }
            set
            {
                if (isOpen != value)
                {
                    isOpen = value;
                    TriggerOnOpenCloseEvent();
                }
            }
        }

        public Terminal(int number)
        {
            this.Number = number;
        }



        public void TriggerOnLuggageTransfered()
        {

            EventHandler handler = OnLuggageSortedOut;
            if (handler != null)
                OnLuggageSortedOut($"{Counter.LuggageBuffer[0].Owner}'s luggage was transfered at {Counter.LuggageBuffer[0].TimeStampOut}. And going to {Counter.LuggageBuffer[0].Destination}", EventArgs.Empty);
        }

        public void TriggerOnOpenCloseEvent()
        {
            EventHandler handler = OnOpenCloseEvent;
            if (handler != null)
            {
                if (IsOpen)
                    OnOpenCloseEvent($"\nTerminal number {Number} was opened", EventArgs.Empty);
                else
                    OnOpenCloseEvent($"\nTerminal number {Number} was closed", EventArgs.Empty);
            }
        }
    }
}
