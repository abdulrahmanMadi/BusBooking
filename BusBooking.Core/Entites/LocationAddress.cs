using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Core.Entites
{
    public class LocationAddress
    {
        public int LocationPointId { get; set; } 
        public int LocationId { get; set; } 
        public Location Location { get; set; } 
        public string LocationTitle { get; set; }
        public string LocationAddressString { get; set; }
    }
}
