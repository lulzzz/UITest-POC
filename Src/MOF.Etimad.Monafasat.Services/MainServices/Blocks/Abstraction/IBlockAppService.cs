using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IBlockAppService
    {
        Task<SupplierBlock> FindBlockByIdAsync(int supplierBlockId);
        Task<QueryResult<SupplierBlockModel>> FindAsyn(BlockSearchCriteria criteria);
        Task<QueryResult<BlockCommitteeModel>> FindBlockCommittee(BlockCommitteeSearchCriteria criteria);
        Task<QueryResult<UserProfileModel>> FindListUsers(BlockCommitteeSearchCriteria criteria);
        Task<List<SupplierAgencyBlockModel>> GetAllSupplierBlocks(string agencyCode, List<string> CRs);
        Task<List<SupplierAgencyBlockModel>> GetAllBlockedSuppliers(string agencyCode, List<string> CRs);
        Task<SupplierBlock> AddBlockAsyn(SupplierBlock block);
        Task<SupplierBlock> UpdateBlockAsync(SupplierBlockModel blockModel);
        Task<SupplierBlock> DeactivateBlockAsyn(int blockId);
        Task<SupplierBlock> ResetSupplier(int supplierBlockId);
        Task<bool> CheckifSupplierBlockedByCrNo(string commericalRegisterNo, string AgencyCode = null);
        Task<SupplierBlockModel> FindBlockById(int supplierBlockId);
        Task<GovAgency> GetAgency(string agencyCode);
        Task<QueryResult<SupplierIntegrationModel>> GetAllSuppliers(SupplierIntegrationSearchCriteria searchCriteria);
        Task<QueryResult<SupplierBlockModel>> GetAdminBlockList(BlockSearchCriteria searchCriteria);
        Task<SupplierBlock> AddAdminBlock(SupplierBlockModel blockModel);
        Task<SupplierBlock> AddSecretaryBlock(SupplierBlockModel blockModel);
        Task<SupplierBlock> ManagerRejectionReason(int supplierBlockId, string reason);
        Task<SupplierBlock> SecretaryRejectionReason(int supplierBlockId, string reason);
        Task<SupplierBlock> ManagerApproval(int supplierBlockId, string verificationCode);
        Task<SupplierBlock> SecretaryApproval(int supplierBlockId);
        Task<QueryResult<SupplierBlockModel>> GetManagerBlockList(BlockSearchCriteria searchCriteria);
        Task<QueryResult<SupplierBlockModel>> GetSecretaryBlockList(BlockSearchCriteria searchCriteria);
        Task<QueryResult<SupplierBlockModel>> GetBlockingListDetails(BlockSearchCriteria searchCriteria);
        Task<QueryResult<SupplierBlockModel>> GetAdminBlockingListDetails(BlockSearchCriteria searchCriteria);
    }
}
