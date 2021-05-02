using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class AdminBlackListAndAccountManagerRequirement : IAuthorizationRequirement { }

   public class AdminBlackListAndAccountManagerHandler : AuthorizationHandler<AdminBlackListAndAccountManagerRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminBlackListAndAccountManagerRequirement requirement)
      {
         var userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.MonafasatAdmin, RoleNames.MonafasatBlackListCommittee, RoleNames.MonafasatAccountManager };
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
