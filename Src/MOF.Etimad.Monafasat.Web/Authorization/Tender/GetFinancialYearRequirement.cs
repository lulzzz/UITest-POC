using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class GetFinancialYearRequirement : IAuthorizationRequirement { }

   public class GetFinancialYearHandler : AuthorizationHandler<GetFinancialYearRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GetFinancialYearRequirement requirement)
      {
         List<string> userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.DataEntry, RoleNames.Auditer, RoleNames.MonafasatAccountManager, RoleNames.MonafasatAdmin, RoleNames.OffersOppeningSecretary, RoleNames.OffersOppeningManager, RoleNames.OffersCheckSecretary, RoleNames.OffersCheckManager, RoleNames.CustomerService, RoleNames.supplier, RoleNames.TechnicalCommitteeUser, RoleNames.UnitManagerUser, RoleNames.UnitSpecialistLevel1, RoleNames.UnitSpecialistLevel2, RoleNames.OffersPurchaseSecretary, RoleNames.OffersPurchaseManager, RoleNames.EtimadOfficer, RoleNames.PurshaseSpecialist, RoleNames.VROOpenAndCheckingViewPolicy, RoleNames.UnitBusinessManagement };
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
