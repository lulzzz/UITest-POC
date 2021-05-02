using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class SuppliersReportRequirement : IAuthorizationRequirement { }

   public class SuppliersReportHandler : AuthorizationHandler<SuppliersReportRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SuppliersReportRequirement requirement)
      {
         List<string> userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.ApproveTenderAward, RoleNames.DataEntry, RoleNames.Auditer, RoleNames.MonafasatAccountManager, RoleNames.MonafasatAdmin, RoleNames.OffersOpeningAndCheckSecretary,
            RoleNames.OffersOpeningAndCheckManager, RoleNames.OffersOppeningSecretary, RoleNames.OffersOppeningManager, RoleNames.OffersCheckSecretary, RoleNames.OffersCheckManager, RoleNames.OffersPurchaseManager,
            RoleNames.OffersPurchaseSecretary, RoleNames.PurshaseSpecialist, RoleNames.EtimadOfficer, RoleNames.UnitSpecialistLevel1, RoleNames.UnitSpecialistLevel2, RoleNames.UnitManagerUser, RoleNames.FinancialSupervisor };

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
