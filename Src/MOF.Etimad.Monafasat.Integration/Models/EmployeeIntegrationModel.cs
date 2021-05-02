using MOF.Etimad.Monafasat.SharedKernal;
using System.Collections.Generic;
namespace MOF.Etimad.Monafasat.Integration
{
    public class EmployeeIntegrationModel : SearchCriteria
    {
        public EmployeeIntegrationModel()
        {
            agencies = new List<AgencyModel>();
            CRs = new List<AgencyModel>();
            vendors = new List<AgencyModel>();
        }
        public int userId { get; set; }
        public string crNumber { get; set; }
        public string nationalId { get; set; }
        public string email { get; set; }
        public string dateOfBirth { get; set; }
        public string mobileNumber { get; set; }
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
        public List<IDMRolesModel> roles { get; set; }
    }

    public class AgencyModel
    {
        public string agencyCode { get; set; }
        public string agencyName { get; set; }
    }
}
