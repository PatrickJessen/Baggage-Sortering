using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationSystem
{
    class ReservationManager
    {
        private Reservation reservation;
        private readonly FileHandler handler = new FileHandler();

        private readonly string folder = "../../../../Assets/";

        public void MakeReservation()
        {
            reservation = new Reservation(handler.GetRandomStringFromFile(folder + "FirstNames.txt"), handler.GetRandomStringFromFile(folder + "LastNames.txt"), handler.GetRandomStringFromFile(folder + "Countries.txt"));
            handler.WriteObjectToFile(reservation, folder + "Reservation.txt");
        }
    }
}
