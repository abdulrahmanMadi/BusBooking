using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Core.Dtos.Auth
{
    public class LoginResponseDto
    {
        public int UserId { get; set; } // JWT token

        public string Token { get; set; } 
        public string UserName { get; set; }
        public string RefreshToken { get; set; } 
    }
}
