using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    public class CheckTenderOffersRequirement : IAuthorizationRequirement { }

    public class CheckTenderOffersHandler : AuthorizationHandler<CheckTenderOffersRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public CheckTenderOffersHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CheckTenderOffersRequirement requirement)
        {
            var authFilterCtx = httpContextAccessor;
            var newRoles = authFilterCtx.HttpContext.Request.Headers["NewRoles"].ToString();
            var userRoles = newRoles.Length > 0 ? newRoles.Substring(0, newRoles.Length - 1).Split(",").ToList() : new List<string>();
            List<string> accessedRoles = new List<string> { RoleNames.OffersCheckManager, RoleNames.OffersCheckSecretary,
                RoleNames.OffersPurchaseManager, RoleNames.OffersPurchaseSecretary, RoleNames.ApproveTenderAward,
                RoleNames.OffersOpeningAndCheckManager, RoleNames.OffersOpeningAndCheckSecretary, RoleNames.OffersOppeningManager, RoleNames.OffersOppeningSecretary};

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
