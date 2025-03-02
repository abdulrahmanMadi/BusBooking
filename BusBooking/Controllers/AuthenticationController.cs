using BusBooking.Core.Dtos.Auth;
using BusBooking.Core.Dtos.User;
using BusBooking.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IAuthenticationRepository authenticationRepository, IConfiguration configuration)
        {
            _authenticationRepository = authenticationRepository;
            _configuration = configuration;
        }

        // Login endpoint
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto loginRequest)
        {
            var loginResponse = _authenticationRepository.Authenticate(loginRequest);
            if (loginResponse == null)
                return Unauthorized(new { message = "Invalid credentials", result = false });

            return Ok(new
            {
                message = "Login successful",
                result = true,
                data = loginResponse
            });
        }

        // Register endpoint
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestDto registerRequest)
        {
            _authenticationRepository.Register(registerRequest);
            return Ok(new
            {
                message = "User registered successfully",
                result = true
            });
        }

        // Refresh token endpoint
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromBody] RefreshTokenRequestDto refreshTokenRequest)
        {
            var userDto = _authenticationRepository.GetUserByRefreshToken(refreshTokenRequest.RefreshToken);
            if (userDto == null || userDto.RefreshTokenExpiryTime < DateTime.UtcNow)
                return Unauthorized(new { message = "Invalid refresh token", result = false });

            var token = GenerateJwtToken(userDto);
            var newRefreshToken = GenerateRefreshToken();

            // Update user's refresh token
            _authenticationRepository.UpdateRefreshToken(userDto.UserId, newRefreshToken, DateTime.UtcNow.AddDays(7));

            return Ok(new
            {
                message = "Token refreshed successfully",
                result = true,
                data = new RefreshTokenResponseDto
                {
                    Token = token,
                    RefreshToken = newRefreshToken
                }
            });
        }

        // Helper method to generate JWT token
        private string GenerateJwtToken(UserDto user)
        {
            // Get JWT settings from configuration
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

            // Create token descriptor
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

            // Generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // Helper method to generate refresh token
        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}