using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{

   public class DeleteAnnouncementRequirement : IAuthorizationRequirement
   {

   }

   public class DeleteAnnouncementHandler : AuthorizationHandler<DeleteAnnouncementRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DeleteAnnouncementRequirement requirement)
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
