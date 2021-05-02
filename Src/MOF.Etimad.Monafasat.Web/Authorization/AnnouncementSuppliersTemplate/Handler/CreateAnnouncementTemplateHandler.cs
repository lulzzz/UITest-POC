using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class CreateAnnouncementTemplateRequirement : IAuthorizationRequirement
   {
   }

   public class CreateAnnouncementTemplateHandler : AuthorizationHandler<CreateAnnouncementTemplateRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreateAnnouncementTemplateRequirement requirement)
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



