using Hexagonale.Movies.Application.Services;
using Hexagonale.Movies.Domain.Ports;
using Hexagonale.Movies.Infrastructure.Data;
using Hexagonale.Movies.Infrastructure.Repositories;
using Hexagonale.Movies.Infrastructure.SeedData;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuration des ports et adapters
builder.Services.AddScoped<IFavorisService, FavorisService>();
builder.Services.AddScoped<IFavorisRepository, FavorisRepository>();
builder.Services.AddScoped<IFilmRepository, FilmRepository>();
builder.Services.AddScoped<IFavorisRepository, FavorisRepository>();

// Configuration de la base de donn�es
builder.Services.AddDbContext<MoviesFDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<MoviesFDbContext>();
        await DatabaseSeeder.SeedAsync(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.UseAuthorization();

app.MapControllers();

app.Run();
