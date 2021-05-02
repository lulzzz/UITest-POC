
using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{


   public class ReviewAnnouncementTemplateJoinRequestRequirement : IAuthorizationRequirement
   {

   }
   public class ReviewAnnouncementTemplateJoinRequestHandler : AuthorizationHandler<ReviewAnnouncementTemplateJoinRequestRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ReviewAnnouncementTemplateJoinRequestRequirement requirement)
      {
         var role = context.User.UserRole();
         if (role == null || !new List<string> { RoleNames.DataEntry, RoleNames.Auditer }.Contains(role))
         {
            context.Fail();
            return Task.CompletedTask;
         }
         context.Succeed(requirement);
         return Task.CompletedTask;
      }
   }
}
