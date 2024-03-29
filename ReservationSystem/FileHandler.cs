﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ReservationSystem
{
    class FileHandler
    {
        public void WriteObjectToFile(Reservation obj, string path)
        {
            File.AppendAllText(path, $"{obj.FirstName} {obj.LastName.ToLower()} {obj.Countries}\n");
        }

        public string GetRandomStringFromFile(string path)
        {
            string name = "";
            string[] lines = File.ReadAllLines(path);
            Random rand = new Random();
            int randNum = rand.Next(0, lines.Length);
            name = lines[randNum];
            return name;
        }
    }
}
