using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class AdminAndBlackListCommitteeRequirement : IAuthorizationRequirement { }

   public class AdminAndBlackListCommitteeHandler : AuthorizationHandler<AdminAndBlackListCommitteeRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminAndBlackListCommitteeRequirement requirement)
      {
         var userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.MonafasatAdmin, RoleNames.MonafasatBlackListCommittee, RoleNames.MonafasatBlockListSecritary };
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
