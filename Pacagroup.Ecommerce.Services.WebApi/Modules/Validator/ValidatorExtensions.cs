using Microsoft.Extensions.DependencyInjection;
using Pacagroup.Ecommerce.Application.Validator;

namespace Pacagroup.Ecommerce.Services.WebApi.Modules.Validator
{
    /// <summary>
    /// Aplicando validaciones con Fluent validation
    /// </summary>
    public static class ValidatorExtensions
    {
        /// <summary>
        /// Agregar validadores personalizados al contenedor de dependencias
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddValidator(this IServiceCollection services)
        {
            services.AddTransient<UserDtoValidator>();

            return services;
        }
    }
}