using BusBooking.Core.Dtos;
using BusBooking.Core.Dtos.Vendor;
using BusBooking.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BusBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public VendorController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Get all vendors
        [HttpGet("GetBusVendors")]
        public IActionResult GetAllVendors()
        {
            var vendors = _userRepository.GetAllVendors();
            return Ok(new
            {
                message = "Vendors retrieved successfully",
                result = true,
                data = vendors
            });
        }

        // Get vendor by ID
        [HttpGet("GetVendorById/{userId}")]
        public IActionResult GetVendorById(int userId)
        {
            var vendor = _userRepository.GetVendorById(userId);
            if (vendor == null)
                return NotFound(new
                {
                    message = "Vendor not found",
                    result = false
                });

            return Ok(new
            {
                message = "Vendor retrieved successfully",
                result = true,
                data = vendor
            });
        }

        // Create a new vendor
        [HttpPost("PostBusVendor")]
        public IActionResult CreateVendor([FromBody] CreateVendorDto createVendorDto)
        {
            _userRepository.CreateVendor(createVendorDto);
            return Ok(new
            {
                message = "Vendor created successfully",
                result = true
            });
        }

        // Update an existing vendor
        [HttpPut("UpdateVendor/{userId}")]
        public IActionResult UpdateVendor(int userId, [FromBody] UpdateVendorDto updateVendorDto)
        {
            _userRepository.UpdateVendor(userId, updateVendorDto);
            return Ok(new
            {
                message = "Vendor updated successfully",
                result = true
            });
        }

        // Delete a vendor
        [HttpDelete("DeleteVendor/{userId}")]
        public IActionResult DeleteVendor(int userId)
        {
            _userRepository.DeleteVendor(userId);
            return Ok(new
            {
                message = "Vendor deleted successfully",
                result = true
            });
        }
    }
}