using AutoMapper;
using FlightProvider.Application.Flight.Commands.Request;
using FlightProvider.Application.Flight.Commands.Response;
using FlightProvider.Entity.Results;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProvider.Application.Flight.Handlers.CommandHandler
{
    public class SearchFlightCommandHandler : IRequestHandler<AvailabilitySearchCommandRequest, Result<List<AvailabilitySearchCommandResponse>>>
    {

        private readonly IAirSearch _airSearch;
        private readonly IMapper _mapper;
        public SearchFlightCommandHandler(IAirSearch airSearch, IMapper mapper)
        {
            _airSearch = airSearch;
            _mapper = mapper;
        }
        public async Task<Result<List<AvailabilitySearchCommandResponse>>> Handle(AvailabilitySearchCommandRequest request, CancellationToken cancellationToken)
        {
            var searchRequest = _mapper.Map<AvailabilitySearchCommandRequest, SearchRequest>(request);
            var response = await Task.Run(() => _airSearch.AvailabilitySearch(searchRequest));
            var searchResponse = _mapper.Map<List<AvailabilitySearchCommandResponse>>(response.FlightOptions);
            if (searchResponse == null)
            {
                return Result.Fail(new FlightNotFoundResult());
            }
            return Result.Ok(searchResponse);
        }
    }
}
