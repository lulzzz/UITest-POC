using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class DashboardRejectedTendersRequirement : IAuthorizationRequirement { }

   public class DashboardRejectedTendersHandler : AuthorizationHandler<DashboardRejectedTendersRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DashboardRejectedTendersRequirement requirement)
      {
         List<string> userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.PurshaseSpecialist, RoleNames.EtimadOfficer, RoleNames.PrePlanningCreator, RoleNames.OffersOpeningAndCheckSecretary, RoleNames.OffersOpeningAndCheckManager, RoleNames.DataEntry, RoleNames.OffersOppeningSecretary, RoleNames.OffersCheckSecretary, RoleNames.OffersPurchaseSecretary, RoleNames.PreQualificationCommitteeSecretary, RoleNames.SecretaryGrievanceCommittee, RoleNames.PrePlanningAuditor, RoleNames.PrePlanningCreator, RoleNames.MonafasatBlockListSecritary, RoleNames.OffersPurchaseManager };
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
