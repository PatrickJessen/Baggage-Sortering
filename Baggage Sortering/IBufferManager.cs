using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baggage_Sortering
{
    interface IBufferManager
    {
        void AddToBuffer(Luggage luggage);
        void RemoveFromBuffer();
        void RemoveFromBufferAtIndex(int index);
        void SortBuffer();
    }
}
