using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IOfferDomainService
    {
        Task IsValidToCreate(Offer offer);
        void IsValidToSend(Offer offer, Tender tender);
        void IsValidAndSent(Offer offer, Tender tender);
        void IsValidToUpdateTwoFilesFinancialDetails(Tender tender, Offer offer, TenderTowFilesFinancialDetailsDetails model);
        void IsValidToUpdateTwoFilesTechnicalCheck(Tender tender, Offer offer, OfferDetailsForCheckingTwoFilesModel model);
        void IsVaildToAcceptOrRejectCombinedInvitation(OfferSolidarity combined);
        void IsValidAddBidding(Tender tenderBiddingDetails, List<Offer> offers, int biddingRoundId, decimal biddingValue, string cR);
        Task<Tender> UpdateTenderAwardingTypeAndStoppingPeriodAndAwardingReport(int tenderId, bool? awardingType, int? StoppingPeriod, string awardingFileName, string awardingFileNameId, bool? hasGuarantee, decimal? awardingPercentage, int? monthCount, string finalGuaranteeDeliveryAddress);

    }
}
