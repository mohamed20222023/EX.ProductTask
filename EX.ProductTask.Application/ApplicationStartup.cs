using Application.IBusiness.Common;
using Application.Business.Common;
using Application.Services;
using Core.Interfaces.Common;
using Microsoft.Extensions.DependencyInjection;
using Application.Business.Products;
using Application.IBusiness.Products;
using Infrastructure.Services.Common;
namespace Application;
public static class ApplicationStartup
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		#region  common
		services.AddAutomapperConfigServices();
		services.AddSingleton(typeof(IClockService), typeof(ClockService));

		services.AddScoped(typeof(IEntitiesBusinessCommon<,,,,>), typeof(EntitiesBusinessCommon<,,,,>));
		services.AddScoped(typeof(IRepositoryMessage), typeof(RepositoryMessageService));
        #endregion


        #region  Basic Data
        services.AddScoped(typeof(IProductBusiness), typeof(ProductBusiness));

		#endregion
		#region  log
		services.AddScoped(typeof(ILogCustom), typeof(LogCustom));
		#endregion


		services.AddHttpClient();

		return services;
	}
}
