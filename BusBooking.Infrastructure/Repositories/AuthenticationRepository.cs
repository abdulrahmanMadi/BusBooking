// BusBooking.Infrastructure/Repositories/AuthenticationRepository.cs
using BusBooking.Core.Dtos;
using BusBooking.Core.Dtos.Auth;
using BusBooking.Core.Dtos.User;
using BusBooking.Core.Entites;
using BusBooking.Core.Interfaces;
using BusBooking.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BusBooking.Infrastructure.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticationRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Authenticate user and generate JWT token
        public LoginResponseDto Authenticate(LoginRequestDto loginRequest)
        {
            // Find the user by username and password
            var user = _context.Users.SingleOrDefault(u => u.UserName == loginRequest.UserName && u.Password == loginRequest.Password);
            if (user == null)
                return null;

            // Generate JWT token
            var token = GenerateJwtToken(user);

            // Generate refresh token
            var refreshToken = GenerateRefreshToken();

            // Update user's refresh token in the database
            UpdateRefreshToken(user.UserId, refreshToken, DateTime.UtcNow.AddDays(7));

            // Return the login response
            return new LoginResponseDto
            {
                Token = token,
                UserName = user.UserName,
                RefreshToken = refreshToken
            };
        }

        // Register a new user
        public void Register(RegisterRequestDto registerRequest)
        {
            // Create a new user entity
            var user = new User
            {
                UserName = registerRequest.UserName,
                EmailId = registerRequest.EmailId,
                FullName = registerRequest.FullName,
                RoleId = registerRequest.RoleId, // Assign role
                CreatedDate = DateTime.UtcNow,
                Password = registerRequest.Password,
                ProjectName = "BusBooking",
                RefreshToken = null,
                RefreshTokenExpiryTime = null,
                ContactNo = registerRequest.ContactNo // Additional field for vendors
            };

            // Add the user to the database
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        // Update user's refresh token
        public void UpdateRefreshToken(int userId, string refreshToken, DateTime expiryTime)
        {
            // Find the user by ID
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
                return;

            // Update the refresh token and expiry time
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = expiryTime;

            // Save changes to the database
            _context.SaveChanges();
        }

        // Get user by refresh token
        public UserDto GetUserByRefreshToken(string refreshToken)
        {
            // Find the user by refresh token
            var user = _context.Users.FirstOrDefault(u => u.RefreshToken == refreshToken);
            if (user == null)
                return null;

            // Map the user entity to a DTO
            return new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                EmailId = user.EmailId,
                FullName = user.FullName,
                Role = user.Role.RoleName,
                CreatedDate = user.CreatedDate,
                ProjectName = user.ProjectName,
                RefreshToken = user.RefreshToken,
                RefreshTokenExpiryTime = user.RefreshTokenExpiryTime
            };
        }

        // Helper method to generate JWT token
        private string GenerateJwtToken(User user)
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
                    new Claim(ClaimTypes.Role, user.Role.RoleName)
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