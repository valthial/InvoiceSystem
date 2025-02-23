using InvoiceSystem.Application.Dto;
using InvoiceSystem.Application.Services;
using InvoiceSystem.Domain.Entities;
using InvoiceSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceSystem.Api.Controllers;

[ApiController]
[Route("api/invoice")]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceRepository _invoiceRepository;

    public InvoiceController(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    [HttpGet("sent")]
    [Authorize]
    public async Task<IActionResult> GetSentInvoices(
        [FromQuery] string? counterPartyCompanyId,
        [FromQuery] DateTimeOffset? dateIssued,
        [FromQuery] string? invoiceId)
    {
        var companyId = User.Claims.FirstOrDefault(c => c.Type == "CompanyId")?.Value;

        if (string.IsNullOrEmpty(companyId))
        {
            return Unauthorized("Company ID is missing in the token.");
        }

        var invoices = await _invoiceRepository.GetSentInvoicesAsync(companyId, counterPartyCompanyId, dateIssued, invoiceId);
        return Ok(invoices);
    }

    [HttpGet("received")]
    [Authorize]
    public async Task<IActionResult> GetReceivedInvoices(
        [FromQuery] string? counterPartyCompanyId,
        [FromQuery] DateTimeOffset? dateIssued,
        [FromQuery] string? invoiceId)
    {
        var companyId = User.Claims.FirstOrDefault(c => c.Type == "CompanyId")?.Value;

        if (string.IsNullOrEmpty(companyId))
        {
            return Unauthorized("Company ID is missing in the token.");
        }

        var invoices = await _invoiceRepository.GetReceivedInvoicesAsync(companyId, counterPartyCompanyId, dateIssued, invoiceId);
        return Ok(invoices);
    }
}