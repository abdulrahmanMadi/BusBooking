using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Core.Dtos
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ProjectName { get; set; }
    }
}
