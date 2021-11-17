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

        public void MakeReservation()
        {
            reservation = new Reservation(handler.GetRandomStringFromFile("../../../../Assets/FirstNames.txt"), handler.GetRandomStringFromFile("../../../../Assets/LastNames.txt"), handler.GetRandomStringFromFile("../../../../Assets/Countries.txt"), DateTime.UtcNow);
            handler.WriteObjectToFile(reservation, "../../../../Assets/Reservation.txt");
        }
    }
}
