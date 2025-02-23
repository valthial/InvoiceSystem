using FluentValidation;
using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Domain.Validators;

public class CompanyValidator :  AbstractValidator<Company>
{
    public CompanyValidator()
    {
        RuleFor(x => x.Name).Null().WithMessage("Company name cannot be null or empty.");
        RuleFor(x => x.Users).Null().WithMessage("Users list cannot be null.");
    }
}