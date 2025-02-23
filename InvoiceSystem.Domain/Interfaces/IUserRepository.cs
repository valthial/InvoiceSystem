using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);
}