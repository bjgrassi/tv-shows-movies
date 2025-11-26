using Microsoft.EntityFrameworkCore;

using ContentController.Repositories;
using ContentService.Services;

var builder = WebApplication.CreateBuilder(args);
var builderConfig = builder.Configuration;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var assembly = System.Reflection.Assembly.GetExecutingAssembly();
builder.Services.AddAutoMapper(assembly);

// DB Config
builder.Services.AddDbContext<ContentDbContext>(c => 
{
    c.UseSqlServer(builderConfig["TvMovieSerieDB"]);
    c.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<ISerieRepository, SerieRepository>();
builder.Services.AddScoped<ISerieService, SerieService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();
app.Run();
