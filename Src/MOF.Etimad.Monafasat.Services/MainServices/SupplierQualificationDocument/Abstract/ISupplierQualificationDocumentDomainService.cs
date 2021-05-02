using MOF.Etimad.Monafasat.SharedKernel;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface ISupplierQualificationDocumentDomainService
    {
        Task CheckApplyingDuplicationValidation(int PreQualificationId, string supplierId);
        Task CheckIfPreQualificationInCorrectStatus(int PreQualificationId, params Enums.TenderStatus[] tenderStatus);
        Task CheckPresentationDateValidation(int PreQualificationId, string supplierId);
        Task CheckIfSupplierBlocked(string CR);
    }
}
