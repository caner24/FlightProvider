using FlightProvider.Application.Flight.Commands.Response;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FlightProvider.Application.Flight.Commands.Request
{
    public record AvailabilitySearchCommandRequest : IRequest<Result<List<AvailabilitySearchCommandResponse>>>
    {
        public string? Origin { get; init; }
        public string? Destination { get; init; }
        public DateTime DepartureDate { get; init; }
    }

}
