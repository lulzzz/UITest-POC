using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    public class DashboardRejectedTendersRequirement : IAuthorizationRequirement { }

    public class DashboardRejectedTendersHandler : AuthorizationHandler<DashboardRejectedTendersRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public DashboardRejectedTendersHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DashboardRejectedTendersRequirement requirement)
        {
            var authFilterCtx = httpContextAccessor;
            var newRoles = authFilterCtx.HttpContext.Request.Headers["NewRoles"].ToString();
            var newRolesList = newRoles.Length > 0 ? newRoles.Substring(0, newRoles.Length - 1).Split(",").ToList() : new List<string>();

            List<string> accessedRoles = new List<string> { RoleNames.SecretaryGrievanceCommittee, RoleNames.PrePlanningCreator, RoleNames.DataEntry, RoleNames.OffersOppeningSecretary, RoleNames.OffersCheckSecretary, RoleNames.MonafasatBlockListSecritary, RoleNames.OffersPurchaseManager };

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