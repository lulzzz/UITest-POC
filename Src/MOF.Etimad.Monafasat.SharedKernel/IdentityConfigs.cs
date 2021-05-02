using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MOF.Etimad.Monafasat.SharedKernel
{
    public static class IdentityConfigs
    {
        public static class Claims
        {
            public const string NameIdentifier = "sub";
            public const string UserCategory = "userCategory";
            public const string UserName = "preferred_username";
            public const string FullName = "fullname";
            public const string FirstName = "firstName";
            public const string SecondName = "secondName";
            public const string ThirdName = "thirdName";
            public const string LastName = "lastName";
            public const string Role = "role";
            public const string Agency = "agency";
            public const string Department = "department";
            public const string Section = "section";
            public const string Sector = "sector";
            public const string GovEntity = "govEntity";
            public const string Email = "email";
            public const string Mobile = "mobile";
            public const string SelectedGovAgency = "selectedAgency";
            public const string Vendor = "vendor";
            public const string SelectedCR = "selectedCR";
            public const string CRProfileCreated = "CRProfileCreated";
            public const string rowVersion = "rowVersion";
            public const string isSemiGovAgency = "isSemiGovAgency";
            public const string NotSubscription = "notSubscription";
            public const string IsSubscription = "isSubscription";
            public const string BranchId = "BranchId";
            public const string CommitteeId = "CommitteeId";
            public const string SemiGovRole = "SemiGovRole";
            public const string vrooffice = "vrooffice";
            public const string isrelatedvro = "isrelatedvro";
            public const string AgencyTypeId = "AgencyTypeId";
            public const string VRORole = "VRORole";
        }
        public static void UpdateUserRoles(ClaimsPrincipal user, List<string> newRoles)
        {
            var identity = user.Identity as ClaimsIdentity;
            var claims = (from c in user.Claims
                          where c.Type == ClaimTypes.Role || c.Type == "role"
                          select c).ToList();
            var roleName = ClaimTypes.Role;
            foreach (var claim in claims)
            {
                roleName = claim.Type;
                identity.RemoveClaim(claim);
            }
            foreach (var role in newRoles)
            {
                identity.AddClaim(new Claim(roleName, role));
            }
        }
        public static void UpdateSemiGovAgenyUserRoles(ClaimsPrincipal user, List<string> newRoles)
        {
            var identity = user.Identity as ClaimsIdentity;
            var claims = (from c in user.Claims
                          where c.Type == "SemiGovRole"
                          select c).ToList();
            var roleName = "SemiGovRole";
            foreach (var claim in claims)
            {
                roleName = claim.Type;
                identity.RemoveClaim(claim);
            }
            foreach (var role in newRoles)
            {
                identity.AddClaim(new Claim(roleName, role));
            }
        }
        public static void UpdateUserRolesList(ClaimsPrincipal user, List<AssignedRoleLevelTypeModel> newRolesModel)
        {
            var identity = user.Identity as ClaimsIdentity;
            var claims = (from c in user.Claims where c.Type == "UserRoleList" select c).ToList();
            var roleName = "UserRoleList";
            foreach (var claim in claims)
            {
                roleName = claim.Type;
                identity.RemoveClaim(claim);
            }
            foreach (var role in newRolesModel)
                if (!(role.RoleName == RoleNames.supplier && string.IsNullOrWhiteSpace(user.SupplierNumber())))
                    identity.AddClaim(new Claim(roleName, (int)role.AssignedRoleLevel + "," + role.Id + "," + role.RoleName + "##" + role.DisplayedRoleName));
        }
        public static void ReomveAllUserRoles(ClaimsPrincipal user)
        {
            var identity = user.Identity as ClaimsIdentity;
            var claims = (from c in user.Claims
                          where c.Type == ClaimTypes.Role || c.Type == "role"
                          select c).ToList();
            foreach (var claim in claims)
            {
                identity.RemoveClaim(claim);
            }
        }
        public static void RemoveClaimByName(ClaimsPrincipal user, string claimName)
        {
            var identity = user.Identity as ClaimsIdentity;
            var claims = (from c in user.Claims
                          where c.Type == claimName
                          select c).ToList();
            foreach (var claim in claims)
            {
                identity.RemoveClaim(claim);
            }
        }
        public static void AddUserBranchId(ClaimsPrincipal user, string branchId)
        {
            var identity = user.Identity as ClaimsIdentity;
            identity.AddClaim(new Claim("BranchId", branchId));
        }
        public static void SetNotSubscription(ClaimsPrincipal user, string notSubscription)
        {
            var identity = user.Identity as ClaimsIdentity;
            identity.AddClaim(new Claim(Claims.NotSubscription, notSubscription));
        }
        public static void SetIsSubscription(ClaimsPrincipal user, bool isSubscription)
        {
            var identity = user.Identity as ClaimsIdentity;
            identity.AddClaim(new Claim(Claims.IsSubscription, isSubscription.ToString()));
        }
        public static void AddUserCommittee(ClaimsPrincipal user, string committeeId)
        {
            var identity = user.Identity as ClaimsIdentity;
            identity.AddClaim(new Claim("CommitteeId", committeeId));
        }
        public static void UpdateUserBranchId(ClaimsPrincipal user, int branchId)
        {
            var claims = (from c in user.Claims
                          where c.Type == "BranchId" || c.Type == "CommitteeId"
                          select c).ToList();
            var identity = user.Identity as ClaimsIdentity;
            foreach (var claim in claims)
            {
                identity.RemoveClaim(claim);
            }
            identity.AddClaim(new Claim("BranchId", branchId.ToString()));
            identity.AddClaim(new Claim("CommitteeId", "0"));
        }
        public static void UpdateUserCommittee(ClaimsPrincipal user, int committeeId)
        {
            var claims = (from c in user.Claims
                          where c.Type == "CommitteeId" || c.Type == "BranchId"
                          select c).ToList();
            var identity = user.Identity as ClaimsIdentity;
            foreach (var claim in claims)
            {
                identity.RemoveClaim(claim);
            }
            identity.AddClaim(new Claim("CommitteeId", committeeId.ToString()));
            identity.AddClaim(new Claim("BranchId", "0"));
        }
        public static bool CheckForUserNewRoles(List<string> newRoles, List<string> ActionAccessedRoles)
        {
            bool check = false;
            foreach (var item in ActionAccessedRoles)
            {
                if (newRoles.Contains(item))
                {
                    check = true;
                }
            }
            return check;
        }
    }
}
