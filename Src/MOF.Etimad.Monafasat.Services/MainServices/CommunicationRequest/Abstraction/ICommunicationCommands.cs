using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface ICommunicationCommands
    {
        Task<AgencyCommunicationRequest> CreateAsync(AgencyCommunicationRequest agencyCommunicationRequest);
        Task<AgencyCommunicationRequest> UpdateAsync(AgencyCommunicationRequest agencyCommunicationRequest);
        Task<PlaintRequest> UpdatePlaintRequestAsync(PlaintRequest plaintRequest);
        Task<AgencyCommunicationRequest> UpdateAgencytRequestAsync(AgencyCommunicationRequest request);

        Task<EscalationRequest> UpdateEscalationRequestAsync(EscalationRequest request);

        Task<SupplierExtendOfferDatesRequest> CreateSupplierExtendOfferDatesRequestAsync(SupplierExtendOfferDatesRequest supplierExtendOfferDatesRequest);

        #region Extend Offers Validity

        Task<ExtendOffersValiditySupplier> UpdateExtendOffersValiditySupplier(ExtendOffersValiditySupplier extendOffersValiditySupplier);
        Task<ExtendOffersValiditySupplier> CreateExtendOffersValiditySupplier(ExtendOffersValiditySupplier extendOffersValiditySupplier);
        Task AddExtendOffersValidityAttachment(ExtendOffersValidityAttachment extendOffersValidityAttachment);
        #endregion
    }
}
