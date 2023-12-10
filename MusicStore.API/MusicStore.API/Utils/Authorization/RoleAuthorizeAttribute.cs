using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MusicStore.API.Utils.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RoleAuthorizeAttribute : TypeFilterAttribute
{
    public RoleAuthorizeAttribute(Type type) : base(type)
    {
    }
}

public class RoleAuthorizeFilter : IAuthorizationFilter
{
    private readonly string[] _roles;

    public RoleAuthorizeFilter(params string[] roles)
    {
        _roles = roles;
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