using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProvider.Application.Stripe.Commands.Request
{
    public record CreateSuccessBillingCommandRequest:IRequest<Result>
    {
        public string session_id { get; init; }
    }
}
