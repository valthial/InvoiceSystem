using InvoiceSystem.Application;
using InvoiceSystem.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var connectionStr = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddApplication();
builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration, connectionStr);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthentication();
app.MapControllers();
app.Run();

