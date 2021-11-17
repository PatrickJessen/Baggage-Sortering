using System;
using System.Threading;

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
                Random rand = new Random();
                Thread.Sleep(rand.Next(2000, 5000));
            }
        }
    }
}
