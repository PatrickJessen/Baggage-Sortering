using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baggage_Sortering
{
    class Terminal : ISortBuffer
    {
        public event EventHandler OnLuggageSortedOut;
        public event EventHandler OnOpenCloseEvent;
        private Luggage[] Luggage;
        public int TerminalNumber { get; set; }
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

        public Terminal(int terminalNumber, int maxLuggageSlots)
        {
            this.TerminalNumber = terminalNumber;
            IsOpen = true;
            this.Luggage = new Luggage[maxLuggageSlots];
        }

        public void TransferLuggageToTerminal(Luggage luggage)
        {
            if (!IsLuggageBufferFull())
            {
                this.Luggage[this.Luggage.Length - 1] = luggage;
                Sort();
            }
        }

        private bool IsLuggageBufferFull()
        {
            int count = 0;
            for (int i = 0; i < Luggage.Length; i++)
                if (Luggage[i] != null)
                    count++;

            if (count == Luggage.Length)
                return true;
            return false;
        }

        public void Sort()
        {
            for (int i = this.Luggage.Length - 1; i > 0; i--)
            {
                try
                {
                    if (Luggage[i] == null)
                        Luggage[i] = Luggage[i + 1];
                }
                catch
                {

                }
            }
        }

        public void TriggerOnLuggageTransfered()
        {

            EventHandler handler = OnLuggageSortedOut;
            //if (handler != null)
                //OnLuggageSortedOut($"{Counter.LuggageBuffer[0].Owner}'s luggage was transfered at {Counter.LuggageBuffer[0].TimeStampOut}. And going to {Counter.LuggageBuffer[0].Destination}", EventArgs.Empty);
        }

        public void TriggerOnOpenCloseEvent()
        {
            EventHandler handler = OnOpenCloseEvent;
            if (handler != null)
            {
                if (IsOpen)
                    OnOpenCloseEvent($"\nTerminal number {TerminalNumber} was opened", EventArgs.Empty);
                else
                    OnOpenCloseEvent($"\nTerminal number {TerminalNumber} was closed", EventArgs.Empty);
            }
        }
    }
}
