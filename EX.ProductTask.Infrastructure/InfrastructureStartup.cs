using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repository;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace Infrastructure;
public static class InfrastructureStartup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
     Action<DbContextOptionsBuilder> options,
       string ConfigurationToken)
    {
        #region  DbContext
        services.AddDbContext<AppDbContext>(options);
         #endregion
        #region common services

        services.AddHttpContextAccessor();
         services.AddScoped(typeof(IRepositoryApp<>), typeof(RepositoryApp<>));
        

         #endregion

        #region  log
        #endregion
        #region  Localization
        services.AddLocalization();
        services.AddDistributedMemoryCache();

        #endregion


		return services;
    }
}
