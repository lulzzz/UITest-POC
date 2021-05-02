using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class AdminAndAccountManagerRequirement : IAuthorizationRequirement { }

   public class AdminAndAccountManagerHandler : AuthorizationHandler<AdminAndAccountManagerRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminAndAccountManagerRequirement requirement)
      {
         var userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.MonafasatAdmin, RoleNames.MonafasatAccountManager };
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
