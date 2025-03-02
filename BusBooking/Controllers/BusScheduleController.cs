using BusBooking.Core.Dtos.Bus;
using BusBooking.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BusBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusScheduleController : ControllerBase
    {
        private readonly IBusScheduleRepository _busScheduleRepository;

        public BusScheduleController(IBusScheduleRepository busScheduleRepository)
        {
            _busScheduleRepository = busScheduleRepository;
        }

        // Get all bus schedules for a vendor
        [HttpGet("GetBusSchedules")]
        public IActionResult GetBusSchedules(int vendorId)
        {
            var schedules = _busScheduleRepository.GetBusSchedulesByVendor(vendorId);
            return Ok(new
            {
                message = "Bus schedules retrieved successfully",
                result = true,
                data = schedules
            });
        }

        // Get bus schedule by ID
        [HttpGet("GetBusScheduleById/{scheduleId}")]
        public IActionResult GetBusScheduleById(int scheduleId)
        {
            var schedule = _busScheduleRepository.GetBusScheduleById(scheduleId);
            if (schedule == null)
                return NotFound(new { message = "Bus schedule not found", result = false });

            return Ok(new
            {
                message = "Bus schedule retrieved successfully",
                result = true,
                data = schedule
            });
        }

        // Create a new bus schedule
        [HttpPost("PostBusSchedule")]
        public IActionResult CreateBusSchedule([FromBody] BusScheduleDto busScheduleDto)
        {
            var result = _busScheduleRepository.CreateBusSchedule(busScheduleDto);
            return Ok(new
            {
                message = "Bus schedule created successfully",
                result = true,
                data = result
            });
        }

        // Update bus schedule
        [HttpPut("PutBusSchedule/{scheduleId}")]
        public IActionResult UpdateBusSchedule(int scheduleId, [FromBody] BusScheduleDto busScheduleDto)
        {
            var result = _busScheduleRepository.UpdateBusSchedule(scheduleId, busScheduleDto);
            if (result == null)
                return NotFound(new { message = "Bus schedule not found", result = false });

            return Ok(new
            {
                message = "Bus schedule updated successfully",
                result = true,
                data = result
            });
        }

        // Delete bus schedule
        [HttpDelete("DeleteBusSchedule/{scheduleId}")]
        public IActionResult DeleteBusSchedule(int scheduleId)
        {
            _busScheduleRepository.DeleteBusSchedule(scheduleId);
            return Ok(new
            {
                message = "Bus schedule deleted successfully",
                result = true
            });
        }
    }
}