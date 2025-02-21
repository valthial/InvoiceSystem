using InvoiceSystem.Application.Common.Interfaces.Repositories;
using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Application.Services.Commands;


public class UserService(IUserRepository userRepository)
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<User> RegisterUserAsync(string email, string password)
    {
        var user = User.Create(password, email);

        await _userRepository.AddAsync(user);

        return user;
    }
}