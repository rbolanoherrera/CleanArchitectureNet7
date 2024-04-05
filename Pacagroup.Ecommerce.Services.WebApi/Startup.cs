using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pacagroup.Ecommerce.Infrastructure.Data;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Authentication;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Injection;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Redis;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Swagger;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Validator;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Versioning;
using Pacagroup.Ecommerce.Transversal.Logging;
using Pacagroup.Ecommerce.Transversal.Mapper.Base;
using System;

namespace Pacagroup.Ecommerce.Services.WebApi
{
    /// <summary>
    /// Clase de arranque de la App
    /// </summary>
    public class Startup
    {
        readonly string myCORSPolicy = "policyAPIDDD";
        public IConfiguration Configuration { get; }


        /// <summary>
        /// Contructor de arranque de la app
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        
            LoggerText.HabilitarLogTxt = Convert.ToBoolean(Configuration["HabilitarLogTxt"]);
            LoggerText.PathFile = System.Environment.CurrentDirectory + @"\logs";
        }


        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            //services.AddHealthChecks().AddCheck<SqlServerHealthCheck>(nameof(SqlServerHealthCheck));
            services.AddMySqlServerHealthCheck(serviceProvider => MySqlServerDependencyInjection.GetConnectionString(Configuration, serviceProvider, "NorthwindConnection"));
            services.AddHealthChecksUI().AddInMemoryStorage();

            services.AddControllers();
            services.AddBuilders();//del proyecto Mapper

            services.AddCors(options => options.AddPolicy(myCORSPolicy, 
                builder => builder.WithOrigins(Configuration["OriginsCORS"])
                .AllowAnyHeader()
                .AllowAnyMethod()
            ));

            services.AddMvc();

            //services.AddJsonOptions(options =>
            //                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            //);

            services.AddLogging(options =>
            {
                options.AddConsole();
            });

            services.AddInjection();

            //LoggerText.writeLog("antes de GetValue<string>(\"Secret\")");

            services.AddAuthentication(Configuration);
            services.AddVersioning();
            services.AddSwagger();
            services.AddValidator();
            services.AddRedisCache(Configuration);

        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(sw =>
                {
                    foreach (var descripcion in provider.ApiVersionDescriptions)
                    {
                        sw.SwaggerEndpoint($"/swagger/{descripcion.GroupName}/swagger.json",
                            descripcion.GroupName.ToUpperInvariant());
                    }
                });
            }

            //app.MapHealthChecks("/health");
            //app.UseHealthChecks("/health");
            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI(config =>
            {
                config.UIPath = "/health-ui";
            });

            //app.UseCors(c =>
            //{
            //    c.AllowAnyOrigin();
            //    c.AllowAnyHeader();
            //    c.AllowAnyMethod();
            //});

            app.UseCors(myCORSPolicy);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
