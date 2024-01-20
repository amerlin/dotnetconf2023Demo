# Demo002 - Aspectio

Connection with the service: Aspectio - https://www.aspecto.io/
Service login: https://app.aspecto.io/

### Application

Create application
```
dotnet new webapi --use-controllers
```

Add MongoDb.Driver
```
dotnet add package MongoDB.Driver
```

Add model
```
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
public class Todo
{
  [BsonElement("_id")]
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string Id { get; set; } = null!;
  [BsonElement("title")]
  public string Title { get; set; } = null!;
}
```

Add package for OpenTelemetry
```
dotnet add package OpenTelemetry.Exporter.OpenTelemetryProtocol
dotnet add package OpenTelemetry.Extensions.Hosting 
dotnet add package OpenTelemetry.Instrumentation.AspNetCore
dotnet add package OpenTelemetry.Instrumentation.Http
dotnet add package MongoDB.Driver.Core.Extensions.DiagnosticSources
dotnet add package MongoDB.Driver.Core.Extensions.OpenTelemetry

dotnet add package OpenTelemetry.Exporter.OpenTelemetryProtocol
```

### Configuration

Program.cs
```
var serviceName = "your-service-name";
var serviceVersion = "your-service-version";
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
```