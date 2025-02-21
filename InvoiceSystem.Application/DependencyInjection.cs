using InvoiceSystem.Application.Common.Interfaces.Repositories;
using InvoiceSystem.Application.Services.Authentication;
using InvoiceSystem.Application.Services.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        return services;
    }
}