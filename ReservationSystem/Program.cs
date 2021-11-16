using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Reflection;

namespace ReservationSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            ReservationManager manager = new ReservationManager();

            while (true)
            {
                manager.MakeReservation();
                ReadFromFile();
                Random rand = new Random();
                Thread.Sleep(rand.Next(2000, 5000));
            }
        }
        static string ReadFromFile()
        {
            string ff = AppDomain.CurrentDomain.BaseDirectory;
            string test = Path.GetDirectoryName("ReservationSystem");
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Reservation.txt");
            string line = "";
            using (StreamReader reader = new StreamReader("../../../../Assets/Reservation.txt"))
            {
                line = reader.ReadLine();
            }
            string fName = line.Split(' ', StringSplitOptions.None)[0];
            string lName = line.Split(' ', StringSplitOptions.None)[1];
            string country = line.Split(' ', StringSplitOptions.None)[2];
            DateTime date = Convert.ToDateTime(line.Split(' ', StringSplitOptions.None)[3] + " " + line.Split(' ', StringSplitOptions.None)[4]);
            return fName;
        }
    }
}
