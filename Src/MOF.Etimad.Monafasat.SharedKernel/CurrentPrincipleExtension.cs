using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.SharedKernel
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
            if (UserIsSemiGovAgency(identity) && UserRoles(identity).Contains(RoleNames.supplier))
            {
                return "";
            }
            else if (UserCategory(identity) == 8 && UserRoles(identity).Contains(RoleNames.supplier))
            {
                return "";
            }
            else if (UserIsSemiGovAgency(identity))
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.SelectedCR);
                return (claim != null) ? claim.Value.Split(',')[0] : string.Empty;
            }
            else if (UserCategory(identity) == 8)
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.Vendor);
                return (claim != null) ? claim.Value.Split(',')[0] : string.Empty;
            }
            else if (UserCategory(identity) == (int)IDMUserCategory.VRO)
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.vrooffice);
                return (claim != null) ? claim.Value : string.Empty;
            }
            else
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.SelectedGovAgency);
                return (claim != null) ? claim.Value.Split(',')[1] : string.Empty;
            }
        }

        public static string RelatedVRoCode(this ClaimsPrincipal identity)
        {
            string vroCode = "";
            if (identity.UserIsRelatedVRO())
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.vrooffice);
                return (claim != null) ? claim.Value : string.Empty;
            }
            return vroCode;
        }
        public static string AgencyLogo(this ClaimsPrincipal identity, string baseUrl)
        {

            if (UserIsSemiGovAgency(identity))
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.SelectedGovAgency);
                return (claim != null && !string.IsNullOrEmpty(claim.Value.Split(',').LastOrDefault())) ? baseUrl + claim.Value.Split(',').LastOrDefault() : "/Etimad-UI/assets/imgs/capitol-building.png";
            }
            else if (UserCategory(identity) == 8)
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.Vendor);
                return (claim != null && claim.Value.Split(',').Length > 2 && !string.IsNullOrEmpty(claim.Value.Split(',').LastOrDefault())) ? claim.Value.Split(',').LastOrDefault() : "/Etimad-UI/assets/imgs/capitol-building.png";
            }
            else
            {
                Claim claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.SelectedGovAgency);
                return (claim != null && !string.IsNullOrEmpty(claim.Value.Split(',').LastOrDefault())) ? baseUrl + ((claim.Value.Split(',').LastOrDefault()).StartsWith('/') ? (claim.Value.Split(',').LastOrDefault()).Substring(1) : (claim.Value.Split(',').LastOrDefault())) : "/Etimad-UI/assets/imgs/capitol-building.png";
            }
        }

        public static string SupplierAgency(this ClaimsPrincipal identity)
        {
            if (UserIsSemiGovAgency(identity))
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.SelectedCR);
                return (claim != null) ? claim.Value.Split(',')[0] : string.Empty;
            }
            else if (UserCategory(identity) == 8)
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.Vendor);
                return (claim != null) ? claim.Value.Split(',')[0] : string.Empty;
            }
            else if (UserCategory(identity) == (int)IDMUserCategory.VRO)
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.vrooffice);
                return (claim != null) ? claim.Value : string.Empty;
            }
            else
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.SelectedGovAgency);
                return (claim != null) ? claim.Value.Split(',')[1] : string.Empty;
            }
        }
        public static string SemiGovAndServiceGovUserAgency(this ClaimsPrincipal identity)
        {
            if (UserIsSemiGovAgency(identity))
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.SelectedCR);
                return (claim != null) ? claim.Value.Split(',')[0] : string.Empty;
            }
            else if (UserCategory(identity) == 8)
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
        public static string SupplierNumber(this ClaimsPrincipal identity)
        {
            if (UserCategory(identity) == (int)IDMUserCategory.GovVendor)
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
        public static string UserRole(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.Where(x => x.Type == ClaimTypes.Role || x.Type == IdentityConfigs.Claims.Role).Select(c => c.Value).FirstOrDefault();
            return claim ?? "";
        }
        public static List<string> UserRoles(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.Where(x => x.Type == ClaimTypes.Role || x.Type == IdentityConfigs.Claims.Role).Select(c => c.Value).ToList();
            return claim ?? new List<string>();
        }

        public static List<string> VROUserRoles(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.Where(x => x.Type == IdentityConfigs.Claims.VRORole).Select(c => c.Value).ToList();
            return claim ?? new List<string>();
        }

        public static string SupplierName(this ClaimsPrincipal identity)
        {
            if (UserCategory(identity) == (int)IDMUserCategory.GovVendor)
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
            else if (UserCategory(identity) == (int)IDMUserCategory.GovVendor)
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
        public static string FullName(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.FullName);
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string UserName(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.UserName);
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string UserCategoryID(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.AgencyTypeId);
            return (claim != null) ? claim.Value : "0";
        }

        public static string Email(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.Email);
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string Mobile(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.Mobile);
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static bool IsArabic(this HttpContext httpContext)
        {
            return !httpContext.Request.Headers.ContainsKey("language") || httpContext.Request.Headers["language"] == "ar-SA";
        }
        public static bool UserIsSemiGovAgency(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.isSemiGovAgency);
            return claim != null && !string.IsNullOrEmpty(claim.Value) && claim.Value == "1";


        }

        public static bool UserIsVRO(this ClaimsPrincipal identity)
        {
            return identity.UserCategory() == (int)IDMUserCategory.VRO;
        }

        public static bool UserIsRelatedVRO(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.isrelatedvro);
            if (identity.UserCategory() != (int)IDMUserCategory.VRO)
                return claim != null && !string.IsNullOrEmpty(claim.Value) && claim.Value == "True";
            else return false;

        }

        public static int UserBranch(this ClaimsPrincipal identity)
        {
            Claim claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.BranchId);
            return (claim != null && !string.IsNullOrEmpty(claim.Value) && int.TryParse(claim.Value.Split(',')[0], out int result)) ? result : 0;
        }

        public static string NotSubscription(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.NotSubscription);
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static bool IsSubscription(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.IsSubscription);
            return claim != null && !string.IsNullOrEmpty(claim.Value) && claim.Value.ToLower() == "true";
        }

        public static string UserRelatedAgencyCode(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.SelectedGovAgency);
            return (claim != null && !string.IsNullOrEmpty(claim.Value)) ? claim.Value.Split(',')[1] : null;
        }
        public static int SelectedCommittee(this ClaimsPrincipal identity)
        {
            Claim claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.CommitteeId);
            return (claim != null && !string.IsNullOrEmpty(claim.Value) && int.TryParse(claim.Value.Split(',')[0], out int result)) ? result : 0;
        }
        public static bool IsSemiGovAgency(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.isSemiGovAgency);
            return claim != null && !string.IsNullOrEmpty(claim.Value) && claim.Value == "1";
        }
        public static int UserCategory(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.UserCategory);
            return (claim != null && int.TryParse(claim.Value, out int result)) ? result : 0;
        }
        public static List<string> GetAllSemiGovAgencyUserRoles(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.Where(x => x.Type == IdentityConfigs.Claims.SemiGovRole).Select(c => c.Value).ToList();
            return claim ?? new List<string>();
        }
        public static UserRole GetUserType(this ClaimsPrincipal identity)
        {
            Claim claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.Role);
            string roleName = (claim != null) ? claim.Value : string.Empty;
            if (string.IsNullOrEmpty(roleName))
                return Enums.UserRole.Anonymous;
            if (System.Enum.TryParse(roleName, true, out UserRole type))
                return type;
            return Enums.UserRole.Anonymous;
        }
        public static string UserVendorId(this ClaimsPrincipal identity)
        {
            Claim claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.Vendor);
            return (claim != null && !string.IsNullOrEmpty(claim.Value)) ? claim.Value : string.Empty;
        }
        public static bool UserIsGovVendor(this ClaimsPrincipal identity)
        {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.UserCategory);
            return claim != null && !string.IsNullOrEmpty(claim.Value) && claim.Value == "8";
        }
        public static List<string> GetUserRolesList(this ClaimsPrincipal user)
        {
            return user.Claims.Where(c => c.Type == "UserRoleList").Select(c => c.Value).ToList();
        }
    }
}
