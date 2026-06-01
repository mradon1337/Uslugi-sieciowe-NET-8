using Microsoft.EntityFrameworkCore;
using TravelQuotesApi.Data;
using TravelQuotesApi.Interfaces;
using TravelQuotesApi.Models;
using TravelQuotesApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// kontrolery
builder.Services.AddControllers();

// Swagger - interfejs do testowania API w przeglądarce
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// baza danych (connection string siedzi w appsettings.json)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// rejestracja repozytorium - kto poprosi o IRepository<Quote>, dostanie QuoteRepository
builder.Services.AddScoped<IRepository<Quote>, QuoteRepository>();

var app = builder.Build();

// Swagger włączony zawsze - wygodniej przy nauce
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
