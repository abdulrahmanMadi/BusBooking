using BusBooking.Core.Dtos.Bus;
using BusBooking.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

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

        [HttpGet("GetBusSchedulesByVendor/{vendorId}")]
        public IActionResult GetBusSchedulesByVendor(int vendorId)
        {
            try
            {
                var schedules = _busScheduleRepository.GetBusSchedulesByVendor(vendorId);
                return Ok(new
                {
                    message = "Bus schedules retrieved successfully",
                    result = true,
                    data = schedules
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

        [HttpGet("GetBusScheduleById/{scheduleId}")]
        public IActionResult GetBusScheduleById(int scheduleId)
        {
            try
            {
                var schedule = _busScheduleRepository.GetBusScheduleById(scheduleId);
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

        [HttpPost("CreateBusSchedule")]
        public IActionResult CreateBusSchedule([FromBody] BusScheduleDto busScheduleDto)
        {
            try
            {
                var result = _busScheduleRepository.CreateBusSchedule(busScheduleDto);
                return Ok(new
                {
                    message = "Bus schedule created successfully",
                    result = true,
                    data = result
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

        [HttpPut("UpdateBusSchedule/{scheduleId}")]
        public IActionResult UpdateBusSchedule(int scheduleId, [FromBody] BusScheduleDto busScheduleDto)
        {
            try
            {
                var result = _busScheduleRepository.UpdateBusSchedule(scheduleId, busScheduleDto);
                return Ok(new
                {
                    message = "Bus schedule updated successfully",
                    result = true,
                    data = result
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

        [HttpDelete("DeleteBusSchedule/{scheduleId}")]
        public IActionResult DeleteBusSchedule(int scheduleId)
        {
            try
            {
                _busScheduleRepository.DeleteBusSchedule(scheduleId);
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
    }
}