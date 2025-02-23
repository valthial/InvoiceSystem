using InvoiceSystem.Domain.Entities;
using InvoiceSystem.Domain.Interfaces;

public class CreateInvoice
{
    private readonly IInvoiceRepository _invoiceRepository;

    public CreateInvoice(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task Execute(Invoice invoice)
    {
        await _invoiceRepository.AddAsync(invoice);
    }
}