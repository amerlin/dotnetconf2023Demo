namespace Demo3Ui.Pages
{
    using Demo3Ui.Models;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly HttpClient _httpClient;
        public List<WeatherForecast> WeatherForecasts { get; set; } = new List<WeatherForecast>();

        public IndexModel(ILogger<IndexModel> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7027");
        }

        public async Task OnGetAsync()
        {
            var response = await _httpClient.GetAsync($"WeatherForecast");
            if(response.IsSuccessStatusCode)
            {
                WeatherForecasts = await response.Content.ReadFromJsonAsync<List<WeatherForecast>>()
                    ?? [];
            }
        }
    }
}
