using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Core.Dtos
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string UserName { get; set; }
    }
}
