using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.IO;
using System.Reflection;

namespace Pacagroup.Ecommerce.Services.WebApi.Modules.Swagger
{
    /// <summary>
    /// Metodo de extension de swagger para quitar todo ese codigo del archivo startup.cs
    /// </summary>
    public static class SwaggerExtension
    {
        /// <summary>
        /// Agregar la configuracion personalizada de Swagger al proyecto
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            //services.AddSwaggerGen();//tambien funciona agregando solo esta línea
            services.AddSwaggerGen(sw =>
            {
                //LoggerText.writeLog("antes de GetName().Name}.xml");

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(Directory.GetCurrentDirectory(), xmlFile);
                sw.IncludeXmlComments(xmlPath);

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference()
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                //Agergar seguridad a swagger para los metodos protegidos con Authorize
                sw.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

                sw.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        securityScheme, new string[]{}
                    }
                });

            });

            return services;
        }
    }
}