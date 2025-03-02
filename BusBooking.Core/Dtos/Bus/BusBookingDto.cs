using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Core.Dtos.Bus
{
    public class BusBookingDto
    {
        public int BookingId { get; set; }
        public int CustId { get; set; }
        public DateTime BookingDate { get; set; }
        public int ScheduleId { get; set; }
        public List<BusBookingPassengerDto> BusBookingPassengers { get; set; }
    }
}