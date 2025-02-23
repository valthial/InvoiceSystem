using InvoiceSystem.Application.Interfaces;
using InvoiceSystem.Domain.Interfaces;

namespace InvoiceSystem.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<(string UserId, string CompanyId)?> ValidateUserCredentialsAsync(string email, string password)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);

        if (user == null || !VerifyPassword(password, user.PasswordHash))
        {
            return null; 
        }

        return (user.Id, user.CompanyId.Id);
    }

    private bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}