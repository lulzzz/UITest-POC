using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class GetAllAgencyAnnouncementHandler : AuthorizationHandler<GetAllAgencyAnnouncementRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GetAllAgencyAnnouncementRequirement requirement)
      {
         var role = context.User.UserRole();
         if (role == null || !new List<string> { RoleNames.MonafasatAdmin,
            RoleNames.DataEntry,
            RoleNames.Auditer,
            RoleNames.FinancialSupervisor,
            RoleNames.OffersOppeningSecretary,
            RoleNames.OffersOppeningManager,
            RoleNames.OffersCheckSecretary,
            RoleNames.OffersCheckManager,
            RoleNames.MonafasatAccountManager,
            RoleNames.CustomerService,
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
