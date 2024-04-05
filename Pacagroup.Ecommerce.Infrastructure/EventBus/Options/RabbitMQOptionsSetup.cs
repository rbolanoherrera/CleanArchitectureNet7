using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Pacagroup.Ecommerce.Infrastructure.EventBus.Options
{
    public class RabbitMQOptionsSetup : IConfigureOptions<RabbitMQOptions>
    {
        private readonly IConfiguration configuration;
        private const string ConfigSectionName = "RabbitMQOptions";

        public RabbitMQOptionsSetup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Configure(RabbitMQOptions options)
        {
            configuration.GetSection(ConfigSectionName).Bind(options);
        }
    }
}