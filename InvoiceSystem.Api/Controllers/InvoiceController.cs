using InvoiceSystem.Application.Dto;
using InvoiceSystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceSystem.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/invoice")]
public class InvoiceController : ControllerBase
{
    private readonly InvoiceService _invoiceService;

    public InvoiceController(InvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpPost(Name = "createInvoice")]
    public async Task<IActionResult> CreateInvoice(InvoiceDto invoiceDto)
    {
        var invoice = await _invoiceService.CreateInvoiceAsync(invoiceDto);
        return Ok(invoice);
    }
    
    [HttpGet(Name = "sent")]
    public async Task<IActionResult> GetSentInvoices(
        [FromQuery] string? counterPartyCompanyId,
        [FromQuery] DateTimeOffset? dateIssued,
        [FromQuery] string? invoiceId)
    {
        var companyId = User.Claims.FirstOrDefault(c => c.Type == "IssuerCompanyId")?.Value;

        if (string.IsNullOrEmpty(companyId))
        {
            return Unauthorized("IssuerCompany ID is missing in the token.");
        }

        var invoices = await _invoiceService.GetSentInvoicesAsync(companyId, counterPartyCompanyId, dateIssued, invoiceId);
        return Ok(invoices);
    }

    [HttpGet(Name = "received")]
    public async Task<IActionResult> GetReceivedInvoices(
        [FromQuery] string? counterPartyCompanyId,
        [FromQuery] DateTimeOffset? dateIssued,
        [FromQuery] string? invoiceId)
    {
        var companyId = User.Claims.FirstOrDefault(c => c.Type == "IssuerCompanyId")?.Value;

        if (string.IsNullOrEmpty(companyId))
        {
            return Unauthorized("IssuerCompany ID is missing in the token.");
        }

        var invoices = await _invoiceService.GetReceivedInvoicesAsync(companyId, counterPartyCompanyId, dateIssued, invoiceId);
        return Ok(invoices);
    }
}