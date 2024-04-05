using Microsoft.Extensions.DependencyInjection;
using Pacagroup.Ecommerce.Application.Interface;
using Pacagroup.Ecommerce.Application.Main;
using Pacagroup.Ecommerce.Domain.Core;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Infrastructure.Data;
using Pacagroup.Ecommerce.Infrastructure.Interface;
using Pacagroup.Ecommerce.Infrastructure.Repository;
using Pacagroup.Ecommerce.Transversal.Common;
using Pacagroup.Ecommerce.Transversal.Logging;

namespace Pacagroup.Ecommerce.Services.WebApi.Modules.Injection
{
    /// <summary>
    /// Clase de extension para limpiar el codigo de la clase startup.cs
    /// </summary>
    public static class InjectionExtension
    {
        /// <summary>
        /// Metodo de extension para agregar las inyecciones de dependencia de los proyectos de la solución
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddInjection(this IServiceCollection services)
        {
            //LoggerText.writeLog("antes de GetSection(\"ConfigJWT\")");

            services.AddSingleton<DapperContext>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            //LoggerText.writeLog("despues de typeof(LoggerAdapter<>)");

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomerDomain, CustomerDomain>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerApplication, CustomerApplication>();
            services.AddScoped<IUserDomain, UserDomain>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserApplication, UserApplication>();
            services.AddScoped<ICategoriesDomain, CategoriesDomain>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<ICategoriesApplication, CategoriesApplication>();

            return services;
        }
    }
}