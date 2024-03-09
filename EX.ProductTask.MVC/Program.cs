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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Data;
using Core.Interfaces.Common;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
   .ReadFrom.Configuration(builder.Configuration)
   .CreateLogger();
try
{

	builder.Services.AddInfrastructure(
		 options => { options.UseSqlServer(builder.Configuration.GetConnectionString("DevelopmentConnection")); },
		  builder.Configuration.GetSection("AppSettings:Token").Value);
	builder.Services.AddDbContext<AppHelperContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("HelperConnection")));
	builder.Services.AddApplication();

	builder.Services.AddCors();
	builder.Services.Configure<MaxTimeToken>(builder.Configuration.GetSection("AppSettings"));
	builder.Services.AddSignalR();

	builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
	{
		options.Password.RequireDigit = true;
		options.Password.RequireLowercase = true;
		options.Password.RequireNonAlphanumeric = true;
		options.Password.RequireUppercase = true;
		options.Password.RequiredLength = 5;
		options.SignIn.RequireConfirmedAccount = false;
	})

.AddEntityFrameworkStores<AppDbContext>()
.AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>(TokenOptions.DefaultProvider);


	builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
		AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
	{
		options.LoginPath = new PathString("/Account/Login");
		options.AccessDeniedPath = new PathString("/Home/NotFound");
	});


	builder.Services.AddControllersWithViews()
		.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
	});

	var app = builder.Build();


	using var scope = app.Services.CreateScope();
	var logCustom = scope.ServiceProvider.GetRequiredService<ILogCustom>();
	logCustom.AppStartClose("appstart", true);
	app.UseDeveloperExceptionPage();
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
	app.MapControllerRoute(
		name: "default",
		pattern: "{controller=Account}/{action=Login}/{id?}");

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
