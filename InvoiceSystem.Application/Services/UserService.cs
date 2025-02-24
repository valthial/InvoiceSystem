using InvoiceSystem.Application.Dto;
using InvoiceSystem.Domain.Entities;
using InvoiceSystem.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        if (string.IsNullOrWhiteSpace(userDto.Email))
        {
            throw new ArgumentException("Email cannot be null or whitespace.", nameof(userDto.Email));
        }

        if (string.IsNullOrWhiteSpace(userDto.Password))
        {
            throw new ArgumentException("Password cannot be null or whitespace.", nameof(userDto.Password));
        }

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