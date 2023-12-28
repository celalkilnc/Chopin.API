using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MusicStore.Application.Models.App.Auth;
using MusicStore.Domain.Entities;

namespace MusicStore.Application.Utils;

public class TokenService
{
    public static mdlGeneratedToken GenerateToken(User user, string secretKey)
    {
        var expTime = DateTime.UtcNow.AddHours(5);
        var role = Helper.GetEnumDescription(user.Status);

        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email,user.Email),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, role) //role definition for the token
        };

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expTime, // Token's time
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(Convert.FromBase64String(secretKey)),
                    SecurityAlgorithms.HmacSha256Signature)
        });

        return new mdlGeneratedToken()
        {
            Token = tokenHandler.WriteToken(token),
            Status = role,
            Expire = expTime
        };
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
        };

        var handler = new JwtSecurityTokenHandler();
        SecurityToken validatedToken;

        try
        {
            return handler.ValidateToken(jwtToken, tokenValidationParameters, out validatedToken);
        }
        catch (SecurityTokenException ex)
        {
            Console.WriteLine($"Token parsing error: {ex.Message}");
            return null;
        }
    }
}