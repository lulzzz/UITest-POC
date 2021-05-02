using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Integration;
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
    public class TenderAppJobService : ITenderAppJobService
    {
        private readonly IQuantityTemplatesProxy _templatesProxy;
        private readonly ITenderJobQueries _tenderQueries;
        private readonly IIDMJobQueries _idmjobqueries;
        private readonly IGenericCommandRepository _genericCommandRepository;
        private readonly ISRMFrameworkAgreementManageProxy _sRMFrameworkAgreementManageProxy;
        private readonly INotificationJobAppService _notificationJobAppService;

        public TenderAppJobService(IIDMJobQueries iDMJobQueries, IQuantityTemplatesProxy quantityTemplatesProxy, ITenderJobQueries tenderQueries, IGenericCommandRepository genericCommandRepository,
        ISRMFrameworkAgreementManageProxy sRMFrameworkAgreementManageProxy, INotificationJobAppService notificationJobAppService)
        {
            _templatesProxy = quantityTemplatesProxy;
            _tenderQueries = tenderQueries;
            _genericCommandRepository = genericCommandRepository;
            _sRMFrameworkAgreementManageProxy = sRMFrameworkAgreementManageProxy;
            _notificationJobAppService = notificationJobAppService;
            _idmjobqueries = iDMJobQueries;
        }
        public async Task SendFinalAwardedTendersToEmarketPlace()
        {
            var tenders = await _tenderQueries.GetAllFinalAwardedTendersForEmarketPlace();
            if (tenders.Any())
            {
                foreach (var tender in tenders)
                {
                    var awardedTenderOffers = tender.Offers.Where(x => x.IsActive == true && (x.TotalOfferAwardingValue != null || x.PartialOfferAwardingValue != null)).ToList();

                    if (awardedTenderOffers.Any())
                    {
                        var isSentToEmarketPlace = await SendToEmarketPlace(awardedTenderOffers.Select(o => o.OfferId).ToList());

                        if (isSentToEmarketPlace)
                        {
                            tender.UpdateEmarketPlaceStatus();
                            _genericCommandRepository.Update(tender);
                        }
                    }
                }
                await _genericCommandRepository.SaveAsync();
            }
        }

        private async Task<bool> SendToEmarketPlace(List<int> offerIds)
        {
            var columns = await _tenderQueries.GetAwardedSupplierQuantityTableItemsValue(offerIds);
            ApiResponse<List<EmarketPlaceResponse>> list = await _templatesProxy.GetEmarketPlace(columns);

            if (list.Data != null)
            {
                var emarketPlaceModel = await _tenderQueries.FillVendorProducts(offerIds, list.Data);
                var result = await _sRMFrameworkAgreementManageProxy.SRMFrameworkAgreementManage(emarketPlaceModel);
                if (!result)
                    return false;
                return result;
            }
            else
            {
                return false;
            }
        }

        public async Task SendNotificatoinAfterFinishTendersStoppingPeriod()
        {
            var tenders = await _tenderQueries.GetAllFinishedStoppingAwardingPeriodTenders();
            if (tenders != null && tenders.Any())
            {
                var agencies = await _tenderQueries.FindAgenciesByAgencyCodes(tenders.Select(t => t.AgencyCode).Distinct().ToList());
                int? agencyCategoryId = 0;
                foreach (var tender in tenders)
                {
                    agencyCategoryId = agencies.FirstOrDefault(a => a.AgencyCode == tender.AgencyCode).CategoryId;
                    //update database flag
                    tender.UpdateFinishedStoppingAwardingPeriodTendersStatus();
                    await SendNotificationFinishStoppingPeroiod(tender, agencyCategoryId);
                    _genericCommandRepository.Update(tender);
                }
                await _genericCommandRepository.SaveAsync();
            }
        }
        private async Task SendNotificationFinishStoppingPeroiod(Tender tender, int? agencyCategoryId)
        {
            NotificationArguments NotificationArgument = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", tender.ReferenceNumber, tender.Purpose, tender.LastEnqueriesDate.Value.ToString("dd/MM/yyyy"), tender.LastOfferPresentationDate.Value.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("dd/MM/yyyy"), tender.OffersOpeningDate == null ? Resources.SharedResources.DisplayInputs.NotFound : tender.OffersOpeningDate?.ToString("hh:mm tt") },
                PanelArgs = new object[] { tender.ReferenceNumber },
                SMSArgs = new object[] { tender.ReferenceNumber }
            };
            MainNotificationTemplateModel StopPeriod = new MainNotificationTemplateModel(NotificationArgument, $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), 0, tender.OffersCheckingCommitteeId);
            MainNotificationTemplateModel StopPeriodDirectPurchase = new MainNotificationTemplateModel(NotificationArgument, $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}", NotificationEntityType.Tender, tender.TenderId.ToString(), 0, tender.DirectPurchaseCommitteeId);
            if (tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
                await _notificationJobAppService.SendNotifications(NotificationOperations.DirectPurchaseSecretary.OperationsOnTheTender.FinishStoppingPeriod, tender.AgencyCode, agencyCategoryId.Value, StopPeriodDirectPurchase, Enums.UserRole.NewMonafasat_SecretaryDirtectPurshasingCommittee.ToString(), (int)tender.DirectPurchaseCommitteeId);
            else
                await _notificationJobAppService.SendNotifications(NotificationOperations.OffersCheckSecretary.OperationsOnTheTender.FinishStoppingPeriod, tender.AgencyCode, agencyCategoryId.Value, StopPeriod, Enums.UserRole.NewMonafasat_OffersCheckSecretary.ToString(), (int)tender.OffersCheckingCommitteeId);
        }

        public async Task<bool> CheckBiddingTenderRounds()
        {
            try
            {
                await FinishCompletedBiddingRounds();
                await StartPendingBiddingRounds();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        async Task<bool> FinishCompletedBiddingRounds()
        {
            List<BiddingRound> biddingRounds = await _tenderQueries.FindFinishedBiddingRounds();

            foreach (var round in biddingRounds)
            {
                round.UpdateStatus((int)Enums.BiddingRoundStatus.Stopped);
                _genericCommandRepository.Update(round);
            }
            await _genericCommandRepository.SaveAsync();
            return true;
        }

        async Task<bool> StartPendingBiddingRounds()
        {
            List<BiddingRound> biddingRounds = await _tenderQueries.FindPendingBiddingRounds();

            foreach (var round in biddingRounds)
            {
                round.UpdateStatus((int)Enums.BiddingRoundStatus.Started);
                round.Tender.UpdateTenderStatus(Enums.TenderStatus.Bidding);
                _genericCommandRepository.Update(round.Tender);
                _genericCommandRepository.Update(round);
            }
            await _genericCommandRepository.SaveAsync();
            return true;
        }

        public async Task<bool> GetTenderOffersForOpening(int id)
        {
            List<Tender> tenders = await _tenderQueries.FindTendersToOpenOffers(0);
            var agencies = await _tenderQueries.FindAgenciesByAgencyCodes(tenders.Select(t => t.AgencyCode).Distinct().ToList());
            int? agencyCategoryId = 0;
            foreach (var tender in tenders)
            {
                agencyCategoryId = agencies.Where(a => a.AgencyCode == tender.AgencyCode).FirstOrDefault().CategoryId;
                NotificationArguments NotificationArguments = new NotificationArguments
                {
                    BodyEmailArgs = new object[] { tender.OffersOpeningDate.Value.ToShortDateString(), tender.ReferenceNumber },
                    SubjectEmailArgs = new object[] { },
                    PanelArgs = new object[] { tender.OffersOpeningDate.Value.ToShortDateString(), tender.ReferenceNumber },
                    SMSArgs = new object[] { tender.OffersOpeningDate.Value.ToShortDateString(), tender.ReferenceNumber }
                };

                MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
                $"Tender/OpenTenderOffers?tenderIdString={Util.Encrypt(tender.TenderId)}",
                NotificationEntityType.Tender,
                tender.TenderId.ToString(), null, tender.OffersOpeningCommitteeId);
                await _notificationJobAppService.SendNotifications(NotificationOperations.OffersOppeningSecretary.OperationsOnTheTender.OffersWillOpenTomorrow, tender.AgencyCode, agencyCategoryId.Value, mainNotificationTemplateModel, Enums.UserRole.NewMonafasat_OffersOpeningSecretary.ToString(), (int)tender.OffersOpeningCommitteeId);
                tender.UpdateOfferOpeningNotificationStatus();
                _genericCommandRepository.Update(tender);
            }

            if (tenders.Any())
            {
                await _genericCommandRepository.SaveAsync();
            }
            return true;
        }

        public async Task<bool> GetTenderOffersForChecking()
        {
            List<Tender> tenders = await _tenderQueries.FindTendersToCheckOffers();
            var agencies = await _tenderQueries.FindAgenciesByAgencyCodes(tenders.Select(t => t.AgencyCode).Distinct().ToList());
            int? agencyCategoryId = 0;

            #region New Notification Per User
            var lowBudgetFlowTenders = tenders.Where(w => w.IsLowBudgetDirectPurchase == true && w.DirectPurchaseCommitteeId == null).ToList();
            var directpurchaseMembers = lowBudgetFlowTenders.Any() ? lowBudgetFlowTenders.Select(d => d.DirectPurchaseMemberId).ToList() : new List<int?>();
            var userProfiles = await _idmjobqueries.FindUsersProfileByIdAsync(directpurchaseMembers);
            await _notificationJobAppService.AddNotificationSettingByUserId(NotificationOperations.DirectPurchaseMember.OperationsOnTheTender.offersWillCheckingTomorrowPurchaseMember, userProfiles, (int)Enums.UserRole.CR_DirectPurchaseMember);
            #endregion

            foreach (var tender in tenders)
            {
                bool isLowBudgetFlow = tender.IsLowBudgetDirectPurchase.HasValue && tender.IsLowBudgetDirectPurchase.Value == true;
                agencyCategoryId = agencies.FirstOrDefault(a => a.AgencyCode == tender.AgencyCode).CategoryId;
                NotificationArguments NotificationArguments = new NotificationArguments
                {
                    BodyEmailArgs = new object[] { tender.OffersCheckingDate.Value.ToShortDateString(), tender.ReferenceNumber },
                    SubjectEmailArgs = new object[] { },
                    PanelArgs = new object[] { tender.OffersCheckingDate.Value.ToShortDateString(), tender.ReferenceNumber },
                    SMSArgs = new object[] { tender.OffersCheckingDate.Value.ToShortDateString(), tender.ReferenceNumber }
                };

                MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
                    // $"Tender/OpenTenderOffers/tenderIdString?={Util.Encrypt(tender.TenderId)}", 
                    $"Tender/CheckDirectPurchaseOffers/tenderIdString?={Util.Encrypt(tender.TenderId)}",
                NotificationEntityType.Tender,
                tender.TenderId.ToString(), null, tender.DirectPurchaseCommitteeId);
                //to do

                if (isLowBudgetFlow)
                {
                    var userprofile = userProfiles.FirstOrDefault(d => d.Id == tender.DirectPurchaseMemberId);
                    await _notificationJobAppService.SendNotificationByUserId(NotificationOperations.DirectPurchaseMember.OperationsOnTheTender.offersWillCheckingTomorrowPurchaseMember, tender.DirectPurchaseMemberId.Value, userprofile.UserName, mainNotificationTemplateModel);
                }
                else
                {
                    await _notificationJobAppService.SendNotifications(NotificationOperations.DirectPurchaseManager.OperationsOnTheTender.offersWillCheckingTomorrow, tender.AgencyCode, agencyCategoryId.Value, mainNotificationTemplateModel, Enums.UserRole.NewMonafasat_ManagerDirtectPurshasingCommittee.ToString(), (int)tender.DirectPurchaseCommitteeId);
                }
                tender.UpdateOfferCheckingNotificationStatus();
                _genericCommandRepository.Update(tender);
            }

            if (tenders.Any())
            {
                await _genericCommandRepository.SaveAsync();
            }

            return true;
        }
    }
}
