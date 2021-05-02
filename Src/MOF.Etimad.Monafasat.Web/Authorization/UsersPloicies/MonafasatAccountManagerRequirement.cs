using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class MonafasatAccountManagerRequirement : IAuthorizationRequirement { }

   public class MonafasatAccountManagerHandler : AuthorizationHandler<MonafasatAccountManagerRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MonafasatAccountManagerRequirement requirement)
      {
         List<string> userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.MonafasatAccountManager, RoleNames.CustomerService };
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
