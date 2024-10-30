using IquiriumBE.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace IquiriumBE.Infrastructure.Authorization
{
    public class HasRoleAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly Roles _role;

        public HasRoleAttribute(Roles role)
        {
            _role = role;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user == null || !user.Identity!.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var roleName = RoleHelper.GetRoleName(_role);
            if (!user.IsInRole(roleName))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
