using InvoiceSystem.Application.Dto;
using Microsoft.AspNetCore.Mvc;
using InvoiceSystem.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace InvoiceSystem.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost(Name = "CreateUser")]
    public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userService.CreateUserAsync(userDto);
        return Ok(user);
    }

    [HttpGet("{email}")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var user = await _userService.GetUserByIdAsync(email);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpGet(Name = "GetUsers")]
    public async Task<IActionResult> GetAllUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var users = await _userService.GetAllUsersAsync(page, pageSize);
        return Ok(users);
    }
    
    [HttpPost("validate")]
    public async Task<IActionResult> ValidateCredentials([FromBody] ValidateCredentialsDto credentials)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = await _userService.ValidateUserCredentialsAsync(credentials.Email, credentials.Password);

        if (result == null)
        {
            return Unauthorized();
        }

        return Ok(result);
    }
}