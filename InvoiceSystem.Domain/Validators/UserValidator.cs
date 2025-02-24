using FluentValidation;
using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Domain.Validators;

public class UserValidator: AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Email).NotNull().WithMessage("User email cannot be null or empty.");
        RuleFor(x => x.PasswordHash).NotNull().WithMessage("User password cannot be null or empty.");
    }
}