using Microsoft.EntityFrameworkCore;

using UserArchiveService.Repositories;
using UserArchiveService.Services;

var builder = WebApplication.CreateBuilder(args);
var builderConfig = builder.Configuration;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var assembly = System.Reflection.Assembly.GetExecutingAssembly();
builder.Services.AddAutoMapper(assembly);

// DB Config
builder.Services.AddDbContext<UserArchiveDbContext>(c => 
{
    c.UseSqlServer(builderConfig["TvUserArchiveDB"]);
    c.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddHttpClient<MovieServiceClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5182/"); 
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
builder.Services.AddScoped<IUserMovieArchiveRepository, UserMovieArchiveRepository>();
builder.Services.AddScoped<IUserMovieArchiveService, UserMovieArchiveService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();
app.Run();
