namespace InvoiceSystem.Domain.Interfaces.Authentication;

public interface ITokenService
{
    string GenerateJwtToken(string userId, string companyId);
}