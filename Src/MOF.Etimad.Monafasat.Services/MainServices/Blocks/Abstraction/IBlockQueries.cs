using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IBlockQueries
    {
        Task<SupplierBlock> FindBlockByIdAsync(int supplierBlockId);
        Task<QueryResult<SupplierBlockModel>> Find(BlockSearchCriteria criteria);
        Task<QueryResult<SupplierBlockModel>> FindBlockedUser(BlockSearchCriteria criteria);
        Task<List<SupplierAgencyBlockModel>> GetAllCurrentBlockedSuppliers(string agencyCode, List<string> CRs);
        Task<List<SupplierAgencyBlockModel>> GetAllBlockedSuppliers(string agencyCode, List<string> CRs);
        Task<QueryResult<BlockCommitteeModel>> FindBlockCommittee(BlockCommitteeSearchCriteria criteria);
        Task<bool> CheckifSupplierBlockedByCrNo(string commericalRegisterNo, string AgencyCode = null);
        Task<SupplierBlockModel> FindBlockById(int supplierBlockId);
        Task<QueryResult<SupplierBlockModel>> FindAdminBlockList(BlockSearchCriteria criteria);
        SupplierBlock FindSupplierBlockById(int supplierBlockId);
        Task<GovAgency> GetAgencyById(string agencyId);
        Task<QueryResult<SupplierBlockModel>> FindManagerBlockList(BlockSearchCriteria criteria);
        Task<QueryResult<SupplierBlockModel>> FindSecretaryBlockList(BlockSearchCriteria criteria);
        Task<QueryResult<SupplierBlockModel>> GetBlockingListDetails(BlockSearchCriteria criteria);
        Task<QueryResult<SupplierBlockModel>> GetAdminBlockingListDetails(BlockSearchCriteria criteria);
    }
}
