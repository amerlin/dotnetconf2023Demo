using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class MetricsController : ControllerBase
{
    [HttpGet("incoming")]
    public IActionResult IncomingHttpRequest()
    {
        return Ok("Incoming HTTP request");
    }

    [HttpGet("outgoing")]
    public async Task OutgoingHttpRequest()
    {
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync("https://amerlin.keantex.com");
        response.EnsureSuccessStatusCode();
    }
}