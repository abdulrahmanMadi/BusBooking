using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Core.Entites
{
    public class BusSchedule
    {
        public int ScheduleId { get; set; }
        public int VendorId { get; set; }
        public User Vendor { get; set; } 
        public string BusName { get; set; }
        public string BusVehicleNo { get; set; }
        public int FromLocationId { get; set; } 
        public Location FromLocation { get; set; }
        public int ToLocationId { get; set; } 
        public Location ToLocation { get; set; } 
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime ScheduleDate { get; set; }
        public decimal Price { get; set; }
        public int TotalSeats { get; set; }
        public int? AvailableSeats { get; set; }

    }
}