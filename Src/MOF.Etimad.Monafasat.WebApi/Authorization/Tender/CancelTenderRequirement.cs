using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    public class CancelTenderRequirement : IAuthorizationRequirement { }

    public class CancelTenderHandler : AuthorizationHandler<CancelTenderRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public CancelTenderHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CancelTenderRequirement requirement)
        {
            var authFilterCtx = httpContextAccessor;
            var newRoles = authFilterCtx.HttpContext.Request.Headers["NewRoles"].ToString();
            var userRoles = newRoles.Length > 0 ? newRoles.Substring(0, newRoles.Length - 1).Split(",").ToList() : new List<string>();
            List<string> accessedRoles = new List<string> { RoleNames.DataEntry, RoleNames.Auditer, RoleNames.supplier, RoleNames.OffersOpeningAndCheckManager, RoleNames.OffersOpeningAndCheckSecretary, RoleNames.OffersPurchaseManager, RoleNames.OffersPurchaseSecretary, RoleNames.OffersOppeningManager, RoleNames.OffersOppeningSecretary, RoleNames.OffersCheckSecretary, RoleNames.OffersCheckManager, RoleNames.MonafasatAdmin, RoleNames.MonafasatAccountManager, RoleNames.TechnicalCommitteeUser, RoleNames.PreQualificationCommitteeManager, RoleNames.PreQualificationCommitteeSecretary, RoleNames.PurshaseSpecialist, RoleNames.EtimadOfficer };

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
