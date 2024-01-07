using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using MusicStore.Application.Models.App;
using MusicStore.Application.Models.App.Auth;
using MusicStore.Domain.Entities;

namespace MusicStore.Application.Utils;

public class TokenService
{
    public static mdlGeneratedToken GenerateToken(User user, string secretKey, string issuer)
    {
        var expire = DateTime.Now.AddHours(1);
        var role = Helper.GetEnumDescription(user.Status);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, role),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSetting.AppSetting.JwtSecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            AppSetting.AppSetting.JwtIssuer,
            AppSetting.AppSetting.JwtAudience,
            claims,
            expires: expire,
            signingCredentials: credentials
        );

        return new()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expire = expire,
            Status = role,
        };
    }

    public static ClaimsPrincipal ValidateToken(string token, string secretKey, string issuer)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSetting.AppSetting.JwtSecretKey));

        var validationParameters = new TokenValidationParameters
        {
            ValidIssuer = AppSetting.AppSetting.JwtIssuer,
            ValidAudience = AppSetting.AppSetting.JwtAudience,
            IssuerSigningKey = key,
        };

        try
        {
            return tokenHandler.ValidateToken(token, validationParameters, out _);
        }
        catch
        {
            return null;
        }
    }

    public static ClaimsPrincipal GetPrincipal(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
                return null;

            var validationParameters = new TokenValidationParameters
            {
                ValidIssuer = AppSetting.AppSetting.JwtIssuer,
                ValidAudience = AppSetting.AppSetting.JwtAudience,
                IssuerSigningKey =
                    new SymmetricSecurityKey(Convert.FromBase64String(AppSetting.AppSetting.JwtSecretKey))
            };

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

            return principal;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Token decrypt error: {ex.Message}");
            return null;
        }
    }

    public static mdlTokenInfo FieldTokenModel(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");


        if (!string.IsNullOrEmpty(token))
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenSlices = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (tokenSlices != null)
            {
                var email = tokenSlices.Claims.FirstOrDefault(c =>
                    c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
                var name = tokenSlices.Claims
                    .FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

                var res = new mdlTokenInfo() { Email = email, Name = name };
                res.SetRoleStrToEnm(tokenSlices.Claims.FirstOrDefault(c =>
                    c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value);

                return res;
            }
        }

        return null;
    }
}