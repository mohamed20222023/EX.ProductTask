using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
namespace Infrastructure.Services.Middlewares;
public class BearerAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    public BearerAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }
     public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            if (ex is SecurityTokenExpiredException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Token expired.");
            }
            else if (ex is SecurityTokenInvalidSignatureException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Invalid token signature.");
            }
            else if (ex is SecurityTokenInvalidIssuerException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Invalid token issuer.");
            }
            else if (ex is SecurityTokenInvalidAudienceException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Invalid token audience.");
            }
            else if (ex is SecurityTokenException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Invalid token.");
            }
            else
            {
                throw;
            }
        }
    }
}