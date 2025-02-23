using InvoiceSystem.Application;
using InvoiceSystem.Domain.Interfaces;
using InvoiceSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddControllers();
// builder.Services.AddInfrastructure(builder.Configuration, connectionStr);
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthentication();
app.MapControllers();
app.Run();