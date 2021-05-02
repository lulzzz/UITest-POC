using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class UnitSpecialistLevel1Requirement : IAuthorizationRequirement { }

   public class UnitSpecialistLevel1Handler : AuthorizationHandler<UnitSpecialistLevel1Requirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UnitSpecialistLevel1Requirement requirement)
      {
         List<string> userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.UnitSpecialistLevel1 };
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
