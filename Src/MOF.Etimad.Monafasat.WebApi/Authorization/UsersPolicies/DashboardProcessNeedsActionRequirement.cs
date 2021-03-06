using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    public class DashboardProcessNeedsActionRequirement : IAuthorizationRequirement { }

    public class DashboardProcessNeedsActionHandler : AuthorizationHandler<DashboardProcessNeedsActionRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public DashboardProcessNeedsActionHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DashboardProcessNeedsActionRequirement requirement)
        {
            var authFilterCtx = httpContextAccessor;
            var newRoles = authFilterCtx.HttpContext.Request.Headers["NewRoles"].ToString();
            var newRolesList = newRoles.Length > 0 ? newRoles.Substring(0, newRoles.Length - 1).Split(",").ToList() : new List<string>();

            List<string> accessedRoles = new List<string> { RoleNames.ManagerGrievanceCommittee, RoleNames.MonafasatBlockListSecritary, RoleNames.MonafasatBlackListCommittee, RoleNames.PrePlanningAuditor, RoleNames.DataEntry, RoleNames.Auditer, RoleNames.OffersOppeningManager, RoleNames.OffersCheckManager, RoleNames.VROOpenAndCheckingViewPolicy };

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