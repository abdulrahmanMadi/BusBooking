using BusBooking.Core.Dtos;
using BusBooking.Core.Entites;
using BusBooking.Core.Interfaces;
using BusBooking.Infrastructure.Data;


namespace BusBooking.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public UserDto Authenticate(LoginRequestDto loginRequest)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == loginRequest.UserName && u.Password == loginRequest.Password);
            if (user == null)
                return null;

            return new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                EmailId = user.EmailId,
                FullName = user.FullName,
                Role = user.Role,
                CreatedDate = user.CreatedDate,
                ProjectName = user.ProjectName
            };
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            return _context.Users.Select(u => new UserDto
            {
                UserId = u.UserId,
                UserName = u.UserName,
                EmailId = u.EmailId,
                FullName = u.FullName,
                Role = u.Role,
                CreatedDate = u.CreatedDate,
                ProjectName = u.ProjectName
            }).ToList();
        }

        public void Register(RegisterRequestDto registerRequest)
        {
            var user = new User
            {
                UserName = registerRequest.UserName,
                EmailId = registerRequest.EmailId,
                FullName = registerRequest.FullName,
                Password = registerRequest.Password,
                Role = registerRequest.Role,
                CreatedDate = DateTime.UtcNow,
                ProjectName = "BusBooking",
                RefreshToken = null,
                RefreshTokenExpiryTime = null
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void AddNewUser(AddNewUserDto addNewUserDto)
        {
            var user = new User
            {
                UserId = addNewUserDto.UserId,
                UserName = addNewUserDto.UserName,
                EmailId = addNewUserDto.EmailId,
                FullName = addNewUserDto.FullName,
                Role = addNewUserDto.Role,
                CreatedDate = addNewUserDto.CreatedDate,
                Password = addNewUserDto.Password,
                ProjectName = addNewUserDto.ProjectName,
                RefreshToken = addNewUserDto.RefreshToken,
                RefreshTokenExpiryTime = addNewUserDto.RefreshTokenExpiryTime
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
