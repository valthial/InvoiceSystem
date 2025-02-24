using FluentValidation;
using InvoiceSystem.Domain.Validators;

namespace InvoiceSystem.Domain.Entities
{
    public sealed class User
    {
        public string Id { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string? CompanyId { get; private set; }
        public Company? Company { get; private set; }

        public static User Create(string email, string passwordHash, Company company, string companyId)
        {
            var user = new User
            {
                Email = email,
                PasswordHash = passwordHash,
                CompanyId = companyId,
                Company = company
            };

            var validator = new UserValidator();
            var validationResult = validator.Validate(user);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            return user;
        }
    }
}