using FluentValidation;
using InvoiceSystem.Application.Dto;
using InvoiceSystem.Domain.Entities;
using InvoiceSystem.Domain.Interfaces;
using InvoiceSystem.Domain.Validators;
using Microsoft.Extensions.Logging;

namespace InvoiceSystem.Application.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository userRepository, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<User> CreateUserAsync(UserDto userDto)
    {
        var validator = new UserDtoValidator();
        var validationResult = validator.Validate(userDto);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        if (await _userRepository.UserExistsAsync(userDto.Email))
        {
            throw new InvalidOperationException($"A user with the email '{userDto.Email}' already exists.");
        }
        
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
        var user = User.Create(userDto.Email, passwordHash, userDto.Company, userDto.CompanyId);
        await _userRepository.CreateUserAsync(user);

        _logger.LogInformation("User created with ID: {UserId}", user.Id);
        return user;
    }
    public async Task<User?> GetUserByIdAsync(string email)
    {
        return await _userRepository.GetUserByEmailAsync(email);
    }

    public async Task<List<User>> GetAllUsersAsync(int page, int pageSize)
    {
        return await _userRepository.GetAllUsersAsync(page, pageSize);
    }

    public async Task<(string UserId, string CompanyId)?> ValidateUserCredentialsAsync(string email, string password)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);

        if (user == null || !VerifyPassword(password, user.PasswordHash))
        {
            return null;
        }

        return (user.Id, user.Company.Id);
    }
    
    private bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}