namespace MusicStore.API.Configurations;

public class AppSettings
{
    private static IConfigurationRoot _configuration;

    static AppSettings()
    {
        // Bu kısım uygulama başladığında bir kez çalışır.
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

        _configuration = builder.Build();
    }
    
    public static string JWTSecretKey => _configuration["JWTSettings:SecretKey"];
    
    public static string InitializationVector => _configuration["Encrypt:Iv"];
    
    public static string Key => _configuration["Encrypt:Key"];
    
}