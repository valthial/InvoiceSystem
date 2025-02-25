using InvoiceSystem.Domain.Entities;
using InvoiceSystem.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using InvoiceSystem.Application.Validators;
using InvoiceSystem.Domain.Interfaces.Services;

namespace InvoiceSystem.Application.Services;

public class CompanyService(ICompanyRepository companyRepository, ILogger<CompanyService> logger)
    : ICompanyService
{
    public async Task<Company> CreateCompanyAsync(Company company)
    {
        var validator = new CompanyValidator();
        var validationResult = await validator.ValidateAsync(company);
    
        if(!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors.ToString());

        if (await companyRepository.CompanyExistsAsync(company.Name)) 
            throw new InvalidOperationException($"A company with the name '{company.Name}' already exists.");
        
        
        await companyRepository.CreateCompanyAsync(company);
        logger.LogInformation("IssuerCompany created with ID: {IssuerCompanyId}", company.Id);
        return company;
    }

    public async Task<Company?> GetCompanyByIdAsync(int id)
    {
        return await companyRepository.GetCompanyByIdAsync(id);
    }

    public async Task<List<Company>> GetAllCompaniesAsync(int page, int pageSize)
    {
        return await companyRepository.GetAllCompaniesAsync(page, pageSize);
    }
}