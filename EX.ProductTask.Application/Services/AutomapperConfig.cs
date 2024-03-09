using Application.Automappers;
using Application.AutoMappers;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
namespace Application.Services;
public static class AutomapperConfig
{
    public static void AddAutomapperConfigServices(this IServiceCollection services)
    {
        var mappingConfig = new MapperConfiguration(mc =>
 {
     mc.AddProfile(new ManagementMapperProfiles());
     mc.AddProfile(new ProductMapperProfiles());
 });

        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);
    }
}
