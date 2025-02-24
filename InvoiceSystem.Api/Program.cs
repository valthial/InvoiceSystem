using InvoiceSystem.Application;
using InvoiceSystem.Application.Services;
using InvoiceSystem.Domain.Interfaces;
using InvoiceSystem.Infrastructure.Repositories;
using InvoiceSystem.Infrastructure.Repositories.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1", 
        new OpenApiInfo
        {
            Title = "Invoice System API", 
            Version = "v1",
            Description = "API for managing companies and invoices.",
            Contact = new OpenApiContact
            {
                Name = "Valentina Koronaiou",
                Email = "valthial@gmail.com"
            }
        });
});

builder.Services.AddAuthentication("HardcodedAuth")
    .AddScheme<AuthenticationSchemeOptions, AuthHandler>("HardcodedAuth", null);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<Microsoft.AspNetCore.HttpsPolicy.HttpsRedirectionOptions>(options =>
    options.HttpsPort = 5000);

builder.Services.AddApplication();
builder.Services.AddControllers();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CompanyService>();
builder.Services.AddScoped<InvoiceService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Invoice System API");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthentication();
app.MapControllers();
app.Run();