using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pacagroup.Ecommerce.Services.WebApi.Modules.HealthCheck
{
    /// <summary>
    /// Configurar los health check para la aplicación
    /// </summary>
    public static class HealthCheckExtensions
    {
        /// <summary>
        /// Agregar los health checks al contenedor de dependencias
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                //.AddSqlServer(configuration.GetConnectionString("NorthwindConnection"), tags: new[] { "database" })
                .AddRedis(configuration.GetConnectionString("RedisConnection"), tags: new[] { "cache" });

            services.AddHealthChecksUI().AddSqlServerStorage(configuration.GetConnectionString("NorthwindConnection"));

            return services;
        }
    }
}