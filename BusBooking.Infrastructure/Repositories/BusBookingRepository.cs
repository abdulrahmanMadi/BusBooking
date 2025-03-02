using BusBooking.Core.Dtos;
using BusBooking.Core.Dtos.Bus;
using BusBooking.Core.Entites;
using BusBooking.Core.Interfaces;
using BusBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<BusBookingDto> GetAllBusBookings(int vendorId)
        {
            return _context.BusBookings
                .Where(bb => bb.Schedule.VendorId == vendorId)
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
        }

        public BusBookingDto GetBusBookingById(int bookingId)
        {
            var booking = _context.BusBookings.FirstOrDefault(bb => bb.BookingId == bookingId);
            return booking == null ? null : new BusBookingDto
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

        public BusBookingDto CreateBusBooking(BusBookingDto busBookingDto)
        {
            var busBooking = new BusBooking.Core.Entites.BusBooking
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

            _context.BusBookings.Add(busBooking);
            _context.SaveChanges();

            busBookingDto.BookingId = busBooking.BookingId;
            return busBookingDto;
        }

        public void DeleteBusBooking(int bookingId)
        {
            var booking = _context.BusBookings.FirstOrDefault(bb => bb.BookingId == bookingId);
            if (booking != null)
            {
                _context.BusBookings.Remove(booking);
                _context.SaveChanges();
            }
        }
    }
}