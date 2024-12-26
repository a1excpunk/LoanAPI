using LoanAPI.Services.DTOs;

namespace LoanAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserServiceResult> RegisterUserAsync(RegisterDto dto);
        Task<UserServiceResult> AuthenticateUserAsync(LoginDto dto);
        Task<bool> BlockUser(int userId);
    }

}
