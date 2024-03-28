using MusicStore.API.Setup;
using MusicStore.Application.Models.Response.Product;
using MusicStore.Application.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
new Module(builder.Configuration).Configure(builder.Services);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    await next.Invoke();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();