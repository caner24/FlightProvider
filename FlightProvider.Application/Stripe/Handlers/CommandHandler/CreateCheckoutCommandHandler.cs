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
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace FlightProvider.Application.Stripe.Handlers.CommandHandler
{
    public class CreateCheckoutCommandHandler : IRequestHandler<CreateCheckoutRequest, Result<string>>
    {

        private readonly IMediator _mediator;
        private readonly ClaimsPrincipal _claim;
        private readonly IHttpContextAccessor _httpContext;
        public CreateCheckoutCommandHandler(IMediator mediator, ClaimsPrincipal claim, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _claim = claim;
            _httpContext = httpContextAccessor;
        }

        public async Task<Result<string>> Handle(CreateCheckoutRequest request, CancellationToken cancellationToken)
        {
            var httpContext = _httpContext.HttpContext.Request;
            var currentUrl = $"{httpContext.Scheme}://{httpContext.Host}";
            var domain = currentUrl;
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = domain + "/api/v1/Stripe/success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = domain + "/api/v1/Stripe/cancel",
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
            var email = _claim.Claims.Where(x => x.Type == ClaimTypes.Email).First().Value;
            options.Metadata.Add($"quantity_{index}", request.Amount.ToString());
            options.Metadata.Add($"price_{index}", request.TotalPrice.ToString());
            options.Metadata.Add($"arrivalcity_{index}", request.ArrivalCity.ToString());
            options.Metadata.Add($"arrivaltime_{index}", request.ArrivalTime.ToString());
            options.Metadata.Add($"arrivaldate_{index}", request.ArrivalDate.ToString());
            options.Metadata.Add($"departuretime_{index}", request.DepartureTime.ToString());
            options.Metadata.Add($"departuredate_{index}", request.DepartureDate.ToString());
            options.Metadata.Add($"email_{index}", email);
            options.Metadata.Add($"departureCity_{index}", request.DepartureCity.ToString());
            options.CustomerEmail = email;

            var service = new SessionService();
            Session session = await service.CreateAsync(options);
            return Result.Ok(session.Url);
        }
    }
}
