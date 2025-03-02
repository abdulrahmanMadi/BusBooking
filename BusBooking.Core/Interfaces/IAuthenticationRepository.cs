using BusBooking.Core.Dtos.Auth;
using BusBooking.Core.Dtos.User;


namespace BusBooking.Core.Interfaces
{
    public interface IAuthenticationRepository
    {
        LoginResponseDto Authenticate(LoginRequestDto loginRequest);
        void Register(RegisterRequestDto registerRequest);
        void UpdateRefreshToken(int userId, string refreshToken, DateTime expiryTime);
        UserDto GetUserByRefreshToken(string refreshToken);
    }


}
