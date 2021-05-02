using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class UnitSpecialistLevel2AndUnitManagerRequirement : IAuthorizationRequirement { }

   public class UnitSpecialistLevel2AndUnitManagerHandler : AuthorizationHandler<UnitSpecialistLevel2AndUnitManagerRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UnitSpecialistLevel2AndUnitManagerRequirement requirement)
      {
         List<string> userRoles = context.User.UserRoles();
         List<string> accessedRoles = new List<string> { RoleNames.UnitSpecialistLevel2, RoleNames.UnitManagerUser };
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
