using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baggage_Sortering
{
    class CounterManager : IBufferManager
    {
        private IManager counter;

        public CounterManager(IManager counter)
        {
            this.counter = counter;
        }

        public void AddToBuffer(Luggage luggage)
        {
            for (int i = 0; i < Counter.LuggageBuffer.Length; i++)
                if (Counter.LuggageBuffer[i] == null)
                    Counter.LuggageBuffer[i] = luggage;
        }

        public void RemoveFromBuffer()
        {
            throw new NotImplementedException();
        }

        public void RemoveFromBufferAtIndex(int index)
        {
            Counter.LuggageBuffer[index] = null;
            SortBuffer();
        }

        public void SortBuffer()
        {
            for (int i = 0; i < Counter.LuggageBuffer.Length; i++)
                if (Counter.LuggageBuffer[i] == null && Counter.LuggageBuffer[i + 1] != null)
                {
                    try
                    {
                        Counter.LuggageBuffer[i] = Counter.LuggageBuffer[i + 1];
                        RemoveFromBufferAtIndex(i + 1);
                    }
                    catch { }
                }
        }
    }
}
