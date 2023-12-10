using MusicStore.API.Setup;

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
    var req = context;
    
    await next.Invoke();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
