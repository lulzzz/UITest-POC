using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace MOF.Etimad.Monafasat.Web.Authorization
{

   public class SupplierAnnouncementTemplatRequirement : IAuthorizationRequirement
   {

   }
   public class SupplierAnnouncementTemplateHandler : AuthorizationHandler<SupplierAnnouncementTemplatRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SupplierAnnouncementTemplatRequirement requirement)
      {
         var role = context.User.UserRole();
         if (role == null || !new List<string> { RoleNames.supplier }.Contains(role))
         {
            context.Fail();
            return Task.CompletedTask;
         }
         context.Succeed(requirement);
         return Task.CompletedTask;
      }
   }
}
