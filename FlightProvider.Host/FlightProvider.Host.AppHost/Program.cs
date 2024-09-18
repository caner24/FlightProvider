using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);


var elasticSearchPassword = builder.AddParameter("elasticsearchpassword");
var rabbitMQUserName = builder.AddParameter("rabbitMQUserName");
var rabbitMQPassword = builder.AddParameter("rabbitMQPassword");


var rabbitmq = builder.AddRabbitMQ("messaging").WithManagementPlugin().WithDataVolume();
var redis = builder.AddRedis("cache", port: 6379).WithDataVolume();

var elastic = builder.AddElasticsearch("elasticsearch", password: elasticSearchPassword);


var flightProviderSoap = builder.AddProject<Projects.FlightProvider>("flightprovidersoap");


var apiService = builder.AddProject<Projects.FlightProvider_Api>("flightproviderapi")
    .WithReference(rabbitmq)
    .WithReference(elastic)
    .WithExternalHttpEndpoints();

builder.AddNpmApp("react", "../../flightprovider.webui")
    .WithEnvironment("BROWSER", "none")
    .WithReference(apiService)
    .WithHttpEndpoint(port: 5002, targetPort: 5173, env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();


builder.Build().Run();
