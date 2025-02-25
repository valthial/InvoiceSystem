using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Domain.Interfaces.Services;

public interface IUserService
{
    Task<User> CreateUserAsync(User user);
    Task<User?> GetUserByIdAsync(string email);
    Task<List<User>> GetAllUsersAsync(int page, int pageSize);
    Task<(string UserId, int CompanyId)?> ValidateUserCredentialsAsync(string email, string password);
}