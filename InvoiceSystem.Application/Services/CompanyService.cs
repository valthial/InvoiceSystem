using InvoiceSystem.Domain.Entities;
using InvoiceSystem.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using InvoiceSystem.Application.Dto;
using InvoiceSystem.Application.Validators;

namespace InvoiceSystem.Application.Services;

public class CompanyService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly ILogger<CompanyService> _logger;

    public CompanyService(ICompanyRepository companyRepository, ILogger<CompanyService> logger)
    {
        _companyRepository = companyRepository;
        _logger = logger;
    }

    public async Task<Company> CreateCompanyAsync(CompanyDto companyDto)
    {
        var validator = new CompanyDtoValidator();
        var validationResult = await validator.ValidateAsync(companyDto);
    
        if(!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors.ToString());

        if (await _companyRepository.CompanyExistsAsync(companyDto.Id))
        {
            throw new InvalidOperationException($"A company with the name '{companyDto.Id}' already exists.");
        }

        var company = Company.Create(companyDto.Name);
        await _companyRepository.CreateCompanyAsync(company);
        _logger.LogInformation("Company created with ID: {CompanyId}", company.Id);
        return company;
    }

    public async Task<Company?> GetCompanyByIdAsync(string id)
    {
        return await _companyRepository.GetCompanyByIdAsync(id);
    }

    public async Task<List<Company>> GetAllCompaniesAsync(int page, int pageSize)
    {
        return await _companyRepository.GetAllCompaniesAsync(page, pageSize);
    }
}