using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Domain.Interfaces.Services;

public interface IInvoiceService
{
    Task<Invoice> CreateInvoiceAsync(Invoice invoice);
    Task<Invoice?> GetInvoiceByIdAsync(int invoiceId);
    Task<IEnumerable<Invoice>> GetAllInvoicesAsync();
}