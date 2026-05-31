using Microsoft.EntityFrameworkCore;
using BlogCMS.Data;
using BlogCMS.Repositories;

var builder = WebApplication.CreateBuilder(args);

// kontrolery
builder.Services.AddControllers();

// Swagger - interfejs do testowania API w przeglądarce
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// podłączenie bazy danych (connection string siedzi w appsettings.json)
builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// gdy repozytorium prosi o DbContext, podajemy nasz BlogDbContext
builder.Services.AddScoped<DbContext, BlogDbContext>();

// rejestracja repozytorium - kto poprosi o IRepository<T>, dostanie EfCoreRepository<T>
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfCoreRepository<>));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
