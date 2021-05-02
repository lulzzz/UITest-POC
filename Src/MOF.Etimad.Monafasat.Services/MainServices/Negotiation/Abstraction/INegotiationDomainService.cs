using MOF.Etimad.Monafasat.Core.Entities;

namespace MOF.Etimad.Monafasat.Services
{
    public interface INegotiationDomainService
    {

        #region Negotiation second stage
        void IsValidToApproveOrRejectNegotiationSecondStageByCheckManagerAsync(Negotiation negotiation);
        void IsValidToApproveOrRejectNegotiationSecondStageByUnitSecretaryAsync(Negotiation negotiation);
        void IsValidToEditOrFinishNegotiationSecondStageByCheckSecretaryAsync(Negotiation negotiation);
        void IsValidToReopenNegotiation(Negotiation negotiation);
        void IsValidToApproveOrRejectNegotiationSecondStageBySupplierAsync(Negotiation negotiation);
        #endregion Negotiation second stage
    }
}
