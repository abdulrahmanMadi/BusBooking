using BusBooking.Core.Dtos;
using BusBooking.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto loginRequest)
        {
            var user = _userRepository.Authenticate(loginRequest);
            if (user == null)
                return Unauthorized(new { message = "Invalid credentials", result = false });

            var token = GenerateJwtToken(user);
            return Ok(new
            {
                message = "Login successful",
                result = true,
                data = new LoginResponseDto { Token = token, UserName = user.UserName }
            });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestDto registerRequest)
        {
            _userRepository.Register(registerRequest);
            return Ok(new
            {
                message = "User registered successfully",
                result = true
            });
        }

        [HttpPost("AddNewUser")]
        public IActionResult AddNewUser([FromBody] AddNewUserDto addNewUserDto)
        {
            _userRepository.AddNewUser(addNewUserDto);
            return Ok(new
            {
                message = "User added successfully",
                result = true
            });
        }

        private string GenerateJwtToken(UserDto user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpiryInMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}