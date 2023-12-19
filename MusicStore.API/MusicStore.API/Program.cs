using MusicStore.API.Configurations;
using MusicStore.API.Setup;
using MusicStore.API.Utils.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

new Module(builder.Configuration).Configure(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.Use(async (context, next) =>
{
    TokenService.DecryptJwtToken(context.Request.Headers["Authorization"],AppSettings.JWTSecretKey);
   
    await next.Invoke();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();