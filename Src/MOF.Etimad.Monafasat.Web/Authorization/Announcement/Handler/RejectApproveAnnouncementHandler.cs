using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class RejectApproveAnnouncementRequirement : IAuthorizationRequirement
   {

   }
   public class RejectApproveAnnouncementHandler : AuthorizationHandler<RejectApproveAnnouncementRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RejectApproveAnnouncementRequirement requirement)
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
