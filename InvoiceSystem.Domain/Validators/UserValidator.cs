using FluentValidation;
using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Domain.Validators;

public class UserValidator: AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Email).Null().WithMessage("User email cannot be null or empty.");
        RuleFor(x => x.CompanyId).Null().WithMessage("User companyId cannot be null or empty.");
        RuleFor(x => x.PasswordHash).Null().WithMessage("User password cannot be null or empty.");
    }
}