using InvoiceSystem.Application.Dto;
using InvoiceSystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceSystem.Api.Controllers;

[ApiController]
[Route("api/company")]
public class CompanyController : ControllerBase
{
    private readonly CompanyService _companyService;

    public CompanyController(CompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateCompany(CompanyDto companyDto)
    {
        var company = await _companyService.CreateCompanyAsync(companyDto.Name, companyDto.Users);
        return Ok(company);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetCompanyById(string id)
    {
        var company = await _companyService.GetCompanyByIdAsync(id);
        if (company == null)
        {
            return NotFound();
        }
        return Ok(company);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllCompanies()
    {
        var companies = await _companyService.GetAllCompaniesAsync();
        return Ok(companies);
    }
}