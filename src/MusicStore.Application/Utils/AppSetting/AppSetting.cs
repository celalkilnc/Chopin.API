using Microsoft.Extensions.Configuration;

namespace MusicStore.Application.Utils.AppSetting;

public class AppSetting : IAppSetting
{
    private static IConfiguration Configuration { get; set; }

    static AppSetting()
    { 
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json");

        Configuration = builder.Build();
    }
 
    public static string JwtSecretKey => Configuration["JWTSettings:SecretKey"];
    public static string JwtIssuer => Configuration["JWTSettings:Issuer"];
    public static string JwtAudience => Configuration["JWTSettings:Audience"];
}