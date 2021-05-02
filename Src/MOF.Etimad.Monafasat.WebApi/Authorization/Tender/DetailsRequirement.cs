using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    public class DetailsRequirement : IAuthorizationRequirement { }

    public class DetailsHandler : AuthorizationHandler<DetailsRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public DetailsHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DetailsRequirement requirement)
        {
            var authFilterCtx = httpContextAccessor;
            var newRoles = authFilterCtx.HttpContext.Request.Headers["NewRoles"].ToString();
            var userRoles = newRoles.Length > 0 ? newRoles.Substring(0, newRoles.Length - 1).Split(",").ToList() : new List<string>();
            List<string> accessedRoles = new List<string> { RoleNames.PurshaseSpecialist, RoleNames.EtimadOfficer, RoleNames.DataEntry, RoleNames.OffersCheckSecretary, RoleNames.OffersPurchaseSecretary, RoleNames.Auditer, RoleNames.supplier, RoleNames.OffersOppeningManager, RoleNames.OffersOppeningSecretary, RoleNames.OffersCheckSecretary, RoleNames.OffersCheckManager, RoleNames.MonafasatAdmin, RoleNames.MonafasatAccountManager, RoleNames.TechnicalCommitteeUser, RoleNames.CustomerService, RoleNames.UnitManagerUser, RoleNames.UnitSpecialistLevel1, RoleNames.UnitSpecialistLevel2 };
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
