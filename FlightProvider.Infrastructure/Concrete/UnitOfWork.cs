using FlightProvider.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProvider.Infrastructure.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IFlightDal _flightDal;
        private readonly FlightContext _flightContext;
        public UnitOfWork(IFlightDal flightDal, FlightContext flightContext)
        {
            _flightDal = flightDal;
            _flightContext = flightContext;
        }


        public IFlightDal FlightDal => _flightDal;


        public async Task BeginTransactionAsync()
        {
            if (_flightContext.Database.CurrentTransaction == null)
            {
                await _flightContext.Database.BeginTransactionAsync();
            }
        }

        public async Task CommitAsync()
        {
            if (_flightContext.Database.CurrentTransaction != null)
            {
                await _flightContext.Database.CurrentTransaction.CommitAsync();
            }
        }
        public void Dispose()
        {
            _flightContext.Dispose();
        }
    }
}
