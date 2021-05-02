using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class SupplierQualificationDomainService : ISupplierQualificationDomainService
    {

        public SupplierQualificationDomainService()
        {


        }


        public async Task CheckPreQualificationDataValidation(SupplierPreQualificationDocumentModel model)
        {
            if (model.PreQualificationResult == null)
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.SelectQualificationResult);
            if (model.RejectionReason == null && model.PreQualificationResult == (int)Enums.OfferTechnicalEvaluationStatusId.NotIdenticalOffer)
            {
                throw new BusinessRuleException(Resources.QualificationResources.ErrorMessages.EnterRejectedReason);
            }
        }

    }
}
