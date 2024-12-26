using Microsoft.EntityFrameworkCore;
using LoanAPI.Interfaces;
using LoanAPI.Models;
using LoanAPI.Helpers;
using LoanAPI.Services.DTOs;
using LoanAPI.Migrations;


public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly LoggerService _loggerService;

    public UserService(AppDbContext context, LoggerService loggerService)
    {
        _context = context;
        _loggerService = loggerService;
    }

    public async Task<UserServiceResult> RegisterUserAsync(RegisterDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Username == dto.Username))
            return new UserServiceResult { Success = false, Message = "Username already exists." };

        var hashedPassword = new PasswordHasher().HashPassword(dto.Password);

        var user = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Username = dto.Username,
            Age = dto.Age,
            Email = dto.Email,
            MonthlyIncome = dto.MonthlyIncome,
            PasswordHash = hashedPassword
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new UserServiceResult { Success = true, Message = "User registered successfully." };
    }

    public async Task<UserServiceResult> AuthenticateUserAsync(LoginDto dto)
    {

        var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == dto.Username);
        var verifyPassword = new PasswordHasher().VerifyPassword(dto.Password, user.PasswordHash);
        if (user == null || !verifyPassword)
            return new UserServiceResult { Success = false, Message = "Invalid credentials." };

        var roles = await _context.Users
            .Where(u => u.Id == user.Id)
            .Select(u => u.Role)
            .ToListAsync();

        return new UserServiceResult { Success = true, Message = "Authentication successful.", User = user };
    }

    public async Task<bool> BlockUser(int userId)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return false;

        user.IsBlocked = true;
        await _context.SaveChangesAsync();

        return true;
    }
}
