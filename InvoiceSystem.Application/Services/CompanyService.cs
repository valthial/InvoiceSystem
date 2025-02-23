using InvoiceSystem.Domain.Entities;
using InvoiceSystem.Domain.Interfaces;

namespace InvoiceSystem.Application.Services;

public class CompanyService
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<Company> CreateCompanyAsync(string name, IEnumerable<User>? users)
    {
        var company = Company.Create(name, users);
        await _companyRepository.AddCompanyAsync(company);
        return company;
    }

    public async Task<Company?> GetCompanyByIdAsync(string id)
    {
        return await _companyRepository.GetCompanyByIdAsync(id);
    }

    public async Task<List<Company>> GetAllCompaniesAsync()
    {
        return await _companyRepository.GetAllCompaniesAsync();
    }
}