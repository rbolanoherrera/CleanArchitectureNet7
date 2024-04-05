using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Pacagroup.Ecommerce.Services.WebApi.Helpers;
using System.Text;
using System;
using System.Threading.Tasks;

namespace Pacagroup.Ecommerce.Services.WebApi.Modules.Authentication
{
    /// <summary>
    /// Metodo de exyensión para quitar toda esta configuración del archivo startup.cs
    /// </summary>
    public static class AuthenticationExtension
    {
        /// <summary>
        /// Metodo de extensión para agregar la configuración de Authenticación al proyecto
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var settingsJWT = configuration.GetSection("ConfigJWT");
            services.Configure<AppSettings>(settingsJWT);

            //LoggerText.writeLog("despues de settingsJWT");

            services.AddSingleton<IConfiguration>(configuration);

            byte[] key = Encoding.ASCII.GetBytes(settingsJWT.GetValue<string>("Secret"));
            string issuer = settingsJWT.GetValue<string>("Issuer");
            string audience = settingsJWT.GetValue<string>("Audience");

            //LoggerText.writeLog("despues de GetValue<string>(\"Audience\")");

            services.AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        int userId = int.Parse(context.Principal.Identity.Name);
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            context.Response.Headers.Add("Token-Expired", "true");

                        return Task.CompletedTask;
                    }
                };

                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }
    }
}