using AutoMapper;
using FluentValidation;
using InvoiceSystem.Application.Dto;
using InvoiceSystem.Domain.Entities;
using InvoiceSystem.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InvoiceSystem.Api.Controllers;

[ApiController]
[Route("api/invoices")]
[Attributes.Authorize]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;
    private readonly IMapper _mapper;

    public InvoiceController(IInvoiceService invoiceService, IMapper mapper)
    {
        _invoiceService = invoiceService;
        _mapper = mapper;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateInvoice([FromBody] InvoiceDto invoiceDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var invoice = _mapper.Map<Invoice>(invoiceDto);

        try
        {
            var createdInvoice = await _invoiceService.CreateInvoiceAsync(invoice);
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
        var invoice = await _invoiceService.GetInvoiceByIdAsync(id);
        if (invoice is null) return NotFound();
        return Ok(invoice);
    }
    
    [HttpGet("sent")]
    public async Task<IActionResult> GetSentInvoices([FromQuery] int companyId)
    {
        var invoices = await _invoiceService.GetAllInvoicesAsync();
        var sentInvoices = invoices.Where(i => i.IssuerCompanyId == companyId).ToList();
        return Ok(sentInvoices);
    }

    [HttpGet("received")]
    public async Task<IActionResult> GetReceivedInvoices([FromQuery] int companyId)
    {
        var invoices = await _invoiceService.GetAllInvoicesAsync();
        var receivedInvoices = invoices.Where(i => i.CounterPartyCompanyId == companyId).ToList();
        return Ok(receivedInvoices);
    }
}