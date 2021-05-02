using AutoMapper;
using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.QualificationEvaluation;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Qualification;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public class QualificationAppService : IQualificationAppService
    {
        private readonly INotificationAppService _notificationAppService;
        private readonly IQualificationQueries _qualificationQueries;
        private readonly IVerificationService _verification;
        private readonly IQualificationCommands _qualificationCommands;
        private readonly IMapper _mapper;
        private readonly IQualificationDomainService _qualificationDomainService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGenericCommandRepository _genericCommandRepository;
        private readonly ISupplierQualificationDocumentDomainService _supplierQualificationDocumentDomainService;
        private readonly IBlockAppService _blockAppService;
        private readonly ITenderQueries _tenderQueries;
        private readonly ITenderCommands _tenderCommands;
        public readonly ITenderAppService _tenderAppService;

        public QualificationAppService(INotificationAppService notificationService, IQualificationQueries qualificationQueries, IQualificationCommands qualificationCommands
            , IVerificationService verification
            , IQualificationDomainService qualificationDomainService, IMapper mapper, IHttpContextAccessor httpContextAccessor, /*IFileNetService filenetService,*/
            IGenericCommandRepository genericCommandRepository, IBlockAppService blockAppService, ITenderAppService tenderAppService, ITenderQueries tenderQueries,
            ITenderCommands tenderCommands, ISupplierQualificationDocumentDomainService supplierQualificationDocumentDomainService)
        {
            _qualificationQueries = qualificationQueries;
            _tenderQueries = tenderQueries;
            _tenderCommands = tenderCommands;
            _qualificationCommands = qualificationCommands;
            _qualificationDomainService = qualificationDomainService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _genericCommandRepository = genericCommandRepository;
            _blockAppService = blockAppService;
            _tenderAppService = tenderAppService;
            _notificationAppService = notificationService;
            _verification = verification;
            _supplierQualificationDocumentDomainService = supplierQualificationDocumentDomainService;
        }

        #region Service Queries

        public async Task<QueryResult<PreQualificationCardModel>> GetSupplierAllQualificationsList(PreQualificationSearchCriteriaModel qualificationSearchCriteria)
        {
            List<SupplierAgencyBlockModel> supplierblocks = await _blockAppService.GetAllSupplierBlocks(null, new List<string> { qualificationSearchCriteria.cr });
            if (string.IsNullOrEmpty(qualificationSearchCriteria.Sort))
            {
                qualificationSearchCriteria.Sort = "SubmitionDate";
                qualificationSearchCriteria.SortDirection = "DESC";
            }
            QueryResult<PreQualificationCardModel> tenders = await _qualificationQueries.GetAllSupplierPreQualificationsBySearchCriteria(qualificationSearchCriteria, supplierblocks
                            , x => x.TenderActivities, x => x.Status, x => x.Agency, x => x.TenderAreas, x => x.FavouriteSupplierTenders, x => x.TenderType);

            return tenders;
        }

        public async Task<QueryResult<PreQualificationCardModel>> GetQualificationListForVisitor(PreQualificationSearchCriteriaModel qualificationSearchCriteria)
        {
            if (string.IsNullOrEmpty(qualificationSearchCriteria.Sort))
            {
                qualificationSearchCriteria.Sort = "SubmitionDate";
                qualificationSearchCriteria.SortDirection = "DESC";
            }
            QueryResult<PreQualificationCardModel> tenders = await _qualificationQueries.GetAllVistorQualificationBySearchCriteria(qualificationSearchCriteria);

            return tenders;
        }

        public async Task<List<string>> GetSubscriptionAwardingInformation(int qualificationId)
        {
            return await _qualificationQueries.GetSubscriptionAwardingInformation(qualificationId);
        }

        public async Task<PreQualificationDetailsModel> GetPreQualificationDetailsModelById(int qualificationId, int branchId)
        {
            return await _qualificationQueries.GetPreQualificationDetailsModelById(qualificationId, branchId);

        }

        public async Task<RegistryReportForQualificationModel> GetQualificationOffersRegistryReportData(int qualificationId)
        {
            if (qualificationId == 0)
                throw new ArgumentNullException(nameof(qualificationId));
            return await _qualificationQueries.GetQualificationOffersRegistryReportData(qualificationId);
        }


        public async Task<QueryResult<QualificationSupplierProjectModel>> FindSupplierProjectModelBySearchCriteria(PreQualificationSearchCriteriaModel criteria)
        {
            return await _qualificationQueries.FindSupplierProjectModelBySearchCriteria(criteria);
        }

        public async Task<QueryResult<PreQualificationCardModel>> FindPreQualificationsModelBySearchCriteria(PreQualificationSearchCriteriaModel criteria)
        {
            Check.ArgumentNotNull(nameof(criteria), criteria);
            if (string.IsNullOrEmpty(criteria.Sort))
            {
                criteria.Sort = "CreatedAt";
                criteria.SortDirection = "DESC";
            }
            return await _qualificationQueries.FindPreQualificationsModelBySearchCriteria(criteria);
        }

        public async Task<QueryResult<PreQualificationCardModel>> FindPreQualificationByCriteriaForUnderOperationsStage(PreQualificationSearchCriteriaModel criteria)
        {
            criteria.BranchId = _httpContextAccessor.HttpContext.User.UserBranch();
            Check.ArgumentNotNull(nameof(criteria), criteria);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.UnderEstablishing);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.Pending);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.Established);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.Approved);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.Rejected);
            if (string.IsNullOrEmpty(criteria.Sort))
            {
                criteria.Sort = "CreatedAt";
                criteria.SortDirection = "DESC";
            }
            return await _qualificationQueries.FindPreQualificationByCriteriaForUnderOperationsStage(criteria);
        }

        public async Task<QueryResult<PreQualificationCardModel>> FindPreQualificationForCheckingStageBySearchCriteria(PreQualificationSearchCriteriaModel criteria)
        {
            Check.ArgumentNotNull(nameof(criteria), criteria);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.Approved);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.DocumentChecking);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.DocumentCheckPending);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.DocumentCheckRejected);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.DocumentCheckConfirmed);
            if (string.IsNullOrEmpty(criteria.Sort))
            {
                criteria.Sort = "CreatedAt";
                criteria.SortDirection = "DESC";
            }
            return await _qualificationQueries.FindPreQualificationBySearchCriteriaForCheckingStage(criteria);
        }

        public async Task<QueryResult<Tender>> FindCheckedPreQualificationsBySearchCriteria(PreQualificationSearchCriteriaModel criteria)
        {
            return await _qualificationQueries.FindPreQualificationsBySearchCriteria(criteria,
                x => x.Status, x => x.Agency, x => x.TechnicalOrganization, x => x.TechnicalOrganization.CommitteeUsers,
                x => x.OffersCheckingCommittee, x => x.OffersCheckingCommittee.CommitteeUsers, x => x.FavouriteSupplierTenders);

        }

        public async Task<PreQualificationBasicDetailsModel> GetPreQualificationDetailsByIdForChecking(int qualificationId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(qualificationId), qualificationId.ToString());
            Tender qualification = await _qualificationQueries.GetQualificationByIdWithChangRequest(qualificationId);
            _qualificationDomainService.IsValidToAccecssCheckingTender(qualification);
            if (qualification != null && qualification.TenderStatusId == (int)Enums.TenderStatus.Approved)
            {
                await _qualificationDomainService.CheckQualificationDateValidation(qualification);
                qualification.UpdateTenderStatus(Enums.TenderStatus.DocumentChecking);
                qualification.DeleteCancelRequestedByDataEntry(qualificationId);

                await _qualificationCommands.UpdateAsync(qualification);
            }
            return await _qualificationQueries.GetPreQualificationDetailsByIdForChecking(qualificationId);
        }

        public async Task<int> GetPreQualificationStatus(int qualificationId)
        {
            return await _qualificationQueries.GetPreQualificationStatus(qualificationId);
        }

        public async Task<QualificationStatusModel> GetPrequalificationDetails(int qualificationId, string agencyCode)
        {
            var PreQualification = await _qualificationQueries.GetPrequalificationDetails(qualificationId);
            return PreQualification;
        }

        public async Task<QualificationStatusModel> GetPrequalificationDetailsForSupplier(int qualificationId, string CR)
        {
            return await _qualificationQueries.GetPrequalificationDetailsForSupplier(qualificationId, CR);
        }

        public async Task<QualificationSmallEvaluation> GetSmallConfigQualificationDetails(int PreQualificationId)
        {
            var details = await GetQualificationSmallEvaluation(PreQualificationId);
            return details;
        }

        public async Task<QualificationMediumEvaluation> GetMediumConfigQualificationDetails(int PreQualificationId)
        {
            var details = await GetQualificationMediumEvaluation(PreQualificationId);
            return details;
        }

        public async Task<QualificationLargeEvaluation> GetLargeConfigQualificationDetails(int PreQualificationId)
        {
            var details = await GetQualificationLargeEvaluation(PreQualificationId);
            return details;
        }


        public async Task<PreQualificationPartialDetailsModel> GetPrequalificationPartialDetails(int qualificationId)
        {
            return await _qualificationQueries.GetPrequalificationPartialDetails(qualificationId, c => c.TenderActivities, c => c.TenderCountries, s => s.TenderAreas, d => d.PreQualificationApplyDocuments);
        }

        public async Task<PreQualificationSavingModel> GetPreQualificationEditingData(int qualificationId)
        {
            var preQualification = await _qualificationQueries.GetPreQualificationEditingData(qualificationId);
            switch (preQualification.QualificationTypeId)
            {
                case (int)Enums.PreQualificationType.Small:
                    preQualification.QualificationSmallEvaluation = await GetQualificationSmallEvaluation(qualificationId);
                    break;
                case (int)Enums.PreQualificationType.Medium:
                    preQualification.QualificationMediumEvaluation = await GetQualificationMediumEvaluation(qualificationId);
                    break;
                case (int)Enums.PreQualificationType.Large:
                    preQualification.QualificationLargeEvaluation = await GetQualificationLargeEvaluation(qualificationId);
                    break;
            }
            return preQualification;
        }
        private async Task<QualificationSmallEvaluation> GetQualificationSmallEvaluation(int qualificationId)
        {
            List<QualificationSupplierData> supplierDataList = new List<QualificationSupplierData>();
            var tender = await _qualificationQueries.GetTenderWithQualificationConfigurations(qualificationId);
            var isSupplier = _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.supplier);
            var supplierCr = _httpContextAccessor.HttpContext.User.SupplierNumber();
            if (isSupplier)
            {
                supplierDataList = await _qualificationQueries.GetQualificationSupplierData(qualificationId, supplierCr);
            }
            return new QualificationSmallEvaluation
            {
                ExperienceYearCountMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfYearsOfExperience).FirstOrDefault().Max,
                ExperienceYearCountMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfYearsOfExperience).FirstOrDefault().Min,
                ExperienceYearCountWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfYearsOfExperience).FirstOrDefault().Weight,
                SupplierExperienceYearCount = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfYearsOfExperience && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsExperienceYearCount = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfYearsOfExperience && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,

                LastProjectCountMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfProjectsImplementedLastThreeYears).FirstOrDefault().Max,
                LastProjectCountMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfProjectsImplementedLastThreeYears).FirstOrDefault().Min,
                LastProjectCountWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfProjectsImplementedLastThreeYears).FirstOrDefault().Weight,
                SupplierLastProjectCount = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfProjectsImplementedLastThreeYears && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsLastProjectCount = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfProjectsImplementedLastThreeYears && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,

                LastProjectCostMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalValueProjectsLastThreeYears).FirstOrDefault().Max,
                LastProjectCostMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalValueProjectsLastThreeYears).FirstOrDefault().Min,
                LastProjectCostWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalValueProjectsLastThreeYears).FirstOrDefault().Weight,
                SupplierLastProjectCost = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalValueProjectsLastThreeYears && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsLastProjectCost = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalValueProjectsLastThreeYears && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,

                CurrentProjectCountMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfExistingProjects).FirstOrDefault().Max,
                CurrentProjectCountMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfExistingProjects).FirstOrDefault().Min,
                CurrentProjectCountWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfExistingProjects).FirstOrDefault().Weight,
                SupplierCurrentProjectCount = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfExistingProjects && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsCurrentProjectCount = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfExistingProjects && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,

                CurrentProjectCostMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.ValueOfExistingProjects).FirstOrDefault().Max,
                CurrentProjectCostMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.ValueOfExistingProjects).FirstOrDefault().Min,
                CurrentProjectCostWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.ValueOfExistingProjects).FirstOrDefault().Weight,
                SupplierCurrentProjectCost = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.ValueOfExistingProjects && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsCurrentProjectCost = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.ValueOfExistingProjects && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,

                TotalEmployeeCountMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfEmployees).FirstOrDefault().Max,
                TotalEmployeeCountMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfEmployees).FirstOrDefault().Min,
                TotalEmployeeCountWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfEmployees).FirstOrDefault().Weight,
                SupplierTotalEmployeeCount = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfEmployees && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsTotalEmployeeCount = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfEmployees && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,

                TotalEmployeePercentageMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.PercentageOfSaudiEmployees).FirstOrDefault().Max,
                TotalEmployeePercentageMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.PercentageOfSaudiEmployees).FirstOrDefault().Min,
                TotalEmployeePercentageWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.PercentageOfSaudiEmployees).FirstOrDefault().Weight,
                SupplierTotalEmployeePercentage = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.PercentageOfSaudiEmployees && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsTotalEmployeePercentage = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.PercentageOfSaudiEmployees && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,

                CashRateMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.CashRate).FirstOrDefault().Max,
                CashRateMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.CashRate).FirstOrDefault().Min,
                CashRateWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.CashRate).FirstOrDefault().Weight,
                SupplierCashRate = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.CashRate && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsCashRate = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.CashRate && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,

                LiquidityRatioMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.LiquidityRatio).FirstOrDefault().Max,
                LiquidityRatioMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.LiquidityRatio).FirstOrDefault().Min,
                LiquidityRatioWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.LiquidityRatio).FirstOrDefault().Weight,
                SupplierLiquidityRatio = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.LiquidityRatio && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsLiquidityRatio = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.LiquidityRatio && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,

                PreviousExperienceWeight = tender.QualificationSubCategoryConfigurations.Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.PreviousExperienceYear).FirstOrDefault().Weight,
                PreviousExperiencePoints = isSupplier && supplierDataList.Count > 0 ? tender.QualificationSubCategoryResults
                .Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.PreviousExperienceYear && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().ResultValue : 0,
                ExistingContractualObligationsWeight = tender.QualificationSubCategoryConfigurations.Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.ExistingContractualObligations).FirstOrDefault().Weight,
                ExistingContractualObligationsPoints = isSupplier && supplierDataList.Count > 0 ? tender.QualificationSubCategoryResults
                .Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.ExistingContractualObligations && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().ResultValue : 0,
                HRWeight = tender.QualificationSubCategoryConfigurations.Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.HumanResource).FirstOrDefault().Weight,
                HRPoints = isSupplier && supplierDataList.Count > 0 ? tender.QualificationSubCategoryResults
                .Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.HumanResource && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().ResultValue : 0,
                FinancialStatementsWeight = tender.QualificationSubCategoryConfigurations.Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.FinancialStatements).FirstOrDefault().Weight,

                TenderPointsToPass = tender.TenderPointsToPass,
                TechnicalAdministrativeCapacity = tender.TechnicalAdministrativeCapacity,
                FinancialCapacity = tender.FinancialCapacity,
                SupplierTotalPoints = isSupplier && supplierDataList.Count > 0 ? tender.QualificationFinalResults.Where(c => c.SupplierSelectedCr == supplierCr).FirstOrDefault().ResultValue : 0,
                FailingReason = isSupplier && supplierDataList.Count > 0 ?
                tender.QualificationFinalResults.Where(q => q.SupplierSelectedCr == supplierCr && q.TenderId == qualificationId).FirstOrDefault().FailingReason : "",
                SupplierResultName = isSupplier && supplierDataList.Count > 0 ? tender.QualificationFinalResults.Where(c => c.SupplierSelectedCr == supplierCr).FirstOrDefault().QualificationLookup.Name : "",
                FinancialAcquiredPoints = isSupplier && supplierDataList.Count > 0 ? tender.QualificationCategoryResults
                .Where(c => c.SupplierSelectedCr == supplierCr && c.QualificationItemCategoryId == (int)Enums.QualificationItemCategory.Financial).FirstOrDefault().ResultValue : 0,
                TechnicalAdministrativeAcquiredPoints = isSupplier && supplierDataList.Count > 0 ? tender.QualificationCategoryResults
                .Where(c => c.SupplierSelectedCr == supplierCr && c.QualificationItemCategoryId == (int)Enums.QualificationItemCategory.Technical).FirstOrDefault().ResultValue : 0,
            };
        }
     
        private async Task<QualificationMediumEvaluation> GetQualificationMediumEvaluation(int qualificationId)
        {
            List<QualificationSupplierData> supplierDataList = new List<QualificationSupplierData>();
            var tender = await _qualificationQueries.GetTenderWithQualificationConfigurations(qualificationId);
            var isSupplier = _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.supplier);
            var supplierCr = _httpContextAccessor.HttpContext.User.SupplierNumber();
            if (isSupplier)
            {
                supplierDataList = await _qualificationQueries.GetQualificationSupplierData(qualificationId, supplierCr);
            }
            return new QualificationMediumEvaluation
            {
                ExperienceYearCountMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfYearsOfExperience).FirstOrDefault().Max,
                ExperienceYearCountMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfYearsOfExperience).FirstOrDefault().Min,
                ExperienceYearCountWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfYearsOfExperience).FirstOrDefault().Weight,
                SupplierExperienceYearCount = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfYearsOfExperience && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsExperienceYearCount = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfYearsOfExperience && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,

                LastProjectCountMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfProjectsImplementedLastThreeYears).FirstOrDefault().Max,
                LastProjectCountMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfProjectsImplementedLastThreeYears).FirstOrDefault().Min,
                LastProjectCountWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfProjectsImplementedLastThreeYears).FirstOrDefault().Weight,
                SupplierLastProjectCount = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfProjectsImplementedLastThreeYears && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsLastProjectCount = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfProjectsImplementedLastThreeYears && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,

                LastProjectCostMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalValueProjectsLastThreeYears).FirstOrDefault().Max,
                LastProjectCostMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalValueProjectsLastThreeYears).FirstOrDefault().Min,
                LastProjectCostWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalValueProjectsLastThreeYears).FirstOrDefault().Weight,
                SupplierLastProjectCost = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalValueProjectsLastThreeYears && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsLastProjectCost = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalValueProjectsLastThreeYears && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,

                CurrentProjectCountMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfExistingProjects).FirstOrDefault().Max,
                CurrentProjectCountMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfExistingProjects).FirstOrDefault().Min,
                CurrentProjectCountWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfExistingProjects).FirstOrDefault().Weight,
                SupplierCurrentProjectCount = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfExistingProjects && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsCurrentProjectCount = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfExistingProjects && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,

                CurrentProjectCostMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.ValueOfExistingProjects).FirstOrDefault().Max,
                CurrentProjectCostMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.ValueOfExistingProjects).FirstOrDefault().Min,
                CurrentProjectCostWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.ValueOfExistingProjects).FirstOrDefault().Weight,
                SupplierCurrentProjectCost = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.ValueOfExistingProjects && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsCurrentProjectCost = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.ValueOfExistingProjects && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,

                TotalEmployeeCountMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfEmployees).FirstOrDefault().Max,
                TotalEmployeeCountMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfEmployees).FirstOrDefault().Min,
                TotalEmployeeCountWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfEmployees).FirstOrDefault().Weight,
                SupplierTotalEmployeeCount = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfEmployees && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsTotalEmployeeCount = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfEmployees && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,

                TotalEmployeePercentageMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.PercentageOfSaudiEmployees).FirstOrDefault().Max,
                TotalEmployeePercentageMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.PercentageOfSaudiEmployees).FirstOrDefault().Min,
                TotalEmployeePercentageWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.PercentageOfSaudiEmployees).FirstOrDefault().Weight,
                SupplierTotalEmployeePercentage = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.PercentageOfSaudiEmployees && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsTotalEmployeePercentage = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.PercentageOfSaudiEmployees && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,

                CashRateMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.CashRate).FirstOrDefault().Max,
                CashRateMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.CashRate).FirstOrDefault().Min,
                CashRateWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.CashRate).FirstOrDefault().Weight,
                SupplierCashRate = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.CashRate && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsCashRate = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.CashRate && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,

                LiquidityRatioMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.LiquidityRatio).FirstOrDefault().Max,
                LiquidityRatioMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.LiquidityRatio).FirstOrDefault().Min,
                LiquidityRatioWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.LiquidityRatio).FirstOrDefault().Weight,
                SupplierLiquidityRatio = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.LiquidityRatio && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsLiquidityRatio = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.LiquidityRatio && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,

                TradeRateMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.TradingRatio).FirstOrDefault().Max,
                TradeRateMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.TradingRatio).FirstOrDefault().Min,
                TradeRateWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.TradingRatio).FirstOrDefault().Weight,
                SupplierTradeRate = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.TradingRatio && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsTradeRate = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.TradingRatio && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,

                PreviousExperienceWeight = tender.QualificationSubCategoryConfigurations.Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.PreviousExperienceYear).FirstOrDefault().Weight,
                PreviousExperiencePoints = isSupplier && supplierDataList.Count > 0 ? tender.QualificationSubCategoryResults
                .Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.PreviousExperienceYear && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().ResultValue : 0,
                ExistingContractualObligationsWeight = tender.QualificationSubCategoryConfigurations.Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.ExistingContractualObligations).FirstOrDefault().Weight,
                ExistingContractualObligationsPoints = isSupplier && supplierDataList.Count > 0 ? tender.QualificationSubCategoryResults
                .Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.ExistingContractualObligations && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().ResultValue : 0,
                HRWeight = tender.QualificationSubCategoryConfigurations.Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.HumanResource).FirstOrDefault().Weight,
                HRPoints = isSupplier && supplierDataList.Count > 0 ? tender.QualificationSubCategoryResults
                .Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.HumanResource && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().ResultValue : 0,
                QualityWeight = tender.QualificationSubCategoryConfigurations.Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.Quality).FirstOrDefault().Weight,
                QualityPoints = isSupplier && supplierDataList.Count > 0 ? tender.QualificationSubCategoryResults
                .Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.Quality && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().ResultValue : 0,
                EnviromentAndHealthyWeight = tender.QualificationSubCategoryConfigurations.Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.EnviromentAndHealthy).FirstOrDefault().Weight,
                EnviromentAndHealthyPoints = isSupplier && supplierDataList.Count > 0 ? tender.QualificationSubCategoryResults
                .Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.EnviromentAndHealthy && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().ResultValue : 0,
                FinancialStatementsWeight = tender.QualificationSubCategoryConfigurations.Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.FinancialStatements).FirstOrDefault().Weight,

                TenderPointsToPass = tender.TenderPointsToPass,
                TechnicalAdministrativeCapacity = tender.TechnicalAdministrativeCapacity,
                FinancialCapacity = tender.FinancialCapacity,
                FailingReason = isSupplier && supplierDataList.Count > 0 ?
                tender.QualificationFinalResults.Where(q => q.SupplierSelectedCr == supplierCr && q.TenderId == qualificationId).FirstOrDefault().FailingReason : "",
                SupplierTotalPoints = isSupplier && supplierDataList.Count > 0 ? tender.QualificationFinalResults.Where(c => c.SupplierSelectedCr == supplierCr).FirstOrDefault().ResultValue : 0,
                SupplierResultName = isSupplier && supplierDataList.Count > 0 ? tender.QualificationFinalResults.Where(c => c.SupplierSelectedCr == supplierCr).FirstOrDefault().QualificationLookup.Name : "",
                FinancialAcquiredPoints = isSupplier && supplierDataList.Count > 0 ? tender.QualificationCategoryResults
                .Where(c => c.SupplierSelectedCr == supplierCr && c.QualificationItemCategoryId == (int)Enums.QualificationItemCategory.Financial).FirstOrDefault().ResultValue : 0,
                TechnicalAdministrativeAcquiredPoints = isSupplier && supplierDataList.Count > 0 ? tender.QualificationCategoryResults
                .Where(c => c.SupplierSelectedCr == supplierCr && c.QualificationItemCategoryId == (int)Enums.QualificationItemCategory.Technical).FirstOrDefault().ResultValue : 0
            };
        }

        
        private async Task<QualificationLargeEvaluation> GetQualificationLargeEvaluation(int qualificationId)
        {
            List<QualificationSupplierData> supplierDataList = new List<QualificationSupplierData>();
            var tender = await _qualificationQueries.GetTenderWithQualificationConfigurations(qualificationId);
            var isSupplier = _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.supplier);
            var supplierCr = _httpContextAccessor.HttpContext.User.SupplierNumber();
            if (isSupplier)
            {
                supplierDataList = await _qualificationQueries.GetQualificationSupplierData(qualificationId, supplierCr);
            }
            return new QualificationLargeEvaluation
            {
                ExperienceYearCountMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfYearsOfExperience).FirstOrDefault().Max,
                ExperienceYearCountMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfYearsOfExperience).FirstOrDefault().Min,
                ExperienceYearCountWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfYearsOfExperience).FirstOrDefault().Weight,
                SupplierExperienceYearCount = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfYearsOfExperience && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsExperienceYearCount = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfYearsOfExperience && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,

                LastProjectCountMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfProjectsImplementedLastThreeYears).FirstOrDefault().Max,
                LastProjectCountMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfProjectsImplementedLastThreeYears).FirstOrDefault().Min,
                LastProjectCountWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfProjectsImplementedLastThreeYears).FirstOrDefault().Weight,
                SupplierLastProjectCount = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfProjectsImplementedLastThreeYears && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsLastProjectCount = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfProjectsImplementedLastThreeYears && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,

                LastProjectCostMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalValueProjectsLastThreeYears).FirstOrDefault().Max,
                LastProjectCostMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalValueProjectsLastThreeYears).FirstOrDefault().Min,
                LastProjectCostWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalValueProjectsLastThreeYears).FirstOrDefault().Weight,
                SupplierLastProjectCost = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalValueProjectsLastThreeYears && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsLastProjectCost = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalValueProjectsLastThreeYears && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,



                CurrentProjectCountMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfExistingProjects).FirstOrDefault().Max,
                CurrentProjectCountMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfExistingProjects).FirstOrDefault().Min,
                CurrentProjectCountWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfExistingProjects).FirstOrDefault().Weight,
                SupplierCurrentProjectCount = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfExistingProjects && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsCurrentProjectCount = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfExistingProjects && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,

                CurrentProjectCostMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.ValueOfExistingProjects).FirstOrDefault().Max,
                CurrentProjectCostMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.ValueOfExistingProjects).FirstOrDefault().Min,
                CurrentProjectCostWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.ValueOfExistingProjects).FirstOrDefault().Weight,
                SupplierCurrentProjectCost = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.ValueOfExistingProjects && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsCurrentProjectCost = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.ValueOfExistingProjects && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,

                TotalEmployeeCountMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfEmployees).FirstOrDefault().Max,
                TotalEmployeeCountMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfEmployees).FirstOrDefault().Min,
                TotalEmployeeCountWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfEmployees).FirstOrDefault().Weight,
                SupplierTotalEmployeeCount = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfEmployees && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsTotalEmployeeCount = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfEmployees && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,



                TotalEmployeePercentageMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.PercentageOfSaudiEmployees).FirstOrDefault().Max,
                TotalEmployeePercentageMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.PercentageOfSaudiEmployees).FirstOrDefault().Min,
                TotalEmployeePercentageWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.PercentageOfSaudiEmployees).FirstOrDefault().Weight,
                SupplierTotalEmployeePercentage = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.PercentageOfSaudiEmployees && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsTotalEmployeePercentage = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.PercentageOfSaudiEmployees && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,


                CashRateMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.CashRate).FirstOrDefault().Max,
                CashRateMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.CashRate).FirstOrDefault().Min,
                CashRateWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.CashRate).FirstOrDefault().Weight,
                SupplierCashRate = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.CashRate && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsCashRate = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.CashRate && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,

                LiquidityRatioMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.LiquidityRatio).FirstOrDefault().Max,
                LiquidityRatioMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.LiquidityRatio).FirstOrDefault().Min,
                LiquidityRatioWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.LiquidityRatio).FirstOrDefault().Weight,
                SupplierLiquidityRatio = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.LiquidityRatio && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsLiquidityRatio = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.LiquidityRatio && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,



                TradeRateMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.TradingRatio).FirstOrDefault().Max,
                TradeRateMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.TradingRatio).FirstOrDefault().Min,
                TradeRateWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.TradingRatio).FirstOrDefault().Weight,
                SupplierTradeRate = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.TradingRatio && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsTradeRate = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.TradingRatio && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,

                ObligationsRationMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.RatioOfObligations).FirstOrDefault().Max,
                ObligationsRationMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.RatioOfObligations).FirstOrDefault().Min,
                ObligationsRationWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.RatioOfObligations).FirstOrDefault().Weight,
                SupplierObligationsRation = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.RatioOfObligations && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsObligationsRation = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.RatioOfObligations && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,

                ProfitabilityRateMax = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.RateOfProfitability).FirstOrDefault().Max,
                ProfitabilityRateMin = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.RateOfProfitability).FirstOrDefault().Min,
                ProfitabilityRateWeight = tender.QualificationConfigurations.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.RateOfProfitability).FirstOrDefault().Weight,
                SupplierProfitabilityRate = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.RateOfProfitability && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().SupplierValue : 0,
                SupplierPointsProfitabilityRate = isSupplier && supplierDataList.Count > 0 ? supplierDataList.Where(c => c.QualificationItemId == (int)Enums.QualificationEvaluationItems.RateOfProfitability && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().PointValue : 0,

                PreviousExperienceWeight = tender.QualificationSubCategoryConfigurations.Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.PreviousExperienceYear).FirstOrDefault().Weight,
                PreviousExperiencePoints = isSupplier && supplierDataList.Count > 0 ? tender.QualificationSubCategoryResults
                .Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.PreviousExperienceYear && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().ResultValue : 0,
                ExistingContractualObligationsWeight = tender.QualificationSubCategoryConfigurations.Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.ExistingContractualObligations).FirstOrDefault().Weight,
                ExistingContractualObligationsPoints = isSupplier && supplierDataList.Count > 0 ? tender.QualificationSubCategoryResults
                .Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.ExistingContractualObligations && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().ResultValue : 0,
                HRWeight = tender.QualificationSubCategoryConfigurations.Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.HumanResource).FirstOrDefault().Weight,
                HRPoints = isSupplier && supplierDataList.Count > 0 ? tender.QualificationSubCategoryResults
                .Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.HumanResource && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().ResultValue : 0,
                QualityWeight = tender.QualificationSubCategoryConfigurations.Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.Quality).FirstOrDefault().Weight,
                QualityPoints = isSupplier && supplierDataList.Count > 0 ? tender.QualificationSubCategoryResults
                .Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.Quality && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().ResultValue : 0,
                EnviromentAndHealthyWeight = tender.QualificationSubCategoryConfigurations.Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.EnviromentAndHealthy).FirstOrDefault().Weight,
                EnviromentAndHealthyPoints = isSupplier && supplierDataList.Count > 0 ? tender.QualificationSubCategoryResults
                .Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.EnviromentAndHealthy && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().ResultValue : 0,
                InsuranceWeight = tender.QualificationSubCategoryConfigurations.Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.Insurance).FirstOrDefault().Weight,
                InsurancePoints = isSupplier && supplierDataList.Count > 0 ? tender.QualificationSubCategoryResults
                .Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.Insurance && c.SupplierSelectedCr == supplierCr)
                .FirstOrDefault().ResultValue : 0,
                FinancialStatementsWeight = tender.QualificationSubCategoryConfigurations.Where(c => c.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.FinancialStatements).FirstOrDefault().Weight,

                TenderPointsToPass = tender.TenderPointsToPass,
                TechnicalAdministrativeCapacity = tender.TechnicalAdministrativeCapacity,
                FinancialCapacity = tender.FinancialCapacity,
                FailingReason = isSupplier && supplierDataList.Count > 0 ?
                tender.QualificationFinalResults.Where(q => q.SupplierSelectedCr == supplierCr && q.TenderId == qualificationId).FirstOrDefault().FailingReason : "",
                SupplierTotalPoints = isSupplier && supplierDataList.Count > 0 ? tender.QualificationFinalResults.Where(c => c.SupplierSelectedCr == supplierCr).FirstOrDefault().ResultValue : 0,
                SupplierResultName = isSupplier && supplierDataList.Count > 0 ? tender.QualificationFinalResults.Where(c => c.SupplierSelectedCr == supplierCr).FirstOrDefault().QualificationLookup.Name : "",
                FinancialAcquiredPoints = isSupplier && supplierDataList.Count > 0 ? tender.QualificationCategoryResults
                .Where(c => c.SupplierSelectedCr == supplierCr && c.QualificationItemCategoryId == (int)Enums.QualificationItemCategory.Financial).FirstOrDefault().ResultValue : 0,
                TechnicalAdministrativeAcquiredPoints = isSupplier && supplierDataList.Count > 0 ? tender.QualificationCategoryResults
                .Where(c => c.SupplierSelectedCr == supplierCr && c.QualificationItemCategoryId == (int)Enums.QualificationItemCategory.Technical).FirstOrDefault().ResultValue : 0
            };
        }



        public async Task<PreQulaificationApprovalModel> GetPreQualificationDetailsByPreQualificationId(int Id, string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(Id), Id.ToString());
            var obj = await _qualificationQueries.GetPreQualificationDetailsForPreQualificationApproval(Id);
            if (obj.AgencyCode != agencyCode)
                throw new AuthorizationException();
            return obj;
        }

        #endregion

        #region Check

        public async Task<QueryResult<PreQualificationResultForChecking>> GetSupplierPreQualificationRequestByQualificationIdAsync(QualificationIdWithSearchCriteria qualificationSearch)
        {
            var qualificationRequest = await _qualificationQueries.GetSupplierPreQualificationRequestByQualificationIdAsync(qualificationSearch);
            return qualificationRequest;
        }

        public async Task<PrequalificationTechnicalExaminationModel> GetPrequalificationTechnicalExamination(int Id)
        {
            var PrequalificationTechnicalExamination = await _qualificationQueries.GetPrequalificationTechnicalExamination(Id);
            return PrequalificationTechnicalExamination;
        }

        #endregion


        #region Service Commands

        public async Task<Tender> SaveDraft(PreQualificationSavingModel model, int userId, string userAgency, int branchId)
        {
            Tender qualification = new Tender();
            model.TenderId = Util.Decrypt(model.TenderIdString);
            qualification.CreatePreQualification(model.TenderId, (int)Enums.TenderType.PreQualification, model.TenderName, model.TechnicalOrganizationId, model.PreQualificationCommitteeId, model.LastEnqueriesDate, model.LastOfferPresentationDate, model.OffersCheckingDate
            , (int)Enums.TenderStatus.UnderEstablishing, model.TenderPointsToPass, model.TechnicalAdministrativeCapacity, model.FinancialCapacity, model.QualificationTypeId, userAgency, branchId);

            List<TenderAttachment> attachments = new List<TenderAttachment>(); if (model.Attachments != null)
            {
                foreach (var item in model.Attachments)
                {
                    attachments.Add(new TenderAttachment(item.Name, item.FileNetReferenceId, item.AttachmentTypeId));
                }
            }
            if (qualification.TenderId == 0)
            {
                qualification.UpdateTenderRelations(model.TenderAreaIDs, model.TenderActivitieIDs, model.TenderConstructionWorkIDs, model.TenderMentainanceRunnigWorkIDs, model.InsideKSA, model.Details, model.ActivityDescription, /*userId,*/ model.TenderCountriesIDs, false);
                qualification.SetPointToPassToQualification(model.TenderPointsToPass, model.TechnicalAdministrativeCapacity, model.FinancialCapacity, model.QualificationTypeId);
                qualification.UpdateAttachments(attachments, userId, false);
                qualification.AddActionHistory((int)Enums.TenderStatus.UnderEstablishing, TenderActions.UpdateTender, "", userId);
                qualification.SetReferenceNumber(await _qualificationCommands.UpdateReferenceNumber()); await _genericCommandRepository.CreateAsync(qualification);
                if (model.QualificationTypeId != null)
                {
                    await CreateQualificationConfiguration(model, qualification, true);
                }
            }
            else
            {
                qualification = await _qualificationQueries.FindPreQualificationByIdandStatus(model.TenderId);
                if ((qualification.TenderStatusId != (int)Enums.TenderStatus.UnderEstablishing && qualification.TenderStatusId != (int)Enums.TenderStatus.Established) || userAgency != qualification.AgencyCode || branchId != qualification.BranchId)
                    throw new AuthorizationException();
                model.TenderStatusId = (int)Enums.TenderStatus.UnderEstablishing;

                qualification.SetPointToPassToQualification(model.TenderPointsToPass, model.TechnicalAdministrativeCapacity, model.FinancialCapacity, model.QualificationTypeId);

                qualification.DeleteQualificationConfigurations();
                qualification.DeleteQualificationSubCategoryConfigurations();

                if (model.QualificationTypeId != null)
                {
                    await CreateQualificationConfiguration(model, qualification, true);
                }

                qualification.UpdatePreQualificationData(model, attachments, _httpContextAccessor.HttpContext.User.UserId());
                _genericCommandRepository.Update(qualification);
            }


            await _genericCommandRepository.SaveAsync();
            return qualification;
        }

        public async Task<Tender> SendPreQualificationToApprove(int tenderId, string agencycode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender PreQualification = await _qualificationQueries.GetQualificationById(tenderId);
            if (PreQualification.AgencyCode != agencycode)
                throw new AuthorizationException();

            await _qualificationDomainService.IsValidToSendQualificationForApprovement(PreQualification);
            PreQualification.UpdateTenderStatus(Enums.TenderStatus.Pending, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.UpdatePreQualification);

            await SendPreQualificationToApproveNotification(PreQualification);
            var result = await _qualificationCommands.UpdatePreQualificationAsync(PreQualification);
            return result;
        }

        public async Task<Tender> SendQualificationToCommitteeApprove(int tenderId, string agencycode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender PreQualification = await _qualificationQueries.GetQualificationById(tenderId);
            if (PreQualification.AgencyCode != agencycode)
                throw new AuthorizationException();

            await _qualificationDomainService.IsValidToSendQualificationForApprovementFromCommitteeManager(PreQualification);
            PreQualification.UpdateTenderStatus(Enums.TenderStatus.PendingQualificationCommitteeManagerApproval, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.UpdatePreQualification);
            #region [Send Notification]
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { PreQualification.ReferenceNumber },
                SubjectEmailArgs = new object[] { PreQualification.ReferenceNumber },
                PanelArgs = new object[] { PreQualification.ReferenceNumber },
                SMSArgs = new object[] { PreQualification.ReferenceNumber }
            };
            #endregion
            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments, $"Qualification/PreQualificationApproval?qualificationIdString={Util.Encrypt(PreQualification.TenderId)}", NotificationEntityType.PreQualification, PreQualification.TenderId.ToString(), PreQualification.BranchId);
            await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.PreQualificationManager.OperationsOnPreQualification.PendingApproveCreateQualification, PreQualification.PreQualificationCommitteeId, mainNotificationTemplateModel);
            var result = await _qualificationCommands.UpdatePreQualificationAsync(PreQualification);
            return result;
        }

        public async Task<Tender> ApproveQualificationFromQualificationSecritary(int tenderId, string agencycode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender PreQualification = await _qualificationQueries.GetQualificationByIdWithChangRequest(tenderId);
            if (PreQualification.ChangeRequests.Any(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling))
            {
                PreQualification.DeleteAllCancelRequest();
            }
            await _qualificationDomainService.IsValidToAcceptQualificationFromCommitteSecrtary(PreQualification);
            PreQualification.UpdateTenderStatus(Enums.TenderStatus.QualificationUnderEstablishingFromCommittee, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.UpdatePreQualification);
            var result = await _qualificationCommands.UpdatePreQualificationAsync(PreQualification);
            return result;
        }

        public async Task SendPreQualificationToApproveNotification(Tender PreQualification)
        {

            #region [Send Notification]
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { PreQualification.ReferenceNumber },
                SubjectEmailArgs = new object[] { PreQualification.ReferenceNumber },
                PanelArgs = new object[] { PreQualification.ReferenceNumber },
                SMSArgs = new object[] { PreQualification.ReferenceNumber }
            };
            #endregion
            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments, $"Qualification/PreQualificationApproval?qualificationIdString={Util.Encrypt(PreQualification.TenderId)}", NotificationEntityType.PreQualification, PreQualification.TenderId.ToString(), PreQualification.BranchId);
            if (PreQualification.TenderTypeId == (int)Enums.TenderType.PreQualification)
            {
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.Auditor.OperationsNeedApprove.submitPrequalificationforapproval, PreQualification.BranchId, mainNotificationTemplateModel);
            }
            else
            {
                if (PreQualification.OffersCheckingCommitteeId != null)
                {
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckManager.OperationsOnPostqualification.SubmitPostQualificationForApproval, PreQualification.OffersCheckingCommitteeId, mainNotificationTemplateModel);
                }
                else
                {
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseManager.OperationsOnPostqualification.SubmitPostQualificationForApproval, PreQualification.DirectPurchaseCommitteeId, mainNotificationTemplateModel);
                }
            }

        }

        public async Task<Tender> ApprovePreQualification(int tenderId, string verficationCode, int typeId, string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            await _verification.CheckForVerificationCode(tenderId, verficationCode, typeId);

            Tender preQualification = await _qualificationQueries.GetQualificationById(tenderId);
            if (preQualification.AgencyCode != agencyCode)
                throw new AuthorizationException();

            await _qualificationDomainService.IsValidToApproveQualification(preQualification);
            preQualification.UpdateTenderStatus(Enums.TenderStatus.QualificationCommitteeApproval, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.UpdatePreQualification);
            var result = await _qualificationCommands.UpdatePreQualificationAsync(preQualification);

            #region NT

            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { preQualification.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { preQualification.ReferenceNumber },
                SMSArgs = new object[] { preQualification.ReferenceNumber }
            };
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(NotificationArguments, $"Qualification/PreQualificationDetails?QualificationId={Util.Encrypt(preQualification.TenderId)}", NotificationEntityType.Tender, preQualification.TenderId.ToString(), preQualification.BranchId);
            if (preQualification.PreQualificationCommitteeId != null)
            {
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.PreQualificationSecretary.OperationsOnPreQualification.PendingCreateQualificationRequest, preQualification.PreQualificationCommitteeId, approveTender);
            }
            if (preQualification.TenderTypeId == (int)Enums.TenderType.PreQualification)
            {
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.OperationsOnTheTender.approvedprequalification, preQualification.BranchId, approveTender);
            }
            else
            {
                if (preQualification.OffersCheckingCommitteeId != null)
                {
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.OperationsOnPostqualification.ApprovePostQualification, preQualification.OffersCheckingCommitteeId, approveTender);
                }
                else
                {
                    if (preQualification.IsLowBudgetDirectPurchase == null || preQualification.IsLowBudgetDirectPurchase == false)
                        await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseSecretary.OperationsOnQualification.ApprovePostQualification, preQualification.DirectPurchaseCommitteeId, approveTender);
                }
            }


            #endregion

            return result;
        }

        public async Task<Tender> ApprovePreQualificationFromCommitteeManager(int tenderId, string verficationCode, int typeId, string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            await _verification.CheckForVerificationCode(tenderId, verficationCode, typeId);
            Tender PreQualification = await _qualificationQueries.GetQualificationById(tenderId);
            await _qualificationDomainService.IsValidToApprovePreQualificationFromCommitteeManager(PreQualification, agencyCode);
            PreQualification.UpdateTenderStatus(Enums.TenderStatus.Approved, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.UpdatePreQualification);
            PreQualification.UpdateSubmitionDate();
            var result = await _qualificationCommands.UpdatePreQualificationAsync(PreQualification);

            #region NT
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { PreQualification.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { PreQualification.ReferenceNumber },
                SMSArgs = new object[] { PreQualification.ReferenceNumber }
            };
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(NotificationArguments, $"Qualification/PreQualificationDetails?QualificationId={Util.Encrypt(PreQualification.TenderId)}", NotificationEntityType.Tender, PreQualification.TenderId.ToString(), PreQualification.BranchId);
            await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.PreQualificationSecretary.OperationsOnPreQualification.ApproveCreateQualificationRequestFromManager, PreQualification.PreQualificationCommitteeId, approveTender);
            if (PreQualification.TenderTypeId == (int)Enums.TenderType.PostQualification)
            {
                var suppliers = _qualificationQueries.GetPostQualificationSuppliers(tenderId);
                MainNotificationTemplateModel mainNotificationTemplateSupplierModel = new MainNotificationTemplateModel(NotificationArguments, $"Qualification/ApplyPreQualificationDocument?qualificationId={Util.Encrypt(PreQualification.TenderId)}", NotificationEntityType.PostQualification, PreQualification.TenderId.ToString(), PreQualification.BranchId, PreQualification.OffersCheckingCommitteeId ?? PreQualification.DirectPurchaseCommitteeId);
                await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnPostqualification.ApprovePostQualification, suppliers, mainNotificationTemplateSupplierModel);
            }
            #endregion

            return result;
        }

        public async Task<Tender> RejectApprovePreQualification(int tenderId, string rejectionReason, string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender preQualification = await _qualificationQueries.GetQualificationById(tenderId);
            if (preQualification.AgencyCode != agencyCode)
                throw new AuthorizationException();
            await _qualificationDomainService.IsValidToRejectQualification(preQualification);
            preQualification.UpdateTenderStatus(Enums.TenderStatus.Rejected, rejectionReason, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.UpdatePreQualification);
            var result = await _qualificationCommands.UpdatePreQualificationAsync(preQualification);
            #region NT

            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { preQualification.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { preQualification.ReferenceNumber },
                SMSArgs = new object[] { preQualification.ReferenceNumber }
            };
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(NotificationArguments, $"Qualification/PreQualificationApproval?qualificationIdString={Util.Encrypt(preQualification.TenderId)}", NotificationEntityType.PreQualification, preQualification.TenderId.ToString(), preQualification.BranchId);
            if (preQualification.TenderTypeId == (int)Enums.TenderType.PreQualification)
            {
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.OperationsOnQualification.RejectPreQualification, preQualification.BranchId, approveTender);
            }
            else
            {
                if (preQualification.OffersCheckingCommitteeId != null)
                {
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.OperationsOnPostqualification.RejectingApprovePostQualification, preQualification.OffersCheckingCommitteeId, approveTender);
                }
                else
                {
                    if (preQualification.IsLowBudgetDirectPurchase.HasValue && preQualification.IsLowBudgetDirectPurchase.Value)
                    {
                        UserProfile userProfile = await _qualificationDomainService.GetDirectPurchaseMemberProfile(preQualification.DirectPurchaseMemberId.Value);
                        await _notificationAppService.AddNotificationSettingByUserId(NotificationOperations.DirectPurchaseMember.OperationsOnTheTender.RejectingApprovePostQualificationLowBudget, userProfile, (int)Enums.UserRole.CR_DirectPurchaseMember);
                        await _notificationAppService.SendNotificationByUserId(NotificationOperations.DirectPurchaseMember.OperationsOnTheTender.RejectingApprovePostQualificationLowBudget, preQualification.DirectPurchaseMemberId.Value, userProfile.UserName, approveTender);
                    }
                    else
                        await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseSecretary.OperationsOnQualification.RejectingApprovePostQualification, preQualification.DirectPurchaseCommitteeId, approveTender);
                }
            }
            #endregion
            return result;
        }

        public async Task<Tender> RejectApprovePreQualificationFromCommitteeManager(int tenderId, string rejectionReason, string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender PreQualification = await _qualificationQueries.GetQualificationById(tenderId);
            if (PreQualification.AgencyCode != agencyCode)
                throw new AuthorizationException();
            if (PreQualification.TenderStatusId != (int)Enums.TenderStatus.PendingQualificationCommitteeManagerApproval)
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.NotAllaowedToRejectApproveQualification);

            PreQualification.UpdateTenderStatus(Enums.TenderStatus.RejectedQualificationApprovalByCommitteeManager, rejectionReason, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.UpdatePreQualification);
            var result = await _qualificationCommands.UpdatePreQualificationAsync(PreQualification);

            #region NT

            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { PreQualification.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { PreQualification.ReferenceNumber },
                SMSArgs = new object[] { PreQualification.ReferenceNumber }
            };
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(NotificationArguments, $"Qualification/PreQualificationApproval?qualificationIdString={Util.Encrypt(PreQualification.TenderId)}", NotificationEntityType.PreQualification, PreQualification.TenderId.ToString(), PreQualification.BranchId);
            await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.PreQualificationSecretary.OperationsOnPreQualification.RejectCreateQualificationRequestFromManager, PreQualification.PreQualificationCommitteeId, approveTender);
            #endregion

            return result;
        }

        public async Task<Tender> ReopenPreQualification(int tenderId, string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _qualificationQueries.GetQualificationById(tenderId);
            if (tender.AgencyCode != agencyCode)
                throw new AuthorizationException();

            await _qualificationDomainService.IsValidToReopenQualification(tender);
            tender.UpdateTenderStatus(Enums.TenderStatus.Established, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.UpdatePreQualification);
            var result = await _qualificationCommands.UpdatePreQualificationAsync(tender);
            return result;
        }

        public async Task<Tender> ReopenPreQualificationFromCommitteeSecrtary(int tenderId, string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _qualificationQueries.GetQualificationById(tenderId);
            if (tender.AgencyCode != agencyCode)
                throw new AuthorizationException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.RejectedQualificationApprovalByCommitteeManager)
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.NotAllaowedToReopenQualification);
            tender.UpdateTenderStatus(Enums.TenderStatus.QualificationUnderEstablishingFromCommittee, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.UpdatePreQualification);
            var result = await _qualificationCommands.UpdatePreQualificationAsync(tender);
            return result;
        }

        public async Task<TenderDatesModel> FindQualificationDatesByQualificationId(int qualificationId)
        {
            TenderDatesModel tenderDatesModel = await _qualificationQueries.FindQualificationDatesByQualificationId(qualificationId);
            if (tenderDatesModel == null)
            {
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            }

            return tenderDatesModel;
        }
        public async Task<bool> AddTenderToSupplierTendersFavouriteList(int tenderId, string cr)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());

            Tender tender = await _qualificationQueries.GetPreQualificationDetailsById(tenderId);
            Check.ArgumentNotNull(nameof(tender), tender);

            FavouriteSupplierTender favouriteTender;

            if (tender.FavouriteSupplierTenders != null)
            {
                if (tender.FavouriteSupplierTenders.Any(t => string.Equals(t.SupplierCRNumber, cr) && t.TenderId == tenderId))
                {
                    favouriteTender = tender.FavouriteSupplierTenders.FirstOrDefault(t => string.Equals(t.SupplierCRNumber, cr) && t.TenderId == tenderId);
                    favouriteTender.SetActive();
                    await _qualificationCommands.UpdateTenderFavouriteListAsync(favouriteTender);
                    return true;
                }
            }

            FavouriteSupplierTender favouriteSupplierTender = new FavouriteSupplierTender(tenderId, cr);
            await _qualificationCommands.CreateTenderFavouriteAsync(favouriteSupplierTender);
            return true;
        }


        public async Task StartPreQualificationCheckingOffersAsync(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _qualificationQueries.GetQualificationById(tenderId);
            _qualificationDomainService.IsValidToStartCheckingTender(tender);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersCheckedPending, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.UpdatePreQualification); var result = await _qualificationCommands.UpdateAsync(tender);
        }

        public async Task ReopenPreQualificationChecking(int tenderId, string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _qualificationQueries.GetPostQualificationById(tenderId);
            if (tender.AgencyCode != agencyCode)
                throw new AuthorizationException();
            _qualificationDomainService.IsValidToReopenCheckTender(tender);
            tender.UpdateTenderStatus(Enums.TenderStatus.DocumentChecking, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.UpdatePreQualification);
            await _qualificationCommands.UpdateAsync(tender);
        }

        public async Task SendPreQualificationToApproveChecking(int tenderId, string agencyCode)
        {
            Tender PreQualification = await _qualificationQueries.GetQualificationById(tenderId);
            if (PreQualification.AgencyCode != agencyCode)
                throw new AuthorizationException();

            _qualificationDomainService.IsValidToSendTenderToApproveChecking(PreQualification);
            PreQualification.UpdateTenderStatus(Enums.TenderStatus.DocumentCheckPending, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.UpdatePreQualification);
            await _qualificationCommands.UpdateAsync(PreQualification);

            #region NT
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { PreQualification.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { PreQualification.ReferenceNumber },
                SMSArgs = new object[] { PreQualification.ReferenceNumber }
            };
            MainNotificationTemplateModel RejectPreQualification = new MainNotificationTemplateModel(NotificationArguments, $"Qualification/CheckPreQualification?tenderIdString={Util.Encrypt(PreQualification.TenderId)}", NotificationEntityType.PreQualification, PreQualification.TenderId.ToString(), null, PreQualification.PreQualificationCommitteeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.PreQualificationManager.OperationsOnPreQualification.SubmitPreQualificationForCheckingApproval, PreQualification.PreQualificationCommitteeId, RejectPreQualification);
            #endregion
        }

        public async Task ApprovePreQualificationChecking(int tenderId, string verificarionCode, int typeId, string agencyCode, List<string> roles)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            await _verification.CheckForVerificationCode(tenderId, verificarionCode, typeId);
            Tender preQualification = await _qualificationQueries.GetQualificationByIdWithChangRequest(tenderId);
            if (preQualification.AgencyCode != agencyCode)
                throw new AuthorizationException();

            _qualificationDomainService.IsValidToApproveTenderChecking(preQualification);
            preQualification.UpdateTenderStatus(Enums.TenderStatus.DocumentCheckConfirmed, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.UpdatePreQualification); preQualification.DeletePendingCancelRequests(tenderId);
            await _qualificationCommands.UpdateAsync(preQualification);
            await SendNotificationForCheckingApproved(preQualification);
        }

        private async Task SendNotificationForCheckingApproved(Tender PreQualification)
        {
            List<QualificationFinalResult> allSuppliers = _qualificationQueries.GetSupplierResultForQualification(PreQualification.TenderId);
            List<string> acceptedSuppliers = allSuppliers.Where(e => e.QualificationLookupId == (int)Enums.QualificationResultLookup.Succeeded).Select(x => x.SupplierSelectedCr).ToList();
            List<string> rejectedSuppliers = allSuppliers.Where(e => e.QualificationLookupId == (int)Enums.QualificationResultLookup.Failed).Select(x => x.SupplierSelectedCr).ToList();
            #region NT
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { PreQualification.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { PreQualification.ReferenceNumber },
                SMSArgs = new object[] { PreQualification.ReferenceNumber }
            };
            MainNotificationTemplateModel ApprovePreQualification = new MainNotificationTemplateModel(NotificationArguments, $"Qualification/CheckPreQualification?tenderIdString={Util.Encrypt(PreQualification.TenderId)}", NotificationEntityType.PreQualification, PreQualification.TenderId.ToString(), null, PreQualification.PreQualificationCommitteeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.PreQualificationSecretary.OperationsOnPreQualification.ApprovePreQualificationChecking, PreQualification.PreQualificationCommitteeId, ApprovePreQualification);
            #endregion
            #region Accepted-Suppliers
            if (acceptedSuppliers.Count() > 0)
            {

                NotificationArguments acceptedSuppliersNotificationArguments = new NotificationArguments
                {
                    BodyEmailArgs = new object[] { PreQualification.ReferenceNumber, Resources.OfferResources.DisplayInputs.Matching },
                    SubjectEmailArgs = new object[] { },
                    PanelArgs = new object[] { PreQualification.ReferenceNumber, Resources.OfferResources.DisplayInputs.Matching },
                    SMSArgs = new object[] { PreQualification.ReferenceNumber, Resources.OfferResources.DisplayInputs.Matching }
                };
                MainNotificationTemplateModel acceptedSuppliersTemplate = new MainNotificationTemplateModel(acceptedSuppliersNotificationArguments, $"Qualification/PreQualificationDetails?QualificationId={Util.Encrypt(PreQualification.TenderId)}", NotificationEntityType.PreQualification, PreQualification.TenderId.ToString(), PreQualification.BranchId);
                await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnPrequalification.approvecheckprequalification, acceptedSuppliers, acceptedSuppliersTemplate);
            }
            #endregion Accepted-Suppliers
            #region Rejected-Suppliers
            if (rejectedSuppliers.Count() > 0)
            {
                foreach (var rejectedSupplier in rejectedSuppliers)
                {
                    var rejectionReason = !string.IsNullOrEmpty(allSuppliers.Where(a => a.SupplierSelectedCr == rejectedSupplier).FirstOrDefault().FailingReason) ?
                        allSuppliers.Where(a => a.SupplierSelectedCr == rejectedSupplier).FirstOrDefault().FailingReason : "لم يجتاز الاختبار";
                    NotificationArguments rejectedSuppliersNotificationArguments = new NotificationArguments
                    {
                        BodyEmailArgs = new object[] { PreQualification.ReferenceNumber, Resources.OfferResources.DisplayInputs.NotMatching, rejectionReason },
                        SubjectEmailArgs = new object[] { },
                        PanelArgs = new object[] { PreQualification.ReferenceNumber, Resources.OfferResources.DisplayInputs.NotMatching, rejectionReason },
                        SMSArgs = new object[] { PreQualification.ReferenceNumber, Resources.OfferResources.DisplayInputs.NotMatching, rejectionReason }
                    };
                    MainNotificationTemplateModel rejectedSuppliersTemplate = new MainNotificationTemplateModel(rejectedSuppliersNotificationArguments, $"Qualification/PreQualificationDetails?QualificationId={Util.Encrypt(PreQualification.TenderId)}", NotificationEntityType.PreQualification, PreQualification.TenderId.ToString(), PreQualification.BranchId);
                    await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnPrequalification.rejectcheckprequalification, new List<string> { rejectedSupplier }, rejectedSuppliersTemplate);
                }
            }
            #endregion Rejected-Suppliers
        }

        public async Task RejectCheckPreQualificationOffers(int tenderId, string rejectionReason)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender PreQualification = await _qualificationQueries.GetQualificationById(tenderId);
            _qualificationDomainService.IsValidToRejectCheckTender(PreQualification);
            PreQualification.UpdateTenderStatus(Enums.TenderStatus.DocumentCheckRejected, rejectionReason, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.UpdatePreQualification);
             await _qualificationCommands.UpdateAsync(PreQualification);
            #region NT
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { PreQualification.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { PreQualification.ReferenceNumber },
                SMSArgs = new object[] { PreQualification.ReferenceNumber }
            };
            MainNotificationTemplateModel RejectPreQualification = new MainNotificationTemplateModel(NotificationArguments, $"Qualification/CheckPreQualification?tenderIdString={Util.Encrypt(PreQualification.TenderId)}", NotificationEntityType.PreQualification, PreQualification.TenderId.ToString(), null, PreQualification.PreQualificationCommitteeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.PreQualificationSecretary.OperationsOnPreQualification.RejectingApprovePreQualificationChecking, PreQualification.PreQualificationCommitteeId, RejectPreQualification);
            #endregion
        }

        #endregion

        #region Post-Qualification

        public async Task<Tender> CreatePoatQualification(PostQualificationApplyDocumentsModel model, int userId, string agencyCode)
        {
            await _qualificationDomainService.IsValidToCreatePostQualification(model);
            await _qualificationDomainService.CanAddPostqualificationIfTenderHasExtendOfferValidity(model.PostQualificationTenderId.Value);
            await _qualificationDomainService.CheckIfSupplierHavePostQualificationBefore(model.CommercialNumbers, model.PostQualificationTenderId.Value);
            List<TenderAttachment> attachments = new List<TenderAttachment>();
            foreach (var item in model.Attachments)
            {
                attachments.Add(new TenderAttachment(item.Name, item.FileNetReferenceId, item.AttachmentTypeId));
            }

            if (model.postQualificationIdString == null)
            {
                var tender = await _qualificationQueries.GetPostQualificationById(model.PostQualificationTenderId.Value);
                Tender qualification = new Tender(model.TenderId, (int)Enums.TenderType.PostQualification, model.TenderName, model.TechnicalOrganizationId, model.OffersCheckingCommitteeId, model.OffersDirectPurchaseCommitteeId, model.LastEnqueriesDate.Value, model.LastOfferPresentationDate.Value
                    , model.OffersCheckingDate, null, (int)Enums.TenderStatus.Established, agencyCode, tender.BranchId, model.PostQualificationTenderId.Value, model.PreQualificationCommitteeId, tender.IsLowBudgetDirectPurchase, tender.DirectPurchaseMemberId);

                qualification.UpdateTenderRelations(model.TenderAreaIDs, model.TenderActivitieIDs, model.TenderConstructionWorkIDs, model.TenderMentainanceRunnigWorkIDs, model.InsideKSA, model.Details, model.ActivityDescription, /*userId,*/ model.TenderCountriesIDs, false);
                qualification.UpdateAttachments(attachments, userId, false);
                qualification.AddActionHistory((int)Enums.TenderStatus.Established, TenderActions.CreatePostQualification, "", userId);
                qualification.AddPostQualificationInvitations(model.CommercialNumbers);
                qualification.SetReferenceNumber(await _qualificationCommands.UpdateReferenceNumber());
                var newQualification = await _genericCommandRepository.CreateAsync(qualification);
                await _genericCommandRepository.SaveAsync();


                var oldPostQualification = await _qualificationQueries.GetPostQualificationByTenderId(tender.TenderId);
                if (oldPostQualification != null)
                {
                    model.FinancialCapacity = oldPostQualification.FinancialCapacity;
                    model.QualificationTypeId = oldPostQualification.QualificationTypeId;
                    newQualification.SetPointToPassToPostQualification(oldPostQualification.TenderPointsToPass, oldPostQualification.TechnicalAdministrativeCapacity, oldPostQualification.FinancialCapacity, oldPostQualification.QualificationTypeId);
                    switch (oldPostQualification.QualificationTypeId)
                    {
                        case (int)Enums.PreQualificationType.Small:
                            model.QualificationSmallEvaluation = await GetQualificationSmallEvaluation(oldPostQualification.TenderId);
                            break;
                        case (int)Enums.PreQualificationType.Medium:
                            model.QualificationMediumEvaluation = await GetQualificationMediumEvaluation(oldPostQualification.TenderId);
                            break;
                        case (int)Enums.PreQualificationType.Large:
                            model.QualificationLargeEvaluation = await GetQualificationLargeEvaluation(oldPostQualification.TenderId);
                            break;
                    }
                    await CreateQualificationConfiguration(model, newQualification);
                    await _genericCommandRepository.SaveAsync();

                }
                return qualification;
            }
            else
            {
                var qualificationEdit = await _qualificationQueries.FindPreQualificationByIdandStatus(Util.Decrypt(model.postQualificationIdString));
                if ((qualificationEdit.TenderStatusId != (int)Enums.TenderStatus.UnderEstablishing && qualificationEdit.TenderStatusId != (int)Enums.TenderStatus.Established && qualificationEdit.TenderStatusId != (int)Enums.TenderStatus.QualificationUnderEstablishingFromCommittee) || agencyCode != qualificationEdit.AgencyCode)
                    throw new AuthorizationException();
                if (_httpContextAccessor.HttpContext.User.UserRole() == RoleNames.PreQualificationCommitteeSecretary)
                {

                    var oldPostQualification = await _qualificationQueries.GetPostQualificationByTenderId(qualificationEdit.PostQualificationTenderId.Value);
                    if (oldPostQualification == null)
                    {
                        await _qualificationDomainService.CheckMasterWeightForQualification(model);
                        qualificationEdit.DeleteQualificationConfigurations();
                        qualificationEdit.DeleteQualificationSubCategoryConfigurations();
                        qualificationEdit.SetPointToPassToQualification(model.TenderPointsToPass, model.TechnicalAdministrativeCapacity, model.FinancialCapacity, model.QualificationTypeId);
                        await CreateQualificationConfiguration(model, qualificationEdit);
                    }
                    qualificationEdit.UpdateQualificationDataFromSecrary(model, attachments, _httpContextAccessor.HttpContext.User.UserId());
                }
                else
                {
                    qualificationEdit.UpdatePostQualificationData(model, attachments, _httpContextAccessor.HttpContext.User.UserId());
                }
                _genericCommandRepository.Update(qualificationEdit);
                await _genericCommandRepository.SaveAsync();
                return qualificationEdit;
            }


        }

        public async Task CheckIfSupplierHavePostQualificationBefore(List<string> crs, int tenderId)
        {

            await _qualificationDomainService.CheckIfSupplierHavePostQualificationBefore(crs, tenderId);
        }

        public async Task checkPostQualificationSupplierBlock(List<string> Crs)
        {
            if (Crs != null && Crs.Count > 0)
            {
                foreach (var item in Crs)
                {
                    await _supplierQualificationDocumentDomainService.CheckIfSupplierBlocked(item);
                }
            }
        }

        public async Task<PostQualificationApplyDocumentsModel> getQualificationDataToCreatePostQualification(string tenderId, string qualificationId)
        {
            PostQualificationApplyDocumentsModel qualification = await _qualificationQueries.getQualificationDataToCreatePostQualification(tenderId, qualificationId);
            var oldPostQualification = await _qualificationQueries.GetPostQualificationByTenderId(tenderId == null ? qualification.PostQualificationTenderId.Value : Util.Decrypt(tenderId));
            if (oldPostQualification != null)
            {
                qualification.HasOldPostQualification = true;
            }
            switch (qualification.QualificationTypeId)
            {
                case (int)Enums.PreQualificationType.Small:
                    qualification.QualificationSmallEvaluation = await GetQualificationSmallEvaluation(Util.Decrypt(qualificationId));
                    break;
                case (int)Enums.PreQualificationType.Medium:
                    qualification.QualificationMediumEvaluation = await GetQualificationMediumEvaluation(Util.Decrypt(qualificationId));
                    break;
                case (int)Enums.PreQualificationType.Large:
                    qualification.QualificationLargeEvaluation = await GetQualificationLargeEvaluation(Util.Decrypt(qualificationId));
                    break;
            }
            return qualification;
        }

        public async Task<PostQualificationApplyDocumentsModel> GetPostQualificationData(int qualificationId)
        {
            PostQualificationApplyDocumentsModel qualification = await _qualificationQueries.GetPostQualificationData(qualificationId);
            switch (qualification.QualificationTypeId)
            {
                case (int)Enums.PreQualificationType.Small:
                    qualification.QualificationSmallEvaluation = await GetQualificationSmallEvaluation(qualificationId);
                    break;
                case (int)Enums.PreQualificationType.Medium:
                    qualification.QualificationMediumEvaluation = await GetQualificationMediumEvaluation(qualificationId);
                    break;
                case (int)Enums.PreQualificationType.Large:
                    qualification.QualificationLargeEvaluation = await GetQualificationLargeEvaluation(qualificationId);
                    break;
            }
            return qualification;
        }

        #endregion

        #region Post-Qualification-Approvement

        public async Task<Tender> SendPostQualificationToApprove(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _qualificationQueries.GetPostQualificationById(tenderId);
            await _qualificationDomainService.IsValidToSendQualificationForApprovement(tender);
            tender.UpdateTenderStatus(Enums.TenderStatus.Pending, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.SendPostQualificationForApprove);
            await RequestingApprovePostQualificationNotification(tender);
            return await _qualificationCommands.UpdatePreQualificationAsync(tender);
        }

        private async Task RequestingApprovePostQualificationNotification(Tender postQualification)
        {
            #region [Send Notification]
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { postQualification.ReferenceNumber },
                SubjectEmailArgs = new object[] { postQualification.ReferenceNumber },
                PanelArgs = new object[] { postQualification.ReferenceNumber },
                SMSArgs = new object[] { postQualification.ReferenceNumber }
            };
            #endregion
            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments, $"Qualification/PostQualificationApproval?qualificationIdString={Util.Encrypt(postQualification.TenderId)}", NotificationEntityType.PostQualification, postQualification.TenderId.ToString(), postQualification.BranchId, postQualification.OffersCheckingCommitteeId ?? postQualification.DirectPurchaseCommitteeId);
            if (postQualification.OffersCheckingCommitteeId != null)
            {
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckManager.OperationsOnPostqualification.SubmitPostQualificationForApproval, postQualification.OffersCheckingCommitteeId, mainNotificationTemplateModel);
            }
            else
            {
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseManager.OperationsOnPostqualification.SubmitPostQualificationForApproval, postQualification.DirectPurchaseCommitteeId, mainNotificationTemplateModel);
            }

        }

        public async Task<Tender> ApprovePostQualification(int tenderId, string agencyCode, string verficationCode, List<string> roles)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            bool check = await _verification.CheckForVerificationCode(tenderId, verficationCode, (int)Enums.VerificationType.Tender);
            Tender tender = await _qualificationQueries.GetPostQualificationById(tenderId);
            if (tender.AgencyCode != agencyCode)
            {
                throw new UnHandledAccessException();
            }
            await _qualificationDomainService.IsValidToApproveQualification(tender);
            tender.UpdateTenderStatus(Enums.TenderStatus.QualificationCommitteeApproval, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ApprovePostQualification);
            var suppliers = _qualificationQueries.GetPostQualificationSuppliers(tenderId);
            await AcceptingApprovingPostQualificationNotification(tender, suppliers, roles);
            tender.UpdateSubmitionDate();
            return await _qualificationCommands.UpdatePreQualificationAsync(tender);
        }

        private async Task AcceptingApprovingPostQualificationNotification(Tender postQualification, List<string> suppliers, List<string> roles)
        {
            #region [Send Notification]
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { postQualification.ReferenceNumber },
                SubjectEmailArgs = new object[] { postQualification.ReferenceNumber },
                PanelArgs = new object[] { postQualification.ReferenceNumber },
                SMSArgs = new object[] { postQualification.ReferenceNumber }
            };
            #endregion
            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments, $"Qualification/ApplyPreQualificationDocument?qualificationId={Util.Encrypt(postQualification.TenderId)}", NotificationEntityType.PostQualification, postQualification.TenderId.ToString(), postQualification.BranchId, postQualification.OffersCheckingCommitteeId ?? postQualification.DirectPurchaseCommitteeId);

            MainNotificationTemplateModel mainNotificationTemplateModelForSecretary = new MainNotificationTemplateModel(NotificationArguments, $"Qualification/PostQualificationApproval?qualificationIdString={Util.Encrypt(postQualification.TenderId)}", NotificationEntityType.PostQualification, postQualification.TenderId.ToString(), postQualification.BranchId, postQualification.OffersCheckingCommitteeId ?? postQualification.DirectPurchaseCommitteeId);

            if (postQualification.OffersCheckingCommitteeId != null)
            {
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.OperationsOnPostqualification.ApprovePostQualification, postQualification.OffersCheckingCommitteeId, mainNotificationTemplateModelForSecretary);
            }
            else
            {
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseSecretary.OperationsOnQualification.ApprovePostQualification, postQualification.DirectPurchaseCommitteeId, mainNotificationTemplateModelForSecretary);
            }
            if (postQualification.PreQualificationCommitteeId != null)
            {
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.PreQualificationSecretary.OperationsOnPreQualification.PendingCreateQualificationRequest, postQualification.PreQualificationCommitteeId, mainNotificationTemplateModelForSecretary);
            }
        }

        public async Task<Tender> RejectApprovePostQualification(int tenderId, string rejectionReason, string agencyCode, List<string> roles)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _qualificationQueries.GetPostQualificationById(tenderId);
            if (tender.AgencyCode != agencyCode)
            {
                throw new UnHandledAccessException();
            }
            await _qualificationDomainService.IsValidToRejectQualification(tender);
            tender.UpdateTenderStatus(Enums.TenderStatus.Rejected, rejectionReason, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.RejectApprovePostQualification);
            await RejectingApprovingPostQualificationNotification(tender, roles);
            return await _qualificationCommands.UpdatePreQualificationAsync(tender);
        }

        private async Task RejectingApprovingPostQualificationNotification(Tender postQualification, List<string> roles)
        {
            #region [Send Notification]
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { postQualification.ReferenceNumber },
                SubjectEmailArgs = new object[] { postQualification.ReferenceNumber },
                PanelArgs = new object[] { postQualification.ReferenceNumber },
                SMSArgs = new object[] { postQualification.ReferenceNumber }
            };
            #endregion


            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments, $"Qualification/PostQualificationApproval?qualificationIdString={Util.Encrypt(postQualification.TenderId)}", NotificationEntityType.PostQualification, postQualification.TenderId.ToString(), postQualification.BranchId, postQualification.OffersCheckingCommitteeId ?? postQualification.DirectPurchaseCommitteeId);
            if (postQualification.OffersCheckingCommitteeId != null)
            {
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.OperationsOnPostqualification.RejectingApprovePostQualification, postQualification.OffersCheckingCommitteeId, mainNotificationTemplateModel);
            }
            else
            {
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseSecretary.OperationsOnQualification.RejectingApprovePostQualification, postQualification.DirectPurchaseCommitteeId, mainNotificationTemplateModel);
            }
        }

        public async Task<Tender> ReopenPostQualification(int tenderId, string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _qualificationQueries.GetPostQualificationById(tenderId);
            if (tender.AgencyCode != agencyCode)
            {
                throw new UnHandledAccessException();
            }
            await _qualificationDomainService.IsValidToReopenQualification(tender);
            tender.UpdateTenderStatus(Enums.TenderStatus.Established, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ReopenPostQualification);
            return await _qualificationCommands.UpdatePreQualificationAsync(tender);
        }

        #endregion

        #region Post-Qualification-Checking

        public async Task<PreQualificationBasicDetailsModel> GetPostQualificationDetailsByIdForChecking(int qualificationId, int userId, List<string> roles)
        {

            Check.ArgumentNotNullOrEmpty(nameof(qualificationId), qualificationId.ToString());
            Tender qualification = await _qualificationQueries.GetQualificationById(qualificationId);
            _qualificationDomainService.IsValidToAccecssPostQualificationCheckingTender(qualification, roles);
            if (qualification != null && qualification.TenderStatusId == (int)Enums.TenderStatus.Approved)
            {
                await _qualificationDomainService.CheckQualificationDateValidation(qualification);
                qualification.UpdateTenderStatus(Enums.TenderStatus.DocumentChecking);
                await _qualificationCommands.UpdateAsync(qualification);
            }
            var result = await _qualificationQueries.GetQualificationForCheckPostQualificationByQualificationId(qualificationId, userId, _httpContextAccessor.HttpContext.User.UserAgency(), roles);
            Check.ArgumentNotNullOrEmpty(nameof(result), result.ToString());
            return result;
        }

        public async Task SendPostQualificationToApproveChecking(int tenderId, List<string> roles)
        {
            Tender tender = await _qualificationQueries.GetPostQualificationById(tenderId);
            _qualificationDomainService.IsValidToSendTenderToApproveChecking(tender, roles);
            tender.UpdateTenderStatus(Enums.TenderStatus.DocumentCheckPending, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.SendPostQulaificationForApproveChecking); 
            
            await SendNotificationForCheckingPostQualificationApproval(tender, roles);
            await _qualificationCommands.UpdateAsync(tender);
        }

        private async Task SendNotificationForCheckingPostQualificationApproval(Tender postQualification, List<string> roles)
        {

            #region [Send Notification]
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { postQualification.ReferenceNumber },
                SubjectEmailArgs = new object[] { postQualification.ReferenceNumber },
                PanelArgs = new object[] { postQualification.ReferenceNumber },
                SMSArgs = new object[] { postQualification.ReferenceNumber }
            };
            #endregion
            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments, $"Qualification/CheckPostQualification?tenderIdString={Util.Encrypt(postQualification.TenderId)}", NotificationEntityType.PostQualification, postQualification.TenderId.ToString(), postQualification.BranchId, postQualification.OffersCheckingCommitteeId ?? postQualification.DirectPurchaseCommitteeId);

            if (postQualification.OffersCheckingCommitteeId != null)
            {
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckManager.OperationsOnPostqualification.SubmitPostQualificationForCheckingApproval, postQualification.OffersCheckingCommitteeId, mainNotificationTemplateModel);
            }
            else
            {
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseManager.OperationsOnPostqualification.SubmitPostQualificationForCheckingApproval, postQualification.DirectPurchaseCommitteeId, mainNotificationTemplateModel);
            }
        }

        public async Task ApprovePostQualificationChecking(int tenderId, List<string> roles)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _qualificationQueries.GetQualificationById(tenderId);
            _qualificationDomainService.IsValidToApproveTenderChecking(tender, roles);
            tender.UpdateTenderStatus(Enums.TenderStatus.DocumentCheckConfirmed, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ApprovePostQualificationChecking);
           
            await SendNotificationForCheckingPostQualificationApproved(tender, roles);
            await _qualificationCommands.UpdateAsync(tender);
        }

        private async Task SendNotificationForCheckingPostQualificationApproved(Tender postQualification, List<string> roles)
        {

            int? CommitteeId = (roles.Any(s => RoleNames.OffersCheckManager.ToString().Contains(s)) ? postQualification.OffersCheckingCommitteeId : postQualification.DirectPurchaseCommitteeId);

            var CheckingCommetee = await _qualificationQueries.GetCheckingCommitteeMembersOnTender(CommitteeId.Value);
            IEnumerable<QualificationFinalResult> allSuppliers = _qualificationQueries.GetSupplierResultForQualification(postQualification.TenderId);
            List<string> acceptedSuppliers = allSuppliers.Where(e => e.QualificationLookupId == (int)Enums.QualificationResultLookup.Succeeded).Select(x => x.SupplierSelectedCr).ToList();
            List<string> rejectedSuppliers = allSuppliers.Where(e => e.QualificationLookupId == (int)Enums.QualificationResultLookup.Failed).Select(x => x.SupplierSelectedCr).ToList();



            #region [Send Notification]
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { postQualification.ReferenceNumber },
                SubjectEmailArgs = new object[] { postQualification.ReferenceNumber },
                PanelArgs = new object[] { postQualification.ReferenceNumber },
                SMSArgs = new object[] { postQualification.ReferenceNumber }
            };
            #endregion

            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments, $"Qualification/CheckPostQualification?tenderIdString={Util.Encrypt(postQualification.TenderId)}", NotificationEntityType.PostQualification, postQualification.TenderId.ToString(), postQualification.BranchId, postQualification.OffersCheckingCommitteeId ?? postQualification.DirectPurchaseCommitteeId);
            MainNotificationTemplateModel mainNotificationTemplateModelForSupplier = new MainNotificationTemplateModel(NotificationArguments, "", NotificationEntityType.PostQualification, postQualification.TenderId.ToString(), postQualification.BranchId, postQualification.OffersCheckingCommitteeId ?? postQualification.DirectPurchaseCommitteeId);

            if (postQualification.OffersCheckingCommitteeId != null)
            {
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.OperationsOnPostqualification.ApprovePostQualificationChecking, postQualification.OffersCheckingCommitteeId, mainNotificationTemplateModel);
            }
            else
            {
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseSecretary.OperationsOnQualification.ApprovePostQualificationChecking, postQualification.DirectPurchaseCommitteeId, mainNotificationTemplateModel);
            }


            for (int i = 0; i < acceptedSuppliers.Count; i++)
            {
                List<string> listOfCr = new List<string>();
                listOfCr.Add(acceptedSuppliers[i]);
                NotificationArguments acceptedSuppliersNotificationArguments = new NotificationArguments
                {
                    BodyEmailArgs = new object[] { postQualification.ReferenceNumber, Resources.OfferResources.DisplayInputs.Matching },
                    SubjectEmailArgs = new object[] { },
                    PanelArgs = new object[] { postQualification.ReferenceNumber, Resources.OfferResources.DisplayInputs.Matching },
                    SMSArgs = new object[] { postQualification.ReferenceNumber, Resources.OfferResources.DisplayInputs.Matching }
                };
                await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnPostqualification.AcceptingPostQualificationDocuments, acceptedSuppliers, mainNotificationTemplateModelForSupplier);
            }


            for (int i = 0; i < rejectedSuppliers.Count; i++)
            {
                List<string> listOfCr = new List<string>();
                listOfCr.Add(rejectedSuppliers[i]);
                #region NT
                NotificationArguments rejectedSuppliersNotificationArguments = new NotificationArguments
                {
                    BodyEmailArgs = new object[] { postQualification.ReferenceNumber, Resources.OfferResources.DisplayInputs.NotMatching, "" },
                    SubjectEmailArgs = new object[] { },
                    PanelArgs = new object[] { postQualification.ReferenceNumber, Resources.OfferResources.DisplayInputs.NotMatching, "" },
                    SMSArgs = new object[] { postQualification.ReferenceNumber, Resources.OfferResources.DisplayInputs.NotMatching, "" }
                };

                await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnPostqualification.RejectingPostQualificationDocuments, rejectedSuppliers, mainNotificationTemplateModelForSupplier);
                #endregion
            }
        }

        public async Task<Tender> RejectCheckPostQualificationOffers(int tenderId, string rejectionReason, List<string> roles)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _qualificationQueries.GetPostQualificationById(tenderId);
            _qualificationDomainService.IsValidToRejectCheckTender(tender, roles);
            tender.UpdateTenderStatus(Enums.TenderStatus.DocumentCheckRejected, rejectionReason, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.RejectPostQulaificationChecking);
            await SendNotificationForCheckingPostQualificationRejected(tender, roles);
            return await _qualificationCommands.UpdateAsync(tender);
        }

        private async Task SendNotificationForCheckingPostQualificationRejected(Tender postQualification, List<string> roles)
        {
            #region [Send Notification]
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { postQualification.ReferenceNumber },
                SubjectEmailArgs = new object[] { postQualification.ReferenceNumber },
                PanelArgs = new object[] { postQualification.ReferenceNumber },
                SMSArgs = new object[] { postQualification.ReferenceNumber }
            };
            #endregion


            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments, $"Qualification/CheckPostQualification?tenderIdString={Util.Encrypt(postQualification.TenderId)}", NotificationEntityType.PostQualification, postQualification.TenderId.ToString(), postQualification.BranchId, postQualification.OffersCheckingCommitteeId ?? postQualification.DirectPurchaseCommitteeId);
            if (postQualification.OffersCheckingCommitteeId != null)
            {
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.OperationsOnPostqualification.RejectingApprovePostQualificationChecking, postQualification.OffersCheckingCommitteeId, mainNotificationTemplateModel);
            }
            else
            {
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseSecretary.OperationsOnQualification.RejectingApprovePostQualificationChecking, postQualification.DirectPurchaseCommitteeId, mainNotificationTemplateModel);
            }


        }

        public async Task<Tender> ReopenPostQualificationChecking(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _qualificationQueries.GetPostQualificationById(tenderId);
            tender.UpdateTenderStatus(Enums.TenderStatus.DocumentChecking, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ReopenPostQualification);
            return await _qualificationCommands.UpdateAsync(tender);
        }

        public async Task<PreQulaificationApprovalModel> GetPostQulaificationByQualificationId(int postQualificationId)
        {
            return await _qualificationQueries.GetPostQualificationByQualificationId(postQualificationId);
        }

        #endregion

        #region QualificationEvaluation

        public async Task<Tender> CreatePreQualification(PreQualificationSavingModel model, int userId, string userAgency, int branchId)
        {
            await _qualificationDomainService.IsValidToCreate(model);

            Tender qualification = new Tender();
            qualification.CreatePreQualification(model.TenderId, (int)Enums.TenderType.PreQualification, model.TenderName, model.TechnicalOrganizationId, model.PreQualificationCommitteeId, model.LastEnqueriesDate.Value, model.LastOfferPresentationDate.Value, model.OffersCheckingDate
             , (int)Enums.TenderStatus.Established, model.TenderPointsToPass, model.TechnicalAdministrativeCapacity, model.FinancialCapacity, model.QualificationTypeId, userAgency, branchId);

            List<TenderAttachment> attachments = new List<TenderAttachment>();
            foreach (var item in model.Attachments)
            {
                attachments.Add(new TenderAttachment(item.Name, item.FileNetReferenceId, item.AttachmentTypeId));
            }
            if (qualification.TenderId == 0)
            {
                qualification.UpdateTenderRelations(model.TenderAreaIDs, model.TenderActivitieIDs, model.TenderConstructionWorkIDs, model.TenderMentainanceRunnigWorkIDs, model.InsideKSA, model.Details, model.ActivityDescription, /*userId,*/ model.TenderCountriesIDs, false);
                qualification.UpdateAttachments(attachments, userId, false);
                qualification.AddActionHistory((int)Enums.TenderStatus.Established, TenderActions.UpdateTender, "", userId);
                qualification.SetReferenceNumber(await _qualificationCommands.UpdateReferenceNumber()); await _genericCommandRepository.CreateAsync(qualification);
            }
            else
            {
                qualification = await _qualificationQueries.FindPreQualificationByIdandStatus(model.TenderId);
                if (_httpContextAccessor.HttpContext.User.UserRole() == RoleNames.PreQualificationCommitteeSecretary)
                {
                    await _qualificationDomainService.CheckMasterWeightForQualification(model);
                    qualification.DeleteQualificationConfigurations();
                    qualification.DeleteQualificationSubCategoryConfigurations();
                    qualification.UpdateQualificationMasterData(model.TenderId, model.TenderPointsToPass, model.TechnicalAdministrativeCapacity, model.FinancialCapacity, model.QualificationTypeId);
                    await CreateQualificationConfiguration(model, qualification);
                    qualification.UpdateQualificationDataFromSecrary(model, attachments, _httpContextAccessor.HttpContext.User.UserId());
                }
                else
                {
                    model.TenderStatusId = (int)Enums.TenderStatus.Established;
                    qualification.UpdatePreQualificationData(model, attachments, _httpContextAccessor.HttpContext.User.UserId());
                }
                _genericCommandRepository.Update(qualification);

            }
            await _genericCommandRepository.SaveAsync();
            return qualification;
        }

        public async Task CreateQualificationConfiguration(PreQualificationSavingModel model, Tender qualification, bool IsDraft = false)
        {
            List<QualificationTypeCategory> Categories = await _qualificationQueries.FindQualificationItems(model.QualificationTypeId.Value);

            if (model.QualificationTypeId == (int)Enums.PreQualificationType.Small)
            {
                AddSmallEvaluationData(model, qualification, Categories, IsDraft);
            }
            else if (model.QualificationTypeId == (int)Enums.PreQualificationType.Medium)
            {
                AddMediumEvaluationData(model, qualification, Categories, IsDraft);
            }
            else if (model.QualificationTypeId == (int)Enums.PreQualificationType.Large)
            {
                AddLargeEvaluationData(model, qualification, Categories, IsDraft);
            }
        }

        private QualificationSubCategoryConfiguration BindQualificationSubCategoryItems(PreQualificationSavingModel model, int subCategoryId, int TenderId, bool IsDraft = false)
        {
            QualificationSubCategoryConfiguration subCategoryConfig;
            decimal Weight = 0;
            switch (subCategoryId)
            {
                case (int)Enums.QualificationSubCategory.PreviousExperienceYear:
                    Weight = CheckItemWeight(model.QualificationSmallEvaluation.PreviousExperienceWeight, IsDraft);
                    break;
                case (int)Enums.QualificationSubCategory.ExistingContractualObligations:
                    Weight = CheckItemWeight(model.QualificationSmallEvaluation.ExistingContractualObligationsWeight, IsDraft);
                    break;
                case (int)Enums.QualificationSubCategory.HumanResource:
                    Weight = CheckItemWeight(model.QualificationSmallEvaluation.HRWeight, IsDraft);
                    break;
                case (int)Enums.QualificationSubCategory.FinancialStatements:
                    Weight = CheckItemWeight(model.FinancialCapacity, IsDraft);
                    break;
            }
            return subCategoryConfig = new QualificationSubCategoryConfiguration(TenderId, subCategoryId, Weight);
        }

        private decimal CheckItemWeight(decimal currentWeight, bool IsDraft = false)
        {
            if (!IsDraft)
            {
                if (currentWeight == 0)
                {
                    throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.pleaseCheckItemWeight);
                }
                return (currentWeight == 0 ? (decimal)Enums.QualificationConfig.WeigthDefaultValue : currentWeight);
            }
            return currentWeight;
        }

        private QualificationSubCategoryConfiguration BindQualificationSubCategoryItemsForMedium(PreQualificationSavingModel model, int subCategoryId, int TenderId, bool IsDraft = false)
        {
            decimal Weight = 0;
            QualificationSubCategoryConfiguration subCategoryConfig;
            switch (subCategoryId)
            {
                case (int)Enums.QualificationSubCategory.PreviousExperienceYear:
                    Weight = CheckItemWeight(model.QualificationMediumEvaluation.PreviousExperienceWeight, IsDraft);
                    break;
                case (int)Enums.QualificationSubCategory.ExistingContractualObligations:
                    Weight = CheckItemWeight(model.QualificationMediumEvaluation.ExistingContractualObligationsWeight, IsDraft);
                    break;
                case (int)Enums.QualificationSubCategory.HumanResource:
                    Weight = CheckItemWeight(model.QualificationMediumEvaluation.HRWeight, IsDraft);
                    break;
                case (int)Enums.QualificationSubCategory.Quality:
                    Weight = CheckItemWeight(model.QualificationMediumEvaluation.QualityWeight, IsDraft);
                    break;
                case (int)Enums.QualificationSubCategory.EnviromentAndHealthy:
                    Weight = CheckItemWeight(model.QualificationMediumEvaluation.EnviromentAndHealthyWeight, IsDraft);
                    break;
                case (int)Enums.QualificationSubCategory.FinancialStatements:
                    Weight = CheckItemWeight(model.FinancialCapacity, IsDraft);
                    break;
            }
            return subCategoryConfig = new QualificationSubCategoryConfiguration(TenderId, subCategoryId, Weight);
        }

        private QualificationSubCategoryConfiguration BindQualificationSubCategoryItemsForLarge(PreQualificationSavingModel model, int subCategoryId, int TenderId, bool IsDraft = false)
        {
            QualificationSubCategoryConfiguration subCategoryConfig = new QualificationSubCategoryConfiguration();
            if (subCategoryId == (int)Enums.QualificationSubCategory.PreviousExperienceYear)
            {
                subCategoryConfig = new QualificationSubCategoryConfiguration(TenderId, subCategoryId, CheckItemWeight(model.QualificationLargeEvaluation.PreviousExperienceWeight, IsDraft));
            }
            else if (subCategoryId == (int)Enums.QualificationSubCategory.ExistingContractualObligations)
            {
                subCategoryConfig = new QualificationSubCategoryConfiguration(TenderId, subCategoryId, CheckItemWeight(model.QualificationLargeEvaluation.ExistingContractualObligationsWeight, IsDraft));
            }

            else if (subCategoryId == (int)Enums.QualificationSubCategory.HumanResource)
            {
                subCategoryConfig = new QualificationSubCategoryConfiguration(TenderId, subCategoryId, CheckItemWeight(model.QualificationLargeEvaluation.HRWeight, IsDraft));
            }

            else if (subCategoryId == (int)Enums.QualificationSubCategory.Quality)
            {
                subCategoryConfig = new QualificationSubCategoryConfiguration(TenderId, subCategoryId, CheckItemWeight(model.QualificationLargeEvaluation.QualityWeight, IsDraft));
            }

            else if (subCategoryId == (int)Enums.QualificationSubCategory.EnviromentAndHealthy)
            {
                subCategoryConfig = new QualificationSubCategoryConfiguration(TenderId, subCategoryId, CheckItemWeight(model.QualificationLargeEvaluation.EnviromentAndHealthyWeight, IsDraft));
            }

            else if (subCategoryId == (int)Enums.QualificationSubCategory.Insurance)
            {
                subCategoryConfig = new QualificationSubCategoryConfiguration(TenderId, subCategoryId, CheckItemWeight(model.QualificationLargeEvaluation.InsuranceWeight, IsDraft));
            }

            else if (subCategoryId == (int)Enums.QualificationSubCategory.FinancialStatements)
            {
                subCategoryConfig = new QualificationSubCategoryConfiguration(TenderId, subCategoryId, CheckItemWeight(model.FinancialCapacity, IsDraft));
            }
            return subCategoryConfig;

        }

        private void AddSmallEvaluationData(PreQualificationSavingModel model, Tender qualification, List<QualificationTypeCategory> Categories, bool IsDraft = false)
        {
            QualificationConfiguration config;
            List<QualificationConfiguration> configLst = new List<QualificationConfiguration>();
            List<QualificationSubCategoryConfiguration> subconfigLst = new List<QualificationSubCategoryConfiguration>();
            foreach (var subCategory in Categories)
            {
                List<QualificationItem> lstOfItemsToConfigure = new List<QualificationItem>();
                subconfigLst.Add(BindQualificationSubCategoryItems(model, subCategory.QualificationSubCategoryId, qualification.TenderId, IsDraft));


                if (subCategory.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.FinancialStatements)
                {
                    lstOfItemsToConfigure = subCategory.QualificationSubCategory.QualificationItems.Where(a => a.ID == (int)Enums.QualificationEvaluationItems.CashRate || a.ID == (int)Enums.QualificationEvaluationItems.LiquidityRatio).ToList();
                }
                else
                {
                    lstOfItemsToConfigure = subCategory.QualificationSubCategory.QualificationItems.ToList();
                }

                foreach (var item in lstOfItemsToConfigure)
                {
                    if (item.ID == (int)Enums.QualificationEvaluationItems.NumberOfYearsOfExperience)
                    {
                        config = new QualificationConfiguration(model.QualificationSmallEvaluation.id, qualification.TenderId, item.ID, model.QualificationSmallEvaluation.ExperienceYearCountMin, model.QualificationSmallEvaluation.ExperienceYearCountMax, CheckItemWeight(model.QualificationSmallEvaluation.ExperienceYearCountWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.NumberOfProjectsImplementedLastThreeYears)
                    {
                        config = new QualificationConfiguration(model.QualificationSmallEvaluation.id, qualification.TenderId, item.ID, model.QualificationSmallEvaluation.LastProjectCountMin, model.QualificationSmallEvaluation.LastProjectCountMax, CheckItemWeight(model.QualificationSmallEvaluation.LastProjectCountWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.TotalValueProjectsLastThreeYears)
                    {
                        config = new QualificationConfiguration(model.QualificationSmallEvaluation.id, qualification.TenderId, item.ID, model.QualificationSmallEvaluation.LastProjectCostMin, model.QualificationSmallEvaluation.LastProjectCostMax, CheckItemWeight(model.QualificationSmallEvaluation.LastProjectCostWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.NumberOfExistingProjects)
                    {
                        config = new QualificationConfiguration(model.QualificationSmallEvaluation.id, qualification.TenderId, item.ID, model.QualificationSmallEvaluation.CurrentProjectCountMin, model.QualificationSmallEvaluation.CurrentProjectCountMax, CheckItemWeight(model.QualificationSmallEvaluation.CurrentProjectCountWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.ValueOfExistingProjects)
                    {
                        config = new QualificationConfiguration(model.QualificationSmallEvaluation.id, qualification.TenderId, item.ID, model.QualificationSmallEvaluation.CurrentProjectCostMin, model.QualificationSmallEvaluation.CurrentProjectCostMax, CheckItemWeight(model.QualificationSmallEvaluation.CurrentProjectCostWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.NumberOfEmployees)
                    {
                        config = new QualificationConfiguration(model.QualificationSmallEvaluation.id, qualification.TenderId, item.ID, model.QualificationSmallEvaluation.TotalEmployeeCountMin, model.QualificationSmallEvaluation.TotalEmployeeCountMax, CheckItemWeight(model.QualificationSmallEvaluation.TotalEmployeeCountWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.NumberOfSaudiEmployees)
                    {
                        config = new QualificationConfiguration(model.QualificationSmallEvaluation.id, qualification.TenderId, item.ID, 0, 0, 0); configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.PercentageOfSaudiEmployees)
                    {
                        config = new QualificationConfiguration(model.QualificationSmallEvaluation.id, qualification.TenderId, item.ID, model.QualificationSmallEvaluation.TotalEmployeePercentageMin, model.QualificationSmallEvaluation.TotalEmployeePercentageMax, CheckItemWeight(model.QualificationSmallEvaluation.TotalEmployeePercentageWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.CashRate)
                    {
                        config = new QualificationConfiguration(model.QualificationSmallEvaluation.id, qualification.TenderId, item.ID, CheckDefaultValue(model.QualificationSmallEvaluation.CashRateMin), CheckDefaultValue(model.QualificationSmallEvaluation.CashRateMax), CheckItemWeight(model.QualificationSmallEvaluation.CashRateWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.LiquidityRatio)
                    {
                        config = new QualificationConfiguration(model.QualificationSmallEvaluation.id, qualification.TenderId, item.ID, CheckDefaultValue(model.QualificationSmallEvaluation.LiquidityRatioMin), CheckDefaultValue(model.QualificationSmallEvaluation.LiquidityRatioMax), CheckItemWeight(model.QualificationSmallEvaluation.LiquidityRatioWeight, IsDraft));
                        configLst.Add(config);
                    }
                }

            }
            _qualificationCommands.CreateSubConfigurationAsync(subconfigLst);
            _qualificationCommands.CreateQualificationConfigurationAsync(configLst);

        }

        private void AddMediumEvaluationData(PreQualificationSavingModel model, Tender qualification, List<QualificationTypeCategory> Categories, bool IsDraft = false)
        {
            QualificationConfiguration config;
            List<QualificationConfiguration> configLst = new List<QualificationConfiguration>();
            List<QualificationSubCategoryConfiguration> subconfigLst = new List<QualificationSubCategoryConfiguration>();
            foreach (var subCategory in Categories)
            {
                List<QualificationItem> lstOfItemsToConfigure = new List<QualificationItem>();
                subconfigLst.Add(BindQualificationSubCategoryItemsForMedium(model, subCategory.QualificationSubCategoryId, qualification.TenderId, IsDraft));


                if (subCategory.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.FinancialStatements)
                {
                    lstOfItemsToConfigure = subCategory.QualificationSubCategory.QualificationItems.Where(a => a.ID == (int)Enums.QualificationEvaluationItems.CashRate || a.ID == (int)Enums.QualificationEvaluationItems.LiquidityRatio || a.ID == (int)Enums.QualificationEvaluationItems.TradingRatio).ToList();
                }
                else
                {
                    lstOfItemsToConfigure = subCategory.QualificationSubCategory.QualificationItems.ToList();
                }

                foreach (var item in lstOfItemsToConfigure)
                {

                    if (item.ID == (int)Enums.QualificationEvaluationItems.NumberOfYearsOfExperience)
                    {
                        config = new QualificationConfiguration(model.QualificationMediumEvaluation.id, qualification.TenderId, item.ID, model.QualificationMediumEvaluation.ExperienceYearCountMin, model.QualificationMediumEvaluation.ExperienceYearCountMax, CheckItemWeight(model.QualificationMediumEvaluation.ExperienceYearCountWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.NumberOfProjectsImplementedLastThreeYears)
                    {
                        config = new QualificationConfiguration(model.QualificationMediumEvaluation.id, qualification.TenderId, item.ID, model.QualificationMediumEvaluation.LastProjectCountMin, model.QualificationMediumEvaluation.LastProjectCountMax, CheckItemWeight(model.QualificationMediumEvaluation.LastProjectCountWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.TotalValueProjectsLastThreeYears)
                    {
                        config = new QualificationConfiguration(model.QualificationMediumEvaluation.id, qualification.TenderId, item.ID, model.QualificationMediumEvaluation.LastProjectCostMin, model.QualificationMediumEvaluation.LastProjectCostMax, CheckItemWeight(model.QualificationMediumEvaluation.LastProjectCostWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.NumberOfExistingProjects)
                    {
                        config = new QualificationConfiguration(model.QualificationMediumEvaluation.id, qualification.TenderId, item.ID, model.QualificationMediumEvaluation.CurrentProjectCountMin, model.QualificationMediumEvaluation.CurrentProjectCountMax, CheckItemWeight(model.QualificationMediumEvaluation.CurrentProjectCountWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.ValueOfExistingProjects)
                    {
                        config = new QualificationConfiguration(model.QualificationMediumEvaluation.id, qualification.TenderId, item.ID, model.QualificationMediumEvaluation.CurrentProjectCostMin, model.QualificationMediumEvaluation.CurrentProjectCostMax, CheckItemWeight(model.QualificationMediumEvaluation.CurrentProjectCostWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.NumberOfEmployees)
                    {
                        config = new QualificationConfiguration(model.QualificationMediumEvaluation.id, qualification.TenderId, item.ID, model.QualificationMediumEvaluation.TotalEmployeeCountMin, model.QualificationMediumEvaluation.TotalEmployeeCountMax, CheckItemWeight(model.QualificationMediumEvaluation.TotalEmployeeCountWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.NumberOfSaudiEmployees)
                    {
                        config = new QualificationConfiguration(model.QualificationMediumEvaluation.id, qualification.TenderId, item.ID, 0, 0, 0); configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.PercentageOfSaudiEmployees)
                    {
                        config = new QualificationConfiguration(model.QualificationMediumEvaluation.id, qualification.TenderId, item.ID, model.QualificationMediumEvaluation.TotalEmployeePercentageMin, model.QualificationMediumEvaluation.TotalEmployeePercentageMax, CheckItemWeight(model.QualificationMediumEvaluation.TotalEmployeePercentageWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.CashRate)
                    {
                        config = new QualificationConfiguration(model.QualificationMediumEvaluation.id, qualification.TenderId, item.ID, CheckDefaultValue(model.QualificationMediumEvaluation.CashRateMin), CheckDefaultValue(model.QualificationMediumEvaluation.CashRateMax), CheckItemWeight(model.QualificationMediumEvaluation.CashRateWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.LiquidityRatio)
                    {
                        config = new QualificationConfiguration(model.QualificationMediumEvaluation.id, qualification.TenderId, item.ID, CheckDefaultValue(model.QualificationMediumEvaluation.LiquidityRatioMin), CheckDefaultValue(model.QualificationMediumEvaluation.LiquidityRatioMax), CheckItemWeight(model.QualificationMediumEvaluation.LiquidityRatioWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.TradingRatio)
                    {
                        config = new QualificationConfiguration(model.QualificationMediumEvaluation.id, qualification.TenderId, item.ID, CheckDefaultValue(model.QualificationMediumEvaluation.TradeRateMin), CheckDefaultValue(model.QualificationMediumEvaluation.TradeRateMax), CheckItemWeight(model.QualificationMediumEvaluation.TradeRateWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.QualityAssuranceStandards)
                    {
                        config = new QualificationConfiguration(model.QualificationMediumEvaluation.id, qualification.TenderId, item.ID, 0, 0, 100);
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.EnvironmentalHealthSafetyStandards)
                    {
                        config = new QualificationConfiguration(model.QualificationMediumEvaluation.id, qualification.TenderId, item.ID, 0, 0, 100);
                        configLst.Add(config);
                    }
                }
            }
            _qualificationCommands.CreateSubConfigurationAsync(subconfigLst);
            _qualificationCommands.CreateQualificationConfigurationAsync(configLst);

        }

        private void AddLargeEvaluationData(PreQualificationSavingModel model, Tender qualification, List<QualificationTypeCategory> Categories, bool IsDraft = false)
        {
            List<QualificationConfiguration> configLst = new List<QualificationConfiguration>();
            List<QualificationSubCategoryConfiguration> subconfigLst = new List<QualificationSubCategoryConfiguration>();
            foreach (var subCategory in Categories)
            {
                List<QualificationItem> lstOfItemsToConfigure = new List<QualificationItem>();
                subconfigLst.Add(BindQualificationSubCategoryItemsForLarge(model, subCategory.QualificationSubCategoryId, qualification.TenderId, IsDraft));


                if (subCategory.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.FinancialStatements)
                {
                    lstOfItemsToConfigure = subCategory.QualificationSubCategory.QualificationItems.Where(a => a.ID == (int)Enums.QualificationEvaluationItems.CashRate
                    || a.ID == (int)Enums.QualificationEvaluationItems.LiquidityRatio || a.ID == (int)Enums.QualificationEvaluationItems.RatioOfObligations ||
                    a.ID == (int)Enums.QualificationEvaluationItems.TradingRatio || a.ID == (int)Enums.QualificationEvaluationItems.RateOfProfitability).ToList();
                }
                else
                {
                    lstOfItemsToConfigure = subCategory.QualificationSubCategory.QualificationItems.Where(a => a.IsDeleted == false).ToList();
                }

                foreach (var item in lstOfItemsToConfigure)
                {
                    QualificationConfiguration config = new QualificationConfiguration();
                    if (item.ID == (int)Enums.QualificationEvaluationItems.NumberOfYearsOfExperience)
                    {
                        config = new QualificationConfiguration(model.QualificationLargeEvaluation.id, qualification.TenderId, item.ID, model.QualificationLargeEvaluation.ExperienceYearCountMin, model.QualificationLargeEvaluation.ExperienceYearCountMax, CheckItemWeight(model.QualificationLargeEvaluation.ExperienceYearCountWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.NumberOfProjectsImplementedLastThreeYears)
                    {
                        config = new QualificationConfiguration(model.QualificationLargeEvaluation.id, qualification.TenderId, item.ID, model.QualificationLargeEvaluation.LastProjectCountMin, model.QualificationLargeEvaluation.LastProjectCountMax, CheckItemWeight(model.QualificationLargeEvaluation.LastProjectCountWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.TotalValueProjectsLastThreeYears)
                    {
                        config = new QualificationConfiguration(model.QualificationLargeEvaluation.id, qualification.TenderId, item.ID, model.QualificationLargeEvaluation.LastProjectCostMin, model.QualificationLargeEvaluation.LastProjectCostMax, CheckItemWeight(model.QualificationLargeEvaluation.LastProjectCostWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.NumberOfExistingProjects)
                    {
                        config = new QualificationConfiguration(model.QualificationLargeEvaluation.id, qualification.TenderId, item.ID, model.QualificationLargeEvaluation.CurrentProjectCountMin, model.QualificationLargeEvaluation.CurrentProjectCountMax, CheckItemWeight(model.QualificationLargeEvaluation.CurrentProjectCountWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.ValueOfExistingProjects)
                    {
                        config = new QualificationConfiguration(model.QualificationLargeEvaluation.id, qualification.TenderId, item.ID, model.QualificationLargeEvaluation.CurrentProjectCostMin, model.QualificationLargeEvaluation.CurrentProjectCostMax, CheckItemWeight(model.QualificationLargeEvaluation.CurrentProjectCostWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.NumberOfEmployees)
                    {
                        config = new QualificationConfiguration(model.QualificationLargeEvaluation.id, qualification.TenderId, item.ID, model.QualificationLargeEvaluation.TotalEmployeeCountMin, model.QualificationLargeEvaluation.TotalEmployeeCountMax, CheckItemWeight(model.QualificationLargeEvaluation.TotalEmployeeCountWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.PercentageOfSaudiEmployees)
                    {
                        config = new QualificationConfiguration(model.QualificationLargeEvaluation.id, qualification.TenderId, item.ID, model.QualificationLargeEvaluation.TotalEmployeePercentageMin, model.QualificationLargeEvaluation.TotalEmployeePercentageMax, CheckItemWeight(model.QualificationLargeEvaluation.TotalEmployeePercentageWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.NumberOfSaudiEmployees)
                    {
                        config = new QualificationConfiguration(model.QualificationLargeEvaluation.id, qualification.TenderId, item.ID, 0, 0, 0);
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.QualityAssuranceStandards)
                    {
                        config = new QualificationConfiguration(model.QualificationLargeEvaluation.id, qualification.TenderId, item.ID, 0, 0, 100);
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.EnvironmentalHealthSafetyStandards)
                    {
                        config = new QualificationConfiguration(model.QualificationLargeEvaluation.id, qualification.TenderId, item.ID, 0, 0, 100);
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.Insurance)
                    {
                        config = new QualificationConfiguration(model.QualificationLargeEvaluation.id, qualification.TenderId, item.ID, 0, 0, 100);
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.CashRate)
                    {
                        config = new QualificationConfiguration(model.QualificationLargeEvaluation.id, qualification.TenderId, item.ID, CheckDefaultValue(model.QualificationLargeEvaluation.CashRateMin), CheckDefaultValue(model.QualificationLargeEvaluation.CashRateMax), CheckItemWeight(model.QualificationLargeEvaluation.CashRateWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.LiquidityRatio)
                    {
                        config = new QualificationConfiguration(model.QualificationLargeEvaluation.id, qualification.TenderId, item.ID, CheckDefaultValue(model.QualificationLargeEvaluation.LiquidityRatioMin), CheckDefaultValue(model.QualificationLargeEvaluation.LiquidityRatioMax), CheckItemWeight(model.QualificationLargeEvaluation.LiquidityRatioWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.TradingRatio)
                    {
                        config = new QualificationConfiguration(model.QualificationLargeEvaluation.id, qualification.TenderId, item.ID, CheckDefaultValue(model.QualificationLargeEvaluation.TradeRateMin), CheckDefaultValue(model.QualificationLargeEvaluation.TradeRateMax), CheckItemWeight(model.QualificationLargeEvaluation.TradeRateWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.RatioOfObligations)
                    {
                        config = new QualificationConfiguration(model.QualificationLargeEvaluation.id, qualification.TenderId, item.ID, CheckDefaultValue(model.QualificationLargeEvaluation.ObligationsRationMin), CheckDefaultValue(model.QualificationLargeEvaluation.ObligationsRationMax), CheckItemWeight(model.QualificationLargeEvaluation.ObligationsRationWeight, IsDraft));
                        configLst.Add(config);
                    }
                    else if (item.ID == (int)Enums.QualificationEvaluationItems.RateOfProfitability)
                    {
                        config = new QualificationConfiguration(model.QualificationLargeEvaluation.id, qualification.TenderId, item.ID, CheckDefaultValue(model.QualificationLargeEvaluation.ProfitabilityRateMin), CheckDefaultValue(model.QualificationLargeEvaluation.ProfitabilityRateMax), CheckItemWeight(model.QualificationLargeEvaluation.ProfitabilityRateWeight, IsDraft));
                        configLst.Add(config);
                    }



                }
            }
            _qualificationCommands.CreateSubConfigurationAsync(subconfigLst);
            _qualificationCommands.CreateQualificationConfigurationAsync(configLst);

        }

        public decimal CheckDefaultValue(decimal Value)
        {
            return Value;
        }

        public async Task<Tender> EditQualificationCommittees(EditeCommitteesModel committeeModel)
        {
            var tender = await _tenderQueries.GetTenderByIdWithoutAnyIncluds(committeeModel.TenderId);
            if (tender.LastEnqueriesDate <= DateTime.Now)
            {
                throw new BusinessRuleException("لا يمكن التعديل  اللجان لتخطى تاريخ اخر موعد لإستلام الاستفسارات");
            }
            if ((tender.TechnicalOrganizationId.HasValue && committeeModel.TechnicalOrganizationId == null) || (tender.PreQualificationCommitteeId.HasValue && committeeModel.PreQualificationCommitteeId == null))
            {
                throw new BusinessRuleException("يجب اختيار اللجان ");
            }
            tender.UpdatePreQualificationCommittees(tender.TenderStatusId, committeeModel.PreQualificationCommitteeId, committeeModel.TechnicalOrganizationId, _httpContextAccessor.HttpContext.User.UserId());
            await _tenderCommands.UpdateAsync(tender);
            return tender;
        }

        #endregion

        public async Task CanAddPostqualificationIfTenderHasExtendOfferValidity(int tenderId)
        {
            await _qualificationDomainService.CanAddPostqualificationIfTenderHasExtendOfferValidity(tenderId);
        }
    }
}
