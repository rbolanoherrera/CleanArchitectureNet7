using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pacagroup.Ecommerce.Application.UseCases;
using Pacagroup.Ecommerce.Persistence;
using Pacagroup.Ecommerce.Persistence.Contexts;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Authentication;
using Pacagroup.Ecommerce.Services.WebApi.Modules.HealthCheck;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Injection;
using Pacagroup.Ecommerce.Services.WebApi.Modules.RateLimiter;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Redis;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Swagger;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Versioning;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Watch;
using Pacagroup.Ecommerce.Transversal.Logging;
using Pacagroup.Ecommerce.Transversal.Mapper.Base;
using System;
using WatchDog;

var builder = WebApplication.CreateBuilder(args);

string myCORSPolicy = "policyAPIDDD";

LoggerText.HabilitarLogTxt = Convert.ToBoolean(builder.Configuration["HabilitarLogTxt"]);
LoggerText.PathFile = System.Environment.CurrentDirectory + @"\logs";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddCors(options => options.AddPolicy(myCORSPolicy,
    policy => policy.WithOrigins(builder.Configuration["OriginsCORS"])
    .AllowAnyHeader()
    .AllowAnyMethod()
));

//services.AddJsonOptions(options =>
//                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase
//);

//builder.Services.AddLogging(options =>
//{
//    options.AddConsole();
//});

builder.Services.AddInjection();

//LoggerText.writeLog("antes de GetValue<string>(\"Secret\")");

builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddVersioning();
builder.Services.AddSwagger();
builder.Services.AddRedisCache(builder.Configuration);
builder.Services.AddHealthChecks();
//builder.Services.AddMySqlServerHealthCheck(serviceProvider => MySqlServerDependencyInjection.GetConnectionString(builder.Configuration, serviceProvider, "NorthwindConnection"));
builder.Services.AddHealthChecks(builder.Configuration);
//builder.Services.AddWatchDogLog(builder.Configuration);//logging and Dashboard
builder.Services.AddRateLimiting(builder.Configuration);

builder.Services.AddMvc();

var app = builder.Build();

//configure the Http request pipeline

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(sw =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        foreach (var descripcion in provider.ApiVersionDescriptions)
        {
            sw.SwaggerEndpoint($"/swagger/{descripcion.GroupName}/swagger.json",
                descripcion.GroupName.ToUpperInvariant());
        }
    });
}


//app.UseWatchDogExceptionLogger();//para capturar las exepciones que se generen en la aplicacion

app.UseHttpsRedirection();

app.UseCors(myCORSPolicy);

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseRateLimiter();

//#pragma warning disable ASP0014 // Suggest using top level route registrations
//app.UseEndpoints(e => { });
//#pragma warning restore ASP0014 // Suggest using top level route registrations
app.MapControllers();

app.UseHealthChecks("/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseHealthChecksUI(config =>
{
    config.UIPath = "/health-ui";
});

//para configurar el dashboard del Logging de WatchDog
//app.UseWatchDog(conf =>
//{
//    conf.WatchPageUsername = builder.Configuration["WatchDog:WatchPageUsername"];
//    conf.WatchPagePassword = builder.Configuration["WatchDog:WatchPagePassword"];
//});

app.Run();

/// <summary>
/// esto se agrega para poder usar la clase program en el proyecto de Test
/// </summary>
public partial class Program { };
