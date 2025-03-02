using BusBooking.Core.Dtos.Bus;
using BusBooking.Core.Entites;
using BusBooking.Core.Interfaces;
using BusBooking.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;

namespace BusBooking.Infrastructure.Repositories
{
    public class BusScheduleRepository : IBusScheduleRepository
    {
        private readonly ApplicationDbContext _context;

        public BusScheduleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all bus schedules for a vendor
        public IEnumerable<BusScheduleDto> GetBusSchedulesByVendor(int vendorId)
        {
            return _context.BusSchedules
                .Where(bs => bs.VendorId == vendorId)
                .Select(bs => new BusScheduleDto
                {
                    ScheduleId = bs.ScheduleId,
                    VendorId = bs.VendorId,
                    BusName = bs.BusName,
                    BusVehicleNo = bs.BusVehicleNo,
                    FromLocationId = bs.FromLocationId, // Updated to use FromLocationId
                    ToLocationId = bs.ToLocationId,   // Updated to use ToLocationId
                    DepartureTime = bs.DepartureTime,
                    ArrivalTime = bs.ArrivalTime,
                    ScheduleDate = bs.ScheduleDate,
                    Price = bs.Price,
                    TotalSeats = bs.TotalSeats
                }).ToList();
        }

        // Get bus schedule by ID
        public BusScheduleDto GetBusScheduleById(int scheduleId)
        {
            var schedule = _context.BusSchedules.FirstOrDefault(bs => bs.ScheduleId == scheduleId);
            return schedule == null ? null : new BusScheduleDto
            {
                ScheduleId = schedule.ScheduleId,
                VendorId = schedule.VendorId,
                BusName = schedule.BusName,
                BusVehicleNo = schedule.BusVehicleNo,
                FromLocationId = schedule.FromLocationId, // Updated to use FromLocationId
                ToLocationId = schedule.ToLocationId,     // Updated to use ToLocationId
                DepartureTime = schedule.DepartureTime,
                ArrivalTime = schedule.ArrivalTime,
                ScheduleDate = schedule.ScheduleDate,
                Price = schedule.Price,
                TotalSeats = schedule.TotalSeats
            };
        }

        // Create a new bus schedule
        public BusScheduleDto CreateBusSchedule(BusScheduleDto busScheduleDto)
        {
            var busSchedule = new BusSchedule
            {
                VendorId = busScheduleDto.VendorId,
                BusName = busScheduleDto.BusName,
                BusVehicleNo = busScheduleDto.BusVehicleNo,
                FromLocationId = busScheduleDto.FromLocationId, // Updated to use FromLocationId
                ToLocationId = busScheduleDto.ToLocationId,     // Updated to use ToLocationId
                DepartureTime = busScheduleDto.DepartureTime,
                ArrivalTime = busScheduleDto.ArrivalTime,
                ScheduleDate = busScheduleDto.ScheduleDate,
                Price = busScheduleDto.Price,
                TotalSeats = busScheduleDto.TotalSeats
            };

            _context.BusSchedules.Add(busSchedule);
            _context.SaveChanges();

            busScheduleDto.ScheduleId = busSchedule.ScheduleId;
            return busScheduleDto;
        }

        // Update bus schedule
        public BusScheduleDto UpdateBusSchedule(int scheduleId, BusScheduleDto busScheduleDto)
        {
            var busSchedule = _context.BusSchedules.FirstOrDefault(bs => bs.ScheduleId == scheduleId);
            if (busSchedule == null) return null;

            busSchedule.BusName = busScheduleDto.BusName;
            busSchedule.BusVehicleNo = busScheduleDto.BusVehicleNo;
            busSchedule.FromLocationId = busScheduleDto.FromLocationId; // Updated to use FromLocationId
            busSchedule.ToLocationId = busScheduleDto.ToLocationId;     // Updated to use ToLocationId
            busSchedule.DepartureTime = busScheduleDto.DepartureTime;
            busSchedule.ArrivalTime = busScheduleDto.ArrivalTime;
            busSchedule.ScheduleDate = busScheduleDto.ScheduleDate;
            busSchedule.Price = busScheduleDto.Price;
            busSchedule.TotalSeats = busScheduleDto.TotalSeats;

            _context.SaveChanges();
            return busScheduleDto;
        }

        // Delete bus schedule
        public void DeleteBusSchedule(int scheduleId)
        {
            var busSchedule = _context.BusSchedules.FirstOrDefault(bs => bs.ScheduleId == scheduleId);
            if (busSchedule != null)
            {
                _context.BusSchedules.Remove(busSchedule);
                _context.SaveChanges();
            }
        }
    }
}