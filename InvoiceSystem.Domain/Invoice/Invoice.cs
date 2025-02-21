namespace InvoiceSystem.Domain.Invoice;

public sealed class Invoice
{
    private Invoice() { }
    
    public string Id { get; private set; }
    public DateTimeOffset DateIssued { get; private set; }
    public decimal NetAmount { get;private set; }
    public decimal VatAmount { get;private set; }
    public decimal TotalAmount { get;private set; }
    public string Description { get;private set; }
    public string CompanyId { get;private set; }
    public string CounterPartyCompanyId { get; private set; }


    public static Invoice Create(DateTimeOffset dateIssued, string companyId, string counterPartyCompanyId, decimal netAmount, decimal vatAmount, decimal totalAmount, string description)
    {
        return new Invoice()
        {
            DateIssued = dateIssued,
            CompanyId = companyId,
            CounterPartyCompanyId = counterPartyCompanyId,
            NetAmount = netAmount,
            VatAmount = vatAmount,
            TotalAmount = totalAmount,
            Description = description
        };
    }
}