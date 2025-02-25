using FluentValidation;
using InvoiceSystem.Application.Validators;
using InvoiceSystem.Domain.Entities;
using InvoiceSystem.Domain.Interfaces;
using InvoiceSystem.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace InvoiceSystem.Application.Services;

public class InvoiceService(IInvoiceRepository invoiceRepository, ILogger<InvoiceService> logger)
    : IInvoiceService
{
    public async Task<Invoice> CreateInvoiceAsync(Invoice invoice)
    {
        var validator = new InvoiceValidator();
        var validationResult = await validator.ValidateAsync(invoice);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        if (await invoiceRepository.InvoiceExistsAsync(invoice.Id))
        {
            throw new InvalidOperationException($"An invoice with the ID '{invoice.Id}' already exists.");
        }
        
        await invoiceRepository.CreateInvoiceAsync(invoice);

        logger.LogInformation("Invoice created with ID: {InvoiceId}", invoice.Id);
        return invoice;
    }

    public async Task<Invoice?> GetInvoiceByIdAsync(int invoiceId)
    {
        if (invoiceId == null) throw new ArgumentException("Invoice ID cannot be null or whitespace.", nameof(invoiceId));

        return await invoiceRepository.GetInvoiceByIdAsync(invoiceId);
    }

    public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
    {
        return await invoiceRepository.GetAllInvoicesAsync();
    } 
}