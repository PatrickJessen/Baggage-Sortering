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
        public Country Destination { get; private set; }
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

        private int maxLuggageSlots;

        public Terminal(Country country, int terminalNumber, int maxLuggageSlots)
        {
            this.maxLuggageSlots = maxLuggageSlots;
            this.Destination = country;
            this.TerminalNumber = terminalNumber;
            IsOpen = true;
            this.Luggage = new Luggage[this.maxLuggageSlots];
        }

        public void TransferLuggageToTerminal(Luggage luggage)
        {
            if (!IsLuggageBufferFull() && IsOpen)
            {
                this.Luggage[this.Luggage.Length - 1] = luggage;
                Sort();
            }
        }

        public bool LuggageBufferIsFull()
        {
            int count = 0;
            for (int i = 0; i < Luggage.Length; i++)
                if (Luggage[i] != null)
                    count++;

            if (count == maxLuggageSlots)
                return true;
            return false;
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

        public void TriggerOnLuggageTransfered(Luggage luggage)
        {

            EventHandler handler = OnLuggageSortedOut;
            if (handler != null)
                OnLuggageSortedOut($"{luggage.Owner}'s luggage was transfered at {luggage.TimeStampOut}. And going to {luggage.Destination}", EventArgs.Empty);
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
