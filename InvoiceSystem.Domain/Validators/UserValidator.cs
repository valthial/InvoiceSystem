using FluentValidation;
using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Domain.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Email)
            .NotNull().WithMessage("Email cannot be null.")
            .NotEmpty().WithMessage("Email cannot be empty.")
            .Must(email => !string.IsNullOrWhiteSpace(email)).WithMessage("Email cannot be whitespace.");

        RuleFor(x => x.PasswordHash)
            .NotNull().WithMessage("Password cannot be null.")
            .NotEmpty().WithMessage("Password cannot be empty.")
            .Must(password => !string.IsNullOrWhiteSpace(password)).WithMessage("Password cannot be whitespace.");
        
        RuleFor(x => x.IssuerCompanyId)
            .NotNull().WithMessage("Company cannot be empty");
    }
}