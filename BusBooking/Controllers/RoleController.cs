using BusBooking.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BusBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public RoleController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Get all roles
        [HttpGet("GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            var roles = _userRepository.GetAllRoles();
            return Ok(new
            {
                message = "Roles retrieved successfully",
                result = true,
                data = roles
            });
        }
    }
}