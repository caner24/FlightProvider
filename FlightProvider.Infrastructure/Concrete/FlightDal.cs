using FlightProvider.Domain.Data.EntityFramework;
using FlightProvider.Entity;
using FlightProvider.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProvider.Infrastructure.Concrete
{
    public class FlightDal : EFCoreRepositoryBase<FlightContext, Flight>, IFlightDal
    {
        public FlightDal(FlightContext tContext) : base(tContext)
        {
        }
    }
}
