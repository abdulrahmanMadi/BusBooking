using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Core.Dtos.Auth
{
    public class RefreshTokenRequestDto
    {
        public string Token { get; set; } // Expired JWT token
        public string RefreshToken { get; set; } // Refresh token
    }
}