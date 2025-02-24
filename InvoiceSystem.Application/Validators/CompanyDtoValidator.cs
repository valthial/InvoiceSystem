using FluentValidation;
using InvoiceSystem.Application.Dto;

namespace InvoiceSystem.Application.Validators;

public class CompanyDtoValidator :  AbstractValidator<CompanyDto>
{
    public CompanyDtoValidator()
    {
        RuleFor(x => x.Name).NotNull().WithMessage("IssuerCompany name cannot be null or empty.");
    }
}