namespace InvoiceSystem.Domain.Entities;

public sealed class Company
{
    private Company() { }

    public string Id { get; private set; }
    public string Name { get; private set; }
    public List<Invoice> Invoices { get; set; }

    public static Company Create(string name)
    {
        return new Company()
        {
            Name = name
        };
    }
}