using BusBooking.Core.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BusBooking.Infrastructure.Data
{
    public class DataSeeder
    {
        private readonly ApplicationDbContext _context;

        public DataSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            // Ensure the database is created
            //_context.Database.EnsureCreated();

            //// 2. Seed Users
            //if (!_context.Users.Any())
            //{
            //    _context.Users.AddRange(
            //        new User
            //        {
            //            UserName = "admin",
            //            EmailId = "admin@example.com",
            //            FullName = "Admin User",
            //            RoleId = 1,
            //            CreatedDate = DateTime.UtcNow,
            //            Password = "admin123",
            //            ContactNo = "01012345678"
            //        },
            //        new User
            //        {
            //            UserName = "vendor1",
            //            EmailId = "vendor1@example.com",
            //            FullName = "Vendor One",
            //            RoleId = 3,
            //            CreatedDate = DateTime.UtcNow,
            //            Password = "vendor123",
            //            ContactNo = "01123456789"
            //        },
            //        new User
            //        {
            //            UserName = "customer1",
            //            EmailId = "customer1@example.com",
            //            FullName = "Customer One",
            //            RoleId = 2,
            //            CreatedDate = DateTime.UtcNow,
            //            Password = "customer123",
            //            ContactNo = "01234567890"
            //        },
            //        // Add more vendors and customers
            //        new User
            //        {
            //            UserName = "vendor2",
            //            EmailId = "vendor2@example.com",
            //            FullName = "Vendor Two",
            //            RoleId = 3,
            //            CreatedDate = DateTime.UtcNow,
            //            Password = "vendor123",
            //            ContactNo = "01156789432"
            //        },
            //        new User
            //        {
            //            UserName = "customer2",
            //            EmailId = "customer2@example.com",
            //            FullName = "Customer Two",
            //            RoleId = 2,
            //            CreatedDate = DateTime.UtcNow,
            //            Password = "customer234",
            //            ContactNo = "01265478901"
            //        },
            //        new User
            //        {
            //            UserName = "customer3",
            //            EmailId = "customer3@example.com",
            //            FullName = "Customer Three",
            //            RoleId = 2,
            //            CreatedDate = DateTime.UtcNow,
            //            Password = "customer345",
            //            ContactNo = "01287654321"
            //        }
            //    );
            //    _context.SaveChanges();
            //}

            //// 3. Seed Locations
            //if (!_context.Locations.Any())
            //{
            //    _context.Locations.AddRange(
            //        new Location { LocationName = "Cairo", Code = "CAI" },
            //        new Location { LocationName = "Alexandria", Code = "ALX" },
            //        new Location { LocationName = "Luxor", Code = "LXR" },
            //        new Location { LocationName = "Aswan", Code = "ASW" },
            //        new Location { LocationName = "Sharm El Sheikh", Code = "SSH" },
            //        new Location { LocationName = "Hurghada", Code = "HRG" },
            //        new Location { LocationName = "Giza", Code = "GIZ" },
            //        new Location { LocationName = "Port Said", Code = "PSD" }
            //    );
            //    _context.SaveChanges();
            //}

            //// 4. Seed LocationAddresses
            //if (!_context.LocationAddresses.Any())
            //{
            //    _context.LocationAddresses.AddRange(
            //        new LocationAddress
            //        {
            //            LocationId = 1, // Cairo
            //            LocationTitle = "Cairo Bus Terminal",
            //            LocationAddressString = "Ramses Station, Cairo, Egypt"
            //        },
            //        new LocationAddress
            //        {
            //            LocationId = 2, // Alexandria
            //            LocationTitle = "Alexandria Bus Station",
            //            LocationAddressString = "Midan Saad Zaghloul, Alexandria, Egypt"
            //        },
            //        new LocationAddress
            //        {
            //            LocationId = 3, // Luxor
            //            LocationTitle = "Luxor Bus Depot",
            //            LocationAddressString = "Luxor Train Station, Luxor, Egypt"
            //        },
            //        new LocationAddress
            //        {
            //            LocationId = 4, // Aswan
            //            LocationTitle = "Aswan Bus Stand",
            //            LocationAddressString = "Aswan Train Station, Aswan, Egypt"
            //        },
            //        new LocationAddress
            //        {
            //            LocationId = 5, // Sharm El Sheikh
            //            LocationTitle = "Sharm El Sheikh Bus Terminal",
            //            LocationAddressString = "Sharm El Sheikh, South Sinai, Egypt"
            //        },
            //        new LocationAddress
            //        {
            //            LocationId = 6, // Hurghada
            //            LocationTitle = "Hurghada Central Station",
            //            LocationAddressString = "Hurghada, Red Sea Governorate, Egypt"
            //        },
            //        new LocationAddress
            //        {
            //            LocationId = 7, // Giza
            //            LocationTitle = "Giza Bus Terminal",
            //            LocationAddressString = "Giza Square, Giza, Egypt"
            //        },
            //        new LocationAddress
            //        {
            //            LocationId = 8, // Port Said
            //            LocationTitle = "Port Said Main Bus Station",
            //            LocationAddressString = "Port Said, Egypt"
            //        }
            //    );
            //    _context.SaveChanges();
            //}

            //// 5. Seed Bus Schedules
            //if (!_context.BusSchedules.Any())
            //{
            //    var vendor1 = _context.Users.FirstOrDefault(u => u.UserName == "vendor1");
            //    var vendor2 = _context.Users.FirstOrDefault(u => u.UserName == "vendor2");

            //    if (vendor1 == null || vendor2 == null)
            //    {
            //        throw new Exception("Vendor users not found. Ensure users are seeded correctly.");
            //    }

            //    _context.BusSchedules.AddRange(
            //        new BusSchedule
            //        {
            //            VendorId = vendor1.UserId,
            //            BusName = "Cairo to Alexandria Express",
            //            BusVehicleNo = "EG1234",
            //            FromLocationId = 1, // Cairo
            //            ToLocationId = 2, // Alexandria
            //            DepartureTime = DateTime.UtcNow.AddHours(2),
            //            ArrivalTime = DateTime.UtcNow.AddHours(5),
            //            ScheduleDate = DateTime.UtcNow.Date,
            //            Price = 200,
            //            TotalSeats = 50
            //        },
            //        new BusSchedule
            //        {
            //            VendorId = vendor1.UserId,
            //            BusName = "Cairo to Luxor Luxury Bus",
            //            BusVehicleNo = "EG5678",
            //            FromLocationId = 1, // Cairo
            //            ToLocationId = 3, // Luxor
            //            DepartureTime = DateTime.UtcNow.AddHours(3),
            //            ArrivalTime = DateTime.UtcNow.AddHours(10),
            //            ScheduleDate = DateTime.UtcNow.Date,
            //            Price = 500,
            //            TotalSeats = 40
            //        },
            //        new BusSchedule
            //        {
            //            VendorId = vendor2.UserId,
            //            BusName = "Alexandria to Hurghada",
            //            BusVehicleNo = "EG1002",
            //            FromLocationId = 2, // Alexandria
            //            ToLocationId = 6, // Hurghada
            //            DepartureTime = DateTime.UtcNow.AddHours(4),
            //            ArrivalTime = DateTime.UtcNow.AddHours(9),
            //            ScheduleDate = DateTime.UtcNow.Date.AddDays(1),
            //            Price = 450,
            //            TotalSeats = 60
            //        },
            //        new BusSchedule
            //        {
            //            VendorId = vendor2.UserId,
            //            BusName = "Giza to Port Said",
            //            BusVehicleNo = "EG9999",
            //            FromLocationId = 7, // Giza
            //            ToLocationId = 8, // Port Said
            //            DepartureTime = DateTime.UtcNow.AddHours(6),
            //            ArrivalTime = DateTime.UtcNow.AddHours(12),
            //            ScheduleDate = DateTime.UtcNow.Date,
            //            Price = 300,
            //            TotalSeats = 55
            //        }
            //    );
            //    _context.SaveChanges();
            //}

            //// 6. Seed Bus Bookings
            //if (!_context.BusBookings.Any())
            //{
            //    var customer1 = _context.Users.FirstOrDefault(u => u.UserName == "customer1");
            //    var customer2 = _context.Users.FirstOrDefault(u => u.UserName == "customer2");
            //    var schedule = _context.BusSchedules.FirstOrDefault();

            //    if (customer1 == null || customer2 == null || schedule == null)
            //    {
            //        throw new Exception("Required data for BusBooking not found.");
            //    }

            //    _context.BusBookings.AddRange(
            //        new BusBooking.Core.Entites.BusBooking
            //        {
            //            CustId = customer1.UserId,
            //            BookingDate = DateTime.UtcNow,
            //            ScheduleId = schedule.ScheduleId
            //        },
            //        new BusBooking.Core.Entites.BusBooking
            //        {
            //            CustId = customer2.UserId,
            //            BookingDate = DateTime.UtcNow.AddDays(1),
            //            ScheduleId = schedule.ScheduleId
            //        }
            //    );
            //    _context.SaveChanges();
            //}

            // 7. Seed Bus Booking Passengers
            // Seed data ensuring unique composite keys
            //if (!_context.BusBookingPassengers.Any())
            //{
            //    _context.BusBookingPassengers.AddRange(
            //        new BusBookingPassenger
            {
                //            PassengerId = 1,
                //            BookingId = 1,
                //            PassengerName = "Ali Hassan",
                //            Age = 23,
                //            Gender = "Male",
                //            SeatNo = 10
                //        },
                //        new BusBookingPassenger
                //        {
                //            PassengerId = 2,
                //            BookingId = 2, // Unique BookingId
                //            PassengerName = "Sara Ahmed",
                //            Age = 27,
                //            Gender = "Female",
                //            SeatNo = 8
                //        }
                //    );
                //    _context.SaveChanges();
                //}

            }
        }
    }
}