using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    public class UnitManagerRequirement : IAuthorizationRequirement { }

    public class UnitManagerHandler : AuthorizationHandler<UnitManagerRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UnitManagerRequirement requirement)
        {
            List<string> userRoles = context.User.UserRoles();
            List<string> accessedRoles = new List<string> { RoleNames.UnitManagerUser };
            bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
            if (hasAccess)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            context.Fail();
            return Task.CompletedTask;
        }
    }
}
