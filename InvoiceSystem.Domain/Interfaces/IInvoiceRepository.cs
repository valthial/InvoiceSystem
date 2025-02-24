using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Domain.Interfaces;

public interface IInvoiceRepository
{
    Task CreateInvoiceAsync(Invoice invoice);
    Task<Invoice?> GetInvoiceByIdAsync(string invoiceId);
    Task<List<Invoice>> GetSentInvoicesAsync(string companyId, string? counterPartyCompanyId = null, DateTimeOffset? dateIssued = null, string? invoiceId = null);
    Task<List<Invoice>> GetReceivedInvoicesAsync(string companyId, string? counterPartyCompanyId = null, DateTimeOffset? dateIssued = null, string? invoiceId = null);
    Task<bool> InvoiceExistsAsync(string invoiceId);
}