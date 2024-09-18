using FlightProvider.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlightProvider.Infrastructure.Concrete
{
    public class FlightContext : IdentityDbContext<User>
    {
        public FlightContext()
        {
            
        }
        public FlightContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Flight> Flight { get; set; }
        public DbSet<FlightDetail> FlightDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

    }
}
