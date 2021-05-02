using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.Negotiations;
using MOF.Etimad.Monafasat.Core.SearchCriterias;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface INegotiationJobQueries
    {
        Task<Offer> GetOfferById(int offerId);

        Task<Tender> FindTenderByTenderId(int tenderId);
        Task<Supplier> FindSupplierByCRNumber(string selectedCr);
        Task<List<NegotiationFirstStage>> FindAllNegotiationWaitingSupplierResponse(); 
        void UpdateNegotiationFirstStage(NegotiationFirstStage NegotiationFirstStage); 
        Task SaveChanges();

        Task<List<NegotiationSecondStage>> FindAllSecondNegotiationWaitingSupplierResponse();
        Task<Negotiation> UpdateNegotiationAsync(Negotiation negotiation); 
        Task<NegotiationSecondStage> UpdateNegotiationSecondStageAsync(NegotiationSecondStage NegotiationSecondStage);
    }
}
