using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Domain.Interfaces;

public interface ICompanyRepository
{
    Task CreateCompanyAsync(Company company);
    Task<Company?> GetCompanyByIdAsync(string id);
    Task<List<Company>> GetAllCompaniesAsync(int page, int pageSize);
    Task<bool> CompanyExistsAsync(string id);
}