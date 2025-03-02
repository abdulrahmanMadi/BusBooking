using BusBooking.Core.Dtos;
using BusBooking.Core.Dtos.User;
using BusBooking.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BusBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Get all users
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            return Ok(new
            {
                message = "Users retrieved successfully",
                result = true,
                data = users
            });
        }

        // Get user by ID
        [HttpGet("GetUserById/{userId}")]
        public IActionResult GetUserById(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
                return NotFound(new
                {
                    message = "User not found",
                    result = false
                });

            return Ok(new
            {
                message = "User retrieved successfully",
                result = true,
                data = user
            });
        }

        // Update user
        [HttpPut("UpdateUser/{userId}")]
        public IActionResult UpdateUser(int userId, [FromBody] UpdateUserDto updateUserDto)
        {
            _userRepository.UpdateUser(userId, updateUserDto);
            return Ok(new
            {
                message = "User updated successfully",
                result = true
            });
        }

        // Delete user
        [HttpDelete("DeleteUser/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            _userRepository.DeleteUser(userId);
            return Ok(new
            {
                message = "User deleted successfully",
                result = true
            });
        }
    }
}