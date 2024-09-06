using Microsoft.Extensions.DependencyInjection;

namespace Pacagroup.Ecommerce.Transversal.Mapper.Base
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddBuilders(this IServiceCollection services)
        {
            services.AddScoped<CustomerBuilder>();
            services.AddScoped<UserBuilder>();
            services.AddScoped<CategoriesBuilder>();
            services.AddScoped<CategoriesForListBuilder>();
            services.AddScoped<DiscountBuilder>();

            return services;
        }
    }
}