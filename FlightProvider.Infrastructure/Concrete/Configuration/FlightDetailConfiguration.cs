using FlightProvider.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProvider.Infrastructure.Concrete.Configuration
{
    public class FlightDetailConfiguration : IEntityTypeConfiguration<FlightDetail>
    {
        public void Configure(EntityTypeBuilder<FlightDetail> builder)
        {
            builder.HasKey(x => x.FlightId);
            builder.HasOne(x => x.Flight).WithMany(x => x.FlightDetail).HasForeignKey(x => x.FlightId);
        }
    }
}
