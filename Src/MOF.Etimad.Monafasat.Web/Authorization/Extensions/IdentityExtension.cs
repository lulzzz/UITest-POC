using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web
{
   public static class IdentityExtension
   {
      public static string UserId(this ClaimsPrincipal identity)
      {
         var claim = identity.Claims.FirstOrDefault(x => x.Type.ToLower() == IdentityConfigs.Claims.NameIdentifier.ToLower());
         if(claim == null)
            claim = identity.Claims.FirstOrDefault(x => x.Type.ToLower() == ClaimTypes.NameIdentifier.ToLower());
         return (claim != null) ? claim.Value : string.Empty;
      }
      public static string UserRole(this ClaimsPrincipal identity)
      {
         var claim = identity.Claims.FirstOrDefault(x => x.Type == "role" || x.Type == ClaimTypes.Role);
         return (claim != null) ? claim.Value : string.Empty;
      }
      public static bool IsSemiGovAgency(this ClaimsPrincipal identity)
      {
         var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.isSemiGovAgency);
         return claim != null && !string.IsNullOrEmpty(claim.Value) && claim.Value == "1";
      }

      public static string UserEmail(this ClaimsPrincipal identity)
      {
         var claim = identity.Claims.FirstOrDefault(x => x.Type == "email");
         return (claim != null) ? claim.Value : string.Empty;
      }

      public static string UserCR(this ClaimsPrincipal identity)
      {
         if (IsSemiGovAgency(identity))
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

      public static string UserCategory(this ClaimsPrincipal identity)
      {
         var claim = identity.Claims.FirstOrDefault(x => x.Type == "userCategory");
         return (claim != null) ? claim.Value : string.Empty;
      }

      public static string UserAgency(this ClaimsPrincipal identity)
      {
         if (IsSemiGovAgency(identity))
         {
            var claim = identity.Claims.FirstOrDefault(x => x.Type == IdentityConfigs.Claims.SelectedCR);
            return (claim != null) ? claim.Value.Split(',')[0] : string.Empty;

         }
         else if(UserCategory(identity)=="8")
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
      public static string UserAgencyName(this ClaimsPrincipal identity)
      {
         var claim = identity.Claims.FirstOrDefault(x => x.Type == "selectedAgency");
         return (claim != null) ? claim.Value.Split(',')[2] : string.Empty;
      }
      public static List<string> UserRoles(this ClaimsPrincipal identity)
      {
         var claim = identity.Claims.Where(x => x.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
         return (claim != null) ? claim : new List<string>();
      }
      public static List<string> GetAllSemiGovAgencyUserRoles(this ClaimsPrincipal identity)
      {
         var claim = identity.Claims.Where(x => x.Type == "SemiGovRole").Select(c => c.Value).ToList();
         return (claim != null) ? claim : new List<string>();
      }
      public static string UserBranchId(this ClaimsPrincipal identity)
      {
         var claim = identity.Claims.FirstOrDefault(x => x.Type == "BranchId");
         // Test for null to avoid issues during local testing
         return (claim != null && !string.IsNullOrEmpty(claim.Value)) ? claim.Value.Split(',')[0] : null;
      }
      public static string SelectedCommitteeId(this ClaimsPrincipal identity)
      {
         var claim = identity.Claims.FirstOrDefault(x => x.Type == "CommitteeId");
         // Test for null to avoid issues during local testing
         return (claim != null && !string.IsNullOrEmpty(claim.Value)) ? claim.Value.Split(',')[0] : null;
      }
      public static string IsBranch(this ClaimsPrincipal identity)
      {
         var claim = identity.Claims.FirstOrDefault(x => x.Type == "IsBranch");
         // Test for null to avoid issues during local testing
         return (claim != null && !string.IsNullOrEmpty(claim.Value)) ? claim.Value.Split(',')[0] : null;
      }
      public static string UserMobile(this ClaimsPrincipal identity)
      {
         var claim = identity.Claims.FirstOrDefault(x => x.Type == "mobile");
         return (claim != null) ? claim.Value : string.Empty;
      }

      public static string UserFullName(this ClaimsPrincipal identity)
      {
         var claim = identity.Claims.FirstOrDefault(x => x.Type == "fullname");
         return (claim != null) ? claim.Value : string.Empty;
      }

      public static string UserGivenName(this ClaimsPrincipal identity)
      {
         var claim = identity.Claims.FirstOrDefault(x => x.Type == "given_name");
         return (claim != null) ? claim.Value : string.Empty;
      }

      //public static string CommitteeId(this ClaimsPrincipal identity)
      //{
      //   var claim = identity.Claims.FirstOrDefault(x => x.Type == "committeeId");
      //   return (claim != null) ? claim.Value : string.Empty;
      //}
      public static bool IsAnonymous(this ClaimsPrincipal identity)
      {
         return !identity.Claims.Any(x => x.Type == "role" || x.Type == ClaimTypes.Role);
      }

   }

}
