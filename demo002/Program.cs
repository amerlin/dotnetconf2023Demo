using MongoDB.Driver;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using MongoDB.Driver.Core.Extensions.DiagnosticSources;

var builder = WebApplication.CreateBuilder(args);

//Mongo Connection
var mongoUrl = MongoUrl.Create("mongodb://admin:local@localhost:27017");
var clientSettings = MongoClientSettings.FromUrl(mongoUrl);
var mongoClient = new MongoClient(clientSettings);
clientSettings.ClusterConfigurator = cb => cb.Subscribe(new DiagnosticsActivityEventSubscriber());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var serviceName = "DotNetConf 2023 - Genova";
var serviceVersion = "version 0.1.0";
builder.Services.AddOpenTelemetry()
    .WithTracing(options => options.AddOtlpExporter(
        opt =>
        {
            opt.Endpoint = new Uri("https://otelcol.aspecto.io:4317");
            opt.Headers = $"Authorization=45b4c828-fe31-4f17-a1f9-98d9a1fdb070";
        }).AddSource(serviceName)
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName, serviceVersion: serviceVersion))
        .AddHttpClientInstrumentation()
        .AddAspNetCoreInstrumentation()
        .AddMongoDBInstrumentation()
    );

//add mongodb
builder.Services.AddSingleton(mongoClient.GetDatabase("todo"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
