using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Core.Dtos.Location
{
    public class LocationAddressDto
    {
        public int LocationPointId { get; set; }
        public int LocationId { get; set; }
        public string LocationTitle { get; set; }
        public string LocationAddress { get; set; }
    }
}
