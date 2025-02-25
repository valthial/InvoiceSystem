using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Domain.Interfaces.Services;

public interface IInvoiceService
{
    Task<Invoice> CreateInvoiceAsync(Invoice invoice);
    Task<Invoice?> GetInvoiceByIdAsync(int invoiceId);
    Task<List<Invoice>> GetSentInvoicesAsync(int companyId, int? counterPartyCompanyId = null,
        DateTimeOffset? dateIssued = null, int? invoiceId = null);
    Task<List<Invoice>> GetReceivedInvoicesAsync(int companyId, int? counterPartyCompanyId = null,
        DateTimeOffset? dateIssued = null, int? invoiceId = null);
}