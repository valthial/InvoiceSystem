using InvoiceSystem.Domain.Entities;
using InvoiceSystem.Domain.Interfaces;

namespace InvoiceSystem.Infrastructure.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly List<Invoice> _invoices = new List<Invoice>();

    public Task AddAsync(Invoice invoice)
    {
        _invoices.Add(invoice);
        return Task.CompletedTask;
    }

    public Task<Invoice?> GetByIdAsync(string invoiceId)
    {
        return Task.FromResult(_invoices.FirstOrDefault(i => i.Id == invoiceId));
    }
}