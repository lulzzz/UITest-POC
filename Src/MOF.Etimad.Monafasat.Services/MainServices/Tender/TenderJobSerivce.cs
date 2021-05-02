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
    public class TenderJobSerivce : ITenderJobSerivce
    {
        private readonly INotificationAppService _notifayAppService = null;
        private readonly IGenericCommandRepository _genericCommandRepository;
        private readonly ITenderQueries _tenderQueries;
        private readonly ITenderCommands _tenderCommands;

        public TenderJobSerivce(INotificationAppService notifayAppService, IGenericCommandRepository genericCommandRepository, ITenderQueries tenderQueries, ITenderCommands tenderCommands)
        {
            _notifayAppService = notifayAppService;
            _genericCommandRepository = genericCommandRepository;
            _tenderQueries = tenderQueries;
            _tenderCommands = tenderCommands;
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
                var suppliers = round.Tender.Offers.Where(o => o.IsActive == true && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer)
                            .Select(o => o.Supplier).ToList();
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
            foreach (var tender in tenders)
            {
                NotificationArguments NotificationArguments = new NotificationArguments
                {
                    BodyEmailArgs = new object[] { tender.OffersOpeningDate.Value.ToShortDateString(), tender.ReferenceNumber },
                    SubjectEmailArgs = new object[] { },
                    PanelArgs = new object[] { tender.OffersOpeningDate.Value.ToShortDateString(), tender.ReferenceNumber },
                    SMSArgs = new object[] { tender.OffersOpeningDate.Value.ToShortDateString(), tender.ReferenceNumber }
                };

                MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
                $"Tender/OpenTenderOffers/tenderIdString?={Util.Encrypt(tender.TenderId)}",
                NotificationEntityType.Tender,
                tender.TenderId.ToString(), null, tender.OffersOpeningCommitteeId);
                await _notifayAppService.SendNotificationForCommitteeUsers(NotificationOperations.OffersOppeningSecretary.OperationsOnTheTender.OffersWillOpenTomorrow, tender.OffersOpeningCommitteeId, mainNotificationTemplateModel);
                tender.UpdateOfferOpeningNotificationStatus();
            }

            if (tenders.Count() > 0)
                await _tenderCommands.UpdateTendersAsync(tenders);

            return true;
        }

        public async Task<bool> GetTenderOffersForChecking()
        {
            List<Tender> tenders = await _tenderQueries.FindTendersToCheckOffers();
            foreach (var tender in tenders)
            {
                NotificationArguments NotificationArguments = new NotificationArguments
                {
                    BodyEmailArgs = new object[] { tender.OffersCheckingDate.Value.ToShortDateString(), tender.ReferenceNumber },
                    SubjectEmailArgs = new object[] { },
                    PanelArgs = new object[] { tender.OffersCheckingDate.Value.ToShortDateString(), tender.ReferenceNumber },
                    SMSArgs = new object[] { tender.OffersCheckingDate.Value.ToShortDateString(), tender.ReferenceNumber }
                };

                MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
                    // $"Tender/OpenTenderOffers/tenderIdString?={Util.Encrypt(tender.TenderId)}", 
                    $"Tender/OfferChecking/tenderIdString?={Util.Encrypt(tender.TenderId)}",
                NotificationEntityType.Tender,
                tender.TenderId.ToString(), null, tender.DirectPurchaseCommitteeId);
                //to do
                await _notifayAppService.SendNotificationForCommitteeUsers(NotificationOperations.DirectPurchaseManager.OperationsOnTheTender.offersWillCheckingTomorrow, tender.DirectPurchaseCommitteeId, mainNotificationTemplateModel);
                tender.UpdateOfferCheckingNotificationStatus();
            }

            if (tenders.Any())
                await _tenderCommands.UpdateTendersAsync(tenders);

            return true;
        }

    }
}
