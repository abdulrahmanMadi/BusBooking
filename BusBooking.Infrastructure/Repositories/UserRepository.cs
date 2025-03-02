
using BusBooking.Core.Dtos.Auth;
using BusBooking.Core.Dtos.User;
using BusBooking.Core.Dtos.Vendor;
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

        // General user methods
        public UserDto Authenticate(LoginRequestDto loginRequest)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == loginRequest.UserName && u.Password == loginRequest.Password);
            if (user == null)
                return null;

            return MapToUserDto(user);
        }

        public void Register(RegisterRequestDto registerRequest)
        {
            var user = new User
            {
                UserName = registerRequest.UserName,
                EmailId = registerRequest.EmailId,
                FullName = registerRequest.FullName,
                RoleId = registerRequest.RoleId,
                CreatedDate = DateTime.UtcNow,
                Password = registerRequest.Password,
                ProjectName = "BusBooking",
                RefreshToken = null,
                RefreshTokenExpiryTime = null,
                ContactNo = registerRequest.ContactNo // Additional field for vendors
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            return _context.Users.Select(u => MapToUserDto(u)).ToList();
        }

        public UserDto GetUserById(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            return user == null ? null : MapToUserDto(user);
        }

        public void UpdateUser(int userId, UpdateUserDto updateUserDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
                return;

            user.UserName = updateUserDto.UserName;
            user.EmailId = updateUserDto.EmailId;
            user.FullName = updateUserDto.FullName;
            user.RoleId = updateUserDto.RoleId;

            _context.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
                return;

            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        // Vendor-specific methods
        public IEnumerable<VendorDto> GetAllVendors()
        {
            return _context.Users
                .Where(u => u.Role.RoleName == "Vendor")
                .Select(u => new VendorDto
                {
                    VendorId = u.UserId,
                    VendorName = u.FullName,
                    ContactNo = u.ContactNo,
                    EmailId = u.EmailId
                }).ToList();
        }

        public VendorDto GetVendorById(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId && u.Role.RoleName == "Vendor");
            return user == null ? null : new VendorDto
            {
                VendorId = user.UserId,
                VendorName = user.FullName,
                ContactNo = user.ContactNo,
                EmailId = user.EmailId
            };
        }

        public void CreateVendor(CreateVendorDto createVendorDto)
        {
            var vendorRole = _context.Roles.FirstOrDefault(r => r.RoleName == "Vendor");
            if (vendorRole == null)
                return;

            var user = new User
            {
                UserName = createVendorDto.VendorName,
                EmailId = createVendorDto.EmailId,
                FullName = createVendorDto.VendorName,
                RoleId = vendorRole.RoleId,
                CreatedDate = DateTime.UtcNow,
                Password = createVendorDto.Password,
                ProjectName = "BusBooking",
                RefreshToken = null,
                RefreshTokenExpiryTime = null,
                ContactNo = createVendorDto.ContactNo
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateVendor(int userId, UpdateVendorDto updateVendorDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId && u.Role.RoleName == "Vendor");
            if (user == null)
                return;

            user.FullName = updateVendorDto.VendorName;
            user.ContactNo = updateVendorDto.ContactNo;
            user.EmailId = updateVendorDto.EmailId;

            _context.SaveChanges();
        }

        public void DeleteVendor(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId && u.Role.RoleName == "Vendor");
            if (user == null)
                return;

            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        // Refresh token methods
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
            var user = _context.Users.FirstOrDefault(u => u.RefreshToken == refreshToken);
            return user == null ? null : MapToUserDto(user);
        }

        // Helper method to map User to UserDto
        private UserDto MapToUserDto(User user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                EmailId = user.EmailId,
                FullName = user.FullName,
                Role = user.Role.RoleName,
                CreatedDate = user.CreatedDate,
                ProjectName = user.ProjectName
            };
        }
        public IEnumerable<RoleDto> GetAllRoles()
        {
            return _context.Roles.Select(r => new RoleDto
            {
                RoleId = r.RoleId,
                RoleName = r.RoleName
            }).ToList();
        }
    }
}