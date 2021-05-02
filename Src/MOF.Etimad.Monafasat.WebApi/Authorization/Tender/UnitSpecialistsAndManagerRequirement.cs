using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    public class UnitSpecialistsAndManagerRequirement : IAuthorizationRequirement { }

    public class UnitSpecialistsAndManagerHandler : AuthorizationHandler<UnitSpecialistsAndManagerRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UnitSpecialistsAndManagerRequirement requirement)
        {
            List<string> userRoles = context.User.UserRoles();
            List<string> accessedRoles = new List<string> { RoleNames.UnitSpecialistLevel1, RoleNames.UnitSpecialistLevel2, RoleNames.UnitManagerUser, RoleNames.UnitBusinessManagement };
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
