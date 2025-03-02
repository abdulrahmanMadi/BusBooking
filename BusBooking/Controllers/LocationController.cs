using BusBooking.Controllers;
using BusBooking.Core.Dtos.Location;
using BusBooking.Core.Interfaces;
using BusBooking.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

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
            var locations = _locationRepository.GetAllLocations();
            return Ok(new
            {
                message = "Locations retrieved successfully",
                result = true,
                data = locations
            });
        }

        [HttpGet("GetBusLocationById/{locationId}")]
        public IActionResult GetLocationById(int locationId)
        {
            var location = _locationRepository.GetLocationById(locationId);
            if (location == null)
                return NotFound(new { message = "Location not found", result = false });

            return Ok(new
            {
                message = "Location retrieved successfully",
                result = true,
                data = location
            });
        }

        [HttpPost("PostBusLocation")]
        public IActionResult CreateLocation([FromBody] LocationDto locationDto)
        {
            var result = _locationRepository.CreateLocation(locationDto);
            return Ok(new
            {
                message = "Location created successfully",
                result = true,
                data = result
            });
        }

        [HttpPut("PutBusLocation/{locationId}")]
        public IActionResult UpdateLocation(int locationId, [FromBody] LocationDto locationDto)
        {
            var result = _locationRepository.UpdateLocation(locationId, locationDto);
            if (result == null)
                return NotFound(new { message = "Location not found", result = false });

            return Ok(new
            {
                message = "Location updated successfully",
                result = true,
                data = result
            });
        }

        [HttpDelete("DeleteBusLocation/{locationId}")]
        public IActionResult DeleteLocation(int locationId)
        {
            _locationRepository.DeleteLocation(locationId);
            return Ok(new
            {
                message = "Location deleted successfully",
                result = true
            });
        }
    }
}
