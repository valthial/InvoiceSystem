using FluentValidation;
using InvoiceSystem.Domain.Validators;

namespace InvoiceSystem.Domain.Entities;


public sealed class Company
{
    private Company() { }

    public string Id { get; private set; }
    public string Name { get; private set; }
    public IEnumerable<User>? Users { get; private set; }

    public static Company Create(string name, IEnumerable<User>? users)
    {
        var company = new Company()
        {
            Name = name,
            Users = users
        };
        
        var validator = new CompanyValidator();
        var validationResult = validator.Validate(company);
        
        if(!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        return company;
    }
}