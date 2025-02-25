using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Application.Dto;

public class InvoiceDto
{
    public string Id { get; set; }
    public DateTimeOffset DateIssued { get; set; }
    public decimal NetAmount { get; set; }
    public decimal VatAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string Description { get; set; }
    public int IssuerCompanyId { get; private set; }
    public Company IssuerCompany { get; private set; }
    public int CounterPartyCompanyId { get; private set; }
    public Company CounterPartyCompany { get; private set; }
}