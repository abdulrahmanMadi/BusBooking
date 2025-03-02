using BusBooking.Core.Dtos;
using BusBooking.Core.Dtos.Auth;
using BusBooking.Core.Dtos.User;
using BusBooking.Core.Dtos.Vendor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Core.Interfaces
{
    public interface IUserRepository
    {
        // General user methods
        UserDto Authenticate(LoginRequestDto loginRequest);
        void Register(RegisterRequestDto registerRequest);
        IEnumerable<UserDto> GetAllUsers();
        UserDto GetUserById(int userId);
        void UpdateUser(int userId, UpdateUserDto updateUserDto);
        void DeleteUser(int userId);

        // Vendor-specific methods
        IEnumerable<VendorDto> GetAllVendors();
        VendorDto GetVendorById(int userId);
        void CreateVendor(CreateVendorDto createVendorDto);
        void UpdateVendor(int userId, UpdateVendorDto updateVendorDto);
        void DeleteVendor(int userId);

        // Refresh token methods
        void UpdateRefreshToken(int userId, string refreshToken, DateTime expiryTime);
        UserDto GetUserByRefreshToken(string refreshToken);
        IEnumerable<RoleDto> GetAllRoles();

    }
}