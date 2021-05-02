using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class CreateAnnouncementRequirement : IAuthorizationRequirement
   {

   }

   public class CreateAnnouncementHandler : AuthorizationHandler<CreateAnnouncementRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreateAnnouncementRequirement requirement)
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
