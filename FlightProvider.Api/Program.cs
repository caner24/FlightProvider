using FlightProvider;
using FlightProvider.Api.Extensions;
using FlightProvider.Api.Mail;
using FlightProvider.Application;
using FlightProvider.Entity;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using Stripe;
using System.Reflection;
using System.Web.Services.Description;

Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .CreateLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.AddServiceDefaults();

    // Add services to the container.
    builder.Services.AddProblemDetails();
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.ConfigureController();
    builder.Services.IdentityConfiguration(builder.Configuration);
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("FlightProvider.Application")));
    builder.Services.SwaggerConfig();
    builder.Services.AddScoped<IAirSearch, AirSearch>();
    builder.Services.ConfigureApiVersioning();
    builder.Services.ConfigureMassTransit(builder.Configuration);
    builder.Services.ConfigureMailService(builder.Configuration);
    builder.Services.StripeConfigurationConfig(builder.Configuration);
    builder.Services.ServiceLifetimeSettings();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .WithExposedHeaders("Location");
        });
    });

    builder.Services.AddStackExchangeRedisOutputCache(_ =>
    {
        _.Configuration = builder.Configuration.GetConnectionString("cache");
    });
    builder.Services.AddAutoMapper(typeof(Program));

    var app = builder.Build();
    app.MapDefaultEndpoints();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseCors("AllowAll");
    app.MapGroup("/api/identity").MapIdentityApi<User>();
    app.UseExceptionHandler();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "An exception happened while project was started.");

}
finally
{
    Log.CloseAndFlush();

}