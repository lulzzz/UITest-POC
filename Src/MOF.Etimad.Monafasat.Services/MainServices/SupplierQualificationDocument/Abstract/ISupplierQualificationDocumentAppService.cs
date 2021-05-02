using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface ISupplierQualificationDocumentAppService
    {
        Task<SupplierPreQualificationDocument> ApplyDocsforSupplier(PreQualificationApplyDocumentsModel model, bool? IsFromjob = null);
        Task<SupplierPreQualificationDocument> FindById(int SupplierPreQualificationDocumentId);
        Task<SupplierPreQualificationDocumentModel> FindPreQualificationDocumentById(int SupplierPreQualificationDocumentId, string SupplierId);

        Task<SupplierPreQualificationDocument> UpdateSupplierPreQualificationDocumentStatus(SupplierPreQualificationDocumentModel supplierPreQualificationDocumentModel);

        Task<SupplierPreQualificationDocument> FindTenderAttachmentsById(int preqSuplierId);
        Task<SupplierPreQualificationDocument> GetSuppierDocument(int PreQualificationId, string supplierId);
        Task<SupplierPreQualificationDocument> CheckSuppierDocument(int PreQualificationId, string supplierId, bool editMode);
        Task<List<QualificationSupplierDataModel>> GetQualificationSupplierData(int qualificationId);
        Task<List<QualificationSupplierDataModel>> GetQualificationSupplierDataForEdit(int qualificationId, string supplierId);

        #region Apply-Post-Qualification

        //Task<SupplierPreQualificationDocument> ApplyPostQualificationDocsforSupplier(PreQualificationApplyDocumentsModel preQSupplier, string supplierCr);
       // Task<SupplierPreQualificationDocument> GetPostQualificationSuppierDocument(int PostQualificationId, string supplierId);

        #endregion


        Task<SupplierPreQualificationDocument> PostQualificationChecking(SupplierPreQualificationDocumentModel supplierPreQualificationDocumentModel);
        Task<QualificationSupplierDataReviewViewModel> GetSupplierDataReviewModel(int qualificationId, string supplierId);
        Task ChooseQualificationResult(ChooseQualificationResultModel chooseQualificationResultModel);
    }
}
