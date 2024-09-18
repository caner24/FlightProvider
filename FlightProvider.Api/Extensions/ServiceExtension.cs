using Elastic.Clients;
using Aspire.Elastic.Clients;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FlightProvider.Infrastructure.Concrete;
using FlightProvider.Entity;
using FlightProvider.Application;
using Asp.Versioning;
using Microsoft.Extensions.Configuration;
using MassTransit;
using FlightProvider.Api.Consumer;
using FlightProvider.Api.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe;
using FlightProvider.Infrastructure.Abstract;
using System.Security.Claims;

namespace FlightProvider.Api.Extensions
{
    public static class ServiceExtension
    {

        public static void IdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlConnection");
            ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);
            services.AddDbContext<FlightContext>(options => options.UseMySql(connectionString, serverVersion, b => b.MigrationsAssembly("FlightProvider.Api").
            EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null)));
            services.AddIdentityApiEndpoints<User>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                options.Lockout.MaxFailedAccessAttempts = 3;
            }).AddUserManager<UserManager<User>>().AddRoles<IdentityRole>().AddRoleManager<RoleManager<IdentityRole>>().AddApiEndpoints().AddDefaultTokenProviders().AddEntityFrameworkStores<FlightContext>();
            services.AddAuthorizationBuilder();
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
     opt.TokenLifespan = TimeSpan.FromHours(2));
        }

        public static void ConfigureController(this IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
            })
.AddApplicationPart(typeof(FlightProvider.Presentation.Controllers.FlightController).Assembly)
.AddNewtonsoftJson(opt =>
    opt.SerializerSettings.ReferenceLoopHandling =
    Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
        }


        public static void ConfigureApiVersioning(this IServiceCollection services)
        {
            var apiVersioningBuilder = services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ReportApiVersions = true;
                o.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-Version"),
                    new MediaTypeApiVersionReader("ver"));
            });
            apiVersioningBuilder.AddApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });
        }
        public static void ConfigureMassTransit(this IServiceCollection services, IConfiguration config)
        {
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumer<EmailConfirmationConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    var host = config.GetConnectionString("messaging");
                    cfg.Host(host);
                    cfg.ReceiveEndpoint("email-confirmation", e =>
                    {
                        e.ConfigureConsumer<EmailConfirmationConsumer>(context);
                    });
                });
            });
            services.AddSingleton<IEmailSender<User>, MailSender>();

        }
        public static void ConfigureMailService(this IServiceCollection services, IConfiguration configuration)
        {
            var email = configuration["EmailConfiguration:From"];
            var smtp = configuration["EmailConfiguration:SmtpServer"];
            var port = configuration["EmailConfiguration:Port"];
            var username = configuration["EmailConfiguration:Username"];
            var password = configuration["EmailConfiguration:Password"];

            services.AddFluentEmail(email).AddSmtpSender(smtp, Convert.ToInt16(port), username, password);
        }

        public static void StripeConfigurationConfig(this IServiceCollection services, IConfiguration configuration)
        {

            StripeConfiguration.ApiKey = configuration["Stripe:Secret"];
            services.AddSingleton<StripeClient>(provider => new StripeClient(configuration["Stripe:Secret"]));
        }

        public static void ServiceLifetimeSettings(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<ClaimsPrincipal>(provider =>
            {
                var context = provider.GetService<IHttpContextAccessor>();
                return context?.HttpContext?.User ?? new ClaimsPrincipal();
            });

            services.AddScoped<IFlightDal, FlightDal>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

    }
}
