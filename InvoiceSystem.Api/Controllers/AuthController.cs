using InvoiceSystem.Application.Interfaces;
using InvoiceSystem.Domain.Interfaces;
using InvoiceSystem.Domain.Interfaces.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceSystem.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IUserService _userService;

    public AuthController(ITokenService tokenService, IUserService userService)
    {
        _tokenService = tokenService;
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        // Validate user credentials
        var userInfo = await _userService.ValidateUserCredentialsAsync(request.Email, request.Password);

        if (userInfo == null)
        {
            return Unauthorized("Invalid email or password.");
        }

        var (userId, companyId) = userInfo.Value;

        // Generate JWT token
        var token = _tokenService.GenerateJwtToken(userId, companyId);
        return Ok(new { Token = token });
    }
}