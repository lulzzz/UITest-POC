using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Interfaces;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface ISupplierQualificationDocumentCommands : IGenericCommandRepository
    {
        Task<SupplierPreQualificationDocument> UpdateAsync(SupplierPreQualificationDocument supplierPreQualificationDocument);


    }
}
