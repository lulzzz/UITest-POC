using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Qualification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IQualificationAppService
    {
        Task<PrequalificationTechnicalExaminationModel> GetPrequalificationTechnicalExamination(int Id);
        Task<Tender> CreatePreQualification(PreQualificationSavingModel model, int userId, string userAgency, int BranchId);
        Task<Tender> SaveDraft(PreQualificationSavingModel model, int userId, string userAgency, int BranchId);
        Task<Tender> SendPreQualificationToApprove(int tenderId, string agencycode);
        Task<Tender> SendQualificationToCommitteeApprove(int tenderId, string agencycode);
        Task<Tender> ApproveQualificationFromQualificationSecritary(int tenderId, string agencycode);
        Task<PreQualificationDetailsModel> GetPreQualificationDetailsModelById(int qualificationId, int BranchId);
        Task<Tender> ApprovePreQualification(int tenderId, string verficationCode, int typeId, string agencyCode);
        Task<Tender> ApprovePreQualificationFromCommitteeManager(int tenderId, string verficationCode, int typeId, string agencyCode);
        Task<Tender> RejectApprovePreQualification(int tenderId, string rejectionReason, string agencyCode);
        Task<Tender> RejectApprovePreQualificationFromCommitteeManager(int tenderId, string rejectionReason, string agencyCode);
        Task<Tender> ReopenPreQualification(int tenderId, string agencyCode);
        Task<Tender> ReopenPreQualificationFromCommitteeSecrtary(int tenderId, string agencyCode);
        Task<TenderDatesModel> FindQualificationDatesByQualificationId(int qualificationId);

        Task<PreQualificationPartialDetailsModel> GetPrequalificationPartialDetails(int qualificationId);

        Task<RegistryReportForQualificationModel> GetQualificationOffersRegistryReportData(int qualificationId);
        Task<QueryResult<PreQualificationCardModel>> FindPreQualificationsModelBySearchCriteria(PreQualificationSearchCriteriaModel criteria);

        Task<QueryResult<QualificationSupplierProjectModel>> FindSupplierProjectModelBySearchCriteria(PreQualificationSearchCriteriaModel criteria);

        Task<QueryResult<PreQualificationCardModel>> FindPreQualificationByCriteriaForUnderOperationsStage(PreQualificationSearchCriteriaModel criteria);
        Task<QueryResult<PreQualificationCardModel>> FindPreQualificationForCheckingStageBySearchCriteria(PreQualificationSearchCriteriaModel criteria);
        Task<PreQualificationBasicDetailsModel> GetPreQualificationDetailsByIdForChecking(int tenderId);
        Task<int> GetPreQualificationStatus(int tenderId);
        Task<QualificationStatusModel> GetPrequalificationDetails(int qualificationId, string agencyCode);
        Task<QualificationStatusModel> GetPrequalificationDetailsForSupplier(int qualificationId, string CR);
        Task<PreQualificationSavingModel> GetPreQualificationEditingData(int tenderId);
        Task<PreQulaificationApprovalModel> GetPreQualificationDetailsByPreQualificationId(int Id, string agencyCode);
        Task<QueryResult<Tender>> FindCheckedPreQualificationsBySearchCriteria(PreQualificationSearchCriteriaModel criteria);
        Task<QueryResult<PreQualificationCardModel>> GetSupplierAllQualificationsList(PreQualificationSearchCriteriaModel qualificationSearchCriteria);

        Task<QueryResult<PreQualificationCardModel>> GetQualificationListForVisitor(PreQualificationSearchCriteriaModel qualificationSearchCriteria);

        Task<Tender> EditQualificationCommittees(EditeCommitteesModel committeeModel);

        Task<QueryResult<PreQualificationResultForChecking>> GetSupplierPreQualificationRequestByQualificationIdAsync(QualificationIdWithSearchCriteria qualificationSearch);
        Task<bool> AddTenderToSupplierTendersFavouriteList(int tenderId, string cr);

        Task StartPreQualificationCheckingOffersAsync(int tenderId);
        Task ReopenPreQualificationChecking(int tenderId, string agencyCode);
        Task SendPreQualificationToApproveChecking(int tenderId, string agencyCode);
        Task ApprovePreQualificationChecking(int tenderId, string verificarionCode, int typeId, string agencyCode, List<string> roles = null);
        Task RejectCheckPreQualificationOffers(int tenderId, string rejectionReason);
        Task<List<string>> GetSubscriptionAwardingInformation(int qualificationId);

        Task<QualificationSmallEvaluation> GetSmallConfigQualificationDetails(int PreQualificationId);
        Task<QualificationMediumEvaluation> GetMediumConfigQualificationDetails(int PreQualificationId);
        Task<QualificationLargeEvaluation> GetLargeConfigQualificationDetails(int PreQualificationId);
        #region Post-Qualification
        Task<PostQualificationApplyDocumentsModel> GetPostQualificationData(int qualificationId);

        Task<PostQualificationApplyDocumentsModel> getQualificationDataToCreatePostQualification(string tenderId, string qualificationId);

        Task checkPostQualificationSupplierBlock(List<string> Crs);
        Task<Tender> CreatePoatQualification(PostQualificationApplyDocumentsModel model, int userId, string agencyCode);
        Task CheckIfSupplierHavePostQualificationBefore(List<string> crs, int tenderId);
        Task<PreQualificationBasicDetailsModel> GetPostQualificationDetailsByIdForChecking(int qualificationId, int userId, List<string> roles);
        #endregion
        #region Post-Qualification-Approval
        Task<Tender> SendPostQualificationToApprove(int tenderId);
        Task<Tender> ApprovePostQualification(int tenderId, string agencyCode, string verficationCode, List<string> roles);
        Task<Tender> RejectApprovePostQualification(int tenderId, string rejectionReason, string agencyCode, List<string> roles);
        Task<Tender> ReopenPostQualification(int tenderId, string agencyCode);
        #endregion
        #region Post-Qualification-Checking
        Task SendPostQualificationToApproveChecking(int tenderId, List<string> roles);
        Task ApprovePostQualificationChecking(int tenderId, List<string> roles);
        Task<Tender> RejectCheckPostQualificationOffers(int tenderId, string rejectionReason, List<string> roles);
        Task<Tender> ReopenPostQualificationChecking(int tenderId);
        Task<PreQulaificationApprovalModel> GetPostQulaificationByQualificationId(int postQualificationId);
        #endregion

        Task CanAddPostqualificationIfTenderHasExtendOfferValidity(int tenderId);
    }
}