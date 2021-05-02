using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class OpenTenderDetailsRequirement : IAuthorizationRequirement { }

   public class OpenTenderDetailsHandler : AuthorizationHandler<OpenTenderDetailsRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OpenTenderDetailsRequirement requirement)
      {
         List<string> userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.DataEntry, RoleNames.Auditer, RoleNames.OffersOppeningSecretary, RoleNames.OffersOppeningManager, RoleNames.OffersCheckSecretary, RoleNames.supplier, RoleNames.OffersCheckManager, RoleNames.MonafasatAdmin, RoleNames.MonafasatAccountManager };
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
