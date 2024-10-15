using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Pacagroup.Ecommerce.Services.WebApi.Modules.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="provider"></param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            this.provider = provider;
        }

        /// <summary>
        /// Aquí se recorrerá el objeto provider que tiene todas las versiones de la Api descubierta de la Aplicación
        /// </summary>
        /// <param name="options"></param>
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Version = description.ApiVersion.ToString(),
                Title = "Implementando Arquitectura DDD en .NET",
                Description = "Un ejemplo de como implementar la arquitectura empresarial en NET",
                TermsOfService = new System.Uri("https://github.com/rbolanoherrera/DDD"),
                Contact = new OpenApiContact()
                {
                    Name = "Rafael Bolaños Herrera",
                    Email = "ralfs1@hotmail.com",
                    Url = new System.Uri("https://github.com/rbolanoherrera/DDD")
                },
                License = new OpenApiLicense()
                {
                    Name = "Open Source",
                    Url = new System.Uri("https://github.com/rbolanoherrera/DDD")
                }
            };

            return info;
        }

    }
}