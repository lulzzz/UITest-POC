using MOF.Etimad.Monafasat.SharedKernal;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class UsersSearchCriteriaModel : SearchCriteria
    {
        public UsersSearchCriteriaModel()
        {
            RoleNames = new List<string>();
        }
        public string MobileNumber { get; set; }
        public string Name { get; set; }
        public List<string> NationalIds { get; set; } = new List<string>();
        public string NationalId { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public List<string> RoleNames { get; set; }
        public int? RoleId { get; set; }
        public string NotAssignedOnlyUserFlag { get; set; }

        public string AgencyId { get; set; }
        public string BranchId { get; set; }
        public int CommitteeId { get; set; }
        public string createdBy { get; set; }
        public string approvedBy { get; set; }

        public string CrNumber { get; set; }
        public string VendorId { get; set; }

        public bool isSemiGovAgency { get; set; }

        public bool isVRO { get; set; }
        public bool isGovVendor { get; set; }
        public int AgencyType { get; set; }

        public List<int> AssignedUserIds { get; set; }
        public int categoryId { get; set; }
    }

    public class IDMUsersSearchCriteriaModel : SearchCriteria
    {
        public string AgencyCode { get; set; }

        public string MobileNumber { get; set; }

        public string NationalId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string RoleName { get; set; }
    }
    public class CommercialRegistrationUsersSearchCriteriaModel : SearchCriteria
    {
        public string CrNumber { get; set; }

        public string RoleName { get; set; }
        public string NotAssignedOnlyUserFlag { get; set; }

        public string AgencyCode { get; set; }
        public string BranchId { get; set; }
        public string OnlyMonafasat { get; set; }
        public List<string> ExcludedNationalIds { get; set; }
    }
}
