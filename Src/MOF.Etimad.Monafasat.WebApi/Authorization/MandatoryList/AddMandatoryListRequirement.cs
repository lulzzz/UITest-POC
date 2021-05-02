using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    public class AddMandatoryListRequirement : IAuthorizationRequirement { }

    public class AddMandatoryListHandler : AuthorizationHandler<AddMandatoryListRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AddMandatoryListRequirement requirement)
        {
            var userRoles = context.User.UserRoles();
            List<string> accessedRoles = new List<string> { RoleNames.MandatoryListOfficer };
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

