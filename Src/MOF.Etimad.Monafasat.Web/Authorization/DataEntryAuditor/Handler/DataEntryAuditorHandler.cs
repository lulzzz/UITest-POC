using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class DataEntryAuditorHandler : AuthorizationHandler<DataEntryAuditorRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DataEntryAuditorRequirement requirement)
      {


         var role = context.User.Claims.FirstOrDefault(x => x.Type == "role");
         if (role == null)
         {
            context.Fail();
            return Task.CompletedTask;
         }
         if (!(role.Value == RoleNames.Auditer || role.Value == RoleNames.DataEntry || role.Value == RoleNames.PurshaseSpecialist || role.Value == RoleNames.EtimadOfficer))
         {
            context.Fail();
            return Task.CompletedTask;
         }

         context.Succeed(requirement);
         return Task.CompletedTask;
      }
   }
}
