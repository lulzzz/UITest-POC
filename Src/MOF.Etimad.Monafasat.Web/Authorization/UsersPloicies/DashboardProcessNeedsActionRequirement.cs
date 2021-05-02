using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class DashboardProcessNeedsActionRequirement : IAuthorizationRequirement { }

   public class DashboardProcessNeedsActionHandler : AuthorizationHandler<DashboardProcessNeedsActionRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DashboardProcessNeedsActionRequirement requirement)
      {
         List<string> userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.supplier, RoleNames.ManagerGrievanceCommittee, RoleNames.MonafasatBlockListSecritary, RoleNames.MonafasatBlackListCommittee, RoleNames.DataEntry, RoleNames.Auditer, RoleNames.OffersOppeningManager, RoleNames.OffersCheckManager, RoleNames.PreQualificationCommitteeManager, RoleNames.OffersPurchaseManager, RoleNames.PrePlanningAuditor, RoleNames.OffersPurchaseSecretary, RoleNames.PrePlanningAuditor, RoleNames.PrePlanningCreator, RoleNames.PurshaseSpecialist, RoleNames.EtimadOfficer, RoleNames.ApproveTenderAward, RoleNames.OffersOpeningAndCheckManager };
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
