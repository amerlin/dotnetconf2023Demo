# Demo 4 - Automatic Metrics

.NET Runtime Metrics -  Prometheus DEMO

##### Setup application
```
dotnet new webapi --use-controllers
```

#### Install nuget packages
```
dotnet add package OpenTelemetry
dotnet add package OpenTelemetry.Instrumentation.AspNetCore
dotnet add package OpenTelemetry.Extensions.Hosting 
dotnet add package OpenTelemetry.Exporter.Console
dotnet add package OpenTelemetry.Instrumentation.Http    (collects our outgoing HTTP request metrics)
dotnet add package OpenTelemetry.Instrumentation.Runtime (collects runtime metrics)
dotnet add package OpenTelemetry.Exporter.Prometheus.AspNetCore --prerelease (collects prometheus metrics)
```

Data to show
```
http.server.duration -> ms 
```

Program.cs
```
builder.Services.AddOpenTelemetry()
    .WithMetrics(builder => builder
        .AddConsoleExporter()
        .AddHttpClientInstrumentation()  //collects our outgoing HTTP request metrics
        .AddRuntimeInstrumentation()     //collects runtime metrics
        .AddPrometheusExporter()         //collects PrometheusExporter
        .AddAspNetCoreInstrumentation());
```

Setup prometheus configuration by prometheus-config.yaml file
Setup the port (port where api is running) and name in the targets configurations: 
```
global:
  scrape_interval: 15s
  evaluation_interval: 15s
scrape_configs:
  - job_name: "Metrics.NET"
    static_configs:
      - targets: ["host.docker.internal:5091"]
```

Prometheus docker image
```
docker run -d -p 9090:9090 -v c:\temp\prometheus-config.yaml:/etc/prometheus/prometheus.yml --name prometheus prom/prometheus
```

Prometheus default scrape api is metrics/  - add in Program.cs
```
app.UseOpenTelemetryPrometheusScrapingEndpoint();
```

Sample metrics that we need to extrac in prometheus
```
http_server_request_duration_seconds_count
```

Sample1
Now, let’s run our application and create a couple of new computer components using the /api/orders/create-component
total_computers_components_ComputerComponents

Sample2 - Tags
Now, let’s run our application and create a couple of new computer components using the /api/orders/create-component

Sample3 - Observe
Observers capture the current values at a particular point in time and allow the caller to provide a callback to control this value
Let’s run our application, and start by creating a couple of computer components using the /api/orders/create-component endpoint. Next, we’ll create a couple of orders, using one or more of our computer component Ids, and send a request to the /api/orders/create-order endpoint.
total_orders_orders

Sample4 - Histograms
The final instrument we’ll look at is the histogram. Unlike the counter and gauge, histograms track the entire value distribution of a given metric. Using histograms gives us the flexibility to view our metrics in different ways, such as percentiles, standard deviation, min/max, etc.

call create-component
call create-order
call create-checkout

components_per_order...

.AddView(
            instrumentName: "components-per-order",
            new ExplicitBucketHistogramConfiguration { Boundaries = new double[] { 1, 2, 5, 10 } })
    )
