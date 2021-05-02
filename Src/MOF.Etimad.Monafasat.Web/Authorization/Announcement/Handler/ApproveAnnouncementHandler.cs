using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class ApproveAnnouncementRequirement : IAuthorizationRequirement
   {

   }
   public class ApproveAnnouncementHandler : AuthorizationHandler<ApproveAnnouncementRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApproveAnnouncementRequirement requirement)
      {
         var role = context.User.UserRole();
         if (role == null || !new List<string> { RoleNames.Auditer }.Contains(role))
         {
            context.Fail();
            return Task.CompletedTask;
         }
         context.Succeed(requirement);
         return Task.CompletedTask;
      }
   }
}
