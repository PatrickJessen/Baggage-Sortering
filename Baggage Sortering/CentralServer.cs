using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baggage_Sortering
{
    class CentralServer
    {
        private Counter[] counter;
        private Terminal[] terminal;

        public CentralServer(Counter[] counter, Terminal[] terminal)
        {
            this.counter = counter;
            this.terminal = terminal;
            for (int i = 0; i < counter.Length; i++)
            {
                counter[i].OnLuggageSortedIn += CentralServer_OnLuggageSortedIn;
                counter[i].OnPassengerCheckedIn += CentralServer_OnPassengerCheckedIn;
                counter[i].OnOpenCloseEvent += CentralServer_OnOpenCloseEvent;
            }
            
            for (int i = 0; i < terminal.Length; i++)
            {
                terminal[i].OnLuggageSortedOut += CentralServer_OnLuggageSortedOut;
                terminal[i].OnOpenCloseEvent += CentralServer_OnOpenCloseEvent1;
            }
        }

        public void OpenCloseCounters()
        {
            while (true)
                if (Input.KeyState(ConsoleKey.C))
                    for (int i = 0; i < counter.Length; i++)
                        counter[i].IsOpen = !counter[i].IsOpen;
        }

        public void OpenCloseTerminals()
        {
            while (true)
                if (Input.KeyState(ConsoleKey.T))
                    for (int i = 0; i < terminal.Length; i++)
                        terminal[i].IsOpen = !terminal[i].IsOpen;
        }

        private void CentralServer_OnOpenCloseEvent1(object sender, EventArgs e)
        {
            Console.WriteLine(sender);
        }

        private void CentralServer_OnLuggageSortedOut(object sender, EventArgs e)
        {
            Console.WriteLine(sender);
        }

        private void CentralServer_OnOpenCloseEvent(object sender, EventArgs e)
        {
            Console.WriteLine(sender);
        }

        private void CentralServer_OnPassengerCheckedIn(object sender, EventArgs e)
        {
            Console.WriteLine(sender);
        }

        private void CentralServer_OnLuggageSortedIn(object sender, EventArgs e)
        {
            Console.WriteLine(sender);
        }
    }
}
