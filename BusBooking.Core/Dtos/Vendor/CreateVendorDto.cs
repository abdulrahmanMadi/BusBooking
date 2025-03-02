using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Core.Dtos.Vendor
{
    public class CreateVendorDto
    {
        public string VendorName { get; set; } // Maps to FullName
        public string ContactNo { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
    }
}