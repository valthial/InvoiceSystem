using FluentValidation;
using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Application.Validators
{
    public class InvoiceValidator : AbstractValidator<Invoice>
    {
        public InvoiceValidator()
        {
            RuleFor(x => x.IssuerCompanyId)
                .NotNull().WithMessage("Issuer company ID is required.")
                .NotEmpty().WithMessage("Issuer company ID cannot be empty or whitespace.");

            RuleFor(x => x.CounterPartyCompanyId)
                .NotNull().WithMessage("Counter-party company ID is required.")
                .NotEmpty().WithMessage("Counter-party company ID cannot be empty or whitespace.");

            RuleFor(x => x.DateIssued)
                .NotEqual(DateTimeOffset.MinValue).WithMessage("Date issued is required.")
                .LessThanOrEqualTo(DateTimeOffset.UtcNow).WithMessage("Date issued cannot be in the future.");

            RuleFor(x => x.NetAmount).GreaterThanOrEqualTo(0).WithMessage("Net amount cannot be negative.");
            RuleFor(x => x.VatAmount).GreaterThanOrEqualTo(0).WithMessage("VAT amount cannot be negative.");
            RuleFor(x => x.TotalAmount).GreaterThanOrEqualTo(0).WithMessage("Total amount cannot be negative.");

            RuleFor(x => x.Description)
                .NotNull().WithMessage("Description is required.")
                .NotEmpty().WithMessage("Description cannot be empty or whitespace.");
        }
    }
}