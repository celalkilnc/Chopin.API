using Microsoft.EntityFrameworkCore;
using MusicStore.API.Persistance;

namespace MusicStore.API.Setup;

public class Module
{
    private readonly IConfiguration _configuration;

    public Module(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void Configure(IServiceCollection services)
    {
        services.AddDbContext<MusicStoreDbContext>(options => options
            .UseNpgsql(_configuration["ConnectionStrings:Postgres"]), ServiceLifetime.Singleton);
    }
}