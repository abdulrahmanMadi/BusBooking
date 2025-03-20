using BusBooking.Core.Dtos.Bus;
using BusBooking.Core.Dtos.Location;
using BusBooking.Core.Interfaces;
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

        [HttpGet("GetBusLocationById/{id}")]
        public IActionResult GetBusLocationById(int id)
        {
            try
            {
                var location = _busBookingRepository.GetBusLocationById(id);
                return Ok(new
                {
                    message = "Location retrieved successfully",
                    result = true,
                    data = location
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message, result = false });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message, result = false });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, result = false });
            }
        }

        [HttpGet("getAddressByLocationId/{id}")]
        public IActionResult GetAddressByLocationId(int id)
        {
            try
            {
                var address = _busBookingRepository.GetAddressByLocationId(id);
                return Ok(new
                {
                    message = "Address retrieved successfully",
                    result = true,
                    data = address
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message, result = false });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message, result = false });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, result = false });
            }
        }

        [HttpPost("PostBusLocationAddress")]
        public IActionResult PostBusLocationAddress([FromBody] LocationAddressDto locationAddressDto)
        {
            try
            {
                var result = _busBookingRepository.PostBusLocationAddress(locationAddressDto);
                return Ok(new
                {
                    message = "Location address created successfully",
                    result = true,
                    data = result
                });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message, result = false });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message, result = false });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, result = false });
            }
        }

        [HttpPut("PutBusLocation/{id}")]
        public IActionResult PutBusLocation(int id, [FromBody] LocationDto locationDto)
        {
            try
            {
                var result = _busBookingRepository.PutBusLocation(id, locationDto);
                return Ok(new
                {
                    message = "Location updated successfully",
                    result = true,
                    data = result
                });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message, result = false });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message, result = false });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message, result = false });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, result = false });
            }
        }

        [HttpPost("PostBusLocation")]
        public IActionResult PostBusLocation([FromBody] LocationDto locationDto)
        {
            try
            {
                var result = _busBookingRepository.PostBusLocation(locationDto);
                return Ok(new
                {
                    message = "Location created successfully",
                    result = true,
                    data = result
                });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message, result = false });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message, result = false });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, result = false });
            }
        }

        [HttpDelete("DeleteBusLocation/{id}")]
        public IActionResult DeleteBusLocation(int id)
        {
            try
            {
                _busBookingRepository.DeleteBusLocation(id);
                return Ok(new
                {
                    message = "Location deleted successfully",
                    result = true
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message, result = false });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message, result = false });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, result = false });
            }
        }

        [HttpGet("GetBusSchedules")]
        public IActionResult GetBusSchedules()
        {
            try
            {
                var schedules = _busBookingRepository.GetBusSchedules();
                return Ok(new
                {
                    message = "Bus schedules retrieved successfully",
                    result = true,
                    data = schedules
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, result = false });
            }
        }

        [HttpGet("SearchBus")]
        public IActionResult SearchBus([FromQuery] int from, [FromQuery] int to, [FromQuery] string date)
        {
            try
            {
                var buses = _busBookingRepository.SearchBus(from, to, date);
                return Ok(new
                {
                    message = "Buses retrieved successfully",
                    result = true,
                    data = buses
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message, result = false });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message, result = false });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, result = false });
            }
        }

        [HttpGet("GetBookedSeats/{scheduleId}")]
        public IActionResult GetBookedSeats(int scheduleId)
        {
            try
            {
                var seats = _busBookingRepository.GetBookedSeats(scheduleId);
                return Ok(new
                {
                    message = "Booked seats retrieved successfully",
                    result = true,
                    data = seats
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message, result = false });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, result = false });
            }
        }

        [HttpGet("GetBusBookingsByCustomerId/{customerId}")]
        public IActionResult GetBusBookingsByCustomerId(int customerId)
        {
            try
            {
                var bookings = _busBookingRepository.GetBusBookingsByCustomerId(customerId);
                return Ok(new
                {
                    message = "Bus bookings retrieved successfully",
                    result = true,
                    data = bookings
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message, result = false });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message, result = false });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, result = false });
            }
        }

        [HttpGet("GetBusScheduleById/{id}")]
        public IActionResult GetBusScheduleById(int id)
        {
            try
            {
                var schedule = _busBookingRepository.GetBusScheduleById(id);
                return Ok(new
                {
                    message = "Bus schedule retrieved successfully",
                    result = true,
                    data = schedule
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message, result = false });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message, result = false });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, result = false });
            }
        }

        [HttpPut("PutBusSchedule/{id}")]
        public IActionResult PutBusSchedule(int id, [FromBody] BusScheduleDto busScheduleDto)
        {
            try
            {
                var result = _busBookingRepository.PutBusSchedule(id, busScheduleDto);
                return Ok(new
                {
                    message = "Bus schedule updated successfully",
                    result = true,
                    data = result
                });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message, result = false });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message, result = false });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message, result = false });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, result = false });
            }
        }

        [HttpPost("PostBusSchedule")]
        public IActionResult PostBusSchedule([FromBody] BusScheduleDto busScheduleDto)
        {
            try
            {
                var result = _busBookingRepository.PostBusSchedule(busScheduleDto);
                return Ok(new
                {
                    message = "Bus schedule created successfully",
                    result = true,
                    data = result
                });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message, result = false });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message, result = false });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, result = false });
            }
        }

        [HttpDelete("DeleteBusSchedule/{id}")]
        public IActionResult DeleteBusSchedule(int id)
        {
            try
            {
                _busBookingRepository.DeleteBusSchedule(id);
                return Ok(new
                {
                    message = "Bus schedule deleted successfully",
                    result = true
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message, result = false });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message, result = false });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, result = false });
            }
        }

        [HttpGet("GetAllBusBookings")]
        public IActionResult GetAllBusBookings()
        {
            try
            {
                var bookings = _busBookingRepository.GetAllBusBookings();
                return Ok(new
                {
                    message = "Bus bookings retrieved successfully",
                    result = true,
                    data = bookings
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, result = false });
            }
        }

        [HttpGet("GetBusBooking/{id}")]
        public IActionResult GetBusBooking(int id)
        {
            try
            {
                var booking = _busBookingRepository.GetBusBooking(id);
                return Ok(new
                {
                    message = "Bus booking retrieved successfully",
                    result = true,
                    data = booking
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message, result = false });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message, result = false });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, result = false });
            }
        }

        [HttpPost("PostBusBooking")]
        public IActionResult PostBusBooking([FromBody] BusBookingDto busBookingDto)
        {
            try
            {
                var result = _busBookingRepository.PostBusBooking(busBookingDto);
                return Ok(new
                {
                    message = "Bus booking created successfully",
                    result = true,
                    data = result
                });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message, result = false });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message, result = false });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, result = false });
            }
        }

        [HttpDelete("DeleteBusBooking/{id}")]
        public IActionResult DeleteBusBooking(int id)
        {
            try
            {
                _busBookingRepository.DeleteBusBooking(id);
                return Ok(new
                {
                    message = "Bus booking deleted successfully",
                    result = true
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message, result = false });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message, result = false });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, result = false });
            }
        }
    }
}