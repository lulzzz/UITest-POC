using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class GetOpeningQuantityTablesComponentRequirement : IAuthorizationRequirement { }

   public class GetOpeningQuantityTablesComponentHandler : AuthorizationHandler<GetOpeningQuantityTablesComponentRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GetOpeningQuantityTablesComponentRequirement requirement)
      {
         var userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> {
            RoleNames.supplier, RoleNames.OffersOppeningSecretary, RoleNames.OffersOppeningManager, RoleNames.OffersCheckSecretary, RoleNames.OffersCheckManager, RoleNames.FinancialSupervisor,
            RoleNames.OffersPurchaseManager, RoleNames.OffersPurchaseSecretary, RoleNames.OffersOpeningAndCheckSecretary, RoleNames.OffersOpeningAndCheckManager, RoleNames.PurshaseSpecialist, RoleNames.EtimadOfficer,
            RoleNames.DataEntry, RoleNames.Auditer, RoleNames.MonafasatAdmin, RoleNames.MonafasatAccountManager, RoleNames.UnitSpecialistLevel1, RoleNames.UnitSpecialistLevel2, RoleNames.UnitManagerUser
         };
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
