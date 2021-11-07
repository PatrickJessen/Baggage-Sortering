using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baggage_Sortering
{
    static class Input
    {
        static ConsoleKey keyInfo = Console.ReadKey().Key;
        public static bool KeyState(ConsoleKey key)
        {
            if (keyInfo == key)
            {
                keyInfo = 0;
                return true;
            }
            return false;
        }

        public static void UpdateKey()
        {
            while (true)
                keyInfo = Console.ReadKey().Key;
        }
    }
}
