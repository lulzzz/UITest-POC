using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{

   public class SearchAnnouncementTemplateJoinedSupplierRequirement : IAuthorizationRequirement
   {

   }
   public class SearchAnnouncementTemplateJoinedSupplierHandler : AuthorizationHandler<SearchAnnouncementTemplateJoinedSupplierRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SearchAnnouncementTemplateJoinedSupplierRequirement requirement)
      {
         var role = context.User.UserRole();
         if (role == null || !new List<string> {
            RoleNames.DataEntry, RoleNames.Auditer, RoleNames.MonafasatAccountManager, RoleNames.MonafasatAdmin, RoleNames.OffersOppeningSecretary,
            RoleNames.OffersOppeningManager, RoleNames.OffersCheckSecretary, RoleNames.OffersCheckManager, RoleNames.PreQualificationCommitteeSecretary, RoleNames.PreQualificationCommitteeManager,
            RoleNames.OffersPurchaseSecretary, RoleNames.OffersPurchaseManager, RoleNames.CustomerService, RoleNames.UnitSpecialistLevel1, RoleNames.UnitSpecialistLevel2, RoleNames.UnitManagerUser
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
