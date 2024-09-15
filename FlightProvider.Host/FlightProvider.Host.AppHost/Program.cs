using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);


var elasticSearchPassword = builder.AddParameter("elasticsearchpassword");
var elastic = builder.AddElasticsearch("elasticsearch", password: elasticSearchPassword);


var flightProviderSoap = builder.AddProject<Projects.FlightProvider>("flightprovidersoap");

var apiService = builder.AddProject<Projects.FlightProvider_Api>("flightproviderapi").
    WithReference(flightProviderSoap)
    .WithReference(elastic)
    .WithExternalHttpEndpoints();

builder.AddNpmApp("react", "../../flightprovider.webui")
    .WithEnvironment("BROWSER", "none")
    .WithReference(apiService)
    .WithHttpEndpoint(port: 5000, targetPort: 5173, env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();
