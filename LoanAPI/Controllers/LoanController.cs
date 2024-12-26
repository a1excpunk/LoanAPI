using FluentValidation;
using LoanAPI.Interfaces;
using LoanAPI.Models;
using LoanAPI.Models.Enums;
using LoanAPI.Services.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LoanAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/loan")]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;
        private readonly IValidator<LoanDto> _loanValidator;
        public LoanController(ILoanService loanService, IValidator<LoanDto> loanValidator)
        {
            _loanService = loanService;
            _loanValidator = loanValidator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLoan([FromBody] LoanDto dto)
        {
            var loanValidationResult = _loanValidator.Validate(dto);
            if (!loanValidationResult.IsValid)
            {
                foreach (var error in loanValidationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("User not authenticated.");

            var userId = int.Parse(userIdClaim.Value);
            var result = await _loanService.CreateLoanAsync(dto, userId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [Authorize(Roles = Role.Accountant)]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllLoans()
        {
            var loans = await _loanService.GetAllLoansAsync();
            return Ok(loans);
        }

        [HttpGet("{loanId}")]
        public async Task<IActionResult> GetLoanById(int loanId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var roleClaim = User.FindFirst(ClaimTypes.Role);

            if (userIdClaim == null || roleClaim == null)
                return Unauthorized("User or role information missing.");

            var userId = int.Parse(userIdClaim.Value);
            var userRole = roleClaim.Value;

            if (userRole == Role.Accountant || userRole == Role.RegularUser)
            {
                var loan = await _loanService.GetLoanByIdAsync(loanId, userId);
                if (loan == null)
                    return NotFound("Loan not found or access denied.");

                return Ok(loan);
            }

            return Forbid();
        }

        [HttpPut("{loanId}")]
        public async Task<IActionResult> UpdateLoan(int loanId, [FromBody] LoanDto dto)
        {
            var loanValidationResult = _loanValidator.Validate(dto);
            if (!loanValidationResult.IsValid)
            {
                foreach (var error in loanValidationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("User not authenticated.");

            var userId = int.Parse(userIdClaim.Value);
            var result = await _loanService.UpdateLoanAsync(loanId, dto, userId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [Authorize(Roles = Role.Accountant)]
        [HttpPatch("{loanId}/status")]
        public async Task<IActionResult> ChangeLoanStatus(int loanId, [FromBody] LoanStatus status)
        {
            var result = await _loanService.ChangeLoanStatusAsync(loanId, status);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpDelete("{loanId}")]
        public async Task<IActionResult> DeleteLoan(int loanId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("User not authenticated.");

            var userId = int.Parse(userIdClaim.Value);
            var result = await _loanService.DeleteLoanAsync(loanId, userId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}
