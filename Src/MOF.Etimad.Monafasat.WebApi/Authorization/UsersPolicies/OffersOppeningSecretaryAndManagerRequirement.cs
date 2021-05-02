using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    public class OffersOppeningSecretaryAndManagerRequirement : IAuthorizationRequirement { }

    public class OffersOppeningSecretaryAndManagerHandler : AuthorizationHandler<OffersOppeningSecretaryAndManagerRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public OffersOppeningSecretaryAndManagerHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OffersOppeningSecretaryAndManagerRequirement requirement)
        {
            var authFilterCtx = httpContextAccessor;
            var newRoles = authFilterCtx.HttpContext.Request.Headers["NewRoles"].ToString();
            var newRolesList = newRoles.Length > 0 ? newRoles.Substring(0, newRoles.Length - 1).Split(",").ToList() : new List<string>();

            List<string> accessedRoles = new List<string> { RoleNames.OffersOppeningSecretary, RoleNames.OffersOppeningManager };

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
