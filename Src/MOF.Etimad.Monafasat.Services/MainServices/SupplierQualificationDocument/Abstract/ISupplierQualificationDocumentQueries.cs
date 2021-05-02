
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface ISupplierQualificationDocumentQueries
    {

        Task<SupplierPreQualificationDocument> FindSupplierPreQualificationDocumentById(int SupplierQualificationDocumentId);

        Task<SupplierPreQualificationDocumentModel> FindSupplierPreQualificationDocumentModel(int SupplierQualificationDocumentId, string SupplierId);
        Task<Tender> GetTenderAndSupplierDocuments(int tenderId);
        Task<SupplierPreQualificationDocument> GetSupplierDocumentByPreQualificationID(int preQualificationId);
        Task<SupplierPreQualificationDocument> GetSuppierDocument(int preQualificationId, string SupplierId);
        Task<Tender> GetPreQualificationbyIdAndStatus(int preQualificationId, List<int> statusIds);
        Task<Tender> GetPreQualificationById(int preQualificationId);
        Task<List<QualificationSupplierDataModel>> GetQualificationSupplierData(int qualificationId);
        Task<List<QualificationSupplierDataModel>> GetQualificationSupplierDataForEdit(int qualificationId, string supplierId);
        Task<QualificationSupplierDataReviewViewModel> GetSupplierDataReviewModel(int qualificationId, string supplierId);
    }
}
