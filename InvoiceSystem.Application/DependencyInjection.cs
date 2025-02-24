using InvoiceSystem.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<UserService>();
        services.AddScoped<CompanyService>();
        services.AddScoped<InvoiceService>();
        return services;
    }
}