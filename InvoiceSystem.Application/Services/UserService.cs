using FluentValidation;
using InvoiceSystem.Application.Validators;
using InvoiceSystem.Domain.Entities;
using InvoiceSystem.Domain.Interfaces;
using InvoiceSystem.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace InvoiceSystem.Application.Services;

public class UserService(IUserRepository userRepository, ILogger<UserService> logger)
    : IUserService
{
    public async Task<User> CreateUserAsync(User user)
    {
        var validator = new UserValidator();
        var validationResult = await validator.ValidateAsync(user);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        if (await userRepository.UserExistsAsync(user.Email))
        {
            throw new InvalidOperationException($"A user with the email '{user.Email}' already exists.");
        }
        
        await userRepository.CreateUserAsync(user);

        logger.LogInformation("User created with ID: {UserId}", user.Id);
        return user;
    }
    public async Task<User?> GetUserByIdAsync(string email)
    {
        return await userRepository.GetUserByEmailAsync(email);
    }

    public async Task<List<User>> GetAllUsersAsync(int page, int pageSize)
    {
        return await userRepository.GetAllUsersAsync(page, pageSize);
    }

    public async Task<(string UserId, int CompanyId)?> ValidateUserCredentialsAsync(string email, string password)
    {
        var user = await userRepository.GetUserByEmailAsync(email);
        if (user is null || !VerifyPassword(password, user.PasswordHash)) return null;
        
        return (user.Id.ToString(), user.IssuerCompanyId);
    }
    
    private static bool VerifyPassword(string password, string? passwordHash) => BCrypt.Net.BCrypt.Verify(password, passwordHash);
}