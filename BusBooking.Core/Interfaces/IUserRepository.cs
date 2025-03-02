using BusBooking.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBooking.Core.Interfaces
{
    public interface IUserRepository
    {
        UserDto Authenticate(LoginRequestDto loginRequest);
        IEnumerable<UserDto> GetAllUsers();
        void Register(RegisterRequestDto registerRequest);
        void AddNewUser(AddNewUserDto addNewUserDto); // New method
    }

}
