namespace InvoiceSystem.Application.Interfaces;

public interface  IUserService
{
    Task<(string UserId, string CompanyId)?> ValidateUserCredentialsAsync(string email, string password);

}