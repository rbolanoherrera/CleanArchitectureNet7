using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Pacagroup.Ecommerce.Infrastructure.Data
{
    public static class MySqlServerDependencyInjection
    {
        public static IServiceCollection AddMySqlServerHealthCheck(this IServiceCollection serviceCollection,
            Func<IServiceProvider, Task<string>> connectionString)
        {
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            string sqlServerConnectionString = connectionString.Invoke(serviceProvider).Result;
            serviceCollection.AddHealthChecks().AddSqlServer(sqlServerConnectionString, tags: new[] { "database" });

            return serviceCollection;
        }

        public static async Task<string> GetConnectionString(IConfiguration configuration, IServiceProvider serviceProvider, 
            string connectionName)
        {
            string connection = string.Empty;
            string? co = configuration.GetConnectionString(connectionName);

            if(!string.IsNullOrEmpty(co))
                connection = co;

            return await Task.FromResult(connection);
        }

    }
}