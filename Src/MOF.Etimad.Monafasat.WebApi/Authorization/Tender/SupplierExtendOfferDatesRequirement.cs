using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    public class SupplierExtendOfferDatesRequirement : IAuthorizationRequirement { }

    public class SupplierExtendOfferDatesHandler : AuthorizationHandler<SupplierExtendOfferDatesRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SupplierExtendOfferDatesRequirement requirement)
        {
            var userRoles = context.User.UserRoles();
            List<string> accessedRoles = new List<string> { RoleNames.DataEntry, RoleNames.PurshaseSpecialist, RoleNames.EtimadOfficer, RoleNames.PreQualificationCommitteeSecretary, RoleNames.OffersPurchaseSecretary, RoleNames.supplier };
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
