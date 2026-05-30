var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// do losowania
var random = new Random();

// pierwszy endpoint - losowa temperatura
app.MapGet("/temperatura", () =>
{
    int temp = random.Next(-20, 41); // od -20 do 40 stopni
    return $"Temperatura: {temp}°C";
});

// lista kierunkow wiatru
var kierunki = new List<string>
{
    "Polnocny",
    "Poludniowy",
    "Wschodni",
    "Zachodni",
    "Polnocno-zachodni",
    "Polnocno-wschodni",
    "Poludniowo-zachodni",
    "Poludniowo-wschodni"
};

// drugi endpoint - losowy kierunek wiatru
app.MapGet("/wiatr", () =>
{
    int index = random.Next(kierunki.Count); // losowy indeks z listy
    return $"Kierunek wiatru: {kierunki[index]}";
});

app.Run();