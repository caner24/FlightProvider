using Elastic.Clients.Elasticsearch;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;

namespace FlightProvider.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly HttpClient _httpClient;
    private readonly ElasticsearchClient _elasticSearchClient;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory httpClient, ElasticsearchClient elasticSearchClient)
    {
        _logger = logger;
        _httpClient = httpClient.CreateClient("soapApi");
        _elasticSearchClient = elasticSearchClient;
    }

    [HttpPost(Name = "GetFlightDetails")]
    public async Task<IActionResult> Get()
    {
        var clientA = _elasticSearchClient;
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5001/Service.svc");
        request.Headers.Add("SOAPAction", "http://tempuri.org/IAirSearch/AvailabilitySearch");
        var content = new StringContent(
            "<Envelope xmlns=\"http://schemas.xmlsoap.org/soap/envelope/\">\r\n" +
            "    <Body>\r\n" +
            "        <AvailabilitySearch xmlns=\"http://tempuri.org/\">\r\n" +
            "            <!-- Optional -->\r\n" +
            "            <request>\r\n" +
            "                <DepartureDate xmlns=\"http://schemas.datacontract.org/2004/07/FlightProvider\">2024-10-01T17:30:00</DepartureDate>\r\n" +
            "                <Destination xmlns=\"http://schemas.datacontract.org/2004/07/FlightProvider\">MUC</Destination>\r\n" +
            "                <Origin xmlns=\"http://schemas.datacontract.org/2004/07/FlightProvider\">FRA</Origin>\r\n" +
            "            </request>\r\n" +
            "        </AvailabilitySearch>\r\n" +
            "    </Body>\r\n" +
            "</Envelope>", Encoding.UTF8, "text/xml");
        request.Content = content;
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadAsStringAsync();
        return Ok(responseData);

    }
}
