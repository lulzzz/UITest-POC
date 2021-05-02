using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{

    public class GetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovementRequirement : IAuthorizationRequirement { }

    public class GetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovementHandler : AuthorizationHandler<GetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovementRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovementRequirement requirement)
        {
            List<string> userRoles = context.User.UserRoles();
            List<string> accessedRoles = new List<string> { RoleNames.DataEntry, RoleNames.Auditer, RoleNames.EtimadOfficer, RoleNames.PurshaseSpecialist, RoleNames.OffersPurchaseSecretary
         , RoleNames.OffersCheckSecretary , RoleNames.OffersPurchaseManager
         , RoleNames.OffersCheckManager,RoleNames.UnitManagerUser,RoleNames.ApproveTenderAward,RoleNames.OffersOppeningManager,RoleNames.OffersOppeningSecretary,RoleNames.UnitSpecialistLevel1,RoleNames.UnitSpecialistLevel2,RoleNames.UnitSecretaryUser};
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
