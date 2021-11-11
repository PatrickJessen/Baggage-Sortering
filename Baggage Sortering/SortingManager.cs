using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Baggage_Sortering
{
    class SortingManager
    {
        private int gateSize;

        public SortingManager(int gateSize)
        {
            this.gateSize = gateSize;
        }

        public void StartSorting(object locker, Counter[] counter, Terminal[] terminal)
        {
            DateTime startTime = DateTime.Now;
            while ((DateTime.Now - startTime).Seconds < 5)
            {
                try
                {
                    Monitor.Enter(locker);
                    for (int i = 0; i < gateSize; i++)
                        for (int j = 0; j < gateSize; j++)
                            if (terminal[j].LuggageBufferIsFull() || counter[i].CounterBelt.IsFull)
                            {
                                Monitor.Wait(locker);
                                break;
                            }
                            else if (CanBeSorted(counter, i) && counter[i].CounterBelt.Luggage[0].Destination == terminal[j].Destination)
                            {
                                terminal[j].TransferLuggageToTerminal(counter[i].CounterBelt.Luggage[0]);
                                terminal[j].TriggerOnLuggageTransfered(counter[i].CounterBelt.Luggage[0]);
                                counter[i].CounterBelt.RemoveAt(0);
                                //todo:: make so the luggage only can be transfered 10 seconds after it was checked in.
                            }
                }
                finally
                {
                    Monitor.PulseAll(locker);
                    Monitor.Exit(locker);
                }
            }
        }

        private bool CanBeSorted(Counter[] counter, int index)
        {
            //FIX!!!!!!!
            if (counter[index].CounterBelt.Luggage[0] != null && (counter[index].CounterBelt.Luggage[0].TimeStampIn - DateTime.UtcNow).Seconds > DateTime.UtcNow.Second + 5)
                return true;
            return false;
        }
    }
}
