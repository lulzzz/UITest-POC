using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Authorization
{
    public class GetAnnouncementDetailsRequirement : IAuthorizationRequirement { }

    public class GetAnnouncementDetailsHandler : AuthorizationHandler<GetAnnouncementDetailsRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public GetAnnouncementDetailsHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GetAnnouncementDetailsRequirement requirement)
        {
            var authFilterCtx = httpContextAccessor;
            var newRoles = authFilterCtx.HttpContext.Request.Headers["NewRoles"].ToString();
            var newRolesList = newRoles.Length > 0 ? newRoles.Substring(0, newRoles.Length - 1).Split(",").ToList() : new List<string>();
            List<string> accessedRoles = new List<string> { RoleNames.MonafasatAdmin, RoleNames.DataEntry
               // , RoleNames.SupplierPolicy,            RoleNames.supplier
                ,RoleNames.Auditer, RoleNames.OffersOppeningSecretary, RoleNames.OffersOppeningManager, RoleNames.OffersCheckSecretary, RoleNames.FinancialSupervisor,
                RoleNames.OffersCheckManager, RoleNames.MonafasatAccountManager, RoleNames.CustomerService, RoleNames.OffersPurchaseSecretary, RoleNames.OffersPurchaseManager };
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

