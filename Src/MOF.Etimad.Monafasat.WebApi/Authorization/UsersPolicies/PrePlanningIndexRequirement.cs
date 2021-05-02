using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    public class PrePlanningIndexRequirement : IAuthorizationRequirement { }

    public class PrePlanningIndexHandler : AuthorizationHandler<PrePlanningIndexRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PrePlanningIndexRequirement requirement)
        {
            List<string> userRoles = context.User.UserRoles();
            List<string> accessedRoles = new List<string> { RoleNames.PrePlanningCreator, RoleNames.PrePlanningAuditor, RoleNames.supplier };
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
