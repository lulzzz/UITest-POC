using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.ViewModel;
using System;

namespace MOF.Etimad.Monafasat.Services
{
    public partial class TenderDomainService
    {
        public void IsValidToUpdateTenderTypeCosts(TenderTypeWithAddedValueModel tenderType)
        {
            if (tenderType == null)
                throw new AuthorizationException();

            if (tenderType.BuyingCost < 0)
                throw new BusinessRuleException(String.Format(Resources.TenderResources.ErrorMessages.BuyingCostCannotBeMinusForType, tenderType.Name));

            if (tenderType.InvitationCost < 0)
                throw new BusinessRuleException(String.Format(Resources.TenderResources.ErrorMessages.InvitationCostCannotBeMinusForType, tenderType.Name));
        }
    }
}
