using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProvider.Presentation.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class FlightController : ControllerBase
    {

        [HttpGet("GetFlightData")]
        public async Task<IActionResult> GetFlightData()
        {
            return Ok();
        }
    }
}
