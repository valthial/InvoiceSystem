using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Application.Dto;

public class CompanyDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<User>? Users { get; set; }
}