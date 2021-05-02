using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface ISupplierService
    {
        Task<QueryResult<SupplierCombinedModelView>> GetCombinedSuppliersByOfferId(CombinedSearchCretriaModel cretria);
        Task<QueryResult<SupplierCombinedModelView>> GetAllSuppliersBySearchCretriaForCombined(SupplierSearchCretriaModel cretria, string CR);

        Task<QueryResult<SupplierInvitationModel>> GetAllSuppliers(SupplierSearchCretriaModel cretria);
        Task<SupplierInvitationModel> GetSupplierByName(string CommericalRegisterNo);
        Task<QueryResult<InvitationCrModel>> GetInvitedSuppliers(SupplierSearchCretriaModel cretria);
        Task<QueryResult<InvitationCrModel>> GetInvitedUnRegisterSuppliers(SupplierSearchCretriaModel cretria);
        Task<QueryResult<InvitationCrModel>> GetInvitedUnRegisterSuppliersForCreation(SupplierSearchCretriaModel cretria);
        Task<QueryResult<SolidarityInvitedRegisteredSupplierModel>> GetSolidarityInvitedSuppliers(SolidaritySearchCriteria cretria);
        Task<QueryResult<SolidarityInvitedUnRegisteredSupplierModel>> GetInvitedUnregisteredSuppliers(SolidarityUnregisteredSearchCriteria cretria);
        Task<QueryResult<SupplierModel>> GetSuppliersBySearchCretria(SupplierSearchCretriaModel cretria);
        Task<List<FavouriteSupplierListModel>> GetFavouriteListsByAgencyBranchId(string agencyCode, int BranchId);
        Task<FavouriteSupplierListModel> AddFavouriteSupplierList(FavouriteSupplierListModel favouriteSupplierList);
        Task<bool> DeleteFavouriteSupplierList(FavouriteSupplierListModel favouriteSupplierList);
        Task<QueryResult<SupplierInvitationModel>> GetFavouriteSuppliersByListId(SupplierSearchCretriaModel cretria);
        Task<bool> AddSupplierToFavouriteList(SupplierSearchCretriaModel cretria);
        Task<bool> DeleteSupplierFromFavouriteList(SupplierSearchCretriaModel cretria);
        Task<List<SupplierModel>> GetAllSuppliersData();

        #region [Supplier Info From Yesser]

        Task<SupplierInfoStatusModel> ValidateMCICRAndGetInfo(string CR);
        Task<SupplierInfoStatusModel> GOSICertificateInquiry(string GOSIId);
        Task<SupplierInfoStatusModel> LicenseStatusInquiry(string licenseNumber);
        Task<SupplierInfoStatusModel> ContractorDetailsInquiry(string partyNumberId);
        Task<SupplierInfoStatusModel> ZakatCertificateInquiryByCR(string CR);
        Task<SupplierInfoStatusModel> GetCOCSubscriptionBySijilNumber(string LicenseNumber, string CityCode);
        Task<SupplierInfoStatusModel> NitaqatInformationInquiry(string EstablishmentLaborOfficeId, string EstablishmentSequenceNumber);
        #endregion

        Task<QueryResult<InvitationCrModel>> GetAllSuppliersBySearchCretriaForInvitation(SupplierSearchCretriaModel cretria);
        Task<QueryResult<SolidarityInvitedRegisteredSupplierModel>> GetAllSuppliersBySearchCretriaForSolidarity(SolidaritySearchCriteria cretria);
        Task<int> GetSuppliersCountFromIDM();

        #region Invitation
        Task<QueryResult<InvitationCrModel>> GetInvitedSuppliersForInvitationInTenderCreation(SupplierSearchCretriaModel cretria);
        Task<QueryResult<InvitationCrModel>> GetQualifiedSuppliers(TenderIdSearchCretriaModel cretria);
        Task<QueryResult<InvitationCrModel>> GetAcceptedSupplierInFirstStageTender(TenderIdSearchCretriaModel cretria);
        Task<QueryResult<InvitationCrModel>> GetAnnouncementTemplateSuppliers(AnnouncementSupplierTemplateInvitationSearchmodel cretria);
        Task<QueryResult<SupplierInvitationModel>> GetAllSuppliersBySearchCretriaForInvitations(SupplierSearchCretriaModel cretria);
        Task<QueryResult<string>> GetEmailsForUnregisteredSuppliers(SupplierSearchCretriaModel cretria);
        Task<QueryResult<string>> GetMobileForUnregisteredSuppliers(SupplierSearchCretriaModel cretria);

        #endregion
        Task<QueryResult<UnRegisterSupplierInvitationModel>> GetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovement(SupplierSearchCretriaModel cretria);
        Task<QueryResult<SupplierInvitationModel>> GetInvitedSuppliersForInvitationAfterTenderApprovement(SupplierSearchCretriaModel cretria);

    }
}
