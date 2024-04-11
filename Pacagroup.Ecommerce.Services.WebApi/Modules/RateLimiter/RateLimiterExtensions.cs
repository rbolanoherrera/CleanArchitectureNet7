using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Pacagroup.Ecommerce.Services.WebApi.Modules.RateLimiter
{
    /// <summary>
    /// Patrón limite de velocidad o Rate Limiting
    /// Se usa para limitar el número de peticiones que se hace a un api en periodo de tiempo determinado
    /// </summary>
    public static class RateLimiterExtensions
    {
        /// <summary>
        /// Agregar Rate Limiter al contenedor de dependencias
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddRateLimiting(this IServiceCollection services, IConfiguration configuration)
        {
            var fixedWindowPolicy = "fixedWindow";
            services.AddRateLimiter(configOptions =>
            {
                configOptions.AddFixedWindowLimiter(policyName: fixedWindowPolicy,
                    fixedWindow =>
                    {
                        fixedWindow.PermitLimit = configuration.GetValue<int>("RateLimiting:PermitLimit");
                        fixedWindow.Window = TimeSpan.FromSeconds(configuration.GetValue<int>("RateLimiting:Window"));
                        fixedWindow.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
                        fixedWindow.QueueLimit = configuration.GetValue<int>("RateLimiting:QueueLimit");
                    });
                configOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            });

            return services;
        }
    }
}