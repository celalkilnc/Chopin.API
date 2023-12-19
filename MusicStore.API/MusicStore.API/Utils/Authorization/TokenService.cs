using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MusicStore.API.Configurations;

namespace MusicStore.API.Utils.Authorization;

public class TokenService
{
    private string secretKey = AppSettings.JWTSecretKey;
    
    public string GenerateToken(string username, string role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Convert.FromBase64String(secretKey);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role) //role definition for the token
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(5), // Token's time
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    public static ClaimsPrincipal DecryptJwtToken(string jwtToken, string secretKey)
    {
        byte[] secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
            // Diğer parametreleri ayarlayabilirsiniz (issuer, audience, expiration, clock skew, vb.)
        };

        var handler = new JwtSecurityTokenHandler();
        SecurityToken validatedToken;

        try
        {
            // Token'ı çözme işlemi
            var principal = handler.ValidateToken(jwtToken, tokenValidationParameters, out validatedToken);

            return principal;
        }
        catch (SecurityTokenException ex)
        {
            Console.WriteLine($"Token çözme hatası: {ex.Message}");
            return null;
        }
    }
}