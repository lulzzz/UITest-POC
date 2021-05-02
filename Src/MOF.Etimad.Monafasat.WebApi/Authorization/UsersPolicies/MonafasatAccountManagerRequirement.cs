using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    public class MonafasatAccountManagerRequirement : IAuthorizationRequirement { }

    public class MonafasatAccountManagerHandler : AuthorizationHandler<MonafasatAccountManagerRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public MonafasatAccountManagerHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MonafasatAccountManagerRequirement requirement)
        {
            var authFilterCtx = httpContextAccessor;
            var newRoles = authFilterCtx.HttpContext.Request.Headers["NewRoles"].ToString();
            var newRolesList = newRoles.Length > 0 ? newRoles.Substring(0, newRoles.Length - 1).Split(",").ToList() : new List<string>();
            //var userRoles = context.User.UserRoles();
            //var role = context.User.Claims.Any(x => (x.Type == ClaimTypes.Role || x.Type == "role") &&( x.Value == RoleNames.OffersCheckSecretary || x.Value == RoleNames.OffersCheckManager) && newRolesList.Contains(x.Value));
            List<string> accessedRoles = new List<string> { RoleNames.MonafasatAccountManager, RoleNames.CustomerService };
            bool hasAccess = IdentityConfigs.CheckForUserNewRoles(newRolesList, accessedRoles);
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
