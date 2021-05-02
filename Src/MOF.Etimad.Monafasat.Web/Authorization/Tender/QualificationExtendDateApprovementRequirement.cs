using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class QualificationExtendDateApprovementRequirement : IAuthorizationRequirement { }

   public class QualificationExtendDateApprovementHandler : AuthorizationHandler<QualificationExtendDateApprovementRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, QualificationExtendDateApprovementRequirement requirement)
      {
         List<string> userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.DataEntry, RoleNames.Auditer, RoleNames.PreQualificationCommitteeManager, RoleNames.PreQualificationCommitteeSecretary };
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
