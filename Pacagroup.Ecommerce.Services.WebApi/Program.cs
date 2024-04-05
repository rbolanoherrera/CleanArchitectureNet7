using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
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

var builder = WebApplication.CreateBuilder(args);

string myCORSPolicy = "policyAPIDDD";

LoggerText.HabilitarLogTxt = Convert.ToBoolean(builder.Configuration["HabilitarLogTxt"]);
LoggerText.PathFile = System.Environment.CurrentDirectory + @"\logs";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddHealthChecks();
//services.AddHealthChecks().AddCheck<SqlServerHealthCheck>(nameof(SqlServerHealthCheck));
builder.Services.AddMySqlServerHealthCheck(serviceProvider => MySqlServerDependencyInjection.GetConnectionString(builder.Configuration, serviceProvider, "NorthwindConnection"));
builder.Services.AddHealthChecksUI().AddInMemoryStorage();

builder.Services.AddBuilders();//del proyecto Mapper para no usar la libreria AutoMapper

builder.Services.AddCors(options => options.AddPolicy(myCORSPolicy,
    policy => policy.WithOrigins(builder.Configuration["OriginsCORS"])
    .AllowAnyHeader()
    .AllowAnyMethod()
));

builder.Services.AddMvc();

//services.AddJsonOptions(options =>
//                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase
//);

builder.Services.AddLogging(options =>
{
    options.AddConsole();
});

builder.Services.AddInjection();

//LoggerText.writeLog("antes de GetValue<string>(\"Secret\")");

builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddVersioning();
builder.Services.AddSwagger();
builder.Services.AddValidator();
builder.Services.AddRedisCache(builder.Configuration);

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

app.UseRouting();

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

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();