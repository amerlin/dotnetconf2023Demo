using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CustomerController(CustomMetrics metrics) : ControllerBase
{
    [HttpPost]
    public async Task Create(int storeId)
    {
        metrics.OrderCounter.Add(1, new KeyValuePair<string, object?>("value", storeId));
    }

}
