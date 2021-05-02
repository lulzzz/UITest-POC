using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class TenderRevisionsRequirement : IAuthorizationRequirement { }

   public class TenderRevisionsHandler : AuthorizationHandler<TenderRevisionsRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TenderRevisionsRequirement requirement)
      {
         List<string> userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.DataEntry, RoleNames.MonafasatAdmin, RoleNames.Auditer, RoleNames.MonafasatAccountManager, RoleNames.EtimadOfficer,RoleNames.PurshaseSpecialist,
                RoleNames.OffersPurchaseSecretary, RoleNames.OffersCheckSecretary, RoleNames.OffersPurchaseManager, RoleNames.OffersCheckManager, RoleNames.PreQualificationCommitteeSecretary, RoleNames.PreQualificationCommitteeManager };
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
