using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    public class AuditerAndTechnicalRequirement : IAuthorizationRequirement { }

    public class AuditerAndTechnicalHandler : AuthorizationHandler<AuditerAndTechnicalRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public AuditerAndTechnicalHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuditerAndTechnicalRequirement requirement)
        {
            var authFilterCtx = httpContextAccessor;
            var newRoles = authFilterCtx.HttpContext.Request.Headers["NewRoles"].ToString();
            var newRolesList = newRoles.Length > 0 ? newRoles.Substring(0, newRoles.Length - 1).Split(",").ToList() : new List<string>();

            List<string> accessedRoles = new List<string> { RoleNames.DataEntry, RoleNames.EtimadOfficer, RoleNames.Auditer, RoleNames.TechnicalCommitteeUser, RoleNames.OffersCheckSecretary, RoleNames.OffersPurchaseSecretary };

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

