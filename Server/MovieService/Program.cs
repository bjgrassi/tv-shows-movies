using Microsoft.EntityFrameworkCore;

using MovieService.Repositories;
using MovieService.Services;

var builder = WebApplication.CreateBuilder(args);
var builderConfig = builder.Configuration;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var assembly = System.Reflection.Assembly.GetExecutingAssembly();
builder.Services.AddAutoMapper(assembly);

// DB Config
builder.Services.AddDbContext<MovieDbContext>(c => 
{
    c.UseSqlServer(builderConfig["TvMovieSerieDB"]);
    c.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieService, MovieService.Services.MovieService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();
app.Run();
