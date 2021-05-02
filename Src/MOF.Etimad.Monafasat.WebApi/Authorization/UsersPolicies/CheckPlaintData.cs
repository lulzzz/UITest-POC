using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    public class CheckPlaintData : IAuthorizationRequirement { }

    public class CheckPlaintDataHandler : AuthorizationHandler<CheckPlaintData>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CheckPlaintData requirement)
        {
            List<string> userRoles = context.User.UserRoles();
            List<string> accessedRoles = new List<string> { RoleNames.SecretaryGrievanceCommittee, RoleNames.ManagerGrievanceCommittee, RoleNames.OffersCheckSecretary, RoleNames.OffersPurchaseSecretary, RoleNames.OffersCheckManager, RoleNames.OffersPurchaseManager, RoleNames.DataEntry, RoleNames.Auditer, RoleNames.OffersOppeningSecretary, RoleNames.OffersOppeningManager, RoleNames.MonafasatAdmin, RoleNames.MonafasatAccountManager };
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
