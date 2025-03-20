using BusBooking.Core.Dtos.Bus;
using BusBooking.Core.Dtos.Location;
using System;
using System.Collections.Generic;

namespace BusBooking.Core.Interfaces
{
    public interface IBusBookingRepository
    {
        // Location methods
        LocationDto GetBusLocationById(int id);
        LocationAddressDto GetAddressByLocationId(int id);
        LocationAddressDto PostBusLocationAddress(LocationAddressDto locationAddressDto);
        LocationDto PutBusLocation(int id, LocationDto locationDto);
        LocationDto PostBusLocation(LocationDto locationDto);
        void DeleteBusLocation(int id);

         IEnumerable<BusBookingDto> GetBusBookingsByCustomerId(int customerId);
        
        IEnumerable<BusScheduleDto> GetBusSchedules();
        IEnumerable<BusScheduleDto> SearchBus(int from, int to, string date);
        IEnumerable<int> GetBookedSeats(int scheduleId);
        BusScheduleDto GetBusScheduleById(int id);
        BusScheduleDto PutBusSchedule(int id, BusScheduleDto busScheduleDto);
        BusScheduleDto PostBusSchedule(BusScheduleDto busScheduleDto);
        void DeleteBusSchedule(int id);

        // Bus booking methods
        IEnumerable<BusBookingDto> GetAllBusBookings();
        BusBookingDto GetBusBooking(int id);
        BusBookingDto PostBusBooking(BusBookingDto busBookingDto);
        void DeleteBusBooking(int id);
    }
}