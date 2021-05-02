using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{


   public class ApproveAnnouncementTemplatRequirement : IAuthorizationRequirement
   {

   }
   public class ApproveAnnouncementTemplateHandler : AuthorizationHandler<ApproveAnnouncementTemplatRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApproveAnnouncementTemplatRequirement requirement)
      {
         var role = context.User.UserRole();
         if (role == null || !new List<string> { RoleNames.DataEntry, RoleNames.Auditer, }.Contains(role))
         {
            context.Fail();
            return Task.CompletedTask;
         }
         context.Succeed(requirement);
         return Task.CompletedTask;
      }
   }
}
