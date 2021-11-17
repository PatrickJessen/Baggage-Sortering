using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Central_Server
{
    static class Input
    {
        static ConsoleKey keyInfo = Console.ReadKey().Key;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool KeyState(ConsoleKey key)
        {
            if (keyInfo == key)
            {
                keyInfo = 0;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void UpdateKey()
        {
            while (true)
                keyInfo = Console.ReadKey().Key;
        }
    }
}
