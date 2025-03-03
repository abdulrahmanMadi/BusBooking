using BusBooking.Core.Dtos.Bus;
using BusBooking.Core.Dtos.Location;
using BusBooking.Core.Entites;
using BusBooking.Core.Interfaces;
using BusBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusBooking.Infrastructure.Repositories
{
    public class BusBookingRepository : IBusBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BusBookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Location methods
        public IEnumerable<LocationDto> GetBusLocations()
        {
            var locations = _context.Locations
                .Select(l => new LocationDto
                {
                    LocationId = l.LocationId,
                    LocationName = l.LocationName,
                    Code = l.Code
                }).ToList();

            if (!locations.Any())
            {
                throw new KeyNotFoundException("No locations found.");
            }

            return locations;
        }

        public LocationDto GetBusLocationById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid LocationId. LocationId must be greater than 0.");
            }

            var location = _context.Locations.FirstOrDefault(l => l.LocationId == id);
            if (location == null)
            {
                throw new KeyNotFoundException($"Location with LocationId: {id} not found.");
            }

            return new LocationDto
            {
                LocationId = location.LocationId,
                LocationName = location.LocationName,
                Code = location.Code
            };
        }

        public LocationAddressDto GetAddressByLocationId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid LocationId. LocationId must be greater than 0.");
            }

            var address = _context.LocationAddresses.FirstOrDefault(la => la.LocationId == id);
            if (address == null)
            {
                throw new KeyNotFoundException($"Address for LocationId: {id} not found.");
            }

            return new LocationAddressDto
            {
                LocationPointId = address.LocationPointId,
                LocationId = address.LocationId,
                LocationTitle = address.LocationTitle,
                LocationAddress = address.LocationAddressString
            };
        }

        public LocationAddressDto PostBusLocationAddress(LocationAddressDto locationAddressDto)
        {
            if (locationAddressDto == null)
            {
                throw new ArgumentNullException(nameof(locationAddressDto), "LocationAddressDto cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(locationAddressDto.LocationTitle))
            {
                throw new ArgumentException("LocationTitle is required.");
            }

            if (string.IsNullOrWhiteSpace(locationAddressDto.LocationAddress))
            {
                throw new ArgumentException("LocationAddress is required.");
            }

            var locationAddress = new LocationAddress
            {
                LocationId = locationAddressDto.LocationId,
                LocationTitle = locationAddressDto.LocationTitle,
                LocationAddressString = locationAddressDto.LocationAddress
            };

            _context.LocationAddresses.Add(locationAddress);
            _context.SaveChanges();

            locationAddressDto.LocationPointId = locationAddress.LocationPointId;
            return locationAddressDto;
        }

        public LocationDto PutBusLocation(int id, LocationDto locationDto)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid LocationId. LocationId must be greater than 0.");
            }

            if (locationDto == null)
            {
                throw new ArgumentNullException(nameof(locationDto), "LocationDto cannot be null.");
            }

            var location = _context.Locations.FirstOrDefault(l => l.LocationId == id);
            if (location == null)
            {
                throw new KeyNotFoundException($"Location with LocationId: {id} not found.");
            }

            location.LocationName = locationDto.LocationName;
            location.Code = locationDto.Code;

            _context.SaveChanges();
            return locationDto;
        }

        public LocationDto PostBusLocation(LocationDto locationDto)
        {
            if (locationDto == null)
            {
                throw new ArgumentNullException(nameof(locationDto), "LocationDto cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(locationDto.LocationName))
            {
                throw new ArgumentException("LocationName is required.");
            }

            if (string.IsNullOrWhiteSpace(locationDto.Code))
            {
                throw new ArgumentException("Code is required.");
            }

            var location = new Location
            {
                LocationName = locationDto.LocationName,
                Code = locationDto.Code
            };

            _context.Locations.Add(location);
            _context.SaveChanges();

            locationDto.LocationId = location.LocationId;
            return locationDto;
        }

        public void DeleteBusLocation(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid LocationId. LocationId must be greater than 0.");
            }

            var location = _context.Locations.FirstOrDefault(l => l.LocationId == id);
            if (location == null)
            {
                throw new KeyNotFoundException($"Location with LocationId: {id} not found.");
            }

            _context.Locations.Remove(location);
            _context.SaveChanges();
        }

        // Bus schedule methods
        public IEnumerable<BusScheduleDto> GetBusSchedules()
        {
            var schedules = _context.BusSchedules
                .Include(bs => bs.FromLocation)
                .Include(bs => bs.ToLocation)
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
                    TotalSeats = bs.TotalSeats
                }).ToList();

            if (!schedules.Any())
            {
                throw new KeyNotFoundException("No bus schedules found.");
            }

            return schedules;
        }

        public IEnumerable<BusScheduleDto> SearchBus(int from, int to, string date)
        {
            if (from == 0)
            {
                throw new ArgumentException("From location cannot be empty.");
            }

            if (to == 0)
            {
                throw new ArgumentException("To location cannot be empty.");
            }

            if (!DateTime.TryParse(date, out DateTime searchDate))
            {
                throw new ArgumentException("Invalid date format.");
            }

            var schedules = _context.BusSchedules
                .Include(bs => bs.FromLocation)
                .Include(bs => bs.ToLocation)
                .Where(bs =>
                    bs.FromLocation.LocationId == from &&
                    bs.ToLocation.LocationId == to &&
                    bs.ScheduleDate.Date == searchDate.Date)
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
                    TotalSeats = bs.TotalSeats
                })
                .ToList();

            if (!schedules.Any())
            {
                throw new KeyNotFoundException("No buses found for the specified criteria.");
            }

            return schedules;
        }


        public IEnumerable<int> GetBookedSeats(int scheduleId)
        {
            if (scheduleId <= 0)
            {
                throw new ArgumentException("Invalid ScheduleId. ScheduleId must be greater than 0.");
            }

            var bookedSeats = _context.BusBookings
                .Where(bb => bb.ScheduleId == scheduleId)
                .SelectMany(bb => bb.BusBookingPassengers.Select(p => p.SeatNo))
                .ToList();

            return bookedSeats;
        }

        public BusScheduleDto GetBusScheduleById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ScheduleId. ScheduleId must be greater than 0.");
            }

            var schedule = _context.BusSchedules
                .Include(bs => bs.FromLocation)
                .Include(bs => bs.ToLocation)
                .FirstOrDefault(bs => bs.ScheduleId == id);

            if (schedule == null)
            {
                throw new KeyNotFoundException($"Bus schedule with ScheduleId: {id} not found.");
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
                TotalSeats = schedule.TotalSeats
            };
        }

        public BusScheduleDto PutBusSchedule(int id, BusScheduleDto busScheduleDto)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ScheduleId. ScheduleId must be greater than 0.");
            }

            if (busScheduleDto == null)
            {
                throw new ArgumentNullException(nameof(busScheduleDto), "BusScheduleDto cannot be null.");
            }

            var schedule = _context.BusSchedules.FirstOrDefault(bs => bs.ScheduleId == id);
            if (schedule == null)
            {
                throw new KeyNotFoundException($"Bus schedule with ScheduleId: {id} not found.");
            }

            schedule.BusName = busScheduleDto.BusName;
            schedule.BusVehicleNo = busScheduleDto.BusVehicleNo;
            schedule.FromLocationId = busScheduleDto.FromLocationId;
            schedule.ToLocationId = busScheduleDto.ToLocationId;
            schedule.DepartureTime = busScheduleDto.DepartureTime;
            schedule.ArrivalTime = busScheduleDto.ArrivalTime;
            schedule.ScheduleDate = busScheduleDto.ScheduleDate;
            schedule.Price = busScheduleDto.Price;
            schedule.TotalSeats = busScheduleDto.TotalSeats;

            _context.SaveChanges();
            return busScheduleDto;
        }

        public BusScheduleDto PostBusSchedule(BusScheduleDto busScheduleDto)
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
                TotalSeats = busScheduleDto.TotalSeats
            };

            _context.BusSchedules.Add(schedule);
            _context.SaveChanges();

            busScheduleDto.ScheduleId = schedule.ScheduleId;
            return busScheduleDto;
        }

        public void DeleteBusSchedule(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ScheduleId. ScheduleId must be greater than 0.");
            }

            var schedule = _context.BusSchedules.FirstOrDefault(bs => bs.ScheduleId == id);
            if (schedule == null)
            {
                throw new KeyNotFoundException($"Bus schedule with ScheduleId: {id} not found.");
            }

            _context.BusSchedules.Remove(schedule);
            _context.SaveChanges();
        }

        // Bus booking methods
        public IEnumerable<BusBookingDto> GetAllBusBookings()
        {
            var bookings = _context.BusBookings
                .Include(bb => bb.BusBookingPassengers)
                .Select(bb => new BusBookingDto
                {
                    BookingId = bb.BookingId,
                    CustId = bb.CustId,
                    BookingDate = bb.BookingDate,
                    ScheduleId = bb.ScheduleId,
                    BusBookingPassengers = bb.BusBookingPassengers.Select(p => new BusBookingPassengerDto
                    {
                        PassengerId = p.PassengerId,
                        BookingId = p.BookingId,
                        PassengerName = p.PassengerName,
                        Age = p.Age,
                        Gender = p.Gender,
                        SeatNo = p.SeatNo
                    }).ToList()
                }).ToList();

            if (!bookings.Any())
            {
                throw new KeyNotFoundException("No bus bookings found.");
            }

            return bookings;
        }

        public BusBookingDto GetBusBooking(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid BookingId. BookingId must be greater than 0.");
            }

            var booking = _context.BusBookings
                .Include(bb => bb.BusBookingPassengers)
                .FirstOrDefault(bb => bb.BookingId == id);

            if (booking == null)
            {
                throw new KeyNotFoundException($"Bus booking with BookingId: {id} not found.");
            }

            return new BusBookingDto
            {
                BookingId = booking.BookingId,
                CustId = booking.CustId,
                BookingDate = booking.BookingDate,
                ScheduleId = booking.ScheduleId,
                BusBookingPassengers = booking.BusBookingPassengers.Select(p => new BusBookingPassengerDto
                {
                    PassengerId = p.PassengerId,
                    BookingId = p.BookingId,
                    PassengerName = p.PassengerName,
                    Age = p.Age,
                    Gender = p.Gender,
                    SeatNo = p.SeatNo
                }).ToList()
            };
        }

        public BusBookingDto PostBusBooking(BusBookingDto busBookingDto)
        {
            if (busBookingDto == null)
            {
                throw new ArgumentNullException(nameof(busBookingDto), "BusBookingDto cannot be null.");
            }

            if (busBookingDto.CustId <= 0)
            {
                throw new ArgumentException("Invalid CustId. CustId must be greater than 0.");
            }

            if (busBookingDto.ScheduleId <= 0)
            {
                throw new ArgumentException("Invalid ScheduleId. ScheduleId must be greater than 0.");
            }

            if (busBookingDto.BusBookingPassengers == null || !busBookingDto.BusBookingPassengers.Any())
            {
                throw new ArgumentException("At least one passenger is required.");
            }

            var booking = new BusBooking.Core.Entites.BusBooking
            {
                CustId = busBookingDto.CustId,
                BookingDate = busBookingDto.BookingDate,
                ScheduleId = busBookingDto.ScheduleId,
                BusBookingPassengers = busBookingDto.BusBookingPassengers.Select(p => new BusBookingPassenger
                {
                    PassengerName = p.PassengerName,
                    Age = p.Age,
                    Gender = p.Gender,
                    SeatNo = p.SeatNo
                }).ToList()
            };

            _context.BusBookings.Add(booking);
            _context.SaveChanges();

            busBookingDto.BookingId = booking.BookingId;
            return busBookingDto;
        }

        public void DeleteBusBooking(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid BookingId. BookingId must be greater than 0.");
            }

            var booking = _context.BusBookings.FirstOrDefault(bb => bb.BookingId == id);
            if (booking == null)
            {
                throw new KeyNotFoundException($"Bus booking with BookingId: {id} not found.");
            }

            _context.BusBookings.Remove(booking);
            _context.SaveChanges();
        }
    }
}