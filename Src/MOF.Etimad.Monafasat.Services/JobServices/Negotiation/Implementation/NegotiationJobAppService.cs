using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using MOF.Etimad.Monafasat.SharedKernal;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public class NegotiationJobAppService : INegotiationJobAppService
    {
        private readonly INegotiationJobQueries _negotiationQueries;
        private readonly INotificationJobAppService _INotificationJobAppService;

        public NegotiationJobAppService(INegotiationJobQueries  negotiationQueries, INotificationJobAppService INotificationJobAppService)
        {
            _negotiationQueries = negotiationQueries;
            _INotificationJobAppService = INotificationJobAppService;
        }
        public async Task UpdateAllNegotiationWaitingSupplierResponse()
        {

            var allNegotiations = await _negotiationQueries.FindAllNegotiationWaitingSupplierResponse();
            foreach (var NEG in allNegotiations)
            {

                var currentSupplier = NEG.NegotiationFirstStageSuppliers.FirstOrDefault(w => w.NegotiationSupplierStatusId == (int)Enums.enNegotiationSupplierStatus.PendeingSupplierReply);
                if (currentSupplier == null || DateTime.Now.Subtract(currentSupplier.PeriodStartDateTime.Value).TotalHours < NEG.SupplierReplyPeriodHours)
                {
                    continue;
                }
                NEG.updateSupplierStatus(currentSupplier.Id, (int)Enums.enNegotiationSupplierStatus.NoReply, null);

                var _notInvitedSuppliers = NEG.NegotiationFirstStageSuppliers.Where(w => w.NegotiationSupplierStatusId == (int)Enums.enNegotiationSupplierStatus.NotSent).OrderBy(d => d.Offer.FinalPriceAfterDiscount).ToList();
                var Next = _notInvitedSuppliers.FirstOrDefault();
                if (Next != null)
                {
                    Next.StartSupplierPeriodService();
                    var SuppliersCRs = Next.SupplierCR;
                    var _offer = await _negotiationQueries.GetOfferById(Next.OfferId);
                    var _tender = await _negotiationQueries.FindTenderByTenderId(_offer.TenderId);
                    var _supplier = await _negotiationQueries.FindSupplierByCRNumber(_offer.CommericalRegisterNo);

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
                    await _INotificationJobAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.AgencyCommunicationRequest.SendNegotiationToSupplier, new List<string> { SuppliersCRs }, mainNotificationTemplateModelforSupplier);

                }
                else
                {
                    NEG.UpdateNegotiationFirstStageStatus((int)Enums.enNegotiationStatus.SupplierNotAgreed, "");

                }


                _negotiationQueries.UpdateNegotiationFirstStage(NEG);
            }

            await _negotiationQueries.SaveChanges();

        }



        public async Task UpdateAllSecondNegotiationWaitingSupplierResponse()
        {

            var allNegotiations = await _negotiationQueries.FindAllSecondNegotiationWaitingSupplierResponse();
            foreach (var NEG in allNegotiations)
            {
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
                await _negotiationQueries.UpdateNegotiationAsync(NEG);
                NotificationArguments NotificationArguments = new NotificationArguments
                {
                    BodyEmailArgs = new object[] { "", NEG.AgencyCommunicationRequest.Tender.ReferenceNumber },
                    SubjectEmailArgs = new object[] { },
                    PanelArgs = new object[] { "", NEG.AgencyCommunicationRequest.Tender.ReferenceNumber },
                    SMSArgs = new object[] { "", NEG.AgencyCommunicationRequest.Tender.ReferenceNumber }
                };

                MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
                $"CommunicationRequest/CreateSecondNegotiation/{Util.Encrypt(NEG.NegotiationId)}",
                NotificationEntityType.Tender,
                NEG.AgencyCommunicationRequest.Tender.TenderId.ToString(), NEG.AgencyCommunicationRequest.Tender.BranchId, committeId);
                await _INotificationJobAppService.SendNotifications(codeId, NEG.AgencyCommunicationRequest.Tender.Agency.AgencyCode, NEG.AgencyCommunicationRequest.Tender.Agency.CategoryId.Value, mainNotificationTemplateModel, userRole, committeId.Value,0);

                #endregion 
                await _negotiationQueries.UpdateNegotiationSecondStageAsync(NEG);
            }

            await _negotiationQueries.SaveChanges();


        }



    }
}
