using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Core.Dtos.Bus
{
    public class BusScheduleDto
    {
        public int ScheduleId { get; set; }
        public int VendorId { get; set; }
        public string BusName { get; set; }
        public string BusVehicleNo { get; set; }
        public int FromLocationId { get; set; } // Updated to use FromLocationId
        public int ToLocationId { get; set; }   // Updated to use ToLocationId
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime ScheduleDate { get; set; }
        public decimal Price { get; set; }
        public int TotalSeats { get; set; }
    }
}