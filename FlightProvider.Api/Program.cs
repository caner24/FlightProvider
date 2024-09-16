using FlightProvider;
using FlightProvider.Api.Extensions;
using FlightProvider.Application;
using FlightProvider.Entity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.ConfigureController();
builder.Services.IdentityConfiguration(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("FlightProvider.Application")));
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAirSearch, AirSearch>();
builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();
app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(_ =>
{
    _.AllowAnyHeader();
    _.AllowAnyMethod();
    _.AllowAnyOrigin();
});
app.MapGroup("/api/identity").MapIdentityApi<User>();
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
