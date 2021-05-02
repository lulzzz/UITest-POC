using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class IndexRequirement : IAuthorizationRequirement { }

   public class IndexHandler : AuthorizationHandler<IndexRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IndexRequirement requirement)
      {
         List<string> userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.PurshaseSpecialist, RoleNames.EtimadOfficer, RoleNames.DataEntry, RoleNames.Auditer, RoleNames.supplier, RoleNames.MonafasatAccountManager,
            RoleNames.MonafasatAdmin, RoleNames.OffersOppeningSecretary, RoleNames.OffersOppeningManager, RoleNames.OffersCheckSecretary, RoleNames.OffersCheckManager, RoleNames.CustomerService, RoleNames.supplier,
            RoleNames.TechnicalCommitteeUser, RoleNames.MonafasatBlackListCommittee, RoleNames.ApproveTenderAward, RoleNames.OffersPurchaseSecretary, RoleNames.OffersPurchaseManager, RoleNames.OffersOpeningAndCheckManager,
            RoleNames.OffersOpeningAndCheckSecretary, RoleNames.UnitManagerUser, RoleNames.UnitSpecialistLevel1, RoleNames.UnitSpecialistLevel2, RoleNames.FinancialSupervisor };
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
