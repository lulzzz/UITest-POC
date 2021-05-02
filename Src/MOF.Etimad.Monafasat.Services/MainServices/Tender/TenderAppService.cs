using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.DTO;
using MOF.Etimad.Monafasat.ViewModel.LCGDto;
using MOF.Etimad.Monafasat.ViewModel.Tender;
using MOF.Etimad.Monafasat.ViewModel.Tender.LocalContent;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public partial class TenderAppService : ITenderAppService
    {
        private readonly IQuantityTemplatesProxy _templatesProxy;
        private readonly ITenderQueries _tenderQueries;
        private readonly ISupplierQueries _supplierQueries;
        private readonly ISupplierCommands _supplierCommands;
        private readonly IIDMQueries _iDMQueries;
        private readonly ITenderCommands _tenderCommands;
        private readonly IMapper _mapper;
        private readonly IBillAppService _billsInqueryAppService;
        private readonly IIDMAppService _idmAppService;
        private readonly ITenderDomainService _tenderDomainService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICommitteeQueries _committeeQueries;
        private readonly IBlockAppService _blockAppService;
        private RootConfigurations _rootConfiguration;
        private readonly INotificationAppService _notificationAppService;
        private readonly IMemoryCache _cache;
        private readonly IIDMAppService _iDMAppService;
        private readonly IGenericCommandRepository _genericCommandRepository;
        private readonly IBranchAppService _branchAppService;
        private readonly IVerificationService _verification;
        private readonly ILogger<TenderAppService> _logger;
        private readonly IBillCommand _billCommand;
        private readonly ILocalContentSettingsAppService _localContentSettingsAppService;
        private readonly ILocalContentConfigurationSettings _localContentConfigurationSettings;
        private readonly ISRMFrameworkAgreementManageProxy _sRMFrameworkAgreementManageProxy;
        private readonly IOfferQueries _offerQueries;
        private readonly ICommunicationQueries _communicationQueries;

        public TenderAppService(IQuantityTemplatesProxy quantityTemplatesProxy, INotificationAppService notificationAppService, ITenderQueries tenderQueries, ITenderCommands tenderCommands, ITenderDomainService tenderDomainService, IMapper mapper, IBillAppService billsInqueryAppService,
            IHttpContextAccessor httpContextAccessor, IIDMQueries iDMQueries, ICommitteeQueries committeeQueries, ISupplierQueries supplierQueries, IBlockAppService blockAppService, IIDMAppService idmAppService,
            ISupplierCommands supplierCommands, IGenericCommandRepository genericCommandRepository,
            IMemoryCache cache, IIDMAppService iDMAppService, IBranchAppService branchAppService, IVerificationService verification,
            ILogger<TenderAppService> logger, IOptionsSnapshot<RootConfigurations> rootConfiguration, IBillCommand billCommand, ILocalContentSettingsAppService localContentSettingsAppService,
            ISRMFrameworkAgreementManageProxy sRMFrameworkAgreementManageProxy, IOfferQueries offerQueries, ICommunicationQueries communicationQueries, ILocalContentConfigurationSettings localContentConfigurationSettings)
        {
            _rootConfiguration = rootConfiguration.Value;
            _templatesProxy = quantityTemplatesProxy;
            _cache = cache;
            _notificationAppService = notificationAppService;
            _supplierQueries = supplierQueries;
            _tenderQueries = tenderQueries;
            _tenderCommands = tenderCommands;
            _billsInqueryAppService = billsInqueryAppService;
            _tenderDomainService = tenderDomainService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _iDMQueries = iDMQueries;
            _committeeQueries = committeeQueries;
            _blockAppService = blockAppService;
            _idmAppService = idmAppService;
            _genericCommandRepository = genericCommandRepository;
            _supplierCommands = supplierCommands;
            _iDMAppService = iDMAppService;
            _branchAppService = branchAppService;
            _verification = verification;
            _logger = logger;
            _billCommand = billCommand;
            _localContentSettingsAppService = localContentSettingsAppService;
            _sRMFrameworkAgreementManageProxy = sRMFrameworkAgreementManageProxy;
            _offerQueries = offerQueries;
            _communicationQueries = communicationQueries;
            _localContentConfigurationSettings = localContentConfigurationSettings;

        }

        #region Service Queries        

        public async Task<Tender> FindTenderByTenderId(int tenderId)
        {
            Check.ArgumentNotNull(nameof(tenderId), tenderId.ToString());
            return await _tenderQueries.FindTenderByTenderId(tenderId);
        }

        public async Task<List<CommitteeModel>> GetBasicCommitteesOnBranch(string agencyCode)
        {
            var result = await _committeeQueries.GetBasicCommitteesOnBranch(agencyCode);
            return result;
        }

        public async Task<List<CommitteeModel>> GetTechincalAndDirectPurchaseCommittees(string agencyCode)
        {
            var result = await _committeeQueries.GetTechincalAndDirectPurchaseCommittees(agencyCode);
            return result;
        }

        public async Task<List<CommitteeUserModel>> GetDirectPurchaseCommitteesMembers(string agencyCode, int branchId)
        {
            var result = await _committeeQueries.GetDirectPurchaseCommitteesMembers(agencyCode, branchId);
            return result;
        }

        public async Task<List<CommitteeModel>> GetVROAndTechnicalCommittees(string agencyCode)
        {
            var result = await _committeeQueries.GetVROAndTechnicalCommittees(agencyCode);
            return result;
        }

        public async Task<BasicOfferTenderInfoModel> GetBasicOfferTenderInfoById(int id)
        {
            BasicOfferTenderInfoModel basicOfferTenderInfoModel = await _tenderQueries.GetBasicOfferTenderInfoById(id);
            return basicOfferTenderInfoModel;
        }

        public async Task<QueryResult<LinkTendersToCommitteeModel>> GetTendersToJoinCommittees(LinkTendersToCommitteeSearchCriteriaModel criteria)
        {
            return await _tenderQueries.GetTendersToJoinCommitteeWithBranches(criteria);
        }

        public async Task<QueryResult<AllTendersViewModel>> GetAllTendersForIndexPage(TenderSearchCriteria criteria)
        {
            Check.ArgumentNotNull(nameof(criteria), criteria);
            if (string.IsNullOrEmpty(criteria.Sort))
            {
                criteria.Sort = "CreatedAt";
                criteria.SortDirection = "DESC";
            }
            return await _tenderQueries.GetAllTendersForIndexPage(criteria);
        }
        public async Task<QueryResult<BasicTenderModel>> FindTenderBySearchCriteriaForIndexPage(TenderSearchCriteria criteria)
        {
            Check.ArgumentNotNull(nameof(criteria), criteria);
            if (string.IsNullOrEmpty(criteria.Sort))
            {
                criteria.Sort = "CreatedAt";
                criteria.SortDirection = "DESC";
            }
            return await _tenderQueries.FindTenderBySearchCriteriaForIndexPage(criteria);
        }

        public async Task<List<int>> FindTenderByAgencyCodeForPurchaseMethod(string agencyCode)
        {
            return await _tenderQueries.FindTenderByAgencyCodeForPurchaseMethod(agencyCode);
        }

        public async Task<QueryResult<BasicTenderModel>> FindTendersForUnderOperationsStageBySearchCriteria(TenderSearchCriteria criteria)
        {
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.UnderEstablishing);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.Established);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.Pending);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.Rejected);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.RejectedFromUnit);
            if (string.IsNullOrEmpty(criteria.Sort))
            {
                criteria.Sort = "CreatedAt";
                criteria.SortDirection = "DESC";
            }
            return await _tenderQueries.FindTenderBySearchCriteriaForUnderOperationsStage(criteria);
        }

        public async Task<QueryResult<BasicTenderModel>> FindTenderForAppliedSuppliersReport(TenderSearchCriteria creteria)
        {
            creteria.TenderStatusIds.Add((int)Enums.TenderStatus.UnderEstablishing);
            creteria.TenderStatusIds.Add((int)Enums.TenderStatus.Established);
            creteria.TenderStatusIds.Add((int)Enums.TenderStatus.Pending);
            creteria.TenderStatusIds.Add((int)Enums.TenderStatus.Rejected);
            if (string.IsNullOrEmpty(creteria.Sort))
            {
                creteria.Sort = "CreatedAt";
                creteria.SortDirection = "DESC";
            }
            return await _tenderQueries.FindTenderForAppliedSuppliersReport(creteria);
        }

        public async Task<QueryResult<TenderCheckingAndAwardingModel>> FindTendersForOpeningStageBySearchCriteria(TenderSearchCriteria creteria)
        {
            creteria.TenderStatusIds.Add((int)Enums.TenderStatus.Approved);
            creteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersTechnicalOppening);
            creteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersTechnicalOppeningPending);
            creteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersTechnicalOppeningRejected);
            creteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersTechnicalOppeningConfirmed);
            creteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersOppening);
            creteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersOppenedPending);
            creteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersOppenedConfirmed);
            creteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersOppenedRejected);
            if (string.IsNullOrEmpty(creteria.Sort))
            {
                creteria.Sort = "CreatedAt";
                creteria.SortDirection = "DESC";
            }
            return await _tenderQueries.FindTenderBySearchCriteriaForOpeningStage(creteria);
        }

        public async Task<QueryResult<TenderCheckingAndAwardingModel>> FindTendersForCheckingStageBySearchCriteria(TenderSearchCriteria criteria)
        {
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersOppenedConfirmed);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersCheckedPending);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersCheckedConfirmed);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.BiddingOffersCheckedConfirmed);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.BiddingOffersCheckedPending);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.BiddingOffersCheckedRejected);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.BiddingOffersCheckedConfirmed);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersCheckedRejected);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersChecking);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.Bidding);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.BiddingApproved);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersTechnicalCheckingPending);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersTechnicalCheckingRejected);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersFinancialChecking);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersFinancialOfferCheckingApproved);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersFinancialOfferCheckingPending);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersFinancialOfferCheckingRejected);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersTechnicalOppeningConfirmed);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersTechnicalChecking);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.BackForCheckingFromPlaint);
            if (string.IsNullOrEmpty(criteria.Sort))
            {
                criteria.Sort = "CreatedAt";
                criteria.SortDirection = "DESC";
            }
            return await _tenderQueries.FindTenderBySearchCriteriaForCheckingStage(criteria);
        }

        public async Task<QueryResult<VROTendersDTO>> GetVROTendersCreatedByGovAgency(TenderSearchCriteria criteria)
        {
            if (string.IsNullOrEmpty(criteria.Sort))
            {
                criteria.Sort = "CreatedAt";
                criteria.SortDirection = "DESC";
            }
            return await _tenderQueries.GetVROTendersCreatedByGovAgency(criteria);
        }

        public async Task<QueryResult<TenderCheckingAndAwardingModel>> GetTendersForCheckingDirectPuchaseStageAsync(TenderSearchCriteria criteria)
        {

            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.Approved);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersChecking);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersTechnicalCheckingPending);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersTechnicalCheckingRejected);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersFinancialChecking);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersFinancialOfferCheckingApproved);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersFinancialOfferCheckingPending);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersFinancialOfferCheckingRejected);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.DirectPurchaseOffersCheckingApproved);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.DirectPurchaseOffersCheckingApprovePending);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.DirectPurchaseOffersCheckingRejected);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.DirectPurchaseOffersChecking);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.BackForCheckingFromPlaint);
            if (string.IsNullOrEmpty(criteria.Sort))
            {
                criteria.Sort = "CreatedAt";
                criteria.SortDirection = "DESC";
            }
            return await _tenderQueries.GetTendersForCheckingDirectPuchaseStageAsync(criteria);
        }

        public async Task<QueryResult<TenderCheckingAndAwardingModel>> FindTendersForAwardingStageBySearchCriteria(TenderSearchCriteria criteria)
        {
            List<int> tenderStatusIds = new List<int>();
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersCheckedConfirmed);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersFinancialOfferCheckingApproved);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.DirectPurchaseOffersCheckingApproved);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersAwarding);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersAwardedPending);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersAwardedConfirmed);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersAwardedRejected);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.VROOffersFinancialCheckingApproved);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.BiddingApproved);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersInitialAwardedPending);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersInitialAwardedConfirmed);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.OffersInitialAwardedRejected);
            criteria.TenderStatusIds.Add((int)Enums.TenderStatus.BackForAwardingFromPlaint);
            if (string.IsNullOrEmpty(criteria.Sort))
            {
                criteria.Sort = "CreatedAt";
                criteria.SortDirection = "DESC";
            }
            if (tenderStatusIds.Any())
                criteria.TenderStatusIdsString = string.Join(", ", tenderStatusIds);
            if (criteria.UserRole == RoleNames.ApproveTenderAward)
            {
                return await _tenderQueries.FindTenderBySearchCriteriaForAwardingStageForApproveTenderAward(criteria);
            }
            else
            {
                return await _tenderQueries.FindTenderBySearchCriteriaForAwardingStage(criteria);
            }
        }

        public async Task<List<string>> FinancialYear()
        {
            return await _tenderQueries.FinancialYear();
        }

        public async Task<List<Invitation>> GetInvitation(List<string> commericalRegisterNos, int tenderId)
        {
            return await _tenderQueries.GetInvitation(commericalRegisterNos, tenderId);
        }

        public async Task<QueryResult<Invitation>> GetNewInvitationsByCRNo(TenderSearchCriteria tenderSearchCriteria)
        {
            return await _tenderQueries.GetNewInvitationsByCRNo(tenderSearchCriteria);
        }

        public async Task<QueryResult<TenderInvitationDetailsModel>> GetSupplierTenders(TenderSearchCriteria tenderSearch)
        {
            return await _tenderQueries.GetSupplierTenders(tenderSearch);
        }

        public async Task<QueryResult<Tender>> GetAllTenders(string cr)
        {
            return await _tenderQueries.GetAllTenders(cr);
        }

        public async Task<QueryResult<TenderInvitationDetailsModel>> GetAllSupplierTenders(TenderSearchCriteria criteria, List<SupplierAgencyBlockModel> allSuppliersBlock)
        {
            return await _tenderQueries.GetAllSupplierTenders(criteria, allSuppliersBlock);
        }

        public async Task<QueryResult<AllTendersForVisitorModel>> GetAllSupplierTendersForVisitor(TenderSearchCriteria criteria)
        {
            return await _tenderQueries.GetAllTendersForVisitor(criteria);
        }

        public async Task<QueryResult<TenderInvitationDetailsModel>> GetAllUnsubscribedSupplierTenders(TenderSearchCriteria criteria)
        {
            return await _tenderQueries.GetAllUnsubscribedSupplierTenders(criteria);
        }

        public async Task<QueryResult<Invitation>> GetSuppliersJoiningRequestsByTenderId(int tenderId, string agencyCode, int BranchId)
        {
            var result = await _tenderQueries.GetSuppliersJoiningRequestsByTenderId(tenderId);
            if (result.Items.Any(s => s.Tender.BranchId != BranchId && s.Tender.AgencyCode != agencyCode))
            {
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.NotAddedAuthorized);
            }
            return result;
        }

        public async Task<QueryResult<TenderCheckingAndAwardingModel>> FindAwardedTenderBySearchCriteria(TenderSearchCriteria criteria)
        {
            if (string.IsNullOrEmpty(criteria.Sort))
            {
                criteria.Sort = "CreatedAt";
                criteria.SortDirection = "DESC";
            }
            return await _tenderQueries.FindAwardedTenderBySearchCriteria(criteria);
        }

        public async Task<TenderInvitationDetailsModel> GetJoiningRequestByInvitationId(int invitationId, string agencyCode, int BranchId)
        {
            var JoiningRequest = await _tenderQueries.GetJoiningRequestByInvitationId(invitationId);
            if (JoiningRequest.Tender.BranchId != BranchId && JoiningRequest.Tender.AgencyCode != agencyCode)
            {
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.NotAddedAuthorized);
            }
            var joiningRequestModel = _mapper.Map<TenderInvitationDetailsModel>(JoiningRequest);
            return joiningRequestModel;
        }

        public async Task<QueryResult<TenderOpenOfferModel>> GetOffersForOpenByTenderIdAsync(TenderBasicSearchCriteria tenderBasicSearchCriteria)
        {
            var invitationsOffers = await _tenderQueries.GetOffersToOpeningByTenderIdAsync(tenderBasicSearchCriteria);
            return invitationsOffers;
        }

        public async Task<QueryResult<TenderCheckOfferModel>> GetOffersForCheckByTenderIdAsync(int tenderId)
        {
            var invitationsOffersModel = await _tenderQueries.GetOffersByTenderIdForCheckAsync(tenderId);
            var suppliers = await _idmAppService.GetSupplierDetailsBySearchCriteria(new SupplierIntegrationSearchCriteria
            {
                SupplierNumbers = invitationsOffersModel.Items.Select(s => s.CommericalRegisterNo).ToList(),
                PageSize = invitationsOffersModel.PageSize
            });
            foreach (var item in invitationsOffersModel.Items)
            {
                item.BaiscActivity = suppliers.Items.FirstOrDefault(s => s.supplierNumber == item.CommericalRegisterNo).activities;
            }
            return invitationsOffersModel;
        }

        public async Task<QueryResult<TenderCheckOfferModel>> GetOffersForCheckByTenderIdForNormalCheckingAsync(TenderBasicSearchCriteria tenderBasicSearchCriteria)
        {
            var invitationsOffers = await _tenderQueries.GetOffersByTenderIdForCheckingGridAsync(tenderBasicSearchCriteria);
            return invitationsOffers;
        }

        public async Task<QueryResult<TenderCheckOfferModel>> GetVROOffersForCheckByTenderIdForNormalCheckingAsync(TenderBasicSearchCriteria tenderBasicSearchCriteria)
        {
            var invitationsOffers = await _tenderQueries.GetVROOffersByTenderIdForCheckingGridAsync(tenderBasicSearchCriteria);
            return invitationsOffers;
        }
        public async Task<QueryResult<TenderCheckOfferModel>> GetOffersForAwardingStageByTenderId(TenderBasicSearchCriteria tenderBasicSearchCriteria)
        {
            var result = await _tenderQueries.GetOffersByTenderIdForOpeningAwardingStage(tenderBasicSearchCriteria);
            return result;
        }

        public async Task<Tender> FindTenderByAgencyCode(string agencyCode)
        {
            return await _tenderQueries.FindTenderByAgencyCode(agencyCode);
        }

        public async Task<List<VacationsDate>> FindAllVacationDates()
        {
            return await _tenderQueries.FindAllVacationDates();
        }

        public async Task<Tender> FindTenderDetailsByTenderId(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            return await _tenderQueries.FindTenderDetailsByTenderId(tenderId);
        }

        public async Task<TenderDetailsModel> GetMainTenderDetailsByTenderId(int tenderId, int branchId)
        {
            TenderDetailsModel tenderDetailsModel = await _tenderQueries.GetMainTenderDetailsByTenderId(tenderId, branchId);
            if (tenderDetailsModel == null)
                throw new UnHandledAccessException();
            return tenderDetailsModel;
        }

        public async Task<TenderDetailsModel> GetMainTenderDetailsForUnit(int tenderId, string agencyCode)
        {
            TenderDetailsModel tenderDetailsModel = await _tenderQueries.GetMainTenderDetailsForUnit(tenderId, agencyCode);
            if (tenderDetailsModel == null)
                throw new UnHandledAccessException();

            return tenderDetailsModel;
        }

        public async Task<TenderDetailsModel> GetMainTenderDetailsForSuppliers(int tenderId, string cr)
        {
            List<SupplierAgencyBlockModel> supplierblocks = new List<SupplierAgencyBlockModel>();
            if (!string.IsNullOrEmpty(cr))
                supplierblocks = await _blockAppService.GetAllSupplierBlocks(null, new List<string> { cr });
            TenderDetailsModel tenderDetailsModel = await _tenderQueries.GetMainTenderDetailsForSuppliers(tenderId, cr, supplierblocks);
            if (tenderDetailsModel == null)
                throw new UnHandledAccessException();
            if ((!string.IsNullOrEmpty(cr) && _httpContextAccessor.HttpContext.User.UserRole() == RoleNames.supplier || _httpContextAccessor.HttpContext.User.UserRole() == "")
                && (tenderDetailsModel.TenderStatusId == (int)Enums.TenderStatus.Established || tenderDetailsModel.TenderStatusId == (int)Enums.TenderStatus.UnderEstablishing || tenderDetailsModel.TenderStatusId == (int)Enums.TenderStatus.Pending || tenderDetailsModel.TenderStatusId == (int)Enums.TenderStatus.Rejected))
                throw new UnHandledAccessException();
            return tenderDetailsModel;
        }

        public async Task<TenderDetailsModel> GetMainTenderDetailsForVisitor(int tenderId)
        {
            TenderDetailsModel tenderDetailsModel = await _tenderQueries.GetMainTenderDetailsForVisitor(tenderId);
            if (tenderDetailsModel == null)
                throw new UnHandledAccessException();
            if (_httpContextAccessor.HttpContext.User.UserRole() == ""
                && (tenderDetailsModel.TenderStatusId == (int)Enums.TenderStatus.Established || tenderDetailsModel.TenderStatusId == (int)Enums.TenderStatus.UnderEstablishing || tenderDetailsModel.TenderStatusId == (int)Enums.TenderStatus.Pending || tenderDetailsModel.TenderStatusId == (int)Enums.TenderStatus.Rejected))
                throw new UnHandledAccessException();
            return tenderDetailsModel;
        }

        public async Task<QueryResult<Tender>> FindTendersByLogedUser(string agencyCode)
        {
            return await _tenderQueries.FindTendersByLogedUser(agencyCode);
        }

        public async Task<AllBasicTenderDataModel> GetBasicTenderDetailsById(int tenderId, string agencyCode)
        {
            AllBasicTenderDataModel allBasicTenderDataModel = await _tenderQueries.GetBasicTenderDetailsById(tenderId, agencyCode);
            if (allBasicTenderDataModel.TenderStatusId == (int)Enums.TenderStatus.UnitStaging)
            {
                if (allBasicTenderDataModel.TenderUnitStatusId == (int)Enums.TenderUnitStatus.WaitingUnitSecretaryReview)
                    allBasicTenderDataModel.TenderStatusName = Resources.TenderResources.DisplayInputs.WaitingUnitSecretaryReview;
                else if (allBasicTenderDataModel.TenderUnitStatusId == (int)Enums.TenderUnitStatus.WaitingManagerApprove)
                    allBasicTenderDataModel.TenderStatusName = Resources.TenderResources.DisplayInputs.WaitingManagerApprove;
            }
            _tenderDomainService.HasAccessToTender(allBasicTenderDataModel);
            return allBasicTenderDataModel;
        }

        public async Task<SecondStageBasicData> GetBasicDataForSecondStageTenderCreation(int tenderId, int branchId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Check.ArgumentNotNullOrEmpty(nameof(branchId), branchId.ToString());
            SecondStageBasicData tender = await _tenderQueries.GetTenderByIdAndBranchForSecondStage(tenderId, branchId);
            if (tender.TenderTypeId == (int)Enums.TenderType.FirstStageTender)
            {
                tender.ReferenceNumber = null;
            }
            if (tender == null || tender.BranchId != branchId)
            { throw new UnHandledAccessException(); }
            return tender;
        }

        public async Task<CreateTenderBasicDataModel> GetBasicTenderByIdAndBranch(int tenderId, int branchId)
        {
            if (tenderId == 0)
            {
                return await _branchAppService.FindInsertionTypeAndPurchaseMenthodsById(branchId);
            }
            CreateTenderBasicDataModel tender = await _tenderQueries.GetBasicTenderByIdAndBranch(tenderId, branchId);
            if (tender == null || tender.BranchId != branchId)
            {
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            }
            if (tender.TenderTypeId == (int)Enums.TenderType.FrameworkAgreement && tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed)
            {
                var tenderEntity = await _tenderQueries.GetTenderForFramWorkReCreation(tenderId);
                tenderEntity.SetPreviousFramWork(tenderId);
                tenderEntity.SetReferenceNumber(await _tenderCommands.UpdateReferenceNumber());
                await _tenderCommands.CreateAsync(tenderEntity);

                tender = _mapper.Map<CreateTenderBasicDataModel>(tenderEntity);
                tender.IsFramWorkRecreation = true;
                tender.AgreementTypeId = (int)Enums.AgreementType.Closed;
            }
            if (tender.PreviousFramWorkId != null)
            {
                tender.IsFramWorkRecreation = true;
            }

            return tender;
        }

        public async Task<TenderInformationModel> GetTenderByIdForEnquiry(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            TenderInformationModel tenderInformationModel = await _tenderQueries.GetTenderById(tenderId);
            if (tenderInformationModel.TechnicalOrganizationId == null)
            {
                throw new BusinessRuleException(Resources.TenderResources.Messages.NoTechnicalCommittee);
            }
            return tenderInformationModel;
        }

        public async Task<TenderInformationModel> GetTenderByIdForJoinigRequests(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            TenderInformationModel tenderInformationModel = await _tenderQueries.GetTenderById(tenderId);
            return tenderInformationModel;
        }

        public async Task<Tender> GetTenderByIdToApplyOffer(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            return await _tenderQueries.FindTenderByTenderId(tenderId);
        }

        public async Task<PurchaseModel> GetTenderByIdToPurchaseConditionBooklet(int tenderId, string cr)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            List<SupplierAgencyBlockModel> supplierblocks = new List<SupplierAgencyBlockModel>();
            return await _tenderQueries.FindTenderWithConditionBookletByTenderIdAndCr(tenderId, cr, supplierblocks);
        }

        public async Task<PurchaseModel> FindTenderForInvitationBillDetailsByTenderIdAndCr(int tenderId, string cr)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            List<SupplierAgencyBlockModel> supplierblocks = new List<SupplierAgencyBlockModel>();
            if (!string.IsNullOrEmpty(cr))
                supplierblocks = await _blockAppService.GetAllSupplierBlocks(null, new List<string> { cr });
            return await _tenderQueries.FindTenderForInvitationBillDetailsByTenderIdAndCr(tenderId, cr, supplierblocks);
        }

        public async Task<TenderCommunicationRequestModel> GetCommunicationRequestsDetailsTenderId(int tenderId, string cr)
        {
            return await _tenderQueries.GetCommunicationRequestsDetailsTenderId(tenderId, cr);
        }

        public async Task<TenderDatesModel> FindTenderDatesByTenderId(int tenderId, int branchId)
        {
            TenderDatesModel tenderDatesModel = await _tenderQueries.FindTenderDatesByTenderId(tenderId);
            if (tenderDatesModel == null || tenderDatesModel.BranchId != branchId)
            {
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            }

            return tenderDatesModel;
        }

        public async Task<GetConditionTemplateModel> FindConditionTemplate(string tenderIdString, int branchId)
        {
            var tenderId = Util.Decrypt(tenderIdString);
            GetConditionTemplateModel conditionsTemplateViewModel = await _tenderQueries.FindConditionTemplate(tenderId, branchId);
            if (conditionsTemplateViewModel == null || conditionsTemplateViewModel.BranchId != branchId)
            {
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            }
            string attachmentRef = "";
            foreach (var item in conditionsTemplateViewModel.AttachmentReferenceLst)
            {
                attachmentRef += ',' + item;
            }
            if (attachmentRef.Any())
                attachmentRef = attachmentRef.Remove(0, 1);
            conditionsTemplateViewModel.AttachmentReference = attachmentRef;


            string LocalContentAttachmentRef = "";
            foreach (var item in conditionsTemplateViewModel.LocalContentAttachmentReferenceLst)
            {
                LocalContentAttachmentRef += ',' + item;
            }
            if (LocalContentAttachmentRef.Any())
                LocalContentAttachmentRef = LocalContentAttachmentRef.Remove(0, 1);
            conditionsTemplateViewModel.StudyMinimumTargetFileNetReferenceId = LocalContentAttachmentRef;

            //check if has mandatory items 

            return conditionsTemplateViewModel;
        }

        public async Task<GetConditionTemplateModel> FindTenderConditionTemplateByTenderId(string tenderIdString, int branchId)
        {
            var tenderId = Util.Decrypt(tenderIdString);
            GetConditionTemplateModel tenderDatesModel = await _tenderQueries.FindTenderConditionTemplateByTenderId(tenderId, branchId);
            if (tenderDatesModel == null)
            {
                throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            }
            tenderDatesModel.AttachmentReference = "";
            foreach (var item in tenderDatesModel.AttachmentReferenceLst)
            {
                tenderDatesModel.AttachmentReference += ',' + item;
            }
            if (tenderDatesModel.AttachmentReference.Any())
                tenderDatesModel.AttachmentReference = tenderDatesModel.AttachmentReference.Remove(0, 1);
            var logosList = await _iDMAppService.GetAgencyLogos(new List<string> { tenderDatesModel.AgencyCode });
            if (logosList.Count > 0)
                tenderDatesModel.AgencyLogo = "data:image/png;base64," + Util.ConvertImageURLToBase64(logosList.FirstOrDefault().Value);
            else
            {
                try
                {
                    tenderDatesModel.AgencyLogo = "data:image/png;base64," + Util.ConvertImageURLToBase64($"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}" + "/images/capitol-building.png");
                }
                catch (Exception)
                {
                    tenderDatesModel.AgencyLogo = "data:image/png;base64,";
                }
            }

            if (tenderDatesModel.VersionId >= (int)Enums.ActivityVersions.Sprint7Activities)
            {
                var tenderWithTemplates = await _tenderQueries.FindTemplatesby(tenderId, tenderDatesModel.VersionId, branchId);
                var columnsResponse = await _templatesProxy.GetMadatoryListColumnIdByTemplatesId(tenderDatesModel.QuantityTableVersionId ?? 0, tenderWithTemplates.Select(d => d.ActivitytemplatId).ToList());
                List<long> cols = new List<long>();
                if (columnsResponse.StatusCode == (int)Enums.QTSStatusCode.OK)
                {
                    cols = columnsResponse.Data.ToList();
                }
                var ExistCols = await _tenderQueries.FindMandatoryListColumns(tenderId, cols);
                tenderDatesModel.HasMandatoryItems = ExistCols != null && ExistCols.Any();
            }
            return tenderDatesModel;
        }

        public async Task<ConditionsBookletTemplateModel> GetTenderConditionsBookletTemplateDetails(int tenderId, int branchId)
        {
            ConditionsBookletTemplateModel tenderDatesModel = await _tenderQueries.GetTenderConditionsBookletTemplateDetails(tenderId);
            var logosList = await _iDMAppService.GetAgencyLogos(new List<string> { tenderDatesModel.AgencyCode });
            if (logosList.Count > 0)
                tenderDatesModel.AgencyLogo = "data:image/png;base64," + Util.ConvertImageURLToBase64(logosList.FirstOrDefault().Value);
            else
            {
                try
                {
                    tenderDatesModel.AgencyLogo = "data:image/png;base64," + Util.ConvertImageURLToBase64($"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}" + "/images/capitol-building.png");
                }
                catch (Exception)
                {
                    tenderDatesModel.AgencyLogo = "data:image/png;base64,";
                }
            }
            if (tenderDatesModel.TenderBranchId != branchId)
            {
                throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            }
            return tenderDatesModel;
        }

        public async Task<TenderDatesModel> FindTenderExtendDatesByTenderId(int tenderId, int branchId)
        {
            var changeRequest = await _tenderQueries.FindTenderExtendDatesRequests(tenderId);
            Check.BusinessRule(changeRequest != null && changeRequest.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending, "لا يمكن تمديد التواريخ الا بعد اعتماد الطلب السابق");
            Check.BusinessRule(changeRequest != null && changeRequest.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Rejected, "لا يمكن تمديد التواريخ الا بعد اغلاق الطلب السابق");
            TenderDatesModel tenderDatesModel = await _tenderQueries.FindTenderDatesByTenderId(tenderId);
            if (tenderDatesModel == null || tenderDatesModel.BranchId != branchId)
            { throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess); }
            return tenderDatesModel;
        }

        public async Task<TenderDatesModel> GetTenderDatesDetailsById(int tenderId)
        {
            return await _tenderQueries.FindTenderDatesDetailsByTenderId(tenderId);
        }

        public async Task<TenderOffersModel> FindTenderInvitationOfferDetailsByTenderId(int tenderId)
        {
            var tender = await _tenderQueries.FindTenderInvitationOfferDetailsByTenderId(tenderId);
            var tenderOffersModel = _mapper.Map<TenderOffersModel>(tender);
            ConvertNumberToText obj = new ConvertNumberToText(tenderOffersModel.EstimatedValue.Value, new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
            tenderOffersModel.EstimatedValueText = obj.ConvertToArabic();
            var numberOfNotOpenedOffers = tender.Offers.Count(a => a.IsOpened != true && a.IsActive == true && a.OfferStatusId == (int)Enums.OfferStatus.Received);
            tenderOffersModel.IsValidToSubmit = numberOfNotOpenedOffers == 0;
            var numberOfCheckOffers = tender.Offers.Count(a => a.OfferTechnicalEvaluationStatusId == null && a.IsActive == true && a.OfferStatusId == (int)Enums.OfferStatus.Received);
            tenderOffersModel.IsValidToCheck = numberOfCheckOffers == 0;
            return tenderOffersModel;
        }
        public async Task<TenderOffersModel> GetTenderOfferDetailsForOppeningStage(int tenderId, int userId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Check.ArgumentNotNullOrEmpty(nameof(userId), userId.ToString());
            var tenderOffersModel = await _tenderQueries.GetTenderOfferDetailsForOppeningStage(tenderId, userId);
            _tenderDomainService.IsValidToGetTenderOfferDetailsForOppeningStage(tenderOffersModel, _httpContextAccessor.HttpContext.User.UserAgency());
            return tenderOffersModel;
        }

        public async Task<int> GetUnOpenOffersForCombinedSuppliers(int tenderId)
        {
            return await _tenderQueries.GetUnOpenOffersForCombinedSuppliers(tenderId);
        }

        public async Task<TenderOffersModel> GetTenderOffersDetailsForOpenAwarding(int tenderId, int userId, string agencyCode)
        {
            var tenderOffersModel = await _tenderQueries.GetTenderOffersDetailsForOpenAwarding(tenderId, userId, agencyCode);
            if (tenderOffersModel == null)
                throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            return tenderOffersModel;
        }
        public async Task<TenderOffersModel> GetTenderAwardingDetails(int tenderId, int userId, string agencyCode)
        {
            var tenderOffersModel = await _tenderQueries.GetTenderAwardingDetailsByTenderId(tenderId, userId, agencyCode);
            if (tenderOffersModel == null)
                throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);

            if (_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.ApproveTenderAward))
            {
                var branchUser = await _branchAppService.GetBranchUsersForAwardingApproval(_httpContextAccessor.HttpContext.User.UserId(), _httpContextAccessor.HttpContext.User.UserBranch());

                if (branchUser != null && tenderOffersModel.EstimatedValue.HasValue)
                {
                    tenderOffersModel.CanAuthorityHolderDoAction = tenderOffersModel.EstimatedValue >= branchUser.EstimatedValueFrom && tenderOffersModel.EstimatedValue <= branchUser.EstimatedValueTo;
                }
            }
            return tenderOffersModel;
        }

        public async Task<Tender> FindTenderOfferDetailsByTenderId(int tenderId)
        {
            var result = await _tenderQueries.FindTenderOfferDetailsByTenderId(tenderId);
            return result;
        }


        public async Task<TenderRelationsModel> FindRelationsDetailsByTenderId(int tenderId)
        {
            var result = await _tenderQueries.FindRelationsDetailsByTenderId(tenderId);
            return result;
        }
        public async Task<TenderLocalContentValuesViewModel> GetTenderLocalContenetValuesByTenderId(int tenderId)
        {
            var result = await _tenderQueries.GetTenderLocalContenetValuesByTenderId(tenderId);
            return result;
        }
        public async Task<LocalContentDetailsViewModel> GetLocalContentDetailsForSupplierByTenderId(int tenderId)
        {
            var result = await _tenderQueries.GetLocalContentDetailsForSupplierByTenderId(tenderId);
            return result;
        }

        public async Task<TenderRelationsModel> FindTenderRelationsModelByTenderId(int tenderId, int branchId)
        {
            TenderRelationsModel model = await _tenderQueries.FindTenderRelationsModelByTenderId(tenderId);
            if (model == null || model.BranchId != branchId)
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            return model;
        }

        private string CheckTenderRejectReason(int tenderStatusId, List<TenderHistory> tenderHistories)
        {
            switch (tenderStatusId)
            {
                case (int)Enums.TenderStatus.Established:
                    {
                        if (tenderHistories.Any(h => h.StatusId == (int)Enums.TenderStatus.Rejected))
                            return tenderHistories.OrderByDescending(t => t.TenderHistoryId).FirstOrDefault(h => h.StatusId == (int)Enums.TenderStatus.Rejected).RejectionReason;
                        break;
                    }
                case (int)Enums.TenderStatus.OffersOppening:
                    {
                        if (tenderHistories.Any(h => h.StatusId == (int)Enums.TenderStatus.OffersOppenedRejected))
                            return tenderHistories.OrderByDescending(t => t.TenderHistoryId).FirstOrDefault(h => h.StatusId == (int)Enums.TenderStatus.OffersOppenedRejected).RejectionReason;
                        break;
                    }
                case (int)Enums.TenderStatus.OffersOppenedConfirmed:
                    {
                        if (tenderHistories.Any(h => h.StatusId == (int)Enums.TenderStatus.OffersCheckedRejected))
                            return tenderHistories.OrderByDescending(t => t.TenderHistoryId).FirstOrDefault(h => h.StatusId == (int)Enums.TenderStatus.OffersCheckedRejected).RejectionReason;
                        break;
                    }
                case (int)Enums.TenderStatus.OffersAwarding:
                    {
                        if (tenderHistories.Any(h => h.StatusId == (int)Enums.TenderStatus.OffersAwardedRejected))
                            return tenderHistories.OrderByDescending(t => t.TenderHistoryId).FirstOrDefault(h => h.StatusId == (int)Enums.TenderStatus.OffersAwardedRejected).RejectionReason;
                        break;
                    }
                default:
                    return "";
            }
            return "";
        }

        public async Task<TenderHistory> FindTenderHistoryByUserIdAndTenderIdAndStatusId(int tenderId, int tenderStatusId)
        {
            var tenderHistories = await _tenderQueries.FindTenderHistoriesByTenderId(tenderId);
            var tenderHistory = tenderHistories.Where(t => t.StatusId == tenderStatusId)
                                                .OrderByDescending(t => t.TenderHistoryId).FirstOrDefault();
            if (tenderStatusId == (int)Enums.TenderStatus.Established || tenderStatusId == (int)Enums.TenderStatus.OffersOppening ||
                tenderStatusId == (int)Enums.TenderStatus.OffersOppenedConfirmed || tenderStatusId == (int)Enums.TenderStatus.OffersAwarding)
                tenderHistory.UpdateStatus(CheckTenderRejectReason(tenderStatusId, tenderHistories));
            return tenderHistory;
        }

        public async Task<Tender> FindTenderAttachmentsById(int tenderId, int branchId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Check.ArgumentNotNullOrEmpty(nameof(branchId), branchId.ToString());
            Tender tender = await _tenderQueries.FindTenderAttachmentsById(tenderId, branchId);
            return tender;
        }

        public async Task<AttachmentsModel> FindTenderAttachmentsModelById(int tenderId, int branchId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Check.ArgumentNotNullOrEmpty(nameof(branchId), branchId.ToString());
            AttachmentsModel tender = await _tenderQueries.FindTenderAttachmentsModelById(tenderId, branchId);
            if (tender == null)
            {
                throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            }
            foreach (var item in tender.BookletReferenceLst)
            {
                tender.BookletReference += ',' + item;
            }
            foreach (var item in tender.AttachmentReferenceLst)
            {
                tender.AttachmentReference += ',' + item;
            }

            if (tender.AttachmentReference.Length > 0)
                tender.AttachmentReference = tender.AttachmentReference.Remove(0, 1);
            if (tender.BookletReference.Length > 0)
                tender.BookletReference = tender.BookletReference.Remove(0, 1);
            return tender;
        }

        public async Task<InvitationStepModel> GetTenderDetailsForInvitationStep(int tenderId, string CR)
        {
            return await _tenderQueries.FindTenderDetailsByInvitationdAsync(tenderId);
        }

        public async Task<Tender> FindTenderByInvitationId(int invitationId)
        {
            return await _tenderQueries.FindTenderByInvitationId(invitationId);
        }

        public async Task<List<AddressModel>> GetAllAddresses()
        {
            return await _tenderQueries.GetAllAddresses();
        }

        public async Task<List<AddressModel>> GetOfferOpeningAddresses(int branchId)
        {
            return await _tenderQueries.GetOfferOpeningAddresses(branchId);
        }

        public async Task<List<AddressModel>> GetOfferOpeningAddressesByBranchId(int branchId)
        {
            return await _tenderQueries.GetOfferOpeningAddresses(branchId);
        }

        public async Task<bool> GetFavouriteTenderSuppliersToApplyOffer()
        {
            List<FavouriteSupplierTender> lst = await _tenderQueries.GetFavouriteTenderSuppliersToApplyOffer();
            var favoriteList = lst.Select(a => new { cr = a.SupplierCRNumber, tenderNumber = a.Tender.TenderNumber, tenderId = a.TenderId });
            return true;
        }

        public async Task<List<Tender>> GetRelatedTendersByActivities(int tenderId)
        {
            var result = await _tenderQueries.GetRelatedTendersByActivities(tenderId);
            return result;
        }

        public async Task<AwardingDetailsModel> GetAwardingInformationForSupplier(int tenderId, string cr)
        {
            return await _tenderQueries.GetAwardingInformationForSupplier(tenderId, cr);
        }

        public async Task<CheckingResultsModel> GetCheckingResultsInformation(int tenderid)
        {
            return await _tenderQueries.GetCheckingResultsInformation(tenderid);
        }


        #endregion

        #region  Service Commands

        public async Task<TenderDatesModel> CreateBasicTender(CreateTenderBasicDataModel model)
        {
            PraperBasicData(model);
            Tender tender = new Tender();
            if (model.TenderId == 0)
                tender = await CreateBasicTenderData(model);
            else
                tender = await UpdateBasicTenderData(model);

            return GetDatesStepData(tender);
        }

        public async Task<TenderRelationsModel> CreateVROTenderByRelatedAgency(CreateTenderBasicDataModel model)
        {
            PraperBasicData(model);
            Tender tender = new Tender();
            if (model.TenderId == 0)
                tender = await CreateGovAgencyVROTender(model);
            else
                tender = await UpdateGovAgencyVROTender(model);
            return GetRelationStepDataForRelatedAgency(tender);
        }

        private async Task<Tender> CreateGovAgencyVROTender(CreateTenderBasicDataModel model)
        {

            Tender tender = new Tender(model.AgencyCode, model.BranchId, model.TenderTypeId, model.InvitationTypeId, model.TenderName, model.TenderNumber, model.Purpose, model.AnnouncementTemplateId, model.TechnicalOrganizationId, model.OffersOpeningCommitteeId, model.OffersCheckingCommitteeId, model.DirectPurchaseCommitteeId, model.VROCommitteeId,
                  model.ReasonForPurchaseTenderTypeId, model.ReasonForLimitedTenderTypeId, model.TenderTypeOtherSelectionReason, model.PreQualificationId, model.PreAnnouncementId, model.OfferPresentationWayId, model.EstimatedValue.Value, model.ConditionsBookletPrice, _httpContextAccessor.HttpContext.User.UserId(),
                  model.AgreementMonthes, model.AgreementYears, model.AgreementDays, model.AgreementTypeId, model.TenderAgreementAgencyIDs, model.NumberOfWinners, model.BonusValue, (int)Enums.TenderCreatedByType.AgenciesRelatedByVRO, null, true, model.SupplierNeedSample, model.SamplesDeliveryAddress, model.QuantityTableVersionId, null);
            tender.SetReferenceNumber(await _tenderCommands.UpdateReferenceNumber());
            tender.AddActivityVersion((int)Enums.ActivityVersions.OldActivities);
            await _tenderCommands.CreateAsync(tender);
            return tender;
        }

        private async Task<Tender> UpdateGovAgencyVROTender(CreateTenderBasicDataModel model)
        {
            var tender = await _tenderQueries.FindTenderRelationsByTenderId(model.TenderId);
            tender.UpdateBasicData(model.TenderTypeId, model.InvitationTypeId, model.TenderName, model.TenderNumber, model.Purpose, model.AnnouncementTemplateId, model.TechnicalOrganizationId,
                model.OffersOpeningCommitteeId, model.OffersCheckingCommitteeId, model.DirectPurchaseCommitteeId, model.VROCommitteeId, model.ReasonForPurchaseTenderTypeId, model.ReasonForLimitedTenderTypeId,
                model.TenderTypeOtherSelectionReason, model.PreQualificationId, model.OfferPresentationWayId, model.EstimatedValue.Value, model.ConditionsBookletPrice, _httpContextAccessor.HttpContext.User.UserId(),
                model.AgreementMonthes, model.AgreementYears, model.AgreementDays, model.AgreementTypeId, model.TenderAgreementAgencyIDs,
                model.NumberOfWinners, model.BonusValue, (int)Enums.TenderCreatedByType.AgenciesRelatedByVRO, true,
                model.SupplierNeedSample, model.SamplesDeliveryAddress, model.QuantityTableVersionId, model.PreAnnouncementId);
            await _tenderCommands.UpdateAsync(tender);
            return tender;
        }

        private static TenderRelationsModel GetRelationStepDataForRelatedAgency(Tender tender)
        {
            TenderRelationsModel relationsModel = new TenderRelationsModel
            {
                TenderName = tender.TenderName,
                TenderNumber = tender.TenderNumber,
                AgencyCode = tender.AgencyCode,
                TenderIdString = Util.Encrypt(tender.TenderId),
                InsideKSA = tender.InsideKSA,
                TenderAreaIDs = tender.TenderAreas.Select(a => a.AreaId).ToList(),
                TenderCountriesIDs = tender.TenderCountries.Select(a => a.CountryId).ToList(),
                TenderActivitieIDs = tender.TenderActivities.Select(a => a.ActivityId).ToList(),
                TenderConstructionWorkIDs = tender.TenderConstructionWorks.Select(a => a.ConstructionWorkId).ToList(),
                TenderMentainanceRunnigWorkIDs = tender.TenderMaintenanceRunnigWorks.Select(a => a.MaintenanceRunningWorkId).ToList(),
                Details = tender.Details,
                ActivityDescription = tender.ActivityDescription,
                TenderId = tender.TenderId,
                ReferenceNumber = tender.ReferenceNumber,
                TenderTypeId = tender.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedConfirmed && tender.TenderTypeId == (int)Enums.TenderType.FirstStageTender ? (int)Enums.TenderType.SecondStageTender : tender.TenderTypeId,
                TenderFirstStageId = tender.TenderFirstStageId != null ? tender.TenderFirstStageId : 0,
                TenderStatusId = tender.TenderStatusId,
                InvitationTypeId = tender.InvitationTypeId,
                OfferPresentationWayId = tender.OfferPresentationWayId,
                PreQualificationId = tender.PreQualificationId,
                TenderCreatedByTypeId = tender.CreatedByTypeId
            };
            return relationsModel;
        }

        private static TenderDatesModel GetDatesStepData(Tender tender)
        {
            return new TenderDatesModel
            {
                TenderName = tender.ReferenceNumber,
                TenderNumber = tender.TenderNumber,
                OffersOpeningAddressId = tender.OffersOpeningAddressId,
                TenderId = tender.TenderId,
                TenderIdString = Util.Encrypt(tender.TenderId),
                TenderStatusId = tender.TenderStatusId,
                TenderTypeId = tender.TenderTypeId,
                InvitationTypeId = tender.InvitationTypeId,
                AgencyCode = tender.AgencyCode,
                BranchId = tender.BranchId,
                LastEnqueriesDate = tender.LastEnqueriesDate,
                LastOfferPresentationDate = tender.LastOfferPresentationDate,
                LastOfferPresentationTime = tender.LastOfferPresentationDate.HasValue ? tender.LastOfferPresentationDate.Value.ToString("hh:mm tt") : "",
                OffersOpeningDate = tender.OffersOpeningDate,
                OffersOpeningTime = tender.OffersOpeningDate.HasValue ? tender.OffersOpeningDate.Value.ToString("hh:mm tt") : "",
                OffersCheckingDate = tender.OffersCheckingDate,
                ReferenceNumber = tender.ReferenceNumber,
                OfferPresentationWayId = tender.OfferPresentationWayId,
                PreQualificationId = tender.PreQualificationId,
                OffersCheckingTime = tender.OffersCheckingDate.HasValue ? tender.OffersCheckingDate.Value.ToString("hh:mm tt") : "",
                SupplierNeedSample = tender.SupplierNeedSample,
                SamplesDeliveryAddress = tender.SamplesDeliveryAddress,
                InitialGuaranteePercentage = tender.InitialGuaranteePercentage,
                OffersOpeningAddress = tender.OffersOpeningAddress?.AddressName,
                InitialGuaranteeAddress = tender.InitialGuaranteeAddress,
                BuildingName = tender.BuildingName,
                FloarNumber = tender.FloarNumber,
                DepartmentName = tender.DepartmentName,
                DeliveryDate = tender.DeliveryDate,
                DeliveryTime = tender.DeliveryDate.HasValue ? tender.DeliveryDate.Value.ToString("hh:mm tt") : "",
                FinalGuaranteePercentage = tender.AwardingDiscountPercentage
            };
        }

        private async Task<Tender> UpdateBasicTenderData(CreateTenderBasicDataModel model)
        {
            var AgencyCode = _httpContextAccessor.HttpContext.User.UserAgency();
            List<AgencyBudgetNumber> agencyBudgetNumber = _mapper.Map<List<AgencyBudgetNumber>>(model.AgencyBudgetNumbers);
            Tender tender = await _tenderQueries.FindTenderWithInvitationsByTenderId(model.TenderId);
            if (tender.TenderTypeId != model.TenderTypeId)
            {
                throw new BusinessRuleException("لا يمكن تعديل نوع المنافسة");
            }
            else
            {
                if (string.IsNullOrEmpty(tender.InitialGuaranteeAddress))
                    model.InitialGuaranteeAddress = null;
                else
                {
                    model.InitialGuaranteeAddress = tender.InitialGuaranteeAddress;
                    model.InitialGuaranteePercentage = tender.InitialGuaranteePercentage;
                }
            }
            if (model.TenderTypeId == (int)Enums.TenderType.FirstStageTender)
            {
                model.EstimatedValue = 0;
                if (model.HasQualification)
                {
                    model.InvitationTypeId = (int)Enums.InvitationType.Specific;
                }
                else
                {
                    model.InvitationTypeId = (int)Enums.InvitationType.Public;
                }
            }
            if (model.AnnouncementTemplateId != null || model.PreAnnouncementId != null)
            {
                var announcementSuppliers = new List<string>();
                if (model.AnnouncementTemplateId != null)
                {
                    announcementSuppliers = await _tenderQueries.GetAnnouncementSuppliersTemplateJoinedRequest(model.AnnouncementTemplateId);
                }
                if (model.PreAnnouncementId != null)
                {
                    var announcementCrs = await _tenderQueries.GetSuppliersJoinedToAnnouncemet(model.PreAnnouncementId);
                    var addedSuppliers = announcementCrs.Where(c => !announcementSuppliers.Any(x => x == c));
                    announcementSuppliers.AddRange(addedSuppliers);
                }

                if (model.HasQualification)
                {

                    var suppliers = await _tenderQueries.GetQualifiedSuppliers(model.PreQualificationId);
                    var addedSuppliers = suppliers.Where(c => announcementSuppliers.Contains(c)).ToList();
                    tender = await _tenderQueries.FindTenderWithInvitationsByTenderId(tender.TenderId);

                    List<string> newsuppliers = new List<string>();
                    foreach (var item in addedSuppliers)
                    {
                        if (!(await _blockAppService.CheckifSupplierBlockedByCrNo(item, AgencyCode)))
                        {
                            newsuppliers.Add(item);
                        }
                    }
                    tender.AddInvitationForQualifiedSuppliers(newsuppliers);
                    await _tenderCommands.UpdateAsync(tender);
                }
                else
                {
                    tender = await _tenderQueries.FindTenderWithInvitationsByTenderId(tender.TenderId);
                    tender.AddInvitationForQualifiedSuppliers(announcementSuppliers);
                    await _tenderCommands.UpdateAsync(tender);
                }
            }
            else if (model.HasQualification && model.PreAnnouncementId == null && model.AnnouncementTemplateId == null)
            {
                tender.Invitations.ForEach(x => x.Delete());
                tender.UnRegisteredSuppliersInvitation.ForEach(x => x.Delete());
                var suppliers = await _tenderQueries.GetQualifiedSuppliers(model.PreQualificationId);
                List<string> newsuppliers = new List<string>();
                foreach (var item in suppliers)
                {
                    if (!(await _blockAppService.CheckifSupplierBlockedByCrNo(item, AgencyCode)))
                    {
                        newsuppliers.Add(item);
                    }
                }
                tender.AddInvitationForQualifiedSuppliers(suppliers);
            }
            else
            {
                tender.Invitations.Where(i => i.InvitedByQualification.HasValue && i.InvitedByQualification.Value).ToList().ForEach(x => x.Delete());
            }

            tender.AgencyBudgetNumbers.ForEach(x => x.Delete());
            tender.UpdateBasicData(model.TenderTypeId, model.InvitationTypeId, model.TenderName, model.TenderNumber, model.Purpose, model.AnnouncementTemplateId, model.TechnicalOrganizationId, model.OffersOpeningCommitteeId, model.OffersCheckingCommitteeId, model.DirectPurchaseCommitteeId, model.VROCommitteeId,
                model.ReasonForPurchaseTenderTypeId, model.ReasonForLimitedTenderTypeId, model.TenderTypeOtherSelectionReason, model.PreQualificationId, model.OfferPresentationWayId, model.EstimatedValue,
                model.ConditionsBookletPrice, _httpContextAccessor.HttpContext.User.UserId(), model.AgreementMonthes, model.AgreementYears, model.AgreementDays, model.AgreementTypeId, model.TenderAgreementAgencyIDs,
                model.NumberOfWinners, model.BonusValue, model.TenderCreatedByType, false, null, null, model.QuantityTableVersionId, model.PreAnnouncementId, agencyBudgetNumber, model.IsLowBudgetDirectPurchase, model.DirectPurchaseCommitteeMemberId);
            await _tenderCommands.UpdateAsync(tender);
            return tender;
        }

        private async Task<Tender> CreateBasicTenderData(CreateTenderBasicDataModel model)
        {
            var agencyCode = _httpContextAccessor.HttpContext.User.UserAgency();
            if (model.TenderTypeId == (int)Enums.TenderType.FirstStageTender)
            {
                model.EstimatedValue = 0;
                model.InvitationTypeId = model.HasQualification ? (int)Enums.InvitationType.Specific : (int)Enums.InvitationType.Public;
            }
            if (model.TenderTypeId == (int)Enums.TenderType.NewTender || model.TenderTypeId == (int)Enums.TenderType.LimitedTender || model.TenderTypeId == (int)Enums.TenderType.FrameworkAgreement || model.TenderTypeId == (int)Enums.TenderType.SecondStageTender || model.TenderTypeId == (int)Enums.TenderType.ReverseBidding)
            {
                model.InitialGuaranteeAddress = "";
            }
            List<AgencyBudgetNumber> agencyBudgetNumber = _mapper.Map<List<AgencyBudgetNumber>>(model.AgencyBudgetNumbers);
            Tender tender = new Tender(model.AgencyCode, model.BranchId, model.TenderTypeId, model.InvitationTypeId, model.TenderName, model.TenderNumber, model.Purpose, model.AnnouncementTemplateId, model.TechnicalOrganizationId, model.OffersOpeningCommitteeId, model.OffersCheckingCommitteeId, model.DirectPurchaseCommitteeId, model.VROCommitteeId,
                  model.ReasonForPurchaseTenderTypeId, model.ReasonForLimitedTenderTypeId, model.TenderTypeOtherSelectionReason, model.PreQualificationId, model.PreAnnouncementId, model.OfferPresentationWayId, model.EstimatedValue, model.ConditionsBookletPrice, _httpContextAccessor.HttpContext.User.UserId(),
                  model.AgreementMonthes, model.AgreementYears, model.AgreementDays, model.AgreementTypeId, model.TenderAgreementAgencyIDs, model.NumberOfWinners, model.BonusValue, model.TenderCreatedByType, model.InitialGuaranteeAddress, false,
                  null, null, model.QuantityTableVersionId, agencyBudgetNumber, model.IsLowBudgetDirectPurchase, model.DirectPurchaseCommitteeMemberId);
            tender.SetReferenceNumber(await _tenderCommands.UpdateReferenceNumber());

            #region Set Activity Version
            var activityCurrentVersion = await _tenderQueries.GetCurrentActivityVersion();
            var versionId = (tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects || tender.TenderTypeId == (int)Enums.TenderType.CurrentTender || tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase) ? (int)Enums.ActivityVersions.OldActivities : activityCurrentVersion.Id;
            tender.AddActivityVersion(versionId);
            #endregion Set Activity Version

            await _tenderCommands.CreateAsync(tender);
            if (model.AnnouncementTemplateId != null || model.PreAnnouncementId != null)
            {
                var announcementSuppliers = new List<string>();
                if (model.AnnouncementTemplateId != null)
                {
                    announcementSuppliers = await _tenderQueries.GetAnnouncementSuppliersTemplateJoinedRequest(model.AnnouncementTemplateId);
                }
                if (model.PreAnnouncementId != null)
                {
                    var announcementCrs = await _tenderQueries.GetSuppliersJoinedToAnnouncemet(model.PreAnnouncementId);
                    var addedSuppliers = announcementCrs.Where(c => !announcementSuppliers.Any(x => x == c));
                    announcementSuppliers.AddRange(addedSuppliers);
                }

                if (model.HasQualification)
                {
                    var suppliers = await _tenderQueries.GetQualifiedSuppliers(model.PreQualificationId);
                    var addedSuppliers = suppliers.Where(c => announcementSuppliers.Contains(c)).ToList();
                    tender = await _tenderQueries.FindTenderWithInvitationsByTenderId(tender.TenderId);

                    List<string> newsuppliers = new List<string>();
                    foreach (var item in addedSuppliers)
                    {
                        if (!(await _blockAppService.CheckifSupplierBlockedByCrNo(item, agencyCode)))
                        {
                            newsuppliers.Add(item);
                        }
                    }
                    tender.AddInvitationForQualifiedSuppliers(addedSuppliers);
                    await _tenderCommands.UpdateAsync(tender);
                }
                else
                {
                    tender = await _tenderQueries.FindTenderWithInvitationsByTenderId(tender.TenderId);
                    tender.AddInvitationForQualifiedSuppliers(announcementSuppliers);
                    await _tenderCommands.UpdateAsync(tender);
                }
            }
            if (model.HasQualification && model.PreAnnouncementId == null && model.AnnouncementTemplateId == null)
            {

                tender = await _tenderQueries.FindTenderWithInvitationsByTenderId(tender.TenderId);
                var suppliers = await _tenderQueries.GetQualifiedSuppliers(model.PreQualificationId);
                List<string> newsuppliers = new List<string>();
                foreach (var item in suppliers)
                {
                    if (!(await _blockAppService.CheckifSupplierBlockedByCrNo(item, agencyCode)))
                    {
                        newsuppliers.Add(item);
                    }
                }
                tender.AddInvitationForQualifiedSuppliers(newsuppliers);
                await _tenderCommands.UpdateAsync(tender);
            }
            return tender;
        }

        private void PraperBasicData(CreateTenderBasicDataModel model)
        {
            Check.ArgumentNotNull(nameof(model), model);
            _tenderDomainService.IsValidToCreateTender(model);
            if (model.TenderStatusId != (int)Enums.TenderStatus.Established)
            {
                model.TenderStatusId = (int)Enums.TenderStatus.UnderEstablishing;
            }
            if (model.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                model.OfferPresentationWayId = (int)Enums.OfferPresentationWayId.TwoSepratedFiles;
                if (model.IsAgencyRelatedByVRO)
                {
                    model.TechnicalOrganizationId = null;
                }
            }
            if (model.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects && model.IsVRO)
                model.TenderCreatedByType = (int)Enums.TenderCreatedByType.VRO;
            if (model.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects && model.IsAgencyRelatedByVRO)
                model.TenderCreatedByType = (int)Enums.TenderCreatedByType.AgenciesRelatedByVRO;
            if (model.HasQualification || model.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects || model.TenderTypeId == (int)Enums.TenderType.LimitedTender || ((model.TenderTypeId == (int)Enums.TenderType.Competition || model.TenderTypeId == (int)Enums.TenderType.FrameworkAgreement || model.TenderTypeId == (int)Enums.TenderType.ReverseBidding || model.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase) && model.InvitationTypeId == (int)Enums.InvitationType.Specific))
            {
                model.InvitationTypeId = (int)Enums.InvitationType.Specific;
            }
            if (model.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            {
                model.OffersCheckingCommitteeId = null;
                model.VROCommitteeId = null;
            }
            else
            {
                model.DirectPurchaseCommitteeId = null;
            }
            if (model.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || model.TenderTypeId == (int)Enums.TenderType.CurrentTender || model.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || model.TenderTypeId == (int)Enums.TenderType.FrameworkAgreement || model.TenderTypeId == (int)Enums.TenderType.ReverseBidding || model.TenderTypeId == (int)Enums.TenderType.LimitedTender)
            {
                model.DirectPurchaseCommitteeId = null;
            }
            else if (model.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                model.OffersOpeningCommitteeId = null;
                model.OffersCheckingCommitteeId = null;
            }
            else if (model.TenderTypeId == (int)Enums.TenderType.Competition || model.TenderTypeId == (int)Enums.TenderType.FirstStageTender)
            {
                model.OffersOpeningCommitteeId = null;
                model.DirectPurchaseCommitteeId = null;
            }
            else if (model.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            {
                model.OffersCheckingCommitteeId = null;
                model.OffersOpeningCommitteeId = null;
            }
            if (model.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || model.TenderTypeId == (int)Enums.TenderType.CurrentTender || model.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || model.TenderTypeId == (int)Enums.TenderType.FirstStageTender || model.TenderTypeId == (int)Enums.TenderType.Competition)
            {
                model.OfferPresentationWayId = (int)Enums.OfferPresentationWayId.OneFile;
            }
        }

        #region Tender Approval Flow

        public async Task SendTenderToApprove(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderForSendToApproval(tenderId);
            IsValidToSendTenderToApprove(tender);
            tender.UpdateTenderStatus(Enums.TenderStatus.Pending, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.SubmitTenderForApproval);
            await SendTenderToApproveNotifications(tender);
            await _tenderCommands.UpdateAsync(tender);
        }
        private async Task SendTenderToApproveNotifications(Tender tender)
        {
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", tender.ReferenceNumber, tender.Purpose, tender.LastEnqueriesDate?.ToString("dd/MM/yyyy"), tender.LastOfferPresentationDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
                          $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}",
                          NotificationEntityType.Tender,
                          tender.TenderId.ToString(),
                          tender.BranchId);
            var VROOfficeCode = tender?.Agency?.VROOfficeCode;
            bool isRelated = !string.IsNullOrEmpty(VROOfficeCode) ? true : false;
            if (tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects && !isRelated)
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.EtimadOfficer.OperationsNeedApprove.submittenderforapproval, tender.BranchId, mainNotificationTemplateModel);
            else
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.Auditor.OperationsNeedApprove.submittenderforapproval, tender.BranchId, mainNotificationTemplateModel);
        }

        private void IsValidToSendTenderToApprove(Tender tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.LastOfferPresentationDate <= DateTime.Now && tender.TenderTypeId == (int)Enums.TenderType.CurrentTender)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.SendToApproveCantbeGreaterThanLastPresentation);
            if (tender.TenderStatusId != (int)Enums.TenderStatus.Established)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.Established));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        public async Task ApproveTenderWithInbudget(int tenderId, string verificationCode, bool iagree, bool competitionIsBudgeted)
        {
            await _verification.CheckForVerificationCode(tenderId, verificationCode, (int)Enums.VerificationType.Tender);
            Tender tender = await _tenderQueries.FindTenderForApprove(tenderId);
            IsValidToApproveTender(tender);
            if (tender.ConditionsBookletPrice.HasValue && tender.ConditionsBookletPrice.Value != 0)
            {
                await SadadDetailsForAgency(tender);
            }

            var oldTendersAndVRO = tender.TenderTypeId == (int)Enums.TenderType.CurrentTender || tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects;

            var isFrameworkTenderCheckValid = await IsFrameworkTenderCheckValid(tender);
            if (!((tender.EstimatedValue.HasValue && tender.EstimatedValue >= (int)ConitionalBookletPriceList.MoreThanTwentyFiveMilion) ||
                isFrameworkTenderCheckValid) || oldTendersAndVRO)
            {
                await ApproveTender(tender, iagree, competitionIsBudgeted, oldTendersAndVRO);
                await SendNotifcationsAfterApproveTender(tender);
            }
            else
            {
                ApproveTenderHasUnit(tender, iagree, competitionIsBudgeted);
                await SendNotifcationsOnUnitApprove(tender);
            }
            if (!oldTendersAndVRO)
            {
                var setting = await _localContentSettingsAppService.Get();


                var isTendernewWithLocalContent = await _tenderQueries.IsTenderHasLocalContent(tender.CreatedAt);
                var tenderLocalContent = await _tenderQueries.GetTenderLocalContentByTenderId(tender.TenderId);
                tender.SetTenderLocalContent(tenderLocalContent);
                if (tenderLocalContent != null)
                {
                     
                    var tenderLocalContentMechanismIds = await _tenderQueries.GetTenderLocalContentMechanism((int)tenderLocalContent.Id);
                    if (isTendernewWithLocalContent && (tenderLocalContentMechanismIds.Contains((int)Enums.LocalContentMechanismPerfermance.MinimumRequiredMechanismLocalContent) || tenderLocalContentMechanismIds.Contains((int)Enums.LocalContentMechanismPerfermance.MechanismForWeighingLocalContentFinancialEvaluation)))
                    {
                        tender.TenderLocalContent.SetTenderLocalContentSettings(tender.TenderId,
                             setting.HighValueContractsAmmount, setting.LocalContentMaximumPercentage, setting.PriceWeightAfterAdjustment, setting.LocalContentWeightAndFinancialMarket);
                    }
                    var isTenderHasNationalProductMechanism = tenderLocalContentMechanismIds.Contains((int)Enums.LocalContentMechanismPerfermance.ThePricePreferenceMechanismNationalProduct);
                    tender.SetLocalContenetNationalProductPercentage(isTenderHasNationalProductMechanism, setting.NationalProductPercentage);
                }
            }
            await _tenderCommands.UpdateAsync(tender);
        }

        private async Task ApproveTender(Tender tender, bool iagree, bool competitionIsBudgeted, bool oldTendersAndVRO)
        {
            tender.UpdateTenderStatus(Enums.TenderStatus.Approved, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ApproveTender);
            tender.SetAuditorAgree(iagree);
            tender.SetCompetitionIsBudgetedOrNot(competitionIsBudgeted);
            if (tender.Invitations.Any())
            {
                var suppliers = tender.Invitations.Select(t => new SupplierInvitationModel { CommericalRegisterNo = t.CommericalRegisterNo, TenderId = tender.TenderId }).ToList();
                await SendInvitationsToUnBlockedSuppliers(suppliers, tender);
            }
            if (tender.UnRegisteredSuppliersInvitation.Any())
            {
                await UnRegesteredsuppliersInvitations(tender);
            }
        }

        private void ApproveTenderHasUnit(Tender tender, bool iagree, bool competitionIsBudgeted)
        {
            var tenderUnitStatus = Enums.TenderUnitStatus.WaitingUnitSecretaryReview;
            if (tender.TenderUnitAssigns.Any() && tender.TenderUnitAssigns.Where(a => a.UnitSpecialistLevel != (int)UnitSpecialistLevel.UnitManager).OrderByDescending(a => a.TenderUnitAssignId).FirstOrDefault().UnitSpecialistLevel == (int)UnitSpecialistLevel.UnitSpecialistLevelTwo)
            {
                tenderUnitStatus = Enums.TenderUnitStatus.TenderTransferdToLevelTwo;
            }
            tender.SetUnitStatus(tenderUnitStatus);
            tender.UpdateTenderStatus(Enums.TenderStatus.UnitStaging, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.SendToUnitForApproval);
            tender.SetCompetitionIsBudgetedOrNot(competitionIsBudgeted);
            tender.SetAuditorAgree(iagree);
            tender.SetIsUnitSecreteryAccepted(null);
            TenderUnitStatusesHistory tenderUnitStatusesHistory = new TenderUnitStatusesHistory(comment: "", tenderUnitStatusId: (int)tenderUnitStatus, estimatedValue: tender.EstimatedValue);
            tender.TenderUnitStatusesHistories.Add(tenderUnitStatusesHistory);
        }

        private async Task SendNotifcationsAfterApproveTender(Tender tender)
        {
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[]{ "", tender.ReferenceNumber,
                                               tender.Purpose, tender.LastEnqueriesDate?.ToString("dd/MM/yyyy hh:mm tt"),
                                               tender.LastOfferPresentationDate?.ToString("dd/MM/yyyy hh:mm tt"),
                                              tender.OffersOpeningDate == null  ?Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt")  },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(NotificationArguments, $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId);
            if (tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.PurshaseSpecialist.OperationsOnTheTender.approvedtender, tender.BranchId, approveTender);
            }
            else
            {
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.OperationsOnTheTender.approvedtender, tender.BranchId, approveTender);
            }
        }
        private async Task UnRegesteredsuppliersInvitations(Tender tender)
        {
            var UnRegesteredsuppliers = tender.UnRegisteredSuppliersInvitation.ToList();
            if (UnRegesteredsuppliers.Any())
            {
                foreach (var item in UnRegesteredsuppliers)
                {
                    if (string.IsNullOrEmpty(item.CrNumber) || tender.AgencyCode == item.CrNumber)
                    {
                        continue;
                    }
                    var checkBlockList = await _blockAppService.CheckifSupplierBlockedByCrNo(item.CrNumber, tender.AgencyCode);
                    if (checkBlockList)
                    {
                        continue;
                    }
                    else
                    {
                        tender.UpdateUnregesteredInvitations();
                        var emails = UnRegesteredsuppliers.Where(t => t.Email != null).Select(x => x.Email).ToList();
                        var mobileNos = UnRegesteredsuppliers.Where(t => t.MobileNo != null).Select(x => x.MobileNo).ToList();
                        await _notificationAppService.SendInvitationByEmail(emails, tender);
                        await _notificationAppService.SendInvitationBySms(mobileNos, tender);
                    }
                }
            }
        }

        private async Task<bool> IsFrameworkTenderCheckValid(Tender tender)
        {
            if (tender.TenderTypeId != (int)Enums.TenderType.FrameworkAgreement)
                return false;
            var result = await _tenderQueries.IsFrameworkTenderCheckValid(tender);
            return result;
        }

        private async Task SadadDetailsForAgency(Tender tender)
        {
            var billModel = new BillModel
            {
                AgencyCode = tender.AgencyCode,
                AgencyType = tender.Agency.CategoryId.Value,
            };
            billModel = await CheckSadadDetailsForAgency(billModel);
            string calculateAgencyCodeFromIDM = billModel.ChapterNumber + billModel.BankBranchId + billModel.SectionId + billModel.SequenceNumber + billModel.NumOfSubDepartments + billModel.NumOfSubSections;
            if (calculateAgencyCodeFromIDM == "" || calculateAgencyCodeFromIDM.Length < 18 || calculateAgencyCodeFromIDM == "000000000000000000")
            {
                throw new BusinessRuleException(Resources.TenderResources.Messages.IDmSadadDetailsError);
            }
        }

        private async Task SendNotifcationsOnUnitApprove(Tender tender)
        {
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel mainNotificationTemplateForDataEnryModel = new MainNotificationTemplateModel(NotificationArguments, $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}",
                          NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId);
            await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.UnitSecrtaryLevel1.OperationsOnTheTender.reviewTender, null, mainNotificationTemplateForDataEnryModel);

        }

        public async Task ApproveTenderRelatedWithVRO(int tenderId)
        {
            Tender tender = await _tenderQueries.FindTenderForApproval(tenderId);
            tender.UpdateTenderStatus(Enums.TenderStatus.PendingVROAuditerApprove, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.PendingVROAuditerApprove);

            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[]{ "", tender.ReferenceNumber,
                                               tender.Purpose, tender.LastEnqueriesDate?.ToString("dd/MM/yyyy hh:mm tt"),
                                               tender.LastOfferPresentationDate?.ToString("dd/MM/yyyy hh:mm tt"),
                                              tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt")  },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            var VROOfficeCode = tender.Agency.VROOfficeCode;
            List<int> VROBranches = await _branchAppService.GetVROBranchsByVROOfficeCode(VROOfficeCode);
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(NotificationArguments, $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId);
            await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.OperationsOnTheTender.sendrelatedvrotender, tender.BranchId, approveTender);
            foreach (var item in VROBranches)
            {
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.EtimadOfficer.OperationsOnTheTender.sendrelatedvrotender, item, approveTender);
            }
            await _tenderCommands.UpdateAsync(tender);
        }
        private void IsValidToApproveTender(Tender tender)
        {
            if (tender == null)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            }
            if (tender.LastOfferPresentationDate < DateTime.Now)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CannotApproveFinishedTender);
            }
            if (tender.TenderStatusId != (int)Enums.TenderStatus.Pending && tender.TenderStatusId != (int)Enums.TenderStatus.PendingVROAuditerApprove)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.Pending));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }
        public async Task ApproveTenderVRO(ApproveVROModel approveVROModel, int branchId)
        {
            int tenderId = Util.Decrypt(approveVROModel.TenderIdString);
            await _verification.CheckForVerificationCode(tenderId, approveVROModel.VerificationCode, (int)Enums.VerificationType.Tender);
            Tender tender = await _tenderQueries.FindTenderForApproval(tenderId);
            IsValidToApproveTender(tender);
            TenderDatesModel datesModel = new TenderDatesModel
            {
                LastEnqueriesDate = approveVROModel.LastEnqueriesDate,
                LastOfferPresentationDate = approveVROModel.LastOfferPresentationDate,
                OffersOpeningDate = approveVROModel.OffersOpeningDate,
                OffersOpeningAddress = approveVROModel.OffersOpeningAddress
            };
            _tenderDomainService.IsValidToUpdateDate(datesModel, tender);
            tender.UpdateTenderStatus(Enums.TenderStatus.Approved, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ApproveTender);
            if (approveVROModel.OffersOpeningAddress != null)
            {
                Address address = new Address(approveVROModel.OffersOpeningAddress, (int)Enums.AddressType.OfferOpeningAddress, branchId);
                Check.ArgumentNotNull(nameof(address), address);
                _cache.Remove(CacheKeys.AddressesCache);
                approveVROModel.OffersOpeningAddressId = await _tenderCommands.CreateAddressAsync(address);
            }
            tender.UpdateTenderInApproveVRO(approveVROModel.LastEnqueriesDate, approveVROModel.LastOfferPresentationDate, approveVROModel.OffersOpeningDate, approveVROModel.OpenCheckCommitteeId, approveVROModel.TechnicalOrganizationId, approveVROModel.OffersOpeningAddressId);
            tender.UpdateTenderBranchAndAgency(_httpContextAccessor.HttpContext.User.UserBranch(), _httpContextAccessor.HttpContext.User.SupplierAgency());
            if (tender.Invitations.Any(c => c.IsActive == true && c.StatusId == (int)InvitationStatus.ToBeSent))
            {
                var suppliers = tender.Invitations.Select(t => new SupplierInvitationModel { CommericalRegisterNo = t.CommericalRegisterNo, TenderId = tenderId }).ToList();
                await SendInvitationsToUnBlockedSuppliers(suppliers, tender);
                var UnRegesteredsuppliers = tender.UnRegisteredSuppliersInvitation.Where(x => x.IsActive == true && x.InvitationStatusId == (int)InvitationStatus.ToBeSent).ToList();
                if (UnRegesteredsuppliers.Any())
                {
                    tender.UpdateUnregesteredInvitations();
                    var emails = UnRegesteredsuppliers.Where(t => t.Email != null).Select(x => x.Email).ToList();
                    var mobileNos = UnRegesteredsuppliers.Where(t => t.MobileNo != null).Select(x => x.MobileNo).ToList();
                    await _notificationAppService.SendInvitationByEmail(emails, tender);
                    await _notificationAppService.SendInvitationBySms(mobileNos, tender);
                }
            }
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[]{ "", tender.ReferenceNumber,
                                               tender.Purpose, tender.LastEnqueriesDate?.ToString("dd/MM/yyyy hh:mm tt"),
                                               tender.LastOfferPresentationDate?.ToString("dd/MM/yyyy hh:mm tt"),
                                              tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt")  },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(NotificationArguments, $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId);
            await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.PurshaseSpecialist.OperationsOnTheTender.approvedtender, tender.BranchId, approveTender);
            await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.EtimadOfficer.OperationsOnTheTender.approvedtender, tender.BranchId, approveTender);
            await _tenderCommands.UpdateAsync(tender);
        }

        public async Task<bool> SendInvitationsToUnBlockedSuppliers(List<SupplierInvitationModel> suppliers, Tender tender)
        {
            var AgencyCode = _httpContextAccessor.HttpContext.User.UserAgency();
            NotificationArguments SupplierNotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "",
                    _httpContextAccessor.HttpContext.User.UserAgencyName(),
                    tender.Branch?.BranchName,
                    tender.ReferenceNumber,
                    tender.TenderName,
                    tender.Purpose,
                    tender.LastEnqueriesDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.LastEnqueriesDate.Value.ToString("dd/MM/yyyy"),
                    tender.LastOfferPresentationDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.LastOfferPresentationDate.Value.ToString("dd/MM/yyyy"),
                    tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"),
                    tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { "", tender.ReferenceNumber },
                SMSArgs = new object[] { "", tender.ReferenceNumber }
            };
            List<string> lstCrs = new List<string>();
            List<SupplierInvitationModel> invitedSupplierLst = new List<SupplierInvitationModel>();
            MainNotificationTemplateModel inviteSupplierModel = new MainNotificationTemplateModel(SupplierNotificationArguments, $"Tender/SupplierInvitationTenders", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId);
            if (suppliers.Any())
            {
                foreach (var item in suppliers)
                {
                    if (string.IsNullOrEmpty(item.CommericalRegisterNo) || AgencyCode == item.CommericalRegisterNo)
                    {
                        continue;
                    }
                    var checkBlockList = await _blockAppService.CheckifSupplierBlockedByCrNo(item.CommericalRegisterNo, AgencyCode);
                    if (checkBlockList)
                    {
                        continue;
                    }
                    else
                    {
                        invitedSupplierLst.Add(item);
                        lstCrs.Add(item.CommericalRegisterNo);
                    }
                }
                tender.SendInvitations(lstCrs);
                await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.invitevendorstotender, invitedSupplierLst.Select(i => i.CommericalRegisterNo).ToList(), inviteSupplierModel);
            }
            return true;
        }

        public async Task RejectTender(int tenderId, string rejectionReason)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.GetTenderByIdWithoutAnyIncluds(tenderId);
            int oldStatus = tender.TenderStatusId;
            IsValidToRejectTender(tender);
            tender.UpdateTenderStatus(Enums.TenderStatus.Rejected, rejectionReason, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.TenderHasBeenRejected);
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] {  "", tender.ReferenceNumber,
                        tender.TenderHistories.FirstOrDefault(h => h.StatusId == (int)Enums.TenderStatus.Rejected).RejectionReason,
                        tender.Purpose, tender.LastEnqueriesDate?.ToString("dd/MM/yyyy hh:mm tt"),
                        tender.LastOfferPresentationDate?.ToString("dd/MM/yyyy hh:mm tt"),
                       tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt")  },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
              $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}",
              NotificationEntityType.Tender,
              tender.TenderId.ToString(),
              tender.BranchId);
            if (tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects && oldStatus == (int)Enums.TenderStatus.PendingVROAuditerApprove)
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.OperationsOnTheTender.rejecttender, tender.BranchId, mainNotificationTemplateModel);
            else if (tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.PurshaseSpecialist.OperationsOnTheTender.rejecttender, tender.BranchId, mainNotificationTemplateModel);
            else
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.OperationsOnTheTender.rejecttender, tender.BranchId, mainNotificationTemplateModel);
            await _tenderCommands.UpdateAsync(tender);
        }

        private void IsValidToRejectTender(Tender tender)
        {
            if (tender == null)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            }
            if (tender.TenderStatusId != (int)Enums.TenderStatus.Pending && tender.TenderStatusId != (int)Enums.TenderStatus.PendingVROAuditerApprove)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.Pending));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        public async Task ReopenTender(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.GetTenderByIdWithoutAnyIncluds(tenderId);
            IsValidToReopenTender(tender);
            tender.UpdateTenderStatus(Enums.TenderStatus.Established, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.TenderReopenedForUpdating);
            await _tenderCommands.UpdateAsync(tender);
        }
        private void IsValidToReopenTender(Tender tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (!(tender.TenderStatusId == (int)Enums.TenderStatus.Rejected || tender.TenderStatusId == (int)Enums.TenderStatus.RejectedFromUnit || tender.TenderStatusId == (int)Enums.TenderStatus.ReturnedFromUnitToAgencyForEdit))
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.Rejected));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }
        public async Task<Tender> DeleteAsync(int tenderId)
        {
            Tender tender = await _tenderQueries.FindTenderByTenderId(tenderId);
            tender.DeActive();
            tender.AddActionHistory(tender.TenderStatusId, TenderActions.DeleteTender, "", _httpContextAccessor.HttpContext.User.UserId());
            return await _tenderCommands.UpdateAsync(tender);
        }

        #endregion

        #region Opening Stage Actions

        public async Task OpenTenderOffer(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderByTenderId(tenderId);
            IsValidToStartOpenTender(tender);

            Enums.TenderStatus newStatus = tender.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase
                && tender.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.TwoSepratedFiles ? Enums.TenderStatus.OffersTechnicalOppening : Enums.TenderStatus.OffersOppening;
            var historyAction = tender.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase
                && tender.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.TwoSepratedFiles ? Enums.TenderActions.OpenTechnicalEnvelope : Enums.TenderActions.OpenEnvelope;
            if (_rootConfiguration.OldFlow.isApplied)
            {
                newStatus = (newStatus == Enums.TenderStatus.OffersTechnicalOppening && !tender.IsOldFlow(_rootConfiguration.OldFlow.EndDate.ToDateTime())) ? Enums.TenderStatus.OffersTechnicalOppening : Enums.TenderStatus.OffersOppening;
                historyAction = (historyAction == Enums.TenderActions.OpenTechnicalEnvelope && !tender.IsOldFlow(_rootConfiguration.OldFlow.EndDate.ToDateTime())) ? TenderActions.OpenTechnicalEnvelope : Enums.TenderActions.OpenEnvelope;
            }

            tender.UpdateTenderStatus(newStatus, "", _httpContextAccessor.HttpContext.User.UserId(), historyAction);
            await _tenderCommands.UpdateAsync(tender);
        }

        private void IsValidToStartOpenTender(Tender tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.TenderStatusId != (int)Enums.TenderStatus.Approved)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.Approved));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
            if (tender.OffersOpeningDate.HasValue && tender.OffersOpeningDate.Value.Date > DateTime.Today.Date && tender.TenderTypeId != (int)Enums.TenderType.CurrentDirectPurchase)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.OfferOpenningMoreThanToday);
        }


        public async Task SendTenderToApproveOppenning(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderByTenderId(tenderId);
            IsValidToSendTenderToApproveOppenning(tender);

            Enums.TenderStatus newStatus = tender.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase
                && tender.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.TwoSepratedFiles ? Enums.TenderStatus.OffersTechnicalOppeningPending : Enums.TenderStatus.OffersOppenedPending;
            var historyAction = tender.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase
                && tender.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.TwoSepratedFiles ? Enums.TenderActions.SendTechnicalOpenEnvelopeReportForApproval : Enums.TenderActions.OpenEnvelope;
            if (_rootConfiguration.OldFlow.isApplied)
            {
                //OffersTechnicalOppenedPending
                //OffersOppenedPending
                newStatus = (newStatus == Enums.TenderStatus.OffersTechnicalOppeningPending && !tender.IsOldFlow(_rootConfiguration.OldFlow.EndDate.ToDateTime())) ? Enums.TenderStatus.OffersTechnicalOppeningPending : Enums.TenderStatus.OffersOppenedPending;
                historyAction = (historyAction == Enums.TenderActions.SendTechnicalOpenEnvelopeReportForApproval && !tender.IsOldFlow(_rootConfiguration.OldFlow.EndDate.ToDateTime())) ? TenderActions.SendTechnicalOpenEnvelopeReportForApproval : Enums.TenderActions.OpenEnvelope;
            }

            tender.UpdateTenderStatus(newStatus, "", _httpContextAccessor.HttpContext.User.UserId(), historyAction);
            await SendNotificationForOpeningApproval(tender);
            await _tenderCommands.UpdateAsync(tender);
        }

        private void IsValidToSendTenderToApproveOppenning(Tender tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersOppening && tender.TenderStatusId != (int)Enums.TenderStatus.OffersTechnicalOppening)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersOppening));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        private async Task SendNotificationForOpeningApproval(Tender tender)
        {
            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber, tender.Purpose, tender.LastEnqueriesDate.Value.ToString("dd/MM/yyyy"), tender.LastOfferPresentationDate.Value.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            int codeid = tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningPending ? NotificationOperations.OffersOppeningManager.TransactionsNeedApproval.sendTechnicalopenenvelopesreportforapproval
                :
                NotificationOperations.OffersOppeningManager.TransactionsNeedApproval.sendopenenvelopesreportforapproval;
            MainNotificationTemplateModel approveTenderOpening = new MainNotificationTemplateModel(NotificationArgument, $"Tender/OpenTenderOffers?tenderIdString={Util.Encrypt(tender.TenderId)}&actionName=review", NotificationEntityType.Tender, tender.TenderId.ToString(), 0, tender.OffersOpeningCommitteeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(codeid, tender.OffersOpeningCommitteeId, approveTenderOpening);
        }

        public async Task ApproveTenderOppening(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.GetTenderByIdWithoutAnyIncluds(tenderId);
            IsValidToApproveTenderOppening(tender);
            await RejectTenderCancelRequestWhileTenderApproval(tender, RoleNames.OffersOppeningSecretary);
            var approveStatus = tender.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedPending ? Enums.TenderStatus.OffersOppenedConfirmed : Enums.TenderStatus.OffersTechnicalOppeningConfirmed;
            tender.UpdateTenderStatus(approveStatus, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ApproveOpenEnvelopes);
            await SendNotificationAfterOpeningApproval(tender);
            _genericCommandRepository.Update(tender);
            await _genericCommandRepository.SaveAsync();
        }
        private async Task SendNotificationAfterOpeningApproval(Tender tender)
        {
            List<Offer> offers = await _tenderDomainService.GetReceivedOffersByTenderId(tender.TenderId);
            List<string> suppliers = offers.Select(o => o.CommericalRegisterNo).ToList();
            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", tender.ReferenceNumber, tender.Purpose, tender.LastEnqueriesDate.Value.ToString("dd/MM/yyyy"), tender.LastOfferPresentationDate.Value.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            NotificationArguments NotificationArgumentSupplier = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", tender.ReferenceNumber, tender.Purpose, tender.LastEnqueriesDate, tender.LastOfferPresentationDate, tender.OffersOpeningDate },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            var suppliercode = (tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningConfirmed ? NotificationOperations.Supplier.OperationsOnTheTender.publishtechnicalopenenvelopes
                : NotificationOperations.Supplier.OperationsOnTheTender.publishopenenvelopes);
            var agencycode = (tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningConfirmed ?
                NotificationOperations.OffersOppeningSecretary.OperationsOnTheTender.publishopenTechnicalenvelopesbackend
                : NotificationOperations.OffersOppeningSecretary.OperationsOnTheTender.publishopenenvelopesbackend);
            if (tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningConfirmed)
            {
                NotificationArgumentSupplier.BodyEmailArgs = new object[] { tender.ReferenceNumber };
                NotificationArgument.BodyEmailArgs = new object[] { tender.ReferenceNumber };
            }

            MainNotificationTemplateModel approveTenderOpen = new MainNotificationTemplateModel(NotificationArgument, $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), 0, tender.OffersOpeningCommitteeId);
            MainNotificationTemplateModel approveTenderOpenForSupplier = new MainNotificationTemplateModel(NotificationArgumentSupplier, $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), 0, tender.OffersOpeningCommitteeId);
            await _notificationAppService.SendNotificationForSuppliers(suppliercode, suppliers, approveTenderOpenForSupplier);
            await _notificationAppService.SendNotificationForCommitteeUsers(agencycode, tender.OffersOpeningCommitteeId == null ? 0 : tender.OffersOpeningCommitteeId, approveTenderOpen);
        }
        private void IsValidToApproveTenderOppening(Tender tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersOppenedPending && tender.TenderStatusId != (int)Enums.TenderStatus.OffersTechnicalOppeningPending)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersOppenedPending));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }
        public async Task<Tender> RejectOpenTenderOffers(int tenderId, string rejectionReason)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.GetTenderByIdWithoutAnyIncluds(tenderId);
            IsValidToRejectOpenTender(tender);
            var rejectedStatus = tender.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedPending ? Enums.TenderStatus.OffersOppenedRejected : Enums.TenderStatus.OffersTechnicalOppeningRejected;
            tender.UpdateTenderStatus(rejectedStatus, rejectionReason, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.RejectOpenEnvelopes);
            var result = await _tenderCommands.UpdateAsync(tender);
            await SendNotificationForOpeningRejected(tender);
            return result;
        }
        public void IsValidToRejectOpenTender(Tender tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersOppenedPending && tender.TenderStatusId != (int)Enums.TenderStatus.OffersTechnicalOppeningPending)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersOppenedPending));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        private async Task SendNotificationForOpeningRejected(Tender tender)
        {
            try
            {
                int code = tender.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedRejected ? NotificationOperations.OffersOppeningSecretary.OperationsOnTheTender.rejectopenenvelope
                    : NotificationOperations.OffersOppeningSecretary.OperationsOnTheTender.rejectTechnicalopenenvelope;

                NotificationArguments NotificationArgument = new NotificationArguments
                {
                    BodyEmailArgs = new object[] { "", tender.ReferenceNumber, tender.TenderHistories.OrderByDescending(i => i.TenderHistoryId).FirstOrDefault().RejectionReason, tender.Purpose, tender.LastEnqueriesDate.Value.ToString("dd/MM/yyyy"), tender.LastOfferPresentationDate.Value.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                    SubjectEmailArgs = new object[] { },
                    PanelArgs = new object[] { tender.ReferenceNumber },
                    SMSArgs = new object[] { tender.ReferenceNumber }
                };
                if (code == NotificationOperations.OffersOppeningSecretary.OperationsOnTheTender.rejectTechnicalopenenvelope)
                {
                    NotificationArgument.BodyEmailArgs = new object[] { tender.ReferenceNumber };
                }

                MainNotificationTemplateModel rejectTenderOpening = new MainNotificationTemplateModel(NotificationArgument, $"Tender/OpenTenderOffers?tenderIdString={Util.Encrypt(tender.TenderId)}&actionName=reopen", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, tender.OffersOpeningCommitteeId);
                await _notificationAppService.SendNotificationForCommitteeUsers(code, tender.OffersOpeningCommitteeId == null ? 0 : (int)tender.OffersOpeningCommitteeId, rejectTenderOpening);
                MainNotificationTemplateModel model = new MainNotificationTemplateModel(NotificationArgument, $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, tender.OffersOpeningCommitteeId);
                if (tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
                    await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.PurshaseSpecialist.OperationsOnTheTender.rejectopenenvelope, tender.BranchId, model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }

        public async Task ReopenTenderOffer(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.GetTenderByIdWithoutAnyIncluds(tenderId);
            IsValidToStartReopenTender(tender);
            var reopenstatus = (tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningRejected ? Enums.TenderStatus.OffersTechnicalOppening : Enums.TenderStatus.OffersOppening);
            tender.UpdateTenderStatus(reopenstatus, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.TenderReopenedForEnvelope);
            await _tenderCommands.UpdateAsync(tender);
        }

        private void IsValidToStartReopenTender(Tender tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);

            Enums.TenderStatus statusid = (Enums.TenderStatus)tender.TenderStatusId;
            if (statusid != Enums.TenderStatus.OffersOppenedRejected && statusid != Enums.TenderStatus.OffersTechnicalOppeningRejected)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersOppenedRejected));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        #endregion

        public async Task SendTenderToApproveCheckingWithNewBiddingRound(BiddingDateTimeViewModel biddingDateTimeViewModel)
        {
            int tenderId = Util.Decrypt(biddingDateTimeViewModel.TenderIdString);
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderForCheckingStatusByTenderId(tenderId);
            _tenderDomainService.IsValidToSendToApproveTenderCheckingWithBidding(tender, _httpContextAccessor.HttpContext.User.UserAgency(), biddingDateTimeViewModel);
            tender.UpdateTenderStatus(Enums.TenderStatus.BiddingOffersCheckedPending, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.AddNewBiddingRound);
            await SendNotificationForCheckingApproval(tender);
            if (!tender.Offers.All(a => a.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.NotIdenticalOffer))
                if (tender.BiddingRounds.Any(a => a.IsActive == true && a.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.New))
                {
                    var biddingRound = tender.BiddingRounds.FirstOrDefault(a => a.IsActive == true && a.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.New);
                    biddingRound.UpdateDates(biddingDateTimeViewModel.BiddingStartDateTime, biddingDateTimeViewModel.BiddingEndDateTime);
                }
                else
                    ConfirmStartNewRound(tender, biddingDateTimeViewModel.BiddingStartDateTime, biddingDateTimeViewModel.BiddingEndDateTime);
            await _tenderCommands.UpdateAsync(tender);
        }

        public async Task SaveCheckingDateTime(CheckingDateTimeViewModel checkingDateTimeViewModel)
        {
            int tenderId = Util.Decrypt(checkingDateTimeViewModel.TenderIdString);
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderForCheckingStatusByTenderId(tenderId);
            tender.SetCheckingDateFlagForUnitNotification(true);
            string gregDate = Util.GetGregorianDateWithFormat(checkingDateTimeViewModel.CheckingDateTime, "dd/MM/yyyy hh:mm tt");
            Util.DetermineTimesForDates(checkingDateTimeViewModel.CheckingDateTime, (int)Enums.TimeMessagesType.TimeOfferChecking, _rootConfiguration);
            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { " ", tender.ReferenceNumber, gregDate },
                SubjectEmailArgs = new object[] { " ", tender.ReferenceNumber },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            if (tender.UnitSpacialistWouldLikeToAttendTheCommitte.HasValue && tender.UnitSpacialistWouldLikeToAttendTheCommitte == true)
            {
                TenderUnitAssign tenderUnitAssign = await _tenderQueries.FindLastTenderUnitAssignsByTenderId(tenderId);
                MainNotificationTemplateModel templateModel = new MainNotificationTemplateModel(NotificationArgument, $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId);
                switch (tenderUnitAssign.UnitSpecialistLevel)
                {
                    case (int)Enums.UnitSpecialistLevel.UnitSpecialistLevelOne:
                        await _notificationAppService.SendNotificationDirectByUserId(NotificationOperations.UnitSecrtaryLevel1.OperationsOnTheTender.offerCheckingDateSet, tenderUnitAssign.UserProfileId, templateModel);
                        break;
                    case (int)Enums.UnitSpecialistLevel.UnitSpecialistLevelTwo:
                        await _notificationAppService.SendNotificationDirectByUserId(NotificationOperations.UnitSecrtaryLevel2.OperationsOnTheTender.offerCheckingDateSet, tenderUnitAssign.UserProfileId, templateModel);
                        break;
                    default:
                        break;
                }
            }
            await _tenderCommands.UpdateAsync(tender);
        }

        public async Task SaveFinancialCheckingDateTime(CheckingDateTimeViewModel financialCheckingDateTimeViewModel)
        {
            int tenderId = Util.Decrypt(financialCheckingDateTimeViewModel.TenderIdString);
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderForCheckingStatusByTenderId(tenderId);
            tender.SetFinancialCheckingDateFlagForUnitNotification(true);
            string gregDate = Util.GetGregorianDateWithFormat(financialCheckingDateTimeViewModel.CheckingDateTime, "dd/MM/yyyy hh:mm tt");
            Util.DetermineTimesForDates(financialCheckingDateTimeViewModel.CheckingDateTime, (int)Enums.TimeMessagesType.TimeOfferChecking, _rootConfiguration);
            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { " ", tender.ReferenceNumber, gregDate },
                SubjectEmailArgs = new object[] { " ", tender.ReferenceNumber },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            if (tender.UnitSpacialistWouldLikeToAttendTheCommitte.HasValue && tender.UnitSpacialistWouldLikeToAttendTheCommitte == true)
            {
                TenderUnitAssign tenderUnitAssign = await _tenderQueries.FindLastTenderUnitAssignsByTenderId(tenderId);
                MainNotificationTemplateModel templateModel = new MainNotificationTemplateModel(NotificationArgument, $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId);
                switch (tenderUnitAssign.UnitSpecialistLevel)
                {
                    case (int)Enums.UnitSpecialistLevel.UnitSpecialistLevelOne:
                        await _notificationAppService.SendNotificationDirectByUserId(NotificationOperations.UnitSecrtaryLevel1.OperationsOnTheTender.offerCheckingDateSet, tenderUnitAssign.UserProfileId, templateModel);
                        break;
                    case (int)Enums.UnitSpecialistLevel.UnitSpecialistLevelTwo:
                        await _notificationAppService.SendNotificationDirectByUserId(NotificationOperations.UnitSecrtaryLevel2.OperationsOnTheTender.offerCheckingDateSet, tenderUnitAssign.UserProfileId, templateModel);
                        break;
                    default:
                        break;
                }
            }
            await _tenderCommands.UpdateAsync(tender);
        }

        #region Checking Stage Actions

        public async Task SendTenderToApproveChecking(int tenderId)
        {
            Tender tender = await _tenderQueries.FindTenderWithStatusAndOffersByTenderId(tenderId);
            IsValidToSendTenderToApproveChecking(tender);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersCheckedPending, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.SendTechEvaluationToApproval);
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", tender.ReferenceNumber, tender.Purpose, tender.LastEnqueriesDate, tender.LastOfferPresentationDate, (tender.OffersOpeningDate != null) ? tender.OffersOpeningDate.ToString() : MOF.Etimad.Monafasat.Resources.SharedResources.DisplayInputs.NotFound, (tender.OffersOpeningDate != null) ? tender.OffersOpeningDate?.Hour.ToString() : MOF.Etimad.Monafasat.Resources.SharedResources.DisplayInputs.NotFound },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(NotificationArguments,
            $"Tender/CheckTenderOffers?tenderIdString={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), null, tender.OffersCheckingCommitteeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckManager.OfferStatus.techEvaluationAction, tender.OffersCheckingCommitteeId, approveTender);
            await _tenderCommands.UpdateAsync(tender);
        }

        private void IsValidToSendTenderToApproveChecking(Tender tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            foreach (Offer offer in tender.Offers.Where(x => x.IsActive == true && x.OfferStatusId == (int)Enums.OfferStatus.Received))
            {
                if (offer.OfferAcceptanceStatusId == null && offer.OfferTechnicalEvaluationStatusId == null)
                {
                    throw new BusinessRuleException(Resources.OfferResources.ErrorMessages.OfferStatusOrTechnicalEvaluationNotExist);
                }
            }
            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersChecking)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersChecking));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        public async Task ApproveTenderChecking(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.GetTenderByIdWitOffersAndBidingRounds(tenderId);
            IsValidToApproveTenderChecking(tender);
            await RejectTenderCancelRequestWhileTenderApproval(tender, RoleNames.OffersCheckSecretary);
            if (tender.TenderTypeId == (int)Enums.TenderType.ReverseBidding)
            {
                tender.UpdateTenderStatus(Enums.TenderStatus.BiddingOffersCheckedConfirmed, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ApproveTechnicalEvaluation);
                tender.BiddingRounds.Where(a => a.BiddingRoundStatusId != (int)Enums.BiddingRoundStatus.New && a.IsActive == true).ToList().ForEach(x => x.Deactive());
            }
            else
                tender.UpdateTenderStatus(Enums.TenderStatus.OffersCheckedConfirmed, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ApproveTechnicalEvaluation);

            await SendNotificationForAfterCheckingApproved(tender);
            _genericCommandRepository.Update(tender);
            await _genericCommandRepository.SaveAsync();
        }

        private void IsValidToApproveTenderChecking(Tender tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.TenderTypeId == (int)Enums.TenderType.ReverseBidding && tender.TenderStatusId != (int)Enums.TenderStatus.BiddingOffersCheckedPending)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.BiddingOffersCheckedPending));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
            if (tender.TenderTypeId != (int)Enums.TenderType.ReverseBidding && tender.TenderStatusId != (int)Enums.TenderStatus.OffersCheckedPending)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersCheckedPending));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
            if (tender.TenderTypeId == (int)Enums.TenderType.ReverseBidding && !tender.Offers.All(a => a.IsActive == true && a.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.NotIdenticalOffer)
                && tender.BiddingRounds.Any(a => a.IsActive == true && a.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.New) && DateTime.Now > tender.BiddingRounds.FirstOrDefault(a => a.IsActive == true && a.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.New).EndDate)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderMustHaveActiveOrCommingBiddingRounds);
            }
        }
        public async Task EndOpenFinantialOffersStage(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderWithStatusAndOffersByTenderId(tenderId);
            IsValidToEndOpenFinantialOffersStage(tender);
            if (_rootConfiguration.OldFlow.isApplied && tender.IsOldFlow(_rootConfiguration.OldFlow.EndDate.ToDateTime()))
            {
                tender.UpdateTenderStatus(Enums.TenderStatus.OffersFinancialChecking, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.FinancialCheckingStarted);
            }
            else
            {
                tender.UpdateTenderStatus(Enums.TenderStatus.OffersOpenFinancialStagePending, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.SendFinancialOpeningToApproval);
                NotificationArguments NotificationArgument = new NotificationArguments
                {
                    BodyEmailArgs = new object[] { tender.ReferenceNumber },
                    PanelArgs = new object[] { tender.ReferenceNumber },
                    SMSArgs = new object[] { tender.ReferenceNumber }
                };
                MainNotificationTemplateModel tenderChecking = new MainNotificationTemplateModel(
                  NotificationArgument, $"Tender/CheckTenderOffers?tenderIdString={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, tender.OffersOpeningCommitteeId);
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersOppeningManager.TransactionsNeedApproval.sendFinancialOpeningForApproval, tender.OffersOpeningCommitteeId, tenderChecking);
            }
            await _tenderCommands.UpdateAsync(tender);
        }

        private void IsValidToEndOpenFinantialOffersStage(Tender tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersOpenFinancialStage)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersOpenFinancialStage));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
            if (tender.Offers.Any(o => o.IsActive == true && o.OfferStatusId == (int)Enums.OfferStatus.Received && o.OfferTechnicalEvaluationStatusId == null || o.OfferTechnicalEvaluationStatusId == 0))
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.AllOffersFinancialDataMustBeEntered);
            }
        }
        private async Task SendNotificationForAfterCheckingApproved(Tender tender)
        {
            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber, tender.Purpose,
                        tender.LastEnqueriesDate?.ToString("dd/MM/yyyy hh:mm tt"), tender.LastOfferPresentationDate?.ToString("dd/MM/yyyy hh:mm tt"),
                       tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt")  },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            IEnumerable<Offer> offers = (await _tenderQueries.GetOffersByTenderIdAsync(tender.TenderId)).Items;
            List<string> passedSuppliers = offers.Where(e => e.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer).Select(e => e.Supplier.SelectedCr).ToList();
            List<string> failedSuppliers = offers.Where(e => e.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.NotIdenticalOffer).Select(e => e.Supplier.SelectedCr).ToList();
            MainNotificationTemplateModel approveTenderCheckingForSupplier = new MainNotificationTemplateModel(NotificationArgument, $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.publishtechnicalevaluation, passedSuppliers, approveTenderCheckingForSupplier);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.SupplierTechnicalOfferRejected, failedSuppliers, approveTenderCheckingForSupplier);

            if (tender.TenderTypeId == (int)Enums.TenderType.ReverseBidding)
            {
                NotificationArguments biddingNotificationArgument = new NotificationArguments
                {
                    BodyEmailArgs = new object[] { "", tender.ReferenceNumber, tender.Purpose,
                        tender.LastEnqueriesDate?.ToString("dd/MM/yyyy hh:mm tt"), tender.LastOfferPresentationDate?.ToString("dd/MM/yyyy hh:mm tt"),
                        tender.OffersOpeningDate == null  ?Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt"),
                        tender.BiddingRounds.FirstOrDefault(a => a.IsActive == true && a.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.New)?.StartDate.ToString("dd/MM/yyyy hh:mm tt"),
                        tender.BiddingRounds.FirstOrDefault(a => a.IsActive == true && a.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.New)?.EndDate.ToString("dd/MM/yyyy hh:mm tt")
                    },
                    SubjectEmailArgs = new object[] { },
                    PanelArgs = new object[] { tender.ReferenceNumber },
                    SMSArgs = new object[] { tender.ReferenceNumber }
                };
                MainNotificationTemplateModel approveBiddingTenderCheckingForSupplier = new MainNotificationTemplateModel(biddingNotificationArgument, $"Tender/TenderBiddingViewAsync?tenderIdString={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId);
                await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.AddNewRound, offers.Where(a => a.OfferTechnicalEvaluationStatusId == (int)OfferTechnicalEvaluationStatusId.IdenticalOffer).Select(e => e.Supplier.SelectedCr).ToList(), approveBiddingTenderCheckingForSupplier);
            }
            MainNotificationTemplateModel approveTenderChecking = new MainNotificationTemplateModel(
               NotificationArgument, $"Tender/CheckTenderOffers?tenderIdString={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, tender.OffersCheckingCommitteeId);

            if (_rootConfiguration.OldFlow.isApplied && !tender.IsOldFlow(_rootConfiguration.OldFlow.EndDate.ToDateTime()))
            {
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersOppeningSecretary.OperationsOnTheTender.publishtechnicalevaluation, tender.OffersOpeningCommitteeId, approveTenderChecking);
            }
            await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.OperationsOnTheTender.publishtechnicalevaluation, tender.OffersCheckingCommitteeId, approveTenderChecking);
        }

        public async Task RejectCheckTenderOffers(int tenderId, string rejectionReason)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.GetTenderByIdWithoutAnyIncluds(tenderId);
            IsValidToRejectCheckTender(tender);
            if (tender.TenderTypeId == (int)Enums.TenderType.ReverseBidding)
                tender.UpdateTenderStatus(Enums.TenderStatus.BiddingOffersCheckedRejected, rejectionReason, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.TenderTechnicalEvaluationWasRejected);
            else
                tender.UpdateTenderStatus(Enums.TenderStatus.OffersCheckedRejected, rejectionReason, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.TenderTechnicalEvaluationWasRejected);
            await SendNotificationForCheckingRejected(tender);
            await _tenderCommands.UpdateAsync(tender);
        }

        private void IsValidToRejectCheckTender(Tender tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.TenderTypeId == (int)Enums.TenderType.ReverseBidding && tender.TenderStatusId != (int)Enums.TenderStatus.BiddingOffersCheckedPending)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.BiddingOffersCheckedPending));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
            if (tender.TenderTypeId != (int)Enums.TenderType.ReverseBidding && tender.TenderStatusId != (int)Enums.TenderStatus.OffersCheckedPending)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersCheckedPending));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }


        private async Task SendNotificationForCheckingRejected(Tender tender)
        {
            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", tender.ReferenceNumber, tender.Purpose,
                        tender.LastEnqueriesDate?.ToString("dd/MM/yyyy hh:mm tt"), tender.LastOfferPresentationDate?.ToString("dd/MM/yyyy hh:mm tt"),
                       tender.OffersOpeningDate == null  ?Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt")   },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel rejectTender = new MainNotificationTemplateModel(NotificationArgument, $"Tender/CheckTenderOffers?tenderIdString={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, tender.OffersCheckingCommitteeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.OperationsOnTheTender.rejecttechnicalevaluation, tender.OffersCheckingCommitteeId, rejectTender);
        }

        public async Task ReopenTenderChecking(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.GetTenderByIdWithoutAnyIncluds(tenderId);
            IsValidToReopenCheckTender(tender);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersChecking, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.TenderReopenedForTechnicalEvaluation);
            await _tenderCommands.UpdateAsync(tender);
        }

        private void IsValidToReopenCheckTender(Tender tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.TenderTypeId == (int)Enums.TenderType.ReverseBidding && tender.TenderStatusId != (int)Enums.TenderStatus.BiddingOffersCheckedRejected && tender.TenderStatusId != (int)Enums.TenderStatus.BackForCheckingFromPlaint)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.BiddingOffersCheckedRejected));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
            if (tender.TenderTypeId != (int)Enums.TenderType.ReverseBidding && tender.TenderStatusId != (int)Enums.TenderStatus.OffersCheckedRejected && tender.TenderStatusId != (int)Enums.TenderStatus.BackForCheckingFromPlaint)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersCheckedRejected));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        #endregion

        #region Two Files tender offers checking stage 

        #region Technical Check

        public async Task SendTenderToApproveTechnicalCheckingAsync(int tenderId, string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderWithStatusAndOffersByTenderId(tenderId);
            IsValidToSendToApproveTenderTechnicalChecking(tender, agencyCode);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersTechnicalCheckingPending, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.SendTechEvaluationToApproval);
            await SendNotificationForCheckingApproval(tender);
            await _tenderCommands.UpdateAsync(tender);
        }

        private void IsValidToSendToApproveTenderTechnicalChecking(Tender tender, string agencyCode)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.AgencyCode != agencyCode)
                throw new UnHandledAccessException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersChecking && tender.TenderStatusId != (int)Enums.TenderStatus.OffersTechnicalChecking)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersChecking));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
            if (tender.Offers.Where(o => o.IsActive == true && o.OfferStatusId == (int)Enums.OfferStatus.Received).Any(o => !o.OfferTechnicalEvaluationStatusId.HasValue))
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.AllTenderOffersMustBeTechnicallyChecked);
            }
        }

        public async Task ApproveTendeTechnicalCheckingAsync(int tenderId, string verificationCode, string agencyCode)
        {
            await _verification.CheckForVerificationCode(tenderId, verificationCode, (int)Enums.VerificationType.Tender);
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.GetTenderByIdWithoutAnyIncluds(tenderId);
            IsValidToApproveOrRejectTenderTechnicalChecking(tender, agencyCode);
            await RejectTenderCancelRequestWhileTenderApproval(tender, RoleNames.OffersCheckSecretary);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersTechnicalCheckingConfirmed, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ApproveTechnicalEvaluation);
            await SendNotificationForAfterCheckingApproved(tender);
            _genericCommandRepository.Update(tender);
            await _genericCommandRepository.SaveAsync();

        }

        public async Task RejectTendeTechnicalCheckingAsync(int tenderId, string rejectionReason, string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderWithStatusAndOffersByTenderId(tenderId);
            IsValidToApproveOrRejectTenderTechnicalChecking(tender, agencyCode);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersTechnicalCheckingRejected, rejectionReason, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.TenderTechnicalEvaluationWasRejected);
            await SendNotificationForCheckingRejected(tender);
            await _tenderCommands.UpdateAsync(tender);
        }
        private void IsValidToApproveOrRejectTenderTechnicalChecking(Tender tender, string agencyCode)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.AgencyCode != agencyCode)
                throw new UnHandledAccessException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersTechnicalCheckingPending)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersTechnicalCheckingPending));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        public async Task ReOpenTendeTechnicalCheckingAsync(int tenderId, string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderWithStatusAndOffersByTenderId(tenderId);
            IsValidToReOpenTenderTechnicalChecking(tender, agencyCode);
            if (_rootConfiguration.OldFlow.isApplied && !tender.IsOldFlow(_rootConfiguration.OldFlow.EndDate.ToDateTime()))
            {
                if (tender.TenderStatusId == (int)Enums.TenderStatus.BackForCheckingFromPlaint)
                {
                    tender.UpdateTenderStatus(Enums.TenderStatus.OffersTechnicalOppeningConfirmed, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.TenderReopenedForTechnicalEvaluation);
                    tender.SetCheckingDateFlagForUnitNotification(false);
                }
                else
                    tender.UpdateTenderStatus(Enums.TenderStatus.OffersTechnicalChecking, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.TenderReopenedForTechnicalEvaluation);
            }
            else
                tender.UpdateTenderStatus(Enums.TenderStatus.OffersChecking, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.TenderReopenedForTechnicalEvaluation);
            await _tenderCommands.UpdateAsync(tender);
        }

        private void IsValidToReOpenTenderTechnicalChecking(Tender tender, string agencyCode)
        {
            if (tender.AgencyCode != agencyCode)
                throw new UnHandledAccessException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersTechnicalCheckingRejected && tender.TenderStatusId != (int)Enums.TenderStatus.BackForCheckingFromPlaint)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersTechnicalCheckingRejected));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        #endregion Tehcnical check

        #region Financial stage

        public async Task MoveTenderToFinancialOffersChecking(int tenderId)
        {
            var tender = await _tenderQueries.FindTenderWithStatusAndOffersByTenderId(tenderId);
            IsValidToMoveTenderToFinancialOffersChecking(tender);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersFinancialChecking, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.FinancialCheckingStarted);
            await _tenderCommands.UpdateAsync(tender);
        }

        private void IsValidToMoveTenderToFinancialOffersChecking(Tender tender)
        {
            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersOpenFinancialStage)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersOpenFinancialStage));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }

            if (!tender.Offers.Any(o => o.IsActive == true && o.OfferTechnicalEvaluationStatusId == null || o.OfferTechnicalEvaluationStatusId == 0))
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.AllOffersFinancialDataMustBeEntered);
            }
        }
        public async Task SendTenderToApproveFinancailCheckingAsync(int tenderId, string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderWithStatusAndOffersByTenderId(tenderId);
            IsValidToSendToApproveTenderFinancailChecking(tender, agencyCode);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersFinancialOfferCheckingPending, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.SendFinancialEvaluationToApproval);
            await SendNotificationToManagerForFinancialCheckingApproval(tender);
            await _tenderCommands.UpdateAsync(tender);
        }
        private void IsValidToSendToApproveTenderFinancailChecking(Tender tender, string agencyCode)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.AgencyCode != agencyCode)
                throw new UnHandledAccessException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersFinancialChecking)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersFinancialChecking));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
            if (tender.Offers.Where(o => o.IsActive == true && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer).Any(o => !o.OfferAcceptanceStatusId.HasValue))
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.AllTendersMustBeFinanciallyChecked);
            }
        }
        private async Task SendNotificationToManagerForFinancialCheckingApproval(Tender tender)
        {
            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", tender.ReferenceNumber, tender.Purpose,
                        tender.LastEnqueriesDate?.ToString("dd/MM/yyyy hh:mm tt"), tender.LastOfferPresentationDate?.ToString("dd/MM/yyyy hh:mm tt"),
                       tender.OffersOpeningDate == null  ?Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel tenderChecking = new MainNotificationTemplateModel(
              NotificationArgument, $"Tender/CheckTenderOffers?tenderIdString={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, tender.OffersCheckingCommitteeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckManager.OperationsNeedToBeAccredited.SendFinancialCheckingForApproval, tender.OffersCheckingCommitteeId, tenderChecking);
        }

        public async Task ApproveTendeFinancialCheckingAsync(int tenderId, string verificationCode, string agencyCode)
        {
            await _verification.CheckForVerificationCode(tenderId, verificationCode, (int)Enums.VerificationType.Tender);
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.GetTenderByIdWithoutAnyIncluds(tenderId);
            IsValidToApproveOrRejectTenderFinancialChecking(tender, agencyCode);
            await RejectTenderCancelRequestWhileTenderApproval(tender, RoleNames.OffersCheckSecretary);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersFinancialOfferCheckingApproved, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ApproveFinancialOpening);
            await SendNotificationAfterFinancialCheckingApproval(tender);
            _genericCommandRepository.Update(tender);
            await _genericCommandRepository.SaveAsync();

        }

        private async Task SendNotificationAfterFinancialCheckingApproval(Tender tender)
        {

            NotificationArguments NotificationArgumentSupplier = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.TenderName },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.TenderName },
                SMSArgs = new object[] { tender.TenderName }
            };
            List<Offer> acceptedOffers = await _tenderDomainService.GetFinancialAcceeptedOffersByTenderId(tender.TenderId);
            List<Offer> rejectedOffers = await _tenderDomainService.GetFinancialRejectedOffersByTenderId(tender.TenderId);
            List<string> acceptedSuppliers = acceptedOffers.Select(o => o.CommericalRegisterNo).ToList();
            List<string> rejectedSuppliers = rejectedOffers.Select(o => o.CommericalRegisterNo).ToList();
            MainNotificationTemplateModel approveTenderCheckingForSupplier = new MainNotificationTemplateModel(NotificationArgumentSupplier,
                $"Tender/DetailsForSupplier?STenderId={Util.Encrypt(tender.TenderId)}",
                NotificationEntityType.Tender,
                tender.TenderName,
                tender.BranchId);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.SupplierFinacialOffersAccepted, acceptedSuppliers, approveTenderCheckingForSupplier);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.SupplierFinacialOffersRejected, rejectedSuppliers, approveTenderCheckingForSupplier);

            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", tender.ReferenceNumber, tender.Purpose,
                        tender.LastEnqueriesDate?.ToString("dd/MM/yyyy hh:mm tt"), tender.LastOfferPresentationDate?.ToString("dd/MM/yyyy hh:mm tt"),
                       tender.OffersOpeningDate == null  ?Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel tenderChecking = new MainNotificationTemplateModel(
              NotificationArgument, $"Tender/CheckTenderOffers?tenderIdString={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, tender.OffersCheckingCommitteeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.OperationsOnTheTender.ApproveFinancialChecking, tender.OffersCheckingCommitteeId, tenderChecking);
        }

        public async Task RejectTendeFinancialCheckingAsync(int tenderId, string rejectionReason, string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderWithStatusAndOffersByTenderId(tenderId);
            IsValidToApproveOrRejectTenderFinancialChecking(tender, agencyCode);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersFinancialOfferCheckingRejected, rejectionReason, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.RejectFinancialOpening);
            await SendNotificationAfterFinancialCheckingRejection(tender);
            await _tenderCommands.UpdateAsync(tender);
        }

        private void IsValidToApproveOrRejectTenderFinancialChecking(Tender tender, string agencyCode)
        {
            if (tender.AgencyCode != agencyCode)
                throw new UnHandledAccessException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersFinancialOfferCheckingPending)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersFinancialOfferCheckingPending));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        private async Task SendNotificationAfterFinancialCheckingRejection(Tender tender)
        {
            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", tender.ReferenceNumber, tender.Purpose,
                        tender.LastEnqueriesDate?.ToString("dd/MM/yyyy hh:mm tt"), tender.LastOfferPresentationDate?.ToString("dd/MM/yyyy hh:mm tt"),
                       tender.OffersOpeningDate == null  ?Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel tenderChecking = new MainNotificationTemplateModel(
              NotificationArgument, $"Tender/CheckTenderOffers?tenderIdString={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, tender.OffersCheckingCommitteeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.OperationsOnTheTender.RejectFinancialChecking, tender.OffersCheckingCommitteeId, tenderChecking);
        }

        public async Task ReOpenTendeFinancialCheckingAsync(int tenderId, string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderWithStatusAndOffersByTenderId(tenderId);
            IsValidToReOpenTenderFinancialChecking(tender, agencyCode);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersFinancialChecking, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.TenderReopenedForFinancialEvaluation);
            await _tenderCommands.UpdateAsync(tender);
        }

        private void IsValidToReOpenTenderFinancialChecking(Tender tender, string agencyCode)
        {
            if (tender.AgencyCode != agencyCode)
                throw new UnHandledAccessException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersFinancialOfferCheckingRejected)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersFinancialOfferCheckingRejected));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        public async Task ApproveTendeFinancialOpeningAsync(int tenderId, string verificationCode)
        {
            await _verification.CheckForVerificationCode(tenderId, verificationCode, (int)Enums.VerificationType.Tender);
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderToApproveFinancialOpening(tenderId);
            _tenderDomainService.IsValidToApproveOrRejectTenderFinancialOpening(tender, _httpContextAccessor.HttpContext.User.UserAgency());
            await RejectTenderCancelRequestWhileTenderApproval(tender, RoleNames.OffersOppeningSecretary);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersOpenFinancialStageApproved, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ApproveFinancialEvaluation);
            await SendNotificationAfterFinancialOpeningApproval(tender);
            _genericCommandRepository.Update(tender);
            await _genericCommandRepository.SaveAsync();
        }

        private async Task SendNotificationAfterFinancialOpeningApproval(Tender tender)
        {
            IEnumerable<Offer> offers = (await _tenderQueries.GetOffersByTenderIdAsync(tender.TenderId)).Items;
            List<string> suppliers = offers.Where(e => e.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer).Select(e => e.Supplier.SelectedCr).ToList();
            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel tenderOpening = new MainNotificationTemplateModel(
              NotificationArgument, $"Tender/CheckTenderOffers?tenderIdString={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, tender.OffersOpeningCommitteeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersOppeningSecretary.OperationsOnTheTender.ApproveFinancialOpening, tender.OffersOpeningCommitteeId, tenderOpening);


            MainNotificationTemplateModel approveTenderOpenForSupplier = new MainNotificationTemplateModel(NotificationArgument, $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), 0, tender.OffersOpeningCommitteeId);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.financialOffersOpeningApproved, suppliers, approveTenderOpenForSupplier);
        }

        public async Task RejectTendeFinancialOpeningAsync(int tenderId, string rejectionReason)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderWithStatusAndOffersByTenderId(tenderId);
            _tenderDomainService.IsValidToApproveOrRejectTenderFinancialOpening(tender, _httpContextAccessor.HttpContext.User.UserAgency());
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersOpenFinancialStageRejected, rejectionReason, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.RejectFinancialOpening);
            await SendNotificationAfterFinancialOpeningRejection(tender);
            await _tenderCommands.UpdateAsync(tender);
        }

        private async Task SendNotificationAfterFinancialOpeningRejection(Tender tender)
        {
            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel tenderChecking = new MainNotificationTemplateModel(
              NotificationArgument, $"Tender/CheckTenderOffers?tenderIdString={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, tender.OffersOpeningCommitteeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersOppeningSecretary.OperationsOnTheTender.RejectFinancialOpening, tender.OffersOpeningCommitteeId, tenderChecking);
        }

        public async Task ReOpenTendeFinancialOpeningAsync(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderWithStatusAndOffersByTenderId(tenderId);
            _tenderDomainService.IsValidToReOpenTenderFinancialOpening(tender, _httpContextAccessor.HttpContext.User.UserAgency());
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersOpenFinancialStage, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ReopenFinancialOpening);
            await _tenderCommands.UpdateAsync(tender);
        }
        #endregion financail stage

        #endregion Two Files tender offers checking stage 

        #region Direct purchase checking

        public async Task<TenderOffersModel> FindDirectPurchaseTenderOfferDetailsByTenderIdForCheckStage(int tenderId, int userId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Check.ArgumentNotNullOrEmpty(nameof(userId), userId.ToString());
            var tender = await _tenderQueries.FindDirectPurchaseTenderOfferDetailsByTenderIdForCheckStage(tenderId, userId);
            if (tender == null)
                throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            await _tenderDomainService.CheckIfUserCanAccessLowBudgetTender(tender.IsLowBudgetDirectPurchase, tender.DirectPurchaseMemberId, userId);
            return tender;
        }

        public async Task StartDirectPurchaseTenderCheckingOffers(int tenderId, string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderForOpenCheckStageByTenderId(tenderId);
            IsValidToStartDirectPurchaseCheckingTender(tender, agencyCode);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersChecking, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.StartDirectPurchaseTenderCheckingOffers);
            await _tenderCommands.UpdateAsync(tender);
        }

        private void IsValidToStartDirectPurchaseCheckingTender(Tender tender, string agencyCode)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.AgencyCode != agencyCode)
                throw new UnHandledAccessException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.Approved)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.Approved));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
            if (DateTime.Now < tender.OffersCheckingDate)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CheckingDateNotStartedYet);
            }
            if (tender.IsLowBudgetDirectPurchase == true && tender.DirectPurchaseMemberId != _httpContextAccessor.HttpContext.User.UserId())
            {
                throw new UnHandledAccessException();
            }
        }

        public async Task SendDirectPurchaseTenderOffersTechnicalCheckingToApprove(int tenderId, string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderForOpenCheckStageByTenderId(tenderId);
            IsValidToSendDirectPurchaseTechnicalCheckingTenderToApprove(tender, agencyCode);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersTechnicalCheckingPending, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.SendDirectPurchaseTenderOffersTechnicalCheckingToApprove);
            await _tenderCommands.UpdateAsync(tender);
            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel mainNotificationTemplate = new MainNotificationTemplateModel(
                NotificationArgument,
                $"Tender/CheckDirectPurchaseOffers?tenderIdString={Util.Encrypt(tender.TenderId)}",
                NotificationEntityType.Tender,
                tender.TenderId.ToString(),
                null,
                tender.DirectPurchaseCommitteeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(
                NotificationOperations.DirectPurchaseManager.OperationsOnTheTender.ApproveTenderCheck,
                tender.DirectPurchaseCommitteeId,
                mainNotificationTemplate);
        }

        private void IsValidToSendDirectPurchaseTechnicalCheckingTenderToApprove(Tender tender, string agencyCode)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.AgencyCode != agencyCode)
                throw new UnHandledAccessException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersChecking)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersChecking));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        public async Task SendDirectPurchaseTenderOffersCheckingToApprove(int tenderId, string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderForOpenCheckStageByTenderId(tenderId);
            IsValidToSendDirectPurchaseCheckingTenderToApprove(tender, agencyCode);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersTechnicalCheckingPending, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.SendDirectPurchaseTenderOffersCheckingToApprove);
            await _tenderCommands.UpdateAsync(tender);
            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel mainNotificationTemplate = new MainNotificationTemplateModel(
                NotificationArgument,
                $"Tender/CheckDirectPurchaseOffers?tenderIdString={Util.Encrypt(tender.TenderId)}",
                NotificationEntityType.Tender,
                tender.TenderId.ToString(),
                null,
                tender.DirectPurchaseCommitteeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(
                NotificationOperations.DirectPurchaseManager.OperationsOnTheTender.ApproveTenderCheck,
                tender.DirectPurchaseCommitteeId,
                mainNotificationTemplate);
        }
        private void IsValidToSendDirectPurchaseCheckingTenderToApprove(Tender tender, string agencyCode)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.AgencyCode != agencyCode)
                throw new UnHandledAccessException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersChecking)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersChecking));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }


        public async Task ApproveDirectPurchaseTenderOffersChecking(int tenderId, string verificationCode, string agencyCode)
        {
            await _verification.CheckForVerificationCode(tenderId, verificationCode, (int)Enums.VerificationType.Tender);
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.GetTenderByIdWithoutAnyIncluds(tenderId);
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            bool isLowBudget = tender.IsLowBudgetDirectPurchase == true;
            if (!isLowBudget)
                await ApproveDirectPurchaseTenderOffersChecking(agencyCode, tender);
            else
                await ApproveLowValueDirectPurchaseTenderOffersChecking(agencyCode, tender);

            _genericCommandRepository.Update(tender);
            await _genericCommandRepository.SaveAsync();
        }
        private async Task ApproveDirectPurchaseTenderOffersChecking(string agencyCode, Tender tender)
        {
            IsValidToApproveOrRejectTenderTechnicalChecking(tender, agencyCode);
            await RejectTenderCancelRequestWhileTenderApproval(tender, RoleNames.OffersPurchaseSecretary);

            tender.UpdateTenderStatus(Enums.TenderStatus.OffersTechnicalCheckingConfirmed, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ApproveDirectPurchaseTenderOffersChecking);
            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel mainNotificationTemplate = new MainNotificationTemplateModel(
                NotificationArgument,
                $"Tender/CheckDirectPurchaseOffers?tenderIdString={Util.Encrypt(tender.TenderId)}",
                NotificationEntityType.Tender,
                tender.TenderId.ToString(),
                null,
                tender.DirectPurchaseCommitteeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(
                NotificationOperations.DirectPurchaseSecretary.OperationsOnTheTender.TenderOffersCheckingApproved,
                tender.DirectPurchaseCommitteeId,
                mainNotificationTemplate);
            List<Offer> offers = await _tenderDomainService.GetNotIdenticalOffersByTenderId(tender.TenderId);
            List<string> suppliers = offers.Select(o => o.CommericalRegisterNo).ToList();
            MainNotificationTemplateModel approveTenderCheckingForSupplier = new MainNotificationTemplateModel(NotificationArgument, $"Tender/DetailsForSupplier?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderName, tender.BranchId);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.SupplierTechnicalOfferRejected, suppliers, approveTenderCheckingForSupplier);

        }
        private async Task ApproveLowValueDirectPurchaseTenderOffersChecking(string agencyCode, Tender tender)
        {
            IsValidToApproveLowBudgetDirectPurchaseCheckingTender(tender, agencyCode);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersCheckedConfirmed, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ApproveDirectPurchaseTenderOffersChecking);
            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel mainNotificationTemplate = new MainNotificationTemplateModel(
                NotificationArgument,
                $"Tender/CheckDirectPurchaseOffers?tenderIdString={Util.Encrypt(tender.TenderId)}",
                NotificationEntityType.Tender,
                tender.TenderId.ToString(),
                null,
                tender.DirectPurchaseCommitteeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(
                NotificationOperations.DirectPurchaseSecretary.OperationsOnTheTender.TenderOffersCheckingApproved,
                tender.DirectPurchaseCommitteeId,
                mainNotificationTemplate);
            List<Offer> offers = await _tenderDomainService.GetNotIdenticalOffersByTenderId(tender.TenderId);
            List<string> suppliers = offers.Select(o => o.CommericalRegisterNo).ToList();
            MainNotificationTemplateModel approveTenderCheckingForSupplier = new MainNotificationTemplateModel(NotificationArgument, $"Tender/DetailsForSupplier?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderName, tender.BranchId);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.SupplierTechnicalOfferRejected, suppliers, approveTenderCheckingForSupplier);
        }

        private void IsValidToApproveLowBudgetDirectPurchaseCheckingTender(Tender tender, string agencyCode)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.AgencyCode != agencyCode)
                throw new UnHandledAccessException();

            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersChecking)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersTechnicalCheckingPending));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }

            if (tender.DirectPurchaseMemberId != _httpContextAccessor.HttpContext.User.UserId())
                throw new UnHandledAccessException();

        }

        public async Task RejectDirectPurchaseTenderOffersChecking(int tenderId, string rejectionReason, string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderForOpenCheckStageByTenderId(tenderId);
            IsValidToApproveOrRejectTenderTechnicalChecking(tender, agencyCode);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersTechnicalCheckingRejected, rejectionReason, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.RejectDirectPurchaseTenderOffersChecking);
            await _tenderCommands.UpdateAsync(tender);
            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel mainNotificationTemplate = new MainNotificationTemplateModel(
                NotificationArgument,
                $"Tender/CheckDirectPurchaseOffers?tenderIdString={Util.Encrypt(tender.TenderId)}",
                NotificationEntityType.Tender,
                tender.TenderId.ToString(),
                null,
                tender.DirectPurchaseCommitteeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(
                NotificationOperations.DirectPurchaseSecretary.OperationsOnTheTender.TenderOffersCheckingRejected,
                tender.DirectPurchaseCommitteeId,
                mainNotificationTemplate);
        }

        public async Task ReopenDirectPurchaseTenderOffersChecking(int tenderId, string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderForOpenCheckStageByTenderId(tenderId);
            IsValidToReopenDirectPurchaseCheckingTender(tender, agencyCode);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersChecking, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ReopenDirectPurchaseTenderOffersChecking);
            await _tenderCommands.UpdateAsync(tender);
        }
        private void IsValidToReopenDirectPurchaseCheckingTender(Tender tender, string agencyCode)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.AgencyCode != agencyCode)
                throw new UnHandledAccessException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersTechnicalCheckingRejected && tender.TenderStatusId != (int)Enums.TenderStatus.BackForCheckingFromPlaint)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersTechnicalCheckingRejected));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        public async Task ApproveDirectPurchaseTenderOffersTechnicalChecking(int tenderId, string verificationCode, string agencyCode)
        {
            await _verification.CheckForVerificationCode(tenderId, verificationCode, (int)Enums.VerificationType.Tender);
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.GetTenderByIdWithoutAnyIncluds(tenderId);
            IsValidToApproveOrRejectTenderTechnicalChecking(tender, agencyCode);
            await RejectTenderCancelRequestWhileTenderApproval(tender, RoleNames.OffersPurchaseSecretary);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersTechnicalCheckingConfirmed, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ApproveDirectPurchaseTenderOffersTechnicalChecking);
            await SendNotificationAfterApproveDirectPurchaseTenderOffersTechnicalChecking(tender);
            _genericCommandRepository.Update(tender);
            await _genericCommandRepository.SaveAsync();
        }
        private async Task SendNotificationAfterApproveDirectPurchaseTenderOffersTechnicalChecking(Tender tender)
        {
            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel mainNotificationTemplate = new MainNotificationTemplateModel(
                NotificationArgument,
                $"Tender/CheckDirectPurchaseOffers?tenderIdString={Util.Encrypt(tender.TenderId)}",
                NotificationEntityType.Tender,
                tender.TenderId.ToString(),
                null,
                tender.DirectPurchaseCommitteeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(
                NotificationOperations.DirectPurchaseSecretary.OperationsOnTheTender.TenderOffersTechnicalCheckingApproved,
                tender.DirectPurchaseCommitteeId,
                mainNotificationTemplate);

        }

        public async Task RejectDirectPurchaseTenderOffersTechnicalChecking(int tenderId, string rejectionReason, string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderForOpenCheckStageByTenderId(tenderId);
            IsValidToApproveOrRejectTenderTechnicalChecking(tender, agencyCode);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersTechnicalCheckingRejected, rejectionReason, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.RejectDirectPurchaseTenderOffersTechnicalChecking);
            await _tenderCommands.UpdateAsync(tender);
            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel mainNotificationTemplate = new MainNotificationTemplateModel(
                NotificationArgument,
                $"Tender/CheckDirectPurchaseOffers?tenderIdString={Util.Encrypt(tender.TenderId)}",
                NotificationEntityType.Tender,
                tender.TenderId.ToString(),
                null,
                tender.DirectPurchaseCommitteeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(
                NotificationOperations.DirectPurchaseSecretary.OperationsOnTheTender.TenderOffersTechnicalCheckingRejected,
                tender.DirectPurchaseCommitteeId,
                mainNotificationTemplate);
        }

        public async Task ReopenDirectPurchaseTenderOffersTechnicalChecking(int tenderId, string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderForOpenCheckStageByTenderId(tenderId);
            IsValidToReOpenTenderTechnicalChecking(tender, agencyCode);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersChecking, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ReopenDirectPurchaseTenderOffersTechnicalChecking);
            await _tenderCommands.UpdateAsync(tender);
        }


        public async Task SendDirectPurchaseTenderOffersFinanceCheckingToApprove(int tenderId, string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderForOpenCheckStageByTenderId(tenderId);
            IsValidToSendDirectPurchaseFinancialCheckingTenderToApprove(tender, agencyCode);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersFinancialOfferCheckingPending, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.SendDirectPurchaseTenderOffersFinanceCheckingToApprove);
            await _tenderCommands.UpdateAsync(tender);
            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel mainNotificationTemplate = new MainNotificationTemplateModel(
                NotificationArgument,
                $"Tender/CheckDirectPurchaseOffers?tenderIdString={Util.Encrypt(tender.TenderId)}",
                NotificationEntityType.Tender,
                tender.TenderId.ToString(),
                null,
                tender.DirectPurchaseCommitteeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(
                NotificationOperations.DirectPurchaseManager.OperationsOnTheTender.ApproveTenderFinanceChecking,
                tender.DirectPurchaseCommitteeId,
                mainNotificationTemplate);
        }

        private void IsValidToSendDirectPurchaseFinancialCheckingTenderToApprove(Tender tender, string agencyCode)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.AgencyCode != agencyCode)
                throw new UnHandledAccessException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersFinancialChecking)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersFinancialChecking));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        public async Task ApproveDirectPurchaseTenderOffersFinanceChecking(int tenderId, string verificationCode, string agencyCode)
        {
            await _verification.CheckForVerificationCode(tenderId, verificationCode, (int)Enums.VerificationType.Tender);
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.GetTenderByIdWithoutAnyIncluds(tenderId);
            IsValidToApproveOrRejectTenderFinancialChecking(tender, agencyCode);
            await RejectTenderCancelRequestWhileTenderApproval(tender, RoleNames.OffersPurchaseSecretary);

            tender.UpdateTenderStatus(Enums.TenderStatus.OffersFinancialOfferCheckingApproved, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ApproveVROTenderOffersFinanceCheckingAsync);
            await SendNotificationAfterApproveDirectPurchaseTenderOffersFinanceChecking(tender);

            _genericCommandRepository.Update(tender);
            await _genericCommandRepository.SaveAsync();
        }
        private async Task SendNotificationAfterApproveDirectPurchaseTenderOffersFinanceChecking(Tender tender)
        {
            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel mainNotificationTemplate = new MainNotificationTemplateModel(
                NotificationArgument,
                $"Tender/CheckDirectPurchaseOffers?tenderIdString={Util.Encrypt(tender.TenderId)}",
                NotificationEntityType.Tender,
                tender.TenderId.ToString(),
                null,
                tender.DirectPurchaseCommitteeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(
                NotificationOperations.DirectPurchaseSecretary.OperationsOnTheTender.TenderOffersFinancialCheckingApproved,
                tender.DirectPurchaseCommitteeId,
                mainNotificationTemplate);
            List<Offer> offers = await _tenderDomainService.GetNotIdenticalOffersByTenderId(tender.TenderId);
            List<string> suppliers = offers.Select(o => o.CommericalRegisterNo).ToList();
            MainNotificationTemplateModel approveTenderCheckingForSupplier = new MainNotificationTemplateModel(NotificationArgument, $"Tender/DetailsForSupplier?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderName, tender.BranchId);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.SupplierTechnicalOfferRejected, suppliers, approveTenderCheckingForSupplier);

            NotificationArguments NotificationArgumentSupplier = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.TenderName },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.TenderName },
                SMSArgs = new object[] { tender.TenderName }
            };
            List<Offer> acceptedOffers = await _tenderDomainService.GetFinancialAcceeptedOffersByTenderId(tender.TenderId);
            List<Offer> rejectedOffers = await _tenderDomainService.GetFinancialRejectedOffersByTenderId(tender.TenderId);
            List<string> acceptedSuppliers = acceptedOffers.Select(o => o.CommericalRegisterNo).ToList();
            List<string> rejectedSuppliers = rejectedOffers.Select(o => o.CommericalRegisterNo).ToList();
            MainNotificationTemplateModel acceptedRejectTenderCheckingForSupplier = new MainNotificationTemplateModel(NotificationArgumentSupplier,
                $"Tender/DetailsForSupplier?STenderId={Util.Encrypt(tender.TenderId)}",
                NotificationEntityType.Tender,
                tender.TenderName,
                tender.BranchId);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.SupplierFinacialOffersAccepted, acceptedSuppliers, acceptedRejectTenderCheckingForSupplier);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.SupplierFinacialOffersRejected, rejectedSuppliers, acceptedRejectTenderCheckingForSupplier);
        }

        public async Task RejectDirectPurchaseTenderOffersFinanceChecking(int tenderId, string rejectionReason, string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderForOpenCheckStageByTenderId(tenderId);
            IsValidToApproveOrRejectTenderFinancialChecking(tender, agencyCode);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersFinancialOfferCheckingRejected, rejectionReason, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.RejectDirectPurchaseTenderOffersFinanceChecking);
            await _tenderCommands.UpdateAsync(tender);
            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel mainNotificationTemplate = new MainNotificationTemplateModel(
                NotificationArgument,
                $"Tender/CheckDirectPurchaseOffers?tenderIdString={Util.Encrypt(tender.TenderId)}",
                NotificationEntityType.Tender,
                tender.TenderId.ToString(),
                null,
                tender.DirectPurchaseCommitteeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(
                NotificationOperations.DirectPurchaseSecretary.OperationsOnTheTender.TenderOffersFinancialCheckingRejected,
                tender.DirectPurchaseCommitteeId,
                mainNotificationTemplate);
        }

        public async Task ReopenDirectPurchaseTenderOffersFinancialChecking(int tenderId, string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderForOpenCheckStageByTenderId(tenderId);
            IsValidToReopenDirectPurchaseFinancialCheckingTender(tender, agencyCode);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersFinancialChecking, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ReopenDirectPurchaseTenderOffersFinancialChecking);
            await _tenderCommands.UpdateAsync(tender);
        }
        private void IsValidToReopenDirectPurchaseFinancialCheckingTender(Tender tender, string agencyCode)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.AgencyCode != agencyCode)
                throw new UnHandledAccessException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersFinancialOfferCheckingRejected)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersFinancialOfferCheckingRejected));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        #endregion Direct purchase checking



        #region Tender Change Requests

        #region Extend Dates Change Request

        public async Task UpdateTenderExtendDates(TenderDatesModel tenderDatesModel, string userRole, string agencyCode)
        {
            Tender tender = new Tender();
            var changeRequest = await _tenderQueries.GetExtendDateChangeRequest(tenderDatesModel.TenderId);
            if (changeRequest == null)
            {
                tender = await _tenderQueries.FindTenderByTenderId(tenderDatesModel.TenderId);
            }
            else
            {
                tender = changeRequest.Tender;
            }
            _tenderDomainService.IsValidToUpdateTender(tender, agencyCode);
            _tenderDomainService.IsValidToUpdateExtendDate(tenderDatesModel, tender);

            if (changeRequest == null)
            {
                changeRequest = new TenderChangeRequest(tenderDatesModel.TenderId, (int)Enums.ChangeRequestType.ExtendDates, (int)Enums.ChangeStatusType.New, userRole, null);
                tender.AddActionHistory(tender.TenderStatusId, TenderActions.CreateExtendDatesRequest, "", _httpContextAccessor.HttpContext.User.UserId());
            }
            else if (changeRequest.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending)
            {
                throw new BusinessRuleException("لا يمكن تنفيذ الطلب لوجود طلب اخر بإنتظار الإعتماد ");
            }
            changeRequest.AddRevisionDates(tenderDatesModel.LastEnqueriesDate, tenderDatesModel.LastOfferPresentationDate,
               tenderDatesModel.LastOfferPresentationTime, tenderDatesModel.OffersOpeningDate, tenderDatesModel.OffersOpeningTime, tenderDatesModel.OffersCheckingDate, tenderDatesModel.OffersCheckingTime, tenderDatesModel.TenderId);
            await _tenderCommands.CreateTenderChangeRequestAsync(changeRequest);
        }

        public async Task SendUpdateDatesToApprove(int tenderId)
        {
            TenderChangeRequest changeRequest = await _tenderQueries.GetExtendDateChangeRequest(tenderId);
            changeRequest.UpdateStatus((int)Enums.ChangeStatusType.Pending, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.sendExtendDatesToApprove);
            await SendUpdateDatesToApproveNotification(changeRequest);
            await _tenderCommands.UpdateTenderChangeRequestAsync(changeRequest);
        }

        private async Task SendUpdateDatesToApproveNotification(TenderChangeRequest changeRequest)
        {
            var changedDates = changeRequest.RevisionDates.FirstOrDefault();
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", changeRequest.Tender.ReferenceNumber, changeRequest.Tender.TenderName, changedDates.LastEnqueriesDate?.ToString("dd/MM/yyyy hh:mm tt"),
                    changedDates.LastOfferPresentationDate?.ToString("dd/MM/yyyy hh:mm tt"),
                    changedDates.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : changedDates.OffersOpeningDate?.ToString("dd/MM/yyyy"), changedDates.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : changedDates.OffersOpeningDate?.ToString("hh:mm tt"),
                    changedDates.OffersCheckingDate == null ? Resources.SharedResources.DisplayInputs.NotFound : changedDates.OffersCheckingDate?.ToString("dd/MM/yyyy"), changedDates.OffersCheckingDate == null ? Resources.SharedResources.DisplayInputs.NotFound : changedDates.OffersCheckingDate?.ToString("hh:mm tt")
                },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { changeRequest.Tender.ReferenceNumber },
                SMSArgs = new object[] { changeRequest.Tender.ReferenceNumber }
            };

            string link = (changeRequest.Tender.TenderTypeId == (int)Enums.TenderType.PostQualification || changeRequest.Tender.TenderTypeId == (int)Enums.TenderType.PreQualification ?
                $"Qualification/QualificationExtendDateApprovement?tenderIdString={Util.Encrypt(changeRequest.Tender.TenderId)}" :
                $"Tender/TenderExtendDateApprovement?tenderIdString={Util.Encrypt(changeRequest.TenderId)}");

            MainNotificationTemplateModel templetModel = new MainNotificationTemplateModel(NotificationArguments, link, NotificationEntityType.Tender, changeRequest.Tender.TenderId.ToString(), changeRequest.Tender.BranchId);
            if (changeRequest.Tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.EtimadOfficer.OperationsNeedApprove.tenderwaitingdateextend, changeRequest.Tender.BranchId, templetModel);
            }
            else
            {
                if (changeRequest.Tender.TenderTypeId == (int)Enums.TenderType.PreQualification || changeRequest.Tender.TenderTypeId == (int)Enums.TenderType.PostQualification)
                {
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.PreQualificationManager.EditOperationsOnQualification.QualificationWaitingExtendDate, changeRequest.Tender.PreQualificationCommitteeId, templetModel);
                }
                else
                {
                    await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.Auditor.OperationsNeedApprove.tenderwaitingdateextend, changeRequest.Tender.BranchId, templetModel);
                }
            }
        }

        public async Task ApproveTenderExtendDatesChangeRequest(int tenderId, string verificationCode, int typeId)
        {
            await _verification.CheckForVerificationCode(tenderId, verificationCode, typeId);
            Tender tender = await _tenderQueries.GetTenderWithExtendDatesChangeRequest(tenderId);
            var extendDatesChangeRequests = tender.ChangeRequests.Where(c => c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.ExtendDates && c.IsActive == true).ToList();
            if (extendDatesChangeRequests.Any())
            {
                var requestStatuses = extendDatesChangeRequests.Select(x => x.ChangeRequestStatusId).ToList();
                if (requestStatuses.IndexOf((int)Enums.ChangeStatusType.Pending) != -1)
                {
                    tender = await AcceptTenderDateUpdateAsync(tender.TenderId);
                }
            }
            await _tenderCommands.UpdateAsync(tender);
        }

        private async Task<Tender> AcceptTenderDateUpdateAsync(int tenderId)
        {
            var tender = await _tenderQueries.FindTenderForUpdateDates(tenderId);
            _tenderDomainService.IsValidToUpdateModel(tender);
            TenderDatesChange tenderDatesChange = await _tenderQueries.FindActiveTenderRevisionDateForExtendDateApproval(tenderId);
            var supplierOfferExtendDates = tender.AgencyCommunicationRequests.Where(x => x.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.SupplierOfferExtendDates && x.StatusId != (int)Enums.AgencyCommunicationRequestStatus.Rejected && x.IsActive == true).ToList();
            bool billSent = true;
            if (tenderDatesChange.LastOfferPresentationDate > tender.LastOfferPresentationDate)
            {
                billSent = await UpdateBillsThroughTahseel(tender.TenderId, tenderDatesChange.LastOfferPresentationDate.Value);
            }
            if (!billSent)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CannotCancelTenderBill);
            }
            if (supplierOfferExtendDates.Any())
            {
                foreach (var item in supplierOfferExtendDates)
                {
                    item.UpdateSupplierOfferExtendDatesRequestStatus((int)Enums.AgencyCommunicationRequestStatus.TenderExtendedDates);
                }
            }
            tenderDatesChange.AcceptRevision();
            var changeRequest = tender.ChangeRequests.FirstOrDefault(x => x.IsActive == true && x.ChangeRequestTypeId == ((int)Enums.ChangeRequestType.ExtendDates));
            changeRequest.UpdateStatus((int)Enums.ChangeStatusType.Approved, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ApproveTenderExtendDates);
            changeRequest.DeActive();
            tender = tender.UpdateDates(tenderDatesChange, _httpContextAccessor.HttpContext.User.UserId());

            string link = (changeRequest.Tender.TenderTypeId == (int)Enums.TenderType.PostQualification || changeRequest.Tender.TenderTypeId == (int)Enums.TenderType.PreQualification ?
                            $"Qualification/QualificationExtendDateApprovement?tenderIdString={Util.Encrypt(changeRequest.Tender.TenderId)}" :
                            $"Tender/TenderExtendDateApprovement?tenderIdString={Util.Encrypt(changeRequest.TenderId)}");
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(new NotificationArguments
            {
                SubjectEmailArgs = new object[] { },
                BodyEmailArgs = new object[] { "", tender.Agency?.NameArabic, tender.Branch?.BranchName, tender.TenderName, tender.ReferenceNumber, tender.Purpose, tender.LastEnqueriesDate?.ToString("dd/MM/yyyy hh:mm tt"), tender.LastOfferPresentationDate?.ToString("dd/MM/yyyy hh:mm tt"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                SMSArgs = new object[] { tender.Agency?.NameArabic, tender.Branch?.BranchName, tender.ReferenceNumber },
                PanelArgs = new object[] { tender.ReferenceNumber }
            }, link, NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId);
            MainNotificationTemplateModel approveTenderForSupplier = new MainNotificationTemplateModel(new NotificationArguments
            {
                SubjectEmailArgs = new object[] { },
                BodyEmailArgs = new object[] { "", tender.Agency?.NameArabic, tender.Branch?.BranchName, tender.TenderName, tender.ReferenceNumber, "", tender.LastEnqueriesDate?.ToString("dd/MM/yyyy hh:mm tt"), tender.LastOfferPresentationDate?.ToString("dd/MM/yyyy hh:mm tt"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                SMSArgs = new object[] { tender.Agency?.NameArabic, tender.Branch?.BranchName, tender.ReferenceNumber },
                PanelArgs = new object[] { tender.ReferenceNumber }
            },
            $"Tender/DetailsForSupplier?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId);
            List<string> suppliers = await (tender.TenderTypeId == (int)Enums.TenderType.PreQualification || tender.TenderTypeId == (int)Enums.TenderType.PostQualification ? _idmAppService.GetAllSupplierOnQualification(tenderId)
                                            : _idmAppService.GetAllSupplierOnTender(tenderId));
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.approvetenderextenddate, suppliers, approveTenderForSupplier);


            if (tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.PurshaseSpecialist.OperationsOnTheTender.approvetenderextenddate, tender.BranchId, approveTender);
            }
            else if (tender.TenderTypeId != (int)Enums.TenderType.PreQualification && tender.TenderTypeId != (int)Enums.TenderType.PostQualification)
            {
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.OperationsOnTheTender.approvetenderextenddate, tender.BranchId, approveTender);
            }
            return await _tenderCommands.UpdateAsync(tender);
        }

        public async Task RejectTenderExtendDate(int tenderId, string rejectionReason)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            var changeRequest = await _tenderQueries.GetExtendDateChangeRequestForRejection(tenderId);
            changeRequest.UpdateStatusToRejection(changeRequest.Tender.TenderStatusId, (int)Enums.ChangeStatusType.Rejected, rejectionReason, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.RejectExtendTenderDates);
            await RejectTenderExtendDateNotification(changeRequest.Tender, rejectionReason);
            await _tenderCommands.UpdateTenderChangeRequestAsync(changeRequest);
        }

        private async Task RejectTenderExtendDateNotification(Tender tender, string rejectionReason)
        {
            string link = (tender.TenderTypeId == (int)Enums.TenderType.PostQualification || tender.TenderTypeId == (int)Enums.TenderType.PreQualification ?
                                                        $"Qualification/QualificationExtendDateApprovement?tenderIdString={Util.Encrypt(tender.TenderId)}" :
                                                        $"Tender/TenderExtendDateApprovement?tenderIdString={Util.Encrypt(tender.TenderId)}");
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(new NotificationArguments
            {
                SubjectEmailArgs = new object[] { },
                BodyEmailArgs = new object[] { "", tender.ReferenceNumber, rejectionReason, tender.Purpose
                ,tender.LastEnqueriesDate, tender.LastOfferPresentationDate,
                    tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt"),
                    tender.OffersCheckingDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersCheckingDate?.ToString("dd/MM/yyyy"), tender.OffersCheckingDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersCheckingDate?.ToString("hh:mm tt")
                },
                SMSArgs = new object[] { tender.ReferenceNumber },
                PanelArgs = new object[] { tender.ReferenceNumber }
            }, link, NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId);
            if (tender.TenderTypeId == (int)Enums.TenderType.PostQualification || tender.TenderTypeId == (int)Enums.TenderType.PreQualification)
            {
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.PreQualificationSecretary.EditOperationsOnQualification.RejectQualificationExtendDate, tender.PreQualificationCommitteeId, approveTender);
            }
            else
            {
                if (tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
                {
                    await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.PurshaseSpecialist.OperationsOnTheTender.rejecttenderextenddate, tender.BranchId, approveTender);
                }
                else
                {
                    await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.OperationsOnTheTender.rejecttenderextenddate, tender.BranchId, approveTender);
                }
            }
        }

        public async Task CancelTenderExtendDateAsync(int tenderId)
        {
            var changeRequest = await _tenderQueries.GetExtendDateChangeRequestForCancel(tenderId);
            var supplierOfferExtendDate = changeRequest.Tender.AgencyCommunicationRequests.Where(x => x.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.SupplierOfferExtendDates && x.IsActive == true && x.StatusId == (int)Enums.AgencyCommunicationRequestStatus.UnderRevision).FirstOrDefault();
            if (supplierOfferExtendDate != null)
            {
                supplierOfferExtendDate.UpdateSupplierOfferExtendDatesRequestStatus((int)Enums.AgencyCommunicationRequestStatus.Rejected, changeRequest.RejectionReason);
            }
            changeRequest.DeActive();
            changeRequest.Tender.AddActionHistory((int)Enums.TenderStatus.Approved, TenderActions.CancelExtend, "", _httpContextAccessor.HttpContext.User.UserId());
            var RevisionDates = changeRequest.RevisionDates.ToList();
            foreach (TenderDatesChange item in RevisionDates)
            {
                item.DeActive();
            }
            await _tenderCommands.UpdateTenderChangeRequestAsync(changeRequest);
        }

        #endregion

        #region Quantities Tables Change Request

        public async Task SendUpdateQuantityTableToApprove(int tenderId)
        {
            TenderChangeRequest changeRequest = await _tenderQueries.GetChangeRequestWithFormQuantityTables(tenderId);
            Tender tender = await _tenderQueries.GetTenderWithQuantityTablesById(tenderId);
            if (changeRequest.Tender.LastOfferPresentationDate < DateTime.Now.Date)
                throw new BusinessRuleException(" لا يمكن إرسال التعديل للاعتماد لإنتهاء تاريخ تقديم العروض");
            if (!(changeRequest.TenderQuantityTableChanges.Any(a => a.IsActive == true && a.TableChangeStatusId == (int)QuantityTableChangeStatus.Add)
                || tender.TenderQuantityTables.Any(a => a.IsActive == true
                                                    && a.QuantitiyItemsJson != null && a.QuantitiyItemsJson.TenderQuantityTableItems.Count > 0
                                                    && !changeRequest.TenderQuantityTableChanges.Any(c => c.IsActive == true
                                                                                                        && c.TableChangeStatusId == (int)QuantityTableChangeStatus.Remove
                                                                                                        && c.TenderQuantitiesTableId == a.Id))))
                throw new BusinessRuleException("لابد من ادخال عنصر واحد على الاقل فى اى جدول");
            changeRequest.UpdateStatus((int)ChangeStatusType.Pending, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.SendForApprovalToq);
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(
                  new NotificationArguments
                  {
                      SubjectEmailArgs = new object[] { },
                      BodyEmailArgs = new object[] { "", changeRequest.Tender.ReferenceNumber, changeRequest.Tender.Purpose, changeRequest.Tender.LastEnqueriesDate, changeRequest.Tender.LastOfferPresentationDate, changeRequest.Tender.OffersOpeningDate, changeRequest.Tender.OffersOpeningDate?.Hour },
                      SMSArgs = new object[] { changeRequest.Tender.ReferenceNumber },
                      PanelArgs = new object[] { changeRequest.Tender.ReferenceNumber },
                  },
                $"Tender/TenderQuantityTableChangesApprovement?tenderIdString={Util.Encrypt(changeRequest.Tender.TenderId)}",
                NotificationEntityType.Tender,
                changeRequest.Tender.TenderId.ToString(),
                changeRequest.Tender.BranchId);
            if (changeRequest.Tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.EtimadOfficer.OperationsOnTheTender.sendforapprovaltoq, changeRequest.Tender.BranchId, approveTender);
            else
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.Auditor.OperationsOnTheTender.sendforapprovaltoq, changeRequest.Tender.BranchId, approveTender);
            await _tenderCommands.UpdateTenderChangeRequestAsync(changeRequest);
        }

        public async Task ApproveTenderQuantityTablesChangeRequest(int tenderId, string verificationCode, int typeId)
        {
            bool check = await _verification.CheckForVerificationCode(tenderId, verificationCode, typeId);
            Tender tender = await _tenderQueries.FindTenderForQuantitiesTableChangeRequests(tenderId);
            if (tender.LastOfferPresentationDate < DateTime.Now.Date)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CannotApproveFinishedTender);
            }
            if (tender.ChangeRequests.Any(a => a.IsActive == true))
            {
                var ChangeRequest = tender.ChangeRequests.FirstOrDefault(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables && x.ChangeRequestStatusId == (int)ChangeStatusType.Pending);
                if (ChangeRequest != null)
                    tender = QuantityTableChangesInApprovement(tender);
            }
            tender.Offers.ForEach(x => x.DeActive());
            await SendSupplierNotificationAfterApproveChangeRequest(tender);
            await _tenderCommands.UpdateAsync(tender);
        }

        private async Task SendSupplierNotificationAfterApproveChangeRequest(Tender tender)
        {
            var suppliersHaveOffers = tender.Offers.Where(x => x.IsActive == true && x.OfferStatusId == (int)OfferStatus.Received).Select(e => e.CommericalRegisterNo).ToList();
            var suppliersWithoutOffers = tender.Offers.Where(x => x.IsActive == true && (x.OfferStatusId == (int)OfferStatus.Established || x.OfferStatusId == (int)OfferStatus.UnderEstablishing)).Select(e => e.CommericalRegisterNo).ToList();
            var invitedSuppliers = tender.ConditionsBooklets.Where(i => !suppliersHaveOffers.Contains(i.CommericalRegisterNo) && i.BillInfo.BillStatusId == (int)Enums.BillStatus.Paid).Select(e => e.CommericalRegisterNo).ToList();
            suppliersWithoutOffers.AddRange(invitedSuppliers);
            suppliersWithoutOffers.AddRange(tender.Invitations.Where(i => !suppliersHaveOffers.Contains(i.CommericalRegisterNo) && i.StatusId == (int)InvitationStatus.Approved).Select(e => e.CommericalRegisterNo).ToList());
            var approvedOffers = tender.Offers.Where(x => x.IsActive == true && x.OfferStatusId == (int)OfferStatus.Received).ToList();
            MainNotificationTemplateModel approvetoq = new MainNotificationTemplateModel(
                               new NotificationArguments
                               {
                                   SubjectEmailArgs = new object[] { },
                                   BodyEmailArgs = new object[] { "", tender.TenderName, tender.ReferenceNumber, tender.Purpose, tender.LastEnqueriesDate, tender.LastOfferPresentationDate, tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                                   SMSArgs = new object[] { tender.ReferenceNumber },
                                   PanelArgs = new object[] { tender.ReferenceNumber },
                               },
                                $"Tender/TenderQuantityTableChangesApprovement?tenderIdString={Util.Encrypt(tender.TenderId)}",
                                NotificationEntityType.Tender,
                                tender.TenderId.ToString(),
                                tender.BranchId);
            MainNotificationTemplateModel approveToqForSupplier = new MainNotificationTemplateModel(
                               new NotificationArguments
                               {
                                   SubjectEmailArgs = new object[] { },
                                   BodyEmailArgs = new object[] { "", tender.TenderName, tender.ReferenceNumber, tender.Purpose, tender.LastEnqueriesDate, tender.LastOfferPresentationDate, tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                                   SMSArgs = new object[] { tender.ReferenceNumber },
                                   PanelArgs = new object[] { tender.ReferenceNumber },
                               },
                                $"Tender/DetailsForSupplier?STenderId={Util.Encrypt(tender.TenderId)}",
                                NotificationEntityType.Tender,
                                tender.TenderId.ToString(),
                                tender.BranchId);
            if (tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.PurshaseSpecialist.OperationsOnTheTender.approvetoq, tender.BranchId, approvetoq);
            else
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.OperationsOnTheTender.approvetoq, tender.BranchId, approvetoq);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.approvetoq, suppliersHaveOffers, approveToqForSupplier);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.approvetoqwithoutoffer, suppliersWithoutOffers, approveToqForSupplier);
        }

        private Tender QuantityTableChangesInApprovement(Tender tender)
        {
            TenderChangeRequest changeRequest = tender.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == ((int)Enums.ChangeRequestType.QuantitiesTables)).FirstOrDefault();
            var quantitytablesChanges = changeRequest.TenderQuantityTableChanges.Where(x => x.IsActive == true).ToList();
            var addedTables = _mapper.Map<List<TenderQuantityTable>>(quantitytablesChanges.Where(x => x.TableChangeStatusId != (int)QuantityTableChangeStatus.Remove));
            addedTables.ForEach(c => c.SetAddedState());
            var oldTable = tender.TenderQuantityTables.Where(x => x.IsActive == true && quantitytablesChanges.Select(s => s.TenderQuantitiesTableId).Contains(x.Id)).ToList();
            foreach (var item in oldTable)
            {
                item.DeleteTenderQuantityTableWithItems();
            }
            tender.TenderQuantityTables.AddRange(addedTables);
            if (changeRequest.HasAlternativeOffer.HasValue)
                tender.UpdateHasAlternative(changeRequest.HasAlternativeOffer ?? false);
            changeRequest.TenderQuantityTableChanges.ForEach(x => x.DeActive());
            changeRequest.UpdateStatus((int)ChangeStatusType.Approved, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ApproveTOQ);
            changeRequest.DeActive();
            return tender;
        }

        public async Task RejectTenderForUpdateQuantityTable(int tenderId, string rejectionReason)
        {
            Tender tender = await _tenderQueries.FindTenderQuantityTablesById(tenderId);
            var changeRequest = tender.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables).FirstOrDefault();
            changeRequest.UpdateStatusToRejection(tender.TenderStatusId, (int)ChangeStatusType.Rejected, rejectionReason, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.RejecToq);
            tender = await _tenderCommands.UpdateAsync(tender);
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(
                 new NotificationArguments
                 {
                     SubjectEmailArgs = new object[] { },
                     BodyEmailArgs = new object[] { "", tender.ReferenceNumber,
                        changeRequest?.RejectionReason,
                        tender.Purpose, tender.LastEnqueriesDate?.ToString("dd/MM/yyyy hh:mm tt"),
                        tender.LastOfferPresentationDate?.ToString("dd/MM/yyyy hh:mm tt"),
                       tender.OffersOpeningDate == null  ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt")  },
                     SMSArgs = new object[] { tender.ReferenceNumber },
                     PanelArgs = new object[] { tender.ReferenceNumber },
                 },
                    $"Tender/TenderQuantityTableChangesApprovement?tenderIdString={Util.Encrypt(tender.TenderId)}",
                    NotificationEntityType.Tender,
                    tender.TenderId.ToString(),
                    tender.BranchId);
            if (tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.PurshaseSpecialist.OperationsOnTheTender.rejecttoq, tender.BranchId, approveTender);
            else
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.OperationsOnTheTender.rejecttoq, tender.BranchId, approveTender);
        }

        public async Task CancelUpdatesInQuantitiesTablesAsync(int tenderId)
        {
            Tender tender = await _tenderQueries.FindTenderQuantityTablesById(tenderId);
            var changeRequest = tender.ChangeRequests.FirstOrDefault(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables);
            var quantitiestablesChanges = changeRequest.TenderQuantityTableChanges.Where(x => x.IsActive == true).ToList();
            tender.AddActionHistory(tender.TenderStatusId, TenderActions.CancelTOQ, "", _httpContextAccessor.HttpContext.User.UserId());

            foreach (var item in quantitiestablesChanges)
            {
                item.QuantitiyItemsChangeJson.Delete();
                _genericCommandRepository.Update(item);
            }
            await _genericCommandRepository.SaveAsync();

            foreach (var item in quantitiestablesChanges)
            {
                item.Delete();
                _genericCommandRepository.Update(item);
            }
            await _genericCommandRepository.SaveAsync();

            changeRequest.Delete();
            _genericCommandRepository.Update(tender);
            await _genericCommandRepository.SaveAsync();
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(new NotificationArguments
            {
                SubjectEmailArgs = new object[] { },
                BodyEmailArgs = new object[] { "", tender.ReferenceNumber, tender.Purpose, tender.LastEnqueriesDate, tender.LastOfferPresentationDate, tender.OffersOpeningDate, tender.OffersOpeningDate.HasValue ? tender.OffersOpeningDate.Value.Hour : 0 },
                SMSArgs = new object[] { tender.ReferenceNumber },
                PanelArgs = new object[] { tender.ReferenceNumber },
            }, $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId);
            if (tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)

                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.EtimadOfficer.OperationsOnTheTender.sendforcancelationtoq, changeRequest.Tender.BranchId, approveTender);
            else
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.Auditor.OperationsOnTheTender.sendforcancelationtoq, changeRequest.Tender.BranchId, approveTender);
        }

        #endregion

        #region Attachments Change Request

        public async Task<Tender> UpdateTenderAttachmentsAfterApprovment(int tenderId, int tenderStatusId, List<TenderAttachmentModel> attachments, string userRole, int branchId)
        {
            var tenderAttachmentsChanges = _mapper.Map<List<TenderAttachmentChanges>>(attachments);
            Tender tender = await _tenderQueries.FindTenderWithAttachments(tenderId);
            var changeRequest = await _tenderQueries.GetAttachmentsChangeRequest(tender.TenderId); // try to remove tender from include

            var oldItems = tender.Attachments.Where(p => attachments.Select(a => a.FileNetReferenceId).ToList().Contains(p.FileNetReferenceId)).ToList();
            var newItems = tenderAttachmentsChanges.Where(p => !tender.Attachments.Select(a => a.FileNetReferenceId).ToList().Contains(p.FileNetReferenceId)).ToList();
            var mutualItems = tender.Attachments.Where(p => !attachments.Select(a => a.FileNetReferenceId).ToList().Contains(p.FileNetReferenceId)).ToList();
            if (newItems.Count == 0 && mutualItems.Count == 0 && oldItems.Count == attachments.Count && changeRequest == null)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.ThereIsNoAttachmentsToUpdate);
            }
            if (changeRequest == null)
            {
                changeRequest = new TenderChangeRequest(tender.TenderId, (int)Enums.ChangeRequestType.Attachments, (int)Enums.ChangeStatusType.New, userRole, null);
                tender.ChangeRequests.Add(changeRequest);
                tender.AddActionHistory(tender.TenderStatusId, TenderActions.CreateAttachmentsRequest, "", _httpContextAccessor.HttpContext.User.UserId());
            }
            var deletedAttachments = _mapper.Map<List<TenderAttachmentChanges>>(mutualItems);
            changeRequest = changeRequest.UpdateAttachmnetsChanges(deletedAttachments, newItems);
            await _tenderCommands.UpdateTenderChangeRequestAsync(changeRequest);
            return changeRequest.Tender;
        }

        public async Task SendUpdateAttachmentsToApprove(int tenderId)
        {
            TenderChangeRequest changeRequest = await _tenderQueries.GetAttachmentsChangeRequest(tenderId); // try to remove tender from include

            changeRequest.UpdateStatus((int)Enums.ChangeStatusType.Pending, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.SendTenderFilesToApprove);
            await SendUpdateAttachmentsToApproveNotification(changeRequest);
            await _tenderCommands.UpdateTenderChangeRequestAsync(changeRequest);
        }

        private async Task SendUpdateAttachmentsToApproveNotification(TenderChangeRequest changeRequest)
        {
            string link = (changeRequest.Tender.TenderTypeId == (int)Enums.TenderType.PostQualification || changeRequest.Tender.TenderTypeId == (int)Enums.TenderType.PreQualification ?
                $"Qualification/QualificationAttachmentsChangesApprovement?tenderIdString={Util.Encrypt(changeRequest.Tender.TenderId)}" :
                $"Tender/TenderAttachmentsChangesApprovement?tenderIdString={Util.Encrypt(changeRequest.Tender.TenderId)}");
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(
                    new NotificationArguments
                    {
                        BodyEmailArgs = new object[] { "", changeRequest.Tender.TenderName, changeRequest.Tender.ReferenceNumber },
                        SubjectEmailArgs = new object[] { },
                        SMSArgs = new object[] { "", changeRequest.Tender.ReferenceNumber },
                        PanelArgs = new object[] { changeRequest.Tender.ReferenceNumber },
                    }, link, NotificationEntityType.Tender, changeRequest.Tender.TenderId.ToString(), changeRequest.Tender.BranchId);
            if (changeRequest.Tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.EtimadOfficer.OperationsOnTheTender.approvetenderattachment, changeRequest.Tender.BranchId, approveTender);
            }
            else
            {
                if (changeRequest.Tender.TenderTypeId == (int)Enums.TenderType.PostQualification || changeRequest.Tender.TenderTypeId == (int)Enums.TenderType.PreQualification)
                {
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.PreQualificationManager.EditOperationsOnQualification.approveQualificationattachment, changeRequest.Tender.PreQualificationCommitteeId, approveTender);
                }
                else
                {
                    await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.Auditor.OperationsOnTheTender.approvetenderattachment, changeRequest.Tender.BranchId, approveTender);
                }
            }
        }

        public async Task ApproveTenderAttachmentsChangeRequest(int tenderId, string verificationCode, int typeId)
        {
            bool check = await _verification.CheckForVerificationCode(tenderId, verificationCode, typeId);
            Tender tender = await _tenderQueries.GetTenderWithAttachmentsChangeRequests(tenderId);
            if (tender.ChangeRequests.Any(c => c.IsActive == true && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Attachments))
            {
                var pendingChangeRequest = tender.ChangeRequests.FirstOrDefault(x => x.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending);
                if (pendingChangeRequest != null)
                    tender = await AttachmentsChangesInApprovement(tender);
            }
            if (tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.CurrentTender || tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                tender.Offers.ForEach(x =>
                {
                    foreach (var item in x.SupplierTenderQuantityTables)
                    {
                        item.DeleteTableAndItems();
                    }
                    x.UpdateStatus(OfferStatus.ChangeByQT);
                    x.DeActive();
                });
            }
            await _tenderCommands.UpdateAsync(tender);
        }

        private async Task<Tender> AttachmentsChangesInApprovement(Tender tender)
        {
            var changeRequest = tender.ChangeRequests.FirstOrDefault(x => x.IsActive == true && x.ChangeRequestTypeId == ((int)Enums.ChangeRequestType.Attachments));
            var attachmentsChanges = changeRequest.AttachmentChanges.Where(x => x.IsActive == true && x.DeletedAttachmentId == null).ToList();
            var addedAttachments = _mapper.Map<List<TenderAttachment>>(attachmentsChanges);
            addedAttachments.ForEach(c => c.SetAddedState());
            tender.Attachments.AddRange(addedAttachments);

            var attachmentsToBeDeleted = changeRequest.AttachmentChanges.Where(x => x.IsActive == true && x.DeletedAttachmentId != null).Select(c => c.DeletedAttachmentId).ToList();
            var deletedAttachments = tender.Attachments.Where(x => attachmentsToBeDeleted.Contains(x.TenderAttachmentId)).ToList();
            deletedAttachments.ForEach(x => x.DeleteAttachment());
            changeRequest.AttachmentChanges.ForEach(x => x.DeActive());

            changeRequest.UpdateStatus((int)ChangeStatusType.Approved, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ApproveTenderFiles);
            changeRequest.DeActive();
            List<string> suppliers = await (tender.TenderTypeId == (int)Enums.TenderType.PreQualification || tender.TenderTypeId == (int)Enums.TenderType.PostQualification ? _idmAppService.GetAllSupplierOnQualification(tender.TenderId)
           : _idmAppService.GetAllSupplierOnTender(tender.TenderId));

            string link = (changeRequest.Tender.TenderTypeId == (int)Enums.TenderType.PostQualification || changeRequest.Tender.TenderTypeId == (int)Enums.TenderType.PreQualification ?
                            $"Qualification/QualificationAttachmentsChangesApprovement?tenderIdString={Util.Encrypt(changeRequest.Tender.TenderId)}" :
                            $"Tender/TenderAttachmentsChangesApprovement?tenderIdString={Util.Encrypt(changeRequest.Tender.TenderId)}");
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(
                    new NotificationArguments
                    {
                        SubjectEmailArgs = new object[] { },
                        BodyEmailArgs = new object[] { "", changeRequest.Tender.TenderName, changeRequest.Tender.ReferenceNumber },
                        SMSArgs = new object[] { "", changeRequest.Tender.ReferenceNumber },
                        PanelArgs = new object[] { changeRequest.Tender.ReferenceNumber },
                    }, link, NotificationEntityType.Tender, changeRequest.Tender.TenderId.ToString(), changeRequest.Tender.BranchId);
            MainNotificationTemplateModel approveTenderForSupplier = new MainNotificationTemplateModel(
                    new NotificationArguments
                    {
                        SubjectEmailArgs = new object[] { },
                        BodyEmailArgs = new object[] { "", changeRequest.Tender.ReferenceNumber, changeRequest.Tender.ReferenceNumber },
                        SMSArgs = new object[] { "", changeRequest.Tender.ReferenceNumber },
                        PanelArgs = new object[] { changeRequest.Tender.ReferenceNumber },
                    },
                    (changeRequest.Tender.TenderTypeId == (int)Enums.TenderType.PreQualification || changeRequest.Tender.TenderTypeId == (int)Enums.TenderType.PostQualification) ? $"Qualification/PreQualificationDetails?QualificationId={Util.Encrypt(changeRequest.Tender.TenderId)}" : $"Tender/DetailsForSupplier?STenderId={Util.Encrypt(changeRequest.Tender.TenderId)}",
                    NotificationEntityType.Tender,
                    changeRequest.Tender.TenderId.ToString(),
                    changeRequest.Tender.BranchId);
            if (tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.PurshaseSpecialist.OperationsOnTheTender.approvetenderattachment, changeRequest.Tender.BranchId, approveTender);
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.EtimadOfficer.OperationsOnTheTender.publishtenderfile, changeRequest.Tender.BranchId, approveTender);
                await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.addtenderattachment, suppliers, approveTenderForSupplier);

            }
            else if (changeRequest.Tender.TenderTypeId == (int)Enums.TenderType.PreQualification || changeRequest.Tender.TenderTypeId == (int)Enums.TenderType.PostQualification)
            {
                await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnPrequalification.ApproveAttachmentChanges, suppliers, approveTenderForSupplier);
            }
            else
            {
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.OperationsOnTheTender.approvetenderattachment, changeRequest.Tender.BranchId, approveTender);
                await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.addtenderattachment, suppliers, approveTenderForSupplier);
            }
            return tender;
        }

        public async Task RejectTenderUpdateAttachment(int tenderId, string rejectionReason)
        {
            Tender tender = await _tenderQueries.GetTenderWithAttachmentsChangeRequestsForRejection(tenderId);
            if (tender.ChangeRequests.Any())
            {
                tender = await RejectAttachmentsChanges(tender, rejectionReason);
            }
            else
            {
                throw new BusinessRuleException("لا يوجد طلب لكى يتم حذفه");
            }
            await _tenderCommands.UpdateAsync(tender);
        }

        private async Task<Tender> RejectAttachmentsChanges(Tender tender, string rejectionReason)
        {
            var changeRequest = tender.ChangeRequests.FirstOrDefault(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Attachments);
            changeRequest.UpdateStatusToRejection((int)Enums.TenderStatus.Approved, (int)Enums.ChangeStatusType.Rejected, rejectionReason, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.RejectTenderFiles);
            string link = (tender.TenderTypeId == (int)Enums.TenderType.PostQualification || tender.TenderTypeId == (int)Enums.TenderType.PreQualification ?
                            $"Qualification/QualificationAttachmentsChangesApprovement?tenderIdString={Util.Encrypt(tender.TenderId)}" :
                            $"Tender/TenderAttachmentsChangesApprovement?tenderIdString={Util.Encrypt(tender.TenderId)}");
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(
                new NotificationArguments
                {
                    SubjectEmailArgs = new object[] { },
                    BodyEmailArgs = new object[] { "", tender.ReferenceNumber, tender.ReferenceNumber },
                    SMSArgs = new object[] { "", tender.ReferenceNumber },
                    PanelArgs = new object[] { tender.ReferenceNumber },
                }, link, NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId);
            if (tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.PurshaseSpecialist.OperationsOnTheTender.removetenderattachment, tender.BranchId, approveTender);
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.EtimadOfficer.OperationsOnTheTender.removetenderattachment, tender.BranchId, approveTender);
            }
            else
            {
                if (tender.TenderTypeId == (int)Enums.TenderType.PostQualification || tender.TenderTypeId == (int)Enums.TenderType.PreQualification)
                {
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.PreQualificationSecretary.EditOperationsOnQualification.RejectedQualificationAttachment, tender.PreQualificationCommitteeId, approveTender);
                }
                else
                {
                    await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.OperationsOnTheTender.removetenderattachment, tender.BranchId, approveTender);
                    await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.Auditor.OperationsOnTheTender.removetenderattachment, tender.BranchId, approveTender);
                }
            }
            return tender;
        }

        public async Task CancelUpdatesInAttachmentsAsync(int tenderId)
        {
            Tender tender = await _tenderQueries.GetTenderWithAttachmentsChangeRequestsForClosing(tenderId);
            if (tender == null || !tender.ChangeRequests.Any(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Attachments))
            {
                throw new BusinessRuleException("لايمكن اغلاق الطلب الرجاء انشاء طلب تعديل ملحقات جدبد");
            }
            var changeRequest = tender.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Attachments).FirstOrDefault();
            var attachmentsChanges = changeRequest.AttachmentChanges.ToList();
            attachmentsChanges.ForEach(i => i.DeActive());
            tender.AddActionHistory(tender.TenderStatusId, TenderActions.CancelAttachements, "", _httpContextAccessor.HttpContext.User.UserId());
            changeRequest.DeActive();
            await _tenderCommands.UpdateAsync(tender);
        }

        #endregion

        #region Tender Cancelation

        public async Task<TenderChangeRequest> FindTenderRevisionCancelByTenderId(int tenderId, bool isActive)
        {
            return await _tenderQueries.FindActiveTenderRevisionCancelByTenderId(tenderId);
        }

        public async Task<TenderCanelationModel> GetTenderDataForCanelation(int tenderId)
        {
            return await _tenderQueries.GetTenderDataForCanelation(tenderId);
        }

        public async Task<TenderRevisionDateModel> FindActiveTenderRevisionDateByTenderId(int tenderId)
        {
            var tenderRevisionDate = await _tenderQueries.FindActiveTenderRevisionDateByTenderId(tenderId);
            return tenderRevisionDate;
        }

        public async Task<TenderRevisionDateModel> FindRejectedRevisionDateByTenderId(int tenderId)
        {
            var tenderRevisionDate = await _tenderQueries.FindRejectedTenderRevisionDateByTenderId(tenderId);
            return tenderRevisionDate;
        }

        public async Task<QueryResult<TenderExtendDateModel>> FindTenderRevisionDateBySearchCriteria(TenderRevisionSearchCriteria criteria)
        {
            return await _tenderQueries.FindTenderRevisionDateBySearchCriteria(criteria);
        }

        public async Task<QueryResult<TenderChangeRequest>> FindTenderRevisionCancelBySearchCriteria(TenderRevisionSearchCriteria criteria)
        {
            Check.ArgumentNotNull(nameof(criteria), criteria);
            return await _tenderQueries.FindTenderRevisionCancelBySearchCriteria(criteria);
        }

        public async Task<TenderChangeRequest> CreateCancelRequest(TenderRevisionCancelModel tenderRevisionCancelModel)
        {
            int tenderId = Util.Decrypt(tenderRevisionCancelModel.TenderIdString);
            Tender tender = await _tenderQueries.FindTenderByTenderId(tenderId);
            _tenderDomainService.CheckIfCanCancelTender(tender);
            TenderChangeRequest changeRequest = await _tenderQueries.GetNotPendingCancelChangeRequest(tender.TenderId);
            if (changeRequest != null)
            {
                throw new BusinessRuleException(Resources.TenderResources.Messages.CannotCreateCancelTender);
            }
            changeRequest = new TenderChangeRequest();
            changeRequest = changeRequest.CreateCancelationRequest(tender, (int)Enums.ChangeRequestType.Canceling, (int)ChangeStatusType.Pending, tenderRevisionCancelModel.CreatedByRoleName, tenderRevisionCancelModel.CancelationReasonId, tenderRevisionCancelModel.CancelationReasonDescription, tenderRevisionCancelModel.SupplierViolatorCRs, _httpContextAccessor.HttpContext.User.UserId());

            if (!(tender.IsLowBudgetDirectPurchase.HasValue && tender.IsLowBudgetDirectPurchase.Value) && tender.TenderStatusId != (int)Enums.TenderStatus.Approved)
            {
                await NotificationAfterSendTenderCancelToApprove(tender, tenderRevisionCancelModel.CreatedByRoleName);
            }
            await _tenderCommands.UpdateAsync(tender);
            return changeRequest;
        }

        private async Task NotificationAfterSendTenderCancelToApprove(Tender tender, string requestedByRoleName)
        {
            MainNotificationTemplateModel approveTender;
            NotificationArguments NotificationArguments = new NotificationArguments();
            NotificationArguments.BodyEmailArgs = new object[] { "", tender.ReferenceNumber, tender.Purpose, tender.LastEnqueriesDate, tender.LastOfferPresentationDate, tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") };
            NotificationArguments.SubjectEmailArgs = new object[] { };
            NotificationArguments.PanelArgs = new object[] { tender.ReferenceNumber };
            NotificationArguments.SMSArgs = new object[] { tender.ReferenceNumber };
            string _link = (tender.TenderTypeId == (int)Enums.TenderType.PostQualification || tender.TenderTypeId == (int)Enums.TenderType.PreQualification ? $"Qualification/CancelQualification?STenderId={Util.Encrypt(tender.TenderId)}" : $"Tender/CancelTender?STenderId={Util.Encrypt(tender.TenderId)}");

            switch (requestedByRoleName)
            {
                case RoleNames.OffersOpeningAndCheckSecretary:
                    approveTender = new MainNotificationTemplateModel(NotificationArguments, $"Tender/CancelTender?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), 0, tender.VROCommitteeId);
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.VROCheckManager.OperationsOnTheTender.SendTenderCancelToApproveVRO, tender.VROCommitteeId, approveTender);
                    break;
                case RoleNames.OffersOppeningSecretary:
                    approveTender = new MainNotificationTemplateModel(NotificationArguments, $"Tender/CancelTender?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), 0, tender.OffersOpeningCommitteeId);
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersOppeningManager.OperationsOnTheTender.tenderwaitingcancelation, tender.OffersOpeningCommitteeId, approveTender);
                    break;
                case RoleNames.OffersCheckSecretary:
                    approveTender = new MainNotificationTemplateModel(NotificationArguments, _link, NotificationEntityType.Tender, tender.TenderId.ToString(), 0, tender.OffersCheckingCommitteeId);
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckManager.OperationsOnTheTender.tenderwaitingcancelation, tender.OffersCheckingCommitteeId, approveTender);
                    break;
                case RoleNames.OffersPurchaseSecretary:
                    approveTender = new MainNotificationTemplateModel(NotificationArguments, _link, NotificationEntityType.Tender, tender.TenderId.ToString(), 0, tender.DirectPurchaseCommitteeId);
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseManager.OperationsOnTheTender.tenderwaitingcancelation, tender.DirectPurchaseCommitteeId, approveTender);
                    break;
                case RoleNames.PreQualificationCommitteeSecretary:
                    approveTender = new MainNotificationTemplateModel(NotificationArguments, _link, NotificationEntityType.Tender, tender.TenderId.ToString(), 0, tender.PreQualificationCommitteeId);
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.PreQualificationManager.EditOperationsOnQualification.QualificationWaitingCancelation, tender.PreQualificationCommitteeId, approveTender);
                    break;
                case RoleNames.DataEntry:
                    approveTender = new MainNotificationTemplateModel(NotificationArguments, $"Tender/CancelTender?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, 0);
                    await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.Auditor.OperationsOnTheTender.tenderwaitingcancelation, tender.BranchId, approveTender);
                    break;
                case RoleNames.PurshaseSpecialist:
                    approveTender = new MainNotificationTemplateModel(NotificationArguments, $"Tender/CancelTender?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, 0);
                    await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.EtimadOfficer.OperationsOnTheTender.tenderwaitingcancelation, tender.BranchId, approveTender);
                    break;
            }
        }

        public async Task<TenderChangeRequest> CreateCancelRequestForQualification(TenderRevisionCancelModel tenderRevisionCancelModel)
        {
            Tender tender = await _tenderQueries.FindTenderByTenderId(Util.Decrypt(tenderRevisionCancelModel.TenderIdString));
            _tenderDomainService.IsValidToUpdateModel(tender);
            _tenderDomainService.CheckIfCanCancelTender(tender);
            TenderChangeRequest changeRequest = await _tenderQueries.GetNotPendingCancelChangeRequest(tender.TenderId);
            if (changeRequest != null)
                throw new BusinessRuleException(Resources.TenderResources.Messages.CannotCreateCancelTender);
            changeRequest = new TenderChangeRequest();
            changeRequest = changeRequest.CreateCancelationRequest(tender, (int)Enums.ChangeRequestType.Canceling, (int)ChangeStatusType.Pending, tenderRevisionCancelModel.CreatedByRoleName, tenderRevisionCancelModel.CancelationReasonId, tenderRevisionCancelModel.CancelationReasonDescription, tenderRevisionCancelModel.SupplierViolatorCRs, _httpContextAccessor.HttpContext.User.UserId());
            if (!(tender.IsLowBudgetDirectPurchase.HasValue && tender.IsLowBudgetDirectPurchase.Value))
            {
                await SendNotificationAfterSendQualificationCancelToApprove(tender, tenderRevisionCancelModel.CreatedByRoleName);
            }
            await _tenderCommands.UpdateAsync(tender);
            return changeRequest;
        }

        private async Task SendNotificationAfterSendQualificationCancelToApprove(Tender tender, string requestedByRoleName)
        {
            MainNotificationTemplateModel approveTender;
            NotificationArguments NotificationArguments = new NotificationArguments();
            string pageLink = $"Qualification/CancelQualification?STenderId={Util.Encrypt(tender.TenderId)}";
            switch (requestedByRoleName)
            {
                case RoleNames.OffersCheckSecretary:
                    NotificationArguments.BodyEmailArgs = new object[] { "", tender.ReferenceNumber, tender.Purpose, tender.LastEnqueriesDate, tender.LastOfferPresentationDate, tender.OffersCheckingDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersCheckingDate?.ToString("dd/MM/yyyy"), tender.OffersCheckingDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersCheckingDate?.ToString("hh:mm tt") };
                    NotificationArguments.SubjectEmailArgs = new object[] { };
                    NotificationArguments.PanelArgs = new object[] { tender.ReferenceNumber };
                    NotificationArguments.SMSArgs = new object[] { tender.ReferenceNumber };
                    approveTender = new MainNotificationTemplateModel(NotificationArguments, pageLink,
                       NotificationEntityType.PostQualification, tender.TenderId.ToString(), 0, tender.OffersCheckingCommitteeId);
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckManager.OperationsOnPostqualification.QualificationWaitingCancle, tender.OffersCheckingCommitteeId, approveTender);
                    break;
                case RoleNames.OffersPurchaseSecretary:
                    NotificationArguments.BodyEmailArgs = new object[] { "", tender.ReferenceNumber, tender.Purpose, tender.LastEnqueriesDate, tender.LastOfferPresentationDate, tender.OffersCheckingDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersCheckingDate?.ToString("dd/MM/yyyy"), tender.OffersCheckingDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersCheckingDate?.ToString("hh:mm tt") };
                    NotificationArguments.SubjectEmailArgs = new object[] { };
                    NotificationArguments.PanelArgs = new object[] { tender.ReferenceNumber };
                    NotificationArguments.SMSArgs = new object[] { tender.ReferenceNumber };
                    approveTender = new MainNotificationTemplateModel(NotificationArguments, pageLink, NotificationEntityType.PostQualification, tender.TenderId.ToString(), 0, tender.DirectPurchaseCommitteeId);
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseManager.OperationsOnPostqualification.QualificationWaitingCancle, tender.DirectPurchaseCommitteeId, approveTender);
                    break;
                case RoleNames.PreQualificationCommitteeSecretary:
                    NotificationArguments.BodyEmailArgs = new object[] { "", tender.ReferenceNumber, tender.Purpose, tender.LastEnqueriesDate, tender.LastOfferPresentationDate, tender.OffersCheckingDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersCheckingDate?.ToString("dd/MM/yyyy"), tender.OffersCheckingDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersCheckingDate?.ToString("hh:mm tt") };
                    NotificationArguments.SubjectEmailArgs = new object[] { };
                    NotificationArguments.PanelArgs = new object[] { tender.ReferenceNumber };
                    NotificationArguments.SMSArgs = new object[] { tender.ReferenceNumber };
                    approveTender = new MainNotificationTemplateModel(NotificationArguments, pageLink,
                        NotificationEntityType.PreQualification, tender.TenderId.ToString(), 0, tender.PreQualificationCommitteeId);
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.PreQualificationManager.EditOperationsOnQualification.QualificationWaitingCancelation, tender.PreQualificationCommitteeId, approveTender);
                    break;
                default:
                case RoleNames.DataEntry:
                    NotificationArguments.BodyEmailArgs = new object[] { "", tender.ReferenceNumber, tender.Purpose, tender.LastEnqueriesDate, tender.LastOfferPresentationDate, tender.OffersCheckingDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersCheckingDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersCheckingDate?.ToString("hh:mm tt") };
                    NotificationArguments.SubjectEmailArgs = new object[] { };
                    NotificationArguments.PanelArgs = new object[] { tender.ReferenceNumber };
                    NotificationArguments.SMSArgs = new object[] { tender.ReferenceNumber };
                    approveTender = new MainNotificationTemplateModel(NotificationArguments, pageLink, NotificationEntityType.PreQualification, tender.TenderId.ToString(), tender.BranchId, 0);
                    await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.Auditor.OperationsOnQualification.QualificationWaitingcancelation, tender.BranchId, approveTender);
                    break;
            }
        }

        public async Task<bool> ApproveTenderCancelRequest(TenderCancelModel cancelModel)
        {
            cancelModel.TenderId = Util.Decrypt(cancelModel.TenderIdString);

            await _verification.CheckForVerificationCode(cancelModel.TenderId, cancelModel.VerificationCode, (int)Enums.VerificationType.Tender);

            var cancelChangeReguest = await _tenderQueries.GetCancelChangeRequest(cancelModel.TenderId);

            cancelChangeReguest.UpdateStatus((int)ChangeStatusType.Approved, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ApproveTenderCancelation);

            cancelChangeReguest.Tender.UpdateTenderStatus(Enums.TenderStatus.Canceled, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ApproveTenderCancelation, true);
            await SendNotificationAfterApproveTenderCancel(cancelChangeReguest);
            string cancelReason = cancelChangeReguest.Tender.TenderTypeId != (int)Enums.TenderType.NationalTransformationProjects ? "(" + cancelChangeReguest?.CancelationReason?.NameAr + ") " + cancelModel.CancelationReasonDescription : Resources.SharedResources.DisplayInputs.NotExist;
            bool isBillCancelled = await CancelBulkBillsThroughTahseel(cancelModel.TenderId, cancelReason);
            if (!isBillCancelled)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CannotCancelTenderBill);
            }
            await _tenderCommands.UpdateTenderChangeRequestAsync(cancelChangeReguest);
            return true;
        }

        private async Task SendNotificationAfterApproveTenderCancel(TenderChangeRequest changeRequest)
        {
            Tender tender = changeRequest.Tender;
            List<string> suppliersCRs = (tender.TenderTypeId == (int)Enums.TenderType.PostQualification || tender.TenderTypeId == (int)Enums.TenderType.PreQualification ? await _idmAppService.GetAllSupplierOnQualification(tender.TenderId) : await _idmAppService.GetAllSupplierOnTender(tender.TenderId));
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", tender.ReferenceNumber, tender.Purpose, tender.LastEnqueriesDate, tender.LastOfferPresentationDate, tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(NotificationArguments, $"Tender/CancelTender?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, tender.VROCommitteeId);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.canceltenderapproved, suppliersCRs, approveTender);
            if (!tender.IsLowBudgetDirectPurchase.HasValue && tender.TenderStatusId != (int)Enums.TenderStatus.Approved)
            {
                MainNotificationTemplateModel approveTenderVRO = new MainNotificationTemplateModel(NotificationArguments, $"Tender/CancelTender?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, tender.VROCommitteeId);
                switch (changeRequest.RequestedByRoleName)
                {
                    case RoleNames.OffersOpeningAndCheckSecretary:
                        await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.VROCheckSecretary.OperationsOnTheTender.CancelTenderVRO, tender.VROCommitteeId, approveTenderVRO);
                        break;
                    case RoleNames.PurshaseSpecialist:
                        await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.PurshaseSpecialist.OperationsOnTheTender.canceltender, tender.BranchId, approveTenderVRO);
                        break;
                    case RoleNames.DataEntry:
                        await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.OperationsOnTheTender.canceltender, tender.BranchId, approveTender);
                        break;
                    case RoleNames.OffersPurchaseSecretary:
                        MainNotificationTemplateModel approveTenderFordirectPurchase = new MainNotificationTemplateModel(NotificationArguments, $"Tender/CancelTender?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), 0, tender.DirectPurchaseCommitteeId);
                        await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseSecretary.OperationsOnTheTender.canceltender, tender.DirectPurchaseCommitteeId, approveTenderFordirectPurchase);
                        break;
                    case RoleNames.OffersCheckSecretary:
                        MainNotificationTemplateModel approveTenderForChecking = new MainNotificationTemplateModel(NotificationArguments, $"Tender/CancelTender?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), 0, tender.OffersCheckingCommitteeId);
                        await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.OperationsOnTheTender.canceltender, tender.OffersCheckingCommitteeId, approveTenderForChecking);
                        break;
                    case RoleNames.OffersOppeningSecretary:
                        MainNotificationTemplateModel approveTenderForOpening = new MainNotificationTemplateModel(NotificationArguments, $"Tender/CancelTender?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), 0, tender.OffersOpeningCommitteeId);
                        await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersOppeningSecretary.OperationsOnTheTender.canceltender, tender.OffersOpeningCommitteeId, approveTenderForOpening);
                        break;
                }
            }
        }

        public async Task<bool> ApproveQualificationCancelRequestAsync(int id, string verificationCode, int typeId)
        {
            Tender tender = await _tenderQueries.FindTenderToApproveCancel(id);
            Check.ArgumentNotNull(nameof(tender), tender);
            _tenderDomainService.IsValidToUpdateModel(tender);
            if (tender.TenderTypeId != (int)Enums.TenderType.PreQualification)
            {
                await _verification.CheckForVerificationCode(id, verificationCode, typeId);
            }
            var tenderRevisionCancel = await _tenderQueries.GetCancelChangeRequest(id);
            tenderRevisionCancel.UpdateStatus((int)ChangeStatusType.Approved, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ApproveTenderCancelation);
            tenderRevisionCancel.Tender.UpdateTenderStatus(Enums.TenderStatus.Canceled, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ApproveTenderCancelation, true);
            var changeRequest = await _tenderQueries.GetCancelChangeRequest(id);
            List<string> crs = await _idmAppService.GetAllSupplierOnQualification(id);
            NotificationArguments NotificationArguments = new NotificationArguments();
            NotificationArguments.BodyEmailArgs = new object[] { tender.ReferenceNumber, tender.Purpose, tender.LastEnqueriesDate, tender.LastOfferPresentationDate, tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") };
            NotificationArguments.SubjectEmailArgs = new object[] { };
            NotificationArguments.PanelArgs = new object[] { tender.ReferenceNumber };
            NotificationArguments.SMSArgs = new object[] { tender.ReferenceNumber };
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(NotificationArguments, $"Qualification/CancelQualification?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, 0);
            switch (changeRequest.RequestedByRoleName)
            {
                case RoleNames.DataEntry:
                    await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.OperationsOnQualification.approveQualificationCancleRequest, tender.BranchId, approveTender);
                    break;
                case RoleNames.PreQualificationCommitteeSecretary:
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.PreQualificationSecretary.OperationsOnPreQualification.ConfirmQualificationCancleRequest, tender.PreQualificationCommitteeId, approveTender);
                    break;
                case RoleNames.OffersCheckSecretary:
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.OperationsOnPostqualification.approveQualificationCancleRequest, tender.OffersCheckingCommitteeId, approveTender);
                    break;
                case RoleNames.OffersPurchaseSecretary:
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseSecretary.OperationsOnQualification.approveQualificationCancleRequest, tender.DirectPurchaseCommitteeId, approveTender);
                    break;
            }
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnPostqualification.QualificationCancle, crs, approveTender);
            await _tenderCommands.UpdateTenderChangeRequestAsync(tenderRevisionCancel);
            return true;
        }

        public async Task<bool> RejectTenderCancelRequestAsync(int tenderId, string rejectionReason)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            var changeRequest = await _tenderQueries.GetCancelChangeRequest(tenderId);
            changeRequest.UpdateStatusToRejection(changeRequest.Tender.TenderStatusId, (int)Enums.ChangeStatusType.Rejected, rejectionReason, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.RejectTenderCancellation);
            await SendNotificationAfterCancelRejection(changeRequest, changeRequest.Tender);
            await _tenderCommands.UpdateTenderChangeRequestAsync(changeRequest);
            return true;
        }

        public async Task RejectTenderCancelRequestWhileTenderApproval(Tender tender, string requestedByRoleName)
        {
            var changeRequest = await _tenderQueries.GetPendingCancelChangeRequest(tender.TenderId, requestedByRoleName);
            if (changeRequest != null)
            {
                changeRequest.UpdateStatusToRejection();
                await SendNotificationAfterCancelRejection(changeRequest, tender);
                _genericCommandRepository.Update(changeRequest);
            }
        }

        private async Task SendNotificationAfterCancelRejection(TenderChangeRequest changeRequest, Tender tender)
        {
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", tender.ReferenceNumber, changeRequest.RejectionReason, tender.Purpose, tender.LastEnqueriesDate, tender.LastOfferPresentationDate, tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(NotificationArguments, $"Tender/CancelTender?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId);
            switch (changeRequest.RequestedByRoleName)
            {
                case RoleNames.DataEntry:
                    await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.OperationsOnTheTender.rejecttendercancellation, tender.BranchId, approveTender);
                    break;
                case RoleNames.PreQualificationCommitteeSecretary:
                    await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.PreQualificationSecretary.EditOperationsOnQualification.RejectQualificationCancleRequest, tender.BranchId, approveTender);
                    break;
                case RoleNames.PurshaseSpecialist:
                    await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.PurshaseSpecialist.OperationsOnTheTender.rejecttendercancellation, tender.BranchId, approveTender);
                    break;
                case RoleNames.OffersOpeningAndCheckSecretary:
                    approveTender = new MainNotificationTemplateModel(NotificationArguments, $"Tender/CancelTender?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), 0, tender.VROCommitteeId);
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.VROCheckSecretary.OperationsOnTheTender.RejectCancelTenderVRO, tender.VROCommitteeId, approveTender);
                    break;
                case RoleNames.OffersOppeningSecretary:
                    approveTender = new MainNotificationTemplateModel(NotificationArguments, $"Tender/CancelTender?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), 0, tender.OffersOpeningCommitteeId);
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersOppeningSecretary.OperationsOnTheTender.rejecttendercancellation, tender.OffersOpeningCommitteeId, approveTender);
                    break;
                case RoleNames.OffersCheckSecretary:
                    string link = (tender.TenderTypeId == (int)Enums.TenderType.PostQualification || tender.TenderTypeId == (int)Enums.TenderType.PreQualification ? $"Qualification/CancelQualification?STenderId={Util.Encrypt(tender.TenderId)}" : $"Tender/CancelTender?STenderId={Util.Encrypt(tender.TenderId)}");
                    NotificationArguments NotificationArgumentsecretary = new NotificationArguments
                    {
                        BodyEmailArgs = new object[] { "", tender.ReferenceNumber, changeRequest.RejectionReason, tender.Purpose, tender.LastEnqueriesDate, tender.LastOfferPresentationDate, tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                        SubjectEmailArgs = new object[] { },
                        PanelArgs = new object[] { tender.ReferenceNumber },
                        SMSArgs = new object[] { tender.ReferenceNumber }
                    };
                    approveTender = new MainNotificationTemplateModel(NotificationArgumentsecretary, link, NotificationEntityType.Tender, tender.TenderId.ToString(), 0, tender.OffersCheckingCommitteeId);
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.OperationsOnTheTender.rejecttendercancellation, tender.OffersCheckingCommitteeId, approveTender);
                    break;
                case RoleNames.OffersPurchaseSecretary:
                    string _link = (tender.TenderTypeId == (int)Enums.TenderType.PostQualification || tender.TenderTypeId == (int)Enums.TenderType.PreQualification ? $"Qualification/CancelQualification?STenderId={Util.Encrypt(tender.TenderId)}" : $"Tender/CancelTender?STenderId={Util.Encrypt(tender.TenderId)}");
                    NotificationArguments _NotificationArgumentsecretary = new NotificationArguments
                    {
                        BodyEmailArgs = new object[] { "", tender.ReferenceNumber, changeRequest.RejectionReason, tender.Purpose, tender.LastEnqueriesDate, tender.LastOfferPresentationDate, tender.OffersCheckingDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersCheckingDate?.ToString("dd/MM/yyyy"), tender.OffersCheckingDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersCheckingDate?.ToString("hh:mm tt") },
                        SubjectEmailArgs = new object[] { },
                        PanelArgs = new object[] { tender.ReferenceNumber },
                        SMSArgs = new object[] { tender.ReferenceNumber }
                    };
                    approveTender = new MainNotificationTemplateModel(_NotificationArgumentsecretary, _link, NotificationEntityType.Tender, tender.TenderId.ToString(), 0, tender.DirectPurchaseCommitteeId);
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseSecretary.OperationsOnTheTender.rejecttendercancellation, tender.DirectPurchaseCommitteeId, approveTender);
                    break;
            }
        }

        public async Task<bool> RejectQualificationCancelRequestAsync(int tenderId, string rejectionReason)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderStatusByTenderId(tenderId);
            Check.ArgumentNotNull(nameof(tender), tender);
            _tenderDomainService.IsValidToUpdateModel(tender);
            var changeRequest = await _tenderQueries.GetChangeRequest(tenderId, (int)Enums.ChangeRequestType.Canceling);
            changeRequest.UpdateStatusToRejection(tender.TenderStatusId, (int)Enums.ChangeStatusType.Rejected, rejectionReason, _httpContextAccessor.HttpContext.User.UserId(), TenderActions.RejectTenderCancellation);
            await _tenderCommands.UpdateTenderChangeRequestAsync(changeRequest);
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", tender.ReferenceNumber, changeRequest.RejectionReason,  tender.LastEnqueriesDate
                == null ? Resources.SharedResources.DisplayInputs.NotFound :tender.LastEnqueriesDate?.ToString("dd/MM/yyyy hh:mm tt")
                , tender.LastOfferPresentationDate    == null ? Resources.SharedResources.DisplayInputs.NotFound :tender.LastOfferPresentationDate?.ToString("dd/MM/yyyy hh:mm tt"), tender.OffersCheckingDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersCheckingDate?.ToString("dd/MM/yyyy"), tender.OffersCheckingDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersCheckingDate?.ToString("hh:mm tt") },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(NotificationArguments, $"Qualification/CancelQualification?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId);
            switch (changeRequest.RequestedByRoleName)
            {
                case RoleNames.DataEntry:
                    await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.OperationsOnQualification.RejectQualificationCancleRequest, tender.BranchId, approveTender);
                    break;
                case RoleNames.PreQualificationCommitteeSecretary:
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.PreQualificationSecretary.EditOperationsOnQualification.RejectQualificationCancleRequest, tender.PreQualificationCommitteeId, approveTender);
                    break;
                case RoleNames.OffersCheckSecretary:
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.OperationsOnPostqualification.RejectQualificationCancleRequest, tender.OffersCheckingCommitteeId, approveTender);
                    break;
                case RoleNames.OffersPurchaseSecretary:
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseSecretary.OperationsOnQualification.RejectQualificationCancleRequest, tender.DirectPurchaseCommitteeId, approveTender);
                    break;
            }
            return true;
        }

        public async Task ReopenTenderAfterCancel(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindTenderStatusByTenderId(tenderId);
            Check.ArgumentNotNull(nameof(tender), tender);
            _tenderDomainService.IsValidToUpdateModel(tender);
            var tenderRevisionCancel = await FindTenderRevisionCancelByTenderId(tender.TenderId, true);
            tenderRevisionCancel.DeActive();
            tender.AddActionHistory(tender.TenderStatusId, TenderActions.CancelCancellation, "", _httpContextAccessor.HttpContext.User.UserId());
            await _tenderCommands.UpdateTenderChangeRequestAsync(tenderRevisionCancel);
        }
        public async Task CancelTenderCancellationRequest(int tenderId)
        {
            Tender tender = await _tenderQueries.FindTenderStatusByTenderId(tenderId);
            var tenderRevisionCancel = await FindTenderRevisionCancelByTenderId(tender.TenderId, true);
            tenderRevisionCancel.DeActive();
            tender.AddActionHistory(tender.TenderStatusId, TenderActions.CancelCancellation, "", _httpContextAccessor.HttpContext.User.UserId());
            await _tenderCommands.UpdateTenderChangeRequestAsync(tenderRevisionCancel);
        }

        #endregion

        #endregion

        public async Task StartTenderCheckingOffers(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.GetTenderByIdWithoutAnyIncluds(tenderId);
            _tenderDomainService.IsValidToStartCheckingTender(tender);
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersChecking, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.UpdateTender);
            await _tenderCommands.UpdateAsync(tender);
        }


        public async Task<SupplierFullDataViewModel> GetSupplierInfoByCR(string CR)
        {
            var IDMSuppliers = await _idmAppService.GetSupplierInfoByCR(CR);
            SupplierFullDataViewModel supplierFullDataModel = _mapper.Map<SupplierFullDataViewModel>(IDMSuppliers);
            return supplierFullDataModel;
        }

        private async Task SendNotificationForCheckingApproval(Tender tender)
        {
            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", tender.ReferenceNumber, tender.Purpose, tender.LastEnqueriesDate.Value, tender.LastOfferPresentationDate.Value, tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(
             NotificationArgument, $"Tender/CheckTenderOffers?tenderIdString={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId, tender.OffersCheckingCommitteeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckManager.OperationsNeedToBeAccredited.sendtechnicalevaluationreportforapproval, tender.OffersCheckingCommitteeId, approveTender);
        }

        public async Task<TenderRelationsModel> UpdateTenderDates(TenderDatesModel tenderDatesModel)
        {
            Tender tender = await _tenderQueries.GetTenderWithDateAndAddressByTenderId(tenderDatesModel.TenderId);
            Check.ArgumentNotNull(nameof(tender), tender);
            _tenderDomainService.IsValidToUpdateDate(tenderDatesModel, tender);
            if (tenderDatesModel.OffersOpeningAddress != null)
            {
                Address address = new Address(tenderDatesModel.OffersOpeningAddress, (int)Enums.AddressType.OfferOpeningAddress, tenderDatesModel.BranchId);
                Check.ArgumentNotNull(nameof(address), address);
                _cache.Remove(CacheKeys.AddressesCache);
                tenderDatesModel.OffersOpeningAddressId = await _tenderCommands.CreateAddressAsync(address);
            }
            var isOldType = tender.TenderTypeId == (int)Enums.TenderType.CurrentTender || tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects;
            if (isOldType || tender.TenderTypeId == (int)Enums.TenderType.Competition || tender.TenderTypeId == (int)Enums.TenderType.FirstStageTender)
            {
                tenderDatesModel.NeedInitialGuarantee = false;
                tenderDatesModel.InitialGuaranteePercentage = null;
                tenderDatesModel.InitialGuaranteeAddress = null;
            }
            if (!isOldType && tenderDatesModel.VersionId >= (int)Enums.ActivityVersions.Sprint7Activities)
            {
                if (tender.TenderDates == null)
                {
                    TenderDates tenderDates = new TenderDates(tenderDatesModel.AwardingExpectedDate, tenderDatesModel.StartingBusinessOrServicesDate, null, tenderDatesModel.OffersDeliveryDate);
                    tender.SetTenderDates(tenderDates);
                }
                else
                {
                    tender.TenderDates.UpdateTenderDates(tenderDatesModel.AwardingExpectedDate, tenderDatesModel.StartingBusinessOrServicesDate, null, tenderDatesModel.OffersDeliveryDate);
                }

                if (tender.TenderAddress == null)
                {
                    TenderAddress tenderAddress = new TenderAddress(tenderDatesModel.IsSampleAddresSameOffersAddress, tenderDatesModel.OfferDeliveryAddress, tenderDatesModel.OfferBuildingName, tenderDatesModel.OfferFloarNumber, tenderDatesModel.OfferDepartmentName);
                    tender.SetTenderAddress(tenderAddress);
                }
                else
                {
                    tender.TenderAddress.UpdateTenderAddress(tenderDatesModel.IsSampleAddresSameOffersAddress, tenderDatesModel.OfferDeliveryAddress, tenderDatesModel.OfferBuildingName, tenderDatesModel.OfferFloarNumber, tenderDatesModel.OfferDepartmentName);
                }

            }
            if (!isOldType)
            {
                tender = tender.UpdateFinalAndInitialGuarantee(tenderDatesModel.FinalGuaranteePercentage, tenderDatesModel.InitialGuaranteePercentage, tenderDatesModel.InitialGuaranteeAddress);
            }
            tender = tender.AddEditTenderDates(tenderDatesModel.LastEnqueriesDate, tenderDatesModel.LastOfferPresentationDate, tenderDatesModel.OffersOpeningDate, tenderDatesModel.OffersCheckingDate,
                                                 tenderDatesModel.SupplierNeedSample, tenderDatesModel.SamplesDeliveryAddress, tenderDatesModel.OffersOpeningAddressId,
                                                tenderDatesModel.BuildingName, tenderDatesModel.FloarNumber, tenderDatesModel.DepartmentName, tenderDatesModel.DeliveryDate, tenderDatesModel.AwardingStoppingPeriod);

            TenderRelationsModel tenderRelationsModel = await _tenderQueries.FindTenderRelationsByTenderIdAfterUpdateDates(tenderDatesModel.TenderId);

            TenderRelationsModel relationsModel = new TenderRelationsModel
            {
                TemplateYears = tender.TemplateYears,
                TenderName = tender.TenderName,
                TenderNumber = tender.TenderNumber,
                AgencyCode = tender.AgencyCode,
                TenderIdString = Util.Encrypt(tender.TenderId),
                InsideKSA = tender.InsideKSA,
                TenderAreaIDs = tenderRelationsModel.TenderAreaIDs,
                TenderCountriesIDs = tenderRelationsModel.TenderCountriesIDs,
                TenderActivitieIDs = tenderRelationsModel.TenderActivitieIDs,
                TenderConstructionWorkIDs = tenderRelationsModel.TenderConstructionWorkIDs,
                TenderMentainanceRunnigWorkIDs = tenderRelationsModel.TenderMentainanceRunnigWorkIDs,
                Details = tender.Details,
                ActivityDescription = tender.ActivityDescription,
                TenderId = tender.TenderId,
                ReferenceNumber = tender.ReferenceNumber,
                TenderTypeId = tender.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedConfirmed && tender.TenderTypeId == (int)Enums.TenderType.FirstStageTender ? (int)Enums.TenderType.SecondStageTender : tender.TenderTypeId,
                TenderFirstStageId = tender.TenderFirstStageId != null ? tender.TenderFirstStageId : 0,
                TenderStatusId = tender.TenderStatusId,
                InvitationTypeId = tender.InvitationTypeId,
                PreQualificationId = tender.PreQualificationId,
                TenderCreatedByTypeId = tender.CreatedByTypeId,
                OfferPresentationWayId = tender.OfferPresentationWayId
            };
            _genericCommandRepository.Update(tender);

            await _genericCommandRepository.SaveAsync();
            return relationsModel;
        }

        #region Vendors Invitations

        public async Task SendInvitationsAsync(List<InvitationCrModel> suppliersModel)
        {
            var AgencyCode = _httpContextAccessor.HttpContext.User.UserAgency();
            var IDMSuppliers = await _idmAppService.GetContactOfficersByCR(suppliersModel.Select(s => s.CrNumber).ToList());
            foreach (var supplier in suppliersModel.Where(x => x.StatusId == 0 && x.IsChecked == true))
            {
                var checkBlockList = await _blockAppService.CheckifSupplierBlockedByCrNo(supplier.CrNumber, AgencyCode);
                if (checkBlockList)
                    throw new BusinessRuleException(string.Format(Resources.BlockResources.Messages.ThisCrBlocked, supplier.CrNumber));
            }
            Tender tender = await _tenderQueries.FindTenderInvitationsWithById(suppliersModel[0].TenderId);
            Check.ArgumentNotNull(nameof(tender), tender);
            List<InvitationCrModel> checkedSuppliers = suppliersModel.Where(x => x.StatusId != (int)InvitationStatus.Approved && x.IsChecked == true).ToList();
            List<string> checkedCommericalRegisterNo = checkedSuppliers.Select(i => i.CrNumber).ToList();
            List<string> unCheckedCommericalRegisterNo = suppliersModel.Where(x => x.StatusId != (int)InvitationStatus.Approved && x.IsChecked == false).Select(i => i.CrNumber).ToList();
            List<ContactOfficerModel> suppliers = IDMSuppliers.Where(x => checkedSuppliers.Where(s => s.StatusId == 0).Select(i => i.CrNumber).Contains(x.supplierNumber)).ToList();
            var invitedSuppliers = _mapper.Map<List<SupplierInvitationModel>>(suppliers);
            foreach (var item in checkedSuppliers)
            {
                var supplier = await _supplierQueries.FindSupplierByCRNumber(item.CrNumber);
                if (supplier == null)
                {
                    var supplierobj = new Supplier(item.CrNumber, item.CrName, await _notificationAppService.GetDefaultSettingByCr());

                    await _supplierCommands.CreateSupplierAsync(supplierobj);
                }
                else
                {
                    if (supplier.NotificationSetting.Count == 0)
                    {
                        supplier.AddNotificationSettings(await _notificationAppService.GetDefaultSettingByCr());
                        await _supplierCommands.UpdateSupplierAsync(supplier);
                    }
                }
            }
            tender.AddSupplierInvitation(checkedCommericalRegisterNo, unCheckedCommericalRegisterNo);
            tender.AddActionHistory(tender.TenderStatusId, TenderActions.InviteVendors, "", _httpContextAccessor.HttpContext.User.UserId());
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", tender.Agency.NameArabic, tender.Branch.BranchName, tender.TenderName, tender.ReferenceNumber, tender.Purpose, tender.LastEnqueriesDate.Value.ToString("dd/MM/yyyy"), tender.LastOfferPresentationDate.Value.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { "", tender.ReferenceNumber },
                SMSArgs = new object[] { "", tender.ReferenceNumber }
            };
            MainNotificationTemplateModel inviteSupplierModel = new MainNotificationTemplateModel(NotificationArguments, $"Tender/SupplierInvitationTenders", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.invitevendorstotender, invitedSuppliers.Select(i => i.CommericalRegisterNo).ToList(), inviteSupplierModel);
            foreach (var item in invitedSuppliers)
            {
                tender.RegisterSupplierMobilNo(new List<string> { item.mobile });
                tender.RegisterSupplierEmails(new List<string> { item.Email });
            }
            await _tenderCommands.UpdateAsync(tender);
        }

        public async Task<Invitation> UpdateInvitationStatus(int invitationId, int invitationStatusId, string UserCR)
        {
            Tender tender = await _tenderQueries.FindTenderByInvitationId(invitationId);
            var checkBlockList = await _blockAppService.CheckifSupplierBlockedByCrNo(UserCR);
            if (checkBlockList)
                throw new BusinessRuleException(Resources.BlockResources.Messages.CrBlocked);
            Invitation invitation = tender.Invitations.FirstOrDefault(x => x.InvitationId == invitationId);
            tender.UpdateSamplesDeliveryAddress("");
            invitation.UpdateStatus(invitationStatusId, "");
            if (invitationStatusId == (int)Enums.InvitationStatus.Withdrawal)
            {
                tender.AddActionHistory(tender.TenderStatusId, TenderActions.CancelVendorInvitation, "", _httpContextAccessor.HttpContext.User.UserId());
                var offer = tender.Offers.FirstOrDefault(o => o.IsActive == true && o.CommericalRegisterNo == UserCR);
                if (offer != null)
                {
                    offer.UpdateStatus(Enums.OfferStatus.Canceled);
                }
                if (invitation.BillInfo != null)
                {
                    await _billCommand.DeleteWithoutSave(invitation.BillInfo);
                }
            }
            else if (invitationStatusId == (int)Enums.InvitationStatus.Approved)
            {
                tender.AddActionHistory(tender.TenderStatusId, TenderActions.AcceptInvitation, "", _httpContextAccessor.HttpContext.User.UserId());
            }
            else if (invitationStatusId == (int)Enums.InvitationStatus.Rejected)
            {
                tender.AddActionHistory(tender.TenderStatusId, TenderActions.RejectInvitation, "", _httpContextAccessor.HttpContext.User.UserId());
            }
            await _tenderCommands.UpdateAsync(tender);
            await UpdateConditionBooklet(tender.TenderId, UserCR);
            return invitation;
        }
        private async Task UpdateConditionBooklet(int tenderId, string UserCR)
        {
            var conditionBooklet = await _tenderQueries.GetConditionBookletByCrAndTenderId(tenderId, UserCR);
            if (conditionBooklet != null)
            {
                conditionBooklet.DeleteWithBill();
                await _billCommand.DeleteWithoutSave(conditionBooklet.BillInfo);
                _genericCommandRepository.Update(conditionBooklet);
                await _genericCommandRepository.SaveAsync();
            }

        }
        public async Task AcceptInvitationWithFees(TenderFinantialCostModel costModel)
        {
            Invitation invitation = await _tenderQueries.GetInvitationById(costModel.Id);
            if (invitation.Tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || invitation.Tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                invitation.UpdateStatus((int)Enums.InvitationStatus.Approved, "");
                await _tenderCommands.UpdateInvititionAsync(invitation);
            }
            else
            {
                bool isBillExpired = (invitation.BillInfo != null && invitation.BillInfo.IsActive == true && invitation.BillInfo.BillStatusId == (int)Enums.BillStatus.SuccessUploaded && invitation.BillInfo.ExpiryDate < DateTime.Now) ? true : false;

                if (isBillExpired)
                {
                    costModel.IsbuyingBooklet = invitation.BillInfo.ConditionsBookletID != null ? true : false;
                    await UpdateInvitationInExciperedBills(invitation.BillInfo);
                }
                invitation.UpdateStatus((int)Enums.InvitationStatus.WaitingPayment, "");
                var cr = _httpContextAccessor.HttpContext.User.SupplierNumber();
                List<TenderFinantialCostModel> finantialCostModel = new List<TenderFinantialCostModel>();
                decimal price = invitation.Tender.ConditionsBookletPrice ?? 0;
                decimal totalPrice = 0;
                var invitationFees = invitation.Tender.TenderType.InvitationCost;
                var buyingBooklet = costModel.IsbuyingBooklet;
                if (buyingBooklet && price != 0 && invitationFees != 0)
                {
                    finantialCostModel.Add(new TenderFinantialCostModel() { FessType = (int)Enums.TenderFeesType.FinantialCostForInvitation, FeesValue = invitationFees, GFSCode = _rootConfiguration.BillingConfiguration.GFSCODEForInvitations });
                    finantialCostModel.Add(new TenderFinantialCostModel() { FessType = (int)Enums.TenderFeesType.ConditionalBookletPrice, FeesValue = price, GFSCode = _rootConfiguration.BillingConfiguration.GFSCODEForConditionalBooklet });
                    totalPrice += invitationFees;
                    totalPrice += price;
                }
                else if (buyingBooklet && price == 0 && invitationFees != 0)
                {
                    finantialCostModel.Add(new TenderFinantialCostModel() { FessType = (int)Enums.TenderFeesType.FinantialCostForInvitation, FeesValue = invitationFees, GFSCode = _rootConfiguration.BillingConfiguration.GFSCODEForInvitations });
                    totalPrice += invitationFees;
                }
                else if (buyingBooklet && price != 0 && invitationFees == 0)
                {
                    finantialCostModel.Add(new TenderFinantialCostModel() { FessType = (int)Enums.TenderFeesType.ConditionalBookletPrice, FeesValue = price, GFSCode = _rootConfiguration.BillingConfiguration.GFSCODEForConditionalBooklet });
                    totalPrice += price;
                }
                else if (invitationFees != 0)
                {
                    finantialCostModel.Add(new TenderFinantialCostModel() { FessType = (int)Enums.TenderFeesType.FinantialCostForInvitation, FeesValue = invitationFees, GFSCode = _rootConfiguration.BillingConfiguration.GFSCODEForInvitations });
                    totalPrice += invitationFees;
                }
                BillInfo billInfo = new BillInfo(totalPrice, DateTime.Now, invitation.Tender.LastOfferPresentationDate.Value.Date == DateTime.Now.Date ? invitation.Tender.LastOfferPresentationDate.Value.AddDays(1) : invitation.Tender.LastOfferPresentationDate.Value, invitation.Tender.AgencyCode);
                if (invitationFees == 0 && !buyingBooklet)
                {
                    invitation.UpdateStatus((int)Enums.InvitationStatus.Approved, "");
                    billInfo.UpdateBillStatus(Enums.BillStatus.Paid);
                    invitation.AddBillInfo(billInfo);
                    await _tenderCommands.UpdateInvititionAsync(invitation);
                }
                else
                {
                    if (price == 0 && invitationFees == 0 && buyingBooklet)
                    {
                        invitation.UpdateStatus((int)Enums.InvitationStatus.Approved, "");
                        billInfo.UpdateBillStatus(Enums.BillStatus.Paid);
                    }
                    if (buyingBooklet)
                    {
                        var conditions = new ConditionsBooklet(cr, billInfo);
                        invitation.Tender.ConditionsBooklets.Add(conditions);
                    }
                    invitation.AddBillInfo(billInfo);
                    await _tenderCommands.UpdateInvititionAsync(invitation);
                    if (billInfo.AmountDue != 0)
                    {
                        BillModel billModel = new BillModel();
                        var supplierInfo = await GetSupplierInfoByCR(cr);
                        billModel.AmountDue = billInfo.AmountDue;
                        billModel.BillInvoiceNumber = "";
                        billModel.DueDate = billInfo.DueDate;
                        billModel.ExpDate = billInfo.ExpiryDate;
                        billModel.AgencyCode = billInfo.AgencyCode;
                        billModel.DisplayLabelAr = supplierInfo != null ? supplierInfo.supplierName : "";
                        billModel.DisplayLabelEn = supplierInfo != null ? supplierInfo.CRNameEN : "";
                        billModel.AgencyType = invitation.Tender.Agency.CategoryId.Value;
                        billModel.RequestType = (int)Enums.RequestType.InvitationRequest;
                        billModel.TenderFees = finantialCostModel;
                        await _billsInqueryAppService.StoreBillsAndUploadToSadad(billModel, billInfo, cr, invitation.Tender.Agency.CategoryId.Value, false);
                    }
                }
            }
        }

        public async Task JoinDirectPurchaseTender(int tenderId, string cR)
        {
            var tender = await _tenderQueries.GetTenderWithTypeAndInvitations(tenderId);
            var invitation = new Invitation(cR, Enums.InvitationStatus.WaitingPayment, InvitationRequestType.Request, false, (int)Enums.InvititedSupplierTypes.Foriegn);
            tender.Invitations.Add(invitation);
            List<TenderFinantialCostModel> finantialCostModel = new List<TenderFinantialCostModel>();
            decimal totalPrice = 0;

            finantialCostModel.Add(new TenderFinantialCostModel() { FessType = (int)Enums.TenderFeesType.FinantialCostForInvitation, FeesValue = tender.TenderType.InvitationCost, GFSCode = _rootConfiguration.BillingConfiguration.GFSCODEForInvitations });
            totalPrice = tender.TenderType.InvitationCost;
            BillInfo billInfo = new BillInfo(totalPrice, DateTime.Now, tender.LastOfferPresentationDate.Value.Date == DateTime.Now.Date ? tender.LastOfferPresentationDate.Value.AddDays(1) : tender.LastOfferPresentationDate.Value, tender.AgencyCode);
            invitation.AddBillInfo(billInfo);
            await _tenderCommands.UpdateAsync(tender);
            if (billInfo.AmountDue != 0)
            {
                BillModel billModel = new BillModel();
                var supplierInfo = await GetSupplierInfoByCR(cR);
                billModel.AmountDue = billInfo.AmountDue;
                billModel.BillInvoiceNumber = "";
                billModel.DueDate = billInfo.DueDate;
                billModel.ExpDate = billInfo.ExpiryDate;
                billModel.AgencyCode = billInfo.AgencyCode;
                billModel.DisplayLabelAr = supplierInfo != null ? supplierInfo.supplierName : "";
                billModel.DisplayLabelEn = supplierInfo != null ? supplierInfo.CRNameEN : "";
                billModel.AgencyType = invitation.Tender.Agency.CategoryId.Value;
                billModel.RequestType = (int)Enums.RequestType.InvitationRequest;
                billModel.TenderFees = finantialCostModel;
                await _billsInqueryAppService.StoreBillsAndUploadToSadad(billModel, billInfo, cR, invitation.Tender.Agency.CategoryId.Value, false);
            }

        }
        private async Task UpdateInvitationInExciperedBills(BillInfo billInfo)
        {

            if (billInfo.ConditionsBookletID != null)
            {
                billInfo.DeleteBooklet();
            }
            await _billCommand.DeleteWithoutSave(billInfo);
            billInfo.DeleteInvitation();
            await _billCommand.Save();
        }
        public async Task SendInvitationsByEmail(int tenderId, List<string> emails)
        {
            Tender tender = await _tenderQueries.FindTenderByTenderId(tenderId);
            Check.ArgumentNotNull(nameof(tender), tender);
            tender.RegisterSupplierEmails(emails);
            await _tenderCommands.UpdateAsync(tender);
            await _notificationAppService.SendInvitationByEmail(emails, tender);
        }

        public async Task SendInvitationBySms(int tenderId, List<string> mobilNoList)
        {
            Tender tender = await _tenderQueries.FindTenderByTenderId(tenderId);
            Check.ArgumentNotNull(nameof(tender), tender);
            tender.RegisterSupplierMobilNo(mobilNoList);
            await _tenderCommands.UpdateAsync(tender);
            await _notificationAppService.SendInvitationBySms(mobilNoList, tender);
        }

        #endregion

        #region Vendors Join Requests

        public async Task<bool> SendRequestToApplayForTender(int tenderId, string commericalRegisterNo)
        {
            Tender tender = await _tenderQueries.FindTenderInvitationByTenderId(tenderId);
            var isSupplierHasJoinRequest = CheckValidAddRequest(tender, commericalRegisterNo);
            if (isSupplierHasJoinRequest)
                return false;

            var isBlockedSupplier = await _blockAppService.CheckifSupplierBlockedByCrNo(commericalRegisterNo);
            if (isBlockedSupplier)
                throw new BusinessRuleException(Resources.BlockResources.Messages.CrBlocked);

            if (tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase && tender.InvitationTypeId == (int)Enums.InvitationType.Public)
            {
                tender.AddInvitationBySupplier(commericalRegisterNo);
                tender.AddActionHistory(tender.TenderStatusId, TenderActions.InviteVendors, "", _httpContextAccessor.HttpContext.User.UserId());
                await SendNotificationForSuppliersForPublicDirectPurchase(tender, commericalRegisterNo);
            }
            else
            {
                tender.SendRequestForTender(commericalRegisterNo);
                tender.AddActionHistory(tender.TenderStatusId, TenderActions.AskForInvitation, "", _httpContextAccessor.HttpContext.User.UserId());
                await SendNotificationForSuppliersJoiningRequest(tender, commericalRegisterNo);
            }
            await _tenderCommands.UpdateAsync(tender);

            return true;
        }


        private async Task SendNotificationForSuppliersJoiningRequest(Tender tender, string commericalRegisterNo)
        {
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", tender.ReferenceNumber, tender.Purpose, tender.LastEnqueriesDate?.ToString("dd/MM/yyyy"), tender.LastOfferPresentationDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel inviteSupplierModel = new MainNotificationTemplateModel(NotificationArguments,
                $"Tender/SuppliersJoiningRequest?tenderIdString=" + Util.Encrypt(tender.TenderId),
                NotificationEntityType.Tender,
                tender.TenderId.ToString(),
                tender.BranchId
            );
            if (tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.PurshaseSpecialist.OperationsOnTheTender.joinrequest, tender.BranchId, inviteSupplierModel);
            else
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.OperationsOnTheTender.joinrequest, tender.BranchId, inviteSupplierModel);
        }
        private async Task SendNotificationForSuppliersForPublicDirectPurchase(Tender tender, string commericalRegisterNo)
        {
            NotificationArguments invNotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", tender.Agency.NameArabic, tender.Branch.BranchName, tender.TenderName, tender.ReferenceNumber,
                    tender.Purpose, tender.LastEnqueriesDate.Value.ToString("dd/MM/yyyy"), tender.LastOfferPresentationDate.Value.ToString("dd/MM/yyyy"),
                    tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"),
                    tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { "", tender.ReferenceNumber },
                SMSArgs = new object[] { "", tender.ReferenceNumber }
            };
            MainNotificationTemplateModel _inviteSupplierModel = new MainNotificationTemplateModel(invNotificationArguments,
                $"Tender/SupplierInvitationTenders", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.invitevendorstotender, new List<string> { commericalRegisterNo }, _inviteSupplierModel);
        }

        private bool CheckValidAddRequest(Tender tender, string commericalRegisterNo)
        {
            var isSupplierHasJoinRequest = tender.Invitations
                 .Any(x => x.CommericalRegisterNo == commericalRegisterNo
                 && x.InvitationTypeId == (int)Enums.InvitationRequestType.Request && x.IsActive == true);
            return isSupplierHasJoinRequest; //== null ? true : false;
        }
        public async Task<Invitation> ApproveJoiningRequestStatus(int invitationId, int statusId)
        {
            Invitation invitation = await _tenderQueries.FindInvitationByInvitationId(invitationId);
            Tender tender = await _tenderQueries.FindTenderByTenderId(invitation.TenderId);
            invitation.UpdateStatus(statusId, "");
            tender.AddActionHistory(tender.TenderStatusId, TenderActions.AcceptRequestForInvitation, "", _httpContextAccessor.HttpContext.User.UserId());
            await _tenderCommands.UpdateInvititionAsync(invitation);
            NotificationArguments acceptvendorrequest = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", tender.TenderName, "", tender.ReferenceNumber, tender.Purpose, tender.LastEnqueriesDate, tender.LastOfferPresentationDate, tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel templetModel = new MainNotificationTemplateModel(acceptvendorrequest,
                $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}",
                NotificationEntityType.Tender,
                tender.TenderId.ToString(),
                tender.BranchId);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.acceptvendorrequest, new List<string> { invitation.CommericalRegisterNo }, templetModel);
            return invitation;
        }

        public async Task<Invitation> RejectJoiningRequestStatus(int invitationId, int invitationStatusId, string rejectionReason)
        {
            Invitation invitation = await _tenderQueries.FindInvitationByInvitationId(invitationId);
            Tender tender = await _tenderQueries.FindTenderByTenderId(invitation.TenderId);
            Check.ArgumentNotNullOrEmpty(nameof(rejectionReason), rejectionReason);
            invitation.UpdateStatus(invitationStatusId, rejectionReason);
            tender.AddActionHistory(tender.TenderStatusId, TenderActions.RejectRequestForInvitation, rejectionReason, _httpContextAccessor.HttpContext.User.UserId());
            await _tenderCommands.UpdateInvititionAsync(invitation);
            MainNotificationTemplateModel rejectvendorrequest = new MainNotificationTemplateModel(
                               new NotificationArguments
                               {
                                   SubjectEmailArgs = new object[] { },
                                   BodyEmailArgs = new object[] { "", tender.TenderName, tender.ReferenceNumber, rejectionReason, tender.LastEnqueriesDate, tender.LastOfferPresentationDate, tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                                   SMSArgs = new object[] { tender.ReferenceNumber },
                                   PanelArgs = new object[] { tender.ReferenceNumber },
                               },
                                $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}",
                                NotificationEntityType.Tender,
                                tender.TenderId.ToString(),
                                tender.BranchId);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.rejectvendorrequest, new List<string> { invitation.CommericalRegisterNo }, rejectvendorrequest);
            return invitation;
        }

        #endregion

        public async Task UpdateTenderRelationsWithoutQuantityTable(TenderRelationsModel model)
        {
            if (model == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            Tender tender = await _tenderQueries.FindTenderRelationsWithoutQuantityTables(Util.Decrypt(model.TenderIdString));
            tender.UpdateTenderRelationsWithoutQuantityTables(model.TenderAreaIDs, model.TenderActivitieIDs, model.TenderConstructionWorkIDs, model.TenderMentainanceRunnigWorkIDs, model.InsideKSA, model.Details, model.ActivityDescription, model.TenderCountriesIDs, model.IsTawreed, false, (int)Enums.TenderStatus.UnderEstablishing);
            await _tenderCommands.UpdateAsync(tender);
        }

        public async Task<QuantitiesTemplateModel> UpdateTenderRelations(TenderRelationsModel model)
        {
            Tender tender = null;
            if (model == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            tender = await _tenderQueries.FindTenderRelationsWithoutQuantityTables(Util.Decrypt(model.TenderIdString));

            List<int> tenderActivitiesIds = tender.TenderActivities.Select(a => a.ActivityId).ToList();
            var differntActivities = tenderActivitiesIds.Except(model.TenderActivitieIDs).Union(model.TenderActivitieIDs.Except(tenderActivitiesIds)).ToList();
            tender.UpdateTenderRelationsWithoutQuantityTables(model.TenderAreaIDs, model.TenderActivitieIDs, model.TenderConstructionWorkIDs, model.TenderMentainanceRunnigWorkIDs, model.InsideKSA, model.Details, model.ActivityDescription, model.TenderCountriesIDs, model.IsTawreed, false, (int)Enums.TenderStatus.UnderEstablishing);

            if (tender.TemplateYears != model.TemplateYears || (differntActivities.Any() && tenderActivitiesIds.Any()))
            {
                tender.UpdateTenderTemplateYears(model.TemplateYears);
                await _tenderQueries.DeleteQTUsingStoredProcedure(tender.TenderId);
            }
            _genericCommandRepository.Update(tender);

            await _genericCommandRepository.SaveAsync();
            QuantitiesTemplateModel QuantityTable = new QuantitiesTemplateModel
            {
                TenderIdString = Util.Encrypt(tender.TenderId),
                TenderName = tender.ReferenceNumber,
                TenderTypeId = tender.TenderTypeId,
                TenderID = tender.TenderId,
                HasAlternativeOffer = tender.HasAlternativeOffer ?? false
            };
            return QuantityTable;
        }

        public async Task<BasicTenderInfoModel> UpdateTenderAttachmentsAsync(List<TenderAttachmentModel> attachments, int branchId)
        {
            var tenderAttachments = new List<TenderAttachment>();
            foreach (var item in attachments)
            {
                tenderAttachments.Add(new TenderAttachment(item.Name, item.FileNetReferenceId, item.AttachmentTypeId, null, null));
            }
            Tender tender = await _tenderQueries.FindTenderAttachmentsStepById(attachments[0].TenderId, branchId);
            var hasInvitation = ((tender.PreQualificationId != null || tender.TenderTypeId == (int)Enums.TenderType.SecondStageTender || tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.ReverseBidding
                || tender.TenderTypeId == (int)Enums.TenderType.LimitedTender || tender.TenderTypeId == (int)Enums.TenderType.FrameworkAgreement || tender.TenderTypeId == (int)Enums.TenderType.Competition || tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects) && tender.InvitationTypeId == (int)Enums.InvitationType.Specific)
                || (tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase && tender.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.TwoSepratedFiles);

            if (!hasInvitation)
            {
                tender.UpdateTenderStatus(Enums.TenderStatus.Established, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.CreateTender);
            }
            else
            {
                tender.UpdateTenderStatus(Enums.TenderStatus.UnderEstablishing);
            }
            tender.UpdateAttachments(tenderAttachments, _httpContextAccessor.HttpContext.User.UserId());
            tender = await _tenderCommands.UpdateAsync(tender);
            return new BasicTenderInfoModel
            {
                TenderId = tender.TenderId,
                TenderIdString = Util.Encrypt(tender.TenderId),
                TenderStatusId = tender.TenderStatusId,
                TenderTypeId = tender.TenderTypeId,
                InvitationTypeId = tender.InvitationTypeId,
                PreQualificationId = tender.PreQualificationId,
                HasInvitaitons = hasInvitation
            };
        }

        public async Task<bool> IsTenderPurchasedBySupplier(int tenderId, string CR)
        {
            return await _tenderQueries.IsTenderPurchasedBySupplier(tenderId, CR);
        }

        private async Task<bool> UpdateBillsThroughTahseel(int tenderId, DateTime ExpDate)
        {

            Tender tenderWithConditionBooklet = await _tenderQueries.FindTenderWithConditionsBookletsBills(tenderId);

            List<BillModel> billModel = new List<BillModel>();
            if (tenderWithConditionBooklet.ConditionsBooklets.Any())
            {
                foreach (var item in tenderWithConditionBooklet.ConditionsBooklets.Where(x => x.BillInfo != null && x.BillInfo.BillInvoiceNumber != null && x.BillInfo.BillStatusId != (int)Enums.BillStatus.Paid).ToList())
                {
                    billModel.Add(new BillModel()
                    {
                        BillInvoiceNumber = item.BillInfo.BillInvoiceNumber,
                        DisplayLabelAr = item.BillInfo.DisplayLabelAr,
                        DisplayLabelEn = item.BillInfo.DisplayLabelEn,
                        ExpDate = ExpDate,
                        ChapterNumber = item.BillInfo.BenChapterNumber,
                        NumOfSubSections = item.BillInfo.BenSubSectionsCount,
                        SequenceNumber = item.BillInfo.BenSequenceNumber,
                        NumOfSubDepartments = item.BillInfo.BenSubDepartmentsCount,
                        SectionId = item.BillInfo.BenSubSectionsCount,
                        BankBranchId = item.BillInfo.BankBranchId,
                        DueDate = DateTime.Now,
                        AmountDue = item.BillInfo.AmountDue,
                        ActionStatus = (int)Enums.BillActionStatus.UpdateBill,
                        AgencyCode = item.BillInfo.AgencyCode
                    });
                }
            }
            var tenderWithInvitations = await _tenderQueries.FindTenderWithInvitationsBills(tenderWithConditionBooklet.TenderId);
            if (tenderWithInvitations.Invitations.Any())
            {
                foreach (var item in tenderWithInvitations.Invitations.Where(x => x.BillInfo != null && x.BillInfo.BillInvoiceNumber != null && x.BillInfo?.BillStatusId != (int)Enums.BillStatus.Paid).ToList())
                {
                    if (!billModel.Select(s => s.BillInvoiceNumber).Contains(item.BillInfo.BillInvoiceNumber))
                    {
                        billModel.Add(new BillModel()
                        {
                            BillInvoiceNumber = item.BillInfo.BillInvoiceNumber,
                            DisplayLabelAr = item.BillInfo.DisplayLabelAr,
                            DisplayLabelEn = item.BillInfo.DisplayLabelEn,
                            ExpDate = ExpDate,
                            ChapterNumber = item.BillInfo.BenChapterNumber,
                            NumOfSubSections = item.BillInfo.BenSubSectionsCount,
                            SequenceNumber = item.BillInfo.BenSequenceNumber,
                            NumOfSubDepartments = item.BillInfo.BenSubDepartmentsCount,
                            SectionId = item.BillInfo.BenSubSectionsCount,
                            BankBranchId = item.BillInfo.BankBranchId,
                            DueDate = DateTime.Now,
                            AmountDue = item.BillInfo.AmountDue,
                            ActionStatus = (int)Enums.BillActionStatus.UpdateBill,
                            AgencyCode = item.BillInfo.AgencyCode
                        });
                    }
                }
            }
            if (billModel.Any())
            {
                var result = await _billsInqueryAppService.UpdateBulkBillsStatus(billModel);
                return result;
            }
            else
            {
                return true;
            }
        }

        private async Task<bool> CancelBulkBillsThroughTahseel(int tenderId, string actionReason)
        {

            Tender tenderWithConditionBooklet = await _tenderQueries.FindTenderWithConditionsBookletsBills(tenderId);

            List<BillModel> billModel = new List<BillModel>();
            if (tenderWithConditionBooklet.ConditionsBooklets.Any())
            {
                foreach (var item in tenderWithConditionBooklet.ConditionsBooklets.Where(x => x.BillInfo != null && x.BillInfo.BillInvoiceNumber != null && x.BillInfo?.BillStatusId != (int)Enums.BillStatus.Paid).ToList())
                {
                    billModel.Add(new BillModel()
                    {
                        BillInvoiceNumber = item.BillInfo.BillInvoiceNumber,
                        DisplayLabelAr = item.BillInfo.DisplayLabelAr,
                        DisplayLabelEn = item.BillInfo.DisplayLabelEn,
                        ChapterNumber = item.BillInfo.BenChapterNumber,
                        NumOfSubSections = item.BillInfo.BenSubSectionsCount,
                        SequenceNumber = item.BillInfo.BenSequenceNumber,
                        NumOfSubDepartments = item.BillInfo.BenSubDepartmentsCount,
                        SectionId = item.BillInfo.BenSubSectionsCount,
                        BankBranchId = item.BillInfo.BankBranchId,
                        AmountDue = item.BillInfo.AmountDue,
                        DueDate = DateTime.Now,
                        ActionReason = actionReason,
                        ActionStatus = (int)BillActionStatus.CancelBill,
                        AgencyCode = item.BillInfo.AgencyCode
                    });
                }
            }

            var tenderWithInvitations = await _tenderQueries.FindTenderWithInvitationsBills(tenderWithConditionBooklet.TenderId);
            if (tenderWithInvitations.Invitations.Any())
            {
                foreach (var item in tenderWithInvitations.Invitations.Where(x => x.BillInfo != null && x.BillInfo.BillInvoiceNumber != null && x.BillInfo?.BillStatusId != (int)Enums.BillStatus.Paid).ToList())
                {
                    if (!billModel.Select(s => s.BillInvoiceNumber).Contains(item.BillInfo.BillInvoiceNumber))
                    {
                        billModel.Add(new BillModel()
                        {
                            BillInvoiceNumber = item.BillInfo.BillInvoiceNumber,
                            DisplayLabelAr = item.BillInfo.DisplayLabelAr,
                            DisplayLabelEn = item.BillInfo.DisplayLabelEn,
                            ChapterNumber = item.BillInfo.BenChapterNumber,
                            NumOfSubSections = item.BillInfo.BenSubSectionsCount,
                            SequenceNumber = item.BillInfo.BenSequenceNumber,
                            NumOfSubDepartments = item.BillInfo.BenSubDepartmentsCount,
                            SectionId = item.BillInfo.BenSubSectionsCount,
                            BankBranchId = item.BillInfo.BankBranchId,
                            AmountDue = item.BillInfo.AmountDue,
                            DueDate = DateTime.Now,
                            ActionReason = actionReason,
                            ActionStatus = (int)Enums.BillActionStatus.CancelBill,
                            AgencyCode = item.BillInfo.AgencyCode
                        });
                    }
                }
            }
            if (billModel.Any())
            {
                var result = await _billsInqueryAppService.UpdateBulkBillsStatus(billModel);
                return result;
            }
            else
            {
                return true;
            }
        }

        public async Task<PurchaseModel> PurshaseTender(int tenderId, string CR, string mobile, string email)
        {
            Tender tender = await _tenderQueries.FindTenderAndAgencyByTenderId(tenderId);
            if (tender.LastOfferPresentationDate < DateTime.Now)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.OfferPresentationDatePassed);
            var conditionsBooklet = await _tenderQueries.FindConditionsBookletForRePurchase(tenderId, CR);
            bool IsBillExpired = (conditionsBooklet != null && conditionsBooklet.BillInfo != null && conditionsBooklet.BillInfo.IsActive == true && conditionsBooklet.BillInfo.BillStatusId == (int)Enums.BillStatus.SuccessUploaded && conditionsBooklet.BillInfo.ExpiryDate < DateTime.Now) ? true : false;

            if (IsBillExpired)
            {
                await UpdateConditionBooklet(tenderId, CR);
            }
            var checkBlockList = await _blockAppService.CheckifSupplierBlockedByCrNo(CR);
            if (checkBlockList)
                throw new BusinessRuleException(Resources.BlockResources.Messages.CrBlocked);
            var price = tender.ConditionsBookletPrice == null ? 0 : (decimal)tender.ConditionsBookletPrice;
            var buyingFees = tender.TenderType.BuyingCost;
            var haveToPayBuyingFees = (!(tender.Invitations.Any(i => i.IsActive == true && i.StatusId == (int)Enums.InvitationStatus.Approved && i.CommericalRegisterNo == CR)) && tender.TenderTypeId != (int)Enums.TenderType.CurrentTender);
            List<TenderFinantialCostModel> finantialCostModel = new List<TenderFinantialCostModel>();
            if (haveToPayBuyingFees && price != 0 && buyingFees != 0)
            {
                finantialCostModel.Add(new TenderFinantialCostModel() { FessType = (int)Enums.TenderFeesType.FinantialCostForConditionalBooklet, FeesValue = buyingFees, GFSCode = _rootConfiguration.BillingConfiguration.GFSCODEForAddedValue });
                finantialCostModel.Add(new TenderFinantialCostModel() { FessType = (int)Enums.TenderFeesType.ConditionalBookletPrice, FeesValue = price, GFSCode = _rootConfiguration.BillingConfiguration.GFSCODEForConditionalBooklet });
                price += buyingFees;
            }
            else if (haveToPayBuyingFees && price == 0 && buyingFees != 0)
            {
                finantialCostModel.Add(new TenderFinantialCostModel() { FessType = (int)Enums.TenderFeesType.FinantialCostForConditionalBooklet, FeesValue = buyingFees, GFSCode = _rootConfiguration.BillingConfiguration.GFSCODEForAddedValue });
                price += buyingFees;
            }
            else if (price != 0)
            {
                finantialCostModel.Add(new TenderFinantialCostModel() { FessType = (int)Enums.TenderFeesType.ConditionalBookletPrice, FeesValue = price, GFSCode = _rootConfiguration.BillingConfiguration.GFSCODEForConditionalBooklet });
            }
            BillInfo billInfo = new BillInfo(price, DateTime.Now, tender.LastOfferPresentationDate.Value.Date == DateTime.Now.Date ? tender.LastOfferPresentationDate.Value.AddDays(1) : tender.LastOfferPresentationDate.Value, tender.AgencyCode);
            BillModel billModel = new BillModel();
            if (billInfo.AmountDue != 0)
            {
                var supplierInfo = await GetSupplierInfoByCR(CR);
                billModel.AmountDue = billInfo.AmountDue;
                billModel.BillInvoiceNumber = "";
                billModel.DueDate = billInfo.DueDate;
                billModel.ExpDate = billInfo.ExpiryDate;
                billModel.AgencyCode = billInfo.AgencyCode;
                billModel.DisplayLabelAr = supplierInfo != null ? supplierInfo.supplierName : "";
                billModel.DisplayLabelEn = supplierInfo != null ? supplierInfo.CRNameEN : "";
                billModel.AgencyType = tender.Agency.CategoryId.Value;
                billModel.RequestType = (int)RequestType.ConditionalBookletRequest;
                billModel.TenderFees = finantialCostModel;
                billModel = await CheckSadadDetailsForAgency(billModel);
                string calculateAgencyCodeFromIDM = billModel.ChapterNumber + billModel.BankBranchId + billModel.SectionId + billModel.SequenceNumber + billModel.NumOfSubDepartments + billModel.NumOfSubSections;
                if (calculateAgencyCodeFromIDM == "" || calculateAgencyCodeFromIDM.Length < 18)
                {
                    throw new BusinessRuleException(Resources.TenderResources.Messages.IDmSadadDetailsNotExist);
                }
            }
            var conditions = new ConditionsBooklet(CR, billInfo);
            tender.ConditionsBooklets.Add(conditions);
            tender.AddActionHistory(tender.TenderStatusId, TenderActions.PurchaseBooklet, "", _httpContextAccessor.HttpContext.User.UserId());
            await _tenderCommands.UpdateAsync(tender);
            await _billsInqueryAppService.StoreBillsAndUploadToSadad(billModel, billInfo, CR, tender.Agency.CategoryId.Value, false);
            PurchaseModel model = new PurchaseModel()
            {
                TenderId = tender.TenderId,
                CommericalRegisterNo = CR,
                ConditionsBookletId = conditions.ConditionsBookletId,
                SadadNumber = Resources.TenderResources.DisplayInputs.SendDetailesByMessage,
                Price = price,
                Email = email,
                Mobile = mobile,
                TenderName = tender.ReferenceNumber,
                TenderNumber = tender.TenderNumber,
                CompanyName = "",
                BillInvoiceNumber = billInfo.BillInvoiceNumber
            };
            return model;
        }

        private async Task<BillModel> CheckSadadDetailsForAgency(BillModel model)
        {
            var govAgencyInfo = await _idmAppService.GetAgencyDetailsRelatedToSadad(model.AgencyCode, model.AgencyType);
            if (govAgencyInfo != null)
            {
                model.ChapterNumber = !string.IsNullOrEmpty(govAgencyInfo.beneficiaryClassNo) ? govAgencyInfo.beneficiaryClassNo : "000";
                model.BankBranchId = !string.IsNullOrEmpty(govAgencyInfo.beneficiaryBranchNo) ? govAgencyInfo.beneficiaryBranchNo : "000";
                model.SectionId = !string.IsNullOrEmpty(govAgencyInfo?.beneficiarySectionNo) ? govAgencyInfo.beneficiarySectionNo : "000";
                model.SequenceNumber = !string.IsNullOrEmpty(govAgencyInfo.beneficiarySerialNo) ? govAgencyInfo.beneficiarySerialNo : "000";
                model.NumOfSubDepartments = !string.IsNullOrEmpty(govAgencyInfo.managementNoForBeneficiary) ? govAgencyInfo.managementNoForBeneficiary : "000";
                model.NumOfSubSections = !string.IsNullOrEmpty(govAgencyInfo.sectionNoForBeneficiary) ? govAgencyInfo.sectionNoForBeneficiary : "000";
            }
            return model;
        }

        #endregion

        #region Favourit Supplier Tenders

        public async Task<bool> DeleteTenderFromSupplierTendersFavouriteList(int tenderId, string cr)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            var favouriteTender = await _tenderQueries.FindFavouriteSupplierTenderByTenderId(tenderId, cr);
            Check.ArgumentNotNull(nameof(favouriteTender), favouriteTender);
            favouriteTender.DeActive();
            await _tenderCommands.UpdateTenderFavouriteListAsync(favouriteTender);
            return true;
        }

        public async Task<bool> DeleteAttachmentAsync(string referenceId)
        {
            TenderAttachment att = await _tenderQueries.FindTenderAttachementByReferenceId(referenceId);
            bool check = false;
            if (att != null && (att.Tender.TenderStatusId == (int)Enums.TenderStatus.Established || att.Tender.TenderStatusId == (int)Enums.TenderStatus.UnderEstablishing))
            {
                att.DeActive();
                await _tenderCommands.UpdateAttachmentAsync(att);
                check = true;
            }
            return check;
        }


        public async Task DeleteTenderAttachmentChangesAsync(string referenceId)
        {
            TenderAttachmentChanges att = await _tenderQueries.FindTenderAttachementChangesByReferenceId(referenceId);
            if (att != null)
            {
                att.DeActive();
                await _tenderCommands.UpdateAttachmentChangeAsync(att);
            }
        }
        public async Task DeleteQualificationAttachments(string referenceId)
        {
            if (!string.IsNullOrEmpty(referenceId))
            {
                string[] lst = referenceId.Split(',');
                foreach (var item in lst)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (item.Contains("/GetFile?id="))
                            referenceId = item.Split("/GetFile?id=")[item.Split("/GetFile?id=").Length - 1];
                        else
                            referenceId = item;
                    }
                }
            }
            SupplierPreQualificationAttachment att = await _tenderQueries.GetQualificationAttachmentsByReferenceId(referenceId);
            if (att != null)
            {
                att.DeActive();
                _genericCommandRepository.Update(att);
            }
        }

        public async Task<bool> AddTenderToSupplierTendersFavouriteList(int tenderId, string cr)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.FindFavouriteSupplierTenderByTenderId(tenderId);
            Check.ArgumentNotNull(nameof(tender), tender);
            var checkBlockList = await _blockAppService.CheckifSupplierBlockedByCrNo(cr);
            if (checkBlockList)
                throw new BusinessRuleException(Resources.BlockResources.Messages.CrBlocked);
            FavouriteSupplierTender favouriteTender;
            if (tender.FavouriteSupplierTenders != null && tender.FavouriteSupplierTenders.Any(t => string.Equals(t.SupplierCRNumber, cr) && t.TenderId == tenderId))
            {
                favouriteTender = tender.FavouriteSupplierTenders.FirstOrDefault(t => string.Equals(t.SupplierCRNumber, cr) && t.TenderId == tenderId);
                favouriteTender.SetActive();
                await _tenderCommands.UpdateTenderFavouriteListAsync(favouriteTender);
                return true;
            }
            FavouriteSupplierTender favouriteSupplierTender = new FavouriteSupplierTender(tenderId, cr);
            await _tenderCommands.CreateTenderFavouriteAsync(favouriteSupplierTender);
            return true;
        }

        public async Task<QueryResult<TenderInvitationDetailsModel>> GetSupplierFavouritTendersList(TenderSearchCriteria tenderSearchCriteria)
        {
            tenderSearchCriteria.OnlyGetFavouriteTenders = true;
            List<SupplierAgencyBlockModel> supplierblocks = await _blockAppService.GetAllSupplierBlocks(null, new List<string> { tenderSearchCriteria.cr });
            var tenders = await _tenderQueries.GetAllSupplierTenders(tenderSearchCriteria, supplierblocks);
            return tenders;
        }

        public async Task<QueryResult<TenderInformationModel>> GetAllTendersHasEnquiry(TenderSearchCriteria tenderSearchCriteria)
        {
            var tenders = await _tenderQueries.GetAllTendersHasEnquiry(tenderSearchCriteria);
            return tenders;
        }

        #endregion

        #region Reports

        public async Task<TenderDetialsReportModel> OpenTenderDetailsReport(int tenderId)
        {
            var result = await _tenderQueries.OpenTenderDetailsReport(tenderId);
            if (result == null)
                throw new UnHandledAccessException();
            return result;
        }
        public async Task<TenderDetialsReportModel> OpenTenderDetailsReportForVisitor(int tenderId)
        {
            var result = await _tenderQueries.OpenTenderDetailsReportForVisitor(tenderId);
            if (result == null)
                throw new UnHandledAccessException();
            return result;
        }
        public async Task<QueryResult<TenderMovement>> GetTenderMovementsByTenderId(SimpleTenderSearchCriteria criteria)
        {
            return await _tenderQueries.GetTenderMovementsFromHistoryByTenderId(criteria);
        }

        public async Task<AwardingReportModel> AwardingReport(int tenderId)
        {
            var result = await _tenderQueries.AwardingReport(tenderId);
            return result;
        }

        public async Task<OfferReportModel> OffersReport(int tenderId)
        {
            var result = await _tenderQueries.OffersReport(tenderId);
            return result;
        }

        public async Task<PrintTenderReceiptReportModel> PrintTenderReceiptReport(int tenderId, string CommericalRegisterNo)
        {
            var result = await _tenderQueries.PrintTenderReceiptReport(tenderId, CommericalRegisterNo);
            var supplier = await _iDMQueries.FindSupplierByCrAsync(result.CommercialNumber);
            result.SupplierName = supplier.SelectedCrName;
            result.Email = supplier.SupplierUserProfiles.FirstOrDefault().UserProfile.Email;
            result.Phone = supplier.SupplierUserProfiles.FirstOrDefault().UserProfile.Mobile;
            return result;
        }

        public async Task<CountAndCloseAppliedOffersModel> CountAndCloseAppliedOffers(int tenderId)
        {
            var result = await _tenderQueries.CountAndCloseAppliedOffers(tenderId);
            return result;
        }

        public async Task JoinCommitteeToAllTenders(CommitteeTenderModel committeeTenderModel)
        {
            LinkTendersToCommitteeSearchCriteriaModel criteria = new LinkTendersToCommitteeSearchCriteriaModel
            {
                AgencyCode = committeeTenderModel.AgencyCode,
                CommitteeTypeId = committeeTenderModel.CommitteeTypeId
            };
            List<Tender> allApprovedTenders = await _tenderQueries.GetTendersToJoinCommittees(criteria);
            foreach (var tender in allApprovedTenders)
            {
                tender.UpdateTenderCommittee(tender.TenderStatusId, committeeTenderModel.CommitteeTypeId, committeeTenderModel.CommitteeId, _httpContextAccessor.HttpContext.User.UserId());
            }
            await _tenderCommands.UpdateRangeAsync(allApprovedTenders);
        }

        public async Task JoinCommitteeToBranchTenders(CommitteeTenderModel committeeTenderModel)
        {
            LinkTendersToCommitteeSearchCriteriaModel criteria = new LinkTendersToCommitteeSearchCriteriaModel
            {
                AgencyCode = committeeTenderModel.AgencyCode,
                CommitteeTypeId = committeeTenderModel.CommitteeTypeId,
                BranchId = committeeTenderModel.BranchId
            };
            var allApprovedTenders = await _tenderQueries.GetTendersToJoinCommittees(criteria);
            foreach (var tender in allApprovedTenders)
            {
                tender.UpdateTenderCommittee(tender.TenderStatusId, committeeTenderModel.CommitteeTypeId, committeeTenderModel.CommitteeId, _httpContextAccessor.HttpContext.User.UserId());
            }
            await _tenderCommands.UpdateRangeAsync(allApprovedTenders);
        }

        public async Task JoinCommitteeToTenders(CommitteeTenderModel committeeTenderModel)
        {
            foreach (var id in committeeTenderModel.TenderIds)
            {
                var tender = await _tenderQueries.FindTenderByTenderId(id);
                tender.UpdateTenderCommittee(tender.TenderStatusId, committeeTenderModel.CommitteeTypeId, committeeTenderModel.CommitteeId, _httpContextAccessor.HttpContext.User.UserId());
                await _tenderCommands.UpdateAsync(tender);
            }
        }

        public async Task CancelJoinCommitteeToTender(CommitteeTenderModel committeeTenderModel)
        {
            Check.ArgumentNotNullOrEmpty(nameof(committeeTenderModel.TenderId), committeeTenderModel.TenderId.ToString());
            var tender = await _tenderQueries.FindTenderByTenderId(committeeTenderModel.TenderId);
            var committee = await _committeeQueries.GetById(committeeTenderModel.CommitteeId);
            if (committee.CommitteeTypeId == (int)Enums.CommitteeType.OpenOfferCommittee)
            {
                tender.ResetOpenCommittee(committeeTenderModel.CommitteeId);
            }
            else if (committee.CommitteeTypeId == (int)Enums.CommitteeType.CheckOfferCommittee)
            {
                tender.ResetCheckCommittee(committeeTenderModel.CommitteeId);
            }
            else if (committee.CommitteeTypeId == (int)Enums.CommitteeType.TechincalCommittee)
            {
                tender.ResetTechnicalCommittee(committeeTenderModel.CommitteeId);
            }
            await _tenderCommands.UpdateAsync(tender);
        }

        #region Edit

        public async Task<EditeCommitteesModel> GetTenderCommitteesByTenderId(int tenderId, int branchId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            EditeCommitteesModel editeCommitteesModel = await _tenderQueries.GetTenderCommitteesByTenderId(tenderId, branchId);
            if (editeCommitteesModel == null)
            {
                throw new UnHandledAccessException();
            }
            return editeCommitteesModel;
        }

        public async Task<Tender> EditCommittees(EditeCommitteesModel committeeModel)
        {
            var tender = await _tenderQueries.GetTenderByIdWithoutAnyIncluds(committeeModel.TenderId);
            tender.UpdateCommittees(tender.TenderStatusId, committeeModel.OffersOpeningCommitteeId, committeeModel.OffersCheckingCommitteeId, committeeModel.TechnicalOrganizationId, committeeModel.VROCommitteeId, committeeModel.DirectPurchaseCommitteeId, committeeModel.DirectPurchaseCommitteeMemberId, _httpContextAccessor.HttpContext.User.UserId());
            await _tenderCommands.UpdateAsync(tender);
            return tender;
        }

        public async Task<Tender> GetTenderSamplesDeliveryAddress(int tenderId, int branchId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(branchId), branchId.ToString());
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.GetTenderByIdAndBranch(tenderId, branchId);
            if (tender == null || tender.BranchId != branchId)
            {
                throw new UnHandledAccessException();
            }
            return tender;
        }

        public async Task<Tender> EditSamplesDeliveryAddress(int tenderId, string address)
        {
            var tender = await _tenderQueries.GetTenderByIdWithoutAnyIncluds(tenderId);
            tender.UpdateSamplesDeliveryAddress(address);
            tender.AddActionHistory(tender.TenderStatusId, TenderActions.UpdateSamplesDeliveryAddress, "", _httpContextAccessor.HttpContext.User.UserId());
            await _tenderCommands.UpdateAsync(tender);
            return tender;
        }

        public async Task<Tender> ConvertTenderInvitationToPublic(int tenderId)
        {
            Tender tender = await _tenderQueries.FindTenderByTenderId(tenderId);
            tender.UpdateTenderInvitationType();
            tender.AddActionHistory(tender.TenderStatusId, TenderActions.ConvertTenderInvitationToPublic, "", _httpContextAccessor.HttpContext.User.UserId());
            await _tenderCommands.UpdateAsync(tender);
            return tender;
        }

        public async Task<Tender> GetTenderForEditAreas(int tenderId, int branchId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tender = await _tenderQueries.GetTenderWithAreas(tenderId, branchId);
            if (tender == null)
            {
                throw new UnHandledAccessException();
            }
            return tender;
        }

        public async Task<Tender> EditAreas(TenderAreasModel areasModel)
        {
            var tender = await _tenderQueries.FindTenderWithAreasById(areasModel.TenderId);
            if (tender.TenderStatusId != (int)Enums.TenderStatus.Approved && tender.TenderStatusId != (int)Enums.TenderStatus.OffersOppening)
            {
                throw new UnHandledAccessException();
            }
            if (tender.InsideKSA.HasValue && tender.InsideKSA.Value)
            {
                tender.UpdateAreas(areasModel.TenderAreaIDs, tender.InsideKSA);
            }
            if (tender.InsideKSA.HasValue && !tender.InsideKSA.Value)
            {
                tender.UpdateCountries(areasModel.TenderCountryIDs, tender.InsideKSA);
            }
            tender.AddActionHistory(tender.TenderStatusId, TenderActions.UpdateTenderRegions, "", _httpContextAccessor.HttpContext.User.UserId());
            await _tenderCommands.UpdateAsync(tender);
            return tender;
        }

        public async Task<Tender> EditCountries(int tenderId, List<int> countriesIds)
        {
            var tender = await _tenderQueries.FindTenderWithAreasById(tenderId);
            if (tender.TenderStatusId != (int)Enums.TenderStatus.Approved && tender.TenderStatusId != (int)Enums.TenderStatus.OffersOppening)
            {
                throw new UnHandledAccessException();
            }
            tender.UpdateCountries(countriesIds, tender.InsideKSA);
            tender.AddActionHistory(tender.TenderStatusId, TenderActions.UpdateTenderRegions, "", _httpContextAccessor.HttpContext.User.UserId());
            await _tenderCommands.UpdateAsync(tender);
            return tender;
        }

        #endregion

        #endregion


        public Task<TenderNewsModel> GetTenderNewsByTenderId(int tenderId)
        {
            return _tenderQueries.GetTenderNewsByTenderId(tenderId);
        }

        #region TenderSyncWithOldMonafsat

        public async Task<SyncDataWithOldMonafasat> SaveSyncInformation(int tenderId, string requestContent, bool tenderUpdateStatus)
        {
            var sync = new SyncDataWithOldMonafasat
            (
                tenderId: tenderId,
                requestInformation: requestContent,
                isSendSuccessfully: tenderUpdateStatus
            );
            var result = await _tenderCommands.CreateSyncDataWithOldMonafasat(sync);
            return result;
        }

        #endregion TenderSyncWithOldMonafsat

        #region Invitatins

        public async Task SubmitTenderInvitationsStep(int tenderId)
        {
            Tender tender = await _tenderQueries.GetTenderByIdForInvitations(tenderId);
            Check.ArgumentNotNull(nameof(tender), tender);
            tender.UpdateTenderStatus(Enums.TenderStatus.Established, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.CreateTender);
            await _tenderCommands.UpdateAsync(tender);
        }

        private async Task CheckSupplierForInvitation(List<CrModel> suppliers)
        {
            var AgencyCode = _httpContextAccessor.HttpContext.User.UserAgency();
            foreach (var item in suppliers)
            {
                if (string.IsNullOrEmpty(item.CrNumber) || AgencyCode == item.CrNumber)
                {
                    continue;
                }
                var checkBlockList = await _blockAppService.CheckifSupplierBlockedByCrNo(item.CrNumber, AgencyCode);
                if (checkBlockList)
                {
                    continue;
                }
                var supplier = await _supplierQueries.FindSupplierByCRNumber(item.CrNumber);
                if (supplier == null)
                {
                    var supplierobj = new Supplier(item.CrNumber, item.CrName, await _notificationAppService.GetDefaultSettingByCr());
                    await _supplierCommands.CreateSupplierAsync(supplierobj);
                }
            }
        }

        public async Task SendInvitationsInTenderCreation(InvitationsInCreateTenderModel invitations)
        {
            await CheckSupplierForInvitation(invitations.InvitedSuppliers);
            Tender tender = await _tenderQueries.GetTenderByIdForInvitations(invitations.TenderId);
            Check.ArgumentNotNull(nameof(tender), tender);
            tender.AddSupplierInvitationWhileCreation(invitations.InvitedSuppliers.Select(s => s.CrNumber).ToList(), _httpContextAccessor.HttpContext.User.UserId());
            await _tenderCommands.UpdateAsync(tender);
        }

        #endregion Invitations

        #region Second-Stage-Tender

        public async Task<TenderDatesModel> CreateSecondStageBasicAsync(SecondStageBasicData secondStageBasicModel)
        {
            List<AgencyBudgetNumber> agencyBudgetNumber = _mapper.Map<List<AgencyBudgetNumber>>(secondStageBasicModel.AgencyBudgetNumber);
            Check.ArgumentNotNull(nameof(secondStageBasicModel), secondStageBasicModel);
            Tender tender;
            Tender firstStageTender = await _tenderQueries.GetTenderByIdAndBranch(secondStageBasicModel.TenderFirstStageId.Value, _httpContextAccessor.HttpContext.User.UserBranch());
            if (secondStageBasicModel.TenderFirstStageId != 0 && firstStageTender.TenderTypeId == (int)Enums.TenderType.FirstStageTender)
            {
                bool HasAcceptedOffers = firstStageTender.Offers.Any(o => o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer && o.IsActive == true);
                if (!HasAcceptedOffers)
                {
                    throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
                }
                Tender RTender = await _tenderQueries.GetTendersRelatedToFirstStage(secondStageBasicModel.TenderFirstStageId.Value, _httpContextAccessor.HttpContext.User.UserBranch());
                if (RTender != null)
                {
                    throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
                }
                var suppliers = await _tenderQueries.GetAcceptedSupplierInFirstStageTender(firstStageTender.TenderId);
                secondStageBasicModel.TenderName = firstStageTender.TenderName;
                secondStageBasicModel.InitialGuaranteeAddress = " ";
                secondStageBasicModel.TenderNumber = firstStageTender.TenderNumber;
                secondStageBasicModel.QuantityTableVersionId = firstStageTender.QuantityTableVersionId;
                secondStageBasicModel.TenderTypeId = (int)Enums.TenderType.SecondStageTender;
                tender = new Tender(secondStageBasicModel.TenderTypeId, secondStageBasicModel.TenderName, secondStageBasicModel.TenderNumber,
                secondStageBasicModel.Purpose, secondStageBasicModel.TechnicalOrganizationId, secondStageBasicModel.OffersCheckingCommitteeId, secondStageBasicModel.OffersOpeningCommitteeId,
                secondStageBasicModel.OfferPresentationWayId.Value, secondStageBasicModel.TenderFirstStageId, secondStageBasicModel.AgencyCode, _httpContextAccessor.HttpContext.User.UserBranch(), secondStageBasicModel.EstimatedValue, secondStageBasicModel.QuantityTableVersionId, secondStageBasicModel.InitialGuaranteeAddress, secondStageBasicModel.InitialGuaranteePercentage, agencyBudgetNumber);
                tender.AddInvitationForAcceptedSuppliersInFirstStage(suppliers);
                tender.SetReferenceNumber(await _tenderCommands.UpdateReferenceNumber());
                var activities = firstStageTender.TenderActivities.Select(a => a.ActivityId).ToList();
                var countries = firstStageTender.TenderCountries.Select(a => a.CountryId).ToList();
                var areas = firstStageTender.TenderAreas.Select(a => a.AreaId).ToList();
                var constructions = firstStageTender.TenderConstructionWorks.Select(a => a.ConstructionWorkId).ToList();
                var maintenanceWorks = firstStageTender.TenderMaintenanceRunnigWorks.Select(a => a.MaintenanceRunningWorkId).ToList();
                tender.UpdateTenderRelations(areas, activities, constructions, maintenanceWorks, firstStageTender.InsideKSA, firstStageTender.Details, firstStageTender.ActivityDescription, countries, false, firstStageTender.TemplateYears);
                tender.UpdateSecondStageTemplates(firstStageTender.TenderConditionsTemplate);

                var activityCurrentVersion = await _tenderQueries.GetCurrentActivityVersion();
                if (activityCurrentVersion != null)
                {
                    tender.AddActivityVersion(activityCurrentVersion.Id);
                }

                await _tenderCommands.CreateAsync(tender);
            }
            else
            {
                tender = await _tenderQueries.FindTenderWithInvitationsByTenderId(secondStageBasicModel.TenderId);
                tender.AgencyBudgetNumbers.ForEach(x => x.Delete());
                tender.UpdateSecondStageBasicData(secondStageBasicModel.Purpose,
                    secondStageBasicModel.TechnicalOrganizationId, secondStageBasicModel.OffersOpeningCommitteeId,
                    secondStageBasicModel.OfferPresentationWayId.Value, _httpContextAccessor.HttpContext.User.UserId(), secondStageBasicModel.EstimatedValue, agencyBudgetNumber);
                await _tenderCommands.UpdateAsync(tender);
            }
            TenderDatesModel tenderDates = new TenderDatesModel
            {
                TenderName = tender.ReferenceNumber,
                TenderNumber = tender.TenderNumber,
                OffersOpeningAddressId = tender.OffersOpeningAddressId,
                TenderId = tender.TenderId,
                TenderIdString = Util.Encrypt(tender.TenderId),
                TenderStatusId = tender.TenderStatusId,
                TenderTypeId = tender.TenderTypeId,
                AgencyCode = tender.AgencyCode,
                BranchId = tender.BranchId,
                ReferenceNumber = tender.ReferenceNumber,
                LastEnqueriesDate = tender.LastEnqueriesDate,
                LastOfferPresentationDate = tender.LastOfferPresentationDate,
                LastOfferPresentationTime = tender.LastOfferPresentationDate.HasValue ? tender.LastOfferPresentationDate.Value.ToString("hh:mm tt") : "",
                OffersOpeningDate = tender.OffersOpeningDate,
                OffersOpeningTime = tender.OffersOpeningDate.HasValue ? tender.OffersOpeningDate.Value.ToString("hh:mm tt") : "",
                OffersCheckingDate = tender.OffersCheckingDate,
                OffersCheckingTime = tender.OffersCheckingDate.HasValue ? tender.OffersCheckingDate.Value.ToString("hh:mm tt") : ""
            };
            return tenderDates;
        }

        #endregion Second-Stage-Tender

        #region Bidding Round

        public async Task<BiddingTenderDetailsModel> FindBiddingRoundDetailsByBiddingRoundId(string tenderStringId, string cR)
        {
            var TenderId = Util.Decrypt(tenderStringId);
            var result = await _tenderQueries.FindBiddingRoundOffersByTenderId(TenderId);
            var indexWords = new string[] { "الاولى", "الثانية", "الثالثة", "الرابعة", "الخامسة", "السادسة", "السابعة", "الثامنة", "التاسعة", "العاشرة" };
            var indexWord = result.BiddingRounds.Where(a => a.IsActive == true).Count() <= 10 ? indexWords[result.BiddingRounds.Where(a => a.IsActive == true).Count() - 1] : "الاخيرة";
            var biddingHasBeenEndded = false;
            decimal minOfferValue = 0;
            _tenderDomainService.IsValidToGetTenderBiddings(result, cR);
            var latestRound = result.BiddingRounds.Where(a => a.IsActive == true).OrderByDescending(b => b.BiddingRoundId).FirstOrDefault(r => r.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.Started && r.StartDate <= DateTime.Now && DateTime.Now < r.EndDate);
            var allRounds = result.BiddingRounds.Where(a => a.IsActive == true).OrderByDescending(b => b.BiddingRoundId);
            decimal lastLeastBiddingValue = 0;
            if (result.BiddingRounds.Where(a => a.IsActive == true).OrderByDescending(b => b.BiddingRoundId).FirstOrDefault()?.BiddingRoundOffers.Count > 0)
                minOfferValue = result.BiddingRounds.Where(a => a.IsActive == true).OrderByDescending(b => b.BiddingRoundId).FirstOrDefault().BiddingRoundOffers.Min(o => o.OfferValue);
            if (result.BiddingRounds.Where(a => a.IsActive == true && a.BiddingRoundOffers.Any(b => b.IsActive == true)).OrderByDescending(b => b.BiddingRoundId).FirstOrDefault(r => r.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.Stopped || r.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.Approved)?.BiddingRoundOffers.Count > 0)
                lastLeastBiddingValue = result.BiddingRounds.Where(a => a.IsActive == true && a.BiddingRoundOffers.Any(b => b.IsActive == true)).OrderByDescending(b => b.BiddingRoundId).FirstOrDefault(r => r.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.Stopped || r.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.Approved).BiddingRoundOffers.Min(o => o.OfferValue);
            if (latestRound == null)
            {
                biddingHasBeenEndded = true;
                latestRound = result.BiddingRounds.Where(a => a.IsActive == true).OrderByDescending(b => b.BiddingRoundId).FirstOrDefault();
            }
            return new BiddingTenderDetailsModel
            {
                ExectutionPlace = result.InsideKSA == true ? string.Join(",", result.TenderAreas.Select(y => y.Area.NameAr).ToList()) : string.Join(",", result.TenderCountries.Select(y => y.Country.NameAr).ToList()),
                TenderId = result.TenderId,
                TenderName = result.TenderName,
                TenderStatusId = result.TenderStatusId,
                TenderRefrenceNumber = result.ReferenceNumber,
                TenderIdString = Util.Encrypt(result.TenderId),
                CanEndBidding = biddingHasBeenEndded &&
                                latestRound.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.Stopped &&
                                minOfferValue <= result.EstimatedValue &&
                                result.BiddingRounds.Where(a => a.IsActive == true).OrderByDescending(b => b.BiddingRoundId).FirstOrDefault().BiddingRoundOffers.Count > 0 &&
                                result.BiddingRounds.Where(a => a.IsActive == true).OrderByDescending(b => b.BiddingRoundId).FirstOrDefault().BiddingRoundOffers.Where(o => o.OfferValue == minOfferValue).Count() == 1,
                CanStartNewRound = biddingHasBeenEndded &&
                                latestRound.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.Stopped &&
                                (minOfferValue > result.EstimatedValue ||
                                result.BiddingRounds.Where(a => a.IsActive == true).OrderByDescending(b => b.BiddingRoundId).FirstOrDefault().BiddingRoundOffers.Count == 0 ||
                                result.BiddingRounds.Where(a => a.IsActive == true).OrderByDescending(b => b.BiddingRoundId).FirstOrDefault().BiddingRoundOffers.Where(o => o.OfferValue == minOfferValue).Count() > 1),
                LastLeastBiddingValue = lastLeastBiddingValue,
                HasPreviousRounds = latestRound != null,
                BiddingRoundIdString = Util.Encrypt(latestRound.BiddingRoundId),
                BiddingStartDate = latestRound.StartDate.ToString("dd/MM/yyyy"),
                StartDate = latestRound.StartDate,
                BiddingRoundStartTime = latestRound.StartDate.ToString("HH:mm:ss"),
                BiddingRoundEndTime = latestRound.EndDate.ToString("HH:mm:ss"),
                biddingRoundDuration = (Math.Max(Convert.ToInt32(((latestRound.EndDate.TimeOfDay - latestRound.StartDate.TimeOfDay).TotalMilliseconds / (1000 * 60 * 60)) % 24), 0) +
                Resources.TenderResources.DisplayInputs.Hour + " " +
                Resources.TenderResources.DisplayInputs.And + " " +
                Math.Max(Convert.ToInt32(((latestRound.EndDate.TimeOfDay - latestRound.StartDate.TimeOfDay).TotalMilliseconds / (1000 * 60)) % 60), 0) +
                Resources.TenderResources.DisplayInputs.Minute),
                EndDate = latestRound.EndDate,
                BiddingRoundDaysToEnd = !biddingHasBeenEndded ? (int)(latestRound.EndDate - DateTime.Now).TotalDays : 0,
                BiddingRoundHoursToEnd = !biddingHasBeenEndded ? (int)(latestRound.EndDate - DateTime.Now.AddDays((int)(latestRound.EndDate - DateTime.Now).TotalDays)).TotalHours : 0,
                BiddingRoundMinutesToEnd = !biddingHasBeenEndded ? (int)(latestRound.EndDate - DateTime.Now.AddHours((int)(latestRound.EndDate - DateTime.Now).TotalHours)).TotalMinutes : 0,
                ActiveSupplierCount = allRounds.SelectMany(r => r.BiddingRoundOffers).GroupBy(a => a.Offer.CommericalRegisterNo).Count(),
                OfferPlaceInBidding = latestRound.BiddingRoundOffers.OrderBy(i => i.OfferValue).ToList().FindIndex(i => i.Offer.CommericalRegisterNo == _httpContextAccessor.HttpContext.User.SupplierNumber()) + 1,
                RoundIndexLabel = "الجولة " + indexWord,
                EstimateValue = result.EstimatedValue ?? 0
            };
        }

        public async Task<BiddingTenderDetailsModel> FindBiddingRoundOffersByBiddingRoundId(string tenderStringId, string cR)
        {
            var TenderId = Util.Decrypt(tenderStringId);
            var result = await _tenderQueries.FindBiddingRoundOffersByTenderId(TenderId);
            var indexWords = new string[] { "الاولى", "الثانية", "الثالثة", "الرابعة", "الخامسة", "السادسة", "السابعة", "الثامنة", "التاسعة", "العاشرة" };
            var indexWord = result.BiddingRounds.Where(a => a.IsActive == true).Count() <= 10 ? indexWords[result.BiddingRounds.Where(a => a.IsActive == true).Count() - 1] : "الاخيرة";
            var biddingHasBeenEndded = false;
            decimal minOfferValue = 0;
            _tenderDomainService.IsValidToGetTenderBiddings(result, cR);
            var latestRound = result.BiddingRounds.Where(a => a.IsActive == true).OrderByDescending(b => b.BiddingRoundId).FirstOrDefault(r => r.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.Started &&
                                                                                                                    r.StartDate <= DateTime.Now && DateTime.Now < r.EndDate);
            var allRounds = result.BiddingRounds.Where(a => a.IsActive == true).OrderByDescending(b => b.BiddingRoundId);
            decimal lastLeastBiddingValue = 0;
            if (result.BiddingRounds.Where(a => a.IsActive == true).OrderByDescending(b => b.BiddingRoundId).FirstOrDefault()?.BiddingRoundOffers.Count > 0)
                minOfferValue = result.BiddingRounds.Where(a => a.IsActive == true).OrderByDescending(b => b.BiddingRoundId).FirstOrDefault().BiddingRoundOffers.Min(o => o.OfferValue);
            if (result.BiddingRounds.Where(a => a.IsActive == true && a.BiddingRoundOffers.Any(b => b.IsActive == true)).OrderByDescending(b => b.BiddingRoundId).FirstOrDefault(r => r.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.Stopped || r.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.Approved)?.BiddingRoundOffers.Count > 0)
                lastLeastBiddingValue = result.BiddingRounds.Where(a => a.IsActive == true && a.BiddingRoundOffers.Any(b => b.IsActive == true)).OrderByDescending(b => b.BiddingRoundId).FirstOrDefault(r => r.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.Stopped || r.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.Approved).BiddingRoundOffers.Min(o => o.OfferValue);
            if (latestRound == null)
            {
                biddingHasBeenEndded = true;
                latestRound = result.BiddingRounds.Where(a => a.IsActive == true).OrderByDescending(b => b.BiddingRoundId).FirstOrDefault();
            }
            return new BiddingTenderDetailsModel
            {
                ExectutionPlace = result.InsideKSA == true ? string.Join(",", result.TenderAreas.Select(y => y.Area.NameAr).ToList()) : string.Join(",", result.TenderCountries.Select(y => y.Country.NameAr).ToList()),
                TenderId = result.TenderId,
                TenderName = result.TenderName,
                TenderStatusId = result.TenderStatusId,
                TenderRefrenceNumber = result.ReferenceNumber,
                TenderIdString = Util.Encrypt(result.TenderId),
                CanEndBidding = biddingHasBeenEndded &&
                                latestRound.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.Stopped &&
                                minOfferValue <= result.EstimatedValue &&
                                result.BiddingRounds.Where(a => a.IsActive == true).OrderByDescending(b => b.BiddingRoundId).FirstOrDefault().BiddingRoundOffers.Count > 0 &&
                                result.BiddingRounds.Where(a => a.IsActive == true).OrderByDescending(b => b.BiddingRoundId).FirstOrDefault().BiddingRoundOffers.Where(o => o.OfferValue == minOfferValue).Count() == 1,
                CanStartNewRound = biddingHasBeenEndded &&
                                latestRound.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.Stopped &&
                                (minOfferValue > result.EstimatedValue ||
                                result.BiddingRounds.Where(a => a.IsActive == true).OrderByDescending(b => b.BiddingRoundId).FirstOrDefault().BiddingRoundOffers.Count == 0 ||
                                result.BiddingRounds.Where(a => a.IsActive == true).OrderByDescending(b => b.BiddingRoundId).FirstOrDefault().BiddingRoundOffers.Where(o => o.OfferValue == minOfferValue).Count() > 1),
                LastLeastBiddingValue = lastLeastBiddingValue,
                HasPreviousRounds = latestRound != null,
                BiddingRoundIdString = Util.Encrypt(latestRound.BiddingRoundId),
                BiddingStartDate = latestRound.StartDate.ToString("dd/MM/yyyy"),
                BiddingRoundStartTime = latestRound.StartDate.ToString("HH:mm:ss"),
                BiddingRoundEndTime = latestRound.EndDate.ToString("HH:mm:ss"),
                biddingRoundDuration = (Math.Max(Convert.ToInt32(((latestRound.EndDate.TimeOfDay - latestRound.StartDate.TimeOfDay).TotalMilliseconds / (1000 * 60 * 60)) % 24), 0) +
                Resources.TenderResources.DisplayInputs.Hour + " " +
                Resources.TenderResources.DisplayInputs.And + " " +
                Math.Max(Convert.ToInt32(((latestRound.EndDate.TimeOfDay - latestRound.StartDate.TimeOfDay).TotalMilliseconds / (1000 * 60)) % 60), 0) +
                Resources.TenderResources.DisplayInputs.Minute),
                BiddingRoundDaysToEnd = !biddingHasBeenEndded ? (int)(latestRound.EndDate - DateTime.Now).TotalDays : 0,
                BiddingRoundHoursToEnd = !biddingHasBeenEndded ? (int)(latestRound.EndDate - DateTime.Now.AddDays((int)(latestRound.EndDate - DateTime.Now).TotalDays)).TotalHours : 0,
                BiddingRoundMinutesToEnd = !biddingHasBeenEndded ? (int)(latestRound.EndDate - DateTime.Now.AddHours((int)(latestRound.EndDate - DateTime.Now).TotalHours)).TotalMinutes : 0,
                BiddingRoundOffers = latestRound.BiddingRoundOffers.OrderByDescending(i => i.BiddingRoundOfferId).Select(o => new BiddingRoundOfferModel
                {
                    OfferId = o.OfferId,
                    OfferValue = o.OfferValue,
                    CommercailName = o.Offer.Supplier.SelectedCrName,
                    CommercialNumber = o.Offer.Supplier.SelectedCr
                }).ToList(),
                ActiveSupplierCount = allRounds.SelectMany(r => r.BiddingRoundOffers).GroupBy(a => a.Offer.CommericalRegisterNo).Count(),
                OfferPlaceInBidding = latestRound.BiddingRoundOffers.OrderBy(i => i.OfferValue).ToList().FindIndex(i => i.Offer.CommericalRegisterNo == _httpContextAccessor.HttpContext.User.SupplierNumber()) + 1,
                RoundIndexLabel = "الجولة " + indexWord,
                EstimateValue = result.EstimatedValue ?? 0
            };
        }

        public async Task EndTenderPedding(string tenderIdString, string biddingRoundIdString, string verificationCode)
        {
            int tenderId = Util.Decrypt(tenderIdString);
            bool check = await _verification.CheckForVerificationCode(tenderId, verificationCode, (int)Enums.VerificationType.Tender);
            int biddingRoundId = Util.Decrypt(biddingRoundIdString);
            var tender = await _tenderQueries.FindBiddingRoundOffersByTenderIdForEndingBiddingRound(tenderId);
            var biddingRound = tender.BiddingRounds.FirstOrDefault(r => r.BiddingRoundId == biddingRoundId && r.IsActive == true);
            _tenderDomainService.IsValidToEndBiddingRound(biddingRound);
            decimal biddingRoundLcl;
            int offersCount = biddingRound.Tender.Offers.Count;
            for (int i = 0; i < offersCount; i++)
            {
                biddingRoundLcl = tender.BiddingRounds.Any(a => a.BiddingRoundOffers.Any(b => b.OfferId == tender.Offers[i].OfferId && b.IsActive == true) && a.IsActive == true) ?
                tender.BiddingRounds.Where(a => a.BiddingRoundOffers.Any(b => b.OfferId == tender.Offers[i].OfferId && b.IsActive == true) && a.IsActive == true).
                OrderByDescending(a => a.BiddingRoundId).FirstOrDefault()
                .BiddingRoundOffers.Where(a => a.IsActive == true).OrderByDescending(o => o.BiddingRoundOfferId).FirstOrDefault(b => b.OfferId == tender.Offers[i].OfferId).OfferValue : 0;
                biddingRound.Tender.Offers[i].UpdateFinalPriceAfterDiscount(biddingRoundLcl);
            }
            biddingRound.UpdateStatus((int)Enums.BiddingRoundStatus.Approved);
            tender.UpdateTenderStatus(Enums.TenderStatus.BiddingApproved, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.ApproveBidding);
            _genericCommandRepository.Update(biddingRound);
            await _tenderCommands.UpdateAsync(tender);
        }

        public async Task StartNewRound(BiddingDateTimeViewModel biddingDateTimeViewModel)
        {
            int tenderId = Util.Decrypt(biddingDateTimeViewModel.TenderIdString);
            var tender = await _tenderQueries.FindTenderForAddingBiddingOfferByTenderId(tenderId);
            var offers = await _tenderQueries.FindTenderOffersForBiddingRound(tenderId);
            _tenderDomainService.IsValidToAddNewBiddingRound(tender, _httpContextAccessor.HttpContext.User.UserAgency(), biddingDateTimeViewModel);
            ConfirmStartNewRound(tender, biddingDateTimeViewModel.BiddingStartDateTime, biddingDateTimeViewModel.BiddingEndDateTime);
            List<string> suppliers = offers.Where(a => a.IsActive == true && a.OfferStatusId == (int)Enums.OfferStatus.Received && a.OfferTechnicalEvaluationStatusId == (int)OfferTechnicalEvaluationStatusId.IdenticalOffer).Select(e => e.Supplier.SelectedCr).ToList();
            NotificationArguments biddingNotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", tender.ReferenceNumber, tender.Purpose,
                        tender.LastEnqueriesDate?.ToString("dd/MM/yyyy hh:mm tt"), tender.LastOfferPresentationDate?.ToString("dd/MM/yyyy hh:mm tt"),
                        tender.OffersOpeningDate == null  ?Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt"),
                        tender.BiddingRounds.FirstOrDefault(a => a.IsActive == true && a.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.New)?.StartDate.ToString("dd/MM/yyyy hh:mm tt"),
                        tender.BiddingRounds.FirstOrDefault(a => a.IsActive == true && a.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.New)?.EndDate.ToString("dd/MM/yyyy hh:mm tt")
                    },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel approveBiddingTenderCheckingForSupplier = new MainNotificationTemplateModel(biddingNotificationArgument, $"Tender/TenderBiddingViewAsync?tenderIdString={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.AddNewRound, suppliers, approveBiddingTenderCheckingForSupplier);
            await _tenderCommands.UpdateAsync(tender);
        }

        public void ConfirmStartNewRound(Tender tender, DateTime startDateTime, DateTime endDateTime)
        {
            _tenderDomainService.IsValidToStartNewRouind(tender);
            BiddingRound biddingRound = new BiddingRound(startDateTime, endDateTime, (int)Enums.BiddingRoundStatus.New, tender.TenderId);
            var suppliers = tender.Offers.Where(o => o.IsActive == true && o.OfferTechnicalEvaluationStatusId == (int)OfferTechnicalEvaluationStatusId.IdenticalOffer)
                            .Select(o => o.Supplier).ToList();
            tender.AddBiddingRounds(biddingRound);
        }

        public async Task<QueryResult<NegotiationOnTenderModel>> GetAllTenderhasNegotiationbySearchCretriaForUnitUsers(NegotiationOnTenderSearchCriteriaModel criteria)
        {
            return await _tenderQueries.GetAllTenderhasNegotiationbySearchCretriaForUnitUsers(criteria);
        }

        #endregion Bidding Round 

        #region Unregistered-Suppliers-Invitations

        public async Task SendInvitationsForUnRegisteredSupplier(UnRegisteredSuppliersInvitationsModel invitationsModel)
        {
            var AgencyCode = _httpContextAccessor.HttpContext.User.UserAgency();
            var RSupplier = await _idmAppService.GetSupplierInfoByCR(invitationsModel.CrNumber);
            if (RSupplier != null)
            {
                throw new BusinessRuleException("  يجب دعوة المورد من الموردين المسجلين ");
            }
            var checkBlockList = await _blockAppService.CheckifSupplierBlockedByCrNo(invitationsModel.CrNumber, AgencyCode);
            if (checkBlockList)
            {
                throw new BusinessRuleException("تم منع هذا المورد");
            }
            Tender tender = await _tenderQueries.FindTenderInvitationsWithById(Util.Decrypt(invitationsModel.TenderIdString));
            await CheckSupplierForInvitation(new List<CrModel> { new CrModel() { CrNumber = invitationsModel.CrNumber, CrName = invitationsModel.CrName } });

            bool result = await _tenderQueries.FindUnRegisteredSuppliersInvitationByTenderIDAndEmail(tender.TenderId, invitationsModel.Email, invitationsModel.CrNumber);
            if (result)
            {
                tender.AddInvitationsForUnRegisteredSupplier(invitationsModel.CrNumber, invitationsModel.InvitationTypeId, invitationsModel.Email, invitationsModel.Mobile, invitationsModel.InCreation ? InvitationStatus.ToBeSent : InvitationStatus.New, invitationsModel.Description);
                await _tenderCommands.UpdateAsync(tender);
            }
            if (!invitationsModel.InCreation)
            {
                if (!string.IsNullOrEmpty(invitationsModel.Email))
                {
                    await _notificationAppService.SendInvitationByEmail(new List<string> { invitationsModel.Email }, tender);
                }
                if (!string.IsNullOrEmpty(invitationsModel.Mobile))
                {
                    await _notificationAppService.SendInvitationBySms(new List<string> { invitationsModel.Mobile }, tender);
                }
            }
        }

        #endregion ConditionsBookletTemplates

        public async Task<ValidationReturndTemplate> ValidateQuantitiesData(Dictionary<string, string> AuthorList)
        {
            int tenderId = int.Parse(AuthorList["TenderId"]);
            int formId = int.Parse(AuthorList["FormId"]);
            int table = int.Parse(AuthorList["TableId"]);
            Tender tender = await _tenderQueries.GetTenderWithFormQuantityItemsWithChanges(table, false, tenderId);
            long createdTable = 0;
            if (table == 0)
            {
                tender.CreateTenderQuantityTables(0, "اسم الجدول", formId);
                await _tenderCommands.UpdateAsync(tender);
                createdTable = tender.TenderQuantityTables.OrderByDescending(a => a.Id).FirstOrDefault().Id;
            }
            else
            {
                createdTable = table;
            }
            if (string.IsNullOrEmpty(AuthorList["ItemNumber"].ToString()))
            {
                var newItemNumber = _tenderQueries.getLastItemNumber("", long.Parse(AuthorList["TableId"])) + 1;
                AuthorList["ItemNumber"] = newItemNumber.ToString();
            }
            ApiResponse<HtmlTableRowTemplateDto> lst = await _templatesProxy.ValidateTenderQuantityItem(tender.TemplateYears == null ? 0 : tender.TemplateYears.Value, AuthorList);
            if (lst.Data == null)
                throw new BusinessRuleException(lst.Message);
            lst.Data.QuantityItemDto.ForEach(x => x.TableId = createdTable);
            tender.UpdateTenderStatus(Enums.TenderStatus.UnderEstablishing);
            var tableName = "اسم الجدول";
            if (AuthorList["TableName"] != null)
                tableName = AuthorList["TableName"];
            long.TryParse(AuthorList["ItemNumber"], out long itemNumber);
            tender.SaveTenderQuantityTables(lst.Data.QuantityItemDto, tableName, itemNumber, out itemNumber, formId);
            await _tenderCommands.UpdateAsync(tender);
            return new ValidationReturndTemplate() { tableId = lst.Data.TableId.Replace('_' + createdTable.ToString() + '_', '_' + table.ToString() + '_'), FormHtml = lst.Data.FormHtml, formTableId = createdTable, itemNumber = itemNumber };
        }

        public async Task<ValidationReturndTemplate> ValidateQuantitiesChangesData(Dictionary<string, string> AuthorList)
        {
            int tenderId = int.Parse(AuthorList["TenderId"]);
            int formId = int.Parse(AuthorList["FormId"]);
            long tableId = long.Parse(AuthorList["TableId"]);
            Tender tender = await _tenderQueries.GetTenderWithFormQuantityItemsWithChanges(tableId, true, tenderId);
            var changeRequest = tender.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables).LastOrDefault();
            if (changeRequest == null)
            {
                changeRequest = tender.CreateTenderQuantityTableChangeRequest(_httpContextAccessor.HttpContext.User.UserId(), _httpContextAccessor.HttpContext.User.UserRole());
                await _tenderCommands.UpdateAsync(tender);
            }
            long createdTable = 0;
            createdTable = tableId;
            if (string.IsNullOrEmpty(AuthorList["ItemNumber"].ToString()))
            {
                var newItemNumber = tender.GetTenderQuantityTableLastIndexInEdit(changeRequest.TenderChangeRequestId, createdTable) + 1;
                AuthorList["ItemNumber"] = newItemNumber.ToString();
            }
            ApiResponse<HtmlTableRowTemplateDto> lst = await _templatesProxy.ValidateTenderQuantityItem(tender.TemplateYears == null ? 0 : tender.TemplateYears.Value, AuthorList);
            lst.Data.QuantityItemDto.ForEach(x => x.TableId = createdTable);
            long.TryParse(AuthorList["ItemNumber"], out long itemNumber);

            tender.SaveTenderQuantityTableChange(changeRequest.TenderChangeRequestId, lst.Data.QuantityItemDto, itemNumber, out itemNumber, createdTable);
            await _tenderCommands.UpdateAsync(tender);
            return new ValidationReturndTemplate() { tableId = lst.Data.TableId.Replace('_' + createdTable.ToString() + '_', '_' + tableId.ToString() + '_'), FormHtml = lst.Data.FormHtml, formTableId = createdTable, itemNumber = itemNumber };
        }

        public async Task<ValidationReturndTemplate> DeleteTenderQuantityItem(Dictionary<string, string> authorList)
        {
            int tenderId = int.Parse(authorList["TenderId"]);
            long table = long.Parse(authorList["TableId"]);
            Tender tender = await _tenderQueries.GetTenderWithFormQuantityItemsWithChanges(table, false, tenderId);
            int itemNumber = int.Parse(authorList["ItemNumber"]);
            int formId = int.Parse(authorList["FormId"]);
            tender.UpdateTenderStatus(Enums.TenderStatus.UnderEstablishing);
            tender.DeleteQuantityTableItems(table, itemNumber);
            await _tenderCommands.UpdateAsync(tender);
            return new ValidationReturndTemplate() { tableId = "Table_" + table.ToString() + "_" + formId.ToString(), formTableId = table };
        }

        public async Task<ValidationReturndTemplate> DeleteTenderQuantityItemChanges(Dictionary<string, string> authorList)
        {
            int tenderId = int.Parse(authorList["TenderId"]);
            long table = long.Parse(authorList["TableId"]);
            Tender tender = await _tenderQueries.GetTenderWithFormQuantityItemsWithChanges(table, true, tenderId);
            var changeRequest = tender.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables).LastOrDefault();
            if (changeRequest == null)
            {
                changeRequest = tender.CreateTenderQuantityTableChangeRequest(_httpContextAccessor.HttpContext.User.UserId(), _httpContextAccessor.HttpContext.User.UserRole());
                await _tenderCommands.UpdateAsync(tender);
            }
            int itemNumber = int.Parse(authorList["ItemNumber"]);
            int formId = int.Parse(authorList["FormId"]);
            tender.DeleteQuantityTableItemsChanges(changeRequest.TenderChangeRequestId, table, itemNumber);
            await _tenderCommands.UpdateAsync(tender);
            return new ValidationReturndTemplate() { tableId = "Table_" + table.ToString() + "_" + formId.ToString(), formTableId = table };
        }

        public async Task<TemplateQuantityTableModel> FindTenderQuantityItemsForConditionsBookletById(int tenderId)
        {
            QuantitiesTemplateModel lst = await _tenderQueries.GetTenderQuantityItems(tenderId);
            if (lst == null)
                lst = new QuantitiesTemplateModel();
            if (lst.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || lst.TenderTypeId == (int)Enums.TenderType.CurrentTender || lst.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                lst.ActivityTemplates = new List<int> { (int)TenderActivityTamplate.OldSystemAndVRO };
            }
            foreach (var item in lst.ActivityTemplates)
            {
                FormConfigurationDTO DTOModel = new FormConfigurationDTO() { TenderId = tenderId, ActivityId = item, QuantityItemDtos = lst.QuantitiesItems.Where(a => a.TemplateId == item).ToList(), YearsCount = lst.TemplateYears == null ? 0 : lst.TemplateYears.Value };
                ApiResponse<List<HtmlTemplateDto>> obj = await _templatesProxy.GenerateTemplate(DTOModel);
                if (obj.Data != null)
                {
                    lst.grids.AddRange(obj.Data.GroupBy(o => new { o.FormId, o.FormName, o.TemplateName, o.FormExcellTemplate, o.FormCategoryId }).Select(o => new HTMLObject
                    {
                        FormId = o.Key.FormId,
                        FormName = o.Key.FormName,
                        TemplateName = o.Key.TemplateName,
                        FormCategoryId = o.Key.FormCategoryId,
                        FormExcellTemplate = _rootConfiguration.APIConfiguration.QuantityTemplates + "/api/QuantityTable/" + o.Key.FormExcellTemplate,
                        gridTables = o.Select(u => new TableModel { TableHtml = u.FormHtml, TableId = u.TableId, TableName = u.TableName, TenderId = u.TenderId.Value, FormId = u.FormId, DescriptionTableHtml = u.DescriptionTableHtml }).ToList()
                    }).ToList());
                }
            }
            var result = "";
            foreach (var template in lst.grids)
            {
                result += "<h3 class='title text-primary p-1 m-0'><span>" + template.TemplateName + " " + template.FormName + "</span></h3></br>";
                foreach (var table in template.gridTables)
                {
                    result += table.TableHtml;
                }
            }
            int index = 0;
            var descriptionResult = new Dictionary<int, List<string>>();
            foreach (var template in lst.grids)
            {
                index = 0;


                if (!descriptionResult.ContainsKey(template.FormCategoryId))
                    descriptionResult.Add(template.FormCategoryId, new List<string>());
                string descriptionResultString = "";
                foreach (var table in template.gridTables)
                {
                    if (index == 0)
                    {
                        descriptionResultString = "<h3 class='title text-primary p-1 m-0'><span>" + template.TemplateName + " " + template.FormName + "</span></h3></br>";
                    }
                    else descriptionResultString = "";
                    descriptionResultString += table.DescriptionTableHtml;
                    descriptionResult[template.FormCategoryId].Add(descriptionResultString);
                    index = index + 1;
                }
            }
            TemplateQuantityTableModel TablesModel = new TemplateQuantityTableModel { DescritionTables = descriptionResult, Tables = result };
            return TablesModel;
        }


        public async Task<QuantityTableStepDTO> FindTenderQuantityItemsByIdForCreation(int tenderId, int branchId, bool isReadOnly)
        {
            var tender = await _tenderQueries.GetTenderWithFormQuantityTablesAndActivitiesWithoutChanges(tenderId);
            if (tender == null || tender.BranchId != branchId)
            {
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            }
            var activityVersionId = await _tenderQueries.GetCurrentTenderActivityVersion(tender.TenderId);

            var activityIds = tender.TenderActivities.Select(a => a.ActivityId).ToList();
            var activityTemplates = await _tenderQueries.GetTemplatesByActivityIdsAndVersionId(activityIds, activityVersionId);

            if (tender.IsTenderContainsTawreedTables.HasValue && tender.IsTenderContainsTawreedTables.Value)
            {
                activityTemplates.Add((int)Enums.ActivityTemplate.TawreedActivityVersion4);
            }
            if (tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.CurrentTender || tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                activityTemplates = new List<int> { (int)TenderActivityTamplate.OldSystemAndVRO };
            }
            TenderActivityDTO tenderActivityDTO = new TenderActivityDTO { ActivityIdsList = activityTemplates, YearCount = tender.TemplateYears ?? 0, VesionId = tender.QuantityTableVersionId == null ? 1 : tender.QuantityTableVersionId };
            ApiResponse<List<TemplateFormDTO>> obj = await _templatesProxy.GetActivitiesTables(tenderActivityDTO);
            QuantityTableStepDTO quantityTableStepDTO = new QuantityTableStepDTO
            {
                TemplateYears = tender.TemplateYears ?? 0,
                TenderID = tender.TenderId,
                TenderCreatedByTypeId = tender.CreatedByTypeId ?? 0,
                TenderIdString = Util.Encrypt(tender.TenderId),
                PreQualificationId = tender.PreQualificationId ?? 0,
                InvitationTypeId = tender.InvitationTypeId ?? 0,
                HasAlternativeOffer = tender.HasAlternativeOffer ?? false,
                TenderName = tender.TenderName,
                TenderStatusId = tender.TenderStatusId,
                ReferenceNumber = tender.ReferenceNumber,
                TenderNumber = tender.TenderNumber,
                TenderTypeId = tender.TenderTypeId,
                OfferPresentationWayId = tender.OfferPresentationWayId ?? 0,
                TemplateFormDTOs = obj.Data,
                QuantityTableVersionId = tender.QuantityTableVersionId
            };
            quantityTableStepDTO.TemplateFormDTOs.ForEach(a =>
            {
                a.FormDTOs.ForEach(f =>
                {
                    f.FormExcellTemplate = "/Tender/GenerateQuantityTableTemplateExcel?stageId=" + f.FormExcellTemplate.Split('/')[1] + "&formId=" + f.FormExcellTemplate.Split('/')[2] + "&yearsCount=" + f.FormExcellTemplate.Split('/')[3];
                    f.Tables = tender.TenderQuantityTables.Where(q => q.IsActive == true && q.FormId == f.FormId)
                    .Select(t => new TableDTO { TableId = t.Id, TableName = t.Name }).ToList();
                });
            });
            if (isReadOnly)
            {
                quantityTableStepDTO.TemplateFormDTOs.Where(a => a.FormDTOs.All(f => f.Tables.Count == 0)).ToList().ForEach(a => quantityTableStepDTO.TemplateFormDTOs.Remove(a));
                quantityTableStepDTO.TemplateFormDTOs.ForEach(a => a.FormDTOs.Where(f => f.Tables.Count == 0).ToList().ForEach(f => a.FormDTOs.Remove(f)));
            }
            quantityTableStepDTO.IsReadOnly = isReadOnly;
            return quantityTableStepDTO;
        }

        public async Task<QuantityTableStepDTO> FindTenderQuantityItemsById(int tenderId, bool isReadOnly)
        {
            var tender = await _tenderQueries.GetTenderWithFormQuantityTablesAndActivitiesWithoutChanges(tenderId);
            var activityTemplates = tender.TenderActivities.SelectMany(a => a.Activity.ActivityTemplateVersions.Select(t => t.TemplateId.Value)).Distinct().ToList();
            if (tender.IsTenderContainsTawreedTables == true)
            {

                activityTemplates.Add((int)Enums.ActivityTemplate.TawreedActivityVersion4);
            }
            if (tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.CurrentTender || tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                activityTemplates = new List<int> { (int)TenderActivityTamplate.OldSystemAndVRO };
            }
            TenderActivityDTO tenderActivityDTO = new TenderActivityDTO { ActivityIdsList = activityTemplates, YearCount = tender.TemplateYears ?? 0, VesionId = tender.QuantityTableVersionId };
            ApiResponse<List<TemplateFormDTO>> obj = await _templatesProxy.GetActivitiesTables(tenderActivityDTO);
            QuantityTableStepDTO quantityTableStepDTO = new QuantityTableStepDTO
            {
                TemplateYears = tender.TemplateYears ?? 0,
                TenderID = tender.TenderId,
                TenderCreatedByTypeId = tender.CreatedByTypeId ?? 0,
                TenderIdString = Util.Encrypt(tender.TenderId),
                PreQualificationId = tender.PreQualificationId ?? 0,
                InvitationTypeId = tender.InvitationTypeId ?? 0,
                HasAlternativeOffer = tender.HasAlternativeOffer ?? false,
                TenderName = tender.TenderName,
                TenderStatusId = tender.TenderStatusId,
                ReferenceNumber = tender.ReferenceNumber,
                TenderNumber = tender.TenderNumber,
                TenderTypeId = tender.TenderTypeId,
                OfferPresentationWayId = tender.OfferPresentationWayId ?? 0,
                TemplateFormDTOs = obj.Data
            };
            quantityTableStepDTO.TemplateFormDTOs.ForEach(a =>
            {
                a.FormDTOs.ForEach(f =>
                {
                    f.FormExcellTemplate = "/Tender/GenerateQuantityTableTemplateExcel?stageId=" + f.FormExcellTemplate.Split('/')[1] + "&formId=" + f.FormExcellTemplate.Split('/')[2] + "&yearsCount=" + f.FormExcellTemplate.Split('/')[3];
                    f.Tables = tender.TenderQuantityTables.Where(q => q.IsActive == true && q.FormId == f.FormId)
                    .Select(t => new TableDTO { TableId = t.Id, TableName = t.Name }).ToList();
                });
            });
            if (isReadOnly)
            {
                quantityTableStepDTO.TemplateFormDTOs.Where(a => a.FormDTOs.All(f => f.Tables.Count == 0)).ToList().ForEach(a => quantityTableStepDTO.TemplateFormDTOs.Remove(a));
                quantityTableStepDTO.TemplateFormDTOs.ForEach(a => a.FormDTOs.Where(f => f.Tables.Count == 0).ToList().ForEach(f => a.FormDTOs.Remove(f)));
            }
            quantityTableStepDTO.IsReadOnly = isReadOnly;
            return quantityTableStepDTO;
        }

        public async Task<QuantityTableStepDTO> FindTenderQuantityItemsByFormIds(int tenderId, bool isReadOnly)
        {
            var tender = await _tenderQueries.GetTenderWithFormQuantityTablesAndActivitiesWithoutChanges(tenderId);
            var formIds = tender.TenderQuantityTables.Select(a => long.Parse(a.FormId.ToString())).ToList();
            ApiResponse<List<TemplateFormDTO>> obj = await _templatesProxy.GenerateTemplatesByFormIds(formIds);
            QuantityTableStepDTO quantityTableStepDTO = new QuantityTableStepDTO
            {
                TemplateYears = tender.TemplateYears ?? 0,
                TenderID = tender.TenderId,
                TenderCreatedByTypeId = tender.CreatedByTypeId ?? 0,
                TenderIdString = Util.Encrypt(tender.TenderId),
                PreQualificationId = tender.PreQualificationId ?? 0,
                InvitationTypeId = tender.InvitationTypeId ?? 0,
                HasAlternativeOffer = tender.HasAlternativeOffer ?? false,
                TenderName = tender.TenderName,
                TenderStatusId = tender.TenderStatusId,
                ReferenceNumber = tender.ReferenceNumber,
                TenderNumber = tender.TenderNumber,
                TenderTypeId = tender.TenderTypeId,
                OfferPresentationWayId = tender.OfferPresentationWayId ?? 0,
                TemplateFormDTOs = obj.Data
            };
            quantityTableStepDTO.TemplateFormDTOs.ForEach(a =>
            {
                a.FormDTOs.ForEach(f =>
                {
                    f.Tables = tender.TenderQuantityTables.Where(q => q.IsActive == true && q.FormId == f.FormId)
                    .Select(t => new TableDTO { TableId = t.Id, TableName = t.Name }).ToList();
                });
            });
            if (isReadOnly)
            {
                quantityTableStepDTO.TemplateFormDTOs.Where(a => a.FormDTOs.All(f => f.Tables.Count == 0)).ToList().ForEach(a => quantityTableStepDTO.TemplateFormDTOs.Remove(a));
                quantityTableStepDTO.TemplateFormDTOs.ForEach(a => a.FormDTOs.Where(f => f.Tables.Count == 0).ToList().ForEach(f => a.FormDTOs.Remove(f)));
            }
            quantityTableStepDTO.IsReadOnly = isReadOnly;
            return quantityTableStepDTO;
        }

        public async Task<QuantityTableModelForExcel> GenerateQuantityTableTemplateExcel(int stageId, int formId, int yearsCount)
        {
            return await _templatesProxy.GenerateQuantityTableTemplateExcel(stageId, formId, yearsCount);
        }

        public async Task<ExcelHeader> GenerateQuantityTableTemplateExcel_New(int stageId, int formId, int yearsCount)
        {
            var result = await _templatesProxy.GenerateQuantityTableTemplateExcelHeader(stageId, formId, yearsCount);
            return result;
        }

        public async Task<QuantityTableStepDTO> FindTenderQuantityItemsChangesById(int tenderId)
        {
            var tender = await _tenderQueries.GetTenderWithFormQuantityTablesWithChanges(tenderId);
            var activityTemplates = tender.TenderActivities.SelectMany(a => a.Activity.ActivityTemplateVersions.Select(t => t.TemplateId.Value)).Distinct().ToList();
            if (tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.CurrentTender || tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                activityTemplates = new List<int> { (int)TenderActivityTamplate.OldSystemAndVRO };
            }
            if (tender.IsTenderContainsTawreedTables == true)
            {
                activityTemplates.Add((int)TenderActivityTamplate.GeneralSupply);
            }
            TenderActivityDTO tenderActivityDTO = new TenderActivityDTO { ActivityIdsList = activityTemplates, YearCount = tender.TemplateYears ?? 0, VesionId = tender.QuantityTableVersionId };
            ApiResponse<List<TemplateFormDTO>> obj = await _templatesProxy.GetActivitiesTables(tenderActivityDTO);
            var hasChangeRequest = tender.ChangeRequests.Any(a => a.IsActive == true && a.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables && a.ChangeRequestStatusId != (int)ChangeStatusType.Approved && (a.TenderQuantityTableChanges.Any() || a.HasAlternativeOffer != tender.HasAlternativeOffer));
            QuantityTableStepDTO quantityTableStepDTO = new QuantityTableStepDTO
            {
                TemplateYears = tender.TemplateYears ?? 0,
                TenderID = tender.TenderId,
                TenderCreatedByTypeId = tender.CreatedByTypeId ?? 0,
                TenderIdString = Util.Encrypt(tender.TenderId),
                PreQualificationId = tender.PreQualificationId ?? 0,
                InvitationTypeId = tender.InvitationTypeId ?? 0,
                TenderName = tender.TenderName,
                TenderStatusId = tender.TenderStatusId,
                ReferenceNumber = tender.ReferenceNumber,
                TenderNumber = tender.TenderNumber,
                TenderTypeId = tender.TenderTypeId,
                OfferPresentationWayId = tender.OfferPresentationWayId ?? 0,
                TemplateFormDTOs = obj.Data,
                IsReadOnly = false,
                QuantityTableVersionId = tender.QuantityTableVersionId,
                ChangeRequestStatusId = hasChangeRequest ?
                                            tender.ChangeRequests.FirstOrDefault(a => a.IsActive == true && a.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables && a.ChangeRequestStatusId != (int)ChangeStatusType.Approved).ChangeRequestStatusId : 0,
                ChangeRequestTypeId = hasChangeRequest ?
                                            tender.ChangeRequests.FirstOrDefault(a => a.IsActive == true && a.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables && a.ChangeRequestStatusId != (int)ChangeStatusType.Approved).ChangeRequestTypeId : 0,
                RejectionReason = tender.ChangeRequests.Any(a => a.IsActive == true && a.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables && a.ChangeRequestStatusId == (int)ChangeStatusType.Rejected) ?
                                            tender.ChangeRequests.FirstOrDefault(a => a.IsActive == true && a.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables && a.ChangeRequestStatusId == (int)ChangeStatusType.Rejected).RejectionReason : "",
                HasAlternativeOffer = hasChangeRequest ?
                                            tender.ChangeRequests.FirstOrDefault(a => a.IsActive == true && a.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables && a.ChangeRequestStatusId != (int)ChangeStatusType.Approved).HasAlternativeOffer ?? false : tender.HasAlternativeOffer ?? false,
                HasChangedAlternativeOffer = hasChangeRequest &&
                                                tender.ChangeRequests.FirstOrDefault(a => a.IsActive == true && a.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables && a.ChangeRequestStatusId != (int)ChangeStatusType.Approved).HasAlternativeOffer.HasValue
            };
            quantityTableStepDTO.TemplateFormDTOs.ForEach(a =>
            {
                a.FormDTOs.ForEach(f =>
                {
                    f.FormExcellTemplate = _rootConfiguration.APIConfiguration.QuantityTemplates + "/api/QuantityTable/" + f.FormExcellTemplate;
                    f.Tables = tender.TenderQuantityTables.Where(q => q.IsActive == true && q.QuantitiyItemsJson.TenderQuantityTableItems.Any() && q.FormId == f.FormId)
                    .Select(t => new TableDTO
                    {
                        TableId = t.Id,
                        TableName = t.Name,
                        IsTableDeleted = IsTableDeleted(tender, t.Id),
                        IsNewTable = false
                    }).ToList();
                    f.Tables.AddRange(GetNewTables(tender, f.FormId));
                });
            });

            quantityTableStepDTO.IsReadOnly = false;
            return quantityTableStepDTO;
        }

        public async Task<QuantityTableStepDTO> FindTenderQuantityItemsChangesReadOnlyById(int tenderId)
        {
            var tender = await _tenderQueries.GetTenderWithFormQuantityTablesWithChanges(tenderId);
            var formIds = tender.TenderQuantityTables.Where(a => a.IsActive == true).Select(a => long.Parse(a.FormId.ToString())).ToList();
            var changeRequest = tender.ChangeRequests.FirstOrDefault(a => a.IsActive == true && a.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables && a.ChangeRequestStatusId != (int)ChangeStatusType.Approved);
            if (changeRequest != null && changeRequest.TenderQuantityTableChanges.Count > 0)
                formIds.AddRange(changeRequest.TenderQuantityTableChanges.Where(a => a.IsActive == true).Select(a => long.Parse(a.FormId.ToString())).ToList());
            ApiResponse<List<TemplateFormDTO>> obj = await _templatesProxy.GenerateTemplatesByFormIds(formIds);
            QuantityTableStepDTO quantityTableStepDTO = new QuantityTableStepDTO
            {
                TemplateYears = tender.TemplateYears ?? 0,
                TenderID = tender.TenderId,
                TenderCreatedByTypeId = tender.CreatedByTypeId ?? 0,
                TenderIdString = Util.Encrypt(tender.TenderId),
                PreQualificationId = tender.PreQualificationId ?? 0,
                InvitationTypeId = tender.InvitationTypeId ?? 0,
                TenderName = tender.TenderName,
                TenderStatusId = tender.TenderStatusId,
                ReferenceNumber = tender.ReferenceNumber,
                TenderNumber = tender.TenderNumber,
                TenderTypeId = tender.TenderTypeId,
                OfferPresentationWayId = tender.OfferPresentationWayId ?? 0,
                TemplateFormDTOs = obj.Data,
                IsReadOnly = true,
                QuantityTableVersionId = tender.QuantityTableVersionId,
                ChangeRequestStatusId = changeRequest != null ? changeRequest.ChangeRequestStatusId : 0,
                ChangeRequestTypeId = changeRequest != null ? changeRequest.ChangeRequestTypeId : 0,
                RejectionReason = changeRequest != null ? changeRequest.RejectionReason : "",
                HasAlternativeOffer = changeRequest != null ? changeRequest.HasAlternativeOffer ?? false : tender.HasAlternativeOffer ?? false,
                HasChangedAlternativeOffer = changeRequest != null && changeRequest.HasAlternativeOffer.HasValue
            };
            quantityTableStepDTO.TemplateFormDTOs.ForEach(a =>
            {
                a.FormDTOs.ForEach(f =>
                {
                    f.Tables = tender.TenderQuantityTables.Where(q => q.IsActive == true && q.FormId == f.FormId)
               .Select(t => new TableDTO
               {
                   TableId = t.Id,
                   TableName = t.Name,
                   IsTableDeleted = IsTableDeleted(tender, t.Id),
                   IsNewTable = false
               }).ToList();
                    f.Tables.AddRange(GetNewTables(tender, f.FormId));
                });
            });
            quantityTableStepDTO.IsReadOnly = true;
            return quantityTableStepDTO;
        }

        private List<TableDTO> GetNewTables(Tender tender, int formId)
        {
            if (tender.ChangeRequests.Any(c => c.IsActive == true && c.ChangeRequestStatusId != (int)ChangeStatusType.Approved && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables) &&
                tender.ChangeRequests.FirstOrDefault(c => c.IsActive == true && c.ChangeRequestStatusId != (int)ChangeStatusType.Approved && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables)
                .TenderQuantityTableChanges.Any(q => q.IsActive == true && q.FormId == formId))
                return tender.ChangeRequests.FirstOrDefault(c => c.IsActive == true && c.ChangeRequestStatusId != (int)ChangeStatusType.Approved && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables)
                    .TenderQuantityTableChanges.Where(q => q.IsActive == true && q.FormId == formId)
                .Select(t => new TableDTO
                {
                    TableId = t.Id,
                    TableName = t.Name,
                    IsTableDeleted = false,
                    IsNewTable = true
                }).ToList();
            return new List<TableDTO>();
        }

        private bool IsTableDeleted(Tender tender, long tableId)
        {
            return tender.ChangeRequests.Any(c => c.IsActive == true && c.ChangeRequestStatusId != (int)ChangeStatusType.Approved && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables) &&
                tender.ChangeRequests.FirstOrDefault(c => c.IsActive == true && c.ChangeRequestStatusId != (int)ChangeStatusType.Approved && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables)
                .TenderQuantityTableChanges.Any(q => q.IsActive == true && q.TenderQuantitiesTableId == tableId && q.TableChangeStatusId == (int)QuantityTableChangeStatus.Remove);
        }

        public async Task<QueryResult<TableModel>> GetTenderTableQuantityItems(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            var cellsCount = 0;
            var isNewTable = quantityTableSearchCretriaModel.QuantityTableId == 0;
            Tender tender = await _tenderQueries.GetTenderWithQuantityTable(quantityTableSearchCretriaModel.TenderId);
            if (quantityTableSearchCretriaModel.QuantityTableId == 0)
            {
                var table = tender.CreateTenderQuantityTables(0, "اسم الجدول", quantityTableSearchCretriaModel.FormId);
                await _tenderCommands.UpdateAsync(tender);
                quantityTableSearchCretriaModel.QuantityTableId = table.Id;
            }
            else
                cellsCount = await _tenderQueries.GetTenderTableQuantityItems(tender, quantityTableSearchCretriaModel.QuantityTableId);
            quantityTableSearchCretriaModel.CellsCount = cellsCount;
            var quantityItems = await _tenderQueries.GetTenderTableQuantityItems(quantityTableSearchCretriaModel);
            List<HTMLObject> hTMLObjects = new List<HTMLObject>();
            FormConfigurationDTO DTOModel = new FormConfigurationDTO() { TenderId = quantityTableSearchCretriaModel.TenderId, QuantityItemDtos = quantityItems.Items.ToList(), YearsCount = tender.TemplateYears == null ? 0 : tender.TemplateYears.Value };
            ApiResponse<List<HtmlTemplateDto>> resultList = new ApiResponse<List<HtmlTemplateDto>>();
            ApiResponse<HtmlTemplateDto> resultItem = new ApiResponse<HtmlTemplateDto>();
            if (DTOModel.QuantityItemDtos.Count != 0)
                resultList = quantityTableSearchCretriaModel.IsReadOnly ? await _templatesProxy.GetMonafasatGOVReadOnly(DTOModel) : await _templatesProxy.GenerateGovTableTemplate(DTOModel);
            else
                resultItem = await _templatesProxy.GetTemplateFormHtml(quantityTableSearchCretriaModel.TenderId, quantityTableSearchCretriaModel.FormId, tender.TemplateYears == null ? 0 : tender.TemplateYears.Value, quantityTableSearchCretriaModel.QuantityTableId);

            ApiResponse<List<HtmlTemplateDto>> obj = new ApiResponse<List<HtmlTemplateDto>> { Data = quantityTableSearchCretriaModel.CellsCount != 0 ? resultList.Data : new List<HtmlTemplateDto> { resultItem.Data } };
            if (obj.Data != null && obj.Data.Count > 0 && obj.Data[0] != null)
            {
                if (obj.Data[0].ColumnsCount != 0)
                    cellsCount = obj.Data[0].ColumnsCount;
                hTMLObjects.AddRange(obj.Data.GroupBy(o => new { o.FormId, o.FormName, o.TemplateName, o.FormExcellTemplate })
                    .Select(o => new HTMLObject
                    {
                        FormId = o.Key.FormId,
                        FormName = o.Key.FormName,
                        TemplateName = o.Key.TemplateName,
                        FormExcellTemplate = _rootConfiguration.APIConfiguration.QuantityTemplates + "/api/QuantityTable/" + o.Key.FormExcellTemplate,
                        Javascript = o.FirstOrDefault().JsScript,
                        gridTables = o.Select(u => new TableModel
                        {
                            TableHtml = u.FormHtml,
                            TableId = u.TableId,
                            TableName = !string.IsNullOrEmpty(u.TableName) ? u.TableName : "اسم الجدول",
                            FormId = u.FormId,
                            DeleteFormHtml = u.DeleteFormHtml,
                            EditFormHtml = u.EditFormHtml,
                            Javascript = u.JsScript,
                            IsReadOnly = quantityTableSearchCretriaModel.IsReadOnly,
                            TemplateYears = tender.TemplateYears
                        }).ToList()
                    }).ToList());
                hTMLObjects.ForEach(x => x.gridTables.ForEach(a => a.TableId = quantityTableSearchCretriaModel.QuantityTableId.ToString()));
                hTMLObjects.ForEach(x => x.gridTables.ForEach(a => a.TenderId = quantityTableSearchCretriaModel.TenderId));
            }
            if (hTMLObjects.Any(a => a.gridTables.Any(b => b.TableId == quantityTableSearchCretriaModel.QuantityTableId.ToString())))
            {
                if (!isNewTable)
                    hTMLObjects.FirstOrDefault(a => a.FormId == quantityTableSearchCretriaModel.FormId).gridTables.FirstOrDefault().TableName = tender.TenderQuantityTables.FirstOrDefault(a => a.Id == quantityTableSearchCretriaModel.QuantityTableId).Name;
                return new QueryResult<TableModel>(hTMLObjects.FirstOrDefault(a => a.FormId == quantityTableSearchCretriaModel.FormId).gridTables.ToList(), quantityItems.TotalCount, quantityTableSearchCretriaModel.PageNumber, quantityTableSearchCretriaModel.PageSize * cellsCount);
            }
            else
            {
                if (!isNewTable)
                    hTMLObjects.FirstOrDefault().gridTables.FirstOrDefault().TableName = tender.TenderQuantityTables.FirstOrDefault(a => a.Id == quantityTableSearchCretriaModel.QuantityTableId).Name;
                hTMLObjects.FirstOrDefault().gridTables.FirstOrDefault().TenderId = quantityTableSearchCretriaModel.TenderId;
                hTMLObjects.FirstOrDefault().gridTables.FirstOrDefault().TableId = quantityTableSearchCretriaModel.QuantityTableId.ToString();
                return new QueryResult<TableModel>(hTMLObjects.FirstOrDefault(a => a.FormId == quantityTableSearchCretriaModel.FormId).gridTables.ToList(), cellsCount, quantityTableSearchCretriaModel.PageNumber, quantityTableSearchCretriaModel.PageSize * cellsCount);
            }
        }

        public async Task<QueryResult<TableModel>> GetTenderTableQuantityItemsChanges(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            var cellsCount = 0;
            var isNewTable = quantityTableSearchCretriaModel.QuantityTableId == 0;
            Tender tender = await _tenderQueries.GetTenderWithQuantityTable(quantityTableSearchCretriaModel.TenderId);
            var changeRequest = await _tenderQueries.GetChangeRequestWithFormQuantityTables(tender.TenderId);
            if (changeRequest != null)
                tender.ChangeRequests.Add(changeRequest);
            if (quantityTableSearchCretriaModel.QuantityTableId == 0 && quantityTableSearchCretriaModel.IsNewTable)
            {
                var tableId = await AddNewTableChange(quantityTableSearchCretriaModel.FormId, quantityTableSearchCretriaModel.TenderId, 0, "اسم الجدول");
                quantityTableSearchCretriaModel.QuantityTableId = tableId;
            }
            else
                cellsCount = !quantityTableSearchCretriaModel.IsNewTable ? await _tenderQueries.GetTenderTableQuantityItems(tender, quantityTableSearchCretriaModel.QuantityTableId) : await _tenderQueries.GetTenderTableQuantityItemsChanges(tender, quantityTableSearchCretriaModel.QuantityTableId);
            quantityTableSearchCretriaModel.CellsCount = cellsCount;
            var quantityItems = !quantityTableSearchCretriaModel.IsNewTable ? await _tenderQueries.GetTenderTableQuantityItems(quantityTableSearchCretriaModel) : await _tenderQueries.GetTenderTableQuantityItemsChanges(quantityTableSearchCretriaModel);
            List<HTMLObject> hTMLObjects = new List<HTMLObject>();
            FormConfigurationDTO DTOModel = new FormConfigurationDTO() { TenderId = quantityTableSearchCretriaModel.TenderId, QuantityItemDtos = quantityItems.Items.ToList(), YearsCount = tender.TemplateYears == null ? 0 : tender.TemplateYears.Value };
            ApiResponse<List<HtmlTemplateDto>> resultList = new ApiResponse<List<HtmlTemplateDto>>();
            ApiResponse<HtmlTemplateDto> resultItem = new ApiResponse<HtmlTemplateDto>();
            if (DTOModel.QuantityItemDtos.Count != 0)
                resultList = quantityTableSearchCretriaModel.IsReadOnly || !quantityTableSearchCretriaModel.IsNewTable ? await _templatesProxy.GetMonafasatGOVReadOnly(DTOModel) : await _templatesProxy.GetMonafasatGOV(DTOModel);
            else
                resultItem = await _templatesProxy.GetTemplateFormHtml(quantityTableSearchCretriaModel.TenderId, quantityTableSearchCretriaModel.FormId, tender.TemplateYears == null ? 0 : tender.TemplateYears.Value, quantityTableSearchCretriaModel.QuantityTableId);

            ApiResponse<List<HtmlTemplateDto>> obj = new ApiResponse<List<HtmlTemplateDto>> { Data = quantityTableSearchCretriaModel.CellsCount != 0 ? resultList.Data : new List<HtmlTemplateDto> { resultItem.Data } };
            if (obj.Data != null && obj.Data.Count > 0 && obj.Data[0] != null)
            {
                hTMLObjects.AddRange(obj.Data.GroupBy(o => new { o.FormId, o.FormName, o.TemplateName, o.FormExcellTemplate })
                        .Select(o => new HTMLObject
                        {
                            FormId = o.Key.FormId,
                            FormName = o.Key.FormName,
                            TemplateName = o.Key.TemplateName,
                            FormExcellTemplate = _rootConfiguration.APIConfiguration.QuantityTemplates + "/api/QuantityTable/" + o.Key.FormExcellTemplate,
                            Javascript = o.FirstOrDefault().JsScript,
                            gridTables = o.Select(u => new TableModel
                            {
                                TableHtml = u.FormHtml,
                                TableId = u.TableId,
                                TableName = !string.IsNullOrEmpty(u.TableName) ? u.TableName : "اسم الجدول",
                                FormId = u.FormId,
                                DeleteFormHtml = u.DeleteFormHtml,
                                EditFormHtml = u.EditFormHtml,
                                IsNewTable = quantityTableSearchCretriaModel.IsNewTable,
                                IsTableDeleted = quantityTableSearchCretriaModel.IsTableDeleted,
                                Javascript = u.JsScript,
                                IsReadOnly = quantityTableSearchCretriaModel.IsReadOnly,
                                TemplateYears = tender.TemplateYears
                            }).ToList()
                        }).ToList());
                hTMLObjects.ForEach(x => x.gridTables.ForEach(a => a.TableId = quantityTableSearchCretriaModel.QuantityTableId.ToString()));
                hTMLObjects.ForEach(x => x.gridTables.ForEach(a => a.TenderId = quantityTableSearchCretriaModel.TenderId));
                hTMLObjects.ForEach(x => x.gridTables.ForEach(a => a.CanUndoChange = tender.ChangeRequests.Count > 0 && tender.ChangeRequests.Any(c => c.IsActive == true && c.ChangeRequestStatusId == (int)QuantityTableChangeStatus.Add && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables)));
                hTMLObjects.ForEach(x => x.gridTables.ForEach(a => a.HasChangeRequest = tender.ChangeRequests.Count > 0 && tender.ChangeRequests.Any(c => c.IsActive == true && c.ChangeRequestStatusId == (int)QuantityTableChangeStatus.Add && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables
                                    && (c.TenderQuantityTableChanges.Any(t => /*t.TenderQuantityTableItemChanges.Any(i => i.IsActive == true) && */t.IsActive == true && t.TableChangeStatusId == (int)QuantityTableChangeStatus.Add)
                                    || c.TenderQuantityTableChanges.Any(t => t.IsActive == true && t.TableChangeStatusId == (int)QuantityTableChangeStatus.Remove)))));
            }
            if (hTMLObjects.Any(a => a.gridTables.Any(b => b.TableId == quantityTableSearchCretriaModel.QuantityTableId.ToString())))
            {
                if (!isNewTable && quantityTableSearchCretriaModel.IsNewTable)
                    hTMLObjects.FirstOrDefault().gridTables.FirstOrDefault().TableName = tender.ChangeRequests.Count > 0 && tender.ChangeRequests.FirstOrDefault(a => a.IsActive == true && a.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables && a.ChangeRequestStatusId != (int)ChangeStatusType.Approved).TenderQuantityTableChanges.FirstOrDefault(a => a.Id == quantityTableSearchCretriaModel.QuantityTableId) != null ? tender.ChangeRequests.FirstOrDefault(a => a.IsActive == true && a.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables && a.ChangeRequestStatusId != (int)ChangeStatusType.Approved).TenderQuantityTableChanges.FirstOrDefault(a => a.Id == quantityTableSearchCretriaModel.QuantityTableId).Name : "اسم الجدول";
                return new QueryResult<TableModel>(hTMLObjects.FirstOrDefault(a => a.FormId == quantityTableSearchCretriaModel.FormId).gridTables.ToList(), quantityItems.TotalCount, quantityTableSearchCretriaModel.PageNumber, quantityTableSearchCretriaModel.PageSize * cellsCount);
            }
            else
            {
                if (!isNewTable && quantityTableSearchCretriaModel.IsNewTable)
                    hTMLObjects.FirstOrDefault().gridTables.FirstOrDefault().TableName = tender.ChangeRequests.FirstOrDefault(a => a.IsActive == true && a.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables && a.ChangeRequestStatusId != (int)ChangeStatusType.Approved).TenderQuantityTableChanges.FirstOrDefault(a => a.Id == quantityTableSearchCretriaModel.QuantityTableId).Name;
                hTMLObjects.FirstOrDefault().gridTables.FirstOrDefault().TenderId = quantityTableSearchCretriaModel.TenderId;
                hTMLObjects.FirstOrDefault().gridTables.FirstOrDefault().TableId = quantityTableSearchCretriaModel.QuantityTableId.ToString();
                return new QueryResult<TableModel>(hTMLObjects.FirstOrDefault(a => a.FormId == quantityTableSearchCretriaModel.FormId).gridTables.ToList(), hTMLObjects.FirstOrDefault(a => a.FormId == quantityTableSearchCretriaModel.FormId).gridTables.Count, quantityTableSearchCretriaModel.PageNumber, quantityTableSearchCretriaModel.PageSize * hTMLObjects.FirstOrDefault(a => a.FormId == quantityTableSearchCretriaModel.FormId).gridTables.Count);
            }
        }

        public async Task<string> GetEmptyForm(int formid, int tenderId, int templateYears, string tableName)
        {
            Tender tender = await _tenderQueries.GetTenderWithFormQuantityItemsWithChanges(0, false, tenderId);
            tender.CreateTenderQuantityTables(0, tableName, formid);
            await _tenderCommands.UpdateAsync(tender);
            long createdTable = tender.TenderQuantityTables.OrderByDescending(a => a.Id).FirstOrDefault().Id;
            var result = await _templatesProxy.GetTemplateFormHtml(tenderId, formid, templateYears, createdTable);
            result.Data.TableName = string.IsNullOrEmpty(tableName) ? "جدول جديد" : tableName;
            result.Data.FormHtml = _rootConfiguration.NewTableTemplateConfiguration.Value
                                    .Replace("#TableId#", createdTable.ToString())
                                    .Replace("#TableName#", tableName)
                                    .Replace("#FormId#", formid.ToString())
                                    .Replace("#TableContent#", result.Data.FormHtml);
            return result.Data.FormHtml;
        }

        public async Task<long> AddNewTable(int formid, int tenderId, int templateYears, string tableName)
        {
            Tender tender = await _tenderQueries.GetTenderWithFormQuantityItemsWithChanges(0, false, tenderId);
            var table = tender.CreateTenderQuantityTables(0, tableName, formid);
            await _tenderCommands.UpdateAsync(tender);
            return table.Id;
        }

        public async Task<long> AddNewTableChange(int formid, int tenderId, int templateYears, string tableName)
        {
            Tender tender = await _tenderQueries.GetTenderWithChangeRequest(tenderId);
            var changeRequest = tender.ChangeRequests.FirstOrDefault(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables && x.ChangeRequestStatusId != (int)Enums.ChangeStatusType.Approved);
            if (changeRequest == null)
            {
                changeRequest = tender.CreateTenderQuantityTableChangeRequest(_httpContextAccessor.HttpContext.User.UserId(), _httpContextAccessor.HttpContext.User.UserRole());
                await _tenderCommands.UpdateAsync(tender);
            }
            var table = tender.CreateTenderQuantityTablesChanges(changeRequest.TenderChangeRequestId, tableName, formid);
            await _tenderCommands.UpdateAsync(tender);
            return table.Id;
        }

        public async Task<long> UpdateTableName(long tableId, int tenderId, string tableName)
        {
            Tender tender = await _tenderQueries.GetTenderWithFormQuantityItemsWithChanges(tableId, false, tenderId);
            tender.UpdateQuantityTableName(tableId, tableName);
            await _tenderCommands.UpdateAsync(tender);
            return tableId == 0 ? tender.TenderQuantityTables.OrderByDescending(q => q.CreatedAt).FirstOrDefault().Id : tableId;
        }

        public async Task<long> UpdateTableChangesName(long tableId, int tenderId, string tableName)
        {
            Tender tender = await _tenderQueries.GetTenderWithFormQuantityItemsWithChanges(tableId, true, tenderId);
            var changeRequest = tender.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables).LastOrDefault();
            if (changeRequest == null)
            {
                changeRequest = tender.CreateTenderQuantityTableChangeRequest(_httpContextAccessor.HttpContext.User.UserId(), _httpContextAccessor.HttpContext.User.UserRole());
                await _tenderCommands.UpdateAsync(tender);
            }
            tender.UpdateQuantityTableChangesName(changeRequest.TenderChangeRequestId, tableId, tableName);
            await _tenderCommands.UpdateAsync(tender);
            return tableId == 0 ? tender.TenderQuantityTables.OrderByDescending(q => q.CreatedAt).FirstOrDefault().Id : tableId;
        }

        public async Task UpdateHasAlternative(int tenderId, bool hasAlternative)
        {
            Tender tender = await _tenderQueries.GetTenderWithFormQuantityTablesWithoutActivitiesAndChanges(tenderId);
            _tenderDomainService.CheckIfQuantityTablesHasItems(tender);

            Func<int, int, Task<List<int>>> GetTemplateFormsByTemplateId = _templatesProxy.GetFormIdsWithTemplateIdAndQtVersionId;
            _tenderDomainService.CheckIfAllE3ashaQuantityTablesHasItems(tender, GetTemplateFormsByTemplateId);

            tender.UpdateHasAlternative(hasAlternative);
            await _tenderCommands.UpdateAsync(tender);
        }

        public async Task DeleteTable(int tableId, int tenderId)
        {
            Tender tender = await _tenderQueries.GetTenderWithFormQuantityItemsWithChanges(tableId, false, tenderId);
            TenderQuantityTable tenderQuantityTable = tender.TenderQuantityTables.FirstOrDefault(t => t.Id == tableId);
            tenderQuantityTable.DeleteTenderQuantityTableWithItems();
            await _tenderCommands.UpdateAsync(tender);
        }

        public async Task DeleteTableChange(int tableId, int tenderId, bool isNewTable)
        {
            Tender tender = await _tenderQueries.GetTenderWithFormQuantityItemsWithChanges(tableId, isNewTable, tenderId);
            var changeRequest = tender.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables).LastOrDefault();
            if (changeRequest == null)
            {
                changeRequest = tender.CreateTenderQuantityTableChangeRequest(_httpContextAccessor.HttpContext.User.UserId(), _httpContextAccessor.HttpContext.User.UserRole());
                await _tenderCommands.UpdateAsync(tender);
            }
            changeRequest.DeleteTenderQuantityTableWithItems(tableId);
            await _tenderCommands.UpdateAsync(tender);
        }

        public async Task ChangeHasAlternativeOffer(int tenderId, bool hasAlternativeOffer)
        {
            Tender tender = await _tenderQueries.GetTenderWithFormQuantityItemsWithChanges(0, false, tenderId);
            var changeRequest = tender.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables).LastOrDefault();
            if (changeRequest == null)
            {
                changeRequest = tender.CreateTenderQuantityTableChangeRequest(_httpContextAccessor.HttpContext.User.UserId(), _httpContextAccessor.HttpContext.User.UserRole());
                await _tenderCommands.UpdateAsync(tender);
            }
            changeRequest.ChangeHasAlternativeOffer(hasAlternativeOffer);
            await _tenderCommands.UpdateAsync(tender);
        }

        public async Task DeleteQuantityTableChangeRequest(int tenderId)
        {
            var cr = await _tenderQueries.GetQTChangeRequest(tenderId);
            var crTables = cr.TenderQuantityTableChanges.Where(d => d.IsActive == true).ToList();
            var tableids = crTables.Select(d => d.Id).ToList();
            var tablejsons = await _tenderQueries.GetQTItemsByTableIds(tableids);
            foreach (var item in cr.TenderQuantityTableChanges)
            {
                item.QuantitiyItemsChangeJson = tablejsons.FirstOrDefault(d => d.TenderQuantityTableChangeId == item.Id);
                if (item.QuantitiyItemsChangeJson == null)
                    continue;
                item.QuantitiyItemsChangeJson.Delete();
                _genericCommandRepository.Update(item);
            }
            await _genericCommandRepository.SaveAsync();

            foreach (var item in cr.TenderQuantityTableChanges)
            {
                item.Delete();
                _genericCommandRepository.Update(item);
            }
            await _genericCommandRepository.SaveAsync();

            cr.Delete();
            _genericCommandRepository.Update(cr);
            await _genericCommandRepository.SaveAsync();
        }

        public async Task DeleteExistingTableChange(long tableId, int tenderId)
        {
            Tender tender = await _tenderQueries.GetTenderWithChangeRequest(tenderId);
            var changeRequest = tender.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables).LastOrDefault();
            if (changeRequest == null)
            {
                changeRequest = tender.CreateTenderQuantityTableChangeRequest(_httpContextAccessor.HttpContext.User.UserId(), _httpContextAccessor.HttpContext.User.UserRole());
                await _tenderCommands.UpdateAsync(tender);
            }
            var deletedTableId = await AddNewTableChange(0, tender.TenderId, 0, "");
            changeRequest.DeleteExistingTenderQuantityTable(deletedTableId, tableId);
            await _tenderCommands.UpdateAsync(tender);
        }


        public async Task<int> GetQauntityTableVersionId()
        {
            var qauntityTableVersionId = await _templatesProxy.GetQuantityTableVersion();

            return qauntityTableVersionId;

        }

        public async Task RemoveUnRegisterSupplier(int tenderId, string crNumber)
        {
            Tender tender = await _tenderQueries.FindTenderInvitationsWithById(tenderId);
            tender.RemoveInvitationsForUnregisteredSupplierByCrAndTenderId(tenderId, crNumber);
            await _tenderCommands.UpdateAsync(tender);
        }


        public async Task<List<TenderQuantityItemDTO>> ExportTenderTableQuantityItems(int tableId, bool isNewChange)
        {
            if (isNewChange)
                return await _tenderQueries.GetTenderTableQuantityItemsForNewByTableId(tableId);
            else
                return await _tenderQueries.GetTenderTableQuantityItemsByTableId(tableId);
        }

        public async Task ImportTenderTableQuantityItems(UploadTableExcelModel model)
        {
            int tenderId = model.TenderId;
            int formId = model.FormId;
            long table = model.TableId;
            Tender tender = await _tenderQueries.GetTenderWithFormQuantityItemsWithChanges(table, model.IsNewTable.HasValue && model.IsNewTable.Value, tenderId);
            long createdTable = table;
            ApiResponse<HtmlTableRowTemplateDto> lst = await _templatesProxy.ImportTenderTableQuantityItems(model);
            if (lst.Data == null)
                throw new BusinessRuleException(Resources.TenderResources.Messages.ErrorInImportedData);

            if (lst.StatusCode == (int)QTSStatusCode.ExcutionFailed)
            {
                throw new BusinessRuleException(lst.Message.ToString());
            }
            lst.Data.QuantityItemDto.ForEach(x => x.TableId = createdTable);
            if (!model.IsNewTable.HasValue || !model.IsNewTable.Value)
            {
                tender.UpdateTenderStatus(Enums.TenderStatus.UnderEstablishing);
                tender.SaveTenderQuantityTableItems(lst.Data.QuantityItemDto, model.TableName);
            }
            else
            {
                var changeRequest = tender.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables).LastOrDefault();
                changeRequest.SaveTenderQuantityTableItems(lst.Data.QuantityItemDto, model.TableName);
            }
            await _tenderCommands.UpdateAsync(tender);
        }

        public async Task<bool> ResendInvitationToSuppliersAsync(SupplierSearchCretriaModel cretria)
        {
            bool isResend = false;
            Tender tender = await _tenderQueries.GetTenderByTenderIdWithInvitations(cretria.InvitationTenderId);
            NotificationArguments SupplierNotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "",
                    _httpContextAccessor.HttpContext.User.UserAgencyName(),
                    tender.Branch?.BranchName,
                    tender.ReferenceNumber,
                    tender.TenderName,
                    tender.Purpose,
                    tender.LastEnqueriesDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.LastEnqueriesDate.Value.ToString("dd/MM/yyyy"),
                    tender.LastOfferPresentationDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.LastOfferPresentationDate.Value.ToString("dd/MM/yyyy"),
                    tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"),
                    tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { "", tender.ReferenceNumber },
                SMSArgs = new object[] { "", tender.ReferenceNumber }
            };
            List<string> lstCrs = new List<string>();
            MainNotificationTemplateModel inviteSupplierModel = new MainNotificationTemplateModel(SupplierNotificationArguments, $"Tender/SupplierInvitationTenders", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId);
            foreach (var item in tender.Invitations.Where(x => x.StatusId == (int)Enums.InvitationStatus.Rejected || x.StatusId == (int)Enums.InvitationStatus.Withdrawal || (x.StatusId == (int)Enums.InvitationStatus.ToBeSent && x.InvitedByQualification == true)))
            {
                var checkBlockList = await _blockAppService.CheckifSupplierBlockedByCrNo(item.CommericalRegisterNo);
                if (!checkBlockList)
                {
                    item.UpdateStatusForResend();
                    lstCrs.Add(item.CommericalRegisterNo);
                    isResend = true;
                }
            }
            await _tenderCommands.UpdateAsync(tender);
            if (lstCrs.Count > 0)
            {
                tender.SendInvitations(lstCrs);
                await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.invitevendorstotender, lstCrs, inviteSupplierModel);
            }
            return isResend;
        }

        public async Task RemoveAttachmentChangeById(int attachmentId)
        {
            var attachment = await _tenderQueries.GetAttachmentById(attachmentId);
            attachment.DeActive();
            await _tenderCommands.UpdateAttachmentChangeAsync(attachment);
        }
        public async Task<List<MandatoryListForExportModel>> GetAllMandatoryListForExport()
        {
            var result = await _tenderQueries.GetAllMandatoryListForExport();
            return result;
        }
        public async Task<TenderDetailsForAppliedSuppliersReportModel> GetTenderDetailsForAppliedSuppliersReport(int tenderId)
        {
            var result = await _tenderQueries.GetTenderDetailsForAppliedSuppliersReport(tenderId);
            return result;
        }
        public async Task<TenderDetailsCheckingStageModel> GetTenderDetailsForCheckingStage(int tenderId, int userId, string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Check.ArgumentNotNullOrEmpty(nameof(userId), userId.ToString());
            Check.ArgumentNotNullOrEmpty(nameof(agencyCode), agencyCode);
            var tenderOffersModel = await _tenderQueries.GetTenderDetailsForCheckingStage(tenderId, userId, agencyCode);
            if (tenderOffersModel == null)
                throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            if (tenderOffersModel.EstimatedValue != null)
            {
                ConvertNumberToText obj = new ConvertNumberToText(tenderOffersModel.EstimatedValue.Value, new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                tenderOffersModel.EstimatedValueText = obj.ConvertToArabic();
            }
            return tenderOffersModel;
        }

        #region LCG

        public async Task<TenderInfo> GetTenderInfo(string tenderReferenceId)
        {
            var result = await _tenderQueries.FindTenderInfo(tenderReferenceId);

            return result;
        }

        public async Task<QueryResult<TenderInfo>> GetTenderList(string supplierId, int pageSize, int pageNumber)
        {

            var result = await _tenderQueries.FindTenderList(supplierId, pageSize, pageNumber);

            return result;
        }



        #endregion

        public async Task<bool> CheckIfActivityCanHasTawreed(ActivityVersionModel activityVersionModel)
        {
            var checkTawreed = true;
            if (activityVersionModel.ActivityIds != null)
            {
                var templateIds = await _tenderQueries.GetTemplatesByActivityIdsAndVersionId(activityVersionModel.ActivityIds, activityVersionModel.ActivityVersionId);
                if ((templateIds.Count == 1 && templateIds.Contains((int)Enums.ActivityTemplate.ConsultingServices)) || templateIds.Contains((int)Enums.ActivityTemplate.TawreedActivityVersion4))
                    checkTawreed = false;

            }

            return checkTawreed;

        }

        public async Task<int> GetCurrentTenderActivityVersion(int tenderId)
        {
            int tenderActivityVersionId = await _tenderQueries.GetCurrentTenderActivityVersion(tenderId);

            return tenderActivityVersionId;
        }


    }
}
