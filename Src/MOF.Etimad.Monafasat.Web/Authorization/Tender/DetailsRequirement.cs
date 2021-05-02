using Microsoft.AspNetCore.Authorization;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Authorization
{
   public class DetailsRequirement : IAuthorizationRequirement { }

   public class DetailsHandler : AuthorizationHandler<DetailsRequirement>
   {
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DetailsRequirement requirement)
      {
         //            var authFilterCtx = httpContextAccessor;
         //var newRoles = authFilterCtx.HttpContext.Request.Headers["NewRoles"].ToString();
         //var newRolesList = newRoles.Length > 0 ? newRoles.Substring(0, newRoles.Length - 1).Split(",").ToList() : new List<string>();
         var userRoles = context.User.UserRoles();
         //var role = context.User.Claims.Any(x => (x.Type == ClaimTypes.Role || x.Type == "role") &&( x.Value == RoleNames.OffersCheckSecretary || x.Value == RoleNames.OffersCheckManager) && newRolesList.Contains(x.Value));
         List<string> accessedRoles = new List<string> { RoleNames.PurshaseSpecialist, RoleNames.EtimadOfficer, RoleNames.DataEntry, RoleNames.Auditer, RoleNames.supplier, RoleNames.OffersOppeningManager, RoleNames.OffersOppeningSecretary, RoleNames.OffersCheckSecretary, RoleNames.OffersCheckManager, RoleNames.MonafasatAdmin, RoleNames.MonafasatAccountManager, RoleNames.TechnicalCommitteeUser, RoleNames.CustomerService };
         bool hasAccess = IdentityConfigs.CheckForUserNewRoles(userRoles, accessedRoles);
         if (hasAccess)
         {
            context.Succeed(requirement);
            return Task.CompletedTask;
         }
         context.Fail();
         return Task.CompletedTask;
      }
   }
}
