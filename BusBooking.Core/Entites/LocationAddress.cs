using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Core.Entites
{
    public class LocationAddress
    {
        public int LocationPointId { get; set; } // Primary key
        public int LocationId { get; set; } // Foreign key to Location
        public Location Location { get; set; } // Navigation property
        public string LocationTitle { get; set; }
        public string LocationAddressString { get; set; }
    }
}
