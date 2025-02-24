using InvoiceSystem.Application.Dto;
using InvoiceSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/company")]
public class CompanyController : ControllerBase
{
    private readonly CompanyService _companyService;

    public CompanyController(CompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpPost(Name = "CreateCompany")]
    public async Task<IActionResult> CreateCompany([FromBody] CompanyDto companyDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var company = await _companyService.CreateCompanyAsync(companyDto);
        return Ok(company);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompanyById(string id)
    {
        var company = await _companyService.GetCompanyByIdAsync(id);
        if (company == null)
        {
            return NotFound();
        }
        return Ok(company);
    }

    [HttpGet(Name = "GetCompanies")]
    public async Task<IActionResult> GetAllCompanies([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var companies = await _companyService.GetAllCompaniesAsync(page, pageSize);
        return Ok(companies);
    }
}