using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.AgencyBudget;
using MOF.Etimad.Monafasat.ViewModel.DTO;
using MOF.Etimad.Monafasat.ViewModel.Tender;
using MOF.Etimad.Monafasat.ViewModel.Tender.LocalContent;
using MOF.Etimad.Monafasat.WebApi.Infrastructure;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TenderController : BaseController
    {
        private readonly ISupplierService _supplierService;
        private readonly ITenderAppService _tenderService;
        private readonly IBlockAppService _blockService;
        private readonly ILookUpService _lookupService;
        private readonly IOfferAppService _offerAppService;
        private readonly IIDMAppService _iDMAppService;
        private readonly IVerificationService _verification;
        private readonly IMapper _mapper;
        private readonly INotificationAppService _notificationAppService;
        private readonly IAgencyBudgetService _agencyBudgetService;
        private readonly IWathiqService _iWathiqService;

        public TenderController(ITenderAppService tenderService, IMapper mapper
            , IIDMAppService iDMAppService, ISupplierService supplierService, IOfferAppService offerAppService, ILookUpService lookupService, IBlockAppService blockService
            , INotificationAppService notificationAppService, IVerificationService verification, IAgencyBudgetService agencyBudgetService, IWathiqService iWathiqService, IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
            _tenderService = tenderService;
            _offerAppService = offerAppService;
            _iDMAppService = iDMAppService;
            _supplierService = supplierService;
            _lookupService = lookupService;
            _mapper = mapper;
            _blockService = blockService;
            _agencyBudgetService = agencyBudgetService;
            _notificationAppService = notificationAppService;
            _verification = verification;
            _iWathiqService = iWathiqService;

        }

        [Authorize(RoleNames.IndexPolicy)]
        [HttpGet]
        [Route("GetAllTendersForIndexPage")]
        public async Task<QueryResult<AllTendersViewModel>> GetAllTendersForIndexPage(TenderSearchCriteria tenderSearchCriteria)
        {
            if (!User.IsInRole(RoleNames.MonafasatAccountManager) && !User.IsInRole(RoleNames.CustomerService))
            {
                tenderSearchCriteria.AgencyCode = User.UserAgency();
                if (!User.IsInRole(RoleNames.MonafasatAdmin) && !User.IsInRole(RoleNames.FinancialSupervisor))
                    tenderSearchCriteria.BranchId = User.UserBranch();
            }
            if (User.UserIsVRO())
                tenderSearchCriteria.IsVro = true;
            tenderSearchCriteria.SelectedCommitteeId = User.SelectedCommittee();
            tenderSearchCriteria.UserRole = User.UserRole();
            var tenderList = await _tenderService.GetAllTendersForIndexPage(tenderSearchCriteria);
            return tenderList;
        }

        [Authorize(RoleNames.IndexPolicy)]
        [HttpGet]
        [Route("GetTendersBySearchCriteria")]
        public async Task<QueryResult<BasicTenderModel>> GetTendersBySearchCriteria(TenderSearchCriteria tenderSearchCriteria)
        {
            if (!User.IsInRole(RoleNames.MonafasatAccountManager) && !User.IsInRole(RoleNames.CustomerService))
            {
                tenderSearchCriteria.AgencyCode = User.UserAgency();
                if (!User.IsInRole(RoleNames.MonafasatAdmin) && !User.IsInRole(RoleNames.FinancialSupervisor))
                    tenderSearchCriteria.BranchId = User.UserBranch();
            }
            if (User.UserIsVRO())
                tenderSearchCriteria.IsVro = true;
            tenderSearchCriteria.SelectedCommitteeId = User.SelectedCommittee();
            tenderSearchCriteria.UserRole = User.UserRole();
            var tenderList = await _tenderService.FindTenderBySearchCriteriaForIndexPage(tenderSearchCriteria);
            return tenderList;
        }
        [HttpGet]
        [Route("GetTendersByAgencyCode")]
        public async Task<QueryResult<BasicTenderModel>> GetTendersByAgencyCode(TenderSearchCriteria tenderSearchCriteria)
        {
            if (!User.IsInRole(RoleNames.MonafasatAccountManager) && !User.IsInRole(RoleNames.CustomerService))
            {
                tenderSearchCriteria.AgencyCode = User.UserAgency();
                if (!User.IsInRole(RoleNames.MonafasatAdmin))
                    tenderSearchCriteria.BranchId = User.UserBranch();
            }
            if (User.UserIsVRO())
                tenderSearchCriteria.IsVro = true;
            tenderSearchCriteria.SelectedCommitteeId = User.SelectedCommittee();
            var tenderList = await _tenderService.FindTenderBySearchCriteriaForIndexPage(tenderSearchCriteria);
            return tenderList;
        }

        [HttpGet]
        [Route("FindTenderByAgencyCodeForPurchaseMethod")]
        public async Task<int[]> FindTenderByAgencyCodeForPurchaseMethod(TenderSearchCriteria criteria)
        {
            var tenderList = await _tenderService.FindTenderByAgencyCodeForPurchaseMethod(criteria.AgencyCode);
            return tenderList.ToArray();
        }


        [Authorize(RoleNames.SupplierPolicy)]
        [HttpGet]
        [Route("GetAllSupplierTenders")]
        public async Task<QueryResult<TenderInvitationDetailsModel>> GetAllSupplierTenders(TenderSearchCriteria criteria)
        {
            criteria.cr = User.SupplierNumber();
            var blockedSuplliers = await _blockService.GetAllSupplierBlocks(criteria.AgencyCode, new List<string> { criteria.cr });
            var tendersListModel = await _tenderService.GetAllSupplierTenders(criteria, blockedSuplliers);
            return tendersListModel;
        }
        [HttpGet]
        [Route("GetBasicOfferTenderInfoById/{id}")]
        public async Task<BasicOfferTenderInfoModel> GetBasicOfferTenderInfoById(int id)
        {
            BasicOfferTenderInfoModel basicOfferTenderInfoModel = await _tenderService.GetBasicOfferTenderInfoById(id);
            return basicOfferTenderInfoModel;
        }
        [Authorize(RoleNames.IndexPolicy)]
        [HttpGet]
        [Route("GetTendersForAppliedSuppliersReport")]
        public async Task<QueryResult<BasicTenderModel>> GetTendersForAppliedSuppliersReport(TenderSearchCriteria tenderSearchCriteria)
        {
            tenderSearchCriteria.AgencyCode = String.IsNullOrWhiteSpace(tenderSearchCriteria.AgencyCode) ? User.UserAgency() : tenderSearchCriteria.AgencyCode;
            if (!User.IsInRole(RoleNames.MonafasatAccountManager) && !User.IsInRole(RoleNames.MonafasatAdmin))
                tenderSearchCriteria.BranchId = User.UserBranch();
            var tenderList = await _tenderService.FindTenderForAppliedSuppliersReport(tenderSearchCriteria);
            return tenderList;
        }
        [HttpGet]
        [Route("GetTendersForCheckingDirectPuchaseStageAsync")]
        public async Task<QueryResult<TenderCheckingAndAwardingModel>> GetTendersForCheckingDirectPuchaseStageAsync(TenderSearchCriteria tenderSearchCriteria)
        {
            tenderSearchCriteria.UserId = User.UserId();
            tenderSearchCriteria.SelectedAgencyCode = User.UserAgency();
            tenderSearchCriteria.SelectedCommitteeId = User.SelectedCommittee();
            var tenderList = await _tenderService.GetTendersForCheckingDirectPuchaseStageAsync(tenderSearchCriteria);
            return tenderList;
        }
        [Authorize(RoleNames.IndexPolicy)]
        [HttpGet]
        [Route("GetTendersToJoinCommittees")]
        public async Task<QueryResult<LinkTendersToCommitteeModel>> GetTendersToJoinCommittees(LinkTendersToCommitteeSearchCriteriaModel criteria)
        {
            criteria.SelectedAgencyCode = User.UserAgency();
            var tenderList = await _tenderService.GetTendersToJoinCommittees(criteria);
            return tenderList;
        }

        //[Authorize(PolicyNames.UnderOperationTenderPolicy)]
        [Authorize(RoleNames.DataEntryAndAuditerPolicy)]

        [HttpGet]
        [Route("GetTendersForUnderOperationsStageIndex")]
        public async Task<QueryResult<BasicTenderModel>> GetTendersForUnderOperationsStageIndex(TenderSearchCriteria tenderSearchCriteria)
        {
            tenderSearchCriteria.SelectedAgencyCode = User.UserAgency();
            tenderSearchCriteria.BranchId = User.UserBranch();
            tenderSearchCriteria.UserId = User.UserId();
            if (User.UserIsVRO())
                tenderSearchCriteria.IsVro = true;
            var tenderList = await _tenderService.FindTendersForUnderOperationsStageBySearchCriteria(tenderSearchCriteria);
            return tenderList;
        }

        [Authorize(RoleNames.OffersOppeningSecretaryAndManagerPolicy)]
        [HttpGet]
        [Route("GetTendersForOpeningStageIndex")]
        public async Task<QueryResult<TenderCheckingAndAwardingModel>> GetTendersForOpeningStageIndex(TenderSearchCriteria tenderSearchCriteria)
        {

            tenderSearchCriteria.UserId = User.UserId();
            tenderSearchCriteria.SelectedAgencyCode = User.UserAgency();
            tenderSearchCriteria.SelectedCommitteeId = User.SelectedCommittee();
            tenderSearchCriteria.UserRole = User.UserRole();
            QueryResult<TenderCheckingAndAwardingModel> tenderList = await _tenderService.FindTendersForOpeningStageBySearchCriteria(tenderSearchCriteria);
            return tenderList;
        }
        [HttpGet]
        [Route("GetSupplierInfoByCR/{supplierCR}")]
        public async Task<SupplierFullDataViewModel> GetSupplierInfoByCR(string supplierCR)
        {
            var supplierFullDataModel = await _tenderService.GetSupplierInfoByCR(supplierCR);
            return supplierFullDataModel;
        }

        [Authorize(RoleNames.GetTendersCheckingStagePolicy)]
        [HttpGet]
        [Route("GetTendersForCheckingStageIndex")]
        public async Task<QueryResult<TenderCheckingAndAwardingModel>> GetTendersForCheckingStageIndex(TenderSearchCriteria tenderSearchCriteria)
        {
            tenderSearchCriteria.UserId = User.UserId();
            tenderSearchCriteria.SelectedAgencyCode = User.UserAgency();
            tenderSearchCriteria.SelectedCommitteeId = User.SelectedCommittee();
            var tenderListModel = await _tenderService.FindTendersForCheckingStageBySearchCriteria(tenderSearchCriteria);
            return tenderListModel;
        }

        [Authorize]
        [HttpGet]
        [Route("GetVROTendersCreatedByGovAgency")]
        public async Task<QueryResult<VROTendersCreatedByGovAgencyModel>> GetVROTendersCreatedByGovAgency(TenderSearchCriteria tenderSearchCriteria)
        {
            tenderSearchCriteria.UserId = User.UserId();
            tenderSearchCriteria.SelectedAgencyCode = User.UserAgency();
            tenderSearchCriteria.BranchId = User.UserBranch();
            tenderSearchCriteria.UserRole = User.UserRole();
            QueryResult<VROTendersDTO> tendersDTO = await _tenderService.GetVROTendersCreatedByGovAgency(tenderSearchCriteria);
            QueryResult<VROTendersCreatedByGovAgencyModel> VROTendersViewModel = _mapper.Map<QueryResult<VROTendersCreatedByGovAgencyModel>>(tendersDTO);
            return VROTendersViewModel;
        }

        [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
        [HttpGet]
        [Route("GetTendersForAwardingStageIndex")]
        public async Task<QueryResult<TenderCheckingAndAwardingModel>> GetTendersForAwardingStageIndex(TenderSearchCriteria tenderSearchCriteria)
        {
            tenderSearchCriteria.UserRole = User.UserRole();
            tenderSearchCriteria.SelectedAgencyCode = User.UserAgency();
            tenderSearchCriteria.BranchId = User.UserBranch();
            tenderSearchCriteria.SelectedCommitteeId = User.SelectedCommittee();
            tenderSearchCriteria.UserId = User.UserId();
            var tenderList = await _tenderService.FindTendersForAwardingStageBySearchCriteria(tenderSearchCriteria);
            return tenderList;
        }

        [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
        [HttpPost]
        [Route("RejectInitialAwardTenderOffer")]
        public async Task RejectInitialAwardTenderOffer([FromBody] RejectionModel rejectionModel)
        {
            await _tenderService.RejectInitialAwardTenderOffer(rejectionModel.TenderId, rejectionModel.RejectionReason, User.SupplierAgency());
        }

        [Authorize(RoleNames.AuditerAndTechnicalPolicy)]
        [HttpGet("GetAllTendersHasEnquiry")]
        public async Task<QueryResult<TenderInformationModel>> GetAllTendersHasEnquiry(TenderSearchCriteria criteria)
        {
            criteria.UserId = User.UserId();
            criteria.BranchId = User.UserBranch();
            var result = await _tenderService.GetAllTendersHasEnquiry(criteria);
            return result;
        }

        // [Authorize(RoleNames.GetFinancialYearPolicy)]
        [HttpGet]
        [Route("GetFinancialYear")]
        public async Task<List<string>> GetFinancialYear()
        {
            var yers = await _tenderService.FinancialYear();
            return yers;
        }

        [HttpPost]
        [Route("CreateVerificationCode")]
        public async Task CreateVerificationCode([FromBody] BasicTenderModel basicTender)
        {
            var userEmail = User.Email();
            var userMobile = User.Mobile();
            await _verification.CreateVerificationCode(basicTender.TenderId, userMobile, userEmail, User.UserId(), (int)Enums.VerificationType.Tender);
        }

        [HttpGet]
        [Route("GetStatus")]
        public async Task<List<LookupModel>> GetStatus()
        {

            if (User.UserIsVRO())
            {
                return await _lookupService.GetVROTenderStatusLookup();
            }
            else
            {
                var tenderStatusList = await _lookupService.GetTenderStatusLookup();
                return tenderStatusList;
            }
        }
        [HttpGet]
        [Route("GetApprovedStatuses")]
        public async Task<List<LookupModel>> GetApprovedStatuses()
        {
            var tenderStatusList = await _lookupService.GetApprovedTenderStatusLookup();
            return tenderStatusList;
        }

        [Authorize(RoleNames.DataEntryAndSupplierPolicy)]
        [HttpGet]
        [Route("GetByTenderIdInvitationDetails/{id}")]
        public async Task<InvitationStepModel> GetByTenderIdInvitationDetails(int id)
        {
            string cr = User.SupplierNumber();
            InvitationStepModel invitationStepModel = await _tenderService.GetTenderDetailsForInvitationStep(id, cr);
            Check.ArgumentNotNull(nameof(invitationStepModel), invitationStepModel);
            return invitationStepModel;
        }

        [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
        [HttpPost]
        [Route("SendTenderToApproveCheckingWithNewBiddingRound")]
        public async Task SendTenderToApproveCheckingWithNewBiddingRound([FromBody] BiddingDateTimeViewModel biddingDateTimeViewModel)
        {
            await _tenderService.SendTenderToApproveCheckingWithNewBiddingRound(biddingDateTimeViewModel);
        }

        [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
        [HttpPost]
        [Route("SaveCheckingDateTime")]
        public async Task SaveCheckingDateTime([FromBody] CheckingDateTimeViewModel checkingDateTimeViewModel)
        {
            await _tenderService.SaveCheckingDateTime(checkingDateTimeViewModel);
        }

        [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
        [HttpPost]
        [Route("SaveFinancialCheckingDateTime")]
        public async Task SaveFinancialCheckingDateTime([FromBody] CheckingDateTimeViewModel financialCheckingDateTimeViewModel)
        {
            await _tenderService.SaveFinancialCheckingDateTime(financialCheckingDateTimeViewModel);
        }

        [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
        [HttpPost]
        [Route("ReopenTenderChecking/{id}")]
        public async Task ReopenTenderChecking(int id)
        {
            await _tenderService.ReopenTenderChecking(id);
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpPost]
        [Route("ReopenTender/{id}")]
        public async Task ReopenTender(int id)
        {
            await _tenderService.ReopenTender(id);
        }

        [Authorize(RoleNames.CreateCancelTenderRequestPolicy)]
        [HttpPost]
        [Route("ReopenTenderAfterCancel/{id}")]
        public async Task ReopenTenderAfterCancel(int id)
        {
            await _tenderService.ReopenTenderAfterCancel(id);
        }

        [Authorize(RoleNames.CreateCancelTenderRequestPolicy)]
        [HttpPost]
        [Route("CancelTenderCancellationRequest/{tenderId}")]
        public async Task CancelTenderCancellationRequest(int tenderId)
        {
            await _tenderService.CancelTenderCancellationRequest(tenderId);
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpPost]
        [Route("SendTenderToApprove/{id}")]
        public async Task SendTenderToApprove(int id)
        {
            await _tenderService.SendTenderToApprove(id);
        }

        [Authorize(RoleNames.OffersOppeningSecretaryPolicy)]
        [HttpPost]
        [Route("SendTenderToApproveOppenning/{id}")]
        public async Task SendTenderToApproveOppenning(int id)
        {
            await _tenderService.SendTenderToApproveOppenning(id);
        }

        [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
        [HttpPost]
        [Route("SendTenderToApproveChecking/{id}")]
        public async Task SendTenderToApproveChecking(int id)
        {
            await _tenderService.SendTenderToApproveChecking(id);
        }

        [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
        [HttpPost]
        [Route("ReopenTenderAwarding/{id}")]
        public async Task ReopenTenderAwarding(int id)
        {
            await _tenderService.ReopenTenderAwarding(id);
        }

        [Authorize(RoleNames.OffersCheckManagerPolicy)]
        [HttpPost]
        [Route("ApproveTenderChecking/{id}")]
        public async Task ApproveTenderChecking(int id)
        {
            await _tenderService.ApproveTenderChecking(id);
        }

        [HttpPost]
        [Route("EmarketPlace")]
        public async Task<bool> EmarketPlace([FromBody] List<int> offerIds)
        {
            var result = await _tenderService.EmarketPlace(offerIds);
            return result;
        }

        [Authorize(RoleNames.OffersOppeningManagerPolicy)]
        [HttpPost]
        [Route("ApproveTenderOppening/{id}/{verificationCode}")]
        public async Task ApproveTenderOppening(int id, string verificationCode)
        {
            int typeId = (int)Enums.VerificationType.Tender;
            await _verification.CheckForVerificationCode(id, verificationCode, typeId);
            await _tenderService.ApproveTenderOppening(id);
        }

        [Authorize(RoleNames.OffersCheckManagerPolicy)]
        [HttpPost]
        [Route("ApproveTenderCheckingWithVerification/{id}/{verificationCode}")]
        public async Task ApproveTenderCheckingWithVerification(int id, string verificationCode)
        {
            int typeId = (int)Enums.VerificationType.Tender;
            await _verification.CheckForVerificationCode(id, verificationCode, typeId);
            await _tenderService.ApproveTenderChecking(id);
        }

        [Authorize(RoleNames.AuditerPolicy)]
        [HttpPost]
        [Route("ApproveTenderWithInbudget/{id}/{verificationCode}/{iagree}/{competitionIsBudgeted}")]
        public async Task ApproveTenderWithInbudget(int id, string verificationCode, bool iagree, bool competitionIsBudgeted)
        {
            await _tenderService.ApproveTenderWithInbudget(id, verificationCode, iagree, competitionIsBudgeted);
        }

        [Authorize(RoleNames.AuditerPolicy)]
        [HttpPost]
        [Route("ApproveTenderRelatedWithVRO/{id}")]
        public async Task ApproveTenderRelatedWithVRO(int id)
        {
            await _tenderService.ApproveTenderRelatedWithVRO(id);
        }


        [Authorize(RoleNames.AuditerPolicy)]
        [HttpPost]
        [Route("ApproveTenderVRO")]
        public async Task ApproveTenderVRO([FromBody] ApproveVROModel vROModel)
        {
            int branchId = User.UserBranch();
            await _tenderService.ApproveTenderVRO(vROModel, branchId);
        }

        #region TenderUpdates



        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpPost]
        [Route("SendUpdateQuantityTableToApprove/{id}")]
        public async Task SendUpdateQuantityTableToApprove(int id)
        {
            await _tenderService.SendUpdateQuantityTableToApprove(id);
        }

       

        [Authorize(Roles = RoleNames.Auditer + "," + RoleNames.EtimadOfficer + "," + RoleNames.PreQualificationCommitteeManager)]
        [HttpPost]
        [Route("ApproveTenderExtendDatesChangeRequest/{id}/{verificationCode}")]
        public async Task ApproveTenderExtendDatesChangeRequest(int id, string verificationCode)
        {
            int typeId = (int)Enums.VerificationType.Tender;
            await _tenderService.ApproveTenderExtendDatesChangeRequest(id, verificationCode, typeId);
        }

        [Authorize(RoleNames.AuditerPolicy)]
        [HttpPost]
        [Route("ApproveTenderQuantityTablesChangeRequest/{id}/{verificationCode}")]
        public async Task ApproveTenderQuantityTablesChangeRequest(int id, string verificationCode)
        {
            int typeId = (int)Enums.VerificationType.Tender;
            await _tenderService.ApproveTenderQuantityTablesChangeRequest(id, verificationCode, typeId);
        }


        [HttpPost]
        [Authorize(Roles = RoleNames.Auditer + "," + RoleNames.EtimadOfficer + "," + RoleNames.PreQualificationCommitteeManager)]
        [Route("ApproveTenderAttachmentsChangeRequest/{id}/{verificationCode}")]
        public async Task ApproveTenderAttachmentsChangeRequest(int id, string verificationCode)
        {
            int typeId = (int)Enums.VerificationType.Tender;
            await _tenderService.ApproveTenderAttachmentsChangeRequest(id, verificationCode, typeId);
        }

        [Authorize(RoleNames.AuditerPolicy)]
        [HttpPost]
        [Route("RejectTenderUpdateQuantityTable")]
        public async Task RejectTenderUpdateQuantityTable([FromBody] RejectionModel rejectionModel)
        {
            await _tenderService.RejectTenderForUpdateQuantityTable(rejectionModel.TenderId, rejectionModel.RejectionReason);
        }


        [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.PurshaseSpecialist)]
        [HttpPost]
        [Route("SendUpdateAttachmentsToApprove/{id}")]
        public async Task SendUpdateAttachmentsToApprove(int id)
        {
            await _tenderService.SendUpdateAttachmentsToApprove(id);
        }

        [Authorize(RoleNames.ApproveSupplierExtendOfferDatesPolicy)]
        [HttpPost]
        [Route("SendUpdateDatesToApprove/{id}")]
        public async Task SendUpdateDatesToApprove(int id)
        {
            await _tenderService.SendUpdateDatesToApprove(id);
        }

        [Authorize(RoleNames.AuditerPolicy)]
        [HttpPost]
        [Route("RejectTenderExtendDate")]
        public async Task RejectTenderExtendDate([FromBody] RejectionModel rejectionModel)
        {
            await _tenderService.RejectTenderExtendDate(rejectionModel.TenderId, rejectionModel.RejectionReason);
        }
        [HttpPost]
        [Route("RejectQualificationExtendDate")]
        [Authorize(Roles = RoleNames.Auditer + "," + RoleNames.EtimadOfficer + "," + RoleNames.PreQualificationCommitteeManager)]
        public async Task RejectQualificationExtendDate([FromBody] RejectionModel rejectionModel)
        {
            await _tenderService.RejectTenderExtendDate(rejectionModel.TenderId, rejectionModel.RejectionReason);
        }
        [HttpPost]
        [Authorize(Roles = RoleNames.Auditer + "," + RoleNames.EtimadOfficer + "," + RoleNames.PreQualificationCommitteeManager)]
        [Route("RejectTenderUpdateAttachment")]
        public async Task RejectTenderUpdateAttachment([FromBody] RejectionModel rejectionModel)
        {
            await _tenderService.RejectTenderUpdateAttachment(rejectionModel.TenderId, rejectionModel.RejectionReason);
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpPost]
        [Route("CancelQuantityTableUpdate/{id}")]
        public async Task CancelQuantityTableUpdate(int id)
        {
            await _tenderService.CancelUpdatesInQuantitiesTablesAsync(id);
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.PurshaseSpecialist)]
        [Route("CancelAttachmentsUpdate/{id}")]
        public async Task CancelAttachmentsUpdate(int id)
        {
            await _tenderService.CancelUpdatesInAttachmentsAsync(id);
        }

        [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.PurshaseSpecialist)]
        [HttpPost]
        [Route("CancelTenderExtendDates/{id}")]
        public async Task CancelTenderExtendDates(int id)
        {
            await _tenderService.CancelTenderExtendDateAsync(id);
        }

        #endregion

        [Authorize(RoleNames.SupplierPolicy)]
        [HttpGet]
        [Route("IsTenderPurchasedBySupplier/{tenderId}")]
        public async Task<bool> IsTenderPurchasedBySupplier(int tenderId)
        {
            var CR = User.SupplierNumber();

            bool isPurchased = await _tenderService.IsTenderPurchasedBySupplier(tenderId, CR);

            return isPurchased;
        }

        [Authorize(RoleNames.AuditerPolicy)]
        [HttpPost]
        [Route("RejectTender")]
        public async Task RejectTender([FromBody] RejectionModel rejectionModel)
        {
            await _tenderService.RejectTender(rejectionModel.TenderId, rejectionModel.RejectionReason);

        }

        [HttpGet]
        [Route("FindAllVacationDates")]
        public async Task<List<VacationsDateModel>> FindAllVacationDates()
        {
            var vacations = await _tenderService.FindAllVacationDates();
            var model = _mapper.Map<List<VacationsDateModel>>(vacations);
            return model;
        }
        [HttpGet]
        [Route("GetBasicTenderDetailsById/{tenderIdString}")]

        public async Task<AllBasicTenderDataModel> GetBasicTenderDetailsById(string tenderIdString)
        {
            var tenderBasicDataModel = await _tenderService.GetBasicTenderDetailsById(Util.Decrypt(tenderIdString), User.UserAgency());
            return tenderBasicDataModel;
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("GetMainTenderDetailsById/{id}")]
        public async Task<TenderDetailsModel> GetMainTenderDetailsById(string id)
        {
            int branchId = User.UserBranch();
            int tenderId = Util.Decrypt(id);
            TenderDetailsModel tenderDetailsModel = await _tenderService.GetMainTenderDetailsByTenderId(tenderId, branchId);
            return tenderDetailsModel;
        }

        [HttpGet]
        [Route("GetMainTenderDetailsForUnit/{tenderId}")]
        public async Task<TenderDetailsModel> GetMainTenderDetailsForUnit(string tenderId)
        {
            TenderDetailsModel tenderDetailsModel = await _tenderService.GetMainTenderDetailsForUnit(Util.Decrypt(tenderId), User.UserAgency());
            return tenderDetailsModel;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetMainTenderDetailsForSuppliers/{tenderId}")]
        public async Task<TenderDetailsModel> GetMainTenderDetailsForSuppliers(string tenderId)
        {
            string cr = User.SupplierNumber();
            TenderDetailsModel tenderDetailsModel = await _tenderService.GetMainTenderDetailsForSuppliers(Util.Decrypt(tenderId), cr);
            return tenderDetailsModel;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetMainTenderDetailsForVisitor/{tenderId}")]
        public async Task<TenderDetailsModel> GetMainTenderDetailsForVisitor(string tenderId)
        {
            TenderDetailsModel tenderDetailsModel = await _tenderService.GetMainTenderDetailsForVisitor(Util.Decrypt(tenderId));
            return tenderDetailsModel;
        }

        [Authorize(RoleNames.AdminAndAccountManagerPolicy)]
        [HttpGet]
        [Route("GetTendersByLogedUser")]
        public async Task<QueryResult<BasicTenderModel>> GetTendersByLogedUser()
        {
            bool statusFlag = GetStatusFlag();
            string agencyCode = User.UserAgency();
            var tenderList = await _tenderService.FindTendersByLogedUser(agencyCode);
            var tenderListModel = _mapper.Map<QueryResult<BasicTenderModel>>(tenderList, opt => opt.Items[nameof(statusFlag)] = statusFlag);
            return tenderListModel;
        }

        #region Create/Update Tender

        [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
        [HttpGet]
        [Route("GetBasicTenderDataById/{id}")]
        public async Task<CreateTenderBasicDataModel> GetBasicTenderDataById(int id)
        {
            CreateTenderBasicDataModel tenderBasicDataModel;
            int branchId = User.UserBranch();
            tenderBasicDataModel = await _tenderService.GetBasicTenderByIdAndBranch(id, branchId);
            return tenderBasicDataModel;
        }

        [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
        [HttpGet]
        [Route("GetBasicTenderLookups/{tenderId}")]
        public async Task<BasicStepLookupsModel> GetBasicTenderLookups(int tenderId)
        {
            BasicStepLookupsModel Model = new BasicStepLookupsModel();

            Model.Qualification = await _lookupService.GetAllPreQualifications(tenderId, User.UserAgency(), User.UserBranch());
            Model.TenderTypes = await _lookupService.GetAgencyPurchaseMethodsModels(User.UserAgency());
            Model.allCommittees = await GetCommitteeByCommitteeTypeOnBranch();
            return Model;
        }


        [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
        [HttpGet]
        [Route("GetTenderTypeLookups")]
        public async Task<List<LookupModel>> GetTenderTypeLookups()
        {
            List<LookupModel> tenderTypes = await _lookupService.GetAgencyPurchaseMethodsModels(User.UserAgency());
            return tenderTypes;
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpGet]
        [Route("GetTenderAgreementAgenciesLookup")]
        public async Task<List<LookupModel>> GetTenderAgreementAgenciesLookup()
        {
            List<LookupModel> govAgencies = await _iDMAppService.GetAllAgenciesList();
            return govAgencies;
        }

        [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
        [HttpGet]
        [Route("GetSuccededPreQualifications")]
        public async Task<List<LookupModel>> GetSuccededPreQualifications()
        {
            List<LookupModel> qualifications = await _lookupService.GetSuccededPreQualificationsForTenderCreation(User.UserBranch());
            return qualifications;
        }


        [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
        [HttpGet]
        [Route("GetFinishedAnnouncementHasOneSupplier/{tenderId}")]
        public async Task<List<LookupModel>> GetFinishedAnnouncementHasOneSupplier(string tenderId)
        {
            int decyptedTenderId = Util.Decrypt(tenderId);

            List<LookupModel> announcementList = await _lookupService.GetFinishedAnnouncementHasOneSupplier(decyptedTenderId, User.UserAgency(), User.UserBranch());
            return announcementList;
        }


        [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
        [HttpGet]
        [Route("GetFinishedAnnouncementForLimitedTender/{tenderId}")]
        public async Task<List<LookupModel>> GetFinishedAnnouncementForLimitedTender(string tenderId)
        {
            int decyptedTenderId = Util.Decrypt(tenderId);

            List<LookupModel> announcementList = await _lookupService.GetFinishedAnnouncementForLimitedTender(decyptedTenderId, User.UserAgency(), User.UserBranch());
            return announcementList;
        }

        [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
        [HttpGet]
        [Route("GetAnnouncementSupplierTemplateForLimitedTender/{tenderId}/{selectedReasonId}")]
        public async Task<List<LookupModel>> GetAnnouncementSupplierTemplateForLimitedTender(string tenderId, int selectedReasonId)
        {
            int decyptedTenderId = Util.Decrypt(tenderId);

            List<LookupModel> announcementList = await _lookupService.GetAnnouncementSupplierTemplateForLimitedTender(decyptedTenderId, User.UserAgency(), selectedReasonId);
            return announcementList;
        }

        [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
        [HttpGet]
        [Route("GetAnnouncementSupplierTemplateForDirectPurchaseTender")]
        public async Task<List<LookupModel>> GetAnnouncementSupplierTemplateForDirectPurchaseTender()
        {
            List<LookupModel> announcementList = await _lookupService.GetAnnouncementSupplierTemplateForDirectPurchaseTender(User.UserAgency());
            return announcementList;
        }
        [HttpGet]
        [Route("GetCommitteeByCommitteeTypeOnBranch")]
        [AllowAnonymous]
        public async Task<List<CommitteeModel>> GetCommitteeByCommitteeTypeOnBranch()
        {
            return await _tenderService.GetBasicCommitteesOnBranch(User.UserAgency());
        }

        [HttpGet]
        [Route("GetVROAndTechnicalCommittees")]
        [AllowAnonymous]
        public async Task<List<CommitteeModel>> GetVROAndTechnicalCommittees()
        {
            return await _tenderService.GetVROAndTechnicalCommittees(User.UserAgency());
        }
        [HttpGet]
        [Route("GetTechincalAndDirectPurchaseCommittees")]
        [AllowAnonymous]
        public async Task<List<CommitteeModel>> GetTechincalAndDirectPurchaseCommittees()
        {
            return await _tenderService.GetTechincalAndDirectPurchaseCommittees(User.UserAgency());
        }

        [HttpGet]
        [Route("GetDirectPurchaseCommitteesMembers")]
        [AllowAnonymous]
        public async Task<List<CommitteeUserModel>> GetDirectPurchaseCommitteesMembers()
        {
            return await _tenderService.GetDirectPurchaseCommitteesMembers(User.UserAgency(), User.UserBranch());
        }



        [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.Auditer)]
        [HttpGet]
        [Route("GetBasicTenderSecondStageDataById/{id}")]
        public async Task<SecondStageBasicData> GetBasicTenderSecondStageDataById(int id)
        {
            var branchId = User.UserBranch();
            SecondStageBasicData tender = await _tenderService.GetBasicDataForSecondStageTenderCreation(id, branchId);
            return tender;
        }

        [Authorize(Roles = RoleNames.DataEntry)]
        [HttpPost("CreateSeoncdStageBasic")]
        public async Task<TenderDatesModel> CreateSeoncdStageBasic([FromBody] SecondStageBasicData tenderModel)
        {
            tenderModel.TenderStatusId = (int)Enums.TenderStatus.UnderEstablishing;
            tenderModel.AgencyCode = User.UserAgency();
            TenderDatesModel tender = await _tenderService.CreateSecondStageBasicAsync(tenderModel);
            return tender;
        }

        [Authorize(RoleNames.GetTenderByEnquiryIdPolicy)]
        [HttpGet]
        [Route("GetTenderByIdForEnquiry/{id}")]
        public async Task<TenderInformationModel> GetTenderByIdForEnquiry(int id)
        {
            TenderInformationModel tenderInformationModel = await _tenderService.GetTenderByIdForEnquiry(id);
            tenderInformationModel.UserCR = User.SupplierNumber();
            return tenderInformationModel;
        }

        [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
        [HttpGet]
        [Route("GetTenderByIdForJoinigRequests/{id}")]
        public async Task<TenderInformationModel> GetTenderByIdForJoinigRequests(int id)
        {
            TenderInformationModel tenderInformationModel = await _tenderService.GetTenderByIdForJoinigRequests(id);
            return tenderInformationModel;
        }

        [HttpGet]
        [Route("GetTenderByIdToApplyOffer/{id}")]
        public async Task<TenderInformationModel> GetTenderByIdToApplyOffer(int id)
        {
            Tender tender = await _tenderService.GetTenderByIdToApplyOffer(id);
            TenderInformationModel tenderInformationModel = _mapper.Map<TenderInformationModel>(tender);
            return tenderInformationModel;
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpPost("Add")]
        public async Task<TenderDatesModel> Add([FromBody] CreateTenderBasicDataModel tenderModel)
        {
            tenderModel.AgencyCode = User.UserAgency();
            tenderModel.BranchId = User.UserBranch();
            tenderModel.IsVRO = User.UserIsVRO();
            tenderModel.IsAgencyRelatedByVRO = User.UserIsRelatedVRO();
            TenderDatesModel tenderDates = await _tenderService.CreateBasicTender(tenderModel);
            return tenderDates;
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpPost("CreateVROTenderByRelatedAgency")]
        public async Task<TenderRelationsModel> CreateVROTenderByRelatedAgency([FromBody] CreateTenderBasicDataModel tenderModel)
        {
            tenderModel.AgencyCode = User.UserAgency();
            tenderModel.BranchId = User.UserBranch();
            tenderModel.IsAgencyRelatedByVRO = User.UserIsRelatedVRO();
            tenderModel.IsVRO = User.UserIsVRO();
            TenderRelationsModel tenderDats = await _tenderService.CreateVROTenderByRelatedAgency(tenderModel);
            return tenderDats;
        }
        #endregion

        #region Edit
        [HttpGet]
        [Route("GetTenderCommitteesByTenderId/{id}")]
        public async Task<EditeCommitteesModel> GetTenderCommitteesByTenderId(int id)
        {

            EditeCommitteesModel tenderBasicDataModel = await _tenderService.GetTenderCommitteesByTenderId(id, User.UserBranch());
            if (User.IsInRole(RoleNames.DataEntry) && tenderBasicDataModel.BranchId != User.UserBranch())
            {
                throw new UnHandledAccessException();
            }
            if (User.IsInRole(RoleNames.OffersCheckSecretary) && tenderBasicDataModel.OffersCheckingCommitteeId != User.SelectedCommittee())
            {
                throw new UnHandledAccessException();
            }
            if (User.IsInRole(RoleNames.OffersPurchaseSecretary) && tenderBasicDataModel.DirectPurchaseCommitteeId != User.SelectedCommittee())
            {
                throw new UnHandledAccessException();
            }
            return tenderBasicDataModel;
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpPost]
        [Route("EditCommittees")]
        public async Task<EditeCommitteesModel> EditCommittees([FromBody] EditeCommitteesModel committeesModel)
        {
            Tender tender = await _tenderService.EditCommittees(committeesModel);
            EditeCommitteesModel model = _mapper.Map<EditeCommitteesModel>(tender);
            return model;
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpGet]
        [Route("GetTenderSamplesAddress/{tenderId}")]
        public async Task<TenderSamplesAddressModel> GetTenderSamplesAddress(int tenderId)
        {
            int branchId = User.UserBranch();
            Tender tender = await _tenderService.GetTenderSamplesDeliveryAddress(tenderId, branchId);
            TenderSamplesAddressModel addressModel = _mapper.Map<TenderSamplesAddressModel>(tender);
            return addressModel;
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpPost]
        [Route("EditSamplesDeliveryAddress/{tenderId}/{samplesDeliveryAddress}")]
        public async Task<TenderSamplesAddressModel> EditSamplesDeliveryAddress(int tenderId, string samplesDeliveryAddress)
        {
            Tender tender = await _tenderService.EditSamplesDeliveryAddress(tenderId, samplesDeliveryAddress);
            TenderSamplesAddressModel addressModel = _mapper.Map<TenderSamplesAddressModel>(tender);
            return addressModel;
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpPost]
        [Route("ConvertTenderInvitationToPublic/{tenderId}")]
        public async Task ConvertTenderInvitationToPublic(int tenderId)
        {
            await _tenderService.ConvertTenderInvitationToPublic(tenderId);
        }



        [HttpGet]
        [Route("GetTenderToEditAreas/{tenderId}")]
        public async Task<TenderAreasModel> GetTenderToEditAreas(int tenderId)
        {
            int branchId = User.UserBranch();
            Tender tender = await _tenderService.GetTenderForEditAreas(tenderId, branchId);
            if (User.IsInRole(RoleNames.DataEntry) && tender.BranchId != User.UserBranch())
            {
                throw new UnHandledAccessException();
            }
            if (User.IsInRole(RoleNames.OffersCheckSecretary) && tender.OffersCheckingCommitteeId != User.SelectedCommittee())
            {
                throw new UnHandledAccessException();
            }
            if (User.IsInRole(RoleNames.OffersPurchaseSecretary) && tender.DirectPurchaseCommitteeId != User.SelectedCommittee())
            {
                throw new UnHandledAccessException();
            }
            TenderAreasModel areasModel = _mapper.Map<TenderAreasModel>(tender);
            return areasModel;
        }

        [HttpPost]
        [Route("EditAreas")]
        public async Task<TenderAreasModel> EditAreas([FromBody] TenderAreasModel areasModel)
        {
            Tender tender = await _tenderService.EditAreas(areasModel);
            TenderAreasModel addressModel = _mapper.Map<TenderAreasModel>(tender);
            return addressModel;
        }


        [HttpPost]
        [Route("EditCountries")]
        public async Task<TenderAreasModel> EditCountries([FromBody] TenderAreasModel countryModel)
        {
            Tender tender = await _tenderService.EditCountries(countryModel.TenderId, countryModel.TenderCountriesIDs);
            TenderAreasModel addressModel = _mapper.Map<TenderAreasModel>(tender);
            return addressModel;
        }

        #endregion

        private bool GetStatusFlag()
        {
            bool statusFlag = true;
            if (User.IsInRole(RoleNames.DataEntry) || User.IsInRole(RoleNames.Auditer))
                statusFlag = false;
            return statusFlag;
        }

        [Authorize(RoleNames.SupplierPolicy)]
        [HttpGet]
        [Route("GetPurchaseModelByTenderId/{id}")]
        public async Task<PurchaseModel> GetPurchaseModelByTenderId(int id)
        {
            var cr = User.SupplierNumber();
            PurchaseModel purchaseModel = await _tenderService.GetTenderByIdToPurchaseConditionBooklet(id, cr);
            purchaseModel.CommericalRegisterNo = cr;
            purchaseModel.Email = User.Email();
            purchaseModel.Mobile = User.Mobile();
            return purchaseModel;
        }

        [Authorize(RoleNames.SupplierPolicy)]
        [HttpGet]
        [Route("GetInvitationBillDetailsModelByTenderId/{id}")]
        public async Task<PurchaseModel> GetInvitationBillDetailsModelByTenderId(int id)
        {
            var cr = User.SupplierNumber();
            PurchaseModel purchaseModel = await _tenderService.FindTenderForInvitationBillDetailsByTenderIdAndCr(id, cr);
            purchaseModel.CommericalRegisterNo = cr;
            purchaseModel.Email = User.Email();
            purchaseModel.Mobile = User.Mobile();
            return purchaseModel;
        }

        [Authorize]
        [HttpGet]
        [Route("GetCommunicationRequestsDetailsTenderId/{id}")]
        public async Task<TenderCommunicationRequestModel> GetCommunicationRequestsDetailsTenderId(int id)
        {
            var cr = "";
            if (User.IsInRole(RoleNames.supplier))
            {
                cr = User.SupplierNumber();
            }
            TenderCommunicationRequestModel tenderCommunicationRequestModel = await _tenderService.GetCommunicationRequestsDetailsTenderId(id, cr);
            return tenderCommunicationRequestModel;
        }

        [Authorize(RoleNames.GetTenderOffersByIdPolicy)]
        [HttpGet]
        [Route("GetTenderOffersByIdAsync/{id}")]
        public async Task<TenderOffersModel> GetTenderOffersByIdAsync(int id)
        {
            TenderOffersModel tender = await _tenderService.FindTenderInvitationOfferDetailsByTenderId(id);
            return tender;
        }

        [Authorize(RoleNames.OffersOppeningSecretaryAndManagerPolicy)]
        [HttpGet]
        [Route("GetTenderOfferDetailsForOppeningStage/{id}")]
        public async Task<TenderOffersModel> GetTenderOfferDetailsForOppeningStage(int id)
        {
            int userId = User.UserId();
            TenderOffersModel tender = await _tenderService.GetTenderOfferDetailsForOppeningStage(id, userId);
            return tender;
        }

        [Authorize(RoleNames.CheckTenderOffersPolicy)]
        [HttpGet]
        [Route("GetTenderOfferDetailsByTenderIdForCheckStage/{id}")]
        public async Task<TenderDetailsCheckingStageModel> GetTenderOfferDetailsByTenderIdForCheckStage(int id)
        {
            int userId = User.UserId();
            string agencyCode = User.SupplierAgency();
            TenderDetailsCheckingStageModel tender = await _tenderService.GetTenderDetailsForCheckingStage(id, userId, agencyCode);
            return tender;
        }

        [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
        [HttpGet]
        [Route("GetTenderOffersDetailsForOpenAwarding/{id}")]
        public async Task<TenderOffersModel> GetTenderOffersDetailsForOpenAwarding(int id)
        {
            int userId = User.UserId();
            string agencyCode = User.SupplierAgency();
            TenderOffersModel tender = await _tenderService.GetTenderOffersDetailsForOpenAwarding(id, userId, agencyCode);
            return tender;
        }


        [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
        [HttpGet]
        [Route("GetTenderOfferDetailsByTenderIdForAwarding/{id}")]
        public async Task<TenderOffersModel> GetTenderOfferDetailsByTenderIdForAwarding(int id)
        {
            int userId = User.UserId();
            string agencyCode = User.SupplierAgency();
            TenderOffersModel tender = await _tenderService.GetTenderAwardingDetails(id, userId, agencyCode);
            return tender;
        }

        #region Offer Oppening

        [Authorize(RoleNames.OffersOppeningSecretaryAndManagerPolicy)]
        [HttpGet]
        [Route("GetOffersForOpenByTenderIdAsync")]
        public async Task<QueryResult<TenderOpenOfferModel>> GetOffersForOpenByTenderIdAsync(TenderBasicSearchCriteria tenderBasicSearchCriteria)
        {
            tenderBasicSearchCriteria.TenderId = Util.Decrypt(tenderBasicSearchCriteria.TenderIdString);
            var invitationsOffers = await _tenderService.GetOffersForOpenByTenderIdAsync(tenderBasicSearchCriteria);
            Check.ArgumentNotNull(nameof(invitationsOffers), invitationsOffers);
            return invitationsOffers;
        }


        [Authorize(Roles = RoleNames.OffersOppeningSecretary + "," + RoleNames.OffersOppeningManager)]
        [HttpGet]
        [Route("GetUnOpenOffersForCombinedSuppliers/{id}")]
        public async Task<int> GetUnOpenOffersForCombinedSuppliers(int id)
        {
            return await _tenderService.GetUnOpenOffersForCombinedSuppliers(id);
        }

        [Authorize(RoleNames.OffersOppeningSecretaryAndManagerPolicy)]
        [HttpPost]
        [Route("OpenTenderOffer/{id}")]
        public async Task OpenTenderOffer(int id)
        {
            await _tenderService.OpenTenderOffer(id);
        }

        [Authorize(RoleNames.OffersOppeningSecretaryPolicy)]
        [HttpPost]
        [Route("ReopenTenderOffer/{id}")]
        public async Task ReopenTenderOffer(int id)
        {
            await _tenderService.ReopenTenderOffer(id);
        }

        [Authorize(RoleNames.OffersOppeningManagerPolicy)]
        [HttpPost]
        [Route("RejectOpenTenderOffers")]
        public async Task RejectOpenTenderOffers([FromBody] RejectionModel rejectionModel)
        {
            await _tenderService.RejectOpenTenderOffers(rejectionModel.TenderId, rejectionModel.RejectionReason);
        }

        #endregion Offer Oppening

        #region Offer Checking

        [Authorize(Roles = RoleNames.OffersCheckManager + "," + RoleNames.OffersCheckSecretary +
            "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersPurchaseSecretary +
            "," + RoleNames.OffersOpeningAndCheckManager + "," + RoleNames.OffersOpeningAndCheckSecretary +
            "," + RoleNames.OffersOppeningSecretary + "," + RoleNames.OffersOppeningManager)]
        [HttpGet]
        [Route("GetOffersForCheckByTenderIdAsync")]
        public async Task<QueryResult<TenderCheckOfferModel>> GetOffersForCheckByTenderIdAsync(TenderBasicSearchCriteria tenderBasicSearchCriteria)
        {
            tenderBasicSearchCriteria.TenderId = Util.Decrypt(tenderBasicSearchCriteria.TenderIdString);
            var invitationsOffers = await _tenderService.GetOffersForCheckByTenderIdForNormalCheckingAsync(tenderBasicSearchCriteria);
            Check.ArgumentNotNull(nameof(Offer), invitationsOffers);
            return invitationsOffers;
        }

        [Authorize(RoleNames.VROOpenAndCheckingViewPolicy)]
        [HttpGet]
        [Route("GetVROOffersForCheckByTenderIdAsync")]
        public async Task<QueryResult<TenderCheckOfferModel>> GetVROOffersForCheckByTenderIdAsync(TenderBasicSearchCriteria tenderBasicSearchCriteria)
        { 
            var invitationsOffers = await _tenderService.GetVROOffersForCheckByTenderIdForNormalCheckingAsync(tenderBasicSearchCriteria);
            Check.ArgumentNotNull(nameof(Offer), invitationsOffers);
            return invitationsOffers;
        }

        #region two files tender check stage

        #region Technical stage

        [Authorize(Roles = RoleNames.OffersCheckSecretary)]
        [HttpPost]
        [Route("SendTenderToApproveTechnicalCheckingAsync/{id}")]
        public async Task SendTenderToApproveTechnicalCheckingAsync(int id)
        {
            await _tenderService.SendTenderToApproveTechnicalCheckingAsync(id, User.UserAgency());
        }

        [Authorize(Roles = RoleNames.OffersCheckManager)]
        [HttpPost]
        [Route("ApproveTendeTechnicalCheckingAsync/{id}/{verificationCode}")]
        public async Task ApproveTendeTechnicalCheckingAsync(int id, string verificationCode)
        {
            await _tenderService.ApproveTendeTechnicalCheckingAsync(id, verificationCode, User.UserAgency());
        }

        [Authorize(Roles = RoleNames.OffersCheckManager)]
        [HttpPost]
        [Route("RejectTendeTechnicalCheckingAsync")]
        public async Task RejectTendeTechnicalCheckingAsync([FromBody] RejectionModel rejectionModel)
        {
            await _tenderService.RejectTendeTechnicalCheckingAsync(rejectionModel.TenderId, rejectionModel.RejectionReason, User.UserAgency());
        }

        [Authorize(Roles = RoleNames.OffersCheckSecretary)]
        [HttpPost]
        [Route("ReOpenTendeTechnicalCheckingAsync/{tenderId}")]
        public async Task ReOpenTendeTechnicalCheckingAsync(int tenderId)
        {
            await _tenderService.ReOpenTendeTechnicalCheckingAsync(tenderId, User.UserAgency());
        }

        #endregion Technical Stage

        #region Financail Stage

        [Authorize(Roles = RoleNames.OffersCheckSecretary)]
        [HttpPost("MoveTenderToFinancialOffersChecking/{tenderIdString}")]
        public async Task<IActionResult> MoveTenderToFinancialOffersChecking(string tenderIdString)
        {

            await _tenderService.MoveTenderToFinancialOffersChecking(Util.Decrypt(tenderIdString));
            return Ok();
        }

        [Authorize(Roles = RoleNames.OffersCheckSecretary)]
        [HttpPost]
        [Route("SendTenderToApproveFinancailCheckingAsync/{id}")]
        public async Task SendTenderToApproveFinancailCheckingAsync(int id)
        {
            await _tenderService.SendTenderToApproveFinancailCheckingAsync(id, User.UserAgency());
        }

        [Authorize(RoleNames.EndOpenFinantialOffersStagePolicy)]
        [HttpPost]
        [Route("EndOpenFinantialOffersStage/{id}")]
        public async Task EndOpenFinantialOffersStage(int id)
        {
            await _tenderService.EndOpenFinantialOffersStage(id);
        }

        [Authorize(Roles = RoleNames.OffersCheckManager)]
        [HttpPost]
        [Route("ApproveTendeFinancialCheckingAsync/{id}/{verificationCode}")]
        public async Task ApproveTendeFinancialCheckingAsync(int id, string verificationCode)
        {
            await _tenderService.ApproveTendeFinancialCheckingAsync(id, verificationCode, User.UserAgency());
        }

        [Authorize(Roles = RoleNames.OffersCheckManager)]
        [HttpPost]
        [Route("RejectTendeFinancialCheckingAsync")]
        public async Task RejectTendeFinancialCheckingAsync([FromBody] RejectionModel rejectionModel)
        {
            await _tenderService.RejectTendeFinancialCheckingAsync(rejectionModel.TenderId, rejectionModel.RejectionReason, User.UserAgency());
        }

        [Authorize(RoleNames.ReOpenTendeFinancialCheckingPolicy)]
        [HttpPost]
        [Route("ReOpenTendeFinancialCheckingAsync/{tenderId}")]
        public async Task ReOpenTendeFinancialCheckingAsync(int tenderId)
        {
            await _tenderService.ReOpenTendeFinancialCheckingAsync(tenderId, User.UserAgency());
        }

        [Authorize(RoleNames.OffersOppeningManagerPolicy)]
        [HttpPost]
        [Route("ApproveTendeFinancialOpeningAsync/{id}/{verificationCode}")]
        public async Task ApproveTendeFinancialOpeningAsync(int id, string verificationCode)
        {
            await _tenderService.ApproveTendeFinancialOpeningAsync(id, verificationCode);
        }

        [Authorize(RoleNames.OffersOppeningManagerPolicy)]
        [HttpPost]
        [Route("RejectTendeFinancialOpeningAsync")]
        public async Task RejectTendeFinancialOpeningAsync([FromBody] RejectionModel rejectionModel)
        {
            await _tenderService.RejectTendeFinancialOpeningAsync(rejectionModel.TenderId, rejectionModel.RejectionReason);
        }

        [Authorize(RoleNames.OffersOppeningSecretaryPolicy)]
        [HttpPost]
        [Route("ReOpenTenderFinancialOpeningAsync/{tenderId}")]
        public async Task ReOpenTenderFinancialOpeningAsync(int tenderId)
        {
            await _tenderService.ReOpenTendeFinancialOpeningAsync(tenderId);
        }

        #endregion Technical Stage

        #endregion two files tender check stage

        #region Direct purchase offer checking     

        [Authorize(RoleNames.OffersPurchaseSecretaryAndManagerPolicy)]
        [HttpGet]
        [Route("GetDirectPurchaseTenderOfferDetailsByTenderIdForCheckStage/{id}")]
        public async Task<TenderOffersModel> GetDirectPurchaseTenderOfferDetailsByTenderIdForCheckStage(int id)
        {
            int userId = User.UserId();
            TenderOffersModel tender = await _tenderService.FindDirectPurchaseTenderOfferDetailsByTenderIdForCheckStage(id, userId);
            return tender;
        }

        [Authorize(RoleNames.OffersPurchaseSecretaryAndManagerPolicy)]
        [HttpGet]
        [Route("GetOffersForCheckByDirectPurchaseTenderId/{tenderId}")]
        public async Task<QueryResult<TenderCheckOfferModel>> GetOffersForCheckByDirectPurchaseTenderId(int tenderId)
        {
            var invitationsOffers = await _tenderService.GetOffersForCheckByTenderIdAsync(tenderId);
            Check.ArgumentNotNull(nameof(Offer), invitationsOffers);
            var invitationsOffersModel = _mapper.Map<QueryResult<TenderCheckOfferModel>>(invitationsOffers);
            return invitationsOffersModel;
        }

        [Authorize(RoleNames.OffersPurchaseSecretaryAndManagerPolicy)]
        [HttpGet]
        [Route("CheckTenderOffersForDirectPurchasePagingAsync/{tenderId}")]
        public async Task<QueryResult<TenderCheckOfferModel>> CheckTenderOffersForDirectPurchasePagingAsync(int tenderId)
        {
            var invitationsOffers = await _tenderService.GetOffersForCheckByTenderIdAsync(tenderId);
            Check.ArgumentNotNull(nameof(Offer), invitationsOffers);
            var invitationsOffersModel = _mapper.Map<QueryResult<TenderCheckOfferModel>>(invitationsOffers);
            return invitationsOffersModel;
        }

        [Authorize(RoleNames.OffersPurchaseSecretaryAndManagerPolicy)]
        [HttpPost]
        [Route("StartDirectPurchaseTenderCheckingOffers/{id}")]
        public async Task StartDirectPurchaseTenderCheckingOffers(int id)
        {
            await _tenderService.StartDirectPurchaseTenderCheckingOffers(id, User.UserAgency());
        }

        [Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
        [HttpPost]
        [Route("SendDirectPurchaseTenderOffersCheckingToApprove/{tenderId}")]
        public async Task SendDirectPurchaseTenderOffersCheckingToApprove(int tenderId)
        {
            await _tenderService.SendDirectPurchaseTenderOffersCheckingToApprove(tenderId, User.UserAgency());
        }

        [Authorize(RoleNames.OffersPurchaseSecretaryAndManagerPolicy)]
        [HttpPost]
        [Route("ApproveDirectPurchaseTenderOffersChecking/{tenderId}/{verificationCode}")]
        public async Task ApproveDirectPurchaseTenderOffersChecking(int tenderId, string verificationCode)
        {
            await _tenderService.ApproveDirectPurchaseTenderOffersChecking(tenderId, verificationCode, User.UserAgency());
        }

        [Authorize(RoleNames.OffersPurchaseManagerPolicy)]
        [HttpPost]
        [Route("RejectDirectPurchaseTenderOffersChecking")]
        public async Task RejectDirectPurchaseTenderOffersChecking([FromBody] RejectionModel rejectionModel)
        {
            await _tenderService.RejectDirectPurchaseTenderOffersChecking(rejectionModel.TenderId, rejectionModel.RejectionReason, User.UserAgency());
        }

        [Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
        [HttpPost]
        [Route("ReopenDirectPurchaseTenderOffersChecking/{tenderId}")]
        public async Task ReopenDirectPurchaseTenderOffersChecking(int tenderId)
        {
            await _tenderService.ReopenDirectPurchaseTenderOffersChecking(tenderId, User.UserAgency());
        }

        [Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
        [HttpPost]
        [Route("SendDirectPurchaseTenderOffersTechnicalCheckingToApprove/{tenderId}")]
        public async Task SendDirectPurchaseTenderOffersTechnicalCheckingToApprove(int tenderId)
        {
            await _tenderService.SendDirectPurchaseTenderOffersTechnicalCheckingToApprove(tenderId, User.UserAgency());
        }

        [Authorize(RoleNames.OffersPurchaseManagerPolicy)]
        [HttpPost]
        [Route("ApproveDirectPurchaseTenderOffersTechnicalChecking/{tenderId}/{verificationCode}")]
        public async Task ApproveDirectPurchaseTenderOffersTechnicalChecking(int tenderId, string verificationCode)
        {
            await _tenderService.ApproveDirectPurchaseTenderOffersTechnicalChecking(tenderId, verificationCode, User.UserAgency());
        }

        [Authorize(RoleNames.OffersPurchaseManagerPolicy)]
        [HttpPost]
        [Route("RejectDirectPurchaseTenderOffersTechnicalChecking")]
        public async Task RejectDirectPurchaseTenderOffersTechnicalChecking([FromBody] RejectionModel rejectionModel)
        {
            await _tenderService.RejectDirectPurchaseTenderOffersTechnicalChecking(rejectionModel.TenderId, rejectionModel.RejectionReason, User.UserAgency());
        }

        [Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
        [HttpPost]
        [Route("ReopenDirectPurchaseTenderOffersTechnicalChecking/{tenderId}")]
        public async Task ReopenDirectPurchaseTenderOffersTechnicalChecking(int tenderId)
        {
            await _tenderService.ReopenDirectPurchaseTenderOffersTechnicalChecking(tenderId, User.UserAgency());
        }

        [Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
        [HttpPost]
        [Route("SendDirectPurchaseTenderOffersFinanceCheckingToApprove/{tenderId}")]
        public async Task SendDirectPurchaseTenderOffersFinanceCheckingToApprove(int tenderId)
        {
            await _tenderService.SendDirectPurchaseTenderOffersFinanceCheckingToApprove(tenderId, User.UserAgency());
        }

        [Authorize(RoleNames.OffersPurchaseManagerPolicy)]
        [HttpPost]
        [Route("ApproveDirectPurchaseTenderOffersFinanceChecking/{tenderId}/{verificationCode}")]
        public async Task ApproveDirectPurchaseTenderOffersFinanceChecking(int tenderId, string verificationCode)
        {
            await _tenderService.ApproveDirectPurchaseTenderOffersFinanceChecking(tenderId, verificationCode, User.UserAgency());
        }

        [Authorize(RoleNames.OffersPurchaseManagerPolicy)]
        [HttpPost]
        [Route("RejectDirectPurchaseTenderOffersFinanceChecking")]
        public async Task RejectDirectPurchaseTenderOffersFinanceChecking([FromBody] RejectionModel rejectionModel)
        {
            await _tenderService.RejectDirectPurchaseTenderOffersFinanceChecking(rejectionModel.TenderId, rejectionModel.RejectionReason, User.UserAgency());
        }

        [Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
        [HttpPost]
        [Route("ReopenDirectPurchaseTenderOffersFinancialChecking/{tenderId}")]
        public async Task ReopenDirectPurchaseTenderOffersFinancialChecking(int tenderId)
        {
            await _tenderService.ReopenDirectPurchaseTenderOffersFinancialChecking(tenderId, User.UserAgency());
        }

        [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
        [HttpPost]
        [Route("StartTenderCheckingOffers/{id}")]
        public async Task StartTenderCheckingOffers(int id)
        {
            await _tenderService.StartTenderCheckingOffers(id);
        }

        [Authorize(RoleNames.OffersCheckManagerPolicy)]
        [HttpPost]
        [Route("RejectCheckTenderOffersReport")]
        public async Task RejectCheckTenderOffersReport([FromBody] RejectionModel rejectionModel)
        {
            await _tenderService.RejectCheckTenderOffers(rejectionModel.TenderId, rejectionModel.RejectionReason);
        }

        #endregion Direct purchase offer checking 

        #endregion Offer Checking

        #region Offer Awarding

        [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
        [HttpPost]
        [Route("AwardTenderOffers/{id}")]
        public async Task AwardTenderOffers(int id)
        {
            await _tenderService.AwardTenderOffers(id);
        }

        [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
        [HttpPost]
        [Route("SendAwardTenderToApprove/{id}")]
        public async Task SendAwardTenderToApprove(int id)
        {

            await _tenderService.SendAwardTenderToApprove(id);
        }

        [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
        [HttpPost]
        [Route("SendAwardTenderToInitialApprove/{id}")]
        public async Task SendAwardTenderToInitialApprove(int id)
        {
            await _tenderService.SendAwardTenderToInitialApprove(id);
        }

        [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
        [HttpGet]
        [Route("IsSupplierPassedForTenderAwarding/{tenderId}/{cr}")]
        public async Task<int> IsSupplierPassedForTenderAwarding(int tenderId, string cr)
        {
            var result = await _tenderService.IsSupplierPassedForTenderAwarding(tenderId, cr, User.SupplierAgency());
            return result;
        }

        [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
        [HttpPost]
        [Route("IsTenderHasPendingRequestsOrNoExeclusionReasons/{tenderId}")]
        public async Task IsTenderHasPendingRequestsOrNoExeclusionReasons(int tenderId)
        {
            await _tenderService.IsTenderHasPendingRequestsOrNoExeclusionReasons(tenderId);
        }

        [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
        [HttpGet]
        [Route("RemoveOfferAwardingValue/{offerId}")]
        public async Task<bool> RemoveOfferAwardingValue(int offerId)
        {
            var result = await _offerAppService.RemoveOfferAwardingValue(offerId);
            return result;
        }
        [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
        [HttpPost]
        [Route("CheckSendTenderToApproveAwarding")]
        public async Task<int> CheckSendTenderToApproveAwarding([FromBody] SendToAwardingModel sendModel)
        {
            var result = await _tenderService.CheckSendTenderToApproveAwarding(Util.Decrypt(sendModel.EncryptedTenderId), sendModel.CRs, User.SupplierAgency());
            return result;
        }


        [Authorize(RoleNames.ApproveTenderAwardPolicy)]
        [HttpPost]
        [Route("ApproveTenderAwardingWithVerification/{id}/{verificationCode}")]
        public async Task ApproveTenderAwardingWithVerification(int id, string verificationCode)
        {
            await _tenderService.ApproveTenderAwarding(id, verificationCode);
        }

        [Authorize(RoleNames.AwardingInitialApprovalPolicy)]
        [HttpPost]
        [Route("ApproveTenderInitialAwarding/{id}/{verificationCode}")]
        public async Task ApproveTenderInitialAwarding(int id, string verificationCode)
        {
            await _tenderService.ApproveTenderInitialAwarding(id, User.SupplierAgency(), verificationCode);
        }

        [Authorize(RoleNames.OffersCheckManagerPolicy)]
        [HttpPost]
        [Route("RejectAwardTenderOffersReport")]
        public async Task RejectAwardTenderOffersReport([FromBody] RejectionModel rejectionModel)
        {
            await _tenderService.RejectAwardTenderOffer(rejectionModel.TenderId, rejectionModel.RejectionReason);
        }

        [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
        [HttpGet]
        [Route("GetOffersForAwardingStageByTenderId")]
        public async Task<QueryResult<TenderCheckOfferModel>> GetOffersForAwardingStageByTenderId(TenderBasicSearchCriteria tenderBasicSearchCriteria)
        {
            tenderBasicSearchCriteria.TenderId = Util.Decrypt(tenderBasicSearchCriteria.TenderIdString);
            var result = await _tenderService.GetOffersForAwardingStageByTenderId(tenderBasicSearchCriteria);
            return result;
        }

        #endregion Offer Awarding


        [Authorize(RoleNames.DataEntryAndAuditerAndSupplierPolicy)]
        [HttpGet]
        [Route("GetDetailsById/{id}")]
        public async Task<TenderOffersStepModel> GetDetailsById(int id)
        {
            Tender tender = await _tenderService.FindTenderDetailsByTenderId(id);
            if (tender == null)
            {
                throw new UnHandledAccessException(Resources.TenderResources.ErrorMessages.FillTenderBasicInformation);
            }
            if (tender.AgencyCode != User.UserAgency())
            {
                throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            }
            var _tender = _mapper.Map<TenderOffersStepModel>(tender);
            return _tender;
        }

        [HttpGet]
        [Route("GetTenderDatesById/{id}")]
        public async Task<TenderDatesModel> GetTenderDatesByTenderId(int id)
        {
            TenderDatesModel tenderDatesModel = await _tenderService.FindTenderDatesByTenderId(id, User.UserBranch());
            return tenderDatesModel;
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("GetOfferDetailsById/{id}")]
        public async Task<TenderOffersStepModel> GetOfferDetailsById(int id)
        {
            string cr = User.SupplierNumber();
            Tender tender = await _tenderService.FindTenderOfferDetailsByTenderId(id);
            var isPurchased = cr != "" ? await _tenderService.IsTenderPurchasedBySupplier(id, cr) : false;
            var _tender = _mapper.Map<TenderOffersStepModel>(tender);
            _tender.IsPurchased = isPurchased;
            return _tender;
        }

        [Authorize(RoleNames.DataEntryAndAuditerAndSupplierPolicy)]
        [HttpGet]
        [Route("GetTenderByAgencyCode/{id}")]
        public async Task<TenderOffersStepModel> FindTenderByAgencyCode(string id)
        {
            Tender tender = await _tenderService.FindTenderByAgencyCode(id);
            var _tender = _mapper.Map<TenderOffersStepModel>(tender);
            return _tender;
        }

        [Authorize(RoleNames.DataEntryAndAuditerAndSupplierPolicy)]
        [HttpGet]
        [Route("GetRelationsById/{id}")]
        public async Task<TenderRelationsModel> GetRelationsById(int id)
        {
            TenderRelationsModel tender = await _tenderService.FindTenderRelationsModelByTenderId(id, User.UserBranch());
            return tender;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAttachmentsByTenderId/{id}")]
        public async Task<TenderAttachmentModel> GetAttachmentsByTenderId(int id)
        {
            Tender tender = await _tenderService.FindTenderAttachmentsById(id, User.UserBranch());
            return ProcessAttachmentsChanges(tender, false);
        }

        private TenderAttachmentModel ProcessAttachmentsChanges(Tender tender, bool isEdit)
        {
            var tenderAttachments = new TenderAttachmentModel();
            if (isEdit)
                if (tender == null || tender.TenderQuantityTables.Count() == 0)
                    throw new UnHandledAccessException(Resources.TenderResources.ErrorMessages.FillTenderQuantityTablesInformation);
                else if (tender.AgencyCode != User.UserAgency())
                    throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            string userCr = User.SupplierNumber() ?? "";
            tenderAttachments.IsPurchased = tender.ConditionsBooklets.Any(c => c.CommericalRegisterNo == userCr && c.BillInfo != null && c.BillInfo.BillStatusId == (int)Enums.BillStatus.Paid);
            var invitation = tender.Invitations.Where(i => i.CommericalRegisterNo == userCr).FirstOrDefault();
            tenderAttachments.InvitationStatusId = invitation != null ? invitation.StatusId : 0;
            tenderAttachments.InvitationTypeId = tender.InvitationTypeId.HasValue ? tender.InvitationTypeId.Value : 0;
            tenderAttachments.TenderTypeId = tender.TenderTypeId;
            tenderAttachments.STenderId = Util.Encrypt(tender.TenderId);
            tenderAttachments.TenderActivityVersionId = tender.TenderVersions.FirstOrDefault(s => s.Version.VersionTypeId == (int)Enums.VersionType.TenderActivity).VersionId;
            var attachments = tender.Attachments.ToList();
            var changeRequest = tender.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Attachments).FirstOrDefault();
            if (changeRequest != null)
            {
                var attachmentsChanges = changeRequest.AttachmentChanges.Where(x => x.IsActive == true).ToList();
                tenderAttachments.AttachmentsChanges = _mapper.Map<List<TenderAttachmentsModelChanges>>(attachmentsChanges);
                tenderAttachments.TenderChangeRequestId = changeRequest.TenderChangeRequestId;
                tenderAttachments.ChangeRequestStatusId = changeRequest.ChangeRequestStatusId;
                tenderAttachments.ChangeRequestTypeId = changeRequest.ChangeRequestTypeId;
                tenderAttachments.ConditionTemplateStageStatusId = changeRequest.ChangeRequestStatusId;
                tenderAttachments.RejectionReason = changeRequest.RejectionReason;
                tenderAttachments.TenderId = changeRequest.TenderId;
            }
            tenderAttachments.TenderName = tender.TenderName;
            tenderAttachments.TenderNumber = tender.TenderNumber;
            tenderAttachments.ReferenceNumber = tender.ReferenceNumber;
            tenderAttachments.OldAttachments = _mapper.Map<List<TenderOldAttachmentsModel>>(attachments.Where(x => x.IsActive == true));
            tenderAttachments.ConditionTemplateStageStatusId = tender.ConditionTemplateStageStatusId;
            if (tenderAttachments.OldAttachments != null) tenderAttachments.OldAttachments.ForEach(x => { x.TenderStatusId = tender.TenderStatusId; });
            return tenderAttachments;
        }

        [HttpGet("ProcessAttachments")]
        public List<TenderAttachmentModel> ProcessAttachments(Tender tender, bool isEdit)
        {
            if (tender.AgencyCode != User.UserAgency())
                throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            var attachments = tender.Attachments.ToList();
            var _tender = _mapper.Map<List<TenderAttachmentModel>>(attachments.Where(x => x.IsActive == true));
            if (_tender != null) _tender.ForEach(x => { x.TenderStatusId = tender.TenderStatusId; });
            return _tender;
        }

        [Authorize(RoleNames.DetailsPolicy)]
        [HttpGet]
        [Route("GetAttachmentsStepByTenderId/{id}")]
        public async Task<List<TenderAttachmentModel>> GetAttachmentsStepByTenderId(int id)
        {
            Tender tender = await _tenderService.FindTenderAttachmentsById(id, User.UserBranch());
            return ProcessAttachments(tender, true);
        }
        //[Authorize(RoleNames.DetailsPolicy)]
        // RODO-Youssef Add Policy
        [HttpGet]
        [Route("GetAttachmentsEditStepByTenderId/{id}")]
        public async Task<AttachmentsModel> GetAttachmentsEditStepByTenderId(int id)
        {
            AttachmentsModel tender = await _tenderService.FindTenderAttachmentsModelById(id, User.UserBranch());
            if (tender == null || (tender.TablesCount == 0 && tender.TenderTypeId != (int)Enums.TenderType.FirstStageTender && tender.TenderTypeId != (int)Enums.TenderType.Competition && tender.TenderTypeId != (int)Enums.TenderType.ReverseBidding && tender.TenderTypeId != (int)Enums.TenderType.PreQualification && tender.TenderTypeId != (int)Enums.TenderType.PostQualification))
                throw new UnHandledAccessException(Resources.TenderResources.ErrorMessages.FillTenderQuantityTablesInformation);
            if (tender.AgencyCode != User.UserAgency())
                throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            return tender;
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("GetRelationsDetailsByTenderId/{id}")]
        public async Task<TenderRelationsModel> GetRelationsDetailsByTenderId(int id)
        {
            TenderRelationsModel tenderRelationsModel = await _tenderService.FindRelationsDetailsByTenderId(id);
            return tenderRelationsModel;
        }
        
        [HttpGet]
        [Route("GetTenderLocalContenetValuesByTenderId/{tenderId}")]
        public async Task<TenderLocalContentValuesViewModel> GetTenderLocalContenetValuesByTenderId(int tenderId)
        {
            TenderLocalContentValuesViewModel tenderRelationsModel = await _tenderService.GetTenderLocalContenetValuesByTenderId(tenderId);
            return tenderRelationsModel;
        }
        [HttpGet]
        [Route("GetLocalContentDetailsForSupplierByTenderId/{tenderId}")]
        public async Task<LocalContentDetailsViewModel> GetLocalContentDetailsForSupplierByTenderId(int tenderId)
        {
            LocalContentDetailsViewModel tenderRelationsModel = await _tenderService.GetLocalContentDetailsForSupplierByTenderId(tenderId);
            return tenderRelationsModel;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetTenderHistoryByUserIdAndTenderIdAndStatusId/{tenderIdString}/{TenderStatusId}")]
        public async Task<TenderHistoryModel> GetTenderHistoryByUserIdAndTenderIdAndStatusId(string tenderIdString, int tenderStatusId = 0)
        {
            TenderHistory tenderHistory = await _tenderService.FindTenderHistoryByUserIdAndTenderIdAndStatusId(Util.Decrypt(tenderIdString), tenderStatusId);
            var _tenderHistoryModel = _mapper.Map<TenderHistoryModel>(tenderHistory);
            return _tenderHistoryModel;
        }

        [Authorize(RoleNames.DetailsPolicy)]
        [HttpGet]
        [Route("GetTenderQuantityTablesById/{id}")]
        public async Task<QuantityTableStepDTO> GetTenderQuantityTablesById(int id)
        {
            QuantityTableStepDTO quantityTableStepDTO = new QuantityTableStepDTO();
            if (id != 0)
            {
                quantityTableStepDTO = await _tenderService.FindTenderQuantityItemsByFormIds(id, false);
                if (quantityTableStepDTO == null || quantityTableStepDTO.TenderTypeId == (int)Enums.TenderType.Competition || quantityTableStepDTO.TenderTypeId == (int)Enums.TenderType.FirstStageTender)
                {
                    throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
                }
            }
            return quantityTableStepDTO;
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpPost]
        [Route("SendInvitations")]
        public async Task<IActionResult> SendInvitations([FromBody] List<InvitationCrModel> suppliers)
        {
            User.UserAgency();
            await _tenderService.SendInvitationsAsync(suppliers);
            return Ok();
        }

        [Authorize(RoleNames.SupplierPolicy)]
        [HttpPost]
        [Route("SendRequestToApplayForTender")]
        public async Task<bool> SendRequestToApplayForTender([FromBody] BasicTenderModel tender)
        {
            var commericalRegisterNo = User.SupplierNumber();
            var result = await _tenderService.SendRequestToApplayForTender(tender.TenderId, commericalRegisterNo);
            return result;
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpPost("UpdateTenderRelations")]
        public async Task<QuantitiesTemplateModel> UpdateTenderRelations([FromBody] TenderRelationsModel relationModel)
        {
            return await _tenderService.UpdateTenderRelations(relationModel);
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpPost("UpdateTenderRelationsWithoutQuantityTable")]
        public async Task UpdateTenderRelationsWithoutQuantityTable([FromBody] TenderRelationsModel relationModel)
        {
            await _tenderService.UpdateTenderRelationsWithoutQuantityTable(relationModel);
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpPost("UpdateTenderDates")]
        public async Task<TenderRelationsModel> UpdateTenderDates([FromBody] TenderDatesModel tenderDatesModel)
        {
            int branchId = User.UserBranch();
            tenderDatesModel.BranchId = branchId;
            TenderRelationsModel tender = await _tenderService.UpdateTenderDates(tenderDatesModel);
            return tender;
        }

        [HttpPost("UpdateTenderExtendDates")]
        public async Task UpdateTenderExtendDates([FromBody] TenderDatesModel tenderDatesModel)
        {
            string userRole = User.UserRoles().FirstOrDefault();
            await _tenderService.UpdateTenderExtendDates(tenderDatesModel, userRole, User.UserAgency());
        }

        [HttpGet]
        [Route("GetTenderDatesDetailsById/{id}")]
        public async Task<TenderDatesModel> GetTenderDatesDetailsById(int id)
        {
            TenderDatesModel tenderDatesModel = await _tenderService.GetTenderDatesDetailsById(id);
            if (tenderDatesModel == null)
                throw new UnHandledAccessException(Resources.TenderResources.ErrorMessages.FillTenderBasicInformation);

            if (User.UserRole() != RoleNames.MonafasatAccountManager && User.UserRole() != RoleNames.CustomerService && tenderDatesModel.TenderCreatedByTypeId != (int)Enums.TenderCreatedByType.AgenciesRelatedByVRO && tenderDatesModel.TenderStatusId == (int)Enums.TenderStatus.PendingVROAuditerApprove && tenderDatesModel.VROOfficeCode != User.UserAgency())
                throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            if (User.UserRole() != RoleNames.MonafasatAccountManager && User.UserRole() != RoleNames.CustomerService && tenderDatesModel.TenderCreatedByTypeId != (int)Enums.TenderCreatedByType.AgenciesRelatedByVRO && User.UserAgency() != "" && tenderDatesModel.AgencyCode != User.UserAgency())
                throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            return tenderDatesModel;
        }

        [HttpGet]
        [Route("GetTenderExtendDatesByTenderId/{id}")]
        [Authorize(RoleNames.DataEntryPolicy)]
        public async Task<TenderDatesModel> GetTenderExtendDatesByTenderId(int id)
        {
            TenderDatesModel tenderDatesModel = await _tenderService.FindTenderExtendDatesByTenderId(id, User.UserBranch());
            return tenderDatesModel;
        }

        [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
        [HttpGet]
        [Route("Delete/{id}")]
        public async Task Delete(int id)
        {
            await _tenderService.DeleteAsync(id);
        }


        [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
        [HttpGet]
        [Route("DeletePostQualification/{id}")]
        public async Task DeletePostQualification(int id)
        {
            await _tenderService.DeleteAsync(id);
        }
        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpPost("SaveTenderAttachments")]
        public async Task<BasicTenderInfoModel> SaveTenderAttachments([FromBody] List<TenderAttachmentModel> attachments)
        {
            return await _tenderService.UpdateTenderAttachmentsAsync(attachments, User.UserBranch());
        }


        [HttpPost("SaveTenderAttachmentsUpdates/{tenderId}/{tenderStatusId}")]
        public async Task SaveTenderAttachmentsUpdates(int tenderId, int tenderStatusId, [FromBody] List<TenderAttachmentModel> attachments)
        {
            var userRole = User.UserRoles().FirstOrDefault();
            await _tenderService.UpdateTenderAttachmentsAfterApprovment(tenderId, tenderStatusId, attachments, userRole, User.UserBranch());
        }

        [HttpGet]
        [Route("GetTenderConditionsTemplateById/{tenderIdString}")]
        public async Task<GetConditionTemplateModel> GetTenderConditionsTemplateById(string tenderIdString)
        {
            GetConditionTemplateModel TemplateModel = await _tenderService.FindTenderConditionTemplateByTenderId(tenderIdString, User.UserBranch());
            TemplateModel.RegulationsDate = _rootConfiguration.RFPFieldsConfiguration.RegulationDate;
            TemplateModel.SecondRegulationsDate = _rootConfiguration.RFPFieldsConfiguration.SecondRegulationDate;
            return TemplateModel;
        }

        [HttpGet]
        [Route("GetConditionTemplate/{tenderIdString}")]
        public async Task<GetConditionTemplateModel> GetConditionTemplate(string tenderIdString)
        {
            return await _tenderService.FindConditionTemplate(tenderIdString, User.UserBranch());
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpPost("AddEditIntroductionTemplate")]
        public async Task AddEditIntroductionTemplate([FromBody] EditConditionTemplateSecondSectionModel model)
        {
            await _tenderService.AddEditIntroductionTemplate(model, User.UserBranch());
        }

        [HttpPost("AddEditLocalContentTemplate")]
        public async Task AddEditLocalContentTemplate([FromBody] LocalContentModel model)
        {
            await _tenderService.AddEditLocalContentTemplate(model, User.UserBranch());
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpPost("AddEditPreparingOffersTemplate")]
        public async Task AddEditPreparingOffersTemplate([FromBody] EditConditionTemplateThridSectionModel model)
        {
            await _tenderService.AddEditPreparingOffersTemplate(model, User.UserBranch());
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpPost("AddEditTechnicalDeclerationsTemplate")]
        public async Task AddEditTechnicalDeclerationsTemplate([FromBody] EditConditionTemplateSeventhSectionModel model)
        {
            await _tenderService.AddEditTechnicalDeclerationsTemplate(model, User.UserBranch());
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpPost("AddEditDescriptionAndConditionsTemplate")]
        public async Task AddEditDescriptionAndConditionsTemplate([FromBody] EditConditionTemplateEighthSectionModel model)
        {
            await _tenderService.AddEditDescriptionAndConditionsTemplate(model, User.UserBranch());
        }

        [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
        [HttpGet]
        [Route("GetAllAddresses")]
        public async Task<List<AddressModel>> GetAllAddresses()
        {
            List<AddressModel> addressList = await _tenderService.GetAllAddresses();
            return addressList;
        }

        //[Authorize(RoleNames.DataEntryAndAuditerPolicy)]
        [AllowAnonymous]
        [HttpGet]
        [Route("GetOfferOpeningAddresses")]
        public async Task<List<AddressModel>> GetOfferOpeningAddresses()
        {
            int branchId = User.UserBranch();
            List<AddressModel> addresses = await _tenderService.GetOfferOpeningAddresses(branchId);
            return addresses;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetOfferOpeningAddressesByBranchId/{branchId}")]
        public async Task<List<AddressModel>> GetOfferOpeningAddressesByBranchId(int branchId)
        {
            List<AddressModel> addresses = await _tenderService.GetOfferOpeningAddressesByBranchId(branchId);
            return addresses;
        }
        [Authorize(RoleNames.SupplierPolicy)]
        [HttpPost("PurshaseTender")]
        public async Task<PurchaseModel> PurshaseTender([FromBody] int tenderId)
        {
            var cr = User.SupplierNumber();
            var result = await _tenderService.PurshaseTender(tenderId, cr, User.Mobile(), User.Email());
            return result;
        }

        [Authorize(RoleNames.SupplierPolicy)]
        [HttpGet("GetSupplierFavouritTendersList")]
        public async Task<QueryResult<TenderInvitationDetailsModel>> GetSupplierFavouritTendersList(TenderSearchCriteria criteria)
        {
            criteria.cr = User.SupplierNumber();
            var tenders = await _tenderService.GetSupplierFavouritTendersList(criteria);
            var result = _mapper.Map<QueryResult<TenderInvitationDetailsModel>>(tenders);
            foreach (var tender in result.Items)
                tender.IsFavouriteTender = true;
            return result;
        }

        [Authorize(RoleNames.SupplierPolicy)]
        [HttpGet("FindAwardedTenderBySearchCriteria")]
        public async Task<QueryResult<TenderCheckingAndAwardingModel>> FindAwardedTenderBySearchCriteria(TenderSearchCriteria criteria)
        {
            criteria.cr = User.SupplierNumber();
            var tenderList = await _tenderService.FindAwardedTenderBySearchCriteria(criteria);
            return tenderList;
        }

        [Authorize(RoleNames.SupplierPolicy)]
        [HttpPost("DeleteTenderFromSupplierTendersFavouriteList")]
        public async Task<bool> DeleteTenderFromSupplierTendersFavouriteList([FromBody] int tenderId)
        {
            var cr = User.SupplierNumber();
            var tenders = await _tenderService.DeleteTenderFromSupplierTendersFavouriteList(tenderId, cr);
            return tenders;
        }

        [Authorize(RoleNames.SupplierPolicy)]
        [HttpGet("AddTenderToSupplierTendersFavouriteList/{tenderId}")]
        public async Task<bool> AddTenderToSupplierTendersFavouriteList(int tenderId)
        {
            var cr = User.SupplierNumber();
            var tenders = await _tenderService.AddTenderToSupplierTendersFavouriteList(tenderId, cr);
            return tenders;
        }

        [AllowAnonymous]
        [HttpGet("CheckifCurrentSupplierBlocked")]
        public async Task<bool> CheckifCurrentSupplierBlocked()
        {
            var cr = User.SupplierNumber();
            var isBlocked = await _blockService.CheckifSupplierBlockedByCrNo(cr);
            return isBlocked;
        }

        #region FavouriteSuppliers

        [Authorize(RoleNames.GetFavouriteSuppliersByListIdPolicy)]
        [HttpGet("GetAllSuppliers")]
        public async Task<QueryResult<SupplierInvitationModel>> GetAllSuppliers(SupplierSearchCretriaModel cretria)
        {
            var Suppliers = await _supplierService.GetAllSuppliers(cretria);
            return Suppliers;
        }

        [Authorize(Roles = RoleNames.MonafasatAdmin)]
        [HttpGet("NotificationStatusReport")]
        public async Task<QueryResult<TenderNotificationStatus>> NotificationStatusReport(TenderNotificationSearchCriteria cretria)
        {
            cretria.tId = Util.Decrypt(cretria.tenderId);
            var result = await _notificationAppService.GetNotificationStatusReport(cretria);
            return result;

        }

        [Authorize(RoleNames.GetFavouriteSuppliersByListIdPolicy)]
        [HttpGet("GetIDMSuppliers")]
        public async Task<SupplierInvitationModel> GetSupplierByCr(string CommericalRegisterNo)
        {
            var Suppliers = await _supplierService.GetSupplierByName(CommericalRegisterNo);

            return Suppliers;
        }

        [Authorize(Roles = RoleNames.GetFavouriteSuppliersByListIdPolicy + "," + RoleNames.MonafasatAdmin + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.CustomerService + "," + RoleNames.OffersOppeningManager + "," + RoleNames.Auditer + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersOppeningSecretary + "," + RoleNames.DataEntry + "," + RoleNames.PurshaseSpecialist + "," + RoleNames.EtimadOfficer)]
        [HttpGet("GetAgencyBranchFavouriteLists")]
        public async Task<List<FavouriteSupplierListModel>> GetAgencyBranchFavouriteLists(FavouriteSupplierListModel cretria)
        {
            cretria.AgencyCode = User.UserAgency();
            cretria.BranchId = User.UserBranch();
            var FavouriteLists = await _supplierService.GetFavouriteListsByAgencyBranchId(cretria.AgencyCode, cretria.BranchId);
            return FavouriteLists;
        }

        [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
        [HttpPost("AddFavouriteSupplierList")]
        public async Task<FavouriteSupplierListModel> AddFavouriteSupplierList([FromBody] FavouriteSupplierListModel favouriteSupplierList)
        {
            favouriteSupplierList.BranchId = User.UserBranch();
            favouriteSupplierList.AgencyCode = User.UserAgency();
            var FavouriteList = await _supplierService.AddFavouriteSupplierList(favouriteSupplierList);
            return FavouriteList;
        }

        [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
        [HttpGet("DeleteFavouriteSupplierList")]
        public async Task<bool> DeleteFavouriteSupplierList(FavouriteSupplierListModel favouriteSupplierList)
        {
            favouriteSupplierList.BranchId = User.UserBranch();
            favouriteSupplierList.AgencyCode = User.UserAgency();
            var result = await _supplierService.DeleteFavouriteSupplierList(favouriteSupplierList);
            return result;
        }

        [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
        [HttpGet("DeleteFavouriteSupplierListToTransfer")]
        public async Task<bool> DeleteFavouriteSupplierListToTransfer(int FavouriteSupplierListId)
        {
            FavouriteSupplierListModel favouriteSupplierList = new FavouriteSupplierListModel();
            favouriteSupplierList.FavouriteSupplierListId = FavouriteSupplierListId;
            favouriteSupplierList.BranchId = User.UserBranch();
            favouriteSupplierList.AgencyCode = User.UserAgency();
            var result = await _supplierService.DeleteFavouriteSupplierList(favouriteSupplierList);
            return result;
        }
        [Authorize(RoleNames.GetFavouriteSuppliersByListIdPolicy)]
        [HttpGet("GetFavouriteSuppliersByListId")]
        public async Task<QueryResult<SupplierInvitationModel>> GetFavouriteSuppliersByListId(SupplierSearchCretriaModel supplierSearchCretria)
        {
            supplierSearchCretria.AgencyCode = User.UserAgency();
            supplierSearchCretria.BranchId = User.UserBranch();
            var FavouriteSupplier = await _supplierService.GetFavouriteSuppliersByListId(supplierSearchCretria);
            return FavouriteSupplier;
        }

        [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
        [HttpPost("AddSupplierToFavouriteSuppliersListAsync")]
        public async Task<bool> AddSupplierToFavouriteSuppliersListAsync([FromBody] SupplierSearchCretriaModel favouriteSupplierList)
        {
            favouriteSupplierList.BranchId = User.UserBranch();
            favouriteSupplierList.AgencyCode = User.UserAgency();
            var tenders = await _supplierService.AddSupplierToFavouriteList(favouriteSupplierList);
            return tenders;
        }

        [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
        [HttpGet("DeleteSupplierFromFavouriteList")]
        public async Task<bool> DeleteSupplierFromFavouriteList(SupplierSearchCretriaModel favouriteSupplierList)
        {
            favouriteSupplierList.BranchId = User.UserBranch();
            favouriteSupplierList.AgencyCode = User.UserAgency();
            var tenders = await _supplierService.DeleteSupplierFromFavouriteList(favouriteSupplierList);
            return tenders;
        }

        #endregion

        [HttpGet("OpenTenderDetailsReportForVisitor/{tenderId}")]
        [AllowAnonymous]
        public async Task<TenderDetialsReportModel> OpenTenderDetailsReportForVisitor(int tenderId)
        {
            return await _tenderService.OpenTenderDetailsReportForVisitor(tenderId);
        }

        [HttpGet("OpenTenderDetailsReport/{tenderId}")]
        [AllowAnonymous]
        public async Task<TenderDetialsReportModel> OpenTenderDetailsReport(int tenderId)
        {
            return await _tenderService.OpenTenderDetailsReport(tenderId);
        }

        [HttpGet("OpenTenderAdvDetailsReport/{tenderId}")]
        [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
        public async Task<TenderDetialsReportModel> OpenTenderAdvDetailsReport(int tenderId)
        {
            return await _tenderService.OpenTenderDetailsReport(tenderId);
        }

        [Authorize(RoleNames.MonafasatAdminPolicy)]
        [HttpGet("GetTenderMovementsByTenderId")]
        public async Task<QueryResult<TenderMovement>> GetTenderMovementsByTenderId(SimpleTenderSearchCriteria criteria)
        {
            return await _tenderService.GetTenderMovementsByTenderId(criteria);
        }

        [HttpGet("AwardingReport/{tenderId}")]
        [Authorize(RoleNames.AwardingReportPolicy)]
        public async Task<AwardingReportModel> AwardingReport(int tenderId)
        {
            return await _tenderService.AwardingReport(tenderId);
        }

        [HttpGet("OffersReport/{tenderId}")]
        [Authorize(RoleNames.OffersReportPolicy)]
        public async Task<OfferReportModel> OffersReport(int tenderId)
        {
            return await _tenderService.OffersReport(tenderId);
        }

        [HttpGet("CountAndCloseAppliedOffers/{tenderId}")]
        [Authorize(RoleNames.CountAndCloseAppliedOffersPolicy)]
        public async Task<CountAndCloseAppliedOffersModel> CountAndCloseAppliedOffers(int tenderId)
        {
            return await _tenderService.CountAndCloseAppliedOffers(tenderId);
        }

        [HttpGet("OffersReport/")]
        [Authorize(RoleNames.OffersReportPolicy)]
        public async Task<PrintTenderReceiptReportModel> PrintTenderReceiptReport(int tenderId, string CommericalRegisterNo)
        {
            return await _tenderService.PrintTenderReceiptReport(tenderId, CommericalRegisterNo);
        }

        [HttpGet("GetCheckingResultsInformation/{tenderId}")]
        [Authorize(RoleNames.SupplierPolicy)]
        public async Task<CheckingResultsModel> GetCheckingResultsInformation(int tenderId)
        {
            var result = await _tenderService.GetCheckingResultsInformation(tenderId);
            return result;
        }

        #region Revisions

        [Authorize(RoleNames.CancelTenderPolicy)]
        [HttpGet]
        [Route("GetTenderRevisionCancelByTenderId/{tenderId}")]
        public async Task<TenderChangeRequestModel> GetTenderRevisionCancelByTenderId(int tenderId)
        {
            TenderChangeRequest tenderRevisionCancel = await _tenderService.FindTenderRevisionCancelByTenderId(tenderId, false);
            return _mapper.Map<TenderChangeRequestModel>(tenderRevisionCancel);
        }

        [Authorize(RoleNames.CancelTenderPolicy)]
        [HttpGet]
        [Route("GetTenderDataForCanelation/{tenderId}")]
        public async Task<TenderCanelationModel> GetTenderDataForCanelation(int tenderId)
        {
            TenderCanelationModel tenderCanelationModel = await _tenderService.GetTenderDataForCanelation(tenderId);
            return tenderCanelationModel;
        }


        [Authorize(RoleNames.CancelTenderPolicy)]
        [HttpGet]
        [Route("GetQualificationDataForCanelation/{tenderId}/{reOpen}")]
        public async Task<TenderCanelationModel> GetQualificationDataForCanelation(int tenderId, bool reOpen)
        {
            TenderCanelationModel tenderCanelationModel = await _tenderService.GetTenderDataForCanelation(tenderId);
            return tenderCanelationModel;
        }


        [Authorize(RoleNames.CancelTenderPolicy)]
        [HttpGet]
        [Route("GetTenderActiveCancelRequestsByTenderId/{tenderId}")]
        public async Task<TenderChangeRequestModel> GetTenderActiveCancelRequestsByTenderId(int tenderId)
        {
            TenderChangeRequest tenderRevisionCancel = await _tenderService.FindTenderRevisionCancelByTenderId(tenderId, true);
            return _mapper.Map<TenderChangeRequestModel>(tenderRevisionCancel);
        }


        [HttpGet]
        [Route("GetRevisionDateByTenderId/{tenderId}")]
        public async Task<TenderRevisionDateModel> GetRevisionDateByTenderId(int tenderId)
        {
            TenderRevisionDateModel tenderRevisionDate = await _tenderService.FindActiveTenderRevisionDateByTenderId(tenderId);
            return tenderRevisionDate;
        }


        [HttpGet]
        [Route("GetRejectedRevisionDateByTenderId/{tenderId}")]
        public async Task<TenderRevisionDateModel> GetRejectedRevisionDateByTenderId(int tenderId)
        {
            TenderRevisionDateModel tenderRevisionDateModel = await _tenderService.FindRejectedRevisionDateByTenderId(tenderId);
            return tenderRevisionDateModel;
        }

        [Authorize(RoleNames.TenderRevisionsPolicy)]
        [HttpGet]
        [Route("GetTenderRevisionDateBySearchCriteria")]
        public async Task<QueryResult<TenderExtendDateModel>> GetTenderRevisionDateBySearchCriteria(TenderRevisionSearchCriteria criteria)
        {
            if (User.IsInRole(RoleNames.MonafasatAdmin))
                criteria.AgencyCode = User.UserAgency();
            else if (User.IsInRole(RoleNames.DataEntry))
                criteria.BranchId = User.UserBranch();
            var revisionList = await _tenderService.FindTenderRevisionDateBySearchCriteria(criteria);
            return revisionList;
        }

        [Authorize(RoleNames.TenderRevisionsPolicy)]
        [HttpGet]
        [Route("GetTenderRevisionCancelBySearchCriteria")]
        public async Task<QueryResult<TenderRevisionCancelModel>> GetTenderRevisionCancelBySearchCriteria(TenderRevisionSearchCriteria criteria)
        {
            criteria.AgencyCode = User.UserAgency();
            var cancelRequests = await _tenderService.FindTenderRevisionCancelBySearchCriteria(criteria);
            var tenderRevisionCancelModelList = _mapper.Map<QueryResult<TenderRevisionCancelModel>>(cancelRequests);
            return tenderRevisionCancelModelList;
        }

        [Authorize(RoleNames.CreateCancelTenderRequestPolicy)]
        [HttpPost]
        [Route("CreateTenderCancelRequest")]
        public async Task<TenderChangeRequestModel> CreateTenderCancelRequest([FromBody] TenderRevisionCancelModel dateModel)
        {
            // try to detect roleName if user has many roles.
            if (User.IsInRole(RoleNames.DataEntry))
                dateModel.CreatedByRoleName = RoleNames.DataEntry;
            if (User.IsInRole(RoleNames.PurshaseSpecialist))
                dateModel.CreatedByRoleName = RoleNames.PurshaseSpecialist;
            else if (User.IsInRole(RoleNames.OffersOppeningSecretary))
                dateModel.CreatedByRoleName = RoleNames.OffersOppeningSecretary;
            else if (User.IsInRole(RoleNames.OffersCheckSecretary))
                dateModel.CreatedByRoleName = RoleNames.OffersCheckSecretary;
            else if (User.IsInRole(RoleNames.OffersPurchaseSecretary))
                dateModel.CreatedByRoleName = RoleNames.OffersPurchaseSecretary;
            else if (User.IsInRole(RoleNames.OffersPurchaseManager))
                dateModel.CreatedByRoleName = RoleNames.OffersPurchaseManager;
            else if (User.IsInRole(RoleNames.OffersOpeningAndCheckSecretary))
                dateModel.CreatedByRoleName = RoleNames.OffersOpeningAndCheckSecretary;
            else if (User.IsInRole(RoleNames.PreQualificationCommitteeSecretary))
                dateModel.CreatedByRoleName = RoleNames.PreQualificationCommitteeSecretary;

            TenderChangeRequest tenderRevisionCancel = await _tenderService.CreateCancelRequest(dateModel);
            return _mapper.Map<TenderChangeRequestModel>(tenderRevisionCancel);
        }


        [Authorize(RoleNames.CreateCancelTenderRequestPolicy)]
        [HttpPost]
        [Route("CreateQualificationCancelRequest")]
        public async Task<TenderChangeRequestModel> CreateQualificationCancelRequest([FromBody] TenderRevisionCancelModel dateModel)
        {
            // try to detect roleName if user has many roles.
            if (User.IsInRole(RoleNames.DataEntry))
                dateModel.CreatedByRoleName = RoleNames.DataEntry;
            if (User.IsInRole(RoleNames.PurshaseSpecialist))
                dateModel.CreatedByRoleName = RoleNames.PurshaseSpecialist;
            else if (User.IsInRole(RoleNames.OffersOppeningSecretary))
                dateModel.CreatedByRoleName = RoleNames.OffersOppeningSecretary;
            else if (User.IsInRole(RoleNames.OffersCheckSecretary))
                dateModel.CreatedByRoleName = RoleNames.OffersCheckSecretary;
            else if (User.IsInRole(RoleNames.OffersPurchaseSecretary))
                dateModel.CreatedByRoleName = RoleNames.OffersPurchaseSecretary;
            else if (User.IsInRole(RoleNames.OffersOpeningAndCheckSecretary))
                dateModel.CreatedByRoleName = RoleNames.OffersOpeningAndCheckSecretary;
            else if (User.IsInRole(RoleNames.PreQualificationCommitteeSecretary))
                dateModel.CreatedByRoleName = RoleNames.PreQualificationCommitteeSecretary;

            TenderChangeRequest tenderRevisionCancel = await _tenderService.CreateCancelRequestForQualification(dateModel);
            return _mapper.Map<TenderChangeRequestModel>(tenderRevisionCancel);
        }



        [Authorize(RoleNames.ApproveOrRejectTenderCancelRequestPolicy)]
        [HttpPost]
        [Route("ApproveTenderCancelRequestAsync")]
        public async Task<bool> ApproveTenderCancelRequestAsync([FromBody] TenderCancelModel cancelModel)
        {
            bool action = await _tenderService.ApproveTenderCancelRequest(cancelModel);
            return action;
        }

        [Authorize(RoleNames.ApproveOrRejectTenderCancelRequestPolicy)]
        [HttpPost]
        [Route("ApproveQualificationCancelRequestAsync/{id}/{verificationCode}")]
        public async Task<bool> ApproveQualificationCancelRequestAsync(int id, string verificationCode)
        {
            int typeId = (int)Enums.VerificationType.Tender;
            bool action = await _tenderService.ApproveQualificationCancelRequestAsync(id, verificationCode, typeId);
            return action;
        }

        [Authorize(RoleNames.ApproveOrRejectTenderCancelRequestPolicy)]
        [HttpPost]
        [Route("RejectTenderCancelRequestAsync")]
        public async Task<bool> RejectTenderCancelRequestAsync([FromBody] RejectionModel rejectionModel)
        {
            bool action = await _tenderService.RejectTenderCancelRequestAsync(rejectionModel.TenderId, rejectionModel.RejectionReason);
            return action;
        }

        [Authorize(RoleNames.ApproveOrRejectTenderCancelRequestPolicy)]
        [HttpPost]
        [Route("RejectQualificationCancelRequestAsync")]
        public async Task<bool> RejectQualificationCancelRequestAsync([FromBody] RejectionModel rejectionModel)
        {
            bool action = await _tenderService.RejectQualificationCancelRequestAsync(rejectionModel.TenderId, rejectionModel.RejectionReason);
            return action;
        }

        [Authorize(RoleNames.MonafasatAdminPolicy)]
        [HttpPost]
        [Route("JoinCommitteeToAllTenders")]
        public async Task JoinCommitteeToAllTenders([FromBody] CommitteeTenderModel committeeTenderModel)
        {
            committeeTenderModel.AgencyCode = User.UserAgency();
            await _tenderService.JoinCommitteeToAllTenders(committeeTenderModel);
        }

        [Authorize(RoleNames.MonafasatAdminPolicy)]
        [HttpPost]
        [Route("JoinCommitteeToTenders")]
        public async Task JoinCommitteeToTenders([FromBody] CommitteeTenderModel committeeTenderModel)
        {
            await _tenderService.JoinCommitteeToTenders(committeeTenderModel);
        }

        [Authorize(RoleNames.MonafasatAdminPolicy)]
        [HttpPost]
        [Route("JoinCommitteeToBranchTenders")]
        public async Task JoinCommitteeToBranchTenders([FromBody] CommitteeTenderModel committeeTenderModel)
        {
            committeeTenderModel.AgencyCode = User.UserAgency();
            await _tenderService.JoinCommitteeToBranchTenders(committeeTenderModel);
        }

        [Authorize(RoleNames.MonafasatAdminPolicy)]
        [HttpPost]
        [Route("CancelJoinCommitteeToTender")]
        public async Task CancelJoinCommitteeToTender([FromBody] CommitteeTenderModel committeeTenderModel)
        {
            await _tenderService.CancelJoinCommitteeToTender(committeeTenderModel);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("SendToFavouriteTenderSuppliersToApplyOffer")]
        public async Task SendToFavouriteTenderSuppliersToApplyOffer()
        {
            await _tenderService.GetFavouriteTenderSuppliersToApplyOffer();
        }

        [Authorize(RoleNames.SupplierPolicy)]
        [HttpGet]
        [Route("GetRelatedTendersByActivities/{tenderId}")]
        public async Task<List<BasicTenderModel>> GetRelatedTendersByActivities(int tenderId)
        {
            List<Tender> tenders = await _tenderService.GetRelatedTendersByActivities(tenderId);
            bool statusFlag = GetStatusFlag();
            var _tenders = _mapper.Map<List<BasicTenderModel>>(tenders, opt => opt.Items[nameof(statusFlag)] = statusFlag);
            return _tenders;
        }

        #endregion

        #region Suscriptions

        [HttpGet("GetAwardingInformationForSupplier/{tenderId}")]
        [Authorize(RoleNames.SupplierPolicy)]
        public async Task<AwardingDetailsModel> GetAwardingInformationForSupplier(int tenderId)
        {

            var result = await _tenderService.GetAwardingInformationForSupplier(tenderId, User.SupplierNumber());
            return result;
        }

        #endregion

        #region TenderNews

        [HttpGet("GetTenderNewsByTenderId/{tenderId}")]
        [Authorize(RoleNames.SupplierPolicy)] // To Be Supplier
        public async Task<TenderNewsModel> GetTenderNewsByTenderId(int tenderId)
        {
            return await _tenderService.GetTenderNewsByTenderId(tenderId);
        }

        #endregion

        #region [Supplier Info]
        [HttpGet("ValidateMCICRAndGetInfo/{CR}")]

        public async Task<SupplierInfoStatusModel> ValidateMCICRAndGetInfo(string CR)
        {
            return await _supplierService.ValidateMCICRAndGetInfo(CR);

        }
        [HttpGet("GetCOCSubscriptionBySijilNumber/{LicenseNumber}/{CityCode}")]

        public async Task<SupplierInfoStatusModel> GetCOCSubscriptionBySijilNumber(string LicenseNumber, string CityCode)
        {
            return await _supplierService.GetCOCSubscriptionBySijilNumber(LicenseNumber, CityCode);

        }
        [HttpGet("ZakatCertificateInquiryByCR/{CR}")]
        public async Task<SupplierInfoStatusModel> ZakatCertificateInquiryByCR(string CR)
        {
            return await _supplierService.ZakatCertificateInquiryByCR(CR);

        }

        [HttpGet("ContractorDetailsInquiry/{partyNumberId}")]
        public async Task<SupplierInfoStatusModel> ContractorDetailsInquiry(string partyNumberId)
        {
            return await _supplierService.ContractorDetailsInquiry(partyNumberId);

        }

        [HttpGet("LicenseStatusInquiry/{licenseNumber}")]
        public async Task<SupplierInfoStatusModel> LicenseStatusInquiry(string licenseNumber)
        {
            return await _supplierService.LicenseStatusInquiry(licenseNumber);

        }
        [HttpGet("GOSICertificateInquiry/{GOSIId}")]
        public async Task<SupplierInfoStatusModel> GOSICertificateInquiry(string GOSIId)
        {
            return await _supplierService.GOSICertificateInquiry(GOSIId);

        }
        [HttpGet("NitaqatInformationInquiry/{EstablishmentLaborOfficeId}/{EstablishmentSequenceNumber}")]
        public async Task<SupplierInfoStatusModel> NitaqatInformationInquiry(string EstablishmentLaborOfficeId, string EstablishmentSequenceNumber)
        {
            return await _supplierService.NitaqatInformationInquiry(EstablishmentLaborOfficeId, EstablishmentSequenceNumber);

        }
        #endregion

        [HttpPost("UpdateTenderSyncDetails")]
        public async Task UpdateTenderSyncDetails(int tenderId, string requestContent, bool tenderUpdateStatus)
        {
            await _tenderService.SaveSyncInformation(tenderId, requestContent, tenderUpdateStatus);
        }

        #region Unit Stage

        [Authorize(RoleNames.UnitSpecialistsAndManagerUserPolicy)]
        [HttpGet]
        [Route("GetTendersForUnitStageIndexAsync")]
        public async Task<QueryResult<BasicTenderModel>> GetTendersForUnitStageIndexAsync(TenderSearchCriteria tenderSearchCriteria)
        {
            tenderSearchCriteria.AgencyCode = User.UserAgency();
            tenderSearchCriteria.UserId = User.UserId();
            var tenderList = await _tenderService.FindTendersForUnitStageBySearchCriteria(tenderSearchCriteria);
            return tenderList;
        }

        #region LevelOne

        [Authorize(RoleNames.UnitSpecialistsAndManagerUserPolicy)]
        [HttpGet]
        [Route("GetAllUnitSecretryLevelTwo/{roleName}")]
        public async Task<List<LookupModel>> GetAllUnitSecretryLevelTwo(string roleName)
        {
            return await _tenderService.GetAllUnitUsersByRoleName(roleName);
        }

        [Authorize(RoleNames.UnitSpecialistLevel1Policy)]
        [HttpPost]
        [Route("OpenTenderByUnitSecretaryAsync/{tenderIdString}")]
        public async Task OpenTenderByUnitSecretaryAsync(string tenderIdString)
        {
            await _tenderService.OpenTenderByUnitSecretaryAsync(tenderIdString);
        }

        [Authorize(RoleNames.UnitSpecialistLevel1Policy)]
        [HttpPost]
        [Route("SendTenderByUnitSecretaryForModificationAsync")]
        public async Task SendTenderByUnitSecretaryForModificationAsync([FromBody] ReturnTenderToDataEntryFromUnitFormodificationsModel returnTenderToDataEntryFromUnitFormodificationsModel)
        {
            await _tenderService.SendTenderByUnitSecretaryForModificationAsync(returnTenderToDataEntryFromUnitFormodificationsModel);
        }

        [Authorize(RoleNames.UnitSpecialistLevel1Policy)]
        [HttpPost]
        [Route("ApproveTenderByUnitSecretaryAsync/{tenderIdString}/{IWouldLikeToAttendTheommitte}")]
        public async Task ApproveTenderByUnitSecretaryAsync(string tenderIdString, bool IWouldLikeToAttendTheommitte)
        {
            await _tenderService.ApproveTenderByUnitSecretaryAsync(tenderIdString, IWouldLikeToAttendTheommitte);
        }

        [Authorize(Roles = RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2)]
        [HttpPost]
        [Route("UpdateTenderLocalContentValues")]
        public async Task UpdateTenderLocalContentValues([FromBody] UpdateTenderNationalProductValuesViewModel viewModel)
        {
            await _tenderService.UpdateTenderLocalContentValues(viewModel);
        }

        [Authorize(RoleNames.UnitSpecialistLevel1Policy)]
        [HttpPost]
        [Route("SendToLevelTwoFromUnitSecretaryLevelOneAsync/{tenderIdString}/{userName}")]
        public async Task SendToLevelTwoFromUnitSecretaryLevelOneAsync(string tenderIdString, string userName, [FromBody] string notes)
        {
            await _tenderService.SendToLevelTwoFromUnitSecretaryLevelOneAsync(tenderIdString, userName, notes);
        }

        [Authorize(RoleNames.UnitSpecialistLevel1Policy)]
        [HttpPost]
        [Route("SendToApproveFromUnitSecretaryToUnitMangerAsync/{tenderIdString}")]
        public async Task SendToApproveFromUnitSecretaryToUnitMangerAsync(string tenderIdString)
        {
            await _tenderService.SendToApproveFromUnitSecretaryToUnitMangerAsync(tenderIdString);
        }

        [Authorize(RoleNames.UnitSpecialistLevel1Policy)]
        [HttpPost]
        [Route("RejectTenderByUnitSecretaryAsync/{tenderIdString}/{comment}")]
        public async Task RejectTenderByUnitSecretaryAsync(string tenderIdString, string comment)
        {
            await _tenderService.RejectTenderByUnitSecretaryAsync(tenderIdString, comment);
        }

        [Authorize(RoleNames.UnitSpecialistLevel1Policy)]
        [HttpPost]
        [Route("ReOpenTenderByUnitSecertaryAsync/{tenderIdString}")]
        public async Task ReOpenTenderByUnitSecertaryAsync(string tenderIdString)
        {
            await _tenderService.ReOpenTenderByUnitSecertaryAsync(tenderIdString);
        }

        #endregion LevelOne

        #region LevelTwo

        [Authorize(RoleNames.UnitSpecialistLevel2Policy)]
        [HttpPost]
        [Route("OpenTenderByUnitSecretaryLevelTwoAsync/{tenderIdString}")]
        public async Task OpenTenderByUnitSecretaryLevelTwoAsync(string tenderIdString)
        {
            await _tenderService.OpenTenderByUnitSecretaryLevelTwoAsync(tenderIdString);
        }

        [Authorize(RoleNames.UnitSpecialistLevel2Policy)]
        [HttpPost]
        [Route("SendTenderByUnitSecretaryLevelTwoForModificationAsync")]
        public async Task SendTenderByUnitSecretaryLevelTwoForModificationAsync([FromBody] ReturnTenderToDataEntryFromUnitFormodificationsModel returnTenderToDataEntryFromUnitFormodificationsModel)
        {
            await _tenderService.SendTenderByUnitSecretaryLevelTwoForModificationAsync(returnTenderToDataEntryFromUnitFormodificationsModel);
        }

        [Authorize(RoleNames.UnitSpecialistLevel2Policy)]
        [HttpPost]
        [Route("RejectTenderByUnitSecretaryLevelTwoAsync/{tenderIdString}/{comment}")]
        public async Task RejectTenderByUnitSecretaryLevelTwoAsync(string tenderIdString, string comment)
        {
            await _tenderService.RejectTenderByUnitSecretaryLevelTwoAsync(tenderIdString, comment);
        }

        [Authorize(RoleNames.UnitSpecialistLevel2Policy)]
        [HttpPost]
        [Route("ApproveTenderByUnitSecretaryLevelTwoAsync/{tenderIdString}")]
        public async Task ApproveTenderByUnitSecretaryLevelTwoAsync(string tenderIdString)
        {
            await _tenderService.ApproveTenderByUnitSecretaryLevelTwoAsync(tenderIdString);
        }

        [Authorize(RoleNames.UnitSpecialistLevel2Policy)]
        [HttpPost]
        [Route("SendToApproveFromUnitSecretaryLevelTwoToUnitMangerAsync/{tenderIdString}")]
        public async Task SendToApproveFromUnitSecretaryLevelTwoToUnitMangerAsync(string tenderIdString)
        {
            await _tenderService.SendToApproveFromUnitSecretaryLevelTwoToUnitMangerAsync(tenderIdString);
        }

        [Authorize(RoleNames.UnitSpecialistLevel2Policy)]
        [HttpPost]
        [Route("ReOpenTenderByUnitSecertaryLevelAsync/{tenderIdString}")]
        public async Task ReOpenTenderByUnitSecertaryLevelAsync(string tenderIdString)
        {
            await _tenderService.ReOpenTenderByUnitSecertaryLevelAsync(tenderIdString);
        }

        #endregion LevelTwo

        #region Unit Manager

        [Authorize(RoleNames.UnitManagerUserPolicy)]
        [HttpPost]
        [Route("ReviewTenderByUnitManager/{tenderIdString}")]
        public async Task ReviewTenderByUnitManager(string tenderIdString)
        {
            await _tenderService.ReviewTenderByUnitManager(tenderIdString);
        }

        [Authorize(RoleNames.UnitManagerUserPolicy)]
        [HttpPost]
        [Route("ApproveTenderByUnitManagerAsync/{tenderIdString}/{verificationCode}")]
        public async Task ApproveTenderByUnitManagerAsync(string tenderIdString, string verificationCode)
        {
            await _tenderService.ApproveTenderByUnitManagerAsync(tenderIdString, verificationCode);
        }

        [Authorize(RoleNames.UnitManagerUserPolicy)]
        [HttpPost]
        [Route("RejectTenderByUnitManagerAsync/{tenderIdString}/{comment}")]
        public async Task RejectTenderByUnitManagerAsync(string tenderIdString, string comment)
        {
            await _tenderService.RejectTenderByUnitManagerAsync(tenderIdString, comment);
        }

        #endregion Unit Manager

        [Authorize(RoleNames.UnitSpecialistsAndManagerUserPolicy)]
        [HttpPost]
        [Route("SendToNewUserUnitBusinessManagementAsync/{tenderIdString}/{userName}")]
        public async Task SendToNewUserUnitBusinessManagementAsync(string tenderIdString, string userName)
        {
            await _tenderService.SendToNewUserUnitBusinessManagementAsync(tenderIdString, userName);
        }

        #endregion 

        #region Bidding Round

        [HttpGet("GetBiddingRoundOffersByBiddingRoundId/{tenderIdString}")]
        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.supplier)]
        public async Task<BiddingTenderDetailsModel> GetBiddingRoundOffersByBiddingRoundId(string tenderIdString)
        {
            var cR = User.SupplierNumber();
            var result = await _tenderService.FindBiddingRoundOffersByBiddingRoundId(tenderIdString, cR);
            return result;
        }

        [HttpGet("GetBiddingRoundDetailsByBiddingRoundId/{tenderIdString}")]
        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.supplier)]
        public async Task<BiddingTenderDetailsModel> GetBiddingRoundDetailsByBiddingRoundId(string tenderIdString)
        {
            var cR = User.SupplierNumber();
            var result = await _tenderService.FindBiddingRoundDetailsByBiddingRoundId(tenderIdString, cR);
            return result;
        }

        [HttpPost("EndTenderPedding/{tenderIdString}/{biddingRoundIdString}/{verificationCode}")]
        [Authorize(Roles = RoleNames.OffersCheckSecretary)]
        public async Task EndTenderPedding(string tenderIdString, string biddingRoundIdString, string verificationCode)
        {
            await _tenderService.EndTenderPedding(tenderIdString, biddingRoundIdString, verificationCode);
        }

        [HttpPost("StartNewRound")]
        [Authorize(Roles = RoleNames.OffersCheckSecretary)]
        public async Task StartNewRound(/*string verificationCode, */[FromBody] BiddingDateTimeViewModel biddingDateTimeViewModel)
        {
            await _tenderService.StartNewRound(biddingDateTimeViewModel/*, verificationCode*/);

        }

        [Authorize(Roles = RoleNames.supplier)]
        [HttpPost("AddOfferBid/{tenderIdString}/{biddingRoundIdString}/{biddingValue}")]
        public async Task AddOfferBid(string tenderIdString, string biddingRoundIdString, decimal biddingValue)
        {
            var cr = User.SupplierNumber();
            await _offerAppService.AddOfferBid(tenderIdString, biddingRoundIdString, biddingValue, cr);
        }
        #endregion

        #region VRO
        [Authorize(RoleNames.VROOpenAndCheckingViewPolicy)]
        [HttpGet]
        [Route("GetVROTendersIndexCheckingAndOpeningStageIndex")]
        public async Task<QueryResult<VROTenderCheckingAndOpeningModel>> GetVROTendersIndexCheckingAndOpeningStageIndex(TenderSearchCriteria tenderSearchCriteria)
        {
            tenderSearchCriteria.UserId = User.UserId();
            tenderSearchCriteria.SelectedAgencyCode = User.UserAgency();
            tenderSearchCriteria.SelectedCommitteeId = User.SelectedCommittee();
            var tenderListModel = await _tenderService.FindTendersForVROCheckingAndOpeningStageBySearchCriteria(tenderSearchCriteria);
            return tenderListModel;
        }

        [Authorize(RoleNames.VROOpenAndCheckingViewPolicy)]
        [HttpGet]
        [Route("FindVROTenderOfferDetailsByTenderIdForCheckStage/{tenderIdString}")]
        public async Task<VROTenderOffersModel> FindVROTenderOfferDetailsByTenderIdForCheckStage(string tenderIdString)
        {
            int userId = User.UserId();
            VROTenderOffersModel tender = await _tenderService.FindVROTenderOfferDetailsByTenderIdForCheckStage(tenderIdString, userId);
            return tender;
        }

        [Authorize(RoleNames.VROOpenAndCheckingViewPolicy)]
        [HttpPost]
        [Route("StartVROTenderOffersCheckingAsync/{tenderIdString}")]
        public async Task StartVROTenderOffersCheckingAsync(string tenderIdString)
        {
            await _tenderService.StartVROTenderOffersCheckingAsync(tenderIdString);
        }

        #region Technical

        [HttpPost]
        [Route("SendVROTenderOffersTechnicalCheckingToApproveAsync/{tenderIdString}")]
        public async Task SendVROTenderOffersTechnicalCheckingToApproveAsync(string tenderIdString)
        {
            await _tenderService.SendVROTenderOffersTechnicalCheckingToApproveAsync(tenderIdString);
        }

        //[Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
        [HttpPost]
        [Route("ReopenVROTenderOffersTechnicalCheckingAsync/{tenderIdString}")]
        public async Task ReopenVROTenderOffersTechnicalCheckingAsync(string tenderIdString)
        {
            await _tenderService.ReopenVROTenderOffersTechnicalCheckingAsync(tenderIdString);
        }

        [HttpPost]
        [Route("ApproveVROTenderOffersTechnicalCheckingAsync/{tenderIdString}/{verificationCode}")]
        public async Task ApproveVROTenderOffersTechnicalCheckingAsync(string tenderIdString, string verificationCode)
        {
            int typeId = (int)Enums.VerificationType.Tender;
            await _verification.CheckForVerificationCode(Util.Decrypt(tenderIdString), verificationCode, typeId);
            await _tenderService.ApproveVROTenderOffersTechnicalCheckingAsync(tenderIdString);
        }

        [HttpPost]
        [Route("RejectVROTenderOffersTechnicalCheckingAsync")]
        public async Task RejectVROTenderOffersTechnicalCheckingAsync([FromBody] RejectionModel rejectionModel)
        {
            await _tenderService.RejectVROTenderOffersTechnicalCheckingAsync(rejectionModel.TenderIdString, rejectionModel.RejectionReason);

        }
        #endregion Technical

        #region Financial

        [Authorize(RoleNames.VROOpenAndCheckingViewPolicy)]
        [HttpPost]
        [Route("StartVROTenderOffersFinancialCheckingAsync/{tenderIdString}/{estimatedValue}")]
        public async Task StartVROTenderOffersFinancialCheckingAsync(string tenderIdString, decimal estimatedValue)
        {
            await _tenderService.StartVROTenderOffersFinancialCheckingAsync(tenderIdString, estimatedValue);
        }

        //[Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
        [HttpPost]
        [Route("SendVROTenderOffersFinanceCheckingToApproveAsync/{tenderIdString}")]
        public async Task SendVROTenderOffersFinanceCheckingToApproveAsync(string tenderIdString)
        {
            await _tenderService.SendVROTenderOffersFinanceCheckingToApproveAsync(tenderIdString);
        }

        //[Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
        [HttpPost]
        [Route("ReopenVROTenderOffersFinancialCheckingAsync/{tenderIdString}")]
        public async Task ReopenVROTenderOffersFinancialCheckingAsync(string tenderIdString)
        {
            await _tenderService.ReopenVROTenderOffersFinancialCheckingAsync(tenderIdString);
        }

        [HttpPost]
        [Route("ApproveVROTenderOffersFinanceCheckingAsync/{tenderIdString}/{verificationCode}")]
        public async Task ApproveVROTenderOffersFinanceCheckingAsync(string tenderIdString, string verificationCode)
        {
            int typeId = (int)Enums.VerificationType.Tender;
            await _verification.CheckForVerificationCode(Util.Decrypt(tenderIdString), verificationCode, typeId);
            await _tenderService.ApproveVROTenderOffersFinanceCheckingAsync(tenderIdString);
        }

        [HttpPost]
        [Route("RejectVROTenderOffersFinanceCheckingAsync")]
        public async Task RejectVROTenderOffersFinanceCheckingAsync([FromBody] RejectionModel rejectionModel)
        {
            await _tenderService.RejectVROTenderOffersFinanceCheckingAsync(rejectionModel.TenderIdString, rejectionModel.RejectionReason);
        }

        #endregion Financial

        #endregion VRO

        [Authorize(RoleNames.UnitSpecialistsAndManagerUserPolicy)]
        [HttpGet]
        [Route("GetAllTenderhasNegotiationbySearchCretriaForUnitUsers")]
        public async Task<QueryResult<NegotiationOnTenderModel>> GetAllTenderhasNegotiationbySearchCretriaForUnitUsers(NegotiationOnTenderSearchCriteriaModel criteria)
        {
            var tenders = await _tenderService.GetAllTenderhasNegotiationbySearchCretriaForUnitUsers(criteria);
            return tenders;
        }


        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpPost("ValidateQuantitiesTables")]
        public async Task<ValidationReturndTemplate> ValidateQuantitiesTables([FromBody] Dictionary<string, string> AuthorList)
        {
            return await _tenderService.ValidateQuantitiesData(AuthorList);
        }
        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpPost("DeleteTenderQuantityItem")]
        public async Task<ValidationReturndTemplate> DeleteTenderQuantityItem([FromBody] Dictionary<string, string> authorList)
        {
            return await _tenderService.DeleteTenderQuantityItem(authorList);
        }
        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpPost("ValidateQuantitiesTablesChanges")]
        public async Task<ValidationReturndTemplate> ValidateQuantitiesTablesChanges([FromBody] Dictionary<string, string> AuthorList)
        {
            return await _tenderService.ValidateQuantitiesChangesData(AuthorList);
        }
        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpPost("DeleteTenderQuantityItemChanges")]
        public async Task<ValidationReturndTemplate> DeleteTenderQuantityItemChanges([FromBody] Dictionary<string, string> authorList)
        {
            return await _tenderService.DeleteTenderQuantityItemChanges(authorList);
        }
       
        [HttpGet]
        [Authorize(RoleNames.DataEntryPolicy)]
        [Route("GetQauntityTableVersionId")]
        public async Task<int> GetQauntityTableVersionId()
        {
            int qauntityTableVersionId = await _tenderService.GetQauntityTableVersionId();
            return qauntityTableVersionId;
        }

        [Authorize(RoleNames.DetailsPolicy)]
        [HttpGet]
        [Route("GetTenderQuantityItemsById/{id}")]
        public async Task<QuantityTableStepDTO> GetTenderQuantityItemsById(int id)
        {
            QuantityTableStepDTO model = new QuantityTableStepDTO();
            if (id == 0)
            {
                model = new QuantityTableStepDTO();
            }
            else
            {
                model = await _tenderService.FindTenderQuantityItemsByIdForCreation(id, User.UserBranch(), false);

                if (model == null || model.TenderTypeId == (int)Enums.TenderType.Competition || model.TenderTypeId == (int)Enums.TenderType.FirstStageTender)
                {
                    throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
                }
            }
            return model;
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpGet]
        [Route("GetTenderQuantityItemsChangesById/{id}")]
        public async Task<QuantityTableStepDTO> GetTenderQuantityItemsChangesById(int id)
        {
            QuantityTableStepDTO model = new QuantityTableStepDTO();
            if (id == 0)
            {
                model = new QuantityTableStepDTO();
            }
            else
            {
                model = await _tenderService.FindTenderQuantityItemsChangesById(id);
                if (model == null || model.TenderTypeId == (int)Enums.TenderType.Competition || model.TenderTypeId == (int)Enums.TenderType.FirstStageTender)
                {
                    throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
                }
            }
            return model;
        }

        [Authorize(RoleNames.DetailsPolicy)]
        [HttpGet]
        [Route("GetReadOnlyTenderQuantityItemsChangesById/{id}")]
        public async Task<QuantityTableStepDTO> GetReadOnlyTenderQuantityItemsChangesById(int id)
        {
            QuantityTableStepDTO model = new QuantityTableStepDTO();
            if (id == 0)
            {
                model = new QuantityTableStepDTO();
            }
            else
            {
                model = await _tenderService.FindTenderQuantityItemsChangesReadOnlyById(id);
                if (model == null || model.TenderTypeId == (int)Enums.TenderType.Competition || model.TenderTypeId == (int)Enums.TenderType.FirstStageTender)
                {
                    throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
                }
            }
            return model;
        }

        [Authorize(RoleNames.DetailsPolicy)]
        [HttpGet]
        [Route("UpdateTableName/{tableid}/{tenderId}/{tableName}")]
        public async Task<long> UpdateTableName(int tableid, int tenderId, string tableName)
        {
            return await _tenderService.UpdateTableName(tableid, tenderId, tableName);
        }

        [Authorize(RoleNames.DetailsPolicy)]
        [HttpGet]
        [Route("UpdateTableChangesName/{tableid}/{tenderId}/{tableName}")]
        public async Task<long> UpdateTableChangesName(int tableid, int tenderId, string tableName)
        {
            return await _tenderService.UpdateTableChangesName(tableid, tenderId, tableName);
        }

        [Authorize(RoleNames.DetailsPolicy)]
        [HttpGet]
        [Route("UpdateHasAlternative/{tenderId}/{hasAlternative}")]
        public async Task UpdateHasAlternative(int tenderId, bool hasAlternative)
        {
            await _tenderService.UpdateHasAlternative(tenderId, hasAlternative);
        }

        [Authorize(RoleNames.DetailsPolicy)]
        [HttpGet]
        [Route("GetEmptyForm/{formid}/{tenderId}/{templateYears}/{tableName}")]
        public async Task<string> GetEmptyForm(int formid, int tenderId, int templateYears, string tableName)
        {
            return await _tenderService.GetEmptyForm(formid, tenderId, templateYears, tableName);
        }

        [Authorize(RoleNames.DetailsPolicy)]
        [HttpGet]
        [Route("DeleteTable/{tableId}/{tenderId}")]
        public async Task DeleteTable(int tableId, int tenderId)
        {
            await _tenderService.DeleteTable(tableId, tenderId);
        }

        [Route("RemoveUnRegisterSupplier/{tenderId}/{crNumber}")]
        public async Task RemoveUnRegisterSupplier(int tenderId, string crNumber)
        {
            await _tenderService.RemoveUnRegisterSupplier(tenderId, crNumber);
        }

        [Authorize(RoleNames.DetailsPolicy)]
        [HttpGet]
        [Route("DeleteTableChange/{tableId}/{tenderId}/{isNewTable}")]
        public async Task DeleteTableChange(int tableId, int tenderId, bool isNewTable)
        {
            await _tenderService.DeleteTableChange(tableId, tenderId, isNewTable);
        }

        [Authorize(RoleNames.DetailsPolicy)]
        [HttpGet]
        [Route("ChangeHasAlternativeOffer/{tenderId}/{hasAlternativeOffer}")]
        public async Task ChangeHasAlternativeOffer(int tenderId, bool hasAlternativeOffer)
        {
            await _tenderService.ChangeHasAlternativeOffer(tenderId, hasAlternativeOffer);
        }

        [Authorize(RoleNames.DetailsPolicy)]
        [HttpGet]
        [Route("DeleteQuantityTableChangeRequest/{tenderId}")]
        public async Task DeleteQuantityTableChangeRequest(int tenderId)
        {
            await _tenderService.DeleteQuantityTableChangeRequest(tenderId);
        }

        [Authorize(RoleNames.DetailsPolicy)]
        [HttpGet]
        [Route("DeleteExistingTableChange/{tableId}/{tenderId}")]
        public async Task DeleteExistingTableChange(long tableId, int tenderId)
        {
            await _tenderService.DeleteExistingTableChange(tableId, tenderId);
        }

        [Authorize]
        [HttpGet]
        [Route("GetTenderQuantityItemsDetailsById/{id}")]
        public async Task<QuantityTableStepDTO> GetTenderQuantityItemsDetailsById(int id)
        {
            QuantityTableStepDTO model;
            if (id == 0)
                model = new QuantityTableStepDTO();
            else
                model = await _tenderService.FindTenderQuantityItemsById(id, true);
            return model ?? new QuantityTableStepDTO();
        }
        [HttpGet]
        [Route("GetTenderQuantityItemsForPrint/{id}")]
        public async Task<TemplateQuantityTableModel> GetTenderQuantityItemsForPrint(int id)
        {
            TemplateQuantityTableModel model;
            if (id != 0)
                model = await _tenderService.FindTenderQuantityItemsForConditionsBookletById(id);
            else
                model = new TemplateQuantityTableModel();
            return model;
        }
       
        [HttpGet("GetAllQuantityTableRowType")]
        public async Task<List<LookupModel>> GetAllQuantityTableRowType()
        {
            var result = await _lookupService.GetAllQuantityTableRowTypes();
            return result;
        }

        [HttpGet]
        [Route("AgencyProjectBudget/{ProjectNumber}/{IsGfs}/{AgencyCode}")]
        public async Task<AgencyBudgetModel> AgencyProjectBudget(string ProjectNumber, bool IsGfs, string AgencyCode)
        {
            return await _agencyBudgetService.GetAgencyProjectBudget(ProjectNumber, IsGfs, AgencyCode);
        }

        #region Unregistered-Suppliers-Invitations

        [HttpGet("CheckForCrNumberExist/{crNumber}")]
        public async Task<UnRegisteredSuppliersInvitationsModel> CheckForCrNumberExist(string crNumber)
        {
            var result = await _iWathiqService.GetCRDataByCR(crNumber);
            UnRegisteredSuppliersInvitationsModel model = new UnRegisteredSuppliersInvitationsModel();
            if (result != null)
            {
                model.CrName = result.Name;
            }
            return model;
        }

        [HttpPost("SendInvitationsForUnRegisteredSupplier")]
        public async Task SendInvitationsForUnRegisteredSupplier([FromBody] UnRegisteredSuppliersInvitationsModel invitationsModel)
        {
            await _tenderService.SendInvitationsForUnRegisteredSupplier(invitationsModel);
        }

        #endregion

        [HttpGet]
        [Route("GetTenderTableQuantityItems")]
        public async Task<QueryResult<TableModel>> GetTenderTableQuantityItems(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            return await _tenderService.GetTenderTableQuantityItems(quantityTableSearchCretriaModel);
        }
        [HttpGet]
        [Route("GetTenderTableQuantityItemsChanges")]
        public async Task<QueryResult<TableModel>> GetTenderTableQuantityItemsChanges(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            return await _tenderService.GetTenderTableQuantityItemsChanges(quantityTableSearchCretriaModel);
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpGet]
        [Route("AddNewTable/{formId}/{tenderId}/{templateYears}/{tableName}")]
        public async Task<long> AddNewTable(int formId, int tenderId, int templateYears, string tableName)
        {
            return await _tenderService.AddNewTable(formId, tenderId, templateYears, tableName);
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpGet]
        [Route("AddNewTableChange/{formId}/{tenderId}/{templateYears}/{tableName}")]
        public async Task<long> AddNewTableChange(int formId, int tenderId, int templateYears, string tableName)
        {
            return await _tenderService.AddNewTableChange(formId, tenderId, templateYears, tableName);
        }
        [HttpGet]
        [Route("ExportTenderTableQuantityItems/{tableId}/{isNewChange}")]
        public async Task<List<TenderQuantityItemDTO>> ExportTenderTableQuantityItems(int tableId, bool isNewChange)
        {
            return await _tenderService.ExportTenderTableQuantityItems(tableId, isNewChange);
        }



        [HttpPost("ImportTenderTableQuantityItemsAsync")]
        [DisableRequestSizeLimit]
        public async Task ImportTenderTableQuantityItemsAsync([FromBody] UploadTableExcelModel model)
        {
            await _tenderService.ImportTenderTableQuantityItems(model);
        }
        [HttpPost("RemoveAttachmentChangeById/{attachmentId}")]
        [Authorize(RoleNames.DataEntryPolicy)]
        public async Task RemoveAttachmentChangeById(int attachmentId)
        {
            await _tenderService.RemoveAttachmentChangeById(attachmentId);
        }

        [HttpGet("GetAllTenderTypesAddedValue")]
        [Authorize(RoleNames.ViewAddedValuePolicy)]
        public async Task<List<TenderTypeWithAddedValueModel>> GetAllTenderTypesAddedValue()
        {
            return await _tenderService.GetAllTenderTypesAddedValue();
        }

        [HttpPost]
        [Route("UpdateTenderTypesAddedValues")]
        [Authorize(RoleNames.EditAddedValuePolicy)]
        public async Task UpdateTenderTypesAddedValues([FromBody] List<TenderTypeWithAddedValueModel> tenderTypes)
        {
            await _tenderService.UpdateTenderTypeAddedValueAsync(tenderTypes);
        }
        [HttpGet]
        [Route("GenerateQuantityTableTemplateExcel/{stageId}/{formId}/{yearsCount}")]
        public async Task<QuantityTableModelForExcel> GenerateQuantityTableTemplateExcel(int stageId, int formId, int yearsCount)
        {
            return await _tenderService.GenerateQuantityTableTemplateExcel(stageId, formId, yearsCount);
        }

        [HttpGet]
        [Route("GenerateQuantityTableTemplateExcel_New/{stageId}/{formId}/{yearsCount}")]
        public async Task<ExcelHeader> GenerateQuantityTableTemplateExcel_New(int stageId, int formId, int yearsCount)
        {
            return await _tenderService.GenerateQuantityTableTemplateExcel_New(stageId, formId, yearsCount);
        }
        [HttpGet]
        [Route("ExportTenderTableQuantityItems_New/{tableId}/{isNewChange}")]
        public async Task<List<TenderQuantityItemDTO>> ExportTenderTableQuantityItems_New(int tableId, bool isNewChange)
        {
            return await _tenderService.ExportTenderTableQuantityItems(tableId, isNewChange);
        }
        [HttpGet]
        [Route("GetAllMandatoryListProductsForExport")]
        public async Task<List<MandatoryListForExportModel>> GetAllMandatoryListProductsForExport()
        {
            var result = await _tenderService.GetAllMandatoryListForExport();
            return result;
        }

        [HttpGet]
        [Route("GetTenderDetailsForAppliedSuppliersReport/{tenderIdString}")]
        public async Task<TenderDetailsForAppliedSuppliersReportModel> GetTenderDetailsForAppliedSuppliersReport(string tenderIdString)
        {
            int tenderId = Util.Decrypt(tenderIdString);
            var result = await _tenderService.GetTenderDetailsForAppliedSuppliersReport(tenderId);
            return result;
        }  
        
        [HttpGet]
        [Route("CheckIfActivityCabHasTawreed")]
        public async Task<bool> CheckIfActivityCanHasTawreed(ActivityVersionModel activityVersionModel)
        {
            var result = await _tenderService.CheckIfActivityCanHasTawreed(activityVersionModel);
            return result;
        }

    }
}
