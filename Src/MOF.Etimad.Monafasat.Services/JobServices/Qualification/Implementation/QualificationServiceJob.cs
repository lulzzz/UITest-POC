using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services.JobServices.Qualification
{
    public class QualificationServiceJob : IQulaificationServiceJob
    {

        private readonly IQualificationServiceQueryJob _QualificationServiceQuery;
        private readonly ISupplierQualificationDocumentAppService _SupplierQualificationDocumentsAppService;

        public QualificationServiceJob(IQualificationServiceQueryJob qualificationServiceQuery, ISupplierQualificationDocumentAppService supplierQualificationDocumentsAppService)
        {
            _QualificationServiceQuery = qualificationServiceQuery;
            _SupplierQualificationDocumentsAppService = supplierQualificationDocumentsAppService;
        }

        public async Task RecalculateSupplierPoint()
        {

            var suppliers = await _QualificationServiceQuery.GetActiveQualificationSuppliers();

            foreach (var item in suppliers)
            {
                var supplierdata = await _QualificationServiceQuery.GetQualificationSupplierData(item.QualificationId, item.SupplierCr);
                SupplierPreQualificationDocument supplierPreQualificationDocument = await _SupplierQualificationDocumentsAppService.GetSuppierDocument(item.QualificationId, item.SupplierCr);
                supplierdata.AttachmentRefrences = await GetEditModeAttahmentsData(supplierPreQualificationDocument);

                await EvaluateSupplierPoints(supplierdata, item.SupplierCr);
            }
        }

        private async Task EvaluateSupplierPoints(PreQualificationApplyDocumentsModel applyDocumentsModel, string supplierCr)
        {
            await PrepareQualificationDataBeforeEvaluation(applyDocumentsModel);
            applyDocumentsModel.SupplierId = supplierCr;
            await _SupplierQualificationDocumentsAppService.ApplyDocsforSupplier(applyDocumentsModel, true);

        }

        private async Task<string> GetEditModeAttahmentsData(SupplierPreQualificationDocument model)
        {
            var list = model.supplierPreQualificationAttachments;
            string AttachmentReference = string.Empty;

            if (list != null)
            {
                foreach (var item in list)
                {
                    AttachmentReference += ',' + "/Upload/GetFile?id=" + item.FileNetReferenceId + ":" + item.FileName;
                }
            }
            return AttachmentReference;
        }

        private async Task PrepareQualificationDataBeforeEvaluation(PreQualificationApplyDocumentsModel applyDocumentsModel)
        {
            applyDocumentsModel.preQualificationSupplierAttachmentModels = await PreparePostedSupplierAttachmentsModel(applyDocumentsModel.AttachmentRefrences);
            applyDocumentsModel.InsuranceAttachmentModels = await PrepareQualificationAttachmentModels(applyDocumentsModel.FileReferenceForInsurance);
            applyDocumentsModel.QualityAssuranceStandardsAttachmentModels = await PrepareQualificationAttachmentModels(applyDocumentsModel.FileReferenceForQualityAssuranceStandards);
            applyDocumentsModel.EnvironmentalHealthSafetyStandardsAttachmentModels = await PrepareQualificationAttachmentModels(applyDocumentsModel.FileReferenceForEnvironmentalHealthSafetyStandards);
            applyDocumentsModel.lstQualificationSupplierDataModel = 
                applyDocumentsModel.lstQualificationSupplierTechDataModel != null 
                ? applyDocumentsModel.lstQualificationSupplierTechDataModel.Concat(applyDocumentsModel.lstQualificationSupplierFinancialDataModel).ToList() 
                : applyDocumentsModel.lstQualificationSupplierDataModel;
        }
        private async Task<List<PreQualificationSupplierAttachmentModel>> PreparePostedSupplierAttachmentsModel(string attachmentReference)
        {
            List<string> attachmentReferences = new List<string>();
            if (!string.IsNullOrEmpty(attachmentReference))
            {
                string[] lst = attachmentReference.Split(',');
                foreach (var item in lst)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (item.Contains("/GetFile?id="))
                            attachmentReferences.Add(item.Split("/GetFile?id=")[item.Split("/GetFile?id=").Length - 1]);
                        else
                            attachmentReferences.Add(item);
                    }
                }
            }
            List<PreQualificationSupplierAttachmentModel> tenderAttachments = new List<PreQualificationSupplierAttachmentModel>();
            foreach (var item in attachmentReferences)
            {
                var arr = item.Split(":");
                PreQualificationSupplierAttachmentModel tenderAttachment = new PreQualificationSupplierAttachmentModel() { FileName = arr[1], FileNetReferenceId = arr[0] };
                tenderAttachments.Add(tenderAttachment);
            }
            return tenderAttachments;
        }

        private async Task<List<QualificationAttachmentModel>> PrepareQualificationAttachmentModels(string attachmentReference)
        {
            List<string> attachmentReferences = new List<string>();
            if (!string.IsNullOrEmpty(attachmentReference) && attachmentReference != "0")
            {
                string[] lst = attachmentReference.Split(',');
                foreach (var item in lst)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (item.Contains("/GetFile/"))
                            attachmentReferences.Add(item.Split("/GetFile/")[item.Split("/GetFile/").Length - 1]);
                        else
                            attachmentReferences.Add(item);
                    }
                }
            }
            List<QualificationAttachmentModel> tenderAttachments = new List<QualificationAttachmentModel>();
            foreach (var item in attachmentReferences)
            {
                var arr = item.Split(":");
                QualificationAttachmentModel tenderAttachment = new QualificationAttachmentModel() { FileName = arr[1], FileNetReferenceId = arr[0] };
                tenderAttachments.Add(tenderAttachment);
            }
            return tenderAttachments;
        }


    }
}
