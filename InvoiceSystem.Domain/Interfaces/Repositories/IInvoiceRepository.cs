using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Domain.Interfaces;

public interface IInvoiceRepository
{
    Task CreateInvoiceAsync(Invoice invoice);
    Task<Invoice?> GetInvoiceByIdAsync(int invoiceId);
    Task<bool> InvoiceExistsAsync(int invoiceId);
    Task<IEnumerable<Invoice>> GetAllInvoicesAsync();
}