using Newtonsoft.Json;
using WeatherWorkerService.Data;
using WeatherWorkerService.Models;

namespace WeatherWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly string _apiKey;

        private readonly HttpClient _httpClient = new();


        public Worker(
            ILogger<Worker> logger,
            IServiceScopeFactory scopeFactory,
            IConfiguration configuration)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _apiKey = configuration["OpenWeather:ApiKey"] ?? string.Empty;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var cities = new[] { "Warszawa", "Chełm", "Lublin" };

            if (string.IsNullOrWhiteSpace(_apiKey))
                _logger.LogWarning("Brak klucza API (OpenWeather:ApiKey w appsettings.json).");

            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var city in cities)
                {
                    var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric";
                    try
                    {
                        var response = await _httpClient.GetAsync(url, stoppingToken);
                        response.EnsureSuccessStatusCode();

                        var content = await response.Content.ReadAsStringAsync(stoppingToken);
                        var weatherData = JsonConvert.DeserializeObject<WeatherData>(content);

                        if (weatherData is null)
                        {
                            _logger.LogWarning("Pusta/niepoprawna odpowiedz dla miasta {City}", city);
                            continue;
                        }

                        var weather = weatherData.ToWeather(city);

                        using var scope = _scopeFactory.CreateScope();
                        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                        dbContext.Weathers.Add(weather);
                        await dbContext.SaveChangesAsync(stoppingToken);

                        _logger.LogInformation(
                            "Zapisano pogode dla {City}: {Temp} C, {Desc}",
                            city, weather.Temperature, weather.Description);
                    }
                    catch (HttpRequestException ex)
                    {
                        _logger.LogError("Blad przy probie pobrania pogody dla miasta {City}: {Message}",
                            city, ex.Message);
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}
