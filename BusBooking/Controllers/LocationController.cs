using BusBooking.Core.Dtos.Location;
using BusBooking.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BusBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;

        public LocationController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        [HttpGet("GetBusLocations")]
        public IActionResult GetAllLocations()
        {
            try
            {
                var locations = _locationRepository.GetAllLocations();
                return Ok(new
                {
                    message = "Locations retrieved successfully",
                    result = true,
                    data = locations
                });
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

        [HttpGet("GetLocationById/{locationId}")]
        public IActionResult GetLocationById(int locationId)
        {
            try
            {
                var location = _locationRepository.GetLocationById(locationId);
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

        [HttpPost("CreateLocation")]
        public IActionResult CreateLocation([FromBody] LocationDto locationDto)
        {
            try
            {
                var result = _locationRepository.CreateLocation(locationDto);
                return Ok(new
                {
                    message = "Location created successfully",
                    result = true,
                    data = result
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

        [HttpPut("UpdateLocation/{locationId}")]
        public IActionResult UpdateLocation(int locationId, [FromBody] LocationDto locationDto)
        {
            try
            {
                var result = _locationRepository.UpdateLocation(locationId, locationDto);
                return Ok(new
                {
                    message = "Location updated successfully",
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

        [HttpDelete("DeleteLocation/{locationId}")]
        public IActionResult DeleteLocation(int locationId)
        {
            try
            {
                _locationRepository.DeleteLocation(locationId);
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
    }
}