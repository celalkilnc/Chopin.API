using Microsoft.EntityFrameworkCore;
using MusicStore.API.Actions;
using MusicStore.API.Actions.Product;
using MusicStore.API.Persistance;
using MusicStore.API.Persistance.Repositories;
using MusicStore.API.Persistance.Repositories.Product;

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

        #region ..::Dependencies::..

        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IProductRepository, ProductRepository>();

        services.AddSingleton<IUserService, UserAction>();
        services.AddSingleton<IProductService, ProductAction>();

        #endregion
    }
}