using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MOF.Etimad.Monafasat.Services
{
    public interface ITenderJobQueries
    {
        Task<List<Tender>> GetAllFinalAwardedTendersForEmarketPlace();
        Task<List<EmarketPlaceRequest>> GetAwardedSupplierQuantityTableItemsValue(List<int> offerIds);
        Task<SRMFrameworkAgreementManageModel> FillVendorProducts(List<int> offerIds, List<EmarketPlaceResponse> clotypes);
        Task<List<Tender>> GetAllFinishedStoppingAwardingPeriodTenders();
        Task<List<BiddingRound>> FindFinishedBiddingRounds();
        Task<List<BiddingRound>> FindPendingBiddingRounds();
        Task<List<Tender>> FindTendersToOpenOffers(int id);
        Task<List<Tender>> FindTendersToCheckOffers();
        Task<List<GovAgency>> FindAgenciesByAgencyCodes(List<string> agencyCodes);
    }
}
