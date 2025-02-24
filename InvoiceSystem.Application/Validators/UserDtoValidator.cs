using FluentValidation;
using InvoiceSystem.Application.Dto;

namespace InvoiceSystem.Domain.Validators;

public class UserDtoValidator : AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotNull().WithMessage("Email cannot be null.")
            .NotEmpty().WithMessage("Email cannot be empty.")
            .Must(email => !string.IsNullOrWhiteSpace(email)).WithMessage("Email cannot be whitespace.");

        RuleFor(x => x.Password)
            .NotNull().WithMessage("Password cannot be null.")
            .NotEmpty().WithMessage("Password cannot be empty.")
            .Must(password => !string.IsNullOrWhiteSpace(password)).WithMessage("Password cannot be whitespace.");

        RuleFor(x => x.CompanyId)
            .NotNull().WithMessage("Company ID cannot be null.")
            .NotEmpty().WithMessage("Company ID cannot be empty.");
    }
}