using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProvider.Entity.Results
{
    public class FlightNotFoundResult : Error
    {
        public FlightNotFoundResult() : base("You'r searched flight was not found.")
        {

        }
    }
}
