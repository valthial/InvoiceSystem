using InvoiceSystem.Application.Dto;
using InvoiceSystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceSystem.Api.Controllers;

[ApiController]
[Route("api/invoice")]
public class InvoiceController : ControllerBase
{
    private readonly CreateInvoice _createInvoice;

    public InvoiceController(CreateInvoice createInvoice)
    {
        _createInvoice = createInvoice;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateInvoice(InvoiceDTO invoiceDto)
    {
        var invoice = Invoice.Create( 
            invoiceDto.DateIssued, 
            invoiceDto.CounterPartyCompany, 
            invoiceDto.CounterPartyCompanyId, 
            invoiceDto.NetAmount,
            invoiceDto.VatAmount,
            invoiceDto.TotalAmount, 
            invoiceDto.Description);
        
        await _createInvoice.Execute(invoice);
        return Ok(invoice);
    }
}