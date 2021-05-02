using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Integration.Enums;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.WebApi
{
    public static class IdentityExtension
    {
        public static string UserId(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == "sub");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string UserRole(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == "role");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static List<string> UserRoles(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.Where(x => x.Type == ClaimTypes.Role || x.Type == "role").Select(c => c.Value).ToList();
            return (claim != null) ? claim : new List<string>();
        }
        public static List<string> GetAllSemiGovAgencyUserRoles(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.Where(x => x.Type == "SemiGovRole").Select(c => c.Value).ToList();
            return (claim != null) ? claim : new List<string>();
        }
        public static UserType GetUserType(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == "role");
            // Test for null to avoid issues during local testing
            var roleName = (claim != null) ? claim.Value : string.Empty;
            if (string.IsNullOrEmpty(roleName))
            {
                return UserType.Anonymous;
            }
            UserType type;
            if (System.Enum.TryParse(roleName, true, out type))
            {
                return type;
            }

            return UserType.Anonymous;
        }


        public static string UserEmail(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == "email");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        //public static string UserCR(this ClaimsPrincipal identity)
        //{
        //    var claim = identity.Claims.FirstOrDefault(x => x.Type == "selectedCR");
        //    // Test for null to avoid issues during local testing
        //    return (claim != null) ? claim.Value.Split(',')[0] : string.Empty;
        //}
        public static string UserCR(this ClaimsPrincipal identity)
        {
            if (UserIsSemiGovAgency(identity))
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.SelectedCR);
                return (claim != null) ? claim.Value.Split(",")[0] : string.Empty;
            }
            else if (UserCategory(identity) == "8")
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.Vendor);
                return (claim != null) ? claim.Value.Split(",")[0] : string.Empty;
            }
            else
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.SelectedCR);
                return (claim != null) ? claim.Value.Split(",")[0] : string.Empty;
            }

        }
        public static string UserCRName(this ClaimsPrincipal identity)
        {
            if (UserIsSemiGovAgency(identity))
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.SelectedCR);
                return (claim != null) ? claim.Value.Split(",")[1] : string.Empty;
            }
            else if (UserCategory(identity) == "8")
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.Vendor);
                return (claim != null) ? claim.Value.Split(",")[1] : string.Empty;
            }
            else
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.SelectedCR);
                return (claim != null) ? claim.Value.Split(",")[1] : string.Empty;
            }

        }
        public static string UserCategory(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == "userCategory");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
        //public static int UserAgency(this ClaimsPrincipal identity)
        //{
        //    var claim = identity.Claims.FirstOrDefault(x => x.Type == "selectedAgency");
        //    // Test for null to avoid issues during local testing 
        //    int result;
        //    return (claim != null && !string.IsNullOrEmpty(claim.Value) && int.TryParse(claim.Value.Split(',')[0], out result)) ? result : 0;
        //}
        public static int UserBranchId(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == "BranchId");
            // Test for null to avoid issues during local testing
            int result;
            //return (claim != null && !string.IsNullOrEmpty(claim.Value)) ? claim.Value.Split(',')[0] : null;
            return (claim != null && !string.IsNullOrEmpty(claim.Value) && int.TryParse(claim.Value.Split(',')[0], out result)) ? result : 0;

        }
        public static int SelectedCommitteeId(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == "CommitteeId");
            // Test for null to avoid issues during local testing
            int result;
            return (claim != null && !string.IsNullOrEmpty(claim.Value) && int.TryParse(claim.Value.Split(',')[0], out result)) ? result : 0;

        }
        //public static string UserAgencyCode(this ClaimsPrincipal identity)
        //{
        //    var claim = identity.Claims.FirstOrDefault(x => x.Type == "selectedAgency");
        //    // Test for null to avoid issues during local testing
        //    return (claim != null && !string.IsNullOrEmpty(claim.Value)) ? claim.Value.Split(',')[1] : null;
        //}
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
        public static string UserVendorId(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.Vendor);
            // Test for null to avoid issues during local testing
            return (claim != null && !string.IsNullOrEmpty(claim.Value)) ? claim.Value : string.Empty;
        }
        public static bool UserIsSemiGovAgency(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.isSemiGovAgency);
            return claim != null && !string.IsNullOrEmpty(claim.Value) && claim.Value == "1";
        }
        public static bool UserIsGovVendor(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.UserCategory);
            return claim != null && !string.IsNullOrEmpty(claim.Value) && claim.Value == ((int)IDMUserCategory.GovVendor).ToString();
        }
        public static string UserMobile(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == "mobile");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string UserFamilyName(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == "family_name");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string UserGivenName(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == "fullname");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

    }
}
