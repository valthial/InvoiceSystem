using InvoiceSystem.Application.Common.Interfaces.Authentication;
using InvoiceSystem.Application.Common.Interfaces.Services;
using InvoiceSystem.Infrastructure.Authentication;
using InvoiceSystem.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using InvoiceSystem.Application.Common.Interfaces.Repositories;
using InvoiceSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using InvoiceSystem.Application.Services.Commands;


namespace InvoiceSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration, string connectionString)
    {

        services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<UserService>();

        return services;
    }
}