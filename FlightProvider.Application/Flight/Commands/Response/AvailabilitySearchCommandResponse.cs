using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightProvider.Application.Flight.Commands.Response
{

    public record AvailabilitySearchCommandResponse
    {
        public string? FlightNumber { get; init; }
        public DateTime DepartureDateTime { get; init; }
        public DateTime ArrivalDateTime { get; init; }
        public decimal Price { get; init; }

    }
}
