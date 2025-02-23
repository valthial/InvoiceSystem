using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Domain.Interfaces;

public interface IInvoiceRepository
{
    Task AddInvoiceAsync(Invoice invoice);
    Task<Invoice?> GetInvoiceByIdAsync(string invoiceId);
    Task<List<Invoice>> GetSentInvoicesAsync(string companyId, string? counterPartyCompanyId, DateTimeOffset? dateIssued, string? invoiceId);
    Task<List<Invoice>> GetReceivedInvoicesAsync(string companyId, string? counterPartyCompanyId, DateTimeOffset? dateIssued, string? invoiceId);
}