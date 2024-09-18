using FlightProvider.Entity.Dto;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProvider.Application.Stripe.Commands.Request
{
    public record CreateCheckoutRequest: FlightDetailConsumerDto,IRequest<Result<string>>
    {

    }
}
