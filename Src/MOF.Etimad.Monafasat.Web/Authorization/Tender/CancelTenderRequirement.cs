using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class CancelTenderRequirement : IAuthorizationRequirement { }

   public class CancelTenderHandler : AuthorizationHandler<CancelTenderRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CancelTenderRequirement requirement)
      {
         var userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.DataEntry, RoleNames.Auditer, RoleNames.supplier, RoleNames.OffersOpeningAndCheckManager, RoleNames.OffersOpeningAndCheckSecretary, RoleNames.OffersPurchaseManager, RoleNames.OffersPurchaseSecretary, RoleNames.OffersOppeningManager, RoleNames.OffersOppeningSecretary, RoleNames.OffersCheckSecretary, RoleNames.OffersCheckManager, RoleNames.MonafasatAdmin, RoleNames.MonafasatAccountManager, RoleNames.TechnicalCommitteeUser, RoleNames.PreQualificationCommitteeManager, RoleNames.PreQualificationCommitteeSecretary, RoleNames.PurshaseSpecialist, RoleNames.EtimadOfficer };
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
