using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    public class EditMandatoryListRequirement : IAuthorizationRequirement { }

    public class EditMandatoryListHandler : AuthorizationHandler<EditMandatoryListRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EditMandatoryListRequirement requirement)
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

