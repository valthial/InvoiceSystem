using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Application.Dto;

public class InvoiceDto
{
    public DateTimeOffset DateIssued { get; set; }
    public decimal NetAmount { get; set; }
    public decimal VatAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string Description { get; set; }
    public int IssuerCompanyId { get; set; }
    public int CounterPartyCompanyId { get; set; }
}