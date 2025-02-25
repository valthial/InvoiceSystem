using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Domain.Interfaces;

public interface ICompanyRepository
{
    Task CreateCompanyAsync(Company company);
    Task<Company?> GetCompanyByIdAsync(int id);
    Task<List<Company>> GetAllCompaniesAsync(int page, int pageSize);
    Task<bool> CompanyExistsAsync(string name);
}