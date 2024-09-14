var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.


builder.AddElasticsearchClient("elasticsearch");

builder.Services.AddHttpClient("soapApi", _ =>
{
    _.BaseAddress = new Uri("http+https://flightprovidersoap");
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(_ =>
{
    _.AllowAnyHeader();
    _.AllowAnyMethod();
    _.AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
