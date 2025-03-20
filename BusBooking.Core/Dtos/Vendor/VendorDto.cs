using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Core.Dtos.Vendor
{
    public class VendorDto
    {
        public int VendorId { get; set; } 
        public string VendorName { get; set; } 
        public string ContactNo { get; set; }
        public string EmailId { get; set; }
    }
}