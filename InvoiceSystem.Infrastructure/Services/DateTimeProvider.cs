using InvoiceSystem.Application.Common.Interfaces.Services;

namespace InvoiceSystem.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}