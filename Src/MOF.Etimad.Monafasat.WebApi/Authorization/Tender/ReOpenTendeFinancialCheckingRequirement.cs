using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    public class ReOpenTendeFinancialCheckingRequirement : IAuthorizationRequirement { }

    public class ReOpenTendeFinancialCheckingHandler : AuthorizationHandler<ReOpenTendeFinancialCheckingRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ReOpenTendeFinancialCheckingRequirement requirement)
        {
            List<string> userRoles = context.User.UserRoles();
            List<string> accessedRoles = new List<string> { RoleNames.OffersCheckSecretary, RoleNames.OffersOppeningSecretary };
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
