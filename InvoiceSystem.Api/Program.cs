using System.Text;
using InvoiceSystem.Application;
using InvoiceSystem.Application.Interfaces;
using InvoiceSystem.Application.Services;
using InvoiceSystem.Domain.Interfaces;
using InvoiceSystem.Domain.Interfaces.Authentication;
using InvoiceSystem.Infrastructure.Repositories;
using InvoiceSystem.Infrastructure.Repositories.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

//TODO Auth
builder.Services.AddScoped<ITokenService, TokenService>(provider =>
    new TokenService(
        builder.Configuration["Jwt:Issuer"],
        builder.Configuration["Jwt:Audience"],
        builder.Configuration["Jwt:Key"]
    )
);
builder.Services.AddAuthorization();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddApplication();
builder.Services.AddControllers();

builder.Services.AddControllers();

builder.Services.AddScoped<CompanyService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthentication();
app.MapControllers();
app.Run();