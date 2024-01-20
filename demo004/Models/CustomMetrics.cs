using System.Diagnostics.Metrics;

public class CustomMetrics
{
    public Counter<int> OrderCounter { get; private set; }
    public CustomMetrics(IMeterFactory meterFactory)
    {
        Meter meter = meterFactory.Create("MyCustomMetric");
        OrderCounter = meter.CreateCounter<int>("mymetric.order.counter");
    }
}

