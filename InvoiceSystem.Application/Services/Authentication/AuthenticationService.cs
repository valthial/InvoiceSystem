using InvoiceSystem.Application.Common.Interfaces.Authentication;
using InvoiceSystem.Application.Common.Interfaces.Repositories;
using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResult Register(string email, string password)
    {
        if (_userRepository.GetByEmailAsync(email) is not null)
        {
            throw new Exception("User with given email already exists");
        }
        
        var user = User.Create(email, password);
        _userRepository.AddAsync(user);

        var token = _jwtTokenGenerator.GenerateToken(user.Id);

        return new AuthenticationResult(
            user.Id,
            email,
            token);
    }

    public AuthenticationResult Login(string email, string password)
    {
        return new AuthenticationResult(
            Guid.NewGuid(),
            email,
            "token");
    }
}