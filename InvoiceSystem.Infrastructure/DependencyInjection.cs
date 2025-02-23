using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using InvoiceSystem.Infrastructure.Repositories;


namespace InvoiceSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration, string connectionString)
    {

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));
        
        return services;
    }
}