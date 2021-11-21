using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baggage_Sortering
{
    class Terminal
    {
        public string Destination { get; private set; }
        public int TerminalNumber { get; set; }
        public bool IsOpen { get; set; }

        private Luggage[] Luggage;
        private int maxLuggageSlots;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="country"></param>
        /// <param name="terminalNumber"></param>
        /// <param name="maxLuggageSlots"></param>
        public Terminal(string country, int terminalNumber, int maxLuggageSlots)
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
        public void TakeInLuggage(Luggage luggage, Server.ServerHandler server)
        {
            if (!IsLuggageBufferFull() && IsOpen)
            {
                this.Luggage[this.Luggage.Length - 1] = luggage;
                server.SendMessageToServer(SendInformation(luggage));
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

        public string SendInformation(Luggage luggage)
        {
            return $"{luggage.Owner}'s luggage was transfered at {luggage.TimeStampOut}. And going to {luggage.Destination}";
        }
    }
}
