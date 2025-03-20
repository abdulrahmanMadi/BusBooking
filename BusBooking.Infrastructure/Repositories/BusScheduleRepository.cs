using BusBooking.Core.Dtos.Bus;
using BusBooking.Core.Entites;
using BusBooking.Core.Interfaces;
using BusBooking.Infrastructure.Data;
using System;
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

        public IEnumerable<BusScheduleDto> GetBusSchedulesByVendor(int vendorId)
        {
            if (vendorId <= 0)
            {
                throw new ArgumentException("Invalid VendorId. VendorId must be greater than 0.");
            }

            var schedules = _context.BusSchedules
                .Where(bs => bs.VendorId == vendorId)
                .Select(bs => new BusScheduleDto
                {
                    ScheduleId = bs.ScheduleId,
                    VendorId = bs.VendorId,
                    BusName = bs.BusName,
                    BusVehicleNo = bs.BusVehicleNo,
                    FromLocationId = bs.FromLocationId,
                    ToLocationId = bs.ToLocationId,
                    DepartureTime = bs.DepartureTime,
                    ArrivalTime = bs.ArrivalTime,
                    ScheduleDate = bs.ScheduleDate,
                    Price = bs.Price,
                    TotalSeats = bs.TotalSeats,
                    AvailableSeats = bs.AvailableSeats

                }).ToList();

            if (!schedules.Any())
            {
                throw new KeyNotFoundException($"No bus schedules found for VendorId: {vendorId}.");
            }

            return schedules;
        }

        public BusScheduleDto GetBusScheduleById(int scheduleId)
        {
            if (scheduleId <= 0)
            {
                throw new ArgumentException("Invalid ScheduleId. ScheduleId must be greater than 0.");
            }

            var schedule = _context.BusSchedules.FirstOrDefault(bs => bs.ScheduleId == scheduleId);
            if (schedule == null)
            {
                throw new KeyNotFoundException($"Bus schedule with ScheduleId: {scheduleId} not found.");
            }

            return new BusScheduleDto
            {
                ScheduleId = schedule.ScheduleId,
                VendorId = schedule.VendorId,
                BusName = schedule.BusName,
                BusVehicleNo = schedule.BusVehicleNo,
                FromLocationId = schedule.FromLocationId,
                ToLocationId = schedule.ToLocationId,
                DepartureTime = schedule.DepartureTime,
                ArrivalTime = schedule.ArrivalTime,
                ScheduleDate = schedule.ScheduleDate,
                Price = schedule.Price,
                TotalSeats = schedule.TotalSeats,
                AvailableSeats = schedule.AvailableSeats

            };
        }

        public BusScheduleDto CreateBusSchedule(BusScheduleDto busScheduleDto)
        {
            if (busScheduleDto == null)
            {
                throw new ArgumentNullException(nameof(busScheduleDto), "BusScheduleDto cannot be null.");
            }

            if (busScheduleDto.VendorId <= 0)
            {
                throw new ArgumentException("Invalid VendorId. VendorId must be greater than 0.");
            }

            if (busScheduleDto.FromLocationId <= 0 || busScheduleDto.ToLocationId <= 0)
            {
                throw new ArgumentException("Invalid LocationId. FromLocationId and ToLocationId must be greater than 0.");
            }

            if (busScheduleDto.DepartureTime >= busScheduleDto.ArrivalTime)
            {
                throw new ArgumentException("DepartureTime must be before ArrivalTime.");
            }

            if (busScheduleDto.TotalSeats <= 0)
            {
                throw new ArgumentException("TotalSeats must be greater than 0.");
            }

            var vendorExists = _context.Users.Any(u => u.UserId == busScheduleDto.VendorId);
            if (!vendorExists)
            {
                throw new KeyNotFoundException($"Vendor with VendorId: {busScheduleDto.VendorId} not found.");
            }

            var fromLocationExists = _context.Locations.Any(l => l.LocationId == busScheduleDto.FromLocationId);
            var toLocationExists = _context.Locations.Any(l => l.LocationId == busScheduleDto.ToLocationId);
            if (!fromLocationExists || !toLocationExists)
            {
                throw new KeyNotFoundException($"Invalid LocationId. FromLocationId or ToLocationId does not exist.");
            }

            var schedule = new BusSchedule
            {
                VendorId = busScheduleDto.VendorId,
                BusName = busScheduleDto.BusName,
                BusVehicleNo = busScheduleDto.BusVehicleNo,
                FromLocationId = busScheduleDto.FromLocationId,
                ToLocationId = busScheduleDto.ToLocationId,
                DepartureTime = busScheduleDto.DepartureTime,
                ArrivalTime = busScheduleDto.ArrivalTime,
                ScheduleDate = busScheduleDto.ScheduleDate,
                Price = busScheduleDto.Price,
                TotalSeats = busScheduleDto.TotalSeats,
                AvailableSeats = busScheduleDto.AvailableSeats

            };

            _context.BusSchedules.Add(schedule);
            _context.SaveChanges();

            busScheduleDto.ScheduleId = schedule.ScheduleId;
            return busScheduleDto;
        }

        public BusScheduleDto UpdateBusSchedule(int scheduleId, BusScheduleDto busScheduleDto)
        {
            if (scheduleId <= 0)
            {
                throw new ArgumentException("Invalid ScheduleId. ScheduleId must be greater than 0.");
            }

            if (busScheduleDto == null)
            {
                throw new ArgumentNullException(nameof(busScheduleDto), "BusScheduleDto cannot be null.");
            }

            var busSchedule = _context.BusSchedules.FirstOrDefault(bs => bs.ScheduleId == scheduleId);
            if (busSchedule == null)
            {
                throw new KeyNotFoundException($"Bus schedule with ScheduleId: {scheduleId} not found.");
            }

            if (busScheduleDto.VendorId <= 0)
            {
                throw new ArgumentException("Invalid VendorId. VendorId must be greater than 0.");
            }

            if (busScheduleDto.FromLocationId <= 0 || busScheduleDto.ToLocationId <= 0)
            {
                throw new ArgumentException("Invalid LocationId. FromLocationId and ToLocationId must be greater than 0.");
            }

            if (busScheduleDto.DepartureTime >= busScheduleDto.ArrivalTime)
            {
                throw new ArgumentException("DepartureTime must be before ArrivalTime.");
            }

            if (busScheduleDto.TotalSeats <= 0)
            {
                throw new ArgumentException("TotalSeats must be greater than 0.");
            }

            var vendorExists = _context.Users.Any(u => u.UserId == busScheduleDto.VendorId);
            if (!vendorExists)
            {
                throw new KeyNotFoundException($"Vendor with VendorId: {busScheduleDto.VendorId} not found.");
            }

            var fromLocationExists = _context.Locations.Any(l => l.LocationId == busScheduleDto.FromLocationId);
            var toLocationExists = _context.Locations.Any(l => l.LocationId == busScheduleDto.ToLocationId);
            if (!fromLocationExists || !toLocationExists)
            {
                throw new KeyNotFoundException($"Invalid LocationId. FromLocationId or ToLocationId does not exist.");
            }

            busSchedule.BusName = busScheduleDto.BusName;
            busSchedule.BusVehicleNo = busScheduleDto.BusVehicleNo;
            busSchedule.FromLocationId = busScheduleDto.FromLocationId;
            busSchedule.ToLocationId = busScheduleDto.ToLocationId;
            busSchedule.DepartureTime = busScheduleDto.DepartureTime;
            busSchedule.ArrivalTime = busScheduleDto.ArrivalTime;
            busSchedule.ScheduleDate = busScheduleDto.ScheduleDate;
            busSchedule.Price = busScheduleDto.Price;
            busSchedule.TotalSeats = busScheduleDto.TotalSeats;
            busSchedule.AvailableSeats = busScheduleDto.AvailableSeats;


            _context.SaveChanges();
            return busScheduleDto;
        }

        public void DeleteBusSchedule(int scheduleId)
        {
            if (scheduleId <= 0)
            {
                throw new ArgumentException("Invalid ScheduleId. ScheduleId must be greater than 0.");
            }

            var busSchedule = _context.BusSchedules.FirstOrDefault(bs => bs.ScheduleId == scheduleId);
            if (busSchedule == null)
            {
                throw new KeyNotFoundException($"Bus schedule with ScheduleId: {scheduleId} not found.");
            }

            _context.BusSchedules.Remove(busSchedule);
            _context.SaveChanges();
        }
    }
}