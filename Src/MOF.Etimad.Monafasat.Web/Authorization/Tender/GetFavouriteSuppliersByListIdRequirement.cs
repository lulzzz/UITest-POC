using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class GetFavouriteSuppliersByListIdRequirement : IAuthorizationRequirement { }

   public class GetFavouriteSuppliersByListIdHandler : AuthorizationHandler<GetFavouriteSuppliersByListIdRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GetFavouriteSuppliersByListIdRequirement requirement)
      {
         List<string> userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.DataEntry, RoleNames.Auditer, RoleNames.MonafasatAdmin, RoleNames.MonafasatAccountManager, RoleNames.OffersOppeningSecretary, RoleNames.CustomerService, RoleNames.OffersOppeningManager, RoleNames.OffersCheckSecretary, RoleNames.OffersCheckManager, RoleNames.PurshaseSpecialist, RoleNames.EtimadOfficer };
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
