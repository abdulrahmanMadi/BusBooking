using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Core.Dtos
{
    public class OperationResult<T>
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
    }
}
