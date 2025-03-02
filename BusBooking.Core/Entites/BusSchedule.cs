using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Core.Entites
{
    public class BusSchedule
    {
        public int ScheduleId { get; set; } // Primary key
        public int VendorId { get; set; } // Foreign key to User (Vendor)
        public User Vendor { get; set; } // Navigation property
        public string BusName { get; set; }
        public string BusVehicleNo { get; set; }
        public int FromLocationId { get; set; } // Foreign key to Location (renamed for clarity)
        public Location FromLocation { get; set; } // Navigation property
        public int ToLocationId { get; set; } // Foreign key to Location (renamed for clarity)
        public Location ToLocation { get; set; } // Navigation property
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime ScheduleDate { get; set; }
        public decimal Price { get; set; }
        public int TotalSeats { get; set; }
    }
}