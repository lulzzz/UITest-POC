using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class OpenOffersReportRequirement : IAuthorizationRequirement { }

   public class OpenOffersReportHandler : AuthorizationHandler<OpenOffersReportRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OpenOffersReportRequirement requirement)
      {
         var userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.supplier, RoleNames.OffersOppeningSecretary, RoleNames.OffersOppeningManager, RoleNames.OffersCheckSecretary, RoleNames.OffersCheckManager, RoleNames.OffersPurchaseManager, RoleNames.OffersPurchaseSecretary, RoleNames.OffersOpeningAndCheckSecretary, RoleNames.OffersOpeningAndCheckManager, RoleNames.PurshaseSpecialist, RoleNames.EtimadOfficer, RoleNames.DataEntry, RoleNames.Auditer, RoleNames.MonafasatAdmin, RoleNames.MonafasatAccountManager, RoleNames.UnitSpecialistLevel1, RoleNames.UnitSpecialistLevel2, RoleNames.UnitManagerUser };
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
