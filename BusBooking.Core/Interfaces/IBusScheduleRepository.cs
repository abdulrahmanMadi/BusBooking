using BusBooking.Core.Dtos.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Core.Interfaces
{
    public interface IBusScheduleRepository
    {
        IEnumerable<BusScheduleDto> GetBusSchedulesByVendor(int vendorId);
        BusScheduleDto GetBusScheduleById(int scheduleId);
        BusScheduleDto CreateBusSchedule(BusScheduleDto busScheduleDto);
        BusScheduleDto UpdateBusSchedule(int scheduleId, BusScheduleDto busScheduleDto);
        void DeleteBusSchedule(int scheduleId);
    }
}
