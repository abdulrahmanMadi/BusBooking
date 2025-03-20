using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Core.Entites
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } //Admin, Customer, Vendor
    }
}
