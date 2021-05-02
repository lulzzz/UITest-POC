using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.QualificationEvaluation;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Qualification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IQualificationQueries
    {
        Task<PrequalificationTechnicalExaminationModel> GetPrequalificationTechnicalExamination(int Id);
        Task<PreQualificationPartialDetailsModel> GetPrequalificationPartialDetails(int qualificationId,
         params Expression<Func<Tender, object>>[] includes);
        Task<QueryResult<Tender>> FindPreQualificationsBySearchCriteria(PreQualificationSearchCriteriaModel criteria, params Expression<Func<Tender, object>>[] includes);
        Task<RegistryReportForQualificationModel> GetQualificationOffersRegistryReportData(int qualificationId);
        Task<QueryResult<PreQualificationCardModel>> FindPreQualificationsModelBySearchCriteria(PreQualificationSearchCriteriaModel criteria, params Expression<Func<Tender, object>>[] includes);

        Task<QueryResult<QualificationSupplierProjectModel>> FindSupplierProjectModelBySearchCriteria(PreQualificationSearchCriteriaModel criteria, params Expression<Func<Tender, object>>[] includes);

        Task<QueryResult<PreQualificationCardModel>> FindPreQualificationBySearchCriteriaForCheckingStage(PreQualificationSearchCriteriaModel criteria);
        Task<QueryResult<PreQualificationCardModel>> GetAllSupplierPreQualificationsBySearchCriteria(PreQualificationSearchCriteriaModel criteria, List<SupplierAgencyBlockModel> allSupplierBlock, params Expression<Func<Tender, object>>[] includes);

        Task<QueryResult<PreQualificationCardModel>> GetAllVistorQualificationBySearchCriteria(PreQualificationSearchCriteriaModel criteria);

        Task<QueryResult<PreQualificationResultForChecking>> GetSupplierPreQualificationRequestByQualificationIdAsync(QualificationIdWithSearchCriteria qualificationSearch);
        Task<Tender> FindPreQualificationByIdandStatus(int tenderId, int? statusId = 0);
        Task<PreQualificationSavingModel> GetPreQualificationEditingData(int Id);
        Task<Tender> GetPreQualificationDetailsById(int Id);
        Task<PreQualificationBasicDetailsModel> GetPreQualificationDetailsByIdForChecking(int Id);
        Task<int> GetPreQualificationStatus(int Id);
        Task<TenderDatesModel> FindQualificationDatesByQualificationId(int qualificationId);
        Task<QualificationStatusModel> GetPrequalificationDetails(int qualificationId);
        Task<QualificationStatusModel> GetPrequalificationDetailsForSupplier(int qualificationId, string CR);
        Task<Tender> GetQualificationById(int Id);
        Task<Tender> GetQualificationByIdWithChangRequest(int Id);
        Task<Tender> GetPostQualificationByTenderId(int Id);
        Task<PreQulaificationApprovalModel> GetPreQualificationDetailsForPreQualificationApproval(int Id);
        Task<List<CommitteeUser>> GetCheckingCommitteeMembersOnTender(int PreQualificaionCommiteeId);
        Task<List<string>> GetSubscriptionAwardingInformation(int qualificationId);
        Task<PreQualificationDetailsModel> GetPreQualificationDetailsModelById(int Id, int branchId);
        Task<QueryResult<PreQualificationCardModel>> FindPreQualificationByCriteriaForUnderOperationsStage(PreQualificationSearchCriteriaModel criteria);
        Task<Tender> FindTenderById(int tenderId, int? statusId = 0);

        #region Qualification

        Task<PreQualificationBasicDetailsModel> GetQualificationForCheckPostQualificationByQualificationId(int tenderId, int userId, string agencyCode, List<string> roles);
        Task<PostQualificationApplyDocumentsModel> GetPostQualificationData(int id);
        Task<PostQualificationApplyDocumentsModel> getQualificationDataToCreatePostQualification(string tenderid, string qualificationId);

        List<string> GetPostQualificationSuppliers(int Id);
        Task<Tender> GetPostQualificationById(int Id);
        List<QualificationFinalResult> GetSupplierResultForQualification(int Id);
        Task<PreQulaificationApprovalModel> GetPostQualificationByQualificationId(int postQualificationId);


        Task<List<QualificationSupplierDataYearly>> GetQualificationSupplierDataYear(int qualificationId, string supplierId);

        Task<List<QualificationSupplierData>> GetQualificationSupplierData(int qualificationId, string supplierId);
        Task<Tender> GetTenderWithQualificationConfigurations(int TenderId);

        Task<List<QualificationSupplierProject>> GetQualificationSupplierProject(int qualificationId, string supplierId);

        Task<List<QualificationTypeCategory>> FindQualificationItems(int PreQualificationTypeID);

        Task<List<QualificationSubCategoryConfiguration>> FindSubCategoryConfiguration(int TenderId);

        Task<QualificationFinalResult> FindFinalResult(string SupplierId, int QualificationId);

        #endregion

    }

}
