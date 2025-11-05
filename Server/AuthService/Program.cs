using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using AuthService.Repositories;
using AuthService.Services;

var builder = WebApplication.CreateBuilder(args);
var builderConfig = builder.Configuration;

builder.Services.AddControllers();

var assembly = System.Reflection.Assembly.GetExecutingAssembly();
builder.Services.AddAutoMapper(assembly);

// DB Config
builder.Services.AddDbContext<AuthDbContext>(c => c.UseSqlServer(builderConfig["TvAuthDB"]));
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();
app.Run();
