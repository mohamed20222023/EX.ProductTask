using System.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace Infrastructure.Services.Middlewares;
public static class StaticFiles
{
    public static IApplicationBuilder CustomStaticFiles(this IApplicationBuilder app, IConfiguration conf)
    {
        app.UseStaticFiles();
        
        var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), conf.GetSection("AppSettings:PhysicalPath").Value);
        if (!Directory.Exists(imagesPath))
            Directory.CreateDirectory(imagesPath);
        app.UseStaticFiles(
            new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(imagesPath),
                RequestPath = new PathString(conf.GetSection("AppSettings:ServerPath").Value)
            });

        return app;
    }
}
