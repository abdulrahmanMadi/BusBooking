using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Core.Dtos.Auth
{
    public class RegisterRequestDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string FullName { get; set; }
        public int RoleId { get; set; } // Role ID ( 1 for Admin, 2 for Customer, 3 for Vendor)
        public string Password { get; set; }
        public string ContactNo { get; set; }
    }
}