using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User?> GetByEmailAsync(string email);
}