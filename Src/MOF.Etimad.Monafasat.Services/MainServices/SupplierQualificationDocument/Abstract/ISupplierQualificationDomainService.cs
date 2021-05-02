using MOF.Etimad.Monafasat.ViewModel;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface ISupplierQualificationDomainService
    {

        Task CheckPreQualificationDataValidation(SupplierPreQualificationDocumentModel model);
    }
}
