using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class ManageUsersAssignationModel //: SearchCriteria
    {
        public ManageUsersAssignationModel()
        {
            agencies = new List<AgencyModel>();
            CRs = new List<AgencyModel>();
            vendors = new List<AgencyModel>();
        }
        public int userId { get; set; }
        public string crNumber { get; set; }
        public string userIdString { get; set; }
        public string nationalId { get; set; }
        public string nationalIdString { get; set; }
        public string email { get; set; }
        public string dateOfBirth { get; set; }
        public string mobileNumber { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.BranchResources.ErrorMessages)), ErrorMessageResourceName = "EnterUserName")]
        public string fullName { get; set; }
        public bool isAdmin { get; set; }

        public bool isAccountManager { get; set; }
        public bool isCSO { get; set; }
        public bool isReportsUser { get; set; }
        public bool isBlackListCommuity { get; set; }
        public string rowVersion { get; set; }
        public List<AgencyModel> agencies { get; set; }
        public List<AgencyModel> CRs { get; set; }
        public List<AgencyModel> vendors { get; set; }
        public string AgencyNames { get; set; }

        public List<RoleModel> roles { get; set; }
        public List<RoleModel> BranchRoles { get; set; }
        public List<RoleModel> CommitteeOpenOfferRoles { get; set; }
        public List<RoleModel> CommitteeAuditOfferRoles { get; set; }
        public List<RoleModel> CommitteeTechnicalRoles { get; set; }
        public List<RoleModel> CommitteeOpenAndCheckRoles { get; set; }
        public List<RoleModel> CommitteePreQualificationRoles { get; set; }
        public List<RoleModel> CommitteePurchaseRoles { get; set; }

        public List<RoleModel> CommitteeBlockRoles { get; set; }
        public List<RoleModel> AllCommitteeRoles { get; set; }
    }


    public class AgencyModel
    {

        public string agencyCode { get; set; }
        public string agencyName { get; set; }
    }
}
