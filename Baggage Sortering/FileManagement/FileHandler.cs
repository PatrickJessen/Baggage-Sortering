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
        public Passenger GetPassengerFromFile(string path)
        {
            string reservationString = GetReservationString(path);
            string fName = reservationString.Split(' ', StringSplitOptions.None)[0];
            string lName = reservationString.Split(' ', StringSplitOptions.None)[1];
            string country = reservationString.Split(' ', StringSplitOptions.None)[2];
            return new Passenger(fName, lName, new Luggage(country, fName, DateTime.UtcNow), new FlightPlan(country));
        }

        private string GetReservationString(string path)
        {
            string line = "";
            using (StreamReader reader = new StreamReader(path))
            {
                try
                {
                    line = reader.ReadLine();
                    if (line == null)
                    {
                        Thread.Sleep(5000);
                        line = reader.ReadLine();
                    }
                    reader.Close();
                    RemoveFirstReservation(path);
                }
                catch
                {

                }
                return line;
            }
        }

        public void RemoveFirstReservation(string path)
        {
            try
            {
                string[] lines = File.ReadAllLines(path); 
                File.WriteAllLines(path, lines.Skip(1).ToArray());
            }
            catch
            {

            }
        }

        public bool IsReservationEmpty(string path)
        {
            string line = "";
            using (StreamReader reader = new StreamReader(path))
            {
                try
                {
                    line = reader.ReadLine();
                    if (line == null)
                    {

                        reader.Close();
                        return true;
                    }
                }
                catch
                {

                }
                return false;
            }
        }
    }
}
