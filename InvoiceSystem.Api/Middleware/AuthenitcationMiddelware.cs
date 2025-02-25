using InvoiceSystem.Api.Models;
using Microsoft.Extensions.Options;

namespace InvoiceSystem.Api.Middleware;

public class AuthenticationMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
{
    private readonly string _apiToken = appSettings.Value.ApiToken;

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("Authorization", out var token))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        if (token != _apiToken)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        await next(context);
    }
}