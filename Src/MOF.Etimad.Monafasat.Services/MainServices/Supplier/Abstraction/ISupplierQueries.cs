using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface ISupplierQueries
    {
        Task<QueryResult<SupplierCombinedModelView>> FindSuppliers(CombinedSearchCretriaModel cretria);
        Task<FavouriteSupplierList> FindFavouriteList(int id);
        Task<FavouriteSupplierList> FindFavouriteListByName(string Name, int BranchId = 0);
        Task<Tender> GetTenderByTenderId(int tenderId);


        Task<FavouriteSupplier> FindFavouriteSupplierByListId(SupplierSearchCretriaModel cretria);
        Task<List<FavouriteSupplierListModel>> GetFavouriteListsByAgencyBranchId(string agencyCode, int BranchId);
        Task<QueryResult<SupplierInvitationModel>> GetFavouriteSuppliersByListId(SupplierSearchCretriaModel cretria);
        Task<Supplier> FindSupplierByCRNumber(string selectedCr);
        Task<QueryResult<SolidarityInvitedUnRegisteredSupplierModel>> GetSolidarityInvitedUnregisteredSuppliers(SolidarityUnregisteredSearchCriteria cretria);
        Task<QueryResult<SolidarityInvitedRegisteredSupplierModel>> GetSolidarityInvitedSuppliers(SolidaritySearchCriteria cretria);
        Task<QueryResult<InvitationCrModel>> GetInvitedSuppliers(SupplierSearchCretriaModel cretria);

        Task<QueryResult<InvitationCrModel>> GetInvitedUnRegisterSuppliers(SupplierSearchCretriaModel cretria);

        Task<QueryResult<InvitationCrModel>> GetInvitedUnRegisterSuppliersForCreation(SupplierSearchCretriaModel cretria);


        Task<List<SupplierModel>> GetAllSuppliersData();

        #region Invitation 

        Task<QueryResult<InvitationCrModel>> GetInvitedSuppliersForInvitationInTenderCreation(SupplierSearchCretriaModel cretria);
        Task<QueryResult<InvitationCrModel>> GetQualifiedSuppliers(TenderIdSearchCretriaModel cretria);

        Task<QueryResult<InvitationCrModel>> GetAcceptedSupplierInFirstStageTender(TenderIdSearchCretriaModel cretria);
        Task<QueryResult<InvitationCrModel>> GetAnnouncementTemplateSuppliers(AnnouncementSupplierTemplateInvitationSearchmodel cretria);
        Task<QueryResult<string>> GetEmailsForUnregisteredSuppliers(SupplierSearchCretriaModel cretria);

        Task<QueryResult<string>> GetMobileForUnregisteredSuppliers(SupplierSearchCretriaModel cretria);
        Task<QueryResult<SupplierInvitationModel>> GetAllSuppliersBySearchCretriaForInvitations(SupplierSearchCretriaModel cretria);

        #endregion
        Task<QueryResult<UnRegisterSupplierInvitationModel>> GetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovement(SupplierSearchCretriaModel cretria);
        Task<QueryResult<SupplierInvitationModel>> GetInvitedSuppliersForInvitationAfterTenderApprovement(SupplierSearchCretriaModel cretria);

        Task SuppliersInFavourite(SupplierSearchCretriaModel cretria, List<SupplierInvitationModel> supplierInvitationModels);
        Task GetInvitatedSupplier(SupplierSearchCretriaModel cretria, List<InvitationCrModel> supplierInvitationModels);
        Task<QueryResult<SupplierCombinedModelView>> GetAllSuppliersBySearchCretriaForCombinedQuery(SupplierSearchCretriaModel cretria, string CR);


    }
}
