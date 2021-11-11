using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baggage_Sortering
{
    class Belt : ISortBuffer
    {
        public bool IsRunning { get; set; }
        public int MaxSlots { get; private set; }
        public int LuggagesOnBelt { get; private set; } = 0;
        private bool isFull;
        public bool IsFull
        {
            get
            {
                return isFull;
            }
            private set
            {
                if (LuggagesOnBelt != MaxSlots)
                    isFull = false;
                else
                    isFull = true;
            }
        }
        public Luggage[] Luggage { get; private set; }

        public Belt(int maxSlots)
        {
            MaxSlots = maxSlots;
            this.Luggage = new Luggage[maxSlots];
        }

        public void Add(Luggage luggage)
        {
            Luggage[MaxSlots - 1] = luggage;
            LuggagesOnBelt++;
            Sort();
        }

        public void RemoveAt(int index)
        {
            Luggage[index] = null;
            LuggagesOnBelt--;
            Sort();
        }

        public void Sort()
        {
            for (int i = MaxSlots; i >= 0; i--)
            {
                try
                {
                    if (Luggage[i] == null)
                    {
                        Luggage[i] = Luggage[i + 1];
                        Luggage[i + 1] = null;
                    }
                }
                catch
                {

                }
            }
        }
    }
}
