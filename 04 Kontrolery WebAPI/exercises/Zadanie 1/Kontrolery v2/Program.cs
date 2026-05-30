using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// rejestracja swaggera
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// wlaczenie swaggera
app.UseSwagger();
app.UseSwaggerUI();

// --- pogoda z OpenWeatherMap ---

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

    return Results.Ok($"Pogoda dla {pogoda.Name}: {pogoda.Main.Temp}\u00b0C " +
                      $"(odczuwalna {pogoda.Main.FeelsLike}\u00b0C), " +
                      $"{pogoda.Weather[0].Description}, " +
                      $"wilgotnosc {pogoda.Main.Humidity}%, " +
                      $"wiatr {pogoda.Wind.Speed} m/s");
});

// --- CRUD miast ---

var miasta = new List<City>
{
    new City { Id = 1, Name = "Warszawa" },
    new City { Id = 2, Name = "Krakow" },
    new City { Id = 3, Name = "Gdansk" }
};

// wszystkie miasta
app.MapGet("/miasta", () => miasta);

// jedno miasto po id
app.MapGet("/miasta/{id}", (int id) =>
{
    var miasto = miasta.FirstOrDefault(m => m.Id == id);
    if (miasto == null)
        return Results.NotFound($"Nie ma miasta o id {id}");
    return Results.Ok(miasto);
});

// dodanie nowego miasta
app.MapPost("/miasta", (City nowe) =>
{
    nowe.Id = miasta.Max(m => m.Id) + 1; // kolejne wolne id
    miasta.Add(nowe);
    return Results.Created($"/miasta/{nowe.Id}", nowe);
});

// aktualizacja miasta po id
app.MapPut("/miasta/{id}", (int id, City zmiana) =>
{
    var miasto = miasta.FirstOrDefault(m => m.Id == id);
    if (miasto == null)
        return Results.NotFound($"Nie ma miasta o id {id}");
    miasto.Name = zmiana.Name;
    return Results.Ok(miasto);
});

// usuniecie miasta po id
app.MapDelete("/miasta/{id}", (int id) =>
{
    var miasto = miasta.FirstOrDefault(m => m.Id == id);
    if (miasto == null)
        return Results.NotFound($"Nie ma miasta o id {id}");
    miasta.Remove(miasto);
    return Results.Ok($"Usunieto miasto {miasto.Name}");
});

app.Run();
