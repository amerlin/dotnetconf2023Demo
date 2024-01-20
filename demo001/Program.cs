// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, Genova!");

// --------

// using Microsoft.Extensions.Logging;
// using OpenTelemetry.Logs;
// using
// var loggerFactory = LoggerFactory.Create(builder =>
// {
//     builder.AddOpenTelemetry(options =>
//     {
//         options.AddConsoleExporter();
//     });
// });
// var logger = loggerFactory.CreateLogger<Program>();
// logger.LogInformation("Hello from OpenTelemetry");

// --------

// using System.Diagnostics.Metrics;
// using OpenTelemetry;
// using OpenTelemetry.Metrics;

// Meter MyMeter = new("ConsoleDemo.Metrics", "1.0");

// Counter<long> RequestCounter = MyMeter.CreateCounter<long>("RequestCounter");

// using var meterProvider = Sdk.CreateMeterProviderBuilder()
//             .AddMeter("ConsoleDemo.Metrics")
//             .AddConsoleExporter()
//             .Build();

// RequestCounter.Add(1, new KeyValuePair<string, object?>("POST Request", HttpMethod.Post));
// RequestCounter.Add(1, new KeyValuePair<string, object?>("GET Request", HttpMethod.Get));
// RequestCounter.Add(1, new KeyValuePair<string, object?>("GET Request", HttpMethod.Get));
// RequestCounter.Add(1, new KeyValuePair<string, object?>("POST Request", HttpMethod.Post));
// RequestCounter.Add(1, new KeyValuePair<string, object?>("PUT Request", HttpMethod.Put));

// --------

// using OpenTelemetry;
// using OpenTelemetry.Trace;
// using System.Diagnostics;
// ActivitySource MyActivitySource = new("ConsoleDemo.Trace");
// using
// var tracerProvider = Sdk.CreateTracerProviderBuilder().AddSource("ConsoleDemo.Trace").AddConsoleExporter().Build();
// using (var activity = MyActivitySource.StartActivity("ActivityStarted"))
// {
//     int StartNumber = 10000;
//     activity?.SetTag("StartNumber", StartNumber);
//     for (int i = 0; i < StartNumber; i++)
//     {
//         DoProcess(i);
//     }
//     activity?.SetStatus(ActivityStatusCode.Ok);
// }

// void DoProcess(int currentNumber)
// {
//     var doubleValue = currentNumber * 2;
// }