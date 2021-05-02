using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.Negotiations;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Negotiation;
using MOF.Etimad.Monafasat.ViewModel.QuantityTableTemplates;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public class CommunicationAppService : ICommunicationAppService
    {

        public IQuantityTemplatesProxy _quantityTemplatesProxy { get; }
        private readonly INegotiationQueries _negotiationQueries;
        private readonly INegotiationCommands _negotaitionCommands;
        private readonly IOfferQueries _offerQueries;
        private readonly IOfferCommands _offerCommands;
        private readonly ICommunicationQueries _communicationQueries;
        private readonly ICommunicationCommands _communicationCommands;
        private readonly IIDMQueries _iDMQueries;
        private readonly ITenderQueries _tenderQueries;
        private readonly ITenderAppService _tenderAppService;
        private readonly INotificationAppService _notificationAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenderCommands _tenderCommands;
        private readonly ITenderDomainService _tenderDomainService;
        private readonly IIDMAppService _idmAppService;
        private readonly IMapper _mapper;
        private readonly IGenericCommandRepository _genericCommandRepository;
        private readonly ICommunicationDomainService _communicationDomainService;
        private readonly RootConfigurations _rootConfiguration;

        public CommunicationAppService(INegotiationCommands negotaitionCommands, INegotiationQueries negotiationQueries, ICommunicationQueries communicationQueries, ICommunicationCommands communicationCommands, ICommunicationDomainService communicationDomainService, IMapper mapper, IGenericCommandRepository genericCommandRepository,
                                ITenderCommands tenderCommands, ITenderQueries tenderQueries, ITenderAppService tenderAppService, IHttpContextAccessor httpContextAccessor, IIDMQueries iDMQueries, IOfferQueries offerQueries, IOfferCommands offerCommands, ITenderDomainService tenderDomainService, INotificationAppService notificationAppService
            , IIDMAppService iDMAppService, IOptionsSnapshot<RootConfigurations> rootConfiguration, IQuantityTemplatesProxy quantityTemplatesProxy)
        {
            _negotaitionCommands = negotaitionCommands;
            _negotiationQueries = negotiationQueries;
            _communicationQueries = communicationQueries;
            _communicationCommands = communicationCommands;
            _communicationDomainService = communicationDomainService;
            _notificationAppService = notificationAppService;
            _tenderCommands = tenderCommands;
            _tenderQueries = tenderQueries;
            _tenderDomainService = tenderDomainService;
            _tenderAppService = tenderAppService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _genericCommandRepository = genericCommandRepository;
            _iDMQueries = iDMQueries;
            this._offerQueries = offerQueries;
            _offerCommands = offerCommands;
            _idmAppService = iDMAppService;
            _rootConfiguration = rootConfiguration.Value;

            _quantityTemplatesProxy = quantityTemplatesProxy;

        }

        #region Tender Communication
        public async Task<TenderPlaintDatailsModel> FindTenderForPlaintRequestById(int tenderId, string selectedCR)
        {
            return await _communicationQueries.FindTenderForPlaintRequestById(tenderId, selectedCR);
        }
        public async Task<TenderPlaintDatailsModel> FindTenderForEscalationRequestById(int tenderId, string selectedCR)
        {
            TenderPlaintDatailsModel obj = await _communicationQueries.FindTenderForEscalationRequestById(tenderId, selectedCR);
            List<TenderHistory> lst = obj.Histories as List<TenderHistory>;
            obj.AwardingDate = lst.Where(t => t.StatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed).OrderByDescending(a => a.TenderHistoryId).FirstOrDefault().CreatedAt.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture);
            obj.OffersOpeningDate = lst.Where(t => t.StatusId == (int)Enums.TenderStatus.OffersOppenedConfirmed).OrderByDescending(a => a.TenderHistoryId).FirstOrDefault() != null ?
                lst.Where(t => t.StatusId == (int)Enums.TenderStatus.OffersOppenedConfirmed).OrderByDescending(a => a.TenderHistoryId).FirstOrDefault().CreatedAt.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture) : "";
            obj.OffersCheckingDate = lst.Where(t => t.StatusId == (int)Enums.TenderStatus.OffersCheckedConfirmed).OrderByDescending(a => a.TenderHistoryId).FirstOrDefault() != null ?
                lst.Where(t => t.StatusId == (int)Enums.TenderStatus.OffersCheckedConfirmed).OrderByDescending(a => a.TenderHistoryId).FirstOrDefault().CreatedAt.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture) : "";
            return obj;
        }
        public async Task<QueryResult<EscalatedTendersModel>> GetEscalatedTenders(EscalationSearchCriteria searchCriteria)
        {
            return await _communicationQueries.GetEscalatedTenders(searchCriteria);
        }
        public async Task<TenderPlaintCommunicationRequestModel> FindSupplierPlaintRequestByTenderIdAndCR(int tenderId, string selectedCR)
        {
            TenderPlaintCommunicationRequestModel Plaint = await _communicationQueries.FindSupplierPlaintRequestByTenderIdAndCR(tenderId, selectedCR);
            if (Plaint == null || Plaint.PlaintRequestId != 0)
            {
                throw new UnHandledAccessException(Resources.TenderResources.ErrorMessages.YouHaveAnotherPlaint);
            }
            TenderHistory lst = Plaint.tenderHistory as TenderHistory;
            if (Plaint == null || lst == null)
            {
                throw new UnHandledAccessException();
            }
            if (Plaint.AwardingStoppingPeriod == null)
            {
                throw new UnHandledAccessException();
            }

            Plaint.TenderAwardingDate = (Plaint.tenderHistory as TenderHistory).CreatedAt;
            await _communicationDomainService.IsValidToGetSupplierPlain(Plaint);
            return Plaint;
        }

        public async Task<TenderPlaintCommunicationRequestModel> FindSupplierPlaintDetailsByPlaintIdAndCR(int agencyRequestId, string selectedCR)
        {
            TenderPlaintCommunicationRequestModel plaint = await _communicationQueries.FindSupplierPlaintDetailsByPlaintIdAndCR(agencyRequestId, selectedCR);
            Check.ArgumentNotNull(nameof(plaint), plaint);

            if (plaint.PlaintRequestId == 0)
            {
                throw new UnHandledAccessException(Resources.TenderResources.ErrorMessages.UnexpectedError);
            }

            int plaintReviewingPeriod = int.Parse(_rootConfiguration.PlaintSettingConfiguration.PlaintReviewingPeriod);
            int plaintPeriod = int.Parse(_rootConfiguration.PlaintSettingConfiguration.PlaintPeriod);
            plaint.TenderAwardingDate = (plaint.tenderHistory as TenderHistory).CreatedAt;
            if ((!plaint.IsEscalation && plaint.PlaintStatusId == (int)Enums.AgencyPlaintStatus.Rejected && plaint.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Approved
                && DateTime.Now.Date >= plaint.LastUpdateDate.Value.Date && DateTime.Now.Date <= plaint.LastUpdateDate.Value.AddDays(plaintPeriod).Date)
                ||
                (!plaint.IsEscalation && plaint.StatusId != (int)Enums.AgencyCommunicationRequestStatus.Approved
                 && DateTime.Now.Date >= plaint.TenderAwardingDate.Value.AddDays(plaint.AwardingStoppingPeriod.Value + plaintReviewingPeriod).Date
                 && DateTime.Now.Date <= plaint.TenderAwardingDate.Value.AddDays(plaint.AwardingStoppingPeriod.Value + plaintPeriod + plaintReviewingPeriod).Date)
                 )
            {
                plaint.CanEscalate = true;
            }

            return plaint;
        }

        public async Task<PlaintRequest> EscalatePlaint(string plaintId, string attachmentId, string attachmentName, string selectedCR)
        {
            Check.ArgumentNotNullOrEmpty(nameof(plaintId), plaintId);
            PlaintRequest plaint = await _communicationQueries.FindPlaintRequestToEscalateById(Util.Decrypt(plaintId), selectedCR);
            Check.ArgumentNotNullOrEmpty(nameof(plaintId), plaintId);
            plaint.EscalatePlaintRequest(attachmentId, attachmentName);
            PlaintRequest obj = await _communicationCommands.UpdatePlaintRequestAsync(plaint);
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[]{ "", plaint.AgencyCommunicationRequest.Tender.ReferenceNumber
                },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { plaint.AgencyCommunicationRequest.Tender.ReferenceNumber },
                SMSArgs = new object[] { plaint.AgencyCommunicationRequest.Tender.ReferenceNumber }
            };
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(NotificationArguments, $"CommunicationRequest/checkEscalationrequests/{Util.Encrypt(plaint.AgencyCommunicationRequest.AgencyRequestId)}", NotificationEntityType.Tender, plaint.AgencyCommunicationRequest.Tender.TenderId.ToString(), plaint.AgencyCommunicationRequest.Tender.BranchId);
            MainNotificationTemplateModel approveTender2 = new MainNotificationTemplateModel(NotificationArguments, "", NotificationEntityType.Tender, plaint.AgencyCommunicationRequest.Tender.TenderId.ToString(), plaint.AgencyCommunicationRequest.Tender.BranchId);

            if (plaint.AgencyCommunicationRequest.Tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseSecretary.AgencyCommunicationRequest.PlaintEsclationCreated, plaint.AgencyCommunicationRequest.Tender.OffersCheckingCommitteeId, approveTender);
            else
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.AgencyCommunicationRequest.PlaintEsclationCreated, plaint.AgencyCommunicationRequest.Tender.OffersCheckingCommitteeId, approveTender2);

            await _notificationAppService.SendNotificationForUsersByRoleName(NotificationOperations.GrievanceSecretary.AgencyCommunicationRequest.PlaintEsclationCreated, RoleNames.SecretaryGrievanceCommittee, approveTender);

            return obj;
        }

        public async Task<PlaintRequestModel> FindPlaintRequestModelById(int plaintId, string agencyCode, int committeeId)
        {
            int plaintReviewingPeriod = int.Parse(_rootConfiguration.PlaintSettingConfiguration.PlaintReviewingPeriod);
            PlaintRequestModel plaint = await _communicationQueries.FindPlaintRequestModelById(plaintId, agencyCode, committeeId);
            Check.ArgumentNotNull(nameof(plaint), plaint);
            plaint.TenderAwardingDate = (plaint.tenderHistory as TenderHistory).CreatedAt;
            if ((plaint.AwardingStoppingPeriod.HasValue && plaint.TenderAwardingDate.AddDays(plaint.AwardingStoppingPeriod.Value) >= DateTime.Now) ||
                (plaint.AwardingStoppingPeriod.HasValue && plaint.TenderAwardingDate.AddDays(plaint.AwardingStoppingPeriod.Value).AddDays(plaintReviewingPeriod) < DateTime.Now)
                )
            {
                plaint.CanTakeAction = false;
            }
            await _communicationDomainService.IsValidToCheckPlain(plaint);
            return plaint;
        }

        public async Task<PlaintRequestModel> FindEscalationRequestModelById(int plaintId)
        {
            int plaintReviewingPeriod = int.Parse(_rootConfiguration.PlaintSettingConfiguration.PlaintReviewingPeriod);
            int plaintPeriod = int.Parse(_rootConfiguration.PlaintSettingConfiguration.PlaintPeriod);

            PlaintRequestModel plaint = await _communicationQueries.FindEscalationRequestModelById(plaintId);
            Check.ArgumentNotNull(nameof(plaint), plaint);
            var TenderAwardingDate = (plaint.tenderHistory as TenderHistory).CreatedAt;
            if (plaint.ApprovalDate.HasValue)
            {
                if ((plaint.ApprovalDate.Value.Date.AddDays(plaintPeriod) <= DateTime.Now.Date)
                    && (plaint.ApprovalDate.Value.Date.AddDays(plaintPeriod + plaintReviewingPeriod) > DateTime.Now.Date))
                {
                    plaint.CanTakeAction = false;
                }
            }
            else
            {
                if (plaint.AwardingStoppingPeriod.HasValue && (TenderAwardingDate.AddDays(plaint.AwardingStoppingPeriod.Value + plaintPeriod + plaintReviewingPeriod).Date <= DateTime.Now.Date)
                    && (plaint.AwardingStoppingPeriod.HasValue && (TenderAwardingDate.AddDays(plaint.AwardingStoppingPeriod.Value + plaintPeriod + plaintReviewingPeriod + plaintReviewingPeriod).Date) > DateTime.Now.Date))
                {

                    plaint.CanTakeAction = false;
                }
            }
            return plaint;
        }
        public async Task<TenderEscalatedPLaintModel> FindAgencyCommunicationEscalatedPalintsByTenderId(int requestId)
        {
            int plaintReviewingPeriod = int.Parse(_rootConfiguration.PlaintSettingConfiguration.PlaintReviewingPeriod);
            int plaintPeriod = int.Parse(_rootConfiguration.PlaintSettingConfiguration.PlaintPeriod);

            TenderEscalatedPLaintModel tender = await _communicationQueries.FindAgencyCommunicationEscalatedPalintsByTenderId(requestId);
            Check.ArgumentNotNull(nameof(tender), tender);
            if (!tender.HasEscalatedPlaints)
                throw new UnHandledAccessException();
            var TenderAwardingDate = (tender.tenderHistory as TenderHistory).CreatedAt;
            if (tender.ApprovalDate.HasValue)
            {
                if ((tender.ApprovalDate.Value.Date.AddDays(plaintPeriod) <= DateTime.Now.Date)
                    && (tender.ApprovalDate.Value.Date.AddDays(plaintPeriod + plaintReviewingPeriod) > DateTime.Now.Date))
                {
                    tender.CanManagerTakeAction = false;
                    tender.CanSecretaryTakeAction = false;
                }
            }
            else
            {
                if ((tender.AwardingStoppingPeriod.HasValue && TenderAwardingDate.AddDays(tender.AwardingStoppingPeriod.Value + plaintPeriod + plaintReviewingPeriod).Date <= DateTime.Now.Date)
                    && (tender.AwardingStoppingPeriod.HasValue && (TenderAwardingDate.AddDays(tender.AwardingStoppingPeriod.Value + plaintPeriod + plaintReviewingPeriod + plaintReviewingPeriod).Date) > DateTime.Now.Date))
                {

                    tender.CanManagerTakeAction = false;
                    tender.CanSecretaryTakeAction = false;
                }
            }
            return tender;
        }

        public async Task<QueryResult<TenderPlaintCheckingModel>> FindTenderEscalatedPlaintRequestsByTenderId(PlaintSearchCriteria plaintSearchCriteria)
        {
            QueryResult<TenderPlaintCheckingModel> tender = await _communicationQueries.FindEscalatedTenderPlaintRequestsByTenderId(plaintSearchCriteria);

            Check.ArgumentNotNull(nameof(tender), tender);
            return tender;
        }
        public async Task<QueryResult<TenderPlaintCheckingModel>> FindTenderPlaintRequestsByTenderId(PlaintSearchCriteria plaintSearchCriteria)
        {
            QueryResult<TenderPlaintCheckingModel> tender = await _communicationQueries.FindTenderPlaintRequestsByTenderId(plaintSearchCriteria);
            Check.ArgumentNotNull(nameof(tender), tender);
            return tender;
        }


        public async Task<List<PendingAgencyRequestAlarm>> GetPendingRequests(string CR)
        {
            List<PendingAgencyRequestAlarm> negotiations = await _negotiationQueries.GetPendingNegotiations(CR);
            return negotiations.OrderByDescending(a => a.RequestCreatedTime).ToList();
        }

        #endregion

        #region Plaint Requests       

        public async Task<TenderPLaintCommunicationModel> FindAgencyCommunicationPalintRequestsByTenderId(int agencyRequestId, string agencyId, int committeeId)
        {
            int plaintReviewingPeriod = int.Parse(_rootConfiguration.PlaintSettingConfiguration.PlaintReviewingPeriod/*_configuration.GetSection("PlaintSetting:PlaintReviewingPeriod").Value*/);
            TenderPLaintCommunicationModel tender = await _communicationQueries.FindAgencyCommunicationPalintRequestsByTenderId(agencyRequestId, agencyId, committeeId);
            Check.ArgumentNotNull(nameof(tender), tender);
            var TenderAwardingDate = (tender.tenderHistory as TenderHistory).CreatedAt;
            if ((tender.AwardingStoppingPeriod.HasValue && TenderAwardingDate.AddDays(tender.AwardingStoppingPeriod.Value) >= DateTime.Now) ||
                (tender.AwardingStoppingPeriod.HasValue && TenderAwardingDate.AddDays(tender.AwardingStoppingPeriod.Value).AddDays(plaintReviewingPeriod) < DateTime.Now)
                )
            {
                tender.CanManagerTakeAction = false;
                tender.CanSecretaryTakeAction = false;
            }

            return tender;
        }

        public async Task<TenderPLaintCommunicationModel> FindAgencyCommunicationPalintRequestsByRequestId(int communicationRequestId, string agencyId, int committeeID)
        {
            TenderPLaintCommunicationModel tender = await _communicationQueries.FindAgencyCommunicationPalintRequestsByRequestId(communicationRequestId, agencyId, committeeID);
            Check.ArgumentNotNull(nameof(tender), tender);
            var TenderAwardingDate = (tender.tenderHistory as TenderHistory).CreatedAt;
            if (tender.AwardingStoppingPeriod.HasValue && TenderAwardingDate.AddDays(tender.AwardingStoppingPeriod.Value) >= DateTime.Now)
            {
                tender.CanManagerTakeAction = false;
                tender.CanSecretaryTakeAction = false;
            }
            return tender;
        }

        public async Task<TenderPLaintCommunicationModel> FindAgencyCommunicationPalintRequestsById(int Id, string agencyId, int committeeId, bool isEscalation)
        {
            TenderPLaintCommunicationModel tender = await _communicationQueries.FindAgencyCommunicationPalintRequestsById(Id, agencyId, committeeId, isEscalation);
            Check.ArgumentNotNull(nameof(tender), tender);
            var TenderAwardingDate = (tender.tenderHistory as TenderHistory).CreatedAt;
            if (tender.AwardingStoppingPeriod.HasValue && TenderAwardingDate.AddDays(tender.AwardingStoppingPeriod.Value) >= DateTime.Now)
            {
                tender.CanManagerTakeAction = false;
                tender.CanSecretaryTakeAction = false;
            }
            return tender;
        }

        public async Task<PlaintNotesModel> SavePlaintNotes(PlaintNotesModel model, string agencyId, int selectedCommittee)
        {
            PlaintRequest obj = await FindPlaintById(Util.Decrypt(model.plaintId), agencyId, selectedCommittee);
            obj.UpdateNotes(model.Notes);
            await _communicationCommands.UpdatePlaintRequestAsync(obj);
            return model;
        }

        /// <summary>
        /// Create Plaint Request By Supplier
        /// </summary>
        /// <param name="tenderPlaintModel"></param>
        /// <param name="selectedCR"></param>
        /// <returns></returns>
        public async Task<TenderPlaintCommunicationRequestModel> CreateCommunicationRequest(TenderPlaintCommunicationRequestModel tenderPlaintModel, string selectedCR)
        {
            tenderPlaintModel.TenderId = Util.Decrypt(tenderPlaintModel.EncryptedTenderId);
            Tender tender = await _communicationQueries.FindTenderWithPlaintRequestByTenderId(Util.Decrypt(tenderPlaintModel.EncryptedTenderId), selectedCR);
            _communicationDomainService.IsValidToCreatePlain(tender, selectedCR);
            tenderPlaintModel.TenderId = Util.Decrypt(tenderPlaintModel.EncryptedTenderId);
            tender.AddAgencyPlaintRequest(tenderPlaintModel);
            await _tenderCommands.UpdateAsync(tender);

            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[]{ "", tender.ReferenceNumber
                },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(NotificationArguments, $"CommunicationRequest/CheckPlaintRequests/{Util.Encrypt(tender.AgencyCommunicationRequests.Where(r => r.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint).OrderByDescending(a => a.AgencyRequestId).FirstOrDefault().AgencyRequestId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), null, tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase ? tender.DirectPurchaseCommitteeId : tender.OffersCheckingCommitteeId);
            if (tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseSecretary.AgencyCommunicationRequest.PlaintRequestCreated, tender.DirectPurchaseCommitteeId, approveTender);
            else
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.AgencyCommunicationRequest.PlaintRequestCreated, tender.OffersCheckingCommitteeId, approveTender);

            return tenderPlaintModel;
        }

        public async Task<AgencyCommunicationRequest> RejectPlaint(string CommunicationRequestId, string rejectionReason, string agencyId, int committeeId, bool isEscalation)
        {
            TenderPLaintCommunicationModel obj = await FindAgencyCommunicationPalintRequestsByRequestId(Util.Decrypt(CommunicationRequestId), agencyId, committeeId);
            if (obj.PlaintsNoNotes > 0)
            {
                throw new BusinessRuleException(Resources.CommunicationRequest.Messages.EnterRejectionNotes);
            }
            Check.ArgumentNotNull(nameof(obj), obj);
            await _communicationDomainService.IsValidToSendPlaintDecission(obj);
            AgencyCommunicationRequest request = await _communicationQueries.FindCommunicationRequestByIdForPlaint(Util.Decrypt(CommunicationRequestId));
            request.UpdateAgencyCommunicationPlaintRequestStatus((int)Enums.AgencyCommunicationRequestStatus.Pending, (int)Enums.AgencyPlaintStatus.Rejected, null, "", rejectionReason);
            await _communicationCommands.UpdateAsync(request);
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[]{ "", request.Tender.ReferenceNumber
                },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { request.Tender.ReferenceNumber },
                SMSArgs = new object[] { request.Tender.ReferenceNumber }
            };
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(NotificationArguments, $"CommunicationRequest/CheckPlaintRequests/{Util.Encrypt(request.AgencyRequestId)}", NotificationEntityType.Tender, request.Tender.TenderId.ToString(), request.Tender.BranchId);
            if (request.Tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseManager.AgencyCommunicationRequest.PlaintSentToApproval, request.Tender.DirectPurchaseCommitteeId, approveTender);
            else
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckManager.AgencyCommunicationRequest.PlaintSentToApproval, request.Tender.OffersCheckingCommitteeId, approveTender);
            return request;
        }

        /// <summary>
        /// Accept Plaint Request by Check/Direct Purchase Secretary
        /// </summary>
        /// <param name="CommunicationRequestId"></param>
        /// <param name="procedureId"></param>
        /// <param name="details"></param>
        /// <param name="agencyId"></param>
        /// <param name="isEscalation"></param>
        /// <param name="committeeId"></param>
        /// <returns></returns>
        public async Task<AgencyCommunicationRequest> AcceptPlaint(string plaintId, int procedureId, string details, string agencyId, bool isEscalation, int committeeId)
        {
            TenderPLaintCommunicationModel obj = await FindAgencyCommunicationPalintRequestsByRequestId(Util.Decrypt(plaintId), agencyId, committeeId);
            if (procedureId == (int)Enums.TenderPlaintRequestProcedure.Other)
            {
                if (string.IsNullOrEmpty(details))
                {
                    throw new BusinessRuleException(Resources.CommunicationRequest.Messages.EnterDetails);
                }
            }
            else
            {
                details = null;
            }
            Check.ArgumentNotNull(nameof(obj), obj);
            await _communicationDomainService.IsValidToSendPlaintDecission(obj);
            AgencyCommunicationRequest request = await _communicationQueries.FindCommunicationRequestByIdForPlaint(Util.Decrypt(plaintId));
            request.UpdateAgencyCommunicationPlaintRequestStatus((int)Enums.AgencyCommunicationRequestStatus.Pending, (int)Enums.AgencyPlaintStatus.Accepted, procedureId, details);
            await _communicationCommands.UpdateAsync(request);
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[]{ "", request.Tender.ReferenceNumber
                },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { request.Tender.ReferenceNumber },
                SMSArgs = new object[] { request.Tender.ReferenceNumber }
            };
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(NotificationArguments, $"CommunicationRequest/CheckPlaintRequests/{Util.Encrypt(request.AgencyRequestId)}", NotificationEntityType.Tender, request.Tender.TenderId.ToString(), request.Tender.BranchId);
            if (request.Tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseManager.AgencyCommunicationRequest.PlaintSentToApproval, request.Tender.DirectPurchaseCommitteeId, approveTender);
            else
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckManager.AgencyCommunicationRequest.PlaintSentToApproval, request.Tender.OffersCheckingCommitteeId, approveTender);
            return request;
        }

        public async Task<AgencyCommunicationRequest> RejectPlaintCommunicationRequest(int requestId, string rejectionReason, string agencyId, int committeeId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(requestId), requestId.ToString());
            AgencyCommunicationRequest request = await _communicationQueries.FindCommunicationRequestByIdForRejectPlaint(requestId);
            TenderPLaintCommunicationModel obj = await FindAgencyCommunicationPalintRequestsById(requestId, agencyId, committeeId, false);
            await _communicationDomainService.IsValidToManagerToChecklPlaint(obj);
            request.UpdatePlaintAgencyCommunicationRequest((int)Enums.AgencyCommunicationRequestStatus.Rejected, rejectionReason);
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[]{ "", request.Tender.ReferenceNumber
                },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { request.Tender.ReferenceNumber },
                SMSArgs = new object[] { request.Tender.ReferenceNumber }
            };
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(NotificationArguments, $"CommunicationRequest/CheckPlaintRequests/{Util.Encrypt(request.AgencyRequestId)}", NotificationEntityType.Tender, request.Tender.TenderId.ToString(), request.Tender.BranchId);
            if (request.Tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseSecretary.AgencyCommunicationRequest.PurchasePlaintRejected, request.Tender.DirectPurchaseCommitteeId, approveTender);
            else
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.AgencyCommunicationRequest.PlaintRejected, request.Tender.OffersCheckingCommitteeId, approveTender);
            return await _communicationCommands.UpdateAsync(request);
        }

        /// <summary>
        /// Approve Plaint Request by Check/Direct Purchase Manager
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="verficationCode"></param>
        /// <param name="agencyId"></param>
        /// <param name="committeeId"></param>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public async Task ApprovePlaintCommunicationRequest(int requestId, string verficationCode, string agencyId, int committeeId, int typeId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(requestId), requestId.ToString());
            await CheckForVerificationCode(requestId, verficationCode, typeId);
            TenderPLaintCommunicationModel plaint = await FindAgencyCommunicationPalintRequestsById(requestId, agencyId, committeeId, false);
            await _communicationDomainService.IsValidToManagerToChecklPlaint(plaint);

            AgencyCommunicationRequest request = await _communicationQueries.FindCommunicationRequestByIdForApprovePlaint(requestId);
            List<AgencyCommunicationRequest> communicationRequests = await _communicationQueries.GetCommunicationRequestWithNegotiationAndExtendOffersValdityByTenderId(request.TenderId);
            List<Tender> postqualifications = await _communicationDomainService.GetPostqualificationOnTenderForCancel(request.TenderId);
            List<NegotiationFirstStageSupplier> negotiationFirstStageSuppliers = await _offerQueries.GetNegotiationFirstStageSuppliersByTenderId(request.TenderId);

            request.UpdatePlaintAgencyCommunicationRequest((int)Enums.AgencyCommunicationRequestStatus.Approved, "");
            await DeActiveExtendOfferValidtySupplier(communicationRequests);
            await ApprovePlaintAndUpdateTenderToAwardingOrCheckingAsync(request, communicationRequests, plaint, postqualifications, negotiationFirstStageSuppliers);
            await SendNotificationAfterApprovePlaintRequest(request);
        }

        private async Task ApprovePlaintAndUpdateTenderToAwardingOrCheckingAsync(AgencyCommunicationRequest request, List<AgencyCommunicationRequest> communicationRequests, TenderPLaintCommunicationModel plaint, List<Tender> postqualifications, List<NegotiationFirstStageSupplier> negotiationFirstStageSuppliers)
        {
            if (request.TenderPlaintRequestProcedureId == (int)Enums.TenderPlaintRequestProcedure.ReOpenTenderChecking)
            {
                foreach (var offer in request.Tender.Offers)
                {
                    offer.ResetOfferToCheck();
                    if (negotiationFirstStageSuppliers.Any(a => a.OfferId == offer.OfferId))
                    {
                        offer.SetFinalPrice(negotiationFirstStageSuppliers.FirstOrDefault(a => a.OfferId == offer.OfferId).offerOriginalAmount);
                    }
                    _genericCommandRepository.Update(offer);
                }
                request.Tender.UpdateTenderStatus(Enums.TenderStatus.BackForCheckingFromPlaint);
            }
            else if (plaint.procedureId == (int)Enums.TenderPlaintRequestProcedure.ReOpenTenderAwarding)
            {
                foreach (var offer in request.Tender.Offers.Where(o => o.OfferStatusId == (int)Enums.OfferStatus.Excluded))
                {
                    offer.ResetOfferToAwarding();
                    _genericCommandRepository.Update(offer);
                }
                request.Tender.UpdateTenderStatus(Enums.TenderStatus.BackForAwardingFromPlaint, Resources.TenderResources.Messages.BackForAwardingFromPlaintReason, _httpContextAccessor.HttpContext.User.UserId(), Enums.TenderActions.BackForAwardingFromPlaint);
            }
            foreach (var postqualification in postqualifications)
            {
                postqualification.ResetPostQualifications();
                _genericCommandRepository.Update(postqualification);
            }

            ResetNegotiationsAndExtendOfferValidityRequests(communicationRequests);
            DeleteTenderAwardingReport(request.Tender);
            _genericCommandRepository.Update(request.Tender);
            await _genericCommandRepository.SaveAsync();
        }

        private async Task SendNotificationAfterApprovePlaintRequest(AgencyCommunicationRequest request)
        {
            var plaintRequests = await _communicationQueries.GetAllPlaintRequestsByRequestId(request.AgencyRequestId);
            var NotifySuppliers = plaintRequests.Select(a => a.Offer.CommericalRegisterNo).ToList();

            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { request.Tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { request.Tender.ReferenceNumber },
                SMSArgs = new object[] { request.Tender.ReferenceNumber }
            };
            MainNotificationTemplateModel approveTenderSupplier = new MainNotificationTemplateModel(NotificationArguments, $"CommunicationRequest/VendorPlaintRequestData/{Util.Encrypt(request.AgencyRequestId)}", NotificationEntityType.Tender, request.Tender.TenderId.ToString(), request.Tender.BranchId);
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(NotificationArguments, $"CommunicationRequest/CheckPlaintRequests/{Util.Encrypt(request.AgencyRequestId)}", NotificationEntityType.Tender, request.Tender.TenderId.ToString(), request.Tender.BranchId);
            if (request.Tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseSecretary.AgencyCommunicationRequest.PurchasePlaintApproved, request.Tender.DirectPurchaseCommitteeId, approveTender);
            else
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.AgencyCommunicationRequest.PlaintApproved, request.Tender.OffersCheckingCommitteeId, approveTender);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.AgencyCommunicationRequest.PlaintApprovedToSupplier, NotifySuppliers, approveTenderSupplier);
        }

        public async Task<PlaintRequest> FindPlaintById(int plaintId, string agencyId, int committeeId)
        {
            return await _communicationQueries.FindPlaintById(plaintId, agencyId, committeeId);
        }



        public async Task<EscalationRequest> FindEscalationByPlaintId(int plaintId, string agencyId)
        {
            return await _communicationQueries.FindEscalationByPlaintId(plaintId, agencyId);
        }
        public async Task<PlaintRequestModel> SaveEscalationNotes(PlaintRequestModel model, string agencyId)
        {
            EscalationRequest obj = await FindEscalationByPlaintId(Util.Decrypt(model.EncryptedPlaintId), agencyId);
            obj.UpdateNotes(model.EscalationNotes);
            await _communicationCommands.UpdateEscalationRequestAsync(obj);
            return model;
        }

        /// <summary>
        ///  Accept esclation request by plaint secretary and send to plaint mgr
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="procedureId"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        public async Task<AgencyCommunicationRequest> AcceptEscalationCommunicationRequest(int requestId, int procedureId, string details)
        {
            Check.ArgumentNotNullOrEmpty(nameof(requestId), requestId.ToString());
            if (procedureId == (int)Enums.TenderPlaintRequestProcedure.Other)
            {
                if (string.IsNullOrEmpty(details))
                {
                    throw new BusinessRuleException(Resources.CommunicationRequest.Messages.EnterDetails);
                }
            }
            else
            {
                details = null;
            }
            AgencyCommunicationRequest request = await _communicationQueries.FindEscalationRequestById(requestId);
            request.UpdateAgencyCommunicationEscalationRequestStatus((int)Enums.AgencyCommunicationRequestStatus.Pending, (int)Enums.AgencyPlaintStatus.Accepted, procedureId, details);


            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[]{ "", request.Tender.ReferenceNumber
                },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { request.Tender.ReferenceNumber },
                SMSArgs = new object[] { request.Tender.ReferenceNumber }
            };
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(NotificationArguments, $"CommunicationRequest/checkEscalationrequests/{Util.Encrypt(request.AgencyRequestId)}", NotificationEntityType.Tender, request.Tender.TenderId.ToString(), request.Tender.BranchId);
            await _notificationAppService.SendNotificationForUsersByRoleName(NotificationOperations.GrievanceManager.AgencyCommunicationRequest.PlaintEsclationSentForApproval, RoleNames.ManagerGrievanceCommittee, approveTender);

            return await _communicationCommands.UpdateAsync(request);
        }

        /// <summary>
        /// Reject esclation request by plaint secretary and send to plaint mgr
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="rejectionReason"></param>
        /// <returns></returns>
        public async Task<AgencyCommunicationRequest> RejectEscalationCommunicationRequest(int requestId, string rejectionReason)
        {
            Check.ArgumentNotNullOrEmpty(nameof(requestId), requestId.ToString());
            if (string.IsNullOrEmpty(rejectionReason))
            {
                throw new BusinessRuleException(Resources.CommunicationRequest.Messages.EnterDetails);
            }

            AgencyCommunicationRequest request = await _communicationQueries.FindEscalationRequestById(requestId);
            int PlaintsNotes = request.PlaintRequests.Where(p => p.EscalationRequest != null && string.IsNullOrEmpty(p.EscalationRequest.EscalationNotes)).Count();
            if (PlaintsNotes > 0)
            {
                throw new BusinessRuleException(Resources.CommunicationRequest.Messages.EnterRejectionNotes);
            }
            request.UpdateAgencyCommunicationEscalationRequestStatus((int)Enums.AgencyCommunicationRequestStatus.Pending, (int)Enums.AgencyPlaintStatus.Rejected, null, "", rejectionReason);
            await _communicationCommands.UpdateAsync(request);
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[]{ "", request.Tender.ReferenceNumber
                },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { request.Tender.ReferenceNumber },
                SMSArgs = new object[] { request.Tender.ReferenceNumber }
            };
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(NotificationArguments, $"CommunicationRequest/checkEscalationrequests/{Util.Encrypt(request.AgencyRequestId)}", NotificationEntityType.Tender, request.Tender.TenderId.ToString(), request.Tender.BranchId);
            await _notificationAppService.SendNotificationForUsersByRoleName(NotificationOperations.GrievanceManager.AgencyCommunicationRequest.PlaintEsclationSentForApproval, RoleNames.ManagerGrievanceCommittee, approveTender);
            return request;
        }

        /// <summary>
        /// Approve esclation request by plaint manager
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="verficationCode"></param>
        /// <param name="agencyId"></param>
        /// <param name="userId"></param>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public async Task ApproveEscalationCommunicationRequest(int requestId, string verficationCode, string agencyId, int userId, int typeId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(requestId), requestId.ToString());
            await CheckForVerificationCode(requestId, verficationCode, typeId);
            TenderEscalatedPLaintModel esclation = await FindAgencyCommunicationEscalatedPalintsByTenderId(requestId);
            await _communicationDomainService.IsValidToManagerToChecklEscalation(esclation);

            AgencyCommunicationRequest request = await _communicationQueries.GetAgencyCommunicationRequestForApproval(esclation.CommunicationRequestId);
            List<AgencyCommunicationRequest> communicationRequests = await _communicationQueries.GetCommunicationRequestWithNegotiationAndExtendOffersValdityByTenderId(request.TenderId);

            List<Tender> postqualifications = await _communicationDomainService.GetPostqualificationOnTenderForCancel(request.TenderId);
            List<NegotiationFirstStageSupplier> negotiationFirstStageSuppliers = await _offerQueries.GetNegotiationFirstStageSuppliersByTenderId(request.TenderId);

            request.UpdateEscalationRequest((int)Enums.AgencyCommunicationRequestStatus.ApprovedByPlaintManager, "");
            await DeActiveExtendOfferValidtySupplier(communicationRequests);
            await ApproveEsclationAndUpdateTenderToAwardingOrCheckingAsync(request, communicationRequests, esclation, postqualifications, negotiationFirstStageSuppliers);
            await SendNotificationAfterApproveEsclationRequest(request);
        }
        private async Task DeActiveExtendOfferValidtySupplier(List<AgencyCommunicationRequest> communicationRequests)
        {
            foreach (var communicationRequest in communicationRequests)
            {
                if (communicationRequest.ExtendOffersValidity != null)
                {
                    var suppliers = await _communicationQueries.FindExtendOfferValidtySupplier(communicationRequest.ExtendOffersValidity.ExtendOffersValidityId);
                    foreach (var item in suppliers)
                    {
                        item.DeActive();
                        _genericCommandRepository.Update(item);
                    }
                }
            }
            await _genericCommandRepository.SaveAsync();
        }
        private async Task ApproveEsclationAndUpdateTenderToAwardingOrCheckingAsync(AgencyCommunicationRequest request, List<AgencyCommunicationRequest> communicationRequests, TenderEscalatedPLaintModel esclation, List<Tender> postqualifications, List<NegotiationFirstStageSupplier> negotiationFirstStageSuppliers)
        {
            if (request.TenderPlaintRequestProcedureId == (int)Enums.TenderPlaintRequestProcedure.ReOpenTenderChecking)
            {
                foreach (var offer in request.Tender.Offers)
                {
                    offer.ResetOfferToCheck();
                    if (negotiationFirstStageSuppliers.Any(a => a.OfferId == offer.OfferId))
                    {
                        offer.SetFinalPrice(negotiationFirstStageSuppliers.FirstOrDefault(a => a.OfferId == offer.OfferId).offerOriginalAmount);
                    }
                    _genericCommandRepository.Update(offer);
                }
                request.Tender.UpdateTenderStatus(Enums.TenderStatus.BackForCheckingFromPlaint);

            }
            else if (esclation.procedureId == (int)Enums.TenderPlaintRequestProcedure.ReOpenTenderAwarding)
            {
                foreach (var offer in request.Tender.Offers)
                {
                    offer.ResetOfferToAwarding();
                    _genericCommandRepository.Update(offer);
                }
                request.Tender.UpdateTenderStatus(Enums.TenderStatus.BackForAwardingFromPlaint, Resources.TenderResources.Messages.BackForAwardingFromPlaintReason, _httpContextAccessor.HttpContext.User.UserId(), Enums.TenderActions.BackForAwardingFromPlaint);
            }
            foreach (var postqualification in postqualifications)
            {
                postqualification.ResetPostQualifications();
                _genericCommandRepository.Update(postqualification);
            }
            ResetNegotiationsAndExtendOfferValidityRequests(communicationRequests);
            DeleteTenderAwardingReport(request.Tender);
            _genericCommandRepository.Update(request.Tender);
            await _genericCommandRepository.SaveAsync();
        }

        private void DeleteTenderAwardingReport(Tender tender)
        {
            tender.DeleteAwardingReport();
        }
        private void ResetNegotiationsAndExtendOfferValidityRequests(List<AgencyCommunicationRequest> communicationRequests)
        {
            foreach (var communicationRequest in communicationRequests)
            {
                if (communicationRequest.ExtendOffersValidity != null)
                {
                    communicationRequest.DeleteExtendOfferValidityRequests();
                }
                if (communicationRequest.Negotiations.Any())
                {
                    communicationRequest.DeleteNegotiationRequests();
                }
                _genericCommandRepository.Update(communicationRequest);
            }
        }
        private async Task SendNotificationAfterApproveEsclationRequest(AgencyCommunicationRequest request)
        {
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", request.EscalationAcceptanceStatus.Name, request.Tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { request.Tender.ReferenceNumber },
                SMSArgs = new object[] { request.Tender.ReferenceNumber }
            };

            var suppliers = request.PlaintRequests.Select(x => x.Offer.CommericalRegisterNo).ToList();

            MainNotificationTemplateModel approveTender1 = new MainNotificationTemplateModel(NotificationArguments, $"CommunicationRequest/checkEscalationrequests/{Util.Encrypt(request.AgencyRequestId)}", NotificationEntityType.Tender, request.Tender.TenderId.ToString(), request.Tender.BranchId);
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(NotificationArguments, $"Tender/DetailsForSupplier?STenderId={Util.Encrypt(request.Tender.TenderId)}", NotificationEntityType.Tender, request.Tender.TenderId.ToString(), request.Tender.BranchId);
            await _notificationAppService.SendNotificationForUsersByRoleName(NotificationOperations.GrievanceSecretary.AgencyCommunicationRequest.PlaintEsclationApproved, RoleNames.SecretaryGrievanceCommittee, approveTender1);
            if (request.EscalationAcceptanceStatusId == (int)Enums.AgencyPlaintStatus.Accepted)
                await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.AgencyCommunicationRequest.PlaintEsclationApprovedToSupplier, suppliers, approveTender);
            else if (request.EscalationAcceptanceStatusId == (int)Enums.AgencyPlaintStatus.Rejected)
                await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.AgencyCommunicationRequest.PlaintEsclationRejectedToSupplier, suppliers, approveTender);
        }

        /// <summary>
        ///  Reject esclation request by plaint manager
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="rejectReason"></param>
        /// <returns></returns>
        public async Task<AgencyCommunicationRequest> RejectEscalationCommunicationRequestApproval(int requestId, string rejectReason)
        {
            Check.ArgumentNotNullOrEmpty(nameof(requestId), requestId.ToString());
            TenderEscalatedPLaintModel obj = await FindAgencyCommunicationEscalatedPalintsByTenderId(requestId);
            await _communicationDomainService.IsValidToManagerToChecklEscalation(obj);
            AgencyCommunicationRequest request = await _communicationQueries.GetAgencyCommunicationRequestForReject(obj.CommunicationRequestId);
            request.UpdateEscalationRequest((int)Enums.AgencyCommunicationRequestStatus.Rejected, rejectReason);
            AgencyCommunicationRequest req = await _communicationCommands.UpdateAgencytRequestAsync(request);
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[]{ "", request.Tender.ReferenceNumber
                },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { request.Tender.ReferenceNumber },
                SMSArgs = new object[] { request.Tender.ReferenceNumber }
            };
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(NotificationArguments, $"CommunicationRequest/checkEscalationrequests/{Util.Encrypt(request.AgencyRequestId)}", NotificationEntityType.Tender, request.Tender.TenderId.ToString(), request.Tender.BranchId);
            await _notificationAppService.SendNotificationForUsersByRoleName(NotificationOperations.GrievanceSecretary.AgencyCommunicationRequest.PlaintEsclationRejected, RoleNames.SecretaryGrievanceCommittee, approveTender);

            return req;
        }

        #endregion

        #region [Negotiation]
        public async Task<AjaxResponse<OffersCompareViewModel>> DeleteNegotiationQitems(int tenderId, int negotiationId, int tableId, int rowNumber)
        {
            #region [VARIABLES ]
            int _negoId = negotiationId;

            Offer offer = await _negotiationQueries.FindOfferByNegotiationTableId(tableId);
            int offerId = offer.OfferId;
            var tender = await _tenderQueries.FindTenderWithOffer(tenderId);
            var isTawreed = tender.QuantityTableVersionId == (int)Enums.QuantityTableVersion.Version2;
            var apiData = new List<TenderQuantityItemDTO>();
            NegotiationSecondStage negotiation = await _negotiationQueries.FindSecondStageNegotiationWithTablesbyId(_negoId);
            var negotiationSupplierQuantitiestables = negotiation.negotiationSupplierQuantitiestable;
            var tableItems = negotiationSupplierQuantitiestables.FirstOrDefault(d => d.Id == tableId).NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems.ToList();

            List<Offer> offers = await _offerQueries.GetOffersForSecondNegotiationByTenderId(tenderId);
            AjaxResponse<OffersCompareViewModel> response = new AjaxResponse<OffersCompareViewModel>();
            List<int> itemNumbers = new List<int>();
            itemNumbers.Add(rowNumber);
            #endregion
            #region [ESTABLISH NEW TABLE ITEMS AFTER UPDATE AND SEND TO CALCULATE NEW OFFERVALUE]
            foreach (var item in tableItems)
            {
                var val = item.Value;
                var tenderQuantityItemDTO = new TenderQuantityItemDTO()
                {
                    ColumnId = item.ColumnId,
                    Id = item.Id,
                    TableId = tableId,
                    TenderFormHeaderId = item.TenderFormHeaderId,
                    Value = val,
                    TemplateId = item.ActivityTemplateId,
                    ItemNumber = item.ItemNumber,
                    IsDefault = true
                };
                apiData.Add(tenderQuantityItemDTO);
            }
            var islastitem = apiData.Select(d => d.ItemNumber).Distinct();
            var remainingTables = negotiationSupplierQuantitiestables.Any(d => d.NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems.Any() && d.Id != tableId);
            if (islastitem.Count() == 1 && !remainingTables)
            {
                throw new BusinessRuleException("لا يمكن حذف اخر عنصر ");
            }
            OffersCompareViewModel offersCompareViewModel = new OffersCompareViewModel();

            FormConfigurationDTO DTOModel = new FormConfigurationDTO()
            {
                NPpercentage = (tender.NationalProductPercentage ?? 0) / 100,
                EncryptedOfferId = Util.Encrypt(offer.OfferId),
                EncryptedTenderId = Util.Encrypt(offer.TenderId),
                EncryptedNegotiationId = Util.Encrypt(negotiation.NegotiationId),
                TenderId = offerId,
                ActivityId = 1,
                QuantityItemDtos = apiData.Where(e => itemNumbers.Any(f => f != e.ItemNumber)).ToList(),
                YearsCount = (tender.TemplateYears.HasValue ? tender.TemplateYears.Value : 0)

            };
            DTOModel.QuantityItemDtos.Clear();

            var otherTableItems = negotiationSupplierQuantitiestables.Where(d => d.Id != tableId && d.NegotiationQuantityItemJson != null).Select(t => new QuantitiesTemplateModel
            {
                QuantitiesItems = t.NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems.Select(i => new TenderQuantityItemDTO
                {
                    ColumnId = i.ColumnId,
                    Id = i.Id,
                    TableId = t.Id,
                    TenderFormHeaderId = i.TenderFormHeaderId,
                    Value = i.Value,
                    TemplateId = i.ActivityTemplateId,
                    ItemNumber = i.ItemNumber,
                    IsDefault = true
                }).ToList(),
            }).ToList();

            var otherItems = otherTableItems.SelectMany(d => d.QuantitiesItems).ToList();
            DTOModel.QuantityItemDtos.AddRange(otherItems);
            var CurrentTableitems = apiData.Where(e => itemNumbers.Any(f => f != e.ItemNumber)).ToList();
            DTOModel.QuantityItemDtos.AddRange(CurrentTableitems);
            decimal amountAfterNegoNP = 0;
            decimal amountAfterNego = 0;
            ApiResponse<TotalCostDTO> offerFinalPriceResponse = await _quantityTemplatesProxy.CalculateOfferFinalPricebyItems(DTOModel);


            if (offerFinalPriceResponse.StatusCode == 200)
                amountAfterNego = offerFinalPriceResponse.Data.TotalCostWithdiscount;
            amountAfterNegoNP = offerFinalPriceResponse.Data.TotalCostNP;
            #endregion

            bool isNotSameOrder = offers.Count > 1 &&
                ((isTawreed) ? (offers.OrderBy(d => d.OfferWeightAfterCalcNPA).ToArray()[1]).OfferWeightAfterCalcNPA.Value <= (amountAfterNegoNP) : (offers.OrderBy(d => d.FinalPriceAfterDiscount).ToArray()[1]).FinalPriceAfterDiscount.Value <= (amountAfterNego));

            if (isNotSameOrder)
            {
                #region [ESTABLISH RESPONSE IN CASE OF OREDER CHANGED]
                negotiation.UpdateNegotiationStatus((int)Enums.enNegotiationStatus.New, "");
                await _negotaitionCommands.UpdateNegotiationSecondStageAsync(negotiation);

                offersCompareViewModel.isSaved = false;
                response = new AjaxResponse<OffersCompareViewModel>();
                response.enMessageType = enAjaxResponseMessageType.warnning;
                response.Message = Resources.CommunicationRequest.ErrorMessages.SupplierOrederMustbeTheSameBeforeandAfterEdit;
                OrderOldOffers(offerId, isTawreed, offers, offersCompareViewModel, offerFinalPriceResponse);
                response.tableId = tableId;
                response.Data = offersCompareViewModel;
                return response;
                #endregion
            }
            #region [ESTABLISH RESPONSE IN CASE OF ORDER DIDNT AFFECTED AND UPDATE DB] 
            OrederOffers(isTawreed, offers, offersCompareViewModel, offerId, amountAfterNego, amountAfterNegoNP);

            negotiation.UpdateNegotiationStatus((int)Enums.enNegotiationStatus.UnderUpdate, "");
            var itemsforDelete = apiData.Where(e => e.TableId == tableId && itemNumbers.Any(dd => dd == e.ItemNumber)).ToList();
            negotiation.DeleteNegotiationQItems(itemsforDelete/*, negotiationSupplierQuantitiestable*/);
            await _negotaitionCommands.UpdateNegotiationSecondStageAsync(negotiation);

            #endregion
            #region [CREATE NEW TABLE ITEMS ]

            #endregion
            #region [RETURN DATA]
            offersCompareViewModel.isSaved = true;
            response.Data = offersCompareViewModel;
            response.tableId = tableId;
            response.Message = Resources.SharedResources.Messages.DataSaved;
            response.enMessageType = enAjaxResponseMessageType.success;
            #endregion
            return response;
        }


        public async Task<AjaxResponse<OffersCompareViewModel>> SaveNegotiationQitems(Dictionary<string, string> AuthorList)
        {
            #region [VARIABLES ]
            int tenderId = Util.Decrypt(AuthorList["EncryptedTenderId"]);
            int _negoId = Util.Decrypt(AuthorList["EncryptedNegotiationId"]);
            int offerId = Util.Decrypt(AuthorList["EncryptedOfferId"]);
            int tableId = int.Parse(AuthorList["TableId"]);
            Offer offer = await _offerQueries.FindOfferWithSupplierTenderQuantitiesByOfferId(offerId);
            var tender = await _tenderQueries.FindTenderWithOffer(tenderId);
            var isTawreed = tender.QuantityTableVersionId == (int)Enums.QuantityTableVersion.Version2;
            var apiData = new List<TenderQuantityItemDTO>();
            NegotiationSecondStage negotiation = await _negotiationQueries.FindSecondStageNegotiationWithTablesbyId(_negoId);
            var negotiationSupplierQuantitiestables = negotiation.negotiationSupplierQuantitiestable.Where(d => d.NegotiationQuantityItemJson != null);
            var tableItems = negotiationSupplierQuantitiestables.FirstOrDefault(d => d.Id == tableId).NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems.ToList();

            List<Offer> offers = await _offerQueries.GetOffersForSecondNegotiationByTenderId(tenderId);
            AjaxResponse<OffersCompareViewModel> response = new AjaxResponse<OffersCompareViewModel>();
            List<int> formItems = new List<int>();
            List<int> itemNumbers = new List<int>();

            #endregion
            #region [ESTABLISH NEW TABLE ITEMS AFTER UPDATE AND SEND TO CALCULATE NEW OFFERVALUE]
            foreach (var item in tableItems)
            {
                string _id = item.Id.ToString();
                var val = item.Value;
                var keyItem = AuthorList.Where(s => System.Text.RegularExpressions.Regex.IsMatch(s.Key, @"\d") && s.Key == _id);
                if (keyItem.Any())
                {
                    formItems.Add((int)item.Id);
                    var _number = (int)item.ItemNumber;
                    if (!itemNumbers.Any(o => o == _number))
                    {
                        itemNumbers.Add(_number);
                    }
                    val = keyItem.FirstOrDefault().Value;

                }

                var tenderQuantityItemDTO = new TenderQuantityItemDTO()
                {
                    ColumnId = item.ColumnId,
                    Id = item.Id,
                    TableId = tableId,
                    TenderFormHeaderId = item.TenderFormHeaderId,
                    Value = val,
                    TemplateId = item.ActivityTemplateId,
                    ItemNumber = item.ItemNumber,
                    IsDefault = true
                };
                apiData.Add(tenderQuantityItemDTO);
            }
            var remainingTables = offer.SupplierTenderQuantityTables.Any(d => d.Id != tableId);
            if (itemNumbers.Count() == 0 && !remainingTables)
            {
                throw new BusinessRuleException("لا يمكن حذف اخر عنصر ");
            }
            OffersCompareViewModel offersCompareViewModel = new OffersCompareViewModel();

            FormConfigurationDTO DTOModel = new FormConfigurationDTO()
            {
                NPpercentage = (tender.NationalProductPercentage ?? 0) / 100,
                EncryptedOfferId = Util.Encrypt(offer.OfferId),
                EncryptedTenderId = Util.Encrypt(offer.TenderId),
                EncryptedNegotiationId = Util.Encrypt(negotiation.NegotiationId),
                TenderId = offerId,
                ActivityId = 1,

                QuantityItemDtos = apiData.Where(e => itemNumbers.Any(f => f == e.ItemNumber)).ToList(),
                YearsCount = (tender.TemplateYears.HasValue ? tender.TemplateYears.Value : 0)

            };
            ApiResponse<List<TableTemplateDTO>> validateResponse = await _quantityTemplatesProxy.NegotiationValidateQuantityItem(DTOModel);
            DTOModel.QuantityItemDtos.Clear();
            var otherTableItems = negotiationSupplierQuantitiestables.Where(d => d.Id != tableId).Select(t => new QuantitiesTemplateModel
            {
                QuantitiesItems = t.NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems.Select(i => new TenderQuantityItemDTO
                {
                    ColumnId = i.ColumnId,
                    Id = i.Id,
                    TableId = t.Id,
                    TenderFormHeaderId = i.TenderFormHeaderId,
                    Value = i.Value,
                    TemplateId = i.ActivityTemplateId,
                    ItemNumber = i.ItemNumber,
                    IsDefault = true
                }).ToList(),
            }).ToList();
            var otherItems = otherTableItems.SelectMany(x => x.QuantitiesItems).ToList();
            DTOModel.QuantityItemDtos.AddRange(otherItems);
            var CurrentTableitems = validateResponse.Data[0].QuantityItemDtos.Select(item => new TenderQuantityItemDTO()
            {
                ColumnId = item.ColumnId,
                Id = item.Id,
                TableId = item.TableId,
                TenderFormHeaderId = item.TenderFormHeaderId,
                Value = item.Value,
                ItemNumber = item.ItemNumber,
                TemplateId = item.TemplateId,
                IsDefault = true
            }).ToList();
            DTOModel.QuantityItemDtos.AddRange(CurrentTableitems);
            decimal amountAfterNegoNP = 0;
            decimal amountAfterNego = 0;
            ApiResponse<TotalCostDTO> offerFinalPriceResponse = await _quantityTemplatesProxy.CalculateOfferFinalPricebyItems(DTOModel);
            if (validateResponse.StatusCode != 200 || offerFinalPriceResponse.StatusCode != 200)
            {
                return BuildFailureResponse(isTawreed, offers, response, offersCompareViewModel);
            }

            if (offerFinalPriceResponse.StatusCode == 200)
                amountAfterNego = offerFinalPriceResponse.Data.TotalCostWithdiscount;
            amountAfterNegoNP = offerFinalPriceResponse.Data.TotalCostNP;
            #endregion
            bool isNotSameOrder = offers.Count > 1 &&
                 ((isTawreed) ? (offers.OrderBy(d => d.OfferWeightAfterCalcNPA).ToArray()[1]).OfferWeightAfterCalcNPA.Value <= (amountAfterNegoNP) : (offers.OrderBy(d => d.FinalPriceAfterDiscount).ToArray()[1]).FinalPriceAfterDiscount.Value <= (amountAfterNego));

            if (isNotSameOrder)
            {
                #region [ESTABLISH RESPONSE IN CASE OF OREDER CHANGED]
                negotiation.UpdateNegotiationStatus((int)Enums.enNegotiationStatus.New, "");
                await _negotaitionCommands.UpdateNegotiationSecondStageAsync(negotiation);

                offersCompareViewModel.isSaved = false;
                response = new AjaxResponse<OffersCompareViewModel>();
                response.enMessageType = enAjaxResponseMessageType.warnning;
                response.Message = Resources.CommunicationRequest.ErrorMessages.SupplierOrederMustbeTheSameBeforeandAfterEdit;

                OrderOldOffers(offerId, isTawreed, offers, offersCompareViewModel, offerFinalPriceResponse);

                response.tableId = tableId;
                response.Data = offersCompareViewModel;
                return response;
                #endregion
            }
            #region [ESTABLISH RESPONSE IN CASE OF ORDER DIDNT AFFECTED AND UPDATE DB]
            OrederOffers(isTawreed, offers, offersCompareViewModel, offerId, amountAfterNego, amountAfterNegoNP);

            negotiation.UpdateNegotiationStatus((int)Enums.enNegotiationStatus.UnderUpdate, "");
            negotiation.UpdateNegotiationQItems(DTOModel.QuantityItemDtos.Where(e => e.TableId == tableId && itemNumbers.Any(dd => dd == e.ItemNumber)).ToList()/*, negotiationSupplierQuantitiestable*/);
            negotiation = await _negotaitionCommands.UpdateNegotiationSecondStageAsync(negotiation);
            await _negotaitionCommands.UpdateNegotiationSecondStageAsync(negotiation);
            DTOModel.QuantityItemDtos = DTOModel.QuantityItemDtos.Where(e => e.TableId == tableId).ToList();

            #endregion
            #region [CREATE NEW TABLE ITEMS ]
            negotiation = await _negotiationQueries.FindSecondStageNegotiationbyId(_negoId);
            tableItems = negotiation.negotiationSupplierQuantitiestable.FirstOrDefault(d => d.Id == tableId).NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems.ToList();

            DTOModel.QuantityItemDtos = tableItems.Select(item => new TenderQuantityItemDTO()
            {
                ColumnId = item.ColumnId,
                Id = item.Id,
                TableId = tableId,
                TenderFormHeaderId = item.TenderFormHeaderId,
                Value = item.Value,
                ItemNumber = item.ItemNumber

            }).ToList();
            ApiResponse<List<HtmlTemplateDto>> obj;
            obj = await _quantityTemplatesProxy.GenerateNegotiationTemplate(DTOModel);
            response.htmlData = obj.Data == null ? "" : obj.Data.FirstOrDefault().EditFormHtml;
            #endregion
            #region [RETURN DATA]
            offersCompareViewModel.isSaved = true;
            response.Data = offersCompareViewModel;
            response.tableId = tableId;
            response.Message = Resources.SharedResources.Messages.DataSaved;
            response.enMessageType = enAjaxResponseMessageType.success;
            #endregion
            return response;
        }
        private static AjaxResponse<OffersCompareViewModel> BuildFailureResponse(bool isTawreed, List<Offer> offers, AjaxResponse<OffersCompareViewModel> response, OffersCompareViewModel offersCompareViewModel)
        {
            offersCompareViewModel.isSaved = false;
            response.enMessageType = enAjaxResponseMessageType.danger;
            response.Message = Resources.SharedResources.ErrorMessages.ModelDataError;
            offersCompareViewModel.OldOffersCompareGrid = offers
                                        .Select(o => new OffersCompareGridViewModel(o.Supplier.SelectedCrName, o.CommericalRegisterNo,
                                        o.FinalPriceAfterDiscount.Value,
                                        Util.Encrypt(o.OfferId),
                                        Util.Encrypt(o.Combined.Where(c => c.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader).FirstOrDefault().Id), o.Combined.Count,
                                        (o.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer ? Resources.OfferResources.DisplayInputs.AccepteOffer : Resources.OfferResources.DisplayInputs.RejectedOffer),
                                        (o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer ? Resources.OfferResources.DisplayInputs.Matching : Resources.OfferResources.DisplayInputs.NotMatching)

                                          ,
o.Tender.Invitations.Count > 0 && o.Tender.ConditionsBooklets.Count == 0 ?
                       o.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == o.CommericalRegisterNo).InvitationStatus.NameAr :
                       o.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == o.CommericalRegisterNo && x.BillInfo != null).BillInfo.BillStatusId == (int)Enums.BillStatus.Paid ? Resources.TenderResources.DisplayInputs.Purchased : Resources.TenderResources.DisplayInputs.NotPurchased

                       , isTawreed

                                        )).OrderBy(d => d.OfferPrice).ToList();
            response.Data = offersCompareViewModel; return response;
        }

        private static void OrederOffers(bool isTawreed, List<Offer> offers, OffersCompareViewModel offersCompareViewModel, int offerId, decimal newamount, decimal newAmountNP)
        {
            offersCompareViewModel.OldOffersCompareGrid = offers
                                                 .Select(o => new OffersCompareGridViewModel(o.Supplier.SelectedCrName, o.CommericalRegisterNo,
                                             o.OfferId == offerId ? newamount : o.FinalPriceAfterDiscount.Value,

                                                 Util.Encrypt(o.OfferId),

                                                 Util.Encrypt(o.Combined.Where(c => c.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader).FirstOrDefault().Id),
                                                 o.Combined.Count,
                                                 (o.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer ? Resources.OfferResources.DisplayInputs.AccepteOffer : Resources.OfferResources.DisplayInputs.RejectedOffer),
                                                 (o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer ? Resources.OfferResources.DisplayInputs.Matching : Resources.OfferResources.DisplayInputs.NotMatching),
        o.Tender.Invitations.Count > 0 && o.Tender.ConditionsBooklets.Count == 0 ? o.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == o.CommericalRegisterNo).InvitationStatus.NameAr : o.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == o.CommericalRegisterNo && x.BillInfo != null).BillInfo.BillStatusId == (int)Enums.BillStatus.Paid ? Resources.TenderResources.DisplayInputs.Purchased : Resources.TenderResources.DisplayInputs.NotPurchased,
        isTawreed,
        (o.OfferId == offerId ? newAmountNP : (o.OfferWeightAfterCalcNPA.HasValue ? o.OfferWeightAfterCalcNPA.Value : 0))
        )

                                                 ).OrderBy(d => isTawreed ? d.OfferPriceNP : d.OfferPrice).ToList();


        }

        private static void OrderOldOffers(int offerId, bool isTawreed, List<Offer> offers, OffersCompareViewModel offersCompareViewModel, ApiResponse<TotalCostDTO> offerFinalPriceResponse)
        {
            offersCompareViewModel.OldOffersCompareGrid = offers
                          .Select(o => new OffersCompareGridViewModel(o.Supplier.SelectedCrName, o.CommericalRegisterNo,
                          o.FinalPriceAfterDiscount.Value,
                          Util.Encrypt(o.OfferId),
                          Util.Encrypt(o.Combined.Where(c => c.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader).FirstOrDefault().Id), o.Combined.Count,
                          (o.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer ? Resources.OfferResources.DisplayInputs.AccepteOffer : Resources.OfferResources.DisplayInputs.RejectedOffer),
                          (o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer ? Resources.OfferResources.DisplayInputs.Matching : Resources.OfferResources.DisplayInputs.NotMatching)

                            ,
o.Tender.Invitations.Count > 0 && o.Tender.ConditionsBooklets.Count == 0 ?
                       o.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == o.CommericalRegisterNo).InvitationStatus.NameAr :
                       o.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == o.CommericalRegisterNo && x.BillInfo != null).BillInfo.BillStatusId == (int)Enums.BillStatus.Paid ? Resources.TenderResources.DisplayInputs.Purchased : Resources.TenderResources.DisplayInputs.NotPurchased
                      , isTawreed, o.OfferWeightAfterCalcNPA ?? 0
                          )).OrderBy(d => d.OfferPriceNP).ToList();

            offersCompareViewModel.NewOffersCompareGrid = new List<OffersCompareGridViewModel>();
            var newOffers = offersCompareViewModel.OldOffersCompareGrid
                                           .Select(o => new OffersCompareGridViewModel(o.CommericalRegisterName, o.CommericalRegisterNo,
                                            o.OfferPrice,
                                           Util.Encrypt(o.OfferId),
                                          Util.Encrypt(o.CombinedId), o.CombinedSupplierCount,
                                         o.OfferAcceptanceStatus, o.OfferTechnicalEvaluationStatus
                                        , o.InvitationPurchaseStatus, isTawreed, o.OfferPriceNP)
                                            ).OrderBy(d => d.OfferPriceNP).ToList();


            foreach (var of in newOffers)
            {
                if (of.OfferId == offerId)
                {
                    of.OfferPrice = offerFinalPriceResponse.Data.TotalCostWithdiscount;
                    of.OfferPriceNP = offerFinalPriceResponse.Data.TotalCostNP;
                }
                offersCompareViewModel.NewOffersCompareGrid.Add(of);
            }

            offersCompareViewModel.NewOffersCompareGrid = offersCompareViewModel.NewOffersCompareGrid.OrderBy(d => isTawreed ? d.OfferPriceNP : d.OfferPrice).ToList();
        }

        public async Task<NegotiationAgencyPageModel> GetNegotiationPageData(int negotiationId, int tenderId)
        {
            var CR = _httpContextAccessor.HttpContext.User.SupplierNumber();


            NegotiationAgencyPageModel negotiationPageModel = new NegotiationAgencyPageModel
            {
                NegotiationTenderModel = await _tenderQueries.GetTendeBasicDataForNegotiation(tenderId),
                NegotiationFirstStageModel = await _communicationQueries.GetNegotiationDatabyId(tenderId, CR)

            };
            if (CR != "" && negotiationPageModel.NegotiationTenderModel.tenderStatusId != (int)Enums.TenderStatus.OffersAwarding)
            {
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.TenderNotinOfferAwarding);
            }
            if (negotiationPageModel.NegotiationTenderModel.tenderTypeId == (int)Enums.TenderType.Competition)
            {
                throw new BusinessRuleException("غير مسموح بالتفاوض  لهذه المنافسة");
            }
            if (negotiationPageModel.NegotiationTenderModel.committeeId != _httpContextAccessor.HttpContext.User.SelectedCommittee() &&
                (_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersCheckManager) || _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersCheckSecretary)

                || _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersPurchaseSecretary) || _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersPurchaseManager)

                ))
            {
                throw new BusinessRuleException("غير مسموح");
            }

            return negotiationPageModel;
        }


        public async Task<NegotiationAgencyPageModel> GetNegotiationDataFirstStage(int negotiationId, int tenderId)
        {
            var CR = _httpContextAccessor.HttpContext.User.SupplierNumber();
            NegotiationAgencyPageModel negotiationPageModel = new NegotiationAgencyPageModel();
            negotiationPageModel.NegotiationTenderModel = await _tenderQueries.GetTendeBasicDataForNegotiation(tenderId);
            negotiationPageModel.CreateNegotiationFirstStageData = await _communicationQueries.GetNegotiationDataForFirstStagebyId(negotiationId);
            var lastNegotiation = await _communicationQueries.GetLastAgreedNegotiationFirstStageByTenderId(tenderId);
            if (lastNegotiation != null && lastNegotiation.NegotiationId != negotiationId)
                negotiationPageModel.CreateNegotiationFirstStageData.IsFirstNegotiation = false;

            NegotiationFirstStageValidation(CR, negotiationPageModel);
            return negotiationPageModel;
        }

        private void NegotiationFirstStageValidation(string CR, NegotiationAgencyPageModel negotiationPageModel)
        {
            if (CR != "" && negotiationPageModel.NegotiationTenderModel.tenderStatusId != (int)Enums.TenderStatus.OffersAwarding)
            {
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.TenderNotinOfferAwarding);
            }
            if (negotiationPageModel.NegotiationTenderModel.tenderTypeId == (int)Enums.TenderType.Competition)
            {
                throw new BusinessRuleException("غير مسموح بالتفاوض  لهذه المنافسة");
            }
            if ((negotiationPageModel.NegotiationTenderModel.committeeId != _httpContextAccessor.HttpContext.User.SelectedCommittee() &&
                (_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersCheckManager) || _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersCheckSecretary)
                || _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersPurchaseSecretary) || _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersPurchaseManager)
                )) && !negotiationPageModel.CreateNegotiationFirstStageData.IsUserHasAccessToLowBudgetTender)
            {
                throw new BusinessRuleException("غير مسموح");
            }
        }

        public async Task<bool> isAllowedToApplySecondStageNegotiation(int tenderId)
        {

            var isAllowedToApply = await _communicationQueries.IsFirstStageNegotiationExist(tenderId);
            return isAllowedToApply;

        }

        public async Task CreateFirstStageNegotiation(NegotiationAgencyFirstStageEditModel negotiationModel)
        {
            int TenderId = Util.Decrypt(negotiationModel.TenderIdString);
            Check.ArgumentNotNull(nameof(negotiationModel), negotiationModel);
            var Tender = await _tenderQueries.FindTenderWithAgenyRequestsAndNegotiations(TenderId);
            if (Tender.TenderTypeId == (int)Enums.TenderType.Competition)
            {
                throw new BusinessRuleException("غير مسموح بالتفاوض  لهذه المنافسة");
            }
            Check.ArgumentNotNull(nameof(Tender), Tender);
            if (Tender.TenderStatusId != (int)Enums.TenderStatus.OffersAwarding)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.WrongTenderStatus);
            var Requests = Tender.AgencyCommunicationRequests.Where(w => w.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Negotiation).ToList();
            Check.ArgumentNotNull(nameof(Requests), Requests);
            NegotiationFirstStage negotiationFirstStage;
            AgencyCommunicationRequest agencyCommunicationRequest;
            if (!Requests.Any())
            {
                agencyCommunicationRequest = new AgencyCommunicationRequest(Tender.TenderId, (int)Enums.AgencyCommunicationRequestType.Negotiation, (int)Enums.AgencyCommunicationRequestStatus.Pending, RoleNames.OffersCheckSecretary);
                Tender.AddAgencyCommunicationRequest(agencyCommunicationRequest);
                await _tenderCommands.UpdateAsync(Tender);
            }
            else
            {
                agencyCommunicationRequest = Requests.First();
                var ExistNegotiation = agencyCommunicationRequest.Negotiations.Where(w => w.NegotiationTypeId == (int)Enums.enNegotiationType.FirstStage).FirstOrDefault();
                if (ExistNegotiation != null)
                    throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.NegotiationRequestUnderProcessing);
            }
            var attachmentlst = new List<NegotiationAttachmentViewModel>
            {
                negotiationModel.Attachment
            };
            negotiationFirstStage = new NegotiationFirstStage(negotiationModel.CalculatedSupplierPeriod(), negotiationModel.ReductionLetterrefId, negotiationModel.ReductionPercent, agencyCommunicationRequest.AgencyRequestId, negotiationModel.NegotiationReasonId, attachmentlst, negotiationModel.StatusId, negotiationModel.ProjectNumber);
            await _negotaitionCommands.CreateNegotiationFirstStageAsync(negotiationFirstStage);
            Tender tender = await _offerQueries.GetTenderbyTenderIdAsync(TenderId);
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };

            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
            $"CommunicationRequest/CreateNegotiation/{negotiationModel.TenderIdString}/{negotiationModel.NegotiationIdString}",
            NotificationEntityType.Tender,
             tender.TenderId.ToString(), tender.OffersCheckingCommitteeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckManager.AgencyCommunicationRequest.SendFirstNegotitionToApprove, tender.OffersCheckingCommitteeId, mainNotificationTemplateModel);


        }
        public async Task UpdateFirstStageNegotiation(NegotiationAgencyFirstStageEditModel negotiationModel)
        {
            if (negotiationModel.Days == 0 && negotiationModel.Hours == 0)
            {
                throw new BusinessRuleException("لابد ان تكون الفترة على الاقل ساعة واحدة");
            }
            if (negotiationModel.ActionType == enSubmitActionType.SaveAndSend)
            {
                negotiationModel.StatusId = (int)Enums.enNegotiationStatus.CheckManagerPendingApprove;
            }

            _httpContextAccessor.HttpContext.User.UserId();
            Check.ArgumentNotNull(nameof(negotiationModel), negotiationModel);
            NegotiationFirstStage negotiationFirstStage = await _negotiationQueries.FindWithAgencyRequestById(Util.Decrypt(negotiationModel.NegotiationIdString));
            Check.ArgumentNotNull(nameof(negotiationFirstStage), negotiationFirstStage);
            var AgencyRequest = negotiationFirstStage.AgencyCommunicationRequest;
            Check.ArgumentNotNull(nameof(AgencyRequest.Tender), AgencyRequest.Tender);
            if (AgencyRequest.Tender.TenderStatusId != (int)Enums.TenderStatus.OffersAwarding)
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.TenderNotinOfferAwarding);
            negotiationFirstStage.UpdateNegotiationFirstStage(negotiationModel.CalculatedSupplierPeriod(), negotiationModel.ReductionLetterrefId, negotiationModel.ReductionPercent, negotiationModel.NegotiationReasonId, negotiationModel.StatusId, new List<NegotiationAttachmentViewModel> { negotiationModel.Attachment }, negotiationModel.ProjectNumber);
            await _negotaitionCommands.UpdateNegotiationFirstStageAsync(negotiationFirstStage);
            AgencyRequest.UpdateAgencyCommunicationRequestStatus((int)Enums.AgencyCommunicationRequestStatus.Pending, "");
            await _communicationCommands.UpdateAsync(AgencyRequest);
            if (negotiationModel.ActionType == Enums.enSubmitActionType.SaveAndSend)
            {
                int code = NotificationOperations.OffersCheckManager.AgencyCommunicationRequest.SendFirstNegotitionToApprove;
                Tender tender = await _offerQueries.GetTenderbyTenderIdAsync(Util.Decrypt(negotiationModel.TenderIdString));
                NotificationArguments NotificationArguments = new NotificationArguments
                {
                    BodyEmailArgs = new object[] { tender.ReferenceNumber },
                    SubjectEmailArgs = new object[] { },
                    PanelArgs = new object[] { tender.ReferenceNumber },
                    SMSArgs = new object[] { tender.ReferenceNumber }
                };
                int? committeeId = tender.OffersCheckingCommitteeId;
                if (tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
                {
                    code = NotificationOperations.OffersCheckManager.AgencyCommunicationRequest.SendFirstNegotitionToApprovePurchase;
                    committeeId = tender.DirectPurchaseCommitteeId;
                }

                MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
                $"CommunicationRequest/Negotiation/{negotiationModel.TenderIdString}/{negotiationModel.NegotiationIdString}",
                NotificationEntityType.Tender,
                 tender.TenderId.ToString(), code);
                await _notificationAppService.SendNotificationForCommitteeUsers(code, committeeId, mainNotificationTemplateModel);


            }
        }

        public async Task UpdateFirstStageNegotiationData(CreateNegotiationFirstStageDataModel negotiationModel)
        {
            if (negotiationModel.Days == 0 && negotiationModel.Hours == 0)
            {
                throw new BusinessRuleException("لابد ان تكون الفترة على الاقل ساعة واحدة");
            }

            Check.ArgumentNotNull(nameof(negotiationModel), negotiationModel);
            NegotiationFirstStage negotiationFirstStage = await _negotiationQueries.FindWithAgencyRequestAndSuppliersById(Util.Decrypt(negotiationModel.NegotiationIdString));
            if (negotiationModel.ActionType == enSubmitActionType.SaveAndSend)
            {
                negotiationModel.StatusId = (int)Enums.enNegotiationStatus.CheckManagerPendingApprove;
            }

            if (negotiationFirstStage.AgencyCommunicationRequest.Tender.TenderStatusId != (int)Enums.TenderStatus.OffersAwarding)
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.TenderNotinOfferAwarding);

            await CheckLowestOfferValue(negotiationFirstStage.AgencyCommunicationRequest.TenderId, negotiationModel.DesiredOffersAmount);

            negotiationFirstStage.UpdateNegotiationFirstStage(negotiationModel.CalculatedSupplierPeriod(), negotiationModel.ReductionLetterrefId, negotiationModel.DesiredOffersAmount, negotiationModel.NegotiationReasonId, negotiationModel.StatusId, new List<NegotiationAttachmentViewModel> { negotiationModel.Attachment }, negotiationModel.ProjectNumber);
            negotiationFirstStage.AgencyCommunicationRequest.UpdateAgencyCommunicationRequestStatus((int)Enums.AgencyCommunicationRequestStatus.Pending, "");

            if (negotiationModel.ActionType == enSubmitActionType.SaveAndSend)
            {
                var Offers = await _offerQueries.GetNotNegotiatedOffersForFirstStageNegotiationByTenderId(negotiationFirstStage.AgencyCommunicationRequest.TenderId);
                var OrderedOfferLst = (negotiationFirstStage.AgencyCommunicationRequest.Tender.QuantityTableVersionId == (int)Enums.QuantityTableVersion.Version2 ? Offers.OrderBy(s => s.OfferWeightAfterCalcNPA).ToList() : Offers.OrderBy(s => s.FinalPriceAfterDiscount).ToList());
                foreach (var offer in OrderedOfferLst)
                {
                    if (offer.negotiationFirstStageSuppliers.Any())
                    {
                        var negSupplier = offer.negotiationFirstStageSuppliers.FirstOrDefault(s => s.IsActive == true);
                        negSupplier.UpdateNegotiationId(Util.Decrypt(negotiationModel.NegotiationIdString));
                        _genericCommandRepository.Update(negSupplier);
                        negotiationFirstStage.NegotiationFirstStageSuppliers.Clear();
                    }
                    else
                    {
                        var SupplierNegotiation = new NegotiationFirstStageSupplier((int)Enums.enNegotiationSupplierStatus.NotSent, null, offer.OfferId, offer.CommericalRegisterNo, negotiationFirstStage.NegotiationId, offer.FinalPriceAfterDiscount ?? 0);
                        negotiationFirstStage.AddSupplier(SupplierNegotiation);
                    }

                }
            }
            await _negotaitionCommands.UpdateNegotiationFirstStageAsync(negotiationFirstStage);

            if (negotiationModel.ActionType == Enums.enSubmitActionType.SaveAndSend && !(negotiationFirstStage.AgencyCommunicationRequest.Tender.IsLowBudgetDirectPurchase.HasValue && negotiationFirstStage.AgencyCommunicationRequest.Tender.IsLowBudgetDirectPurchase.Value))
            {
                int code = NotificationOperations.OffersCheckManager.AgencyCommunicationRequest.SendFirstNegotitionToApprove;
                Tender tender = await _offerQueries.GetTenderbyTenderIdAsync(Util.Decrypt(negotiationModel.TenderIdString));
                NotificationArguments NotificationArguments = new NotificationArguments
                {
                    BodyEmailArgs = new object[] { tender.ReferenceNumber },
                    SubjectEmailArgs = new object[] { },
                    PanelArgs = new object[] { tender.ReferenceNumber },
                    SMSArgs = new object[] { tender.ReferenceNumber }
                };
                int? committeeId = tender.OffersCheckingCommitteeId;
                if (tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
                {
                    code = NotificationOperations.OffersCheckManager.AgencyCommunicationRequest.SendFirstNegotitionToApprovePurchase;
                    committeeId = tender.DirectPurchaseCommitteeId;
                }

                MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
                $"CommunicationRequest/NewNegotiation/{negotiationModel.TenderIdString}/{negotiationModel.NegotiationIdString}",
                NotificationEntityType.Tender,
                 tender.TenderId.ToString(), code);
                await _notificationAppService.SendNotificationForCommitteeUsers(code, committeeId, mainNotificationTemplateModel);

            }
        }

        public async Task SendSecondFirstStageNegotiationToApprove(CreateNegotiationFirstStageDataModel negotiationModel)
        {

            Check.ArgumentNotNull(nameof(negotiationModel), negotiationModel);
            NegotiationFirstStage negotiationFirstStage = await _negotiationQueries.FindNegotiationWithAgencyRequestById(Util.Decrypt(negotiationModel.NegotiationIdString));

            if (negotiationFirstStage.AgencyCommunicationRequest.Tender.TenderStatusId != (int)Enums.TenderStatus.OffersAwarding)
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.TenderNotinOfferAwarding);

            negotiationFirstStage.UpdateNegotiationFirstStageStatus((int)Enums.enNegotiationStatus.CheckManagerPendingApprove, "");
            negotiationFirstStage.AgencyCommunicationRequest.UpdateAgencyCommunicationRequestStatus((int)Enums.AgencyCommunicationRequestStatus.Pending, "");

            #region Add-Suppliers-Negotiation
            var Offers = await _offerQueries.GetNotNegotiatedOffersForFirstStageNegotiationByTenderId(negotiationFirstStage.AgencyCommunicationRequest.TenderId);
            var OrderedOfferLst = (negotiationFirstStage.AgencyCommunicationRequest.Tender.QuantityTableVersionId == (int)Enums.QuantityTableVersion.Version2 ? Offers.OrderBy(s => s.OfferWeightAfterCalcNPA).ToList() : Offers.OrderBy(s => s.FinalPriceAfterDiscount).ToList());

            foreach (var offer in OrderedOfferLst)
            {
                if (offer.negotiationFirstStageSuppliers.Any())
                {
                    var negSupplier = offer.negotiationFirstStageSuppliers.FirstOrDefault(s => s.IsActive == true);
                    negSupplier.UpdateNegotiationId(Util.Decrypt(negotiationModel.NegotiationIdString));
                    _genericCommandRepository.Update(negSupplier);
                }
                else
                {
                    var SupplierNegotiation = new NegotiationFirstStageSupplier((int)Enums.enNegotiationSupplierStatus.NotSent, null, offer.OfferId, offer.CommericalRegisterNo, negotiationFirstStage.NegotiationId, offer.FinalPriceAfterDiscount ?? 0);
                    negotiationFirstStage.AddSupplier(SupplierNegotiation);
                }
            }
            await _negotaitionCommands.UpdateNegotiationFirstStageAsync(negotiationFirstStage);
            #endregion  Add-Suppliers-Negotiation
            #region Notification
            int code = NotificationOperations.OffersCheckManager.AgencyCommunicationRequest.SendFirstNegotitionToApprove;
            Tender tender = await _offerQueries.GetTenderbyTenderIdAsync(Util.Decrypt(negotiationModel.TenderIdString));
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            int? committeeId = tender.OffersCheckingCommitteeId;
            if (tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            {
                code = NotificationOperations.OffersCheckManager.AgencyCommunicationRequest.SendFirstNegotitionToApprovePurchase;
                committeeId = tender.DirectPurchaseCommitteeId;
            }
            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
            $"CommunicationRequest/NewNegotiation/{negotiationModel.TenderIdString}/{negotiationModel.NegotiationIdString}",
            NotificationEntityType.Tender,
             tender.TenderId.ToString(), code);
            await _notificationAppService.SendNotificationForCommitteeUsers(code, committeeId, mainNotificationTemplateModel);
            #endregion Notification
        }



        private async Task CheckLowestOfferValue(int tenderId, decimal reductionAmount)
        {
            var offer = await _offerQueries.GetTheLowestOfferByTenderId(tenderId);
            if (reductionAmount >= offer.FinalPriceAfterDiscount || reductionAmount < 1)
            {
                throw new BusinessRuleException("القيمة المستهدفة للعروض يجب ان تكون اكبر من 1 واقل من قيمة اقل عرض ");
            }
        }

        public async Task<List<NegotiationOfferModel>> PreviewOfferListDiscount(NegotiationAgencyFirstStageEditModel negotiationModel)
        {
            return await GetOffersForNegotiation(Util.Decrypt(negotiationModel.TenderIdString), negotiationModel.ReductionPercent);
        }
        public async Task<List<NegotiationOfferModel>> PreviewOfferListDiscount(string TenderIdString, decimal ReductionAmount)
        {
            return await GetOffersForNegotiation(Util.Decrypt(TenderIdString), ReductionAmount);
        }
        public async Task<NegotiationAgencyFirstStageEditModel> GetNegotiationFirstStageDatabyId(string negotiationIdString)
        {
            return await _communicationQueries.GetNegotiationFirstStageDatabyId(Util.Decrypt(negotiationIdString));
        }
        public async Task<List<NegotiationOfferModel>> GetOffersForNegotiation(int tenderId, decimal Discount)
        {
            return await _offerQueries.GetOffersForNegotiationByTenderId(tenderId, Discount);
        }
        public async Task<QueryResult<NegotiationOfferModel>> GetOffersListForFirstStageNegotiation(NegotiationOffersSearchModel negotiationOffersSearchModel)
        {
            return await _offerQueries.GetOffersListForFirstStageNegotiation(negotiationOffersSearchModel);
        }
        public async Task<QueryResult<NegotiationOfferModel>> GetOffersForFirstStageNegotiation(NegotiationOffersSearchModel negotiationOffersSearchModel)
        {
            return await _offerQueries.GetOffersForFirstStageNegotiation(negotiationOffersSearchModel);
        }
        public async Task<QueryResult<NegotiationOfferModel>> GetNotNegotitedOffersForFirstStageNegotiation(NegotiationOffersSearchModel negotiationOffersSearchModel)
        {
            return await _offerQueries.GetNotNegotitedOffersForFirstStageNegotiation(negotiationOffersSearchModel);
        }


        public async Task<string> CreateNegotiationRequest(StartNegotiationModel model)
        {

            Tender Tender = await _tenderQueries.GetTenderAgencyCommunicationById(Util.Decrypt(model.TenderIdString));
            int? committeeId = Tender.OffersCheckingCommitteeId;
            if (Tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || Tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            {
                committeeId = Tender.DirectPurchaseCommitteeId;
            }
            CheckValidationToAddNegotiation(Tender, committeeId);

            Check.ArgumentNotNull(nameof(Tender), Tender);
            if (Tender.TenderStatusId != (int)Enums.TenderStatus.OffersAwarding)
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.TenderNotinOfferAwarding);

            var lastNegotiation = await _communicationQueries.GetLastNegotiationFirstStageWithSupplierByTenderId(Util.Decrypt(model.TenderIdString));
            switch ((Enums.enNegotiationType)model.enNegotiationTypeId)
            {
                case enNegotiationType.FirstStage:
                    if (lastNegotiation != null)
                        IsValidToCreateFirstStageNegotiation(lastNegotiation);


                    var Requests = Tender.AgencyCommunicationRequests.Where(w => w.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Negotiation && w.IsActive == true).ToList();
                    Check.ArgumentNotNull(nameof(Requests), Requests);
                    AgencyCommunicationRequest agencyCommunicationRequest;
                    if (!Requests.Any())
                    {
                        agencyCommunicationRequest = new AgencyCommunicationRequest(Tender.TenderId, (int)Enums.AgencyCommunicationRequestType.Negotiation, (int)Enums.AgencyCommunicationRequestStatus.Pending, RoleNames.OffersCheckSecretary);
                        Tender.AddAgencyCommunicationRequest(agencyCommunicationRequest);
                        await _tenderCommands.UpdateAsync(Tender);
                    }
                    else
                    {
                        agencyCommunicationRequest = Requests.First();

                    }
                    NegotiationFirstStage negotiationFirstStage;

                    if (lastNegotiation != null)
                    {

                        var attachments = _mapper.Map<List<NegotiationAttachmentViewModel>>(lastNegotiation.Attachments);
                        negotiationFirstStage = new NegotiationFirstStage(lastNegotiation.SupplierReplyPeriodHours, lastNegotiation.DiscountLetterRefID, lastNegotiation.DiscountAmount, agencyCommunicationRequest.AgencyRequestId, lastNegotiation.NegotiationReasonId, attachments, (int)Enums.enNegotiationStatus.UnderUpdate, "");
                    }
                    else
                    {
                        negotiationFirstStage = new NegotiationFirstStage(0, "", 0, agencyCommunicationRequest.AgencyRequestId, null, new List<NegotiationAttachmentViewModel>(), (int)Enums.enNegotiationStatus.UnderUpdate, "");
                    }

                    negotiationFirstStage = await _negotaitionCommands.CreateNegotiationFirstStageAsync(negotiationFirstStage);
                    return Util.Encrypt(negotiationFirstStage.NegotiationId);

                case Enums.enNegotiationType.SecondStage:
                    if (lastNegotiation == null || lastNegotiation.StatusId != (int)Enums.enNegotiationStatus.SupplierNotAgreed)
                        throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.TenderMusthasNotAgreedNegotaition);
                    if (lastNegotiation.NegotiationReasonId != (int)Enums.NegotiationFirstStageRejectionReasons.HighPriceThanBudget)
                    {
                        throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.FirstStageNegotiationReasonNotBudget);

                    }

                    var SecondStageNegotiation = await _communicationQueries.GetNegotiationSecondStageTenderId(Util.Decrypt(model.TenderIdString));
                    if (SecondStageNegotiation != null)
                        throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.SecondStageNegotiationAlreadyExist);


                    var AgencyRequests = Tender.AgencyCommunicationRequests.Where(w => w.IsActive == true && w.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Negotiation).ToList();
                    var offer = await _offerQueries.GetTheLowestOffer(Tender.TenderId);
                    if (offer == null)
                        throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.NoOffers);
                    if (!AgencyRequests.Any())
                    {
                        agencyCommunicationRequest = new AgencyCommunicationRequest(Tender.TenderId, (int)Enums.AgencyCommunicationRequestType.Negotiation, (int)Enums.AgencyCommunicationRequestStatus.UnderUpdate, RoleNames.OffersCheckSecretary);
                        Tender.AddAgencyCommunicationRequest(agencyCommunicationRequest);
                        Tender = await _tenderCommands.UpdateAsync(Tender);
                    }
                    else
                    {
                        agencyCommunicationRequest = AgencyRequests.FirstOrDefault();
                    }
                    var negotiationSecondStage = new NegotiationSecondStage(agencyCommunicationRequest.AgencyRequestId, null, lastNegotiation.NegotiationId, offer.OfferId);

                    negotiationSecondStage.AddSupplierQuantityTables(offer.SupplierTenderQuantityTables);
                    negotiationSecondStage = await _negotaitionCommands.CreateNegotiationSecondStageAsync(negotiationSecondStage);
                    #region Notifications
                    #endregion

                    return Util.Encrypt(negotiationSecondStage.NegotiationId);
                default:
                    return "";
            }
        }

        private void IsValidToCreateFirstStageNegotiation(NegotiationFirstStage lastNego)
        {
            if (lastNego.StatusId == (int)Enums.enNegotiationStatus.UnderUpdate)
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.NegotiationRequestUnderProcessing);

            if ((lastNego.StatusId == (int)Enums.enNegotiationStatus.SupplierAgreed || lastNego.StatusId == (int)Enums.enNegotiationStatus.SupplierAgreedWithExtraDiscount) && !lastNego.NegotiationFirstStageSuppliers.Any(s => s.IsActive == true && s.NegotiationSupplierStatusId == (int)Enums.enNegotiationSupplierStatus.NotSent))
                throw new BusinessRuleException("جميع الموردين تم إرسال لهم طلب تفاوض بالفعل");

            if (lastNego.StatusId != (int)Enums.enNegotiationStatus.SupplierAgreed && lastNego.StatusId != (int)Enums.enNegotiationStatus.SupplierAgreedWithExtraDiscount)
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.FirstStageNegotiationAlreadyExist);
        }

        private void CheckValidationToAddNegotiation(Tender Tender, int? committeeId)
        {

            if (committeeId.HasValue && committeeId != _httpContextAccessor.HttpContext.User.SelectedCommittee())
            {
                throw new BusinessRuleException("غير مسموح  ");

            }
            if (Tender.TenderTypeId == (int)Enums.TenderType.Competition || Tender.TenderTypeId == (int)Enums.TenderType.ReverseBidding)
            {
                throw new BusinessRuleException("غير مسموح بالتفاوض  لهذه المنافسة");
            }

        }

        public async Task<SecondStageNegotiationModelcs> GetSecondStageNegotiation(int NegotiationId)
        {
            SecondStageNegotiationModelcs model = await _negotiationQueries.FindNegotiationSecondStagebyId(NegotiationId);
            var cr = _httpContextAccessor.HttpContext.User.SupplierNumber();
            if (_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.supplier) && model.CR != cr)
            {
                throw new AuthorizationException();
            }
            return model;
        }
        public async Task<SecondStageNegotiationModelcs> GetSecondNegotiation(int NegotiationId)
        {

            SecondStageNegotiationModelcs model = await _negotiationQueries.FindNegotiationSecondbyId(NegotiationId);
            if (_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.supplier) && (model.CR != _httpContextAccessor.HttpContext.User.SupplierNumber()))
            {
                throw new BusinessRuleException("غير مسموح بعرض بيانات التفاوض");

            }
            if (_httpContextAccessor.HttpContext.User.SupplierNumber() != "" && model.tenderStatusId != (int)Enums.TenderStatus.OffersAwarding)
            {
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.TenderNotinOfferAwarding);
            }
            var Tender = await _tenderQueries.FindTenderByTenderId(Util.Decrypt(model.TenderIdString));
            int? committeeId = Tender.OffersCheckingCommitteeId;
            if (Tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || Tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            {
                committeeId = Tender.DirectPurchaseCommitteeId;
            }
            var IsCommiteeUser = _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersCheckManager) || _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersCheckSecretary) || _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersPurchaseManager) || _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersPurchaseSecretary);
            if (IsCommiteeUser && committeeId.HasValue && committeeId != _httpContextAccessor.HttpContext.User.SelectedCommittee())
            {
                throw new BusinessRuleException("غير مسموح  ");

            }
            model.SupplierTenderMainInfo = await _negotiationQueries.FindTenderDetailsForSecondNegotiation(Tender.TenderId);
            return model;
        }
        public async Task<SecondStageNegotiationModelcs> GetSecondNegotiation_NEW(int NegotiationId)
        {
            bool ispurchase = false;
            var userId = _httpContextAccessor.HttpContext.User.UserId();
            SecondStageNegotiationModelcs model = await _negotiationQueries.FindNegotiationSecondbyId(NegotiationId, userId);
            if (_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.supplier) && (model.CR != _httpContextAccessor.HttpContext.User.SupplierNumber()))
            {
                throw new BusinessRuleException("غير مسموح بعرض بيانات التفاوض");

            }
            if (_httpContextAccessor.HttpContext.User.SupplierNumber() != "" && model.tenderStatusId != (int)Enums.TenderStatus.OffersAwarding)
            {
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.TenderNotinOfferAwarding);
            }
            var Tender = await _tenderQueries.FindTenderByTenderId(Util.Decrypt(model.TenderIdString));
            int? committeeId = Tender.OffersCheckingCommitteeId;
            if (Tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || Tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            {
                ispurchase = true;
                committeeId = Tender.DirectPurchaseCommitteeId;
            }
            var IsCommiteeUser = _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersCheckManager) || _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersCheckSecretary) || _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersPurchaseManager) || _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersPurchaseSecretary);
            if (IsCommiteeUser && committeeId.HasValue && committeeId != _httpContextAccessor.HttpContext.User.SelectedCommittee())
            {
                throw new BusinessRuleException("غير مسموح  ");

            }
            model.SupplierTenderMainInfo = await _negotiationQueries.FindTenderDetailsForSecondNegotiation(Tender.TenderId);

            var isUserHasAccessToTender = (Tender.IsLowBudgetDirectPurchase.HasValue && Tender.IsLowBudgetDirectPurchase.Value && Tender.DirectPurchaseMemberId == userId);
            bool isEditable = ((_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersCheckSecretary) || _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersPurchaseSecretary) || isUserHasAccessToTender)
                && model.StatusId == (int)enNegotiationStatus.UnderUpdate);

            model.QuantitiesTemplateModel = await GetNegotiationQuantityItemsForSecondNegotiation(Tender.TenderId, NegotiationId, !isEditable);
            return model;
        }
        public async Task<QuantitiesTemplateModel> GetNegotiationQuantityItemsForSecondNegotiation(int tenderId, int negotiationId, bool isReadOnly)
        {

            QuantitiesTemplateModel lst = await _negotiationQueries.FindNegotiationQuantityItemsForSecondNegotiation(negotiationId);

            var tables = await _negotiationQueries.FindTableHtmlTemplatebyNegotiationId(negotiationId, tenderId);
            if (lst == null)
                lst = new QuantitiesTemplateModel();
            lst.negotiationId = negotiationId;
            FormConfigurationDTO DTOModel = new FormConfigurationDTO() { TenderId = tenderId, FormIds = lst.FormIds, QuantityItemDtos = lst.QuantitiesItems.ToList(), YearsCount = lst.TemplateYears == null ? 0 : lst.TemplateYears.Value };
            ApiResponse<List<HtmlTemplateDto>> obj = await _quantityTemplatesProxy.GetNegotiationGOVTemplates(DTOModel);
            if (obj.Data != null)
            {
                lst.grids.AddRange(obj.Data.Where(f => tables.Select(t => t.FormId).Contains(f.FormId)).GroupBy(o => new { o.FormId, o.FormName, o.TemplateName, o.FormExcellTemplate }).Select(o => new HTMLObject
                {
                    FormId = o.Key.FormId,
                    FormName = o.Key.FormName,
                    TemplateName = o.Key.TemplateName,
                    gridTables = tables.Where(f => f.FormId == o.Key.FormId).ToList()
                }).ToList());
            }
            lst.IsReadOnly = isReadOnly;
            return lst;
        }
        public async Task<QueryResult<TableModel>> GetNegotiationTableQuantityItems(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            var cellsCount = 0;
            if (quantityTableSearchCretriaModel.QuantityTableId == 0)
            {
                var firstQTTableId = await _negotiationQueries.FindFirstQTId(quantityTableSearchCretriaModel.negotiationId, quantityTableSearchCretriaModel.FormId);
                quantityTableSearchCretriaModel.QuantityTableId = firstQTTableId;
            }
            if (quantityTableSearchCretriaModel.QuantityTableId == 0)
            {
                throw new BusinessRuleException("لا يوجد بيانات");
            }
            cellsCount = await _negotiationQueries.GetNegotaitionTableQuantityItems(quantityTableSearchCretriaModel.QuantityTableId);
            if (cellsCount == 0)
            {
                throw new BusinessRuleException("الجدول غير موجود");
            }
            quantityTableSearchCretriaModel.CellsCount = cellsCount;
            var quantityItems = await _negotiationQueries.GetSupplierQTableItemsForNegotiation(quantityTableSearchCretriaModel);
            QuantitiesTemplateModel lst = await _negotiationQueries.GetNegotiationQuantityTableTemplateForNegotiation(quantityTableSearchCretriaModel.TenderId, quantityTableSearchCretriaModel.OfferId);
            lst.QuantitiesItems = quantityItems.Items.ToList();
            if (lst == null)
                lst = new QuantitiesTemplateModel();
            lst.grid = new List<string>();
            if (lst.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || lst.TenderTypeId == (int)Enums.TenderType.CurrentTender || lst.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                lst.ActivityTemplates = new List<int> { (int)TenderActivityTamplate.OldSystemAndVRO };
            }
            foreach (var item in lst.ActivityTemplates)
            {
                FormConfigurationDTO DTOModel = new FormConfigurationDTO()
                {
                    SubmitAction = "/Offer/SaveCheckingQuantityItem",
                    TenderId = quantityTableSearchCretriaModel.TenderId,
                    HasAlternative = false,
                    ApplySelected = false,
                    CanEditAlternative = false,
                    AllowEdit = false,
                    ActivityId = 1,
                    EncryptedOfferId = Util.Encrypt(quantityTableSearchCretriaModel.OfferId),
                    EncryptedTenderId = Util.Encrypt(quantityTableSearchCretriaModel.TenderId),
                    EncryptedNegotiationId = Util.Encrypt(quantityTableSearchCretriaModel.negotiationId),
                    QuantityItemDtos = lst.QuantitiesItems.ToList(),
                    YearsCount = lst.TemplateYears ?? 0
                };
                ApiResponse<List<HtmlTemplateDto>> resultList;
                resultList = quantityTableSearchCretriaModel.IsReadOnly ? await _quantityTemplatesProxy.GenerateNegotiationReadOnlyTemplate(DTOModel) : await _quantityTemplatesProxy.GenerateNegotiationTemplate(DTOModel);
                ApiResponse<List<HtmlTemplateDto>> obj = new ApiResponse<List<HtmlTemplateDto>> { Data = resultList.Data };
                if (obj.Data != null && obj.Data.Count > 0 && obj.Data[0] != null)
                {
                    lst.grid.AddRange(obj.Data.Select(a => a.FormHtml).ToList());
                    lst.grids.AddRange(obj.Data.GroupBy(o => new { o.FormId, o.FormName, o.TemplateName, o.FormExcellTemplate })
                        .Select(o => new HTMLObject
                        {
                            FormId = o.Key.FormId,
                            FormName = o.Key.FormName,
                            TemplateName = o.Key.TemplateName,
                            Javascript = o.FirstOrDefault().JsScript,

                            gridTables = o.Select(u => new TableModel { TableHtml = u.FormHtml, TableId = u.TableId, TableName = !string.IsNullOrEmpty(u.TableName) ? u.TableName : "اسم الجدول", FormId = u.FormId, DeleteFormHtml = u.DeleteFormHtml, EditFormHtml = u.EditFormHtml, Javascript = u.JsScript, IsTableHasAlternative = u.IsTableHasAlternative }).ToList()
                        }).ToList());
                }
            }
            lst.IsReadOnly = quantityTableSearchCretriaModel.IsReadOnly;
            return new QueryResult<TableModel>(lst.grids[0].gridTables.ToList(), quantityItems.TotalCount, quantityTableSearchCretriaModel.PageNumber, quantityTableSearchCretriaModel.PageSize * cellsCount);
        }

        public async Task<bool> SaveSecondStageNegotaion(SecondStageNegotiationModel NegotiationObj, List<NegotiationQuantityTableModel> QuantityTable, bool IsSend)
        {

            var secondStageNegotiation = await _negotiationQueries.FindSecondStageNegotiationbyId(Util.Decrypt(NegotiationObj.NegotiationId));
            secondStageNegotiation.UpdateNegotiationSecondStage(NegotiationObj.NegotiationReasonId, (IsSend ? (int)Enums.enNegotiationStatus.CheckManagerPendingApprove : (int)Enums.enNegotiationStatus.UnderUpdate));
            await _negotaitionCommands.UpdateNegotiationSecondStageAsync(secondStageNegotiation);
            return true;

        }


        #endregion

        #region Extend offer presentation dates request
        public async Task CreateSupplierExtendOfferDatesAgencyRequest(ExtendOffersDateRequestModel extendOffersDateRequestModel)
        {
            if (string.IsNullOrEmpty(extendOffersDateRequestModel.RequestReason))
                throw new BusinessRuleException("الرجاء ادخال سبب تقديم الطلب");

            Check.ArgumentNotNullOrEmpty(nameof(extendOffersDateRequestModel.TenderId), extendOffersDateRequestModel.TenderId.ToString());
            Check.ArgumentNotNull(nameof(extendOffersDateRequestModel.RequestReason), extendOffersDateRequestModel.RequestReason);
            Check.ArgumentNotNull(nameof(extendOffersDateRequestModel.RequestedDate), extendOffersDateRequestModel.RequestedDate.ToString());
            Tender tender = await _offerQueries.GetTenderbyTenderIdAsync(extendOffersDateRequestModel.TenderId);
            var supplierRequest = await _communicationQueries.FindCommunicationRequestByTenderIdAndCr(extendOffersDateRequestModel.TenderId, extendOffersDateRequestModel.Cr);
            var request = new AgencyCommunicationRequest(extendOffersDateRequestModel.TenderId,
            (int)Enums.AgencyCommunicationRequestType.SupplierOfferExtendDates,
            (int)Enums.AgencyCommunicationRequestStatus.RequestSent, RoleNames.supplier);
            await _communicationDomainService.IsValidToCreateRequest(supplierRequest, extendOffersDateRequestModel.RequestedDate, tender);
            request = await _communicationCommands.CreateAsync(request);
            SupplierExtendOfferDatesRequest supplierExtendOfferDatesRequest = new SupplierExtendOfferDatesRequest(extendOffersDateRequestModel.RequestReason, extendOffersDateRequestModel.RequestedDate, request.AgencyRequestId, extendOffersDateRequestModel.Cr);
            supplierExtendOfferDatesRequest = await _communicationCommands.CreateSupplierExtendOfferDatesRequestAsync(supplierExtendOfferDatesRequest);
            request.SetSupplierExtendOfferDates(supplierExtendOfferDatesRequest.SupplierExtendOfferDatesRequestId);
            await _communicationCommands.UpdateAgencytRequestAsync(request);

            NotificationArguments NotificationArguments = new NotificationArguments
            {

                BodyEmailArgs = new object[] { tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };

            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
            $"CommunicationRequest/SupplierExtendOfferDates/{Util.Encrypt(request.AgencyRequestId)}",
            NotificationEntityType.Tender,
             tender.TenderId.ToString(), tender.BranchId);
            if (tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.PurshaseSpecialist.AgencyCommunicationRequest.SendOffersPostponeRequest, tender.BranchId, mainNotificationTemplateModel);
            else if (tender.TenderTypeId == (int)Enums.TenderType.PostQualification || tender.TenderTypeId == (int)Enums.TenderType.PreQualification)
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.PreQualificationSecretary.EditOperationsOnQualification.SendOffersPostponeRequestForQulification, tender.PreQualificationCommitteeId, mainNotificationTemplateModel);
            else
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.AgencyCommunicationRequest.SendOffersPostponeRequest, tender.BranchId, mainNotificationTemplateModel);

        }

        public async Task<AgencyCommunicationRequest> ApproveSupplierExtendOfferDatesRequest(int SupplierExtendDatesAgencyCommunicationRequestId)
        {
            Check.ArgumentNotNull(nameof(SupplierExtendDatesAgencyCommunicationRequestId), SupplierExtendDatesAgencyCommunicationRequestId.ToString());
            AgencyCommunicationRequest agencyCommunicationRequest = await _communicationQueries.GetAgencyCommunicationRequestForSupplierExtendDatesRequest(SupplierExtendDatesAgencyCommunicationRequestId);
            _communicationDomainService.IsValidToApproveSupplierExtendOfferDatesRequest(agencyCommunicationRequest);
            agencyCommunicationRequest.UpdateSupplierOfferExtendDatesRequestStatus((int)Enums.AgencyCommunicationRequestStatus.UnderRevision);
            return await _communicationCommands.UpdateAsync(agencyCommunicationRequest);
        }

        public async Task<AgencyCommunicationRequest> RejectSupplierExtendOfferDatesRequest(int SupplierExtendDatesAgencyCommunicationRequestId)
        {
            Check.ArgumentNotNull(nameof(SupplierExtendDatesAgencyCommunicationRequestId), SupplierExtendDatesAgencyCommunicationRequestId.ToString());
            AgencyCommunicationRequest agencyCommunicationRequest = await _communicationQueries.GetAgencyCommunicationRequestForSupplierExtendDatesRequest(SupplierExtendDatesAgencyCommunicationRequestId);
            string cr = agencyCommunicationRequest.SupplierExtendOfferDatesRequest.CR;
            _communicationDomainService.IsValidToRejectSupplierExtendOfferDatesRequest(agencyCommunicationRequest);
            agencyCommunicationRequest.UpdateSupplierOfferExtendDatesRequestStatus((int)Enums.AgencyCommunicationRequestStatus.Rejected);


            List<string> suppliers = new List<string>();
            suppliers.Add(cr);
            NotificationArguments NotificationArgumentsforSupplier = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", agencyCommunicationRequest.Tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { "", agencyCommunicationRequest.Tender.ReferenceNumber },
                SMSArgs = new object[] { "", agencyCommunicationRequest.Tender.ReferenceNumber }
            };

            MainNotificationTemplateModel mainNotificationTemplateModelforSupplier = new MainNotificationTemplateModel(NotificationArgumentsforSupplier,
            $"CommunicationRequest/SupplierExtendOfferDates/{Util.Encrypt(agencyCommunicationRequest.AgencyRequestId)}",
            NotificationEntityType.Tender,
            agencyCommunicationRequest.Tender.TenderId.ToString(), null, agencyCommunicationRequest.Tender.OffersCheckingCommitteeId);
            if (agencyCommunicationRequest.Tender.TenderTypeId == (int)Enums.TenderType.PreQualification || agencyCommunicationRequest.Tender.TenderTypeId == (int)Enums.TenderType.PostQualification)
            {
                await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.AgencyCommunicationRequest.RejectQualificationPostponeRequest, suppliers, mainNotificationTemplateModelforSupplier);
            }
            else
                await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.AgencyCommunicationRequest.RejectOffersPostponeRequest, suppliers, mainNotificationTemplateModelforSupplier);
            var result = await _communicationCommands.UpdateAsync(agencyCommunicationRequest);
            return result;
        }

        public async Task<SupplierExtendOfferDatesAgencyRequestModel> FindSupplierExtendOfferDatesAgencyRequestRequestById(int agencyRequestId)
        {
            return await _communicationQueries.FindSupplierExtendOfferDatesRequestById(agencyRequestId);
        }

        public async Task<List<SupplierExtendOfferDatesAgencyRequestModel>> FindSupplierExtendOfferDatesRequestsByTenderId(int tenderId)
        {
            return await _communicationQueries.FindSupplierExtendOfferDatesRequestsByTenderId(tenderId);
        }

        public async Task<ExtendOfferPresentationDatesModel> FindTenderExtendDatesByTenderId(int tenderId, int agencyRequestId)
        {
            var changeRequest = await _tenderQueries.GetChangeRequest(tenderId, (int)Enums.ChangeRequestType.ExtendDates);

            if (changeRequest == null)
            {
                return await _tenderQueries.FindTenderDatesForExtendDatesRequestByTenderId(tenderId, agencyRequestId);
            }
            else
            {
                return await _tenderQueries.FindTenderRevisionDateForExtendOffersRequestByTenderId(tenderId, agencyRequestId);
            }
        }

        public async Task<bool> CheckForVerificationCode(int tenderId, string verificationCodeString, int typeId)
        {
            int userId = _httpContextAccessor.HttpContext.User.UserId();
            var verificationCode = await _tenderQueries.FindVerificationCode(userId, typeId);
            _tenderDomainService.IsValidVerificationCode(verificationCode.ExpiredDate, verificationCode.VerificationCodeNo, verificationCodeString);
            return true;
        }

        #endregion

        #region Extend Offers Validity
        public async Task<ExtendOffersValidityModel> GetExtendOffersValidity(int agencyRequestId, int tenderId, int userId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(agencyRequestId), agencyRequestId.ToString());
            Check.ArgumentNotNullOrEmpty(nameof(userId), userId.ToString());
            Tender tender;
            if (tenderId == 0)
            {
                tender = await _communicationQueries.GetTenderByAgencyRequestId(agencyRequestId);
            }
            else
            {
                tender = await _tenderAppService.FindTenderByTenderId(tenderId);
            }
            if (!(tender.IsLowBudgetDirectPurchase.HasValue && tender.IsLowBudgetDirectPurchase.Value && tender.DirectPurchaseMemberId == userId))
            {
                if ((tender.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase && tender.OffersCheckingCommitteeId == null) || (tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase && (tender.DirectPurchaseCommitteeId == null)))
                {
                    throw new UnHandledAccessException();
                }
            }
            ExtendOffersValidityModel extendOffersValidityModel = await _communicationQueries.GetExtendOffersValidity(agencyRequestId, userId);
            if (extendOffersValidityModel == null)
            {
                extendOffersValidityModel = new ExtendOffersValidityModel
                {
                    StatusId = tender.TenderStatusId,
                    TenderIdString = Util.Encrypt(tender.TenderId),
                    ExtendOffersValiditySavingModel = CheckExtendOffersValiditySavingModel(tender, userId),
                    TenderBasicInfoModel = await _communicationQueries.GetTenderBasicInfoModel(tender.TenderId),
                    AgencyRequestStatusId = 0,
                };
                var date = DateTime.Now;
                var endTime = _rootConfiguration.OfferTimesConfiguration.EndOfferTime - 12;
                var endTimeForVactionDays = _rootConfiguration.OfferTimesConfiguration.EndOfferVacationTime - 12;
                bool isVacationDayFordailyTime = (date.DayOfWeek.ToString() == "Saturday" || date.DayOfWeek.ToString() == "Friday") ? true : false;
                if (isVacationDayFordailyTime)
                {
                    extendOffersValidityModel.ExtendOffersValiditySavingModel.ReplyReceivingDurationTime = endTimeForVactionDays + ":00 PM";
                }
                else
                {
                    extendOffersValidityModel.ExtendOffersValiditySavingModel.ReplyReceivingDurationTime = endTime + ":00 PM";

                }
            }

            return extendOffersValidityModel;
        }

        private static ExtendOffersValiditySavingModel CheckExtendOffersValiditySavingModel(Tender tender, int userId)
        {
            ExtendOffersValiditySavingModel extendOffersValiditySavingModel;
            if (tender.IsLowBudgetDirectPurchase.HasValue && tender.IsLowBudgetDirectPurchase.Value && tender.DirectPurchaseMemberId == userId)
            {
                extendOffersValiditySavingModel = new ExtendOffersValiditySavingModel { TenderIdString = Util.Encrypt(tender.TenderId), AgencyRequestStatusId = (int)Enums.AgencyCommunicationRequestStatus.UnderUpdate, IsLowBudgetDirectPurchase = true };
            }
            else
            {
                extendOffersValiditySavingModel = new ExtendOffersValiditySavingModel { TenderIdString = Util.Encrypt(tender.TenderId), AgencyRequestStatusId = (int)Enums.AgencyCommunicationRequestStatus.UnderUpdate };
            }
            return extendOffersValiditySavingModel;
        }

        public async Task<QueryResult<ExtendOffersGridModel>> GetTenderOffersPagingAsync(int tenderId, int extendOfferValidityId, int extendofferValidityStatusId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            QueryResult<ExtendOffersGridModel> extendOffersGridModel;
            if (extendOfferValidityId == 0)
            {
                extendOffersGridModel = await _communicationQueries.GetOffersForExtendOfferValidityCreation(tenderId);
            }
            else
            {
                extendOffersGridModel = await _communicationQueries.GetTenderOffersPagingAsync(tenderId, extendOfferValidityId);
            }

            NegotiationSecondStage negotiation = await _negotiationQueries.FindNegotiationSecondStageByTenderId(tenderId);
            if (negotiation != null)
            {
                bool isOfferExist = extendOffersGridModel.Items.Any(d => d.OfferId == negotiation.OfferId);
                if (isOfferExist && negotiation.StatusId != (int)enNegotiationStatus.New && negotiation.StatusId != (int)enNegotiationStatus.SupplierAgreed)
                {
                    var tender = await _tenderQueries.FindTenderByTenderId(tenderId);
                    var tables = negotiation.negotiationSupplierQuantitiestable.Where(d => d.NegotiationQuantityItemJson != null).Select(t => new QuantitiesTemplateModel
                    {
                        QuantitiesItems = t.NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems.Select(i => new TenderQuantityItemDTO
                        {
                            ColumnId = i.ColumnId,
                            Id = i.Id,
                            TableId = t.Id,
                            TenderFormHeaderId = i.TenderFormHeaderId,
                            Value = i.Value,
                            ItemNumber = i.ItemNumber,
                            IsDefault = true,
                            TemplateId = i.ActivityTemplateId
                        }).ToList(),
                    }).ToList();
                    var items = tables.SelectMany(x => x.QuantitiesItems).ToList();

                    FormConfigurationDTO DTOModel = new FormConfigurationDTO()
                    {
                        NPpercentage = (tender.NationalProductPercentage ?? 0) / 100,
                        EncryptedOfferId = Util.Encrypt(negotiation.Offer.OfferId),
                        EncryptedTenderId = Util.Encrypt(negotiation.Offer.TenderId),
                        EncryptedNegotiationId = Util.Encrypt(negotiation.NegotiationId),
                        TenderId = negotiation.OfferId,
                        ActivityId = 1,
                        QuantityItemDtos = items,
                        YearsCount = (tender.TemplateYears.HasValue ? tender.TemplateYears.Value : 0)
                    };
                    ApiResponse<TotalCostDTO> offerFinalPriceResponse = await _quantityTemplatesProxy.CalculateOfferFinalPricebyItems(DTOModel);
                    extendOffersGridModel.Items.FirstOrDefault().OfferPriceNP = offerFinalPriceResponse.Data.TotalCostNP;
                    extendOffersGridModel.Items.FirstOrDefault().OfferPrice = offerFinalPriceResponse.Data.TotalCostWithdiscount;
                }
            }
            return extendOffersGridModel;
        }

        public async Task<ExtendOffersDisplayFilesModel> GetOfferToExtendbyId(int offerid, int userId)
        {
            ExtendOffersDisplayFilesModel extendOffersDisplayFilesModel = await _communicationQueries.GetOfferToExtendbyId(offerid, userId);
            if (extendOffersDisplayFilesModel == null)
                throw new UnHandledAccessException();
            return extendOffersDisplayFilesModel;
        }
        public async Task<OfferNegotiationFileModel> GetOfferFilesbyId(int offerid)
        {

            OfferNegotiationFileModel offerNegotiationFileModel = await _communicationQueries.GetOfferFilesbyId(offerid);
            if (offerNegotiationFileModel == null)
                throw new UnHandledAccessException();
            return offerNegotiationFileModel;
        }
        public async Task<OfferDetailModel> GetOfferDetails(int combinedId, int userId)
        {
            OfferDetailModel result = await _communicationQueries.GetOfferDetails(combinedId);
            return result;
        }
        public async Task<QueryResult<CombinedSupplierModel>> GetCombinedSuppliers(int offerid, int userId)
        {
            QueryResult<CombinedSupplierModel> combinedSuppliers = await _communicationQueries.GetCombinedSuppliers(offerid, userId);
            if (combinedSuppliers.Items.Count() == 0)
            {
                throw new UnHandledAccessException();
            }
            return combinedSuppliers;
        }
        public async Task<CombinedSupplierModel> GetTenderOfferIDsByOfferID(int offerId)
        {
            return await _communicationQueries.GetTenderOfferIDsByOfferID(offerId);
        }
        public async Task<AgencyCommunicationRequest> GetCommunicationRequestByRequestId(int communcicationRequestId)
        {

            var result = await _communicationQueries.GetAgencyCommunicationRequestById(communcicationRequestId);
            return result;
        }

        // Create extend offer validity Request by check/purchase secretary
        public async Task<string> AddExtendOffersValidity(ExtendOffersValiditySavingModel model)
        {
            await IsValidToCreateExtendOfferValidityRequest(model);
            AgencyCommunicationRequest request = null;
            string agencyCommunicationRequest;
            if (model.AgencyRequestId == 0)
            {
                var tenderHasValidExtendOffersValidity = await _communicationQueries.GetTenderHasValidExtendOffersValidity(Util.Decrypt(model.TenderIdString));
                if (tenderHasValidExtendOffersValidity.TenderHasExtendOffersValidity && tenderHasValidExtendOffersValidity != null)
                {
                    throw new BusinessRuleException("هناك طلب تمديد غير منتهي لا يمكن انشاء طلب اخر");
                }
                request = new AgencyCommunicationRequest(model.AgencyRequestId, Util.Decrypt(model.TenderIdString),
                            (int)Enums.AgencyCommunicationRequestType.ExtendOfferValidtiy,
                            (int)Enums.AgencyCommunicationRequestStatus.UnderUpdate, RoleNames.OffersCheckSecretary);

                request.AddAgencyCommunicationRequestValidity(model.ExtendOffersValidityId, model.OffersDuration, model.ExtendOffersReason, model.ReplyReceivingDurationDays.Value, model.ReplyReceivingDurationTime);

                List<Offer> offers = await _communicationQueries.GetOffersToSendExtendOfferValidityByTenderId(request.TenderId);
                if (offers.Any())
                {
                    request = await _communicationCommands.CreateAsync(request);
                    await AddExtendOffersValiditySupplier(request, offers);
                }
                else
                {
                    throw new BusinessRuleException("لا يمكن انشاء طلب جديد لأن جميع الموردين تم استباعدهم من المنافسة");
                }
            }
            else
            {

                AgencyCommunicationRequest Request = await _communicationQueries.FindAgencyCommunicationRequestById(model.AgencyRequestId);
                request = new AgencyCommunicationRequest(model.AgencyRequestId, Request.TenderId,
                                (int)Enums.AgencyCommunicationRequestType.ExtendOfferValidtiy,
                                (int)Enums.AgencyCommunicationRequestStatus.UnderUpdate, RoleNames.OffersCheckSecretary);
                request.AddAgencyCommunicationRequestValidity(model.ExtendOffersValidityId, model.OffersDuration, model.ExtendOffersReason, model.ReplyReceivingDurationDays.Value, model.ReplyReceivingDurationTime);
                request = await _communicationCommands.UpdateAsync(request);
            }
            agencyCommunicationRequest = Util.Encrypt(request.AgencyRequestId);
            return agencyCommunicationRequest;
        }

        private async Task AddExtendOffersValiditySupplier(AgencyCommunicationRequest request, List<Offer> offers)
        {
            List<string> suppliers = offers.Select(e => e.Supplier.SelectedCr).Distinct().ToList();
            List<string> validSuppliers = new List<string>();
            List<Offer> validOffers = new List<Offer>();
            foreach (var cr in suppliers)
            {
                bool isSupplierFailedInPostqualification = await _communicationDomainService.IsSupplierFailedInPostqualification(request.TenderId, cr);
                if (!isSupplierFailedInPostqualification)
                {
                    validSuppliers.Add(cr);
                    Offer offer = offers.FirstOrDefault(o => o.CommericalRegisterNo == cr);
                    validOffers.Add(offer);
                }
            }
            for (int i = 0; i < validOffers.Count; i++)
            {
                ExtendOffersValiditySupplier extendOffersValiditySupplier = new ExtendOffersValiditySupplier((int)Enums.SupplierExtendOffersValidityStatus.Sent,
                    null, validOffers[i].OfferId, validOffers[i].CommericalRegisterNo, request.ExtendOffersValidity.ExtendOffersValidityId);
                _genericCommandRepository.Update(extendOffersValiditySupplier);
            }
            await _genericCommandRepository.SaveAsync();
        }
        private async Task IsValidToCreateExtendOfferValidityRequest(ExtendOffersValiditySavingModel model)
        {
            Check.ArgumentNotNull(nameof(model), model);

            bool hasPostQualification = await _communicationDomainService.IsTenderHasActivePostqualification(Util.Decrypt(model.TenderIdString));
            if (hasPostQualification)
            {
                throw new BusinessRuleException("لا يمكن اضافة طلب تمديد سريان لأن هذه المنافسة عليها تأهيل لاحق غير منتهي");
            }

            var totalOffersDuration = await _communicationQueries.GetExtendOffersValidityDaysByTenderId(Util.Decrypt(model.TenderIdString));

            if (model.OffersDuration + totalOffersDuration > 90)
            {
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.MustNotExceed90Days);
            }

            if (model.ReplyReceivingDurationDays > model.OffersDuration)
            {
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.RecivingReplayDaysMustBeLessthanOffersDuration);
            }
            if (string.IsNullOrEmpty(model.ExtendOffersReason))
            {
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.RequiredExtendOffersReason);
            }
        }

        // Send extend offer validity Request to approve 
        public async Task SendToApproveExtendOffersRequest(int agencyRequestId)
        {
            AgencyCommunicationRequest request = await _communicationQueries.FindAgencyCommunicationRequestByIdForSendToApproval(agencyRequestId);
            await IsValidToSendExtendOffersRequestToApprove(request);
            ResetExeclusionReasonsAndAwardingValues(request);
            request.UpdateAgencyCommunicationRequestStatus((int)Enums.AgencyCommunicationRequestStatus.Pending, "");
            _genericCommandRepository.Update(request);
            await _genericCommandRepository.SaveAsync();
            await SendExtendoffersValidityToApproveNotifications(request);
        }
        private void ResetExeclusionReasonsAndAwardingValues(AgencyCommunicationRequest request)
        {
            List<Offer> offers = request.Tender.Offers.Where(o => o.OfferStatusId == (int)Enums.OfferStatus.Received && o.IsActive == true && (!string.IsNullOrEmpty(o.ExclusionReason) || o.PartialOfferAwardingValue != null || o.TotalOfferAwardingValue != null)).ToList();
            if (offers.Any())
            {
                foreach (var offer in offers)
                {
                    offer.ResetAwardingValue();
                    offer.RemoveExclusionReasonForSuppliers();
                    _genericCommandRepository.Update(offer);
                }
            }
        }

        private async Task SendExtendoffersValidityToApproveNotifications(AgencyCommunicationRequest request)
        {
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { request.Tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { request.Tender.ReferenceNumber },
                SMSArgs = new object[] { request.Tender.ReferenceNumber }
            };

            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
              $"CommunicationRequest/ExtendOffersValidity/{Util.Encrypt(request.AgencyRequestId)}",
              NotificationEntityType.Tender,
              request.Tender.TenderId.ToString(), null, request.Tender.OffersCheckingCommitteeId);

            MainNotificationTemplateModel templateModelDirectPurchase = new MainNotificationTemplateModel(NotificationArguments,
            $"CommunicationRequest/ExtendOffersValidity/{Util.Encrypt(request.AgencyRequestId)}",
            NotificationEntityType.Tender,
            request.Tender.TenderId.ToString(), null, request.Tender.DirectPurchaseCommitteeId);

            if (request.Tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseManager.AgencyCommunicationRequest.SendExtendOffersToApprove, request.Tender.DirectPurchaseCommitteeId, templateModelDirectPurchase);
            else
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckManager.AgencyCommunicationRequest.SendExtendOffersToApprove, request.Tender.OffersCheckingCommitteeId, mainNotificationTemplateModel);
        }
        private async Task IsValidToSendExtendOffersRequestToApprove(AgencyCommunicationRequest request)
        {
            if (request == null)
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.RequestNotFound);
            if (request.StatusId != (int)Enums.AgencyCommunicationRequestStatus.UnderUpdate)
            {
                MemberInfo property = typeof(Enums.TenderStatus).GetProperty(nameof(Enums.AgencyCommunicationRequestStatus.UnderUpdate));
                var RequestStatus = Resources.CommunicationRequest.Messages.ResourceManager.GetString(nameof(Enums.AgencyCommunicationRequestStatus.UnderUpdate));
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.CanNotProceedStatusIs + RequestStatus);
            }
            if (request.ExtendOffersValidity.NewOffersExpiryDate < DateTime.Now)
            {
                throw new BusinessRuleException("لا يمكن ارسال الطلب للاعتماد لأن تاريخ انتهاء مدة سريان العروض أقل من تاريخ اليوم");
            }
            if (request.Tender.AgencyCode != _httpContextAccessor.HttpContext.User.UserAgency())
            {
                throw new UnHandledAccessException();
            }
        }
        public async Task ApproveExtendOffersRequestAsync(int AgencyRequestId)
        {
            AgencyCommunicationRequest request = await _communicationQueries.FindAgencyCommunicationRequestByIdForApproval(AgencyRequestId);
            IsValidToApprovedExtendOffersRequest(request);
            ResetExeclusionReasonsAndAwardingValues(request);

            request.UpdateAgencyCommunicationRequestStatus((int)Enums.AgencyCommunicationRequestStatus.Approved, "");

            List<Offer> offers = await _communicationQueries.GetOffersToSendExtendOfferValidityByTenderId(request.TenderId);
            List<string> suppliers = offers.Select(e => e.Supplier.SelectedCr).Distinct().ToList();

            List<string> validSuppliers = new List<string>();
            List<Offer> validOffers = new List<Offer>();
            foreach (var cr in suppliers)
            {
                bool isSupplierFailedInPostqualification = await _communicationDomainService.IsSupplierFailedInPostqualification(request.TenderId, cr);
                if (!isSupplierFailedInPostqualification)
                {
                    validSuppliers.Add(cr);
                    Offer offer = offers.FirstOrDefault(o => o.CommericalRegisterNo == cr);
                    validOffers.Add(offer);
                }
            }
            if (!request.ExtendOffersValidity.ExtendOffersValiditySuppliers.Any())
            {
                for (int i = 0; i < validOffers.Count; i++)
                {
                    ExtendOffersValiditySupplier extendOffersValiditySupplier = new ExtendOffersValiditySupplier((int)Enums.SupplierExtendOffersValidityStatus.Sent,
                        null, validOffers[i].OfferId, validOffers[i].CommericalRegisterNo, request.ExtendOffersValidity.ExtendOffersValidityId);
                    await _communicationCommands.CreateExtendOffersValiditySupplier(extendOffersValiditySupplier);
                }
            }
            request.CreatedBy = request.CreatedBy?.Split('|')[0];
            await _communicationCommands.UpdateAsync(request);

            await SendNotificationAfterApproveExtendOfferValidity(request, validSuppliers);
        }

        private void IsValidToApprovedExtendOffersRequest(AgencyCommunicationRequest request)
        {
            if (request == null)
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.RequestNotFound);
            if (!(request.Tender.IsLowBudgetDirectPurchase.HasValue && request.Tender.IsLowBudgetDirectPurchase.Value) && request.StatusId != (int)Enums.AgencyCommunicationRequestStatus.Pending)
            {
                MemberInfo property = typeof(Enums.TenderStatus).GetProperty(nameof(Enums.AgencyCommunicationRequestStatus.Pending));
                var RequestStatus = Resources.CommunicationRequest.Messages.ResourceManager.GetString(nameof(Enums.AgencyCommunicationRequestStatus.Pending));
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.CanNotProceedStatusIs + RequestStatus);
            }
            if (request.ExtendOffersValidity.NewOffersExpiryDate < DateTime.Now)
            {
                throw new BusinessRuleException("لا يمكن اعتماد الطلب لأن تاريخ انتهاء مدة سريان العروض أقل من تاريخ اليوم");
            }
            if (request.Tender.AgencyCode != _httpContextAccessor.HttpContext.User.UserAgency())
            {
                throw new UnHandledAccessException();
            }
        }
        private async Task SendNotificationAfterApproveExtendOfferValidity(AgencyCommunicationRequest request, List<string> suppliers)
        {

            if (!(request.Tender.IsLowBudgetDirectPurchase.HasValue && request.Tender.IsLowBudgetDirectPurchase.Value))
            {
                NotificationArguments NotificationArguments = new NotificationArguments
                {
                    BodyEmailArgs = new object[] { request.Tender.ReferenceNumber },
                    SubjectEmailArgs = new object[] { },
                    PanelArgs = new object[] { request.Tender.ReferenceNumber },
                    SMSArgs = new object[] { request.Tender.ReferenceNumber }
                };

                MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
                $"CommunicationRequest/ExtendOffersValidity/{Util.Encrypt(request.AgencyRequestId)}",
                NotificationEntityType.Tender,
                request.Tender.TenderId.ToString(), null, request.Tender.OffersCheckingCommitteeId);
                MainNotificationTemplateModel purshaseTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
                $"CommunicationRequest/ExtendOffersValidity/{Util.Encrypt(request.AgencyRequestId)}",
                NotificationEntityType.Tender,
                request.Tender.TenderId.ToString(), null, request.Tender.DirectPurchaseCommitteeId);
                if (request.Tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseSecretary.AgencyCommunicationRequest.ApproveExtendOffers, request.Tender.DirectPurchaseCommitteeId, purshaseTemplateModel);
                else
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.AgencyCommunicationRequest.ApproveExtendOffers, request.Tender.OffersCheckingCommitteeId, mainNotificationTemplateModel);
            }

            NotificationArguments NotificationArgumentsforSupplier = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", request.Tender.TenderName, request.ExtendOffersValidity.ReplyReceivingDurationDays },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { "", request.Tender.TenderName, request.ExtendOffersValidity.ReplyReceivingDurationDays },
                SMSArgs = new object[] { "", request.Tender.TenderName, request.ExtendOffersValidity.ReplyReceivingDurationDays }
            };

            MainNotificationTemplateModel mainNotificationTemplateModelforSupplier = new MainNotificationTemplateModel(NotificationArgumentsforSupplier,
            $"CommunicationRequest/ExtendOffersValiditySupplier/{Util.Encrypt(request.AgencyRequestId)}",
            NotificationEntityType.Tender,
            request.Tender.TenderId.ToString(), null, request.Tender.OffersCheckingCommitteeId);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.AgencyCommunicationRequest.SendExtendOffersToSuppliers, suppliers, mainNotificationTemplateModelforSupplier);
        }

        public async Task RejectExtendOffersRequest(int AgencyRequestId, string RejectionReason)
        {
            AgencyCommunicationRequest request = await _communicationQueries.FindAgencyCommunicationRequestById(AgencyRequestId);
            _communicationDomainService.IsValidToRejectExtendOffersRequest(request);
            request.UpdateAgencyCommunicationRequestStatus((int)Enums.AgencyCommunicationRequestStatus.Rejected, RejectionReason);
            await _communicationCommands.UpdateAsync(request);
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { request.Tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { request.Tender.ReferenceNumber },
                SMSArgs = new object[] { request.Tender.ReferenceNumber }
            };

            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
            $"CommunicationRequest/ExtendOffersValidity/{Util.Encrypt(request.AgencyRequestId)}",
            NotificationEntityType.Tender,
            request.Tender.TenderId.ToString(), null, request.Tender.OffersCheckingCommitteeId);

            MainNotificationTemplateModel purshaseTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
            $"CommunicationRequest/ExtendOffersValidity/{Util.Encrypt(request.AgencyRequestId)}",
            NotificationEntityType.Tender,
            request.Tender.TenderId.ToString(), null, request.Tender.DirectPurchaseCommitteeId);

            if (request.Tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseSecretary.AgencyCommunicationRequest.RejectExtendOffers, request.Tender.DirectPurchaseCommitteeId, purshaseTemplateModel);
            else
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.AgencyCommunicationRequest.RejectExtendOffers, request.Tender.OffersCheckingCommitteeId, mainNotificationTemplateModel);
        }

        // Delete extend offer validity Request by check/purchase manager
        public async Task DeleteExtendOffersRequestAsync(int agencyRequestId)
        {
            AgencyCommunicationRequest request = await _communicationQueries.FindAgencyCommunicationRequestByIdForDelete(agencyRequestId);
            _communicationDomainService.IsValidToDeleteExtendOffersRequest(request);
            request.DeleteExtendOfferValidityRequests();
            await _communicationCommands.UpdateAsync(request);
        }

        public async Task<SupplierExtendOffersValidityViewModel> GetSupplierExtendOffersValidityForSupplier(int supplierExtendOffersValidtyId, string Cr)
        {
            var entity = await _communicationQueries.GetSupplierExtendOffersValidityForSupplier(supplierExtendOffersValidtyId, Cr);
            if (entity == null)
            {
                throw new UnHandledAccessException();
            }
            else if (entity.SupplierExtendOfferValidityStatusId == (int)Enums.SupplierExtendOffersValidityStatus.Sent)
            {
                entity.UpdateExtendOffersValiditySupplier((int)Enums.SupplierExtendOffersValidityStatus.UnderProcessing);
                await _communicationCommands.UpdateExtendOffersValiditySupplier(entity);
            }
            SupplierExtendOffersValidityViewModel model = await _communicationQueries.GetSupplierExtendOffersValidityForSupplierViewModel(supplierExtendOffersValidtyId, Cr);
            model.ExtendOffersValidityAttachementViewModel = await _communicationQueries.GetExtendOffersValidityAttachmentModel(Util.Decrypt(model.SupplierExtendOffersValidityId));
            return model;
        }

        public async Task AcceptExtendOffersValidityBySupplier(int supplierExtendOffersValidtyId, string supplierCr, string supplierName)
        {
            Check.ArgumentNotNullOrEmpty(nameof(supplierExtendOffersValidtyId), supplierExtendOffersValidtyId.ToString());
            ExtendOffersValiditySupplier ExtendOffersValiditySupplier = await _communicationQueries.GetExtendOffersValiditySupplierById(supplierExtendOffersValidtyId, supplierCr);
            _communicationDomainService.IsValidToAcceptExtendOffersValidityBySupplier(ExtendOffersValiditySupplier);
            ExtendOffersValiditySupplier.UpdateExtendOffersValiditySupplier((int)Enums.SupplierExtendOffersValidityStatus.AcceptedInitially);
            await _communicationCommands.UpdateExtendOffersValiditySupplier(ExtendOffersValiditySupplier);
            Tender tender = await _offerQueries.GetTenderbyTenderIdAsync(ExtendOffersValiditySupplier.ExtendOffersValidity.AgencyCommunicationRequest.TenderId);
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { supplierName, tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { supplierName, tender.ReferenceNumber },
                SMSArgs = new object[] { supplierName, tender.ReferenceNumber }
            };

            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
            $"CommunicationRequest/ExtendOffersValidity/{Util.Encrypt(ExtendOffersValiditySupplier.ExtendOffersValidity.AgencyCommunicationRequestId)}",
            NotificationEntityType.Tender,
             tender.TenderId.ToString(), null, tender.OffersCheckingCommitteeId);
            MainNotificationTemplateModel templateModelDirectPurchase = new MainNotificationTemplateModel(NotificationArguments,
                        $"CommunicationRequest/ExtendOffersValidity/{Util.Encrypt(ExtendOffersValiditySupplier.ExtendOffersValidity.AgencyCommunicationRequestId)}",
                        NotificationEntityType.Tender,
                         tender.TenderId.ToString(), null, tender.DirectPurchaseCommitteeId);

            if (tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            {
                if (tender.IsLowBudgetDirectPurchase.HasValue && tender.IsLowBudgetDirectPurchase.Value)
                {
                    UserProfile userProfile = await _iDMQueries.FindUserProfileByIdWithoutIncludesAsync(tender.DirectPurchaseMemberId.Value);
                    await _notificationAppService.AddNotificationSettingByUserId(NotificationOperations.DirectPurchaseMember.AgencyCommunicationRequest.AcceptExtendOffersValidityBySupplierForDirectPurchaseMember, userProfile, (int)Enums.UserRole.CR_DirectPurchaseMember);
                    await _notificationAppService.SendNotificationByUserId(NotificationOperations.DirectPurchaseMember.AgencyCommunicationRequest.AcceptExtendOffersValidityBySupplierForDirectPurchaseMember, tender.DirectPurchaseMemberId.Value, userProfile.UserName, templateModelDirectPurchase);
                }
                else
                {
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseManager.AgencyCommunicationRequest.AcceptExtendOffersValidityBySupplier, tender.DirectPurchaseCommitteeId, templateModelDirectPurchase);
                }
            }
            else
            {
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.AgencyCommunicationRequest.AcceptExtendOffersValidityBySupplier, tender.OffersCheckingCommitteeId, mainNotificationTemplateModel);
            }
        }

        public async Task AddInitialGuaranteeAttachmentToEXV(ExtendOffersValidityAttachementViewModel extendOffersValidityAttachementViewModel, int supplierExtendOffersValidityId, string suplierCR, string supplierName)
        {
            Check.ArgumentNotNullOrEmpty(nameof(extendOffersValidityAttachementViewModel), extendOffersValidityAttachementViewModel.ToString());
            Check.ArgumentNotNullOrEmpty(nameof(supplierExtendOffersValidityId), supplierExtendOffersValidityId.ToString());

            ExtendOffersValiditySupplier extendOffersValiditySupplier = await _communicationQueries.GetSupplierExtendOffersValidityForSupplierByExtendOffersValiditySupplierId(supplierExtendOffersValidityId, suplierCR);
            ExtendOffersValidityAttachment attachment = new ExtendOffersValidityAttachment(extendOffersValidityAttachementViewModel.Name, extendOffersValidityAttachementViewModel.FileNetReferenceId, (int)Enums.AttachmentType.InitialGuarantee, supplierExtendOffersValidityId);
            await _communicationCommands.AddExtendOffersValidityAttachment(attachment);
            ExtendOffersValidity extendOffersValidity = await _communicationQueries.GetExtendOffersValidityForAddInitialGuaranteeById(extendOffersValiditySupplier.ExtendOffersValidityId);
            extendOffersValiditySupplier.UpdateExtendOffersValiditySupplier((int)Enums.SupplierExtendOffersValidityStatus.Accepted);
            await _communicationCommands.UpdateExtendOffersValiditySupplier(extendOffersValiditySupplier);
            Tender tender = await _offerQueries.GetTenderbyTenderIdAsync(extendOffersValidity.AgencyCommunicationRequest.TenderId);
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { supplierName, tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { supplierName, tender.ReferenceNumber },
                SMSArgs = new object[] { supplierName, tender.ReferenceNumber }
            };

            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
            $"CommunicationRequest/ExtendOffersValidity/{Util.Encrypt(extendOffersValidity.AgencyCommunicationRequestId)}",
            NotificationEntityType.Tender,
             tender.TenderId.ToString(), null, tender.OffersCheckingCommitteeId);

            MainNotificationTemplateModel purshaseNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
            $"CommunicationRequest/ExtendOffersValidity/{Util.Encrypt(extendOffersValidity.AgencyCommunicationRequestId)}",
            NotificationEntityType.Tender,
             tender.TenderId.ToString(), null, tender.DirectPurchaseCommitteeId);

            if (tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            {
                if (tender.IsLowBudgetDirectPurchase.HasValue && tender.IsLowBudgetDirectPurchase.Value)
                {
                    UserProfile userProfile = await _iDMQueries.FindUserProfileByIdWithoutIncludesAsync(tender.DirectPurchaseMemberId.Value);
                    await _notificationAppService.AddNotificationSettingByUserId(NotificationOperations.DirectPurchaseMember.AgencyCommunicationRequest.SendInitialWarantyCopyToDirectPurchaseMember, userProfile, (int)Enums.UserRole.CR_DirectPurchaseMember);
                    await _notificationAppService.SendNotificationByUserId(NotificationOperations.DirectPurchaseMember.AgencyCommunicationRequest.SendInitialWarantyCopyToDirectPurchaseMember, tender.DirectPurchaseMemberId.Value, userProfile.UserName, purshaseNotificationTemplateModel);
                }
                else
                {
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseManager.AgencyCommunicationRequest.SendInitialWarantyCopy, tender.DirectPurchaseCommitteeId, purshaseNotificationTemplateModel);
                }
            }
            else
            {
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.AgencyCommunicationRequest.SendInitialWarantyCopy, tender.OffersCheckingCommitteeId, mainNotificationTemplateModel);
            }
        }

        public async Task RejectExtendOffersValidityBySupplier(int extendOffersValidtyId, string supplierCr, string SupplierName)
        {
            Check.ArgumentNotNullOrEmpty(nameof(extendOffersValidtyId), extendOffersValidtyId.ToString());

            ExtendOffersValiditySupplier ExtendOffersValiditySupplier = await _communicationQueries.GetExtendOffersValiditySupplierById(extendOffersValidtyId, supplierCr);
            _communicationDomainService.IsValidToRejectExtendOffersValidityBySupplier(ExtendOffersValiditySupplier);
            ExtendOffersValiditySupplier.UpdateExtendOffersValiditySupplier((int)Enums.SupplierExtendOffersValidityStatus.Rejected);
            ExtendOffersValiditySupplier = await _communicationCommands.UpdateExtendOffersValiditySupplier(ExtendOffersValiditySupplier);

            Offer offer = await _offerQueries.FindOfferWithQuantityTableByTenderIdAndCR(ExtendOffersValiditySupplier.ExtendOffersValidity.AgencyCommunicationRequest.TenderId, supplierCr);
            offer.UpdateStatus(Enums.OfferStatus.Excluded);
            await _offerCommands.UpdateAsync(offer);
            Tender tender = await _offerQueries.GetTenderbyTenderIdAsync(offer.TenderId);
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { SupplierName, tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { SupplierName, tender.ReferenceNumber },
                SMSArgs = new object[] { SupplierName, tender.ReferenceNumber }
            };
            MainNotificationTemplateModel PurshaseTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
                        $"CommunicationRequest/ExtendOffersValidity/{Util.Encrypt(ExtendOffersValiditySupplier.ExtendOffersValidity.AgencyCommunicationRequestId)}",
                        NotificationEntityType.Tender,
                         tender.TenderId.ToString(), null, tender.DirectPurchaseCommitteeId);
            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
            $"CommunicationRequest/ExtendOffersValidity/{Util.Encrypt(ExtendOffersValiditySupplier.ExtendOffersValidity.AgencyCommunicationRequestId)}",
            NotificationEntityType.Tender,
             tender.TenderId.ToString(), null, tender.OffersCheckingCommitteeId);

            if (tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            {
                if (tender.IsLowBudgetDirectPurchase.HasValue && tender.IsLowBudgetDirectPurchase.Value)
                {
                    UserProfile userProfile = await _iDMQueries.FindUserProfileByIdWithoutIncludesAsync(tender.DirectPurchaseMemberId.Value);
                    await _notificationAppService.AddNotificationSettingByUserId(NotificationOperations.DirectPurchaseMember.AgencyCommunicationRequest.RejectExtendOffersValidityBySupplierForDirectPurchaseMember, userProfile, (int)Enums.UserRole.CR_DirectPurchaseMember);
                    await _notificationAppService.SendNotificationByUserId(NotificationOperations.DirectPurchaseMember.AgencyCommunicationRequest.RejectExtendOffersValidityBySupplierForDirectPurchaseMember, tender.DirectPurchaseMemberId.Value, userProfile.UserName, PurshaseTemplateModel);
                }
                else
                {
                    await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseSecretary.AgencyCommunicationRequest.RejectExtendOffersValidityBySupplier, tender.DirectPurchaseCommitteeId, PurshaseTemplateModel);
                }
            }
            else
            {
                await _notificationAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersCheckSecretary.AgencyCommunicationRequest.RejectExtendOffersValidityBySupplier, tender.OffersCheckingCommitteeId, mainNotificationTemplateModel);
            }

        }
        public async Task CancelSupplierExtendOfferValidity(int extendOffersValidtyId, string supplierCr)
        {
            ExtendOffersValiditySupplier ExtendOffersValiditySupplier = await _communicationQueries.GetExtendOffersValiditySupplierById(extendOffersValidtyId, supplierCr);
            ExtendOffersValiditySupplier.UpdateExtendOffersValiditySupplier((int)Enums.SupplierExtendOffersValidityStatus.Sent);
            await _communicationCommands.UpdateExtendOffersValiditySupplier(ExtendOffersValiditySupplier);
        }


        #endregion

        #region Tender Communication Request

        public async Task<QueryResult<CommunicationRequestGrid>> GetSuppliersCommunicationRequestsGridAsync(SimpleTenderSearchCriteria criteria)
        {
            criteria.currentUserRole = _httpContextAccessor.HttpContext.User.UserRole();
            QueryResult<CommunicationRequestGrid> suppliersCommunicationRequestsGridModel = await _communicationQueries.GetSuppliersCommunicationRequestsGridAsync(criteria);
            return suppliersCommunicationRequestsGridModel;
        }
        public async Task<QueryResult<CommunicationRequestGrid>> GetAgencyCommunicationRequestsGridAsync(SimpleTenderSearchCriteria criteria)
        {
            criteria.currentUserRole = _httpContextAccessor.HttpContext.User.UserRole();
            QueryResult<CommunicationRequestGrid> agencyCommunicationRequests = await _communicationQueries.GetAgencyCommunicationRequestsGridAsync(criteria);
            return agencyCommunicationRequests;
        }
        public async Task UpdateTenderExtendDates(ExtendOfferPresentationDatesModel tenderDatesModel, string userRole, string agencyCode)
        {
            Tender tender = await _tenderQueries.GetTenderAgencyCommunicationByIdForExtendDatesRequist(tenderDatesModel.TenderId);
            _tenderDomainService.IsValidToUpdateTender(tender, agencyCode);
            _communicationDomainService.IsValidToUpdateModel(tenderDatesModel, tender.EstimatedValue);
            if (tender.LastOfferPresentationDate > tenderDatesModel.LastOfferPresentationDate || tender.LastEnqueriesDate > tenderDatesModel.LastEnqueriesDate || tender.OffersOpeningDate > tenderDatesModel.OffersOpeningDate)
            {
                throw new BusinessRuleException("تمديد التواريخ يجب ان يكون اكبر من تواريخ المنافسه");
            }
            var changeRequest = tender.ChangeRequests.FirstOrDefault(a => a.IsActive == true && a.ChangeRequestStatusId != (int)Enums.ChangeStatusType.Approved && a.ChangeRequestTypeId == (int)Enums.ChangeRequestType.ExtendDates);

            if (changeRequest == null)
            {
                changeRequest = new TenderChangeRequest(tender.TenderId, (int)Enums.ChangeRequestType.ExtendDates, (int)Enums.ChangeStatusType.New, userRole, null);
            }
            if (changeRequest.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending)
            {
                throw new BusinessRuleException("لا يمكن تنفيذ الطلب لوجود طلب اخر بإنتظار الإعتماد ");
            }
            changeRequest.AddRevisionDates(tenderDatesModel.LastEnqueriesDate, tenderDatesModel.LastOfferPresentationDate,
               tenderDatesModel.LastOfferPresentationTime, tenderDatesModel.OffersOpeningDate, tenderDatesModel.OffersOpeningTime, tenderDatesModel.OffersCheckingDate, tenderDatesModel.OffersCheckingTime, tender.TenderId);
            await _tenderCommands.CreateTenderChangeRequestAsync(changeRequest);

            TenderRevisionDateModel tenderRevisionDate = await FindActiveTenderRevisionDateByTenderId(tender.TenderId);
            TenderDatesChange tenderRevisionDateEntity = _mapper.Map<TenderDatesChange>(tenderRevisionDate);
            tender = tender.UpdateDates(tenderRevisionDateEntity, _httpContextAccessor.HttpContext.User.UserId());
            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(new NotificationArguments
            {
                SubjectEmailArgs = new object[] { },
                BodyEmailArgs = new object[] { "", tender.Agency?.NameArabic, tender.Branch?.BranchName, tender.TenderName, tender.ReferenceNumber, tender.Purpose, tender.LastEnqueriesDate?.ToString("dd/MM/yyyy hh:mm tt"), tender.LastOfferPresentationDate?.ToString("dd/MM/yyyy hh:mm tt"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                SMSArgs = new object[] { tender.Agency?.NameArabic, tender.Branch?.BranchName, tender.ReferenceNumber },
                PanelArgs = new object[] { tender.ReferenceNumber }
            },
            $"Tender/TenderExtendDateApprovement?tenderIdString={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId);

            MainNotificationTemplateModel approveTenderForSupplier = new MainNotificationTemplateModel(new NotificationArguments
            {
                SubjectEmailArgs = new object[] { },
                BodyEmailArgs = new object[] { "", tender.Agency?.NameArabic, tender.Branch?.BranchName, tender.TenderName, tender.ReferenceNumber, tender.Purpose, tender.LastEnqueriesDate?.ToString("dd/MM/yyyy hh:mm tt"), tender.LastOfferPresentationDate?.ToString("dd/MM/yyyy hh:mm tt"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                SMSArgs = new object[] { tender.Agency?.NameArabic, tender.Branch?.BranchName, tender.ReferenceNumber },
                PanelArgs = new object[] { tender.ReferenceNumber }
            },
            $"Tender/DetailsForSupplier?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId);

            List<string> suppliers = await _idmAppService.GetAllSupplierOnTender(tender.TenderId);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.approvetenderextenddate, suppliers, approveTenderForSupplier);

            if (tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.PurshaseSpecialist.OperationsOnTheTender.approvetenderextenddate, tender.BranchId, approveTender);
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.EtimadOfficer.OperationsOnTheTender.approvetenderextenddate, tender.BranchId, approveTender);
            }
            else
            {
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.OperationsOnTheTender.approvetenderextenddate, tender.BranchId, approveTender);
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.Auditor.OperationsOnTheTender.approvetenderextenddate, tender.BranchId, approveTender);
            }
            await _tenderCommands.UpdateAsync(tender);
        }
        public async Task UpdateQualificationExtendDates(ExtendOfferPresentationDatesModel tenderDatesModel, string userRole, string agencyCode)
        {
            Tender tender = await _tenderQueries.GetTenderAgencyCommunicationById(tenderDatesModel.TenderId);
            _tenderDomainService.IsValidToUpdateTender(tender, agencyCode);
            _communicationDomainService.IsValidToUpdateModel(tenderDatesModel, tender.EstimatedValue);
            if (tender.LastOfferPresentationDate > tenderDatesModel.LastOfferPresentationDate || tender.LastEnqueriesDate > tenderDatesModel.LastEnqueriesDate || tender.OffersCheckingDate > tenderDatesModel.OffersCheckingDate)
            {
                throw new BusinessRuleException("تمديد التواريخ يجب ان يكون اكبر من تواريخ المنافسه");
            }
            var changeRequest = tender.ChangeRequests.FirstOrDefault(a => a.IsActive == true && a.ChangeRequestStatusId != (int)Enums.ChangeStatusType.Approved && a.ChangeRequestTypeId == (int)Enums.ChangeRequestType.ExtendDates);
            if (changeRequest == null)
            {
                changeRequest = new TenderChangeRequest(tender.TenderId, (int)Enums.ChangeRequestType.ExtendDates, (int)Enums.ChangeStatusType.New, userRole, null);
            }
            if (changeRequest.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending)
            {
                throw new BusinessRuleException("لا يمكن تنفيذ الطلب لوجود طلب اخر بإنتظار الإعتماد ");
            }
            changeRequest.AddRevisionDates(tenderDatesModel.LastEnqueriesDate, tenderDatesModel.LastOfferPresentationDate,
               tenderDatesModel.LastOfferPresentationTime, tenderDatesModel.OffersOpeningDate, tenderDatesModel.OffersOpeningTime, tenderDatesModel.OffersCheckingDate, tenderDatesModel.OffersCheckingTime, tender.TenderId);
            await _tenderCommands.CreateTenderChangeRequestAsync(changeRequest);

            TenderRevisionDateModel tenderRevisionDate = await FindActiveTenderRevisionDateByTenderId(tender.TenderId);
            TenderDatesChange tenderRevisionDateEntity = _mapper.Map<TenderDatesChange>(tenderRevisionDate);
            tender = tender.UpdateDates(tenderRevisionDateEntity, _httpContextAccessor.HttpContext.User.UserId());

            MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(new NotificationArguments
            {
                SubjectEmailArgs = new object[] { },
                BodyEmailArgs = new object[] { "", tender.Agency?.NameArabic, tender.Branch?.BranchName, tender.TenderName, tender.ReferenceNumber, tender.Purpose, tender.LastEnqueriesDate?.ToString("dd/MM/yyyy hh:mm tt"), tender.LastOfferPresentationDate?.ToString("dd/MM/yyyy hh:mm tt"), tender.OffersCheckingDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersCheckingDate?.ToString("dd/MM/yyyy"), tender.OffersCheckingDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersCheckingDate?.ToString("hh:mm tt") },
                SMSArgs = new object[] { tender.Agency?.NameArabic, tender.Branch?.BranchName, tender.ReferenceNumber },
                PanelArgs = new object[] { tender.ReferenceNumber }
            },
            $"Qualification/QualificationExtendDateApprovement?tenderIdString={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId);

            MainNotificationTemplateModel approveTenderForSupplier = new MainNotificationTemplateModel(new NotificationArguments
            {
                SubjectEmailArgs = new object[] { },
                BodyEmailArgs = new object[] { "", tender.Agency?.NameArabic, tender.Branch?.BranchName, tender.TenderName, tender.ReferenceNumber, "", tender.LastEnqueriesDate?.ToString("dd/MM/yyyy hh:mm tt"), tender.LastOfferPresentationDate?.ToString("dd/MM/yyyy hh:mm tt"), tender.OffersCheckingDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersCheckingDate?.ToString("dd/MM/yyyy"), tender.OffersCheckingDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersCheckingDate?.ToString("hh:mm tt") },
                SMSArgs = new object[] { tender.Agency?.NameArabic, tender.Branch?.BranchName, tender.ReferenceNumber },
                PanelArgs = new object[] { tender.ReferenceNumber }
            },
            $"Tender/DetailsForSupplier?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), tender.BranchId);

            List<string> suppliers = await _idmAppService.GetAllSupplierOnQualification(tender.TenderId);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnPrequalification.ApproveQualificationExtendDate, suppliers, approveTenderForSupplier);
            await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.OperationsOnQualification.ApproveQualificationextenddate, tender.BranchId, approveTender);
            await _tenderCommands.UpdateAsync(tender);
        }
        public async Task<TenderRevisionDateModel> FindActiveTenderRevisionDateByTenderId(int tenderId)
        {
            var tenderRevisionDate = await _tenderQueries.FindActiveTenderRevisionDateByTenderId(tenderId);
            return tenderRevisionDate;
        }
        #endregion 
    }
}



