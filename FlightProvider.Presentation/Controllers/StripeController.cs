
using Asp.Versioning;
using FlightProvider.Application.Flight.Commands.Request;
using MediatR;
using Microsoft.Extensions.Configuration;
using FlightProvider.Application.Stripe.Commands.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace FlightProvider.Presentation.Controllers
{

    [ApiController]
    [Route("api/v{v:apiVersion}/[controller]/")]
    [ApiVersion("1.0")]

    public class StripeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StripeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("createCheckoutSession")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] CreateCheckoutRequest createCheckoutRequest)
        {
            var response = await _mediator.Send(createCheckoutRequest);
            Response.Headers.Add("Location", response.Value);
            return AcceptedAtAction(nameof(Success), new { sessionId = response.Value });
        }

        [HttpGet("success")]
        public async Task<IActionResult> Success([FromQuery] CreateSuccessBillingCommandRequest createSuccessBillingCommandRequest)
        {
            var response = await _mediator.Send(createSuccessBillingCommandRequest);
            if (!response.IsSuccess)
                return BadRequest(response.Errors);
            return Ok();
        }

        [HttpGet("cancel")]
        public IActionResult Cancel()
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "The payment has been canceled.");
        }
    }
}
