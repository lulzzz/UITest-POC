using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class QualificationSecretaryAndManagerRequirement : IAuthorizationRequirement { }

   public class QualificationSecretaryAndManagerHandler : AuthorizationHandler<QualificationSecretaryAndManagerRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, QualificationSecretaryAndManagerRequirement requirement)
      {
         List<string> userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.PreQualificationCommitteeManager, RoleNames.PreQualificationCommitteeSecretary };
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
