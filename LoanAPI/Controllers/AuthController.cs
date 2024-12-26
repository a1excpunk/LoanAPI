using LoanAPI.Services.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LoanAPI.Services.Interfaces;
using LoanAPI.Helpers;
using FluentValidation;
using LoanAPI.Models;

namespace LoanAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtHelper _jwtHelper;
        private readonly IValidator<Accountant> _accountantValidator;
        private readonly IValidator<User> _userValidator;

        public AuthController(IUserService userService, JwtHelper jwtHelper, IValidator<Accountant> accountantValidator, IValidator<User> userValidator)
        {
            _userService = userService;
            _jwtHelper = jwtHelper;
            _accountantValidator = accountantValidator;
            _userValidator = userValidator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (dto.Role == Role.Accountant)
            {
                var validationResult = await _accountantValidator.ValidateAsync((IValidationContext)dto);
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);
            }
            else if (dto.Role == Role.RegularUser)
            {
                var validationResult = await _userValidator.ValidateAsync((IValidationContext)dto);
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);
            }

            var result = await _userService.RegisterUserAsync(dto);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.AuthenticateUserAsync(dto);
            if (!result.Success)
                return Unauthorized(result.Message);

            var token = _jwtHelper.GenerateToken(result.User, result.Role.ToString());
            return Ok(new { Token = token });
        }
    }
}