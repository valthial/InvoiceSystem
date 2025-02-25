using FluentValidation;
using InvoiceSystem.Domain.Validators;

namespace InvoiceSystem.Domain.Entities;

public sealed class Invoice
{
    private Invoice() { }

    public int Id { get; private set; }
    public DateTimeOffset DateIssued { get; private set; }
    public decimal NetAmount { get; private set; }
    public decimal VatAmount { get; private set; }
    public decimal TotalAmount { get; private set; }
    public string Description { get; private set; }
    public int IssuerCompanyId { get; private set; }
    public int CounterPartyCompanyId { get; private set; }
    public Company IssuerCompany { get; private set; }
    public Company CounterPartyCompany { get; private set; }

    public static Invoice Create(DateTimeOffset dateIssued, int issuerCompanyId, int counterPartyCompanyId, decimal netAmount, decimal vatAmount, decimal totalAmount, string description)
    {
        var invoice = new Invoice()
        {
            DateIssued = dateIssued,
            IssuerCompanyId = issuerCompanyId,
            CounterPartyCompanyId = counterPartyCompanyId,
            NetAmount = netAmount,
            VatAmount = vatAmount,
            TotalAmount = totalAmount,
            Description = description
        };

        var validator = new InvoiceValidator();
        var validationResult = validator.Validate(invoice);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return invoice;
    }
}