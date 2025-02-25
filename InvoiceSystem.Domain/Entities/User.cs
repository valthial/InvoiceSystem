using FluentValidation;
using InvoiceSystem.Domain.Validators;

namespace InvoiceSystem.Domain.Entities
{
    public sealed class User
    {
        public int Id { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public int IssuerCompanyId { get; private set; }

        public static User Create(string email, string passwordHash, int companyId)
        {
            var user = new User
            {
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(passwordHash),
                IssuerCompanyId = companyId
            };

            var validator = new UserValidator();
            var validationResult = validator.Validate(user);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            return user;
        }
    }
}