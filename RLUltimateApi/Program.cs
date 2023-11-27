using Microsoft.EntityFrameworkCore;
using RLUltimateApi.Controllers;
using RLUltimateApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DATABASE_CONNECTION_STRING");
builder.Services.AddDbContext<UltimateContext>(options => options.UseMySQL(connectionString));
builder.Services.AddScoped<UltimateContext>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseMiddleware<ApiKeyMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
