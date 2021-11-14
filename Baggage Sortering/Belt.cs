using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baggage_Sortering
{
    class Belt
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxSlots"></param>
        public Belt(int maxSlots)
        {
            MaxSlots = maxSlots;
            this.Luggage = new Luggage[maxSlots];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="luggage"></param>
        public void Add(Luggage luggage)
        {
            Luggage[MaxSlots - 1] = luggage;
            LuggagesOnBelt++;
            MoveToFirstAvailableSlot();
        }

        /// <summary>
        /// 
        /// </summary>
        public void RemoveFirst()
        {
            Luggage[0] = null;
            LuggagesOnBelt--;
            MoveToFirstAvailableSlot();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Luggage GetFirst()
        {
            return Luggage[0];
        }

        /// <summary>
        /// 
        /// </summary>
        private void MoveToFirstAvailableSlot()
        {
            for (int i = Luggage.Length -1; i >= 0; i--)
            {
                try
                {
                    if (Luggage[i] == null && Luggage[i + 1] != null)
                    {
                        Luggage[i] = Luggage[i + 1];
                        Luggage[i + 1] = null;
                    }
                }
                catch { }
            }
        }
    }
}
