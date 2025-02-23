using InvoiceSystem.Domain.Entities;
using InvoiceSystem.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystem.Infrastructure.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly List<Company> _companies = new();

    public Task<Company?> GetCompanyByIdAsync(string id)
    {
        var company = _companies.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(company);
    }

    public Task<List<Company>> GetAllCompaniesAsync()
    {
        return Task.FromResult(_companies);
    }

    public Task AddCompanyAsync(Company company)
    {
        _companies.Add(company);
        return Task.CompletedTask;
    }
}