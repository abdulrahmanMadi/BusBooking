﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Core.Dtos.User
{
    public class RoleDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } // e.g., Admin, Customer, Vendor
    }
}