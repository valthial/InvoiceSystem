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
            .Include(i => i.IssuerCompany)
            .Include(i => i.CounterPartyCompany)
            .FirstOrDefaultAsync(i => i.Id == invoiceId);
    }

    public async Task<List<Invoice>> GetSentInvoicesAsync(int companyId, int? counterPartyCompanyId = null, DateTimeOffset? dateIssued = null, int? invoiceId = null)
    {
        var query = _context.Invoices
            .Include(i => i.IssuerCompany)
            .Include(i => i.CounterPartyCompany)
            .Where(i => i.IssuerCompanyId == companyId);
        
        if (counterPartyCompanyId != null)
        {
            query = query.Where(i => i.CounterPartyCompanyId == counterPartyCompanyId);
        }

        if (dateIssued.HasValue)
        {
            query = query.Where(i => i.DateIssued == dateIssued.Value);
        }

        if (invoiceId != null)
        {
            query = query.Where(i => i.Id == invoiceId);
        }

        return await query.ToListAsync();
    }

    public async Task<List<Invoice>> GetReceivedInvoicesAsync(int companyId, int? counterPartyCompanyId = null, DateTimeOffset? dateIssued = null, int? invoiceId = null)
    {
        var query = _context.Invoices
            .Include(i => i.IssuerCompany)
            .Include(i => i.CounterPartyCompany)
            .Where(i => i.CounterPartyCompanyId == companyId);

        if (counterPartyCompanyId != null)
        {
            query = query.Where(i => i.IssuerCompanyId == counterPartyCompanyId);
        }

        if (dateIssued.HasValue)
        {
            query = query.Where(i => i.DateIssued == dateIssued.Value);
        }

        if (invoiceId != null)
        {
            query = query.Where(i => i.Id == invoiceId);
        }

        return await query.ToListAsync();
    }

    public async Task<bool> InvoiceExistsAsync(int invoiceId)
    {
        return await _context.Invoices.AnyAsync(i => i.Id == invoiceId);
    }
}