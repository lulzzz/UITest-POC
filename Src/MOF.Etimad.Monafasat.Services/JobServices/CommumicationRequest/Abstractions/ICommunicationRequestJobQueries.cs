using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.Negotiations;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MOF.Etimad.Monafasat.Services
{
    public interface ICommunicationRequestJobQueries
    {
        Task<List<AgencyCommunicationRequest>> FindTendersWithPlaintsAfterStoppingPeriodJob();
        Task<List<ExtendOffersValiditySupplier>> GetExtendOfferValiditySuppliers();
        Task<List<Offer>> GetNonExecludedOffersForExtendOffersByValidityIds(List<int> supplierExtendOfferValidityIds);
        Task<List<AgencyCommunicationRequest>> GetExtendOffersValidityForExecludeSuppliers();
        Task<Offer> GetOfferById(int offerId);

        Task<Tender> FindTenderByTenderId(int tenderId);
        Task<Supplier> FindSupplierByCRNumber(string selectedCr);
        Task<List<NegotiationFirstStage>> FindAllNegotiationWaitingSupplierResponse();
        void UpdateNegotiationFirstStage(NegotiationFirstStage NegotiationFirstStage);
        Task SaveChanges();

        Task<List<NegotiationSecondStage>> FindAllSecondNegotiationWaitingSupplierResponse();
        Task<Negotiation> UpdateNegotiationAsync(Negotiation negotiation);
        Task<NegotiationSecondStage> UpdateNegotiationSecondStageAsync(NegotiationSecondStage NegotiationSecondStage);

        Task<List<GovAgency>> FindAgenciesByAgencyCodes(List<string> agencyCodes);
    }
}
