using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Application.Dto;

public class UserDto
{
    public string Email { get; set; }
    public string? Password { get; set; }
    public Company Company { get; set; }
    public string? CompanyId { get; set; }
}