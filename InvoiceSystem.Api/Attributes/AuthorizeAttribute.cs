using InvoiceSystem.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace InvoiceSystem.Api.Attributes;

public class AuthorizeAttribute() : TypeFilterAttribute(typeof(AuthorizeFilter))
{
    private class AuthorizeFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IOptions<AppSettings>>();
            if (token != appSettings.Value.ApiToken)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}