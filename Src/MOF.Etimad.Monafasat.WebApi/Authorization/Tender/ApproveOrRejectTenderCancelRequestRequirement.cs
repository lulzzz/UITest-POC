using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    public class ApproveOrRejectTenderCancelRequestRequirement : IAuthorizationRequirement { }

    public class ApproveOrRejectTenderCancelRequestHandler : AuthorizationHandler<ApproveOrRejectTenderCancelRequestRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public ApproveOrRejectTenderCancelRequestHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApproveOrRejectTenderCancelRequestRequirement requirement)
        {
            var authFilterCtx = httpContextAccessor;
            var newRoles = authFilterCtx.HttpContext.Request.Headers["NewRoles"].ToString();
            var userRoles = newRoles.Length > 0 ? newRoles.Substring(0, newRoles.Length - 1).Split(",").ToList() : new List<string>();
            List<string> accessedRoles = new List<string> { RoleNames.OffersPurchaseSecretary, RoleNames.Auditer, RoleNames.EtimadOfficer, RoleNames.OffersOpeningAndCheckManager, RoleNames.OffersPurchaseManager, RoleNames.OffersOppeningManager, RoleNames.OffersCheckManager, RoleNames.PreQualificationCommitteeManager };
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
