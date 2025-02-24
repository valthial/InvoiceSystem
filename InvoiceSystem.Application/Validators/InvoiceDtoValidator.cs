using FluentValidation;
using InvoiceSystem.Application.Dto;

namespace InvoiceSystem.Application.Validators
{
    public class InvoiceDtoValidator : AbstractValidator<InvoiceDto>
    {
        public InvoiceDtoValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("Invoice data cannot be null.");
            RuleFor(x => x.IssuerCompanyId)
                .NotNull().WithMessage("Issuer company ID is required.")
                .NotEmpty().WithMessage("Issuer company ID cannot be empty or whitespace.");
            
            RuleFor(x => x.CounterPartyCompanyId)
                .NotNull().WithMessage("Counter-party company ID is required.")
                .NotEmpty().WithMessage("Counter-party company ID cannot be empty or whitespace.");
            
            RuleFor(x => x.DateIssued)
                .NotEqual(default(DateTime)).WithMessage("Date issued is required.");
            
            RuleFor(x => x.NetAmount).GreaterThanOrEqualTo(0).WithMessage("Net amount cannot be negative.");
            RuleFor(x => x.VatAmount).GreaterThanOrEqualTo(0).WithMessage("VAT amount cannot be negative.");
            RuleFor(x => x.TotalAmount).GreaterThanOrEqualTo(0).WithMessage("Total amount cannot be negative.");
        }
    }
}