using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class PrePlanningAuditingRequirement : IAuthorizationRequirement { }

   public class PrePlanningAuditingHandler : AuthorizationHandler<PrePlanningAuditingRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PrePlanningAuditingRequirement requirement)
      {
         List<string> userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.PrePlanningAuditor };
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
