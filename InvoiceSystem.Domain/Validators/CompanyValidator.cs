using FluentValidation;
using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Domain.Validators;

public class CompanyValidator :  AbstractValidator<Company>
{
    public CompanyValidator()
    {
        RuleFor(x => x.Name).NotNull().WithMessage("IssuerCompany name cannot be null or empty.");
    }
}