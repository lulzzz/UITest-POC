using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class GetAnnouncementDetailsHandler : AuthorizationHandler<GetAnnouncementDetailsRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GetAnnouncementDetailsRequirement requirement)
      {
         var role = context.User.UserRole();
         if (role == null || !new List<string> { RoleNames.MonafasatAdmin,
            RoleNames.DataEntry,
            RoleNames.Auditer,
            //RoleNames.SupplierPolicy,
            //RoleNames.supplier,
            RoleNames.OffersOppeningSecretary,
            RoleNames.OffersOppeningManager,
            RoleNames.OffersCheckSecretary,
            RoleNames.OffersCheckManager,
            RoleNames.MonafasatAccountManager,
            RoleNames.CustomerService,
            RoleNames.FinancialSupervisor,
            RoleNames.OffersPurchaseSecretary,
            RoleNames.OffersPurchaseManager
         }.Contains(role))
         {
            context.Fail();
            return Task.CompletedTask;
         }
         context.Succeed(requirement);
         return Task.CompletedTask;
      }
   }
}
