using FlightProvider.Application.Stripe.Commands.Request;
using FlightProvider.Entity;
using FlightProvider.Infrastructure.Abstract;
using FluentResults;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Stripe;
using Stripe.Checkout;
using Microsoft.Extensions.Options;
using FlightProvider.Entity.Dto;
using MassTransit.Transports;

namespace FlightProvider.Application.Stripe.Handlers.CommandHandler
{
    public class CreateSuccessBillingCommandHandler : IRequestHandler<CreateSuccessBillingCommandRequest, Result>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IFlightDal _flightDal;
        private readonly ClaimsPrincipal _claimsPrincipal;
        public CreateSuccessBillingCommandHandler(IPublishEndpoint publishEndpoint, IFlightDal flightDal, ClaimsPrincipal claimPrincipal)
        {
            _publishEndpoint = publishEndpoint;
            _flightDal = flightDal;
            _claimsPrincipal = claimPrincipal;
        }
        public async Task<Result> Handle(CreateSuccessBillingCommandRequest request, CancellationToken cancellationToken)
        {
            var service = new SessionService();
            try
            {
                Session session = service.Get(request.session_id);
                var index = 0;

                var quantity = session.Metadata[$"quantity_{index}"];
                var price = session.Metadata[$"price_{index}"];

                var arrivalcity_ = session.Metadata[$"arrivalcity_{index}"];
                var arrivaltime_ = session.Metadata[$"arrivaltime_{index}"];
                var arrivaldate_ = session.Metadata[$"arrivaldate_{index}"];

                var fligdeparturetime_htId = session.Metadata[$"departuretime_{index}"];
                var departuredate_ = session.Metadata[$"departuredate_{index}"];
                var departureCity_ = session.Metadata[$"departureCity_{index}"];


                var flightNumber = Guid.NewGuid();
                var emailAdress = session.Metadata[$"email_{index}"];
                await _flightDal.AddAsync(new Entity.Flight
                {
                    TotalPrice = Convert.ToDouble(price),
                    CreatedDate = DateTime.Now,
                    FlightNo = flightNumber,
                    FlightDetail = new List<FlightDetail>
    {
        new FlightDetail
        {
            ArrivalCity = arrivalcity_,
            ArrivalDate = DateOnly.FromDateTime(Convert.ToDateTime(arrivaldate_)),
            ArrivalTime = TimeSpan.Parse(arrivaltime_),
            DepartureCity = departureCity_,
            DepartureDate = DateOnly.FromDateTime(Convert.ToDateTime(departuredate_)),
            DepartureTime = TimeSpan.Parse(fligdeparturetime_htId)
        }
                    }
                });

                await _publishEndpoint.Publish(new FlightTicketDto { Email = emailAdress, FlightNumber = flightNumber.ToString() });


                return Result.Ok();

            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}

