using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baggage_Sortering
{
    class SortingManager
    {
        private IManager[] counter;
        private IManager[] terminal;
        private int bufferSize;
        private int gateSize;

        public SortingManager(int gateSize = 3)
        {
            this.gateSize = gateSize;

            counter = new Counter[gateSize];
            terminal = new Terminal[gateSize];
            bufferSize = gateSize * 3;
            Counter.LuggageBuffer = new Luggage[bufferSize];

            Initialize();
        }

        private void Initialize()
        {
            for (int i = 1; i <= gateSize; i++)
            {
                if (counter[i] == null)
                {
                    counter[i] = new Counter(i);
                    counter[i].IsOpen = true;
                }

                if (terminal[i] == null)
                {
                    terminal[i] = new Terminal(i);
                    terminal[i].IsOpen = true;
                }
            }
        }
    }
}
