using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Core.Dtos.Bus
{
    public class BusBookingPassengerDto
    {
        public int PassengerId { get; set; }
        public int BookingId { get; set; }
        public string PassengerName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public int SeatNo { get; set; }
    }
}