using InvoiceSystem.Application.Dto;
using InvoiceSystem.Domain.Entities;
using InvoiceSystem.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace InvoiceSystem.Application.Services;

public class InvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly ILogger<InvoiceService> _logger;

    public InvoiceService(IInvoiceRepository invoiceRepository, ILogger<InvoiceService> logger)
    {
        _invoiceRepository = invoiceRepository;
        _logger = logger;
    }
    public async Task<Invoice> CreateInvoiceAsync(InvoiceDto invoiceDto)
    {
        if (invoiceDto == null)
        {
            throw new ArgumentNullException(nameof(invoiceDto), "Invoice data cannot be null.");
        }

        // Validate required fields
        if (string.IsNullOrWhiteSpace(invoiceDto.IssuerCompanyId))
        {
            throw new ArgumentException("Issuer company ID is required.", nameof(invoiceDto.IssuerCompanyId));
        }

        if (string.IsNullOrWhiteSpace(invoiceDto.CounterPartyCompanyId))
        {
            throw new ArgumentException("Counter-party company ID is required.", nameof(invoiceDto.CounterPartyCompanyId));
        }

        if (invoiceDto.DateIssued == default)
        {
            throw new ArgumentException("Date issued is required.", nameof(invoiceDto.DateIssued));
        }

        if (await _invoiceRepository.InvoiceExistsAsync(invoiceDto.Id))
        {
            throw new InvalidOperationException($"An invoice with the ID '{invoiceDto.Id}' already exists.");
        }
        
        var invoice = Invoice.Create(
            invoiceDto.DateIssued,
            invoiceDto.IssuerCompanyId,
            invoiceDto.CounterPartyCompanyId,
            invoiceDto.NetAmount,
            invoiceDto.VatAmount,
            invoiceDto.TotalAmount,
            invoiceDto.Description);

        await _invoiceRepository.CreateInvoiceAsync(invoice);

        _logger.LogInformation("Invoice created with ID: {InvoiceId}", invoice.Id);
        return invoice;
    }

    public async Task<Invoice?> GetInvoiceByIdAsync(string invoiceId)
    {
        if (string.IsNullOrWhiteSpace(invoiceId))
        {
            throw new ArgumentException("Invoice ID cannot be null or whitespace.", nameof(invoiceId));
        }

        return await _invoiceRepository.GetInvoiceByIdAsync(invoiceId);
    }

    public async Task<List<Invoice>> GetSentInvoicesAsync(string companyId, string? counterPartyCompanyId = null, DateTimeOffset? dateIssued = null, string? invoiceId = null)
    {
        if (string.IsNullOrWhiteSpace(companyId))
        {
            throw new ArgumentException("IssuerCompany ID cannot be null or whitespace.", nameof(companyId));
        }

        return await _invoiceRepository.GetSentInvoicesAsync(companyId, counterPartyCompanyId, dateIssued, invoiceId);
    }
    
    public async Task<List<Invoice>> GetReceivedInvoicesAsync(string companyId, string? counterPartyCompanyId = null, DateTimeOffset? dateIssued = null, string? invoiceId = null)
    {
        if (string.IsNullOrWhiteSpace(companyId))
        {
            throw new ArgumentException("IssuerCompany ID cannot be null or whitespace.", nameof(companyId));
        }

        return await _invoiceRepository.GetReceivedInvoicesAsync(companyId, counterPartyCompanyId, dateIssued, invoiceId);
    }
}