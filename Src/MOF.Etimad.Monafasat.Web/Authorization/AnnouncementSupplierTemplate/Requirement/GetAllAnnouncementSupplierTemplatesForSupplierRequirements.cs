using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class GetAllAnnouncementSupplierTemplatesForSupplierRequirements : IAuthorizationRequirement { }


   public class GetAllAnnouncementSupplierTemplatesForSupplierHandler : AuthorizationHandler<GetAllAnnouncementSupplierTemplatesForSupplierRequirements>
   {
      private readonly IHttpContextAccessor httpContextAccessor;
      public GetAllAnnouncementSupplierTemplatesForSupplierHandler(IHttpContextAccessor httpContextAccessor)
      {
         this.httpContextAccessor = httpContextAccessor;
      }
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GetAllAnnouncementSupplierTemplatesForSupplierRequirements requirement)
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
