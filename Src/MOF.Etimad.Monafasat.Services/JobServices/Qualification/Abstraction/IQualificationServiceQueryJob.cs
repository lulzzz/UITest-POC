using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services.JobServices.Qualification
{
    public interface IQualificationServiceQueryJob
    {
        Task<List<QualificationSupplierDto>> GetActiveQualificationSuppliers();
        Task<PreQualificationApplyDocumentsModel> GetQualificationSupplierData(int qualificationId, string supplierId);
    }
}
