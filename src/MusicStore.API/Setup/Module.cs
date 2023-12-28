using Microsoft.EntityFrameworkCore;
using MusicStore.API.Actions;
using MusicStore.API.Services;
using MusicStore.Persistance;
using MusicStore.Persistance.Repositories.Media;
using MusicStore.Persistance.Repositories.Product;
using MusicStore.Persistance.Repositories.User;

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
        services.AddDbContext<MSDBContext>(options => options
            .UseNpgsql(_configuration["ConnectionStrings:Postgres"]), ServiceLifetime.Singleton);
        
        #region ..::Dependencies::..

        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IProductRepository, ProductRepository>();
        services.AddSingleton<IPhotoRepository,PhotoRepository>();


        services.AddSingleton<IAuthService, AuthAction>();

        #endregion
    }
}
