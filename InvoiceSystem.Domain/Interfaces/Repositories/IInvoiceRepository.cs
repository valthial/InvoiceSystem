using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Domain.Interfaces;

public interface IInvoiceRepository
{
    Task CreateInvoiceAsync(Invoice invoice);
    Task<Invoice?> GetInvoiceByIdAsync(int invoiceId);
    Task<List<Invoice>> GetSentInvoicesAsync(int companyId, int? counterPartyCompanyId = null, DateTimeOffset? dateIssued = null, int? invoiceId = null);
    Task<List<Invoice>> GetReceivedInvoicesAsync(int companyId, int? counterPartyCompanyId = null, DateTimeOffset? dateIssued = null, int? invoiceId = null);
    Task<bool> InvoiceExistsAsync(int invoiceId);
}