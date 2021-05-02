using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class SendAnnouncementForApprovalRequirement : IAuthorizationRequirement
   {
   }
   public class SendAnnouncementForApprovalHandler : AuthorizationHandler<SendAnnouncementForApprovalRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SendAnnouncementForApprovalRequirement requirement)
      {
         var role = context.User.UserRole();
         if (role == null || !new List<string> { RoleNames.DataEntry }.Contains(role))
         {
            context.Fail();
            return Task.CompletedTask;
         }
         context.Succeed(requirement);
         return Task.CompletedTask;
      }
   }


}
