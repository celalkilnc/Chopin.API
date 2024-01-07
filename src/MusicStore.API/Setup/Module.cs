using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MusicStore.API.Actions;
using MusicStore.API.Services;
using MusicStore.Application.Utils.AppSetting;
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
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["JWTSettings:Issuer"],
                    ValidAudience = _configuration["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:SecretKey"]))
                };
            });
        
        #region ..::Dependencies::..

        services.AddSingleton<IAppSetting,AppSetting>();
        
        //Repo
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IProductRepository, ProductRepository>();
        services.AddSingleton<IPhotoRepository,PhotoRepository>();

        //Actions
        services.AddSingleton<IAuthService, AuthAction>();
        services.AddSingleton<IProductService,ProductAction>();

        #endregion
    }
}
