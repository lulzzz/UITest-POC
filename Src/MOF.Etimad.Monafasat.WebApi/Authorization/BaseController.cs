using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;

namespace MOF.Etimad.Monafasat.WebApi.Infrastructure
{
    public class BaseController : Controller
    {
        protected RootConfigurations _rootConfiguration;
        public BaseController(IOptionsSnapshot<RootConfigurations> rootConfiguration)
        {
            _rootConfiguration = rootConfiguration.Value;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var newRoles = HttpContext.Request.Headers["NewRoles"].ToString();
            var govAgencyRoles = HttpContext.Request.Headers["GovAgencyRoles"].ToString();
            if (!string.IsNullOrEmpty(newRoles))
            {
                var newRolesList = newRoles.Length > 0 ? newRoles.Substring(0, newRoles.Length - 1).Split(",").ToList() : new List<string>();
                IdentityConfigs.UpdateUserRoles(HttpContext.Request.HttpContext.User, newRolesList);
            }
            if (!string.IsNullOrEmpty(govAgencyRoles))
            {
                var newRolesList = govAgencyRoles.Substring(0, govAgencyRoles.Length - 1).Split(",").ToList();
                IdentityConfigs.UpdateSemiGovAgenyUserRoles(HttpContext.Request.HttpContext.User, newRolesList);
            }
            var branchId = HttpContext.Request.Headers["BranchId"].ToString();
            var committeeId = HttpContext.Request.Headers["CommitteeId"].ToString();
            if (!string.IsNullOrEmpty(committeeId) && committeeId != "0")
            {
                IdentityConfigs.AddUserCommittee(HttpContext.Request.HttpContext.User, committeeId);
            }
            if (!string.IsNullOrEmpty(branchId) && branchId != "0")
            {
                IdentityConfigs.AddUserBranchId(HttpContext.Request.HttpContext.User, branchId);
            }
            if (User.Identity.IsAuthenticated)
            {
                var user = User.FullName();
                var role = User.UserRoles().FirstOrDefault();
                var userId = User.UserId();
                var identity = new ClaimsIdentity();
                identity.AddClaim(new Claim(ClaimTypes.Name, user));
                if (!string.IsNullOrEmpty(role))
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                identity.AddClaim(new Claim("UserId", userId.ToString()));
                if (Thread.CurrentPrincipal == null || user != Thread.CurrentPrincipal.Identity.Name)
                    Thread.CurrentPrincipal = new ClaimsPrincipal(identity);
            }
            else
            {
                var identity = new ClaimsIdentity();
                identity.AddClaim(new Claim(ClaimTypes.Name, "Anonymouth"));
                if (Thread.CurrentPrincipal == null)
                    Thread.CurrentPrincipal = new ClaimsPrincipal(identity);
            }
            base.OnActionExecuting(context);
        }
    }
}