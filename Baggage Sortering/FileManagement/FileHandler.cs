using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Baggage_Sortering.FileManagement
{
    class FileHandler
    {
        public Passenger GetReservationFromFile(string path)
        {
            string reservationString = GetReservationString(path);
            string fName = reservationString.Split(' ', StringSplitOptions.None)[0];
            string lName = reservationString.Split(' ', StringSplitOptions.None)[1];
            string country = reservationString.Split(' ', StringSplitOptions.None)[2];
            DateTime date = Convert.ToDateTime(reservationString.Split(' ', StringSplitOptions.None)[3] + " " + reservationString.Split(' ', StringSplitOptions.None)[4]);
            return new Passenger(fName, lName, new Luggage(country, fName, date), new FlightPlan(country));
        }

        private string GetReservationString(string path)
        {
            string line = "";
            using (StreamReader reader = new StreamReader(path))
            {
                if (reader.ReadLine() == null)
                {
                    Thread.Sleep(5000);
                }
                line = reader.ReadLine();
                reader.Close();
                RemoveFirstReservation(path);
                return line;
            }
        }

        public void RemoveFirstReservation(string path)
        {
            string[] lines = File.ReadAllLines(path); 
            File.WriteAllLines(path, lines.Skip(1).ToArray());
        }
    }
}
