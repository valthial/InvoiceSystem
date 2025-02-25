using AutoMapper;
using FluentValidation;
using InvoiceSystem.Application.Dto;
using InvoiceSystem.Domain.Entities;
using InvoiceSystem.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceSystem.Api.Controllers;

[ApiController]
[Route("api/v1/invoices")]
public class InvoiceController(IInvoiceService invoiceService, IMapper mapper) : ControllerBase
{
    [HttpPost(Name = "createInvoice")]
    public async Task<IActionResult> CreateInvoice(InvoiceDto invoiceDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var invoice = mapper.Map<Invoice>(invoiceDto);
        
        try
        {
            var createdInvoice = await invoiceService.CreateInvoiceAsync(invoice);
            return Ok(createdInvoice);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetInvoiceByIdAsync(int id)
    {
        var invoice = await invoiceService.GetInvoiceByIdAsync(id);

        if (invoice is null) return NotFound();
        
        return Ok(invoice);
    }
    
    //TODO: Does this build with swagger?
    [HttpGet("sent", Name = "GetSentInvoices")]
    public async Task<IActionResult> GetSentInvoices(
        [FromQuery] int? counterPartyCompanyId,
        [FromQuery] DateTimeOffset? dateIssued,
        [FromQuery] int? invoiceId)
    {
        var companyId = User.Claims.FirstOrDefault(c => c.Type == "IssuerCompanyId").Value;

        if (string.IsNullOrEmpty(companyId)) return Unauthorized("IssuerCompany ID is missing in the token.");
        int.TryParse(companyId, out int company);
        var invoices = await invoiceService.GetSentInvoicesAsync(company, counterPartyCompanyId, dateIssued, invoiceId);
        return Ok(invoices);
    }

    //TODO: Does this build with swagger?
    [HttpGet(Name = "received")]
    [HttpGet("received", Name = "GetReceivedInvoices")]
    public async Task<IActionResult> GetReceivedInvoices(
        [FromQuery] int? counterPartyCompanyId,
        [FromQuery] DateTimeOffset? dateIssued,
        [FromQuery] int? invoiceId)
    {
        var companyId = User.Claims.FirstOrDefault(c => c.Type == "IssuerCompanyId")?.Value;
        
        if (string.IsNullOrEmpty(companyId)) return Unauthorized("IssuerCompany ID is missing in the token.");

        int.TryParse(companyId, out int company);
        var invoices = await invoiceService.GetReceivedInvoicesAsync(company, counterPartyCompanyId, dateIssued, invoiceId);
        return Ok(invoices);
    }
}