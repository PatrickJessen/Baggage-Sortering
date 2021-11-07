using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baggage_Sortering
{
    class NameGenerator
    {
        public string[] names = new string[] { "John", "Thommas", "Frederik", "Michelle", "Olivia", "Hans", "Grethe", "Bob", "Ingrid" };

        public string GenerateName()
        {
            Random rand = new Random();
            return names[rand.Next(0, names.Length)];
        }
    }
}
