using System.Text.Json.Serialization;

namespace InvoiceSystem.Domain.Entities
{
    public sealed class User
    {
        [JsonConstructor]
        private User() { }

        public Guid Id { get; private set; }
        public string Email { get; private set; } = null!;
        public string PasswordHash { get; private set; } = null!;

        public static User Create(string email, string password)
        {
            return new User
            {
                Email = email,
                PasswordHash = HashPassword(password)
            };
        }

        private static string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public bool VerifyPassword(string password)
        {
            return HashPassword(password) == PasswordHash;
        }
    }
}
