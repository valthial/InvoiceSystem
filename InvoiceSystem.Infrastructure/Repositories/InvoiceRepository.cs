using InvoiceSystem.Domain.Entities;
using InvoiceSystem.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystem.Infrastructure.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly AppDbContext _context;

    public InvoiceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateInvoiceAsync(Invoice invoice)
    {
        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync();
    }

    public async Task<Invoice?> GetInvoiceByIdAsync(int invoiceId)
    {
        return await _context.Invoices
            .Include(i => i.IssuerCompanyId)
            .Include(i => i.CounterPartyCompanyId)
            .FirstOrDefaultAsync(i => i.Id == invoiceId);
    }

    public async Task<bool> InvoiceExistsAsync(int invoiceId)
    {
        return await _context.Invoices.AnyAsync(i => i.Id == invoiceId);
    }
    
    public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
    {
        return await _context.Set<Invoice>().ToListAsync();
    } 

}