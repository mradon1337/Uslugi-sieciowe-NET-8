using Microsoft.EntityFrameworkCore;
using BlogCMS.Data;
using BlogCMS.Repositories;

var builder = WebApplication.CreateBuilder(args);

// kontrolery
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// podłączenie bazy danych
builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<DbContext, BlogDbContext>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(EfCoreRepository<>));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
