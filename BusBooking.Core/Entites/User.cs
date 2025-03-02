﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Core.Entites
{
    public class User
    {
        public int UserId { get; set; } // Primary key
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string FullName { get; set; }
        public int RoleId { get; set; } // Foreign key to Role
        public Role Role { get; set; } // Navigation property
        public DateTime CreatedDate { get; set; }
        public string Password { get; set; }
        public string ProjectName { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public string ContactNo { get; set; } // Additional field for vendors
    }
}
