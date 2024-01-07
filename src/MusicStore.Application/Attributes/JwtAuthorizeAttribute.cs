using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MusicStore.Application.Utils;
using MusicStore.Application.Utils.AppSetting;
using MusicStore.Domain.Entities;

namespace MusicStore.Application.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class JwtAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private List<string> _roles;

    public JwtAuthorizeAttribute(string roles)
    {
        if (!string.IsNullOrEmpty(roles))
            _roles = new List<string>(roles.Split(","));
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (_roles != null)
        {
            var userRole = GetRoleFromToken(context);
            if (!_roles.Contains(userRole))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }

        if (token == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        } 

        if (!ValidateToken(token, AppSetting.JwtSecretKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }
    }

    private bool ValidateToken(string token, string secretKey)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            return true;
        }
        catch
        {
            return false;
        }
    }

    private string GetRoleFromToken(AuthorizationFilterContext context)
    {
        var httpContext = context.HttpContext;

        var authenticationService = httpContext.RequestServices.GetRequiredService<IAuthenticationService>();
        var result = authenticationService.AuthenticateAsync(httpContext, JwtBearerDefaults.AuthenticationScheme)
            .Result; 

        return result.Principal?.FindFirst(ClaimTypes.Role)?.Value;
    }
}