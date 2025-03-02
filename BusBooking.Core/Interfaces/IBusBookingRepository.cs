using BusBooking.Core.Dtos.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Core.Interfaces
{
    public interface IBusBookingRepository
    {
        IEnumerable<BusBookingDto> GetAllBusBookings(int vendorId);
        BusBookingDto GetBusBookingById(int bookingId);
        BusBookingDto CreateBusBooking(BusBookingDto busBookingDto);
        void DeleteBusBooking(int bookingId);

    }
}
