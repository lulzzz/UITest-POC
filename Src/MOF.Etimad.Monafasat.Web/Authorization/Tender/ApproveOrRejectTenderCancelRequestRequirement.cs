using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class ApproveOrRejectTenderCancelRequestRequirement : IAuthorizationRequirement { }

   public class ApproveOrRejectTenderCancelRequestHandler : AuthorizationHandler<ApproveOrRejectTenderCancelRequestRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApproveOrRejectTenderCancelRequestRequirement requirement)
      {
         var userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.OffersPurchaseSecretary, RoleNames.Auditer, RoleNames.EtimadOfficer, RoleNames.OffersOpeningAndCheckManager, RoleNames.OffersPurchaseManager, RoleNames.OffersOppeningManager, RoleNames.OffersCheckManager, RoleNames.PreQualificationCommitteeManager };
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
