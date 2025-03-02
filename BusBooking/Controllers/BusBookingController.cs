using BusBooking.Core.Dtos.Bus;
using BusBooking.Core.Interfaces;
using BusBooking.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BusBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusBookingController : ControllerBase
    {
        private readonly IBusBookingRepository _busBookingRepository;

        public BusBookingController(IBusBookingRepository busBookingRepository)
        {
            _busBookingRepository = busBookingRepository;
        }

        [HttpGet("GetAllBusBookings/{vendorId}")]
        public IActionResult GetAllBusBookings(int vendorId)
        {
            var bookings = _busBookingRepository.GetAllBusBookings(vendorId);
            return Ok(bookings);
        }

        [HttpGet("GetBusBooking/{bookingId}")]
        public IActionResult GetBusBookingById(int bookingId)
        {
            var booking = _busBookingRepository.GetBusBookingById(bookingId);
            if (booking == null) return NotFound();
            return Ok(booking);
        }

        [HttpPost("PostBusBooking")]
        public IActionResult CreateBusBooking([FromBody] BusBookingDto busBookingDto)
        {
            var result = _busBookingRepository.CreateBusBooking(busBookingDto);
            return Ok(result);
        }

        [HttpDelete("DeleteBusBooking/{bookingId}")]
        public IActionResult DeleteBusBooking(int bookingId)
        {
            _busBookingRepository.DeleteBusBooking(bookingId);
            return Ok();
        }
    }
}