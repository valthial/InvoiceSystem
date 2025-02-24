using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<List<User>> GetAllUsersAsync(int page, int pageSize);
    Task CreateUserAsync(User user);
    Task<bool> UserExistsAsync(string email);
}