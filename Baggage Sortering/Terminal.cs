using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baggage_Sortering
{
    class Terminal
    {
        public event EventHandler OnLuggageSortedOut;
        public event EventHandler OnOpenCloseEvent;

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

        private Luggage[] Luggage;
        private int maxLuggageSlots;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="country"></param>
        /// <param name="terminalNumber"></param>
        /// <param name="maxLuggageSlots"></param>
        public Terminal(Country country, int terminalNumber, int maxLuggageSlots)
        {
            this.maxLuggageSlots = maxLuggageSlots;
            this.Destination = country;
            this.TerminalNumber = terminalNumber;
            this.Luggage = new Luggage[this.maxLuggageSlots];
            IsOpen = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="luggage"></param>
        public void TakeInLuggage(Luggage luggage)
        {
            if (!IsLuggageBufferFull() && IsOpen)
            {
                this.Luggage[this.Luggage.Length - 1] = luggage;
                TriggerOnLuggageTransfered(luggage);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsLuggageBufferFull()
        {
            int count = 0;
            for (int i = 0; i < Luggage.Length; i++)
                if (Luggage[i] != null)
                    count++;

            return IsFull(count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private bool IsFull(int count)
        {
            if (count == Luggage.Length)
                return true;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="luggage"></param>
        public void TriggerOnLuggageTransfered(Luggage luggage)
        {

            EventHandler handler = OnLuggageSortedOut;
            if (handler != null)
                OnLuggageSortedOut($"{luggage.Owner}'s luggage was transfered at {luggage.TimeStampOut}. And going to {luggage.Destination}", EventArgs.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        public void TriggerOnOpenCloseEvent()
        {
            EventHandler handler = OnOpenCloseEvent;
            if (handler != null)
            {
                ForwardMessage();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ForwardMessage()
        {
            if (IsOpen)
            {
                OnOpenCloseEvent($"\nTerminal number {TerminalNumber} was opened", EventArgs.Empty); 
                return;
            }

            OnOpenCloseEvent($"\nTerminal number {TerminalNumber} was closed", EventArgs.Empty);
        }
    }
}
