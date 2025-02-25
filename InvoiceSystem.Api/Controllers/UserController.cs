using AutoMapper;
using InvoiceSystem.Application.Dto;
using InvoiceSystem.Application.DTO;
using Microsoft.AspNetCore.Mvc;
using InvoiceSystem.Domain.Entities;
using InvoiceSystem.Domain.Interfaces.Services;

namespace InvoiceSystem.Api.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UserController(IUserService userService, IMapper mapper) : ControllerBase
{
    [HttpPost(Name = "CreateUser")]
    public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var user = mapper.Map<User>(userDto);
        
        await userService.CreateUserAsync(user);
        return Ok(user);
    }

    [HttpGet("{email}")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var user = await userService.GetUserByIdAsync(email);

        if (user is null) return NotFound();
        
        return Ok(user);
    }

    [HttpGet(Name = "GetUsers")]
    public async Task<IActionResult> GetAllUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var users = await userService.GetAllUsersAsync(page, pageSize);
        return Ok(users);
    }
    
    [HttpPost("validate")]
    public async Task<IActionResult> ValidateCredentials([FromBody] ValidateCredentialsDto credentials)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = await userService.ValidateUserCredentialsAsync(credentials.Email, credentials.Password);

        if (result is null)
        {
            return Unauthorized();
        }

        return Ok((result.Value.UserId,result.Value.CompanyId));
    }
}