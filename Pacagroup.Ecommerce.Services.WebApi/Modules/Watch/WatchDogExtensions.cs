using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WatchDog;
using WatchDog.src.Enums;

namespace Pacagroup.Ecommerce.Services.WebApi.Modules.Watch
{
    /// <summary>
    /// Extension de la Libreria  Watch Dog para Logging de aplicaciones con Dashboard de vista y busqueda
    /// </summary>
    public static class WatchDogExtensions
    {
        /// <summary>
        /// Metodo para agregar al contenedor de dependencias
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddWatchDogLog(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddWatchDogServices(opt =>
            {
                opt.SetExternalDbConnString = configuration.GetConnectionString("WatchDogLoggerConnection");
                opt.DbDriverOption = WatchDog.src.Enums.WatchDogDbDriverEnum.MSSQL;
                opt.IsAutoClear = true;
                opt.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Monthly;
            });

            return services;
        }
    }
}