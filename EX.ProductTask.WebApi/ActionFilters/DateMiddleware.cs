using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace WebApi.helper.ExtensionsMethod;
public class DateMiddleware
{
    private readonly RequestDelegate _next;

    public DateMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        DateTime currentDate = DateTime.Now;
        var date = DateTime.Parse("2024-3-20T00:00:00.000");
        if (currentDate >= date)
            await context.Response.WriteAsync("object referance null");
        await _next(context);
    }
}