using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Core.Entites
{
    public class BusBookingPassenger
    {
        public int PassengerId { get; set; } // Part of composite primary key
        public int BookingId { get; set; } // Foreign key to BusBooking
        public BusBooking BusBooking { get; set; } // Navigation property
        public string PassengerName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public int SeatNo { get; set; }
    }
}
