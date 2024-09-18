using FlightProvider.Application.Stripe.Commands.Request;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stripe;
using Stripe.Checkout;

namespace FlightProvider.Application.Stripe.Handlers.CommandHandler
{
    public class CreateCheckoutCommandHandler : IRequestHandler<CreateCheckoutRequest, Result<string>>
    {

        private readonly IMediator _mediator;
        public CreateCheckoutCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Result<string>> Handle(CreateCheckoutRequest request, CancellationToken cancellationToken)
        {
            var domain = "https://localhost:7155";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = domain + "/api/Stripe/success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = domain + "/api/Stripe/cancel",
                Metadata = new Dictionary<string, string>()
            };

            int index = 0;

            options.LineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(request.TotalPrice * 100),
                    Currency = "TRY",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = request.FlightNo,
                    },
                },
                Quantity = request.Amount,
            });

            options.Metadata.Add($"flightid_{index}", request.FlightNo.ToString());
            options.Metadata.Add($"quantity_{index}", request.Amount.ToString());
            options.Metadata.Add($"price_{index}", request.TotalPrice.ToString());
            options.Metadata.Add($"arrivalcity_{index}", request.ArrivalCity.ToString());
            options.Metadata.Add($"arrivaltime_{index}", request.ArrivalTime.ToString());
            options.Metadata.Add($"arrivaldate_{index}", request.ArrivalDate.ToString());
            options.Metadata.Add($"departuretime_{index}", request.DepartureTime.ToString());
            options.Metadata.Add($"departuredate_{index}", request.DepartureDate.ToString());
            options.Metadata.Add($"departureCity_{index}", request.DepartureCity.ToString());
            options.CustomerEmail = request.Email;

            var service = new SessionService();
            Session session = await service.CreateAsync(options);
            return Result.Ok(session.Url);
        }
    }
}
