using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Domain.Interfaces;

public interface ICompanyRepository
{
    Task<Company?> GetCompanyByIdAsync(string id);
    Task<List<Company>> GetAllCompaniesAsync();
    Task AddCompanyAsync(Company company);
}