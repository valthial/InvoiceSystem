using FluentValidation;
using InvoiceSystem.Application.Dto;
using InvoiceSystem.Application.Validators;
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
        var validator = new InvoiceDtoValidator();
        var validationResult = validator.Validate(invoiceDto);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

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