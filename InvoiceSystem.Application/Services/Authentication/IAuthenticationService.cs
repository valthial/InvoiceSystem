namespace InvoiceSystem.Application.Services.Authentication;

public interface IAuthenticationService
{
    AuthenticationResult Register(string email, string password);
    AuthenticationResult Login(string email, string password);
}