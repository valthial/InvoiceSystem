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

    public async Task AddInvoiceAsync(Invoice invoice)
    {
        await _context.Invoices.AddAsync(invoice);
        await _context.SaveChangesAsync();
    }

    public async Task<Invoice?> GetInvoiceByIdAsync(string invoiceId)
    {
        return await _context.Invoices
            .Include(i => i.Company)
            .Include(i => i.CounterPartyCompany)
            .FirstOrDefaultAsync(i => i.Id == invoiceId);
    }

    public async Task<List<Invoice>> GetSentInvoicesAsync(
        string companyId,
        string? counterPartyCompanyId,
        DateTimeOffset? dateIssued,
        string? invoiceId)
    {
        var query = _context.Invoices
            .Include(i => i.Company)
            .Include(i => i.CounterPartyCompany)
            .Where(i => i.CompanyId == companyId);

        if (!string.IsNullOrEmpty(counterPartyCompanyId))
        {
            query = query.Where(i => i.CounterPartyCompanyId == counterPartyCompanyId);
        }

        if (dateIssued.HasValue)
        {
            query = query.Where(i => i.DateIssued == dateIssued.Value);
        }

        if (!string.IsNullOrEmpty(invoiceId))
        {
            query = query.Where(i => i.Id == invoiceId);
        }

        return await query.ToListAsync();
    }

    public async Task<List<Invoice>> GetReceivedInvoicesAsync(
        string companyId,
        string? counterPartyCompanyId,
        DateTimeOffset? dateIssued,
        string? invoiceId)
    {
        var query = _context.Invoices
            .Include(i => i.Company)
            .Include(i => i.CounterPartyCompany)
            .Where(i => i.CounterPartyCompanyId == companyId);

        if (!string.IsNullOrEmpty(counterPartyCompanyId))
        {
            query = query.Where(i => i.CompanyId == counterPartyCompanyId);
        }

        if (dateIssued.HasValue)
        {
            query = query.Where(i => i.DateIssued == dateIssued.Value);
        }

        if (!string.IsNullOrEmpty(invoiceId))
        {
            query = query.Where(i => i.Id == invoiceId);
        }

        return await query.ToListAsync();
    }
}