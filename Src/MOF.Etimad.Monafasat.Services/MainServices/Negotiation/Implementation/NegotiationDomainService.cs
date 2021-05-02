using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;

namespace MOF.Etimad.Monafasat.Services
{
    public class NegotiationDomainService : INegotiationDomainService
    {
        private readonly IAppDbContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICommitteeQueries _committeeQueries;
        public NegotiationDomainService(IAppDbContext context, IHttpContextAccessor httpContextAccessor, ICommitteeQueries committeeQueries)
        {
            this.context = context;
            _httpContextAccessor = httpContextAccessor;
            _committeeQueries = committeeQueries;
        }


        #region Negotiation second stage

        public void IsValidToApproveOrRejectNegotiationSecondStageByCheckManagerAsync(Negotiation negotiation)
        {
            if (negotiation == null)
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.RequestNotFound);
            if (negotiation.StatusId != (int)Enums.enNegotiationStatus.CheckManagerPendingApprove && !(negotiation.AgencyCommunicationRequest.Tender.IsLowBudgetDirectPurchase.HasValue && negotiation.AgencyCommunicationRequest.Tender.IsLowBudgetDirectPurchase.Value))
            {
                var RequestStatus = Resources.CommunicationRequest.Messages.ResourceManager.GetString(nameof(Enums.enNegotiationStatus.CheckManagerPendingApprove));
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.CanNotProceedStatusIs + RequestStatus);
            }
            if (negotiation.AgencyCommunicationRequest.Tender.AgencyCode != _httpContextAccessor.HttpContext.User.UserAgency())
            {
                throw new UnHandledAccessException();
            }
        }

        public void IsValidToApproveOrRejectNegotiationSecondStageByUnitSecretaryAsync(Negotiation negotiation)
        {
            if (negotiation == null)
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.RequestNotFound);
            if (negotiation.StatusId != (int)Enums.enNegotiationStatus.UnitSpecialestPendingApproved)
            {
                var RequestStatus = Resources.CommunicationRequest.Messages.ResourceManager.GetString(nameof(Enums.enNegotiationStatus.UnitSpecialestPendingApproved));
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.CanNotProceedStatusIs + RequestStatus);
            }

        }

        public void IsValidToEditOrFinishNegotiationSecondStageByCheckSecretaryAsync(Negotiation negotiation)
        {
            if (negotiation == null)
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.RequestNotFound);
            if (negotiation.StatusId != (int)Enums.enNegotiationStatus.CheckManagerReject && negotiation.StatusId != (int)Enums.enNegotiationStatus.UnitSpecialistReject)
            {
                var RequestStatus = Resources.CommunicationRequest.Messages.ResourceManager.GetString(nameof(Enums.enNegotiationStatus.CheckManagerReject));
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.CanNotProceedStatusIs + RequestStatus);
            }
            if (negotiation.AgencyCommunicationRequest.Tender.AgencyCode != _httpContextAccessor.HttpContext.User.UserAgency())
            {
                throw new UnHandledAccessException();
            }
        }

        public void IsValidToApproveOrRejectNegotiationSecondStageBySupplierAsync(Negotiation negotiation)
        {
            if (negotiation == null)
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.RequestNotFound);
            if (negotiation.StatusId != (int)Enums.enNegotiationStatus.SentToSuppliers)
            {
                var RequestStatus = Resources.CommunicationRequest.Messages.ResourceManager.GetString(nameof(Enums.enNegotiationStatus.UnderUpdate));
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.CanNotProceedStatusIs + RequestStatus);
            }

        }
        public void IsValidToReopenNegotiation(Negotiation negotiation)
        {
            if (negotiation == null)
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.RequestNotFound);
            if (negotiation.StatusId != (int)Enums.enNegotiationStatus.CheckManagerReject && negotiation.StatusId != (int)Enums.enNegotiationStatus.UnitSpecialistReject)
            {
                var RequestStatus = Resources.CommunicationRequest.Messages.ResourceManager.GetString(nameof(Enums.enNegotiationStatus.UnderUpdate));
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.CanNotProceedStatusIs + RequestStatus);
            }

            if (negotiation.StatusId == (int)Enums.enNegotiationStatus.New || negotiation.StatusId == (int)Enums.enNegotiationStatus.UnderUpdate)
            {
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.CanNotProceedStatusIs + "   تحت التحديث");
            }

        }
        #endregion Negotiation second stage
    }
}
