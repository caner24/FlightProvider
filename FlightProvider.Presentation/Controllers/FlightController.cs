
using FlightProvider.Application.Flight.Commands.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProvider.Presentation.Controllers
{

    [ApiController]
    [Route("api/[controller]/")]
    public class FlightController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FlightController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AvailabilitySearchRequest")]
        public async Task<IActionResult> AvailabilitySearchRequest([FromBody] AvailabilitySearchCommandRequest searchFlightCommandRequest)
        {
            var response = await _mediator.Send(searchFlightCommandRequest);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Value);
            }
            return Ok(response.Value);
        }
    }
}
