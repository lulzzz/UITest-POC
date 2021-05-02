using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class MandatoryListApprovalRequirement : IAuthorizationRequirement { }

   public class MandatoryListApprovalHandler : AuthorizationHandler<MandatoryListApprovalRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MandatoryListApprovalRequirement requirement)
      {
         var userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.MandatoryListApprover };
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

