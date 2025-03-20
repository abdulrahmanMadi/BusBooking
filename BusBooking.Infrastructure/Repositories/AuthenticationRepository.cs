using BusBooking.Core.Dtos;
using BusBooking.Core.Dtos.Auth;
using BusBooking.Core.Dtos.User;
using BusBooking.Core.Entites;
using BusBooking.Core.Interfaces;
using BusBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore; 
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

        public LoginResponseDto Authenticate(LoginRequestDto loginRequest)
        {
            var user = _context.Users
                .Include(u => u.Role) 
                .SingleOrDefault(u => u.UserName == loginRequest.UserName && u.Password == loginRequest.Password);

            if (user == null)
                return null;

            var token = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            UpdateRefreshToken(user.UserId, refreshToken, DateTime.UtcNow.AddDays(7));

            // Return the login response
            return new LoginResponseDto
            {
                UserId=user.UserId,
                Token = token,
                UserName = user.UserName,
                RefreshToken = refreshToken
            };
        }

        public void Register(RegisterRequestDto registerRequest)
        {
            var user = new User
            {
                UserId = registerRequest.UserId,

                UserName = registerRequest.UserName,
                EmailId = registerRequest.EmailId,
                FullName = registerRequest.FullName,
                RoleId = registerRequest.RoleId, 
                CreatedDate = DateTime.UtcNow,
                Password = registerRequest.Password,
                ProjectName = "BusBooking",
                RefreshToken = null,
                RefreshTokenExpiryTime = null,
                ContactNo = registerRequest.ContactNo 
            };
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateRefreshToken(int userId, string refreshToken, DateTime expiryTime)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
                return;

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = expiryTime;
            _context.SaveChanges();
        }

        public UserDto GetUserByRefreshToken(string refreshToken)
        {
            var user = _context.Users
                .Include(u => u.Role) 
                .FirstOrDefault(u => u.RefreshToken == refreshToken);

            if (user == null)
                return null;
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
        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);
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
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}