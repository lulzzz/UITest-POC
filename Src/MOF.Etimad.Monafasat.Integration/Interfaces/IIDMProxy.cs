using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Integration
{
    public interface IIDMProxy
    {
        Task<SupplierFullDataModel> GetSupplierInfoByCR(string supplierNumber);
        Task<QueryResult<SupplierIntegrationModel>> GetSuppliersBySearchCriteria(SupplierIntegrationSearchCriteria searchCriteria);
        Task<AgencyInfoModel> GetAgencyDetailsRelatedToSadad(string agencyCode, int agencyType);
        Task<Dictionary<string, string>> GetAgencyLogos(List<string> agencyCodes);
        Task<QueryResult<EmployeeIntegrationModel>> GetMonafasatUsersByAgencyType(UsersSearchCriteriaModel searchCriteria);
        Task<QueryResult<EmployeeIntegrationModel>> GetMonafasatUsersByAgencyTypeAndRoleName(UsersSearchCriteriaModel searchCriteria);
        Task<QueryResult<ContactOfficerModel>> GetContactOfficersByCR(List<string> supplierNumbers);
        Task<List<EmployeeIntegrationModel>> GetEmployeeDetailsByRoleName(string roleName);

        Task<UserAPIModel> GetUserbyUserName(string userName);
    }
}
