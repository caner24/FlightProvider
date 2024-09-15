using Elastic.Clients;
using Aspire.Elastic.Clients;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FlightProvider.Infrastructure.Concrete;
using FlightProvider.Entity;

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


        public static void AddSoapClient(this IServiceCollection services)
        {
            services.AddHttpClient("soapApi", _ =>
            {
                _.BaseAddress = new Uri("http+https://flightprovidersoap");
            });
        }

        public static void ConfigureController(this IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
                config.CacheProfiles.Add("5mins", new CacheProfile() { Duration = 300 });
            })
.AddXmlDataContractSerializerFormatters()
.AddApplicationPart(Assembly.Load("FlightProvider.Application"))
.AddNewtonsoftJson(opt =>
    opt.SerializerSettings.ReferenceLoopHandling =
    Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
        }
    }
}
