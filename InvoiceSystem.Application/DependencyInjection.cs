using InvoiceSystem.Application.Mapper;
using InvoiceSystem.Application.Services;
using InvoiceSystem.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(CompanyProfile));
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IInvoiceService, InvoiceService>();
        return services;
    }
}