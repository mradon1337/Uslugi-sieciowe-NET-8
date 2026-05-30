using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var httpClient = new HttpClient();
string apiKey = "368220bc78e816154ef241f44c949127";

app.MapGet("/pogoda/{miasto}", async (string miasto) =>
{
    string url = $"https://api.openweathermap.org/data/2.5/weather?q={miasto}&appid={apiKey}&units=metric&lang=pl";

    var response = await httpClient.GetAsync(url);

    // jak miasto nie istnieje albo zly klucz
    if (!response.IsSuccessStatusCode)
    {
        return Results.NotFound($"Nie znaleziono pogody dla miasta: {miasto}");
    }

    string json = await response.Content.ReadAsStringAsync();

    // zamiana JSON na obiekt C#
    var pogoda = JsonSerializer.Deserialize<WeatherResponse>(json);

    return Results.Ok($"Pogoda dla {pogoda.Name}: {pogoda.Main.Temp}°C " +
                      $"(odczuwalna {pogoda.Main.FeelsLike}°C), " +
                      $"{pogoda.Weather[0].Description}, " +
                      $"wilgotnosc {pogoda.Main.Humidity}%, " +
                      $"wiatr {pogoda.Wind.Speed} m/s");
});

app.Run();