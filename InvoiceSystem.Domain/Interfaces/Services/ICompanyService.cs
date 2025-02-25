using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Domain.Interfaces.Services;

public interface ICompanyService
{
    Task<Company> CreateCompanyAsync(Company company);
    Task<Company?> GetCompanyByIdAsync(int id);
    Task<List<Company>> GetAllCompaniesAsync(int page, int pageSize);
}