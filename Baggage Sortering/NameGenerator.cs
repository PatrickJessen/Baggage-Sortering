using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baggage_Sortering
{
    class NameGenerator
    {
        private string[] names = new string[] { "John", "Thommas", "Frederik", "Michelle", "Olivia", "Hans", "Grethe", "Bob", "Ingrid", "Lotte", "Gunner", "Mathilde", "Lise", "Arne" };
        private string[] lastNames = new string[] { "Eriksen", "Jensen", "Hansen", "Mortensen", "McAfee", "Johnson", "Madsen", "Gade", "Fisker", "Friis", "Frandsen" };

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GenerateName()
        {
            Random rand = new Random();
            return names[rand.Next(0, names.Length)];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GenerateLastName()
        {
            Random rand = new Random();
            return lastNames[rand.Next(0, lastNames.Length)];
        }
    }
}
