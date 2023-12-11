using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MusicStore.API.Utils.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RoleAuthorizeAttribute : TypeFilterAttribute
{
    public RoleAuthorizeAttribute(string roles) : base(typeof(RoleAuthorizeFilter))
    {
        Arguments = new object[] { roles };
    }
}

public class RoleAuthorizeFilter : IAuthorizationFilter
{
    private readonly string[] _roles;

    public RoleAuthorizeFilter(string roles)
    {
        _roles =  roles.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(role => role.Trim())
            .ToArray();
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (!user.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
 
        if (!_roles.Any(role => user.IsInRole(role)))
        {
            context.Result = new ForbidResult();
            return;
        }
    }
}