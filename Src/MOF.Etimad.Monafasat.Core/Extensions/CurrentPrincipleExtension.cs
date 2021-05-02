

using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;

namespace MOF.Etimad.Monafasat.Core
{
    public static class CurrentPrincipleExtension
    {
        public static int UserId(this ClaimsPrincipal identity)
        {
            var user = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.NameIdentifier.ToLower());
            if (user == null)
                return -1;
            return int.Parse(user.Value);
        }
        public static string UserAgency(this ClaimsPrincipal identity)
        {
            if (UserIsSemiGovAgency(identity))
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.SelectedCR);
                return (claim != null) ? claim.Value.Split(',')[0] : string.Empty;

            }
            else if (UserCategory(identity) == "8")
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.Vendor);
                return (claim != null) ? claim.Value.Split(',')[0] : string.Empty;
            }
            else
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.SelectedGovAgency);
                return (claim != null) ? claim.Value.Split(',')[1] : string.Empty;
            }
        }
        public static string UserCR(this ClaimsPrincipal identity)
        {
            if (UserIsSemiGovAgency(identity))
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.SelectedCR);
                return (claim != null) ? claim.Value.Split(',')[0] : string.Empty;
            }
            else if (UserCategory(identity) == "8")
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.Vendor);
                return (claim != null) ? claim.Value.Split(',')[0] : string.Empty;
            }
            else
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.SelectedCR);
                return (claim != null) ? claim.Value.Split(',')[0] : string.Empty;
            }
        }        
        public static List<string> UserRoles(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.Where(x => x.Type == ClaimTypes.Role || x.Type == IdentityConfigs.Claims.Role).Select(c => c.Value).ToList();
            return claim ?? new List<string>();
        }
        public static string UserCRName(this ClaimsPrincipal identity)
        {
            if (UserIsSemiGovAgency(identity))
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.SelectedCR);
                return (claim != null) ? claim.Value.Split(',')[1] : string.Empty;
            }
            else if (UserCategory(identity) == "8")
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.Vendor);
                return (claim != null) ? claim.Value.Split(',')[1] : string.Empty;
            }
            else
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.SelectedCR);
                return (claim != null) ? claim.Value.Split(',')[1] : string.Empty;
            }

        }
        public static string UserAgencyName(this ClaimsPrincipal identity)
        {
            if (UserIsSemiGovAgency(identity))
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.SelectedCR);
                return (claim != null) ? claim.Value.Split(',')[1] : string.Empty;
            }
            else if (UserCategory(identity) == "8")
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.Vendor);
                return (claim != null) ? claim.Value.Split(',')[1] : string.Empty;
            }
            else
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.SelectedGovAgency);
                return (claim != null) ? claim.Value.Split(',')[2] : string.Empty;
            }
        }
        public static string UserGivenName(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.FullName);
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string User_UserName(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.UserName);
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string UserEmail(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.Email);
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string UserMobile(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.Mobile);
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static bool IsArabic(this HttpContext httpContext)
        {
            return !httpContext.Request.Headers.ContainsKey("language") || httpContext.Request.Headers["language"] == "ar-SA";
        }
        private static bool UserIsSemiGovAgency(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.isSemiGovAgency);
            return claim != null && !string.IsNullOrEmpty(claim.Value) && claim.Value == "1";
        }
        private static string UserCategory(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == "userCategory");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static int UserBranch(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).Claims.FirstOrDefault(x => x.Type == "BranchId");
            int result;
            return (claim != null && !string.IsNullOrEmpty(claim.Value) && int.TryParse(claim.Value.Split(',')[0], out result)) ? result : 0;
        }
    }
}
