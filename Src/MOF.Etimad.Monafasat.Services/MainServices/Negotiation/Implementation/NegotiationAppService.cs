using AutoMapper;
using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.Negotiations;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Offer;
using MOF.Etimad.Monafasat.ViewModel.QuantityTableTemplates;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public class NegotiationAppService : INegotiationAppService
    {
        private readonly IOfferQueries offerQueries;
        private readonly IOfferCommands offerCommands;
        private readonly INegotiationQueries _negotiationQueries;
        private readonly INegotiationCommands _negotiationCommands;
        private readonly IIDMQueries _iDMQueries;
        private readonly ITenderQueries _tenderQueries;
        private readonly ITenderAppService _tenderAppService;
        private readonly INotificationAppService _notificationAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenderCommands _tenderCommands;
        private readonly ISupplierQueries supplierQueries_;
        private readonly IMapper _mapper;
        private IGenericCommandRepository _genericCommandRepository;

        public IQuantityTemplatesProxy _quantityTemplatesProxy { get; }

        private readonly INegotiationDomainService _negotiationDomainService;
        private readonly ICheckFundProxy _checkFundProxy;

        private readonly IVerificationService _verification;
        public NegotiationAppService(ISupplierQueries _supplierQueries, IOfferCommands _offerCommands/*, ICommunicationNotification CommunicationNotification*/, INegotiationQueries negotiationQueries, INegotiationCommands negotiationCommands, INegotiationDomainService negotiationDomainService, IMapper mapper, IGenericCommandRepository genericCommandRepository,
                                ITenderCommands tenderCommands, ITenderQueries tenderQueries, ITenderAppService tenderAppService, INotificationAppService notificationAppService, IVerificationService verification, IHttpContextAccessor httpContextAccessor, IIDMQueries iDMQueries, IOfferQueries _offerQueries, ICheckFundProxy checkFundProxy, IQuantityTemplatesProxy quantityTemplatesProxy)
        {
            offerCommands = _offerCommands;
            supplierQueries_ = _supplierQueries;
            _negotiationQueries = negotiationQueries;
            _negotiationCommands = negotiationCommands;
            _negotiationDomainService = negotiationDomainService;
            _notificationAppService = notificationAppService;
            _tenderCommands = tenderCommands;
            _tenderQueries = tenderQueries;
            _tenderAppService = tenderAppService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _genericCommandRepository = genericCommandRepository;
            _iDMQueries = iDMQueries;
            offerQueries = _offerQueries;
            _checkFundProxy = checkFundProxy;

            _verification = verification;
            _quantityTemplatesProxy = quantityTemplatesProxy;
        }

        public async Task<bool> ChangeCommunicationRequestStatus(NegotiationAgencyActionStatusModel negotiationActionStatus)
        {
            var NegotiantionRequest = await _negotiationQueries.FindWithAgencyRequestById(Util.Decrypt(negotiationActionStatus.NegotiationIdString));
            var firstStageNegotiation = await _negotiationQueries.FindWithSuppliersById(Util.Decrypt(negotiationActionStatus.NegotiationIdString));

            firstStageNegotiation.UpdateNegotiationFirstStageStatus((int)negotiationActionStatus.Status, negotiationActionStatus.RejectionReason);
            Tender tender = await offerQueries.GetTenderbyTenderIdAsync(NegotiantionRequest.AgencyCommunicationRequest.TenderId);
            if (negotiationActionStatus.Status == Enums.enNegotiationStatus.SentToSuppliers)
            {
                bool check = await _verification.CheckForVerificationCode(NegotiantionRequest.AgencyCommunicationRequest.TenderId, negotiationActionStatus.VerificationCode, (int)Enums.VerificationType.Negotiation);

                var Offers = await offerQueries.GetOffersForFirstStageNegotiationByTenderId(NegotiantionRequest.AgencyCommunicationRequest.TenderId);
                var OrderedOfferLst = (tender.QuantityTableVersionId == (int)Enums.QuantityTableVersion.Version2 ? Offers.OrderBy(s => s.OfferAmountNP).ToList() : Offers.OrderBy(s => s.OfferAmount).ToList());
                var lowestOffer = OrderedOfferLst.FirstOrDefault();
                foreach (var offer in OrderedOfferLst)
                {
                    var SupplierNegotiation = new NegotiationFirstStageSupplier((int)Enums.enNegotiationSupplierStatus.NotSent, null, offer.OfferId, offer.CommericalRegisterNo, firstStageNegotiation.NegotiationId, offer.OfferAmount);
                    firstStageNegotiation.AddSupplier(SupplierNegotiation);
                }
                firstStageNegotiation = await _negotiationCommands.UpdateNegotiationFirstStageAsync(firstStageNegotiation);
                firstStageNegotiation.StartNegotiation(lowestOffer.OfferId);
                await _negotiationCommands.UpdateNegotiationFirstStageAsync(firstStageNegotiation);

                var SuppliersCRs = OrderedOfferLst.Select(s => s.CommericalRegisterNo).FirstOrDefault();
                var SupplierName = OrderedOfferLst.Select(s => s.SupplierName).FirstOrDefault();
                NotificationArguments NotificationArgumentsforSupplier = new NotificationArguments
                {
                    BodyEmailArgs = new object[] { SupplierName, tender.TenderName },
                    SubjectEmailArgs = new object[] { },
                    PanelArgs = new object[] { SupplierName, tender.TenderName },
                    SMSArgs = new object[] { SupplierName, tender.TenderName }
                };

                MainNotificationTemplateModel mainNotificationTemplateModelforSupplier = new MainNotificationTemplateModel(NotificationArgumentsforSupplier,
                $"CommunicationRequest/SupplierNegotiation/{Util.Encrypt(tender.TenderId)}/{Util.Encrypt(NegotiantionRequest.NegotiationId)}",
                NotificationEntityType.Tender,
                tender.TenderId.ToString(), null, tender.OffersCheckingCommitteeId);
                await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.AgencyCommunicationRequest.SendNegotiationToSupplier, new List<string> { SuppliersCRs }, mainNotificationTemplateModelforSupplier);


                NotificationArguments NotificationArguments = new NotificationArguments
                {
                    BodyEmailArgs = new object[] { tender.ReferenceNumber },
                    SubjectEmailArgs = new object[] { },
                    PanelArgs = new object[] { tender.ReferenceNumber },
                    SMSArgs = new object[] { tender.ReferenceNumber }
                };

                int? committeId = firstStageNegotiation.AgencyCommunicationRequest.Tender.OffersCheckingCommitteeId;

                int codeId = NotificationOperations.OffersCheckSecretary.AgencyCommunicationRequest.ApproveNegotiationFirstSecratary;
                if (firstStageNegotiation.AgencyCommunicationRequest.Tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || firstStageNegotiation.AgencyCommunicationRequest.Tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
                {
                    committeId = firstStageNegotiation.AgencyCommunicationRequest.Tender.DirectPurchaseCommitteeId;

                    codeId = NotificationOperations.DirectPurchaseSecretary.AgencyCommunicationRequest.ApproveFirstNegotiation;
                }


                MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
                       $"CommunicationRequest/Negotiation/{Util.Encrypt(tender.TenderId)}/{Util.Encrypt(NegotiantionRequest.NegotiationId)}",
                  NotificationEntityType.Tender,
                  tender.TenderId.ToString(), null, tender.OffersCheckingCommitteeId);
                await _notificationAppService.SendNotificationForCommitteeUsers(codeId, committeId, mainNotificationTemplateModel);

                return true;
            }
            else
            {
                NotificationArguments NotificationArguments = new NotificationArguments
                {
                    BodyEmailArgs = new object[] { tender.ReferenceNumber },
                    SubjectEmailArgs = new object[] { },
                    PanelArgs = new object[] { tender.ReferenceNumber },
                    SMSArgs = new object[] { tender.ReferenceNumber }
                };

                MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
                       $"CommunicationRequest/Negotiation/{Util.Encrypt(tender.TenderId)}/{Util.Encrypt(NegotiantionRequest.NegotiationId)}",
                  NotificationEntityType.Tender,
                  tender.TenderId.ToString(), null, tender.OffersCheckingCommitteeId);


                int? committeId = firstStageNegotiation.AgencyCommunicationRequest.Tender.OffersCheckingCommitteeId;

                int codeId = NotificationOperations.OffersCheckSecretary.AgencyCommunicationRequest.RejectFirstNegotiation;
                if (firstStageNegotiation.AgencyCommunicationRequest.Tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || firstStageNegotiation.AgencyCommunicationRequest.Tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
                {
                    committeId = firstStageNegotiation.AgencyCommunicationRequest.Tender.DirectPurchaseCommitteeId;

                    codeId = NotificationOperations.DirectPurchaseSecretary.AgencyCommunicationRequest.RejectFirstNegotiation;
                }


                await _notificationAppService.SendNotificationForCommitteeUsers(codeId, committeId, mainNotificationTemplateModel);

                await _negotiationCommands.UpdateNegotiationFirstStageAsync(firstStageNegotiation);

            }

            return true;
        }

        public async Task<bool> ApproveNegotiationRequestFirstStage(NegotiationAgencyActionStatusModel negotiationActionStatus)
        {
            var firstStageNegotiation = await _negotiationQueries.GetNegotiationFirstStageWithAgency(Util.Decrypt(negotiationActionStatus.NegotiationIdString));
            bool check = await _verification.CheckForVerificationCode(firstStageNegotiation.AgencyCommunicationRequest.TenderId, negotiationActionStatus.VerificationCode, (int)Enums.VerificationType.Negotiation);
            firstStageNegotiation.UpdateNegotiationFirstStageStatus((int)Enums.enNegotiationStatus.SentToSuppliers, negotiationActionStatus.RejectionReason);

            Tender tender = firstStageNegotiation.AgencyCommunicationRequest.Tender;
            var Offers = await offerQueries.GetAcceptedOffersForFirstStageNegotiationByTenderId(firstStageNegotiation.AgencyCommunicationRequest.TenderId);
            var OrderedOfferLst = (tender.QuantityTableVersionId == (int)Enums.QuantityTableVersion.Version2 ? Offers.OrderBy(s => s.OfferAmountNP).ToList() : Offers.OrderBy(s => s.OfferAmount).ToList());
            var lowestOffer = OrderedOfferLst.FirstOrDefault();

            firstStageNegotiation.StartNegotiation(lowestOffer.OfferId);
            await _negotiationCommands.UpdateNegotiationFirstStageAsync(firstStageNegotiation);

            var SuppliersCRs = OrderedOfferLst.Select(s => s.CommericalRegisterNo).FirstOrDefault();
            var SupplierName = OrderedOfferLst.Select(s => s.SupplierName).FirstOrDefault();
            NotificationArguments NotificationArgumentsforSupplier = new NotificationArguments
            {
                BodyEmailArgs = new object[] { SupplierName, tender.TenderName },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { SupplierName, tender.TenderName },
                SMSArgs = new object[] { SupplierName, tender.TenderName }
            };

            MainNotificationTemplateModel mainNotificationTemplateModelforSupplier = new MainNotificationTemplateModel(NotificationArgumentsforSupplier,
            $"CommunicationRequest/SupplierNegotiationDetails/{Util.Encrypt(tender.TenderId)}/{Util.Encrypt(firstStageNegotiation.NegotiationId)}",
            NotificationEntityType.Tender,
            tender.TenderId.ToString(), null, tender.OffersCheckingCommitteeId);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.AgencyCommunicationRequest.SendNegotiationToSupplier, new List<string> { SuppliersCRs }, mainNotificationTemplateModelforSupplier);

            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            int? committeId = firstStageNegotiation.AgencyCommunicationRequest.Tender.OffersCheckingCommitteeId;
            int codeId = NotificationOperations.OffersCheckSecretary.AgencyCommunicationRequest.ApproveNegotiationFirstSecratary;
            if (firstStageNegotiation.AgencyCommunicationRequest.Tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || firstStageNegotiation.AgencyCommunicationRequest.Tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            {
                committeId = firstStageNegotiation.AgencyCommunicationRequest.Tender.DirectPurchaseCommitteeId;
                codeId = NotificationOperations.DirectPurchaseSecretary.AgencyCommunicationRequest.ApproveFirstNegotiation;
            }

            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
                   $"CommunicationRequest/NewNegotiation/{Util.Encrypt(tender.TenderId)}/{Util.Encrypt(firstStageNegotiation.NegotiationId)}",
              NotificationEntityType.Tender,
              tender.TenderId.ToString(), null, tender.OffersCheckingCommitteeId);
            if (tender.IsLowBudgetDirectPurchase.HasValue && tender.IsLowBudgetDirectPurchase.Value)
            {
                UserProfile userProfile = await _iDMQueries.FindUserProfileByIdWithoutIncludesAsync(tender.DirectPurchaseMemberId.Value);
                await _notificationAppService.AddNotificationSettingByUserId(NotificationOperations.DirectPurchaseMember.AgencyCommunicationRequest.ApproveFirstStageNegotiationFromDirectPurchaseMember, userProfile, (int)Enums.UserRole.CR_DirectPurchaseMember);
                await _notificationAppService.SendNotificationByUserId(NotificationOperations.DirectPurchaseMember.AgencyCommunicationRequest.ApproveFirstStageNegotiationFromDirectPurchaseMember, tender.DirectPurchaseMemberId.Value, userProfile.UserName, mainNotificationTemplateModel);
            }
            else
            {
                await _notificationAppService.SendNotificationForCommitteeUsers(codeId, committeId, mainNotificationTemplateModel);
            }
            return true;
        }

        public async Task<bool> RejectNegotiationRequestFirstStage(NegotiationAgencyActionStatusModel negotiationActionStatus)
        {
            var firstStageNegotiation = await _negotiationQueries.FindWithSuppliersById(Util.Decrypt(negotiationActionStatus.NegotiationIdString));
            firstStageNegotiation.UpdateNegotiationFirstStageStatus((int)Enums.enNegotiationStatus.CheckManagerReject, negotiationActionStatus.RejectionReason);
            Tender tender = firstStageNegotiation.AgencyCommunicationRequest.Tender;
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };

            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
                   $"CommunicationRequest/NewNegotiation/{Util.Encrypt(tender.TenderId)}/{Util.Encrypt(firstStageNegotiation.NegotiationId)}",
              NotificationEntityType.Tender,
              tender.TenderId.ToString(), null, tender.OffersCheckingCommitteeId);

            int? committeId = firstStageNegotiation.AgencyCommunicationRequest.Tender.OffersCheckingCommitteeId;
            int codeId = NotificationOperations.OffersCheckSecretary.AgencyCommunicationRequest.RejectFirstNegotiation;
            if (firstStageNegotiation.AgencyCommunicationRequest.Tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || firstStageNegotiation.AgencyCommunicationRequest.Tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            {
                committeId = firstStageNegotiation.AgencyCommunicationRequest.Tender.DirectPurchaseCommitteeId;
                codeId = NotificationOperations.DirectPurchaseSecretary.AgencyCommunicationRequest.RejectFirstNegotiation;
            }

            await _notificationAppService.SendNotificationForCommitteeUsers(codeId, committeId, mainNotificationTemplateModel);
            await _negotiationCommands.UpdateNegotiationFirstStageAsync(firstStageNegotiation);
            return true;
        }

        public async Task<NegotiationSupplierViewModel> GetSupplierNegotiationPageInfo(string TenderId, string NegotiationId, string CR)
        {
            var offer = await offerQueries.GetOfferByTenderAndCR(Util.Decrypt(TenderId), CR);
            NegotiationSupplierViewModel supplierOfferInfo = new NegotiationSupplierViewModel();
            supplierOfferInfo.supplierOfferInfo = await _negotiationQueries.FindSupplierOfferDetails(Util.Decrypt(NegotiationId), offer.OfferId);
            Check.ArgumentNotNull(nameof(supplierOfferInfo.supplierOfferInfo), supplierOfferInfo.supplierOfferInfo);
            var Negotiation = await _negotiationQueries.GetNegotiationFirstStageWithAgency(Util.Decrypt(NegotiationId));
            var SupplierNegotiation = Negotiation.NegotiationFirstStageSuppliers.Where(w => w.OfferId == offer.OfferId).FirstOrDefault();
            if (SupplierNegotiation.NegotiationSupplierStatusId == (int)Enums.enNegotiationSupplierStatus.Agree)
                throw new BusinessRuleException("تم الرد بالموافقة على هذا التفاوض");
            else if (SupplierNegotiation.NegotiationSupplierStatusId == (int)Enums.enNegotiationSupplierStatus.DisAgree)
                throw new BusinessRuleException("تم الرد بالرفض على هذا التفاوض");
            if (SupplierNegotiation.NegotiationSupplierStatusId != (int)Enums.enNegotiationSupplierStatus.PendeingSupplierReply)
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.NotAllowedSupplierNegotiation);

            SupplierNegotiation.SetAsReported(true);

            await _negotiationCommands.UpdateNegotiationFirstStageAsync(Negotiation);
            supplierOfferInfo.SupplierTenderMainInfo = await _negotiationQueries.FindTenderDetails(Util.Decrypt(TenderId));
            return supplierOfferInfo;
        }

        public async Task<NegotiationSupplierViewModel> GetSupplierNegotiationFirstStageInfo(string TenderId, string NegotiationId, string CR)
        {
            var offer = await offerQueries.GetOfferByTenderAndCR(Util.Decrypt(TenderId), CR);
            NegotiationSupplierViewModel supplierOfferInfo = new NegotiationSupplierViewModel();
            supplierOfferInfo.supplierOfferInfo = await _negotiationQueries.FindSupplierOfferDetailsForNegotiationFirstStage(Util.Decrypt(NegotiationId), offer.OfferId);
            Check.ArgumentNotNull(nameof(supplierOfferInfo.supplierOfferInfo), supplierOfferInfo.supplierOfferInfo);
            var Negotiation = await _negotiationQueries.GetNegotiationFirstStageWithAgency(Util.Decrypt(NegotiationId));
            var SupplierNegotiation = Negotiation.NegotiationFirstStageSuppliers.Where(w => w.OfferId == offer.OfferId).FirstOrDefault();
            if (SupplierNegotiation.NegotiationSupplierStatusId == (int)Enums.enNegotiationSupplierStatus.Agree)
                throw new BusinessRuleException("تم الرد بالموافقة على هذا التفاوض");
            else if (SupplierNegotiation.NegotiationSupplierStatusId == (int)Enums.enNegotiationSupplierStatus.DisAgree)
                throw new BusinessRuleException("تم الرد بالرفض على هذا التفاوض");
            if (SupplierNegotiation.NegotiationSupplierStatusId != (int)Enums.enNegotiationSupplierStatus.PendeingSupplierReply)
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.NotAllowedSupplierNegotiation);


            SupplierNegotiation.SetAsReported(true);

            await _negotiationCommands.UpdateNegotiationFirstStageAsync(Negotiation);
            supplierOfferInfo.SupplierTenderMainInfo = await _negotiationQueries.FindTenderDetails(Util.Decrypt(TenderId));
            return supplierOfferInfo;
        }

        public async Task AgreeOnOfferNegotiationFirstStage(string NegotiationId, string TenderId, string CR, string CRName)
        {

            var offer = await offerQueries.GetOfferForNegotiation(Util.Decrypt(TenderId), CR);
            Check.ArgumentNotNull(nameof(offer), offer);
            var negotiationFirstStage = await _negotiationQueries.GetNegotiationFirstStageWithAgency(Util.Decrypt(NegotiationId));
            Check.ArgumentNotNull(nameof(negotiationFirstStage), negotiationFirstStage);
            var tender = negotiationFirstStage.AgencyCommunicationRequest.Tender;
            var negotiationFirstStageSupplier = negotiationFirstStage.NegotiationFirstStageSuppliers.Where(w => w.OfferId == offer.OfferId).FirstOrDefault();
            if (negotiationFirstStageSupplier.NegotiationSupplierStatusId != (int)Enums.enNegotiationSupplierStatus.PendeingSupplierReply)
            {
                throw new BusinessRuleException("تم تغير حالة الطلب");
            }
            if (DateTime.Now.Subtract(negotiationFirstStageSupplier.PeriodStartDateTime.Value).TotalHours > negotiationFirstStage.SupplierReplyPeriodHours)
            {
                negotiationFirstStage.UpdateNegotiationFirstStageStatus((int)Enums.enNegotiationSupplierStatus.DisAgree, "");
                await _negotiationCommands.UpdateNegotiationFirstStageAsync(negotiationFirstStage);
                await
                    _negotiationCommands.SaveChanges();
                throw new BusinessRuleException("إنتهى وقت الرد");
            }
            negotiationFirstStageSupplier.UpdateNegotiationFirstStageSupplier((int)Enums.enNegotiationSupplierStatus.Agree, negotiationFirstStageSupplier.PeriodStartDateTime);
            negotiationFirstStage.UpdateNegotiationFirstStageStatus((int)Enums.enNegotiationStatus.SupplierAgreed, negotiationFirstStage.RejectionReason);
            await _negotiationCommands.UpdateNegotiationFirstStageAsync(negotiationFirstStage);
            try
            {
                await UpdateOfferFinalPrice(offer, negotiationFirstStage.DiscountAmount);
            }
            catch (Exception ds)
            {

                throw;
            }

            NotificationArguments NotificationArgumentsforSupplier = new NotificationArguments
            {
                BodyEmailArgs = new object[] { CRName, tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { CRName, tender.ReferenceNumber },
                SMSArgs = new object[] { CRName, tender.ReferenceNumber }
            };
            int? committeId = tender.OffersCheckingCommitteeId;

            int codeId = NotificationOperations.OffersCheckSecretary.AgencyCommunicationRequest.AgreeNegotiationFirstSuppliers;
            if (tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            {
                committeId = tender.DirectPurchaseCommitteeId;

                codeId = NotificationOperations.DirectPurchaseSecretary.AgencyCommunicationRequest.AgreeNegotiationFirstSuppliers;
            }

            MainNotificationTemplateModel mainNotificationTemplateModelforSupplier = new MainNotificationTemplateModel(NotificationArgumentsforSupplier,
            $"CommunicationRequest/CreateNegotiation/{Util.Encrypt(tender.TenderId)}/{Util.Encrypt(negotiationFirstStage.NegotiationId)}",
            NotificationEntityType.Tender,
            tender.TenderId.ToString(), null, codeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(codeId, committeId, mainNotificationTemplateModelforSupplier);

        }
        public async Task AgreeOnFirstStageNegotiationBySupplier(string NegotiationId, string TenderId, string CR, string CRName)
        {

            var offer = await offerQueries.GetOfferForNegotiation(Util.Decrypt(TenderId), CR);
            Check.ArgumentNotNull(nameof(offer), offer);
            var negotiationFirstStage = await _negotiationQueries.GetNegotiationFirstStageWithAgency(Util.Decrypt(NegotiationId));
            var tender = negotiationFirstStage.AgencyCommunicationRequest.Tender;
            Check.ArgumentNotNull(nameof(negotiationFirstStage), negotiationFirstStage);
            var negotiationFirstStageSupplier = negotiationFirstStage.NegotiationFirstStageSuppliers.Where(w => w.OfferId == offer.OfferId).FirstOrDefault();
            if (negotiationFirstStageSupplier.NegotiationSupplierStatusId != (int)Enums.enNegotiationSupplierStatus.PendeingSupplierReply)
            {
                throw new BusinessRuleException("تم تغير حالة الطلب");
            }
            if (DateTime.Now.Subtract(negotiationFirstStageSupplier.PeriodStartDateTime.Value).TotalHours > negotiationFirstStage.SupplierReplyPeriodHours)
            {
                negotiationFirstStage.UpdateNegotiationFirstStageStatus((int)Enums.enNegotiationSupplierStatus.DisAgree, "");
                await _negotiationCommands.UpdateNegotiationFirstStageAsync(negotiationFirstStage);
                await
                    _negotiationCommands.SaveChanges();
                throw new BusinessRuleException("إنتهى وقت الرد");
            }
            negotiationFirstStageSupplier.UpdateNegotiationFirstStageSupplier((int)Enums.enNegotiationSupplierStatus.Agree, negotiationFirstStageSupplier.PeriodStartDateTime);
            negotiationFirstStage.UpdateNegotiationFirstStageStatus((int)Enums.enNegotiationStatus.SupplierAgreed, negotiationFirstStage.RejectionReason);
            await _negotiationCommands.UpdateNegotiationFirstStageAsync(negotiationFirstStage);

            await UpdateOfferFinalPriceFirstStageNegotiation(offer, negotiationFirstStage.DiscountAmount);

            NotificationArguments NotificationArgumentsforSupplier = new NotificationArguments
            {
                BodyEmailArgs = new object[] { CRName, tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { CRName, tender.ReferenceNumber },
                SMSArgs = new object[] { CRName, tender.ReferenceNumber }
            };
            int? committeId = tender.OffersCheckingCommitteeId;

            int codeId = NotificationOperations.OffersCheckSecretary.AgencyCommunicationRequest.AgreeNegotiationFirstSuppliers;
            if (tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            {
                committeId = tender.DirectPurchaseCommitteeId;

                codeId = NotificationOperations.DirectPurchaseSecretary.AgencyCommunicationRequest.AgreeNegotiationFirstSuppliers;
            }

            MainNotificationTemplateModel mainNotificationTemplateModelforSupplier = new MainNotificationTemplateModel(NotificationArgumentsforSupplier,
            $"CommunicationRequest/CreateNegotiation/{Util.Encrypt(tender.TenderId)}/{Util.Encrypt(negotiationFirstStage.NegotiationId)}",
            NotificationEntityType.Tender,
            tender.TenderId.ToString(), null, codeId);
            if (tender.IsLowBudgetDirectPurchase.HasValue && tender.IsLowBudgetDirectPurchase.Value)
            {
                UserProfile userProfile = await _iDMQueries.FindUserProfileByIdWithoutIncludesAsync(tender.DirectPurchaseMemberId.Value);
                await _notificationAppService.AddNotificationSettingByUserId(NotificationOperations.DirectPurchaseMember.AgencyCommunicationRequest.AcceptFirstStageNegotiationBySupplier, userProfile, (int)Enums.UserRole.CR_DirectPurchaseMember);
                await _notificationAppService.SendNotificationByUserId(NotificationOperations.DirectPurchaseMember.AgencyCommunicationRequest.AcceptFirstStageNegotiationBySupplier, tender.DirectPurchaseMemberId.Value, userProfile.UserName, mainNotificationTemplateModelforSupplier);
            }
            else
            {
                await _notificationAppService.SendNotificationForCommitteeUsers(codeId, committeId, mainNotificationTemplateModelforSupplier);
            }

        }

        public async Task AgreeOnNegotiationFirstStageWithExtraDiscount(AcceptNegotiationWithExtraDiscountModel extraDiscountModel)
        {
            var cr = _httpContextAccessor.HttpContext.User.SupplierNumber();
            var crName = _httpContextAccessor.HttpContext.User.SupplierName();

            var offer = await offerQueries.GetOfferForNegotiation(Util.Decrypt(extraDiscountModel.TenderIdString), cr);
            Check.ArgumentNotNull(nameof(offer), offer);
            var negotiationFirstStage = await _negotiationQueries.GetNegotiationFirstStageWithAgency(Util.Decrypt(extraDiscountModel.NegotiationIdString));

            var tender = negotiationFirstStage.AgencyCommunicationRequest.Tender;

            Check.ArgumentNotNull(nameof(negotiationFirstStage), negotiationFirstStage);
            var negotiationFirstStageSupplier = negotiationFirstStage.NegotiationFirstStageSuppliers.Where(w => w.OfferId == offer.OfferId).FirstOrDefault();
            if (negotiationFirstStageSupplier.NegotiationSupplierStatusId != (int)Enums.enNegotiationSupplierStatus.PendeingSupplierReply)
            {
                throw new BusinessRuleException("تم تغير حالة الطلب");
            }
            if (DateTime.Now.Subtract(negotiationFirstStageSupplier.PeriodStartDateTime.Value).TotalHours > negotiationFirstStage.SupplierReplyPeriodHours)
            {
                negotiationFirstStage.UpdateNegotiationFirstStageStatus((int)Enums.enNegotiationSupplierStatus.DisAgree, "");
                await _negotiationCommands.UpdateNegotiationFirstStageAsync(negotiationFirstStage);
                await
                    _negotiationCommands.SaveChanges();
                throw new BusinessRuleException("إنتهى وقت الرد");
            }
            negotiationFirstStageSupplier.UpdateNegotiationFirstStageSupplier((int)Enums.enNegotiationSupplierStatus.AgreeWithExtraDiscount, negotiationFirstStageSupplier.PeriodStartDateTime);
            //negotiationFirstStage.UpdateNegotiationFirstStageStatus((int)Enums.enNegotiationStatus.SupplierAgreedWithExtraDiscount, negotiationFirstStage.RejectionReason);
            negotiationFirstStage.AgreeWithExtraDiscount(extraDiscountModel.ExtraDiscountValue);
            await _negotiationCommands.UpdateNegotiationFirstStageAsync(negotiationFirstStage);
            await UpdateOfferFinalPriceFirstStageNegotiation(offer, extraDiscountModel.ExtraDiscountValue.Value);

            NotificationArguments NotificationArgumentsforSupplier = new NotificationArguments
            {
                BodyEmailArgs = new object[] { crName, tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { crName, tender.ReferenceNumber },
                SMSArgs = new object[] { crName, tender.ReferenceNumber }
            };
            int? committeId = offer.Tender.OffersCheckingCommitteeId;

            int codeId = NotificationOperations.OffersCheckSecretary.AgencyCommunicationRequest.AgreeNegotiationFirstSuppliers;
            if (offer.Tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            {
                committeId = tender.DirectPurchaseCommitteeId;

                codeId = NotificationOperations.DirectPurchaseSecretary.AgencyCommunicationRequest.AgreeNegotiationFirstSuppliers;
            }

            MainNotificationTemplateModel mainNotificationTemplateModelforSupplier = new MainNotificationTemplateModel(NotificationArgumentsforSupplier,
            $"CommunicationRequest/NewNegotiation/{Util.Encrypt(tender.TenderId)}/{Util.Encrypt(negotiationFirstStage.NegotiationId)}",
            NotificationEntityType.Tender,
            tender.TenderId.ToString(), null, codeId);
            if (tender.IsLowBudgetDirectPurchase.HasValue && tender.IsLowBudgetDirectPurchase.Value)
            {
                UserProfile userProfile = await _iDMQueries.FindUserProfileByIdWithoutIncludesAsync(tender.DirectPurchaseMemberId.Value);
                await _notificationAppService.AddNotificationSettingByUserId(NotificationOperations.DirectPurchaseMember.AgencyCommunicationRequest.AcceptFirstStageNegotiationBySupplier, userProfile, (int)Enums.UserRole.CR_DirectPurchaseMember);
                await _notificationAppService.SendNotificationByUserId(NotificationOperations.DirectPurchaseMember.AgencyCommunicationRequest.AcceptFirstStageNegotiationBySupplier, tender.DirectPurchaseMemberId.Value, userProfile.UserName, mainNotificationTemplateModelforSupplier);
            }
            else
            {
                await _notificationAppService.SendNotificationForCommitteeUsers(codeId, committeId, mainNotificationTemplateModelforSupplier);
            }

        }

        private async Task UpdateOfferFinalPrice(Offer offer, decimal discountAmount)
        {
            offer.UpdateFinalPriceForNegotiation(discountAmount);
            await offerCommands.UpdateAsync(offer);
        }
        private async Task UpdateOfferFinalPriceFirstStageNegotiation(Offer offer, decimal discountAmount)
        {
            offer.UpdateFinalPriceForNegotiationFirstStage(discountAmount);
            await offerCommands.UpdateAsync(offer);
        }

        public async Task DisAgreeOfferAfterNegotiationFirstStage(string NegotiationId, string TenderId, string CR)
        {

            var offer = await offerQueries.GetOfferByTenderAndCR(Util.Decrypt(TenderId), CR);
            Check.ArgumentNotNull(nameof(offer), offer);
            var negotiationFirstStage = await _negotiationQueries.GetNegotiationFirstStageWithAgency(Util.Decrypt(NegotiationId));
            Check.ArgumentNotNull(nameof(negotiationFirstStage), negotiationFirstStage);
            var negotiationFirstStageSupplier = negotiationFirstStage.NegotiationFirstStageSuppliers.Where(w => w.OfferId == offer.OfferId).FirstOrDefault();

            negotiationFirstStageSupplier.UpdateNegotiationFirstStageSupplier((int)Enums.enNegotiationSupplierStatus.DisAgree, negotiationFirstStageSupplier.PeriodStartDateTime);
            await _negotiationCommands.UpdateNegotiationFirstStageAsync(negotiationFirstStage);
            var nextSupplier = GetNextSupplier(negotiationFirstStageSupplier, negotiationFirstStage.NegotiationFirstStageSuppliers);
            if (nextSupplier != null)
            {
                var supplier = await _negotiationQueries.GetSupplierInfoByCr(nextSupplier.SupplierCR);
                nextSupplier.UpdateNegotiationFirstStageSupplier((int)Enums.enNegotiationSupplierStatus.PendeingSupplierReply, DateTime.Now);
                var SuppliersCRs = nextSupplier.SupplierCR;
                var SupplierName = supplier.SelectedCrName;
                NotificationArguments NotificationArgumentsforSupplier = new NotificationArguments
                {
                    BodyEmailArgs = new object[] { SupplierName, negotiationFirstStage.AgencyCommunicationRequest.Tender.TenderName },
                    SubjectEmailArgs = new object[] { },
                    PanelArgs = new object[] { SupplierName, negotiationFirstStage.AgencyCommunicationRequest.Tender.TenderName },
                    SMSArgs = new object[] { SupplierName, negotiationFirstStage.AgencyCommunicationRequest.Tender.TenderName }
                };
                string link = $"CommunicationRequest/SupplierNegotiation/{Util.Encrypt(offer.Tender.TenderId)}/{Util.Encrypt(negotiationFirstStage.NegotiationId)}";
                if (negotiationFirstStage.IsNewNegotiation.HasValue)
                {
                    link = $"CommunicationRequest/SupplierNegotiationDetails/{Util.Encrypt(offer.Tender.TenderId)}/{Util.Encrypt(negotiationFirstStage.NegotiationId)}";
                }
                MainNotificationTemplateModel mainNotificationTemplateModelforSupplier = new MainNotificationTemplateModel(NotificationArgumentsforSupplier,
                link,
                NotificationEntityType.Tender,
                offer.TenderId.ToString(), null, negotiationFirstStage.AgencyCommunicationRequest.Tender.OffersCheckingCommitteeId);
                await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.AgencyCommunicationRequest.SendNegotiationToSupplier, new List<string> { SuppliersCRs }, mainNotificationTemplateModelforSupplier);
            }
            else
            {
                negotiationFirstStage.UpdateNegotiationFirstStageStatus((int)Enums.enNegotiationStatus.SupplierNotAgreed, negotiationFirstStage.RejectionReason);
            }
            await _negotiationCommands.UpdateNegotiationFirstStageAsync(negotiationFirstStage);
        }

        #region Negotiation second stage

        //public async Task<SecondStageNegotiationViewModel> GetNegotiationQuantityTableModel(int negotiaitionId)
        //{
        //    string userAgency = _httpContextAccessor.HttpContext.User.UserAgency();
        //    if (_httpContextAccessor.HttpContext.User.UserRole() != RoleNames.supplier.ToString())
        //    {
        //        Check.ArgumentNotNullOrEmpty(nameof(userAgency), userAgency.ToString());
        //    }
        //    _negotiationDomainService.IsValidToUpdateModel(negotiaitionId, userAgency);
        //    return await _negotiationQueries.GetNegotiationQuantityTableModelByNegotiaitionId(negotiaitionId);
        //}

        //public async Task<SecondStageNegotiationViewModel> GetNegotiationQuantityTableModel_NEWNEGO(int negotiaitionId)
        //{
        //    string userAgency = _httpContextAccessor.HttpContext.User.UserAgency();
        //    if (_httpContextAccessor.HttpContext.User.UserRole() != RoleNames.supplier.ToString())
        //    {
        //        Check.ArgumentNotNullOrEmpty(nameof(userAgency), userAgency.ToString());
        //    }
        //    _negotiationDomainService.IsValidToUpdateModel(negotiaitionId, userAgency);
        //    return await _negotiationQueries.GetNegotiationQuantityTableModelByNegotiaitionId(negotiaitionId);
        //}

        public async Task SendToApproveNegotiationSecondStageByCheckManagerAsync(int NegotiationId, int Days, int Hours)
        {
            NegotiationSecondStage negotiation = await _negotiationQueries.FindNegotiationSecondStageById(NegotiationId);
            negotiation.UpdateReplyPeriod((Days * 24) + Hours);
            Offer offer = await offerQueries.FindOfferWithSupplierQuantitiesByOfferId(negotiation.OfferId);
            if (!await isLowestOfferorSameValue(offer.TenderId, offer.OfferId, NegotiationId))
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.SupplierOrederMustbeTheSameBeforeandAfterEdit);
            negotiation.UpdateNegotiationStatus((int)Enums.enNegotiationStatus.CheckManagerPendingApprove, ""); await _negotiationCommands.UpdateNegotiationAsync(negotiation);
            int? committeId = negotiation.AgencyCommunicationRequest.Tender.OffersCheckingCommitteeId;
            int codeId = NotificationOperations.OffersCheckManager.AgencyCommunicationRequest.SendSecondNegotitionToApprove;
            if (negotiation.AgencyCommunicationRequest.Tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || negotiation.AgencyCommunicationRequest.Tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            {
                committeId = negotiation.AgencyCommunicationRequest.Tender.DirectPurchaseCommitteeId;
                codeId = NotificationOperations.DirectPurchaseManager.AgencyCommunicationRequest.SendSecondNegotitionToApprove;
            }
            await _negotiationCommands.UpdateNegotiationAsync(negotiation);
            NotificationArguments NotificationArgumentsforSupplier = new NotificationArguments
            {
                BodyEmailArgs = new object[] { negotiation.AgencyCommunicationRequest.Tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { negotiation.AgencyCommunicationRequest.Tender.ReferenceNumber },
                SMSArgs = new object[] { negotiation.AgencyCommunicationRequest.Tender.ReferenceNumber }
            };
            MainNotificationTemplateModel mainNotificationTemplateModelforSupplier = new MainNotificationTemplateModel(NotificationArgumentsforSupplier,
            $"CommunicationRequest/CreateSecondNegotiationRequest/{Util.Encrypt(negotiation.NegotiationId)}",
            NotificationEntityType.Tender,
            negotiation.AgencyCommunicationRequest.Tender.TenderId.ToString(), negotiation.AgencyCommunicationRequest.Tender.BranchId, committeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(codeId, committeId, mainNotificationTemplateModelforSupplier);
        }

        public async Task ApproveNegotiationSecondStageByCheckManagerAsync(int NegotiationId, string verficationCode, int Days, int Hours)
        {
            Negotiation negotiation = await _negotiationQueries.FindNegotiationById(NegotiationId);
            var tender = negotiation.AgencyCommunicationRequest.Tender;
            if (tender.IsLowBudgetDirectPurchase.HasValue && tender.IsLowBudgetDirectPurchase.Value)
            {
                negotiation.UpdateReplyPeriod((Days * 24) + Hours);
                Offer offer = await offerQueries.FindOfferWithSupplierQuantitiesByOfferId(((NegotiationSecondStage)negotiation).OfferId);
                if (!await isLowestOfferorSameValue(offer.TenderId, offer.OfferId, NegotiationId))
                    throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.SupplierOrederMustbeTheSameBeforeandAfterEdit);
            }
            await _verification.CheckForVerificationCode(tender.TenderId, verficationCode, (int)Enums.VerificationType.Negotiation);
            _negotiationDomainService.IsValidToApproveOrRejectNegotiationSecondStageByCheckManagerAsync(negotiation);
            negotiation.UpdateNegotiationStatus((int)Enums.enNegotiationStatus.UnitSpecialestPendingApproved, "");
            await _negotiationCommands.UpdateNegotiationAsync(negotiation);

            int codeId = NotificationOperations.UnitSecrtaryLevel1.AgencyCommunicationRequest.SendNegotiationRequestToApprove;
            NotificationArguments NotificationArgumentsforSupplier = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel mainNotificationTemplateModelforSupplier = new MainNotificationTemplateModel(NotificationArgumentsforSupplier,
                $"CommunicationRequest/CreateSecondNegotiationRequest/{Util.Encrypt(negotiation.NegotiationId)}",
                NotificationEntityType.Tender,
                tender.TenderId.ToString());
            await _notificationAppService.SendNotificationForUsersByRoleName(codeId, RoleNames.UnitSpecialistLevel1.ToString(), mainNotificationTemplateModelforSupplier);
            codeId = NotificationOperations.UnitSecrtaryLevel2.AgencyCommunicationRequest.SendNegotiationRequestToApprove;
            NotificationArgumentsforSupplier = new NotificationArguments
            {
                BodyEmailArgs = new object[] { tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            mainNotificationTemplateModelforSupplier = new MainNotificationTemplateModel(NotificationArgumentsforSupplier,
                $"CommunicationRequest/CreateSecondNegotiationRequest/{Util.Encrypt(negotiation.NegotiationId)}",
                NotificationEntityType.Tender,
                tender.TenderId.ToString());
            await _notificationAppService.SendNotificationForUsersByRoleName(codeId, RoleNames.UnitSpecialistLevel2.ToString(), mainNotificationTemplateModelforSupplier);

            if (tender.IsLowBudgetDirectPurchase.HasValue && tender.IsLowBudgetDirectPurchase.Value)
            {
                UserProfile userProfile = await _iDMQueries.FindUserProfileByIdWithoutIncludesAsync(tender.DirectPurchaseMemberId.Value);
                await _notificationAppService.AddNotificationSettingByUserId(NotificationOperations.DirectPurchaseMember.AgencyCommunicationRequest.ApproveSecondStageNegotiationFromDirectPurchaseMember, userProfile, (int)Enums.UserRole.CR_DirectPurchaseMember);
                await _notificationAppService.SendNotificationByUserId(NotificationOperations.DirectPurchaseMember.AgencyCommunicationRequest.ApproveSecondStageNegotiationFromDirectPurchaseMember, tender.DirectPurchaseMemberId.Value, userProfile.UserName, mainNotificationTemplateModelforSupplier);
            }
        }

        public async Task RejectNegotiationSecondStageByCheckManagerAsync(int NegotiationId, string RejectionReason)
        {
            Negotiation negotiation = await _negotiationQueries.FindNegotiationById(NegotiationId);
            _negotiationDomainService.IsValidToApproveOrRejectNegotiationSecondStageByCheckManagerAsync(negotiation);
            negotiation.UpdateNegotiationStatus((int)Enums.enNegotiationStatus.CheckManagerReject, RejectionReason);
            await _negotiationCommands.UpdateNegotiationAsync(negotiation);
            int? committeId = negotiation.AgencyCommunicationRequest.Tender.OffersCheckingCommitteeId;
            int codeId = NotificationOperations.OffersCheckSecretary.AgencyCommunicationRequest.SendRejectionToSecretary;
            if (negotiation.AgencyCommunicationRequest.Tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || negotiation.AgencyCommunicationRequest.Tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            {
                committeId = negotiation.AgencyCommunicationRequest.Tender.DirectPurchaseCommitteeId;
                codeId = NotificationOperations.DirectPurchaseSecretary.AgencyCommunicationRequest.SendRejectionToSecretary;
            }
            await _negotiationCommands.UpdateNegotiationAsync(negotiation);
            NotificationArguments NotificationArgumentsforSupplier = new NotificationArguments
            {
                BodyEmailArgs = new object[] { negotiation.AgencyCommunicationRequest.Tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { negotiation.AgencyCommunicationRequest.Tender.ReferenceNumber },
                SMSArgs = new object[] { negotiation.AgencyCommunicationRequest.Tender.ReferenceNumber }
            };
            MainNotificationTemplateModel mainNotificationTemplateModelforSupplier = new MainNotificationTemplateModel(NotificationArgumentsforSupplier,
            $"CommunicationRequest/CreateSecondNegotiationRequest/{Util.Encrypt(negotiation.NegotiationId)}",
            NotificationEntityType.Tender,
            negotiation.AgencyCommunicationRequest.Tender.TenderId.ToString(), negotiation.AgencyCommunicationRequest.Tender.BranchId, committeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(codeId, committeId, mainNotificationTemplateModelforSupplier);
        }

        public async Task ApproveNegotiationSecondStageByUnitSecretaryAsync(int NegotiationId, string verficationCode)
        {
            NegotiationSecondStage negotiation = await _negotiationQueries.FindNegotiationSecondStageById(NegotiationId);
            await _verification.CheckForVerificationCode(negotiation.AgencyCommunicationRequest.TenderId, verficationCode, (int)Enums.VerificationType.Negotiation);
            _negotiationDomainService.IsValidToApproveOrRejectNegotiationSecondStageByUnitSecretaryAsync(negotiation);
            negotiation.UpdateSecondNegotiationStatus((int)Enums.enNegotiationStatus.SentToSuppliers);
            await _negotiationCommands.UpdateNegotiationAsync(negotiation);
            #region[Send Approval To Agency]

            int codeId = NotificationOperations.OffersCheckSecretary.AgencyCommunicationRequest.ApproveSecondNegotitionFromUnit;
            int? committeId = negotiation.AgencyCommunicationRequest.Tender.OffersCheckingCommitteeId;
            if (negotiation.AgencyCommunicationRequest.Tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || negotiation.AgencyCommunicationRequest.Tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            {
                codeId = NotificationOperations.DirectPurchaseSecretary.AgencyCommunicationRequest.ApproveSecondNegotitionFromUnit;
                committeId = negotiation.AgencyCommunicationRequest.Tender.DirectPurchaseCommitteeId;
            }
            await _negotiationCommands.UpdateNegotiationAsync(negotiation);
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { negotiation.AgencyCommunicationRequest.Tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { negotiation.AgencyCommunicationRequest.Tender.ReferenceNumber },
                SMSArgs = new object[] { negotiation.AgencyCommunicationRequest.Tender.ReferenceNumber }
            };

            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
            $"CommunicationRequest/CreateSecondNegotiationRequest/{Util.Encrypt(negotiation.NegotiationId)}",
            NotificationEntityType.Tender,
            negotiation.AgencyCommunicationRequest.Tender.TenderId.ToString(), negotiation.AgencyCommunicationRequest.Tender.BranchId, committeId);
            await _notificationAppService.SendNotificationForCommitteeUsers(codeId, committeId, mainNotificationTemplateModel);

            #endregion
            #region[Send Approval To Supplier]


            await _negotiationCommands.UpdateNegotiationAsync(negotiation);
            string supplierName = ((NegotiationSecondStage)negotiation).Offer.Supplier.SelectedCrName;
            NotificationArguments NotificationArgumentsforSupplier = new NotificationArguments
            {
                BodyEmailArgs = new object[] { supplierName, negotiation.AgencyCommunicationRequest.Tender.TenderName },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { supplierName, negotiation.AgencyCommunicationRequest.Tender.TenderName },
                SMSArgs = new object[] { supplierName, negotiation.AgencyCommunicationRequest.Tender.TenderName }
            };

            MainNotificationTemplateModel mainNotificationTemplateModelforSupplier = new MainNotificationTemplateModel(NotificationArgumentsforSupplier,
            $"CommunicationRequest/CreateSecondNegotiationRequest/{Util.Encrypt(negotiation.NegotiationId)}",
            NotificationEntityType.Tender,
            negotiation.AgencyCommunicationRequest.Tender.TenderId.ToString(), negotiation.AgencyCommunicationRequest.Tender.BranchId, committeId);
            await _notificationAppService.SendNotificationForSuppliers(
            NotificationOperations.Supplier.AgencyCommunicationRequest.SendSecondNegotiationToSupplier, new List<string> { ((NegotiationSecondStage)negotiation).Offer.CommericalRegisterNo }, mainNotificationTemplateModelforSupplier);

            #endregion
        }

        public async Task RejectNegotiationSecondStageByUnitSecretaryAsync(int NegotiationId, string RejectionReason)
        {
            Negotiation negotiation = await _negotiationQueries.FindNegotiationById(NegotiationId);
            _negotiationDomainService.IsValidToApproveOrRejectNegotiationSecondStageByUnitSecretaryAsync(negotiation);
            negotiation.UpdateNegotiationStatus((int)Enums.enNegotiationStatus.UnitSpecialistReject, RejectionReason);
            await _negotiationCommands.UpdateNegotiationAsync(negotiation);
            #region[Send Approval To Agency]
            int? committeId = negotiation.AgencyCommunicationRequest.Tender.OffersCheckingCommitteeId;

            int codeId = NotificationOperations.OffersCheckSecretary.AgencyCommunicationRequest.RejectUnitSecondNegotition;
            if (negotiation.AgencyCommunicationRequest.Tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || negotiation.AgencyCommunicationRequest.Tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            {
                codeId = NotificationOperations.DirectPurchaseSecretary.AgencyCommunicationRequest.RejectUnitSecondNegotition;
                committeId = negotiation.AgencyCommunicationRequest.Tender.DirectPurchaseCommitteeId;

            }
            await _negotiationCommands.UpdateNegotiationAsync(negotiation);
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { negotiation.AgencyCommunicationRequest.Tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { negotiation.AgencyCommunicationRequest.Tender.ReferenceNumber },
                SMSArgs = new object[] { negotiation.AgencyCommunicationRequest.Tender.ReferenceNumber }
            };

            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
            $"CommunicationRequest/CreateSecondNegotiationRequest/{Util.Encrypt(negotiation.NegotiationId)}",
            NotificationEntityType.Tender,
            negotiation.AgencyCommunicationRequest.Tender.TenderId.ToString(), negotiation.AgencyCommunicationRequest.Tender.BranchId, committeId);
            if (negotiation.AgencyCommunicationRequest.Tender.IsLowBudgetDirectPurchase.HasValue && negotiation.AgencyCommunicationRequest.Tender.IsLowBudgetDirectPurchase.Value)
            {
                UserProfile userProfile = await _iDMQueries.FindUserProfileByIdWithoutIncludesAsync(negotiation.AgencyCommunicationRequest.Tender.DirectPurchaseMemberId.Value);
                await _notificationAppService.AddNotificationSettingByUserId(NotificationOperations.DirectPurchaseMember.AgencyCommunicationRequest.RejectApproveSecondNegotiationFromUnitUser, userProfile, (int)Enums.UserRole.CR_DirectPurchaseMember);
                await _notificationAppService.SendNotificationByUserId(NotificationOperations.DirectPurchaseMember.AgencyCommunicationRequest.RejectApproveSecondNegotiationFromUnitUser, negotiation.AgencyCommunicationRequest.Tender.DirectPurchaseMemberId.Value, userProfile.UserName, mainNotificationTemplateModel);
            }
            else
            {
                await _notificationAppService.SendNotificationForCommitteeUsers(codeId, committeId, mainNotificationTemplateModel);
            }

            #endregion
        }

        public async Task EditNegotiationInfoAsync(int NegotiationId)
        {
            Negotiation negotiation = await _negotiationQueries.FindNegotiationById(NegotiationId);
            _negotiationDomainService.IsValidToEditOrFinishNegotiationSecondStageByCheckSecretaryAsync(negotiation);
            negotiation.UpdateNegotiationStatus((int)Enums.enNegotiationStatus.UnderUpdate, ""); await _negotiationCommands.UpdateNegotiationAsync(negotiation);
        }

        public async Task FinishNegotiationAsync(int NegotiationId)
        {
            Negotiation negotiation = await _negotiationQueries.FindNegotiationById(NegotiationId);
            _negotiationDomainService.IsValidToEditOrFinishNegotiationSecondStageByCheckSecretaryAsync(negotiation);
            negotiation.DeActive();
            await _negotiationCommands.UpdateNegotiationAsync(negotiation);
        }

        public async Task<string> ApproveNegotiationSecondStageBySupplierAsync(int NegotiationId)
        {
            NegotiationSecondStage negotiation = await _negotiationQueries.FindNegotiationSecondStageForSupplierById(NegotiationId);
            _negotiationDomainService.IsValidToApproveOrRejectNegotiationSecondStageBySupplierAsync(negotiation);
            if (DateTime.Now.Subtract(negotiation.PeriodStartDate.Value).TotalHours > negotiation.SupplierReplyPeriodHours)
            {
                negotiation.UpdateNegotiationStatus((int)Enums.enNegotiationStatus.SupplierNotAgreed, "");
                await _negotiationCommands.UpdateNegotiationAsync(negotiation);
                await _negotiationCommands.SaveChanges();
                throw new BusinessRuleException("إنتهى وقت الرد");
            }


            negotiation.UpdateNegotiationStatus((int)Enums.enNegotiationStatus.SupplierAgreed, "");
            negotiation.StartSuppierPeriod();
            var offer = await offerQueries.GetOfferById(negotiation.OfferId);
            var totalCost = await OfferFinalPriceAfterNegotiation(offer.OfferId, NegotiationId);
            var amountAfterNego = totalCost.TotalCostWithdiscount;
            var amountAfterNegoNP = totalCost.TotalCostNP;

            bool isTawreed = offer.Tender.QuantityTableVersionId == (int)Enums.QuantityTableVersion.Version2;
            offer.UpadatePriceAfterNegotiation(amountAfterNego);
            if (isTawreed)
                offer.UpadatePriceNPAfterNegotiation(amountAfterNegoNP);
            await offerCommands.UpdateAsync(offer);
            await _negotiationCommands.UpdateNegotiationAsync(negotiation);
            #region[Send Approval To Agency]
            NegotiationSecondStage negotiationfoNotification = await _negotiationQueries.FindNegotiationSecondStageById(NegotiationId);

            int? committeId = negotiationfoNotification.AgencyCommunicationRequest.Tender.OffersCheckingCommitteeId;

            int codeId = NotificationOperations.OffersCheckSecretary.AgencyCommunicationRequest.ApproveSecondNegotiationSupplier;
            if (negotiationfoNotification.AgencyCommunicationRequest.Tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || negotiationfoNotification.AgencyCommunicationRequest.Tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            {
                committeId = negotiationfoNotification.AgencyCommunicationRequest.Tender.DirectPurchaseCommitteeId;

                codeId = NotificationOperations.DirectPurchaseSecretary.AgencyCommunicationRequest.ApproveSecondNegotiationSupplier;
            }
            await _negotiationCommands.UpdateNegotiationAsync(negotiation);
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { _httpContextAccessor.HttpContext.User.SupplierName(), negotiationfoNotification.AgencyCommunicationRequest.Tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { _httpContextAccessor.HttpContext.User.SupplierName(), negotiationfoNotification.AgencyCommunicationRequest.Tender.ReferenceNumber },
                SMSArgs = new object[] { _httpContextAccessor.HttpContext.User.SupplierName(), negotiationfoNotification.AgencyCommunicationRequest.Tender.ReferenceNumber }
            };

            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
            $"CommunicationRequest/CreateSecondNegotiationRequest/{Util.Encrypt(negotiationfoNotification.NegotiationId)}",
            NotificationEntityType.Tender,
            negotiationfoNotification.AgencyCommunicationRequest.Tender.TenderId.ToString(), negotiationfoNotification.AgencyCommunicationRequest.Tender.BranchId, committeId);
            if (negotiationfoNotification.AgencyCommunicationRequest.Tender.IsLowBudgetDirectPurchase.HasValue && negotiationfoNotification.AgencyCommunicationRequest.Tender.IsLowBudgetDirectPurchase.Value)
            {
                UserProfile userProfile = await _iDMQueries.FindUserProfileByIdWithoutIncludesAsync(negotiationfoNotification.AgencyCommunicationRequest.Tender.DirectPurchaseMemberId.Value);
                await _notificationAppService.AddNotificationSettingByUserId(NotificationOperations.DirectPurchaseMember.AgencyCommunicationRequest.AcceptSecondNegotiationBySupplier, userProfile, (int)Enums.UserRole.CR_DirectPurchaseMember);
                await _notificationAppService.SendNotificationByUserId(NotificationOperations.DirectPurchaseMember.AgencyCommunicationRequest.AcceptSecondNegotiationBySupplier, negotiationfoNotification.AgencyCommunicationRequest.Tender.DirectPurchaseMemberId.Value, userProfile.UserName, mainNotificationTemplateModel);
            }
            else
            {
                await _notificationAppService.SendNotificationForCommitteeUsers(codeId, committeId, mainNotificationTemplateModel);
            }

            #endregion
            return amountAfterNego.ToString();
        }
        public async Task ReopenNegotiationSecondStageAsync(int NegotiationId)
        {
            Negotiation negotiation = await _negotiationQueries.FindNegotiationById(NegotiationId);
            _negotiationDomainService.IsValidToReopenNegotiation(negotiation);
            negotiation.UpdateNegotiationStatus((int)Enums.enNegotiationStatus.UnderUpdate, "");
            await _negotiationCommands.UpdateNegotiationAsync(negotiation);
        }

        public async Task ResetSecondNegotiation(int NegotiationId)
        {
            var negotiationSecondStage = await _negotiationQueries.FindSecondStageNegotiationbyId(NegotiationId);
            var offer = await offerQueries.FindOfferWithSupplierTenderQuantitiesByOfferId(negotiationSecondStage.OfferId);

            // var jsonItems = negotiationSecondStage.negotiationSupplierQuantitiestable.Select(f => f.NegotiationQuantityItemJson).ToList();
            foreach (var item in negotiationSecondStage.negotiationSupplierQuantitiestable.Where(f => f.NegotiationQuantityItemJson != null).Select(f => f.NegotiationQuantityItemJson).ToList())
            {
                item.Delete();
            }
            await _negotiationCommands.UpdateNegotiationSupplierQuantityTablesAsync(negotiationSecondStage.negotiationSupplierQuantitiestable);
            //foreach (var item in negotiationSecondStage.negotiationSupplierQuantitiestable.SelectMany(f => f.NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems))
            //{
            //    item.Delete();
            //    await _negotiationCommands.UpdateNegotiationSupplierQuantityTableItemAsync(item);
            //}
            await _negotiationCommands.SaveChanges();
            foreach (var tbl in negotiationSecondStage.negotiationSupplierQuantitiestable)
            {
                tbl.DeleteTable();
            }
            await _negotiationCommands.SaveChanges();
            negotiationSecondStage.AddSupplierQuantityTables(offer.SupplierTenderQuantityTables);
            await _negotiationCommands.UpdateNegotiationSecondStageAsync(negotiationSecondStage);
        }

        public async Task RejectNegotiationSecondStageBySupplierAsync(int NegotiationId)
        {
            NegotiationSecondStage negotiation = await _negotiationQueries.FindNegotiationSecondStageById(NegotiationId);
            if (DateTime.Now.Subtract(negotiation.PeriodStartDate.Value).TotalHours > negotiation.SupplierReplyPeriodHours)
            {
                negotiation.UpdateNegotiationStatus((int)Enums.enNegotiationStatus.SupplierNotAgreed, "");
                await _negotiationCommands.UpdateNegotiationAsync(negotiation);
                await
                   _negotiationCommands.SaveChanges();
                throw new BusinessRuleException("إنتهى وقت الرد");
            }
            _negotiationDomainService.IsValidToApproveOrRejectNegotiationSecondStageBySupplierAsync(negotiation);
            negotiation.UpdateNegotiationStatus((int)Enums.enNegotiationStatus.SupplierNotAgreed, ""); await _negotiationCommands.UpdateNegotiationAsync(negotiation);
            #region[Send Approval To Agency]
            string SupplierNumber = _httpContextAccessor.HttpContext.User.SupplierName();
            int? committeId = negotiation.AgencyCommunicationRequest.Tender.OffersCheckingCommitteeId;

            int codeId = NotificationOperations.OffersCheckSecretary.AgencyCommunicationRequest.RecectSecondNegotiationSupplier;
            if (negotiation.AgencyCommunicationRequest.Tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || negotiation.AgencyCommunicationRequest.Tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            {
                committeId = negotiation.AgencyCommunicationRequest.Tender.DirectPurchaseCommitteeId;

                codeId = NotificationOperations.DirectPurchaseSecretary.AgencyCommunicationRequest.RecectSecondNegotiationSupplier;
            }
            await _negotiationCommands.UpdateNegotiationAsync(negotiation);
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { SupplierNumber, negotiation.AgencyCommunicationRequest.Tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { SupplierNumber, negotiation.AgencyCommunicationRequest.Tender.ReferenceNumber },
                SMSArgs = new object[] { SupplierNumber, negotiation.AgencyCommunicationRequest.Tender.ReferenceNumber }
            };

            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
            $"CommunicationRequest/CreateSecondNegotiationRequest/{Util.Encrypt(negotiation.NegotiationId)}",
            NotificationEntityType.Tender,
            negotiation.AgencyCommunicationRequest.Tender.TenderId.ToString(), negotiation.AgencyCommunicationRequest.Tender.BranchId, committeId);
            if (negotiation.AgencyCommunicationRequest.Tender.IsLowBudgetDirectPurchase.HasValue && negotiation.AgencyCommunicationRequest.Tender.IsLowBudgetDirectPurchase.Value)
            {
                UserProfile userProfile = await _iDMQueries.FindUserProfileByIdWithoutIncludesAsync(negotiation.AgencyCommunicationRequest.Tender.DirectPurchaseMemberId.Value);
                await _notificationAppService.AddNotificationSettingByUserId(NotificationOperations.DirectPurchaseMember.AgencyCommunicationRequest.RejectSecondNegotiationBySupplier, userProfile, (int)Enums.UserRole.CR_DirectPurchaseMember);
                await _notificationAppService.SendNotificationByUserId(NotificationOperations.DirectPurchaseMember.AgencyCommunicationRequest.RejectSecondNegotiationBySupplier, negotiation.AgencyCommunicationRequest.Tender.DirectPurchaseMemberId.Value, userProfile.UserName, mainNotificationTemplateModel);
            }
            else
            {
                await _notificationAppService.SendNotificationForCommitteeUsers(codeId, committeId, mainNotificationTemplateModel);
            }

            #endregion
        }

        public async Task<AjaxResponse<OffersCompareViewModel>> DeleteNegotiationTenderQuantityTable(int tableId)
        {
            OffersCompareViewModel offersCompareViewModel = new OffersCompareViewModel();
            AjaxResponse<OffersCompareViewModel> response = new
                AjaxResponse<OffersCompareViewModel>();
            NegotiationSecondStage negotiationQuantityTable = await _negotiationQueries.GetNegotiationByQuantityTableId(tableId);
            int count = negotiationQuantityTable.negotiationSupplierQuantitiestable.Where(d => d.NegotiationQuantityItemJson != null && d.NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems.Any()).Count(f => f.Id != tableId);
            if (count == 0)
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.CantRemoveLastTable);
            var table = negotiationQuantityTable.negotiationSupplierQuantitiestable.FirstOrDefault(f => f.Id == tableId);
            List<Offer> offers = await offerQueries.GetOffersForSecondNegotiationByTenderId(negotiationQuantityTable.AgencyCommunicationRequest.TenderId);
            var offerId = negotiationQuantityTable.OfferId;
            var offer = await offerQueries.FindOfferById(negotiationQuantityTable.OfferId);

            var tables = negotiationQuantityTable.negotiationSupplierQuantitiestable.Where(d => d.Id != tableId && d.NegotiationQuantityItemJson != null).Select(t => new QuantitiesTemplateModel
            {
                QuantitiesItems = t.NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems.Select(i => new TenderQuantityItemDTO
                {
                    ColumnId = i.ColumnId,
                    Id = i.Id,
                    TableId = t.Id,
                    TemplateId = 0,
                    TenderFormHeaderId = i.TenderFormHeaderId,
                    Value = i.Value,
                    ItemNumber = i.ItemNumber
                }).ToList(),
            }).ToList();
            var items = tables.SelectMany(x => x.QuantitiesItems).ToList();

            //var items = negotiationQuantityTable.negotiationSupplierQuantitiestable.Where(d => d.Id != tableId).SelectMany(f => f.NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems).Select(item => new TenderQuantityItemDTO()
            //{
            //    ColumnId = item.ColumnId,
            //    Id = item.Id,
            //    TableId = item.NegotiationSupplierQuantityTableId,
            //    TenderFormHeaderId = item.TenderFormHeaderId,
            //    Value = item.Value,
            //    ItemNumber = item.ItemNumber
            //}).ToList();
            var amountAfter = await CalculateTotalOfferAmount(offer.TenderId, offer.Tender.TemplateYears.HasValue ? offer.Tender.TemplateYears.Value : 0, offer.Tender.NationalProductPercentage ?? 0, items);
            var tender = await _tenderQueries.FindTenderWithOffer(offer.TenderId);
            var amountofLowest = tender.Offers.Where
                (w => w.OfferAcceptanceStatusId == 1 && w.OfferTechnicalEvaluationStatusId == 1).OrderBy(d => d.FinalPriceAfterDiscount).First().FinalPriceAfterDiscount;
            if (amountAfter.TotalCostNP > amountofLowest)
            {
                {
                    offersCompareViewModel.isSaved = false;
                    response = new AjaxResponse<OffersCompareViewModel>();

                    negotiationQuantityTable.UpdateNegotiationStatus((int)Enums.enNegotiationStatus.New, "");
                    await _negotiationCommands.UpdateNegotiationSecondStageAsync(negotiationQuantityTable);
                    response = new AjaxResponse<OffersCompareViewModel>
                    {
                        enMessageType = enAjaxResponseMessageType.warnning,
                        Message = Resources.CommunicationRequest.ErrorMessages.SupplierOrederMustbeTheSameBeforeandAfterEdit
                    };
                    offersCompareViewModel.OldOffersCompareGrid = offers
                                  .Select(o => new OffersCompareGridViewModel(o.Supplier.SelectedCrName, o.CommericalRegisterNo,
                                  o.FinalPriceAfterDiscount.Value,
                                  Util.Encrypt(o.OfferId),
                                  Util.Encrypt(o.Combined.Where(c => c.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader).FirstOrDefault().Id), o.Combined.Count,
                                  (o.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer ? Resources.OfferResources.DisplayInputs.AccepteOffer : Resources.OfferResources.DisplayInputs.RejectedOffer),
                                  (o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer ? Resources.OfferResources.DisplayInputs.Matching : Resources.OfferResources.DisplayInputs.NotMatching),
                                  o.Tender.Invitations.Count > 0 && o.Tender.ConditionsBooklets.Count == 0 ?
                           o.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == o.CommericalRegisterNo).InvitationStatus.NameAr :
                           o.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == o.CommericalRegisterNo && x.BillInfo != null).BillInfo.BillStatusId == (int)Enums.BillStatus.Paid ? Resources.TenderResources.DisplayInputs.Purchased : Resources.TenderResources.DisplayInputs.NotPurchased
                           )).OrderBy(d => d.OfferPrice).ToList();
                    offersCompareViewModel.NewOffersCompareGrid = new List<OffersCompareGridViewModel>();
                    var newOffers = offersCompareViewModel.OldOffersCompareGrid
                                                   .Select(o => new OffersCompareGridViewModel(o.CommericalRegisterName, o.CommericalRegisterNo,
                                                    o.OfferPrice,
                                                   Util.Encrypt(o.OfferId),
                                                  Util.Decrypt(o.CombinedId), o.CombinedSupplierCount,
                                                 o.OfferAcceptanceStatus, o.OfferTechnicalEvaluationStatus, o.InvitationPurchaseStatus)).OrderBy(d => d.OfferPrice).ToList();
                    foreach (var of in newOffers)
                    {
                        if (of.OfferId == offerId)
                        {
                            of.OfferPrice = amountAfter.TotalCostWithdiscount;
                        }
                        offersCompareViewModel.NewOffersCompareGrid.Add(of);
                    }
                    offersCompareViewModel.NewOffersCompareGrid = offersCompareViewModel.NewOffersCompareGrid.OrderBy(d => d.OfferPrice).ToList();
                    response.Data = offersCompareViewModel;
                    return response;
                }
            }
            //foreach (var item in table.NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems)
            //{
            //    //item.Delete();
            //    //await _negotiationCommands.UpdateNegotiationSupplierQuantityTableItemAsync(item);
            //}

            // Replace Updating Item With Updating Table 
            if (table.NegotiationQuantityItemJson != null)
            {
                table.NegotiationQuantityItemJson.Delete();

            }
            await _negotiationCommands.UpdateNegotiationSupplierQuantityTablesAsync(new List<NegotiationSupplierQuantityTable> { table });


            await _negotiationCommands.SaveChanges();
            table.DeleteTable();
            await _negotiationCommands.UpdateNegotiationSecondStageAsync(negotiationQuantityTable);
            offersCompareViewModel.OldOffersCompareGrid = offers
                                          .Select(o => new OffersCompareGridViewModel(o.Supplier.SelectedCrName, o.CommericalRegisterNo,
                                          o.FinalPriceAfterDiscount.Value,
                                          Util.Encrypt(o.OfferId),
                                          Util.Encrypt(o.Combined.Where(c => c.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader).FirstOrDefault().Id), o.Combined.Count,
                                          (o.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer ? Resources.OfferResources.DisplayInputs.AccepteOffer : Resources.OfferResources.DisplayInputs.RejectedOffer),
                                          (o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer ? Resources.OfferResources.DisplayInputs.Matching : Resources.OfferResources.DisplayInputs.NotMatching),
                                          o.Tender.Invitations.Count > 0 && o.Tender.ConditionsBooklets.Count == 0 ?
                                          o.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == o.CommericalRegisterNo).InvitationStatus.NameAr :
                                          o.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == o.CommericalRegisterNo && x.BillInfo != null).BillInfo.BillStatusId == (int)Enums.BillStatus.Paid ? Resources.TenderResources.DisplayInputs.Purchased : Resources.TenderResources.DisplayInputs.NotPurchased
                                          )).OrderBy(d => d.OfferPrice).ToList();
            negotiationQuantityTable.UpdateNegotiationStatus((int)Enums.enNegotiationStatus.UnderUpdate, "");
            offersCompareViewModel.isSaved = true;
            response.Data = offersCompareViewModel;
            response.Message = Resources.SharedResources.Messages.DataSaved;
            response.enMessageType = enAjaxResponseMessageType.success;
            return response;
        }


        private async Task<TotalCostDTO> CalculateTotalOfferAmount(int tenderId, int templateYears, decimal _NPpercentage, List<TenderQuantityItemDTO> tenderQuantityItems)
        {
            FormConfigurationDTO DTOModel = new FormConfigurationDTO()
            {
                TenderId = tenderId,
                NPpercentage = _NPpercentage,
                ActivityId = 1,
                QuantityItemDtos = tenderQuantityItems,
                YearsCount = templateYears
            };
            ApiResponse<TotalCostDTO> validateResponse = await _quantityTemplatesProxy.CalculateOfferFinalPricebyItems(DTOModel);
            if (validateResponse.StatusCode != 200)
            {
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.QuantityTableServiceError);
            }
            return validateResponse.Data;
        }

        public async Task<NegotiationQuantityTableItemModel> UpdateNegotiationQuantityTableItem(NegotiationQuantityTableItemModel model)
        {
            return null;
        }


        #endregion

        private NegotiationFirstStageSupplier GetNextSupplier(NegotiationFirstStageSupplier firstStageSupplier, List<NegotiationFirstStageSupplier> negotiationFirstStageSuppliers)
        {
            var OrderedSupplierList = negotiationFirstStageSuppliers.OrderBy(o => o.Id).ToList();
            int currentIndex = OrderedSupplierList.IndexOf(firstStageSupplier);
            currentIndex += 1;
            if (currentIndex >= OrderedSupplierList.Count)
            {
                return null;
            }
            var nextSupplier = OrderedSupplierList[currentIndex];
            return nextSupplier;
        }

        public async Task DeleteNegotiationQuantityItem(string quantityItemId)
        {
            //var quantityItem = await _negotiationQueries.FindNegotiationQuantityItem(Util.Decrypt(quantityItemId));
            //quantityItem.deleteQuantityItem();
            //await _negotiationCommands.UpdateNegotiationQuantityTableItem(quantityItem);
        }

        public async Task UpdateNegotiationQuantityItem(string quantityItemId, int QTY)
        {
            //var quantityItem = await _negotiationQueries.FindNegotiationQuantityItem(Util.Decrypt(quantityItemId));
            //quantityItem.updateQTY(QTY);
            //await _negotiationCommands.UpdateNegotiationQuantityTableItem(quantityItem);
        }

        public async Task<OfferQuantityTableHtmlVM> GetNegotiationQuantityTables(int negotiationId)
        {
            QuantitiesTemplateModel lst = new QuantitiesTemplateModel();
            var negotiation = await _negotiationQueries.FindSecondStageNegotiationbyId(negotiationId);
            var Qitems = await _negotiationQueries.FindSecondStageNegotiationbyTQIId(negotiationId);
            var offer = await offerQueries.FindOfferWithStatusById(negotiation.OfferId);
            lst.QuantitiesItems = Qitems;
            FormConfigurationDTO DTOModel = new FormConfigurationDTO()
            {
                EncryptedNegotiationId = Util.Encrypt(negotiationId),
                TenderId = offer.TenderId,
                HasAlternative = false,
                ActivityId = 1,
                EncryptedOfferId = Util.Encrypt(offer.OfferId),
                EncryptedTenderId = Util.Encrypt(offer.TenderId),
                QuantityItemDtos = lst.QuantitiesItems.ToList(),
                YearsCount = (offer.Tender.TemplateYears.HasValue ? offer.Tender.TemplateYears.Value : 0)
            };
            ApiResponse<List<HtmlTemplateDto>> obj;
            if ((negotiation.StatusId == (int)Enums.enNegotiationStatus.UnderUpdate || negotiation.StatusId == (int)Enums.enNegotiationStatus.New) && (_httpContextAccessor.HttpContext.User.UserRole() == RoleNames.OffersPurchaseSecretary.ToString() || (_httpContextAccessor.HttpContext.User.UserRole() == RoleNames.OffersCheckSecretary.ToString())))
            {
                obj = await _quantityTemplatesProxy.GenerateNegotiationTemplate(DTOModel);
                OfferQuantityTableHtmlVM offerQuantityTableHtmlVM = new OfferQuantityTableHtmlVM
                {
                    statusId = negotiation.StatusId,
                    offerStatusModel = new OfferStatusModel { offerIdString = Util.Encrypt(offer.OfferId), OfferStatusId = offer.OfferStatusId, offerStatusName = offer.Status.NameAr }
                };
                if (obj.StatusCode == 200)
                {
                    foreach (var tbl in obj.Data)
                    {
                        offerQuantityTableHtmlVM.tableFormHtmls.Add(new TableFormHtml { FormId = tbl.FormId, TableName = tbl.FormName, FormHtml = tbl.FormHtml, tableId = long.Parse(tbl.TableId), DeleteFormHtml = tbl.DeleteFormHtml, EditFormHtml = tbl.EditFormHtml, JsScript = tbl.JsScript, TemplateName = tbl.TemplateName });
                    }
                }
                return offerQuantityTableHtmlVM;
            }
            else
            {
                obj = await _quantityTemplatesProxy.GenerateNegotiationReadOnlyTemplate(DTOModel);
                OfferQuantityTableHtmlVM offerQuantityTableHtmlVM = new OfferQuantityTableHtmlVM
                {
                    offerStatusModel = new OfferStatusModel { offerIdString = Util.Encrypt(offer.OfferId), OfferStatusId = offer.OfferStatusId, offerStatusName = offer.Status.NameAr },
                    statusId = negotiation.StatusId
                };
                if (obj.StatusCode == 200)
                {
                    foreach (var tbl in obj.Data)
                    {
                        offerQuantityTableHtmlVM.tableFormHtmls.Add(new TableFormHtml { FormId = tbl.FormId, TableName = tbl.FormName, FormHtml = tbl.FormHtml, tableId = long.Parse(tbl.TableId), DeleteFormHtml = tbl.DeleteFormHtml, EditFormHtml = tbl.EditFormHtml, JsScript = tbl.JsScript, TemplateName = tbl.TemplateName });
                    }
                }
                return offerQuantityTableHtmlVM;
            }
        }

        public async Task<AjaxResponse<OffersCompareViewModel>> SaveNegotiationQitems(Dictionary<string, string> AuthorList)
        {
            #region [VARIABLES ]
            int tenderId = Util.Decrypt(AuthorList["EncryptedTenderId"]);
            int _negoId = Util.Decrypt(AuthorList["EncryptedNegotiationId"]);
            int offerId = Util.Decrypt(AuthorList["EncryptedOfferId"]);
            int formId = int.Parse(AuthorList["FormId"]);
            int tableId = int.Parse(AuthorList["TableId"]);
            Offer offer = await offerQueries.FindOfferWithSupplierTenderQuantitiesByOfferId(offerId);
            var tender = await _tenderQueries.FindTenderWithOffer(tenderId);
            var isTawreed = tender.QuantityTableVersionId == (int)Enums.QuantityTableVersion.Version2;
            var apiData = new List<TenderQuantityItemDTO>();
            var deleteData = new List<TenderQuantityItemDTO>();
            NegotiationSecondStage negotiation = await _negotiationQueries.FindSecondStageNegotiationWithTablesbyId(_negoId);
            var negotiationSupplierQuantitiestables = negotiation.negotiationSupplierQuantitiestable;//await  _negotiationQueries.GetNegotiationSupplierQuantitiestable(_negoId);
            var tableItems = negotiationSupplierQuantitiestables.FirstOrDefault(d => d.Id == tableId).NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems.ToList();

            List<Offer> offers = await offerQueries.GetOffersForSecondNegotiationByTenderId(tenderId);
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
            //var otherTableItems = negotiationSupplierQuantitiestables.Where(d => d.Id != tableId).SelectMany(s => s.NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems).ToList();


            var otherTableItems = negotiationSupplierQuantitiestables.Where(d => d.Id != tableId).Select(t => new QuantitiesTemplateModel
            {
                QuantitiesItems = t.NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems.Select(i => new TenderQuantityItemDTO
                {
                    ColumnId = i.ColumnId,
                    Id = i.Id,
                    TableId = t.Id,
                    TenderFormHeaderId = i.TenderFormHeaderId,
                    Value = i.Value,
                    ItemNumber = i.ItemNumber
                }).ToList(),
            }).ToList();
            var otherItems = otherTableItems.SelectMany(x => x.QuantitiesItems).ToList();
            //var otherItems = otherTableItems.Select(item => new TenderQuantityItemDTO()
            //{
            //    ColumnId = item.ColumnId,
            //    Id = item.Id,
            //    TableId = item.NegotiationSupplierQuantityTableId,
            //    TenderFormHeaderId = item.TenderFormHeaderId,
            //    Value = item.Value,
            //    ItemNumber = item.ItemNumber,
            //    IsDefault = true
            //}).ToList();
            DTOModel.QuantityItemDtos.AddRange(otherItems);
            var CurrentTableitems = validateResponse.Data[0].QuantityItemDtos.Select(item => new TenderQuantityItemDTO()
            {
                ColumnId = item.ColumnId,
                Id = item.Id,
                TableId = item.TableId,
                TenderFormHeaderId = item.TenderFormHeaderId,
                Value = item.Value,
                ItemNumber = item.ItemNumber,
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
                await _negotiationCommands.UpdateNegotiationSecondStageAsync(negotiation);

                offersCompareViewModel.isSaved = false;
                response = new AjaxResponse<OffersCompareViewModel>();
                if (offers.Count() == 0)
                {
                }
                response = new AjaxResponse<OffersCompareViewModel>();
                response.enMessageType = enAjaxResponseMessageType.warnning;
                response.Message = Resources.CommunicationRequest.ErrorMessages.SupplierOrederMustbeTheSameBeforeandAfterEdit;

                if (offers.Count() == 0)
                {
                }
                OrderOldOffers(offerId, isTawreed, offers, offersCompareViewModel, offerFinalPriceResponse);
                negotiation = await _negotiationQueries.FindSecondStageNegotiationbyId(_negoId);
                tableItems = negotiationSupplierQuantitiestables.FirstOrDefault(d => d.Id == tableId).NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems.ToList();

                DTOModel.QuantityItemDtos = tableItems.Select(item => new TenderQuantityItemDTO()
                {
                    ColumnId = item.ColumnId,
                    Id = item.Id,
                    TableId = tableId,
                    TenderFormHeaderId = item.TenderFormHeaderId,
                    Value = item.Value,
                    ItemNumber = item.ItemNumber
                }).ToList();
                ApiResponse<List<HtmlTemplateDto>> objj;
                objj = await _quantityTemplatesProxy.GenerateNegotiationTemplate(DTOModel);
                response.htmlData = objj.Data.FirstOrDefault().EditFormHtml;
                response.tableId = tableId;
                response.Data = offersCompareViewModel;
                return response;
                #endregion
            }
            #region [ESTABLISH RESPONSE IN CASE OF ORDER DIDNT AFFECTED AND UPDATE DB]
            OrederOffers(isTawreed, offers, offersCompareViewModel, offerId, amountAfterNego, amountAfterNegoNP);

            negotiation.UpdateNegotiationStatus((int)Enums.enNegotiationStatus.UnderUpdate, "");
            negotiation.UpdateNegotiationQItems(DTOModel.QuantityItemDtos.Where(e => e.TableId == tableId && itemNumbers.Any(dd => dd == e.ItemNumber)).ToList()/*, negotiationSupplierQuantitiestable*/);
            negotiation = await _negotiationCommands.UpdateNegotiationSecondStageAsync(negotiation);
            var itemsforDelete = apiData.Where(e => e.TableId == tableId && !itemNumbers.Any(dd => dd == e.ItemNumber)).ToList();
            negotiation.DeleteNegotiationQItems(itemsforDelete/*, negotiationSupplierQuantitiestable*/);
            await _negotiationCommands.UpdateNegotiationSecondStageAsync(negotiation);
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



            var newOffers = offersCompareViewModel.OldOffersCompareGrid
                                        .Select(o => new OffersCompareGridViewModel(o.CommericalRegisterName, o.CommericalRegisterNo,
                                         o.OfferPrice,
                                        Util.Encrypt(o.OfferId),
                                       Util.Encrypt(o.CombinedId), o.CombinedSupplierCount,
                                      o.OfferAcceptanceStatus, o.OfferTechnicalEvaluationStatus
                                    ,
                                      o.InvitationPurchaseStatus, isTawreed)).OrderBy(d => isTawreed ? d.OfferPriceNP : d.OfferPrice).ToList();



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

        private async Task<TotalCostDTO> OfferFinalPriceAfterNegotiation(int offerId, int NegotiationId)
        {
            var apiData = new List<TenderQuantityItemDTO>();
            NegotiationSecondStage negotiation = await _negotiationQueries.FindSecondStageNegotiationbyId(NegotiationId);
            var tables = negotiation.negotiationSupplierQuantitiestable.Select(t => new QuantitiesTemplateModel
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
            var items = tables.SelectMany(x => x.QuantitiesItems).ToList();
            apiData.AddRange(items);


            OffersCompareViewModel offersCompareViewModel = new OffersCompareViewModel();
            FormConfigurationDTO DTOModel = new FormConfigurationDTO()
            {
                TenderId = offerId,
                ActivityId = 1,
                QuantityItemDtos = apiData,
                YearsCount = 0,
                NPpercentage = (negotiation.Offer.Tender.NationalProductPercentage.HasValue ? negotiation.Offer.Tender.NationalProductPercentage.Value : 0) / 100
            };
            ApiResponse<TotalCostDTO> offerFinalPriceResponse = await _quantityTemplatesProxy.CalculateOfferFinalPricebyItems(DTOModel);
            if (offerFinalPriceResponse.StatusCode != 200)
                return new TotalCostDTO();


            return offerFinalPriceResponse.Data;
        }

        private async Task<bool> isLowestOfferorSameValue(int tenderId, int offerId, int NegotiationId)
        {
            List<Offer> offers = await offerQueries.GetOffersForSecondNegotiationByTenderId(tenderId);

            Offer lowestOffer = offers.OrderBy(d => (d.OfferWeightAfterCalcNPA ?? 0)).FirstOrDefault();
            var secondofferoldamount = offers.Count > 1 && lowestOffer.Tender.TenderActivities.Any(d => d.ActivityId == (int)Enums.ConditionsTemplateCategory.GeneralSuppliesSupply) ?
                ((offers.OrderBy(d => d.OfferWeightAfterCalcNPA).ToArray()[1]).OfferWeightAfterCalcNPA.Value) : offers.Count > 1 ? (offers.OrderBy(d => d.FinalPriceAfterDiscount).ToArray()[1]).FinalPriceAfterDiscount.Value : 0;


            var totalCost = await OfferFinalPriceAfterNegotiation(offerId, NegotiationId);
            var amountAfterNego = lowestOffer.Tender.TenderActivities.Any(d => d.ActivityId == (int)Enums.ConditionsTemplateCategory.GeneralSuppliesSupply) ? totalCost.TotalCostNP : totalCost.TotalCostWithdiscount;
            if (amountAfterNego <= 0)
                throw new BusinessRuleException("قيمة العرض لا يمكن ان تكون صفر ");

            var isSameOrder = (secondofferoldamount == 0 || secondofferoldamount > amountAfterNego);

            var firstofferoldamount = lowestOffer.Tender.TenderActivities.Any(d => d.ActivityId == (int)Enums.ConditionsTemplateCategory.GeneralSuppliesSupply) ?
                     ((offers.OrderBy(d => d.OfferWeightAfterCalcNPA).ToArray()[0]).OfferWeightAfterCalcNPA.Value) : (offers.OrderBy(d => d.FinalPriceAfterDiscount).ToArray()[0]).FinalPriceAfterDiscount.Value;



            if (firstofferoldamount == amountAfterNego)
            {
                throw new BusinessRuleException("لابد من تغير البيانات والضغط على حفظ  ");
            }
            return isSameOrder;
        }


        #region New-Negotiation-FirstStage
        #endregion
    }

}



