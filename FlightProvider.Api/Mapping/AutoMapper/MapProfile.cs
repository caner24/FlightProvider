using AutoMapper;
using FlightProvider.Application.Flight.Commands.Request;
using FlightProvider.Application.Flight.Commands.Response;

namespace FlightProvider.Api.Mapping.AutoMapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {

            CreateMap<SearchRequest, AvailabilitySearchCommandRequest>().ReverseMap();
            CreateMap<AvailabilitySearchCommandResponse, FlightOption>().ReverseMap();

        }
    }
}
