using Microsoft.Extensions.DependencyInjection;
using Pacagroup.Ecommerce.Application.Interface.UseCases;
using Pacagroup.Ecommerce.Application.UseCases.Discounts;
using Pacagroup.Ecommerce.Application.Validator;
using Pacagroup.Ecommerce.Transversal.Mapper.Base;

namespace Pacagroup.Ecommerce.Application.UseCases
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddBuilders();//del proyecto Mapper para no usar la libreria AutoMapper
            services.AddScoped<ICustomerApplication, CustomerApplication>();
            services.AddScoped<IUserApplication, UserApplication>();
            services.AddScoped<ICategoriesApplication, CategoriesApplication>();
            services.AddScoped<IDiscountsApplication, DiscountApplication>();

            services.AddTransient<UserDtoValidator>();
            services.AddTransient<DiscountDtoValidator>();

            return services;
        }
    }
} 