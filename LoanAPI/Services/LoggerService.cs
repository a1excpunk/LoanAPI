using LoanAPI.Migrations;
using LoanAPI.Models;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

public class LoggerService
{
    private readonly AppDbContext _context;

    public LoggerService(AppDbContext context)
    {
        _context = context;
    }

    public async Task LogErrorAsync(string errorMessage, string userId, string userRole, string errorStatus)
    {
        var methodName = new StackFrame(1).GetMethod().Name;

        var errorLog = new ErrorLog
        {
            ErrorMessage = errorMessage,
            UserId = userId,
            UserRole = userRole,
            ErrorStatus = errorStatus,
            MethodName = methodName,
            InsertDate = DateTime.Now
        };

        await _context.ErrorLogs.AddAsync(errorLog);
        await _context.SaveChangesAsync();
    }
}
