using AutoMapper;
using InvoiceSystem.Application.Dto;
using InvoiceSystem.Domain.Entities;
using InvoiceSystem.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceSystem.Api.Controllers;

[ApiController]
[Route("api/companies")]
[Attributes.Authorize]
public class CompanyController(ICompanyService companyService, IMapper mapper) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateCompany([FromBody] CompanyDto companyDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var company = mapper.Map<Company>(companyDto);
        
        var createdCompany = await companyService.CreateCompanyAsync(company);
        return Ok(createdCompany);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompanyById(int id)
    {
        var company = await companyService.GetCompanyByIdAsync(id);
        if (company is null)
        {
            return NotFound();
        }
        return Ok(company);
    }

    [HttpGet("getAllCompanies")]
    public async Task<IActionResult> GetAllCompanies([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var companies = await companyService.GetAllCompaniesAsync(page, pageSize);
        return Ok(companies);
    }
}