using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InvoiceSystem.Domain.Interfaces.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace InvoiceSystem.Infrastructure.Repositories.Authentication;

public class TokenService : ITokenService
{
    private readonly string? _issuer;
    private readonly string? _audience;
    private readonly string? _key;

    public TokenService(string? issuer, string? audience, string? key)
    {
        _issuer = issuer;
        _audience = audience;
        _key = key;
    }

    public string GenerateJwtToken(string userId, string companyId)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim("CompanyId", companyId)
        };

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}