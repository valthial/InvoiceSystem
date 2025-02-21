namespace InvoiceSystem.Application.Services.Authentication;


public record AuthenticationResult(
    Guid Id,
    string Email,
    string Token);
