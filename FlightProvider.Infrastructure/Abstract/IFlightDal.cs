using FlightProvider.Domain.Data;
using FlightProvider.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProvider.Infrastructure.Abstract
{
    public interface IFlightDal : IEntityRepositoryBase<Flight>
    {

    }
}
