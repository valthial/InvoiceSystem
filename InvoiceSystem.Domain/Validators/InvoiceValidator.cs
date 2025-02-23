using FluentValidation;
using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Domain.Validators
{
    public class InvoiceValidator : AbstractValidator<Invoice>
    {
        public InvoiceValidator()
        {
            RuleFor(x => x.NetAmount).GreaterThanOrEqualTo(0).WithMessage("Net amount cannot be negative.");
            RuleFor(x => x.VatAmount).GreaterThanOrEqualTo(0).WithMessage("VAT amount cannot be negative.");
            RuleFor(x => x.TotalAmount).GreaterThanOrEqualTo(0).WithMessage("Total amount cannot be negative.");
        }
    }
}