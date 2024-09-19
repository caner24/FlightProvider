
using Asp.Versioning;
using FlightProvider.Application.Flight.Commands.Request;
using FlightProvider.Infrastructure.Abstract;
using FlightProvider.Presentation.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProvider.Presentation.Controllers
{

    [ApiController]
    [Route("api/v{v:apiVersion}/[controller]/")]
    [ApiVersion("1.0")]
    public class FlightController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        public FlightController(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("AvailabilitySearchRequest")]
        [OutputCache(Duration = 60)]
        public async Task<IActionResult> AvailabilitySearchRequest([FromBody] AvailabilitySearchCommandRequest searchFlightCommandRequest)
        {
            var response = await _mediator.Send(searchFlightCommandRequest);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Value);
            }
            return Ok(response.Value);
        }

        [HttpGet("GetFlightData")]
        [OutputCache(Duration = 60)]
        public async Task<IActionResult> GetFlightData()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://www.flypgs.com/apint/BestDeals/GetCitiesForBestDeals?language=tr");
            request.Headers.Add("Cookie", "ak_bmsc=E86EFE8A36B97E77B4AD1EE1A5392C58~000000000000000000000000000000~YAAQn4YUAnHIxf6RAQAAoRXcChmJOnHnXy3gmV/CV5UGRMaWrIlSlwi8EyWDtPdTI0sUjs9DLxF039Pp06m1cXtkF3I+3TsZbYrhkmMO2wb5Ec7wsj13A5rcKLlV8Yj34pk2k+DITAGWYITzMvk4Eba3PNo8QUC0Uzc+FeJ6LZ+vUtB0nnRgj0+NvZlxSk4f1r7vWdnnpPh1Vjz9REL5JOAxq4EANB5JPIjeKC7oBCq+ueEmG7QKpW/3TOSgceJdDGPjBmGK9EvqTmxXCKOp0l3sYKRQL0Erqc//TtC8UkLYLTsoJ6OGGjRZqoQvZG59AlOF8rQ6KPzYYZPM2p/DzCnC3NDYSm9SsN/Lq7yAkZt0Vp7ZpHVezMXrpwE=; bm_sv=EE1D722625CD4D85B7A4C2A54075F7E8~YAAQDxczFwd00MmRAQAAs8TxChlsA7kYXf/7AtBdBf0pfkUYapgzLVPfUt5+Rtf/ccILv2qYziKHql6tJY+J06Lj5sCNtJ9+hiWMkKd5vZyRW9e2UHzgI8a6TmjkNlVQizsP9voPTZJRwJChf/sCwHG921dquHMlouw4GAEnJOxK9/3kuu/BDq/WaCNC68hUQBiBh9RRm82UVz+qUOBbyE3KHamLR073A2jbpceEbv9PKniJq2ztyJ4Imhp7gdWPPA==~1; HMF_CI=4ada3aca9cad8029c80b0b18b686af2329dd762e5093f74ec8d271b0250fa236724991d1eb06a6647024b99d09eb888bfc063f86cafc742d5f3d194801845c9663");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();

            return Ok(JsonConvert.DeserializeObject(data));
        }

        [HttpGet("GetFlightWithNumber")]
        [OutputCache(Duration = 60)]
        public async Task<IActionResult> GetFlightData([FromQuery] string flightNumber)
        {
            if (!Guid.TryParse(flightNumber, out Guid flightGuid))
            {
                return BadRequest("Invalid flight number format.");
            }
            var response = await _unitOfWork.FlightDal
                .Get(x => x.FlightNo == flightGuid).Include(x => x.FlightDetail)
                .FirstOrDefaultAsync();

            if (response == null)
                return BadRequest();

            return Ok(response);
        }


    }
}
