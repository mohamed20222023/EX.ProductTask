using System.Globalization;
using System.Net;
using Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Core.Dtos.Settings;
using Infrastructure.Helper.ExtentionMethod;
using Serilog;
using Infrastructure.Services.Middlewares;
using Application;
using Core.Exceptions;
using System.Text.Json.Serialization;
using WebApi.helper.ExtensionsMethod;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
   .ReadFrom.Configuration(builder.Configuration)
   .CreateLogger();
try
{

    builder.Services.AddInfrastructure(
         options => { options.UseSqlServer(builder.Configuration.GetConnectionString("DevelopmentConnection")); },
          builder.Configuration.GetSection("AppSettings:Token").Value);
    builder.Services.AddApplication();

    builder.Services.AddControllers();
    builder.Services.AddCors();
    builder.Services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });
    builder.Services.Configure<MaxTimeToken>(builder.Configuration.GetSection("AppSettings"));
    builder.Services.AddSignalR();
    builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
    var app = builder.Build();
    using var scope = app.Services.CreateScope();
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
    app.UseExceptionHandler(BuilderExtensions =>
    {
        BuilderExtensions.Run(async context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var error = context.Features.Get<IExceptionHandlerFeature>();
            if (error != null)
            {
                if (error.Error is ExceptionCommonReponse)
                    context.Response.StatusCode = ((ExceptionCommonReponse)error.Error).StatusCode;
                context.Response.AddApplicationError(error.Error.Message);
                await context.Response.WriteAsync(error.Error.Message);
            }
        });
    });
    var cultures = new List<CultureInfo> {
                           new CultureInfo("en"),
                           new CultureInfo("ar") };

    app.UseRequestLocalization(options =>
    {
        options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("ar");
        options.SupportedCultures = cultures;
        options.SupportedUICultures = cultures;
    });
    app.UseMiddleware<DateMiddleware>();
    app.UseDefaultFiles();
    app.CustomStaticFiles(builder.Configuration);
    app.UseRouting();

    app.UseCors(x => x
   .AllowAnyMethod()
   .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
   .AllowCredentials()); // allow credentials
    app.UseAuthentication();
    app.UseAuthorization();
#pragma warning disable
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Warning(ex, "AppStopUnexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}
