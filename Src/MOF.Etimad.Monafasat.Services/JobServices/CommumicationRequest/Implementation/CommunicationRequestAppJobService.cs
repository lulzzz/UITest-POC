using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public class CommunicationRequestAppJobService : ICommunicationRequestAppJobService
    {
        private readonly INotificationJobAppService _notificationJobAppService;
        private readonly ICommunicationRequestJobQueries _communicationRequestJobQueries;
        private readonly IGenericCommandRepository _genericCommandRepository;


        public CommunicationRequestAppJobService(ICommunicationRequestJobQueries communicationRequestJobQueries, IGenericCommandRepository genericCommandRepository,
           INotificationJobAppService notificationJobAppService)
        {
            _communicationRequestJobQueries = communicationRequestJobQueries;
            _genericCommandRepository = genericCommandRepository;
            _notificationJobAppService = notificationJobAppService;
        }


        public async Task FindTendersWithPlaintsAfterStoppingPeriodJob()
        {
            List<AgencyCommunicationRequest> agencyCommunications = await _communicationRequestJobQueries.FindTendersWithPlaintsAfterStoppingPeriodJob();

            var agencyCodeList = agencyCommunications.Select(d => d.Tender.AgencyCode).Distinct().ToList();
            var agencies = await _communicationRequestJobQueries.FindAgenciesByAgencyCodes(agencyCodeList);
            int? agencyCategoryId = 0;
            foreach (var request in agencyCommunications)
            {
                if (request.PlaintNotification == null)
                {
                    continue;
                }
                PlaintRequestNotification obj = request.PlaintNotification;
                obj.Update(true);
                _genericCommandRepository.Update(obj);
                await _genericCommandRepository.SaveAsync();

                agencyCategoryId = agencies.Where(a => a.AgencyCode == request.Tender.AgencyCode).FirstOrDefault().CategoryId;

                NotificationArguments NotificationArguments = new NotificationArguments
                {
                    BodyEmailArgs = new object[] { "", request.PlaintAcceptanceStatus.Name, request.Tender.ReferenceNumber },
                    SubjectEmailArgs = new object[] { },
                    PanelArgs = new object[] { request.Tender.ReferenceNumber },
                    SMSArgs = new object[] { request.Tender.ReferenceNumber }
                };
                MainNotificationTemplateModel approveTender = new MainNotificationTemplateModel(NotificationArguments, $"Tender/Details?STenderId{Util.Encrypt(request.Tender.TenderId)}", NotificationEntityType.Tender, request.Tender.TenderId.ToString(), request.Tender.BranchId);

                if (request.Tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
                    await _notificationJobAppService.SendNotifications(NotificationOperations.DirectPurchaseSecretary.AgencyCommunicationRequest.PlaintStoppingPeriodEnd, request.Tender.AgencyCode, agencyCategoryId.Value, approveTender, Enums.UserRole.NewMonafasat_SecretaryDirtectPurshasingCommittee.ToString(), (int)request.Tender.DirectPurchaseCommitteeId);
                else
                    await _notificationJobAppService.SendNotifications(NotificationOperations.OffersCheckSecretary.AgencyCommunicationRequest.PlaintStoppingPeriodEnd, request.Tender.AgencyCode, agencyCategoryId.Value, approveTender, Enums.UserRole.NewMonafasat_OffersCheckSecretary.ToString(), (int)request.Tender.OffersCheckingCommitteeId);
            }
        }

        public async Task ExcludeSupplierOffer()
        {
            List<ExtendOffersValiditySupplier> suppliersHasExtendOffersValidity = await _communicationRequestJobQueries.GetExtendOfferValiditySuppliers();

            var supplierExtendOfferValidityIds = suppliersHasExtendOffersValidity.Select(e => e.Id).Distinct().ToList();
            List<Offer> nonExecludedOfferList = await _communicationRequestJobQueries.GetNonExecludedOffersForExtendOffersByValidityIds(supplierExtendOfferValidityIds);
            Offer nonExecludedOffer = null;

            foreach (ExtendOffersValiditySupplier supplier in suppliersHasExtendOffersValidity)
            {
                if (supplier.SupplierExtendOfferValidityStatusId == (int)Enums.SupplierExtendOffersValidityStatus.Rejected ||
                    (supplier.SupplierExtendOfferValidityStatusId != (int)Enums.SupplierExtendOffersValidityStatus.Accepted
                   && DateTime.Now > supplier.ExtendOffersValidity.NewOffersExpiryDate))
                {
                    nonExecludedOffer = nonExecludedOfferList.Where(o => o.ExtendOffersValiditySupplier.Id == supplier.Id).FirstOrDefault();
                    if (nonExecludedOffer != null)
                    {
                        nonExecludedOffer.UpdateStatus(Enums.OfferStatus.Excluded);
                        _genericCommandRepository.Update(nonExecludedOffer);
                    }
                }
            }

            List<AgencyCommunicationRequest> agencyCommunicationRequests = await _communicationRequestJobQueries.GetExtendOffersValidityForExecludeSuppliers();

            foreach (var item in agencyCommunicationRequests)
            {
                item.UpdateSupplierOfferExtendDatesRequestStatus((int)Enums.AgencyCommunicationRequestStatus.Finished);
                _genericCommandRepository.Update(item);
            }
            await _genericCommandRepository.SaveAsync();
        }

        public async Task UpdateAllNegotiationWaitingSupplierResponse()
        {

            var allNegotiations = await _communicationRequestJobQueries.FindAllNegotiationWaitingSupplierResponse();
            foreach (var NEG in allNegotiations)
            {

                var currentSupplier = NEG.NegotiationFirstStageSuppliers.FirstOrDefault(w => w.NegotiationSupplierStatusId == (int)Enums.enNegotiationSupplierStatus.PendeingSupplierReply);
                if (currentSupplier == null || DateTime.Now.Subtract(currentSupplier.PeriodStartDateTime.Value).TotalHours < NEG.SupplierReplyPeriodHours)
                {
                    continue;
                }
                NEG.UpdateSupplierStatus(currentSupplier.Id, (int)Enums.enNegotiationSupplierStatus.NoReply, null);

                var _notInvitedSuppliers = NEG.NegotiationFirstStageSuppliers.Where(w => w.NegotiationSupplierStatusId == (int)Enums.enNegotiationSupplierStatus.NotSent).OrderBy(d => d.Offer.FinalPriceAfterDiscount).ToList();
                var Next = _notInvitedSuppliers.FirstOrDefault();
                if (Next != null)
                {
                    Next.StartSupplierPeriodService();
                    var SuppliersCRs = Next.SupplierCR;
                    var _offer = await _communicationRequestJobQueries.GetOfferById(Next.OfferId);
                    var _tender = await _communicationRequestJobQueries.FindTenderByTenderId(_offer.TenderId);
                    var _supplier = await _communicationRequestJobQueries.FindSupplierByCRNumber(_offer.CommericalRegisterNo);



                    NotificationArguments NotificationArgumentsforSupplier = new NotificationArguments
                    {
                        BodyEmailArgs = new object[] { _supplier.SelectedCrName, _tender.TenderName },
                        SubjectEmailArgs = new object[] { },
                        PanelArgs = new object[] { _supplier.SelectedCrName, _tender.TenderName },
                        SMSArgs = new object[] { _supplier.SelectedCrName, _tender.TenderName }
                    };

                    MainNotificationTemplateModel mainNotificationTemplateModelforSupplier = new MainNotificationTemplateModel(NotificationArgumentsforSupplier,
                    $"CommunicationRequest/SupplierNegotiation/{Util.Encrypt(_offer.TenderId)}/{Util.Encrypt(NEG.NegotiationId)}",
                    NotificationEntityType.Tender,
                    _offer.TenderId.ToString(), null, _tender.OffersCheckingCommitteeId);

                    _communicationRequestJobQueries.UpdateNegotiationFirstStage(NEG);
                    await _communicationRequestJobQueries.SaveChanges();
                    await _notificationJobAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.AgencyCommunicationRequest.SendNegotiationToSupplier, new List<string> { SuppliersCRs }, mainNotificationTemplateModelforSupplier);
                }
                else
                {
                    NEG.UpdateNegotiationFirstStageStatus((int)Enums.enNegotiationStatus.SupplierNotAgreed, "");
                    _communicationRequestJobQueries.UpdateNegotiationFirstStage(NEG);
                    await _communicationRequestJobQueries.SaveChanges();

                }
            }
        }



        public async Task UpdateAllSecondNegotiationWaitingSupplierResponse()
        {

            var allNegotiations = await _communicationRequestJobQueries.FindAllSecondNegotiationWaitingSupplierResponse();

            var agencies = await _communicationRequestJobQueries.FindAgenciesByAgencyCodes(allNegotiations.Select(d => d.AgencyCommunicationRequest.Tender.AgencyCode).Distinct().ToList());
            int? agencyCategoryId = 0;
            foreach (var NEG in allNegotiations)
            {
                agencyCategoryId = agencies.Where(a => a.AgencyCode == NEG.AgencyCommunicationRequest.Tender.AgencyCode).FirstOrDefault().CategoryId;

                NEG.UpdateNegotiationStatus((int)Enums.enNegotiationStatus.SupplierNotAgreed);

                #region[Send Approval To Agency]
                int? committeId = NEG.AgencyCommunicationRequest.Tender.OffersCheckingCommitteeId;
                string userRole = Enums.UserRole.NewMonafasat_OffersCheckSecretary.ToString();
                int codeId = NotificationOperations.OffersCheckSecretary.AgencyCommunicationRequest.RecectSecondNegotiationSupplier;
                if (NEG.AgencyCommunicationRequest.Tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || NEG.AgencyCommunicationRequest.Tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
                {
                    committeId = NEG.AgencyCommunicationRequest.Tender.DirectPurchaseCommitteeId;
                    userRole = Enums.UserRole.NewMonafasat_SecretaryDirtectPurshasingCommittee.ToString();
                    codeId = NotificationOperations.DirectPurchaseSecretary.AgencyCommunicationRequest.RecectSecondNegotiationSupplier;
                }
                await _communicationRequestJobQueries.UpdateNegotiationAsync(NEG);
                NotificationArguments NotificationArguments = new NotificationArguments
                {
                    BodyEmailArgs = new object[] { "", NEG.AgencyCommunicationRequest.Tender.ReferenceNumber },
                    SubjectEmailArgs = new object[] { },
                    PanelArgs = new object[] { "", NEG.AgencyCommunicationRequest.Tender.ReferenceNumber },
                    SMSArgs = new object[] { "", NEG.AgencyCommunicationRequest.Tender.ReferenceNumber }
                };

                MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
                $"CommunicationRequest/CreateSecondNegotiationRequestAsync/{Util.Encrypt(NEG.NegotiationId)}",
                NotificationEntityType.Tender,
                NEG.AgencyCommunicationRequest.Tender.TenderId.ToString(), NEG.AgencyCommunicationRequest.Tender.BranchId, committeId);
                await _notificationJobAppService.SendNotifications(codeId, NEG.AgencyCommunicationRequest.Tender.AgencyCode, agencyCategoryId.Value, mainNotificationTemplateModel, userRole, committeId.Value, 0);

                #endregion 
                await _communicationRequestJobQueries.UpdateNegotiationSecondStageAsync(NEG);
            }
            await _communicationRequestJobQueries.SaveChanges();
        }
    }
}
