using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Qualification;
using MOF.Etimad.SharedKernel;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services.MainServices.SupplierQualificationDocument.Abstract
{
    public class SupplierQualificationDocumentQueries : ISupplierQualificationDocumentQueries
    {
        private IAppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SupplierQualificationDocumentQueries(IAppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<SupplierPreQualificationDocument> FindSupplierPreQualificationDocumentById(int SupplierQualificationDocumentId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(SupplierQualificationDocumentId), SupplierQualificationDocumentId.ToString());
            var supplierPreQualificationDocument = await _context.SupplierPreQualificationDocument
                .Where(t => t.SupplierPreQualificationDocumentId == SupplierQualificationDocumentId && t.IsActive == true)
                .Include(x => x.PreQualification).FirstOrDefaultAsync();
            return supplierPreQualificationDocument;
        }

        public async Task<SupplierPreQualificationDocumentModel> FindSupplierPreQualificationDocumentModel(int SupplierQualificationDocumentId, string SupplierId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(SupplierQualificationDocumentId), SupplierQualificationDocumentId.ToString());
            var supplierPreQualificationDocument = await _context.SupplierPreQualificationDocument
                .Where(t => t.PreQualificationId == SupplierQualificationDocumentId && t.SupplierId == SupplierId && t.IsActive == true)
                .Select(x => new SupplierPreQualificationDocumentModel
                {
                    SupplierId = x.SupplierId,
                    PreQualificationId = x.PreQualificationId,
                    PreQualificationIdString = Util.Encrypt(x.PreQualificationId),
                    PreQualificationResult = x.PreQualificationResult,
                    RejectionReason = x.RejectionReason,
                    FinalResultStatusId = x.PreQualification.QualificationFinalResults.FirstOrDefault(q => q.IsActive == true && q.SupplierSelectedCr == SupplierId && q.TenderId == x.PreQualificationId).QualificationLookupId,
                    FinalResultStatusName = x.PreQualification.QualificationFinalResults.FirstOrDefault(q => q.IsActive == true && q.SupplierSelectedCr == SupplierId && q.TenderId == x.PreQualificationId).QualificationLookup.Name,
                    SupplierPreQualificationDocumentId = x.SupplierPreQualificationDocumentId,
                    SupplierPreQualificationDocumentIdString = Util.Encrypt(x.SupplierPreQualificationDocumentId),
                    TenderStatusId = x.PreQualification.TenderStatusId,
                    canEditResult = x.PreQualification.QualificationFinalResults.FirstOrDefault(q => q.IsActive == true && q.SupplierSelectedCr == SupplierId && q.TenderId == x.PreQualificationId).ResultValue >= x.PreQualification.TenderPointsToPass,
                    FailingReason = x.PreQualification.QualificationFinalResults.FirstOrDefault(q => q.IsActive == true && q.SupplierSelectedCr == SupplierId && q.TenderId == x.PreQualificationId).FailingReason,
                }).FirstOrDefaultAsync();
            return supplierPreQualificationDocument;
        }
        public async Task<Tender> GetTenderAndSupplierDocuments(int tenderId)
        {
            return await _context.Tenders.Include(w => w.PreQualificationApplyDocuments).WhereIf(true, x => x.TenderId == tenderId).FirstOrDefaultAsync();
        }
        public async Task<SupplierPreQualificationDocument> GetSupplierDocumentByPreQualificationID(int preQualificationId)
        {
            var entities = await _context.SupplierPreQualificationDocument.Include(t => t.supplierPreQualificationAttachments)
                    .Where(t => t.SupplierPreQualificationDocumentId == preQualificationId && t.IsActive == true).FirstOrDefaultAsync();
            return entities;
        }

        public async Task<Tender> GetPreQualificationById(int preQualificationId)
        {

            return await _context.Tenders.Where(w => w.TenderId == preQualificationId).FirstOrDefaultAsync();
        }
        public async Task<SupplierPreQualificationDocument> GetSuppierDocument(int preQualificationId, string SupplierId)
        {

            return await _context.SupplierPreQualificationDocument.Include(w => w.supplierPreQualificationAttachments).Where(w => w.PreQualificationId == preQualificationId && w.SupplierId == SupplierId && w.IsActive == true).FirstOrDefaultAsync();
        }

        public async Task<Tender> GetPreQualificationbyIdAndStatus(int preQualificationId, List<int> statusIds)
        {
            return await _context.Tenders.Include(w => w.PreQualificationApplyDocuments).Where(w => w.TenderId == preQualificationId && statusIds.Any(a => a == w.TenderStatusId)).FirstOrDefaultAsync();
        }

        public async Task<List<QualificationSupplierDataModel>> GetQualificationSupplierData(int qualificationId)
        {

            var arProvider = CultureInfo.CreateSpecificCulture("ar-SA");
            var supplierNumber = _httpContextAccessor.HttpContext.User.SupplierNumber();
            var result = await _context.QualificationConfiguration.Where(q => q.TenderId == qualificationId && q.IsActive == true)
                                .Select(q => new QualificationSupplierDataModel
                                {
                                    IsConfigure = q.QualificationItem.IsConfigure,
                                    QualificationConfigurationId = q.ID,
                                    Max = q.Max,
                                    Min = q.Min,
                                    QualificationItemId = q.QualificationItemId,
                                    QualificationItemName = q.QualificationItem.Name,
                                    QualificationItemCode = q.QualificationItem.Code,
                                    QualificationItemTypeId = q.QualificationItem.QualificationItemTypeId,
                                    QualificationTypeId = q.Tender.QualificationTypeId.HasValue ? q.Tender.QualificationTypeId.Value : 0,
                                    QualificationSubCategoryId = q.QualificationItem.SubCategoryId,
                                    QualificationCategoryId = q.QualificationItem.QualificationSubCategory.QualificationCategoryId,
                                    SupplierSelectedCr = supplierNumber,
                                    TenderIdStr = Util.Encrypt(q.TenderId),
                                    OfferCheckingDate = q.Tender.OffersCheckingDate != null ? q.Tender.OffersCheckingDate : null,
                                    OffersCheckingDateHijri = q.Tender.OffersCheckingDate.HasValue ? q.Tender.OffersCheckingDate.Value.ToString("dd/MM/yyyy", arProvider) : "", // please not change this format 
                                    Value = q.Value,
                                    Weight = q.Weight
                                }).OrderBy(a => a.QualificationItemCode).ToListAsync();
            return result;
        }


        public async Task<List<QualificationSupplierDataModel>> GetQualificationSupplierDataForEdit(int qualificationId, string supplierId)
        {
            var arProvider = CultureInfo.CreateSpecificCulture("ar-SA");
            Tender qualification = await _context.Tenders.Where(a => a.TenderId == qualificationId).FirstOrDefaultAsync();


            var result = await _context.QualificationSupplierData.Where(a => a.TenderId == qualificationId && a.SupplierSelectedCr == supplierId && a.IsActive == true)
                                .Select(q => new QualificationSupplierDataModel
                                {
                                    QualificationConfigurationId = q.QualificationConfigurationId,
                                    QualificationSupplierDataId = q.ID,
                                    IsConfigure = q.QualificationItem.IsConfigure,
                                    Max = q.QualificationConfiguration.Max,
                                    Min = q.QualificationConfiguration.Min,
                                    QualificationLookupId = (q.QualificationLookupId != null ? q.QualificationLookupId.Value : 0),
                                    QualificationItemId = q.QualificationItemId.Value,
                                    QualificationItemName = q.QualificationItem.Name,
                                    QualificationItemCode = q.QualificationItem.Code,
                                    QualificationItemTypeId = q.QualificationItem.QualificationItemTypeId,
                                    QualificationTypeId = q.Tender.QualificationTypeId.HasValue ? q.Tender.QualificationTypeId.Value : 0,
                                    QualificationSubCategoryId = q.QualificationItem.SubCategoryId,
                                    QualificationCategoryId = q.QualificationItem.QualificationSubCategory.QualificationCategoryId,
                                    SupplierSelectedCr = q.SupplierSelectedCr,
                                    FileReferenceForQualityAssuranceStandards = (q.QualificationConfigurationAttachments.FirstOrDefault() != null ? GetEditModeAttahmentsData(q.QualificationConfigurationAttachments) : string.Empty),
                                    FileReferenceForEnvironmentalHealthSafetyStandards = (q.QualificationConfigurationAttachments.FirstOrDefault() != null ? GetEditModeAttahmentsData(q.QualificationConfigurationAttachments) : string.Empty),
                                    FileReferenceForInsurance = (q.QualificationConfigurationAttachments.FirstOrDefault() != null ? GetEditModeAttahmentsData(q.QualificationConfigurationAttachments) : string.Empty),
                                    TenderIdStr = Util.Encrypt(q.TenderId),
                                    OfferCheckingDate = q.Tender.OffersCheckingDate.Value,
                                    InsuranceCoverageEndDate = q.CoverageExpireDate,
                                    InsuranceCoverageEndDateHijri = q.CoverageExpireDate.HasValue ? q.CoverageExpireDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "",
                                    InsuranceProvider = q.InsuranceProvider,
                                    InsuranceCoverage = q.LevelOfCoverage,
                                    OffersCheckingDateHijri = q.Tender.OffersCheckingDate.HasValue ? q.Tender.OffersCheckingDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "",
                                    Value = q.SupplierValue,
                                    SupplierValue = q.SupplierValue,
                                    Weight = q.QualificationConfiguration.Weight,
                                    QualificationStatusId = q.Tender.PreQualificationApplyDocuments.Any(r => r.IsActive == true && r.SupplierId == supplierId) ? q.Tender.PreQualificationApplyDocuments.FirstOrDefault(r => r.IsActive == true && r.SupplierId == supplierId).StatusId : null,
                                }).OrderBy(a => a.QualificationItemCode).ToListAsync();

            if (result != null)
            {
                if (qualification.QualificationTypeId == (int)Enums.PreQualificationType.Large)
                {
                    result.FirstOrDefault().lstQualificationSupplierProjectModel = new List<QualificationSupplierProjectModel>();
                    result.FirstOrDefault().lstQualificationSupplierProjectModel = await _context.QualificationSupplierProject.
                    Where(a => a.TenderId == qualificationId && a.SupplierSelectedCr == supplierId && a.IsActive == true).Select(x => new QualificationSupplierProjectModel
                    {
                        ContractName = x.ContractName,
                        ContractValue = x.ContractValue,
                        Description = x.Description,
                        EmailAddress = x.EmailAddress,
                        PhoneNumber = x.PhoneNumber,
                        OwnerName = x.OwnerName,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate,
                        StartDateHijri = x.StartDate.HasValue ? x.StartDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "",
                        EndDateHijri = x.EndDate.HasValue ? x.EndDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : ""
                    }).ToListAsync();
                }

                result.FirstOrDefault().lstQualificationSupplierDataYearlyViewModel = new List<QualificationSupplierDataYearlyViewModel>();
                result.FirstOrDefault().lstQualificationSupplierDataYearlyViewModel = await _context.QualificationSupplierDataYearly.
                    Where(a => a.TenderId == qualificationId && a.SupplierSelectedCr == supplierId && a.IsActive == true).Select(x => new QualificationSupplierDataYearlyViewModel
                    {
                        ItemCode = x.QualificationItem.Code,
                        Name = x.QualificationItem.Name,
                        QualificationItemId = x.QualificationItemId.Value,
                        SupplierValue = x.SupplierValue,
                        QualificationYearId = x.QualificationYearId
                    }).ToListAsync();


            }
            return result;
        }

        private static string GetEditModeAttahmentsData(List<QualificationConfigurationAttachment> lst)
        {
            var list = lst;
            string AttachmentReference = string.Empty;

            if (list != null)
            {
                foreach (var item in list)
                {
                    AttachmentReference += ',' + "/Upload/GetFile?id=" + item.FileReferenceId + ":" + item.FileName;
                }
            }
            return AttachmentReference;
        }


        public async Task<QualificationSupplierDataReviewViewModel> GetSupplierDataReviewModel(int qualificationId, string supplierId)
        {

            int QualificationTypeId = 0;


            QualificationSupplierDataReviewViewModel supplierData = new QualificationSupplierDataReviewViewModel();
            var List = await _context.QualificationSupplierData.Where(q => q.TenderId == qualificationId && q.SupplierSelectedCr == supplierId && q.IsActive == true)
                                .Select(q => new QualificationSupplierDataModel
                                {
                                    QualificationConfigurationId = q.ID,
                                    ItemCode = q.QualificationItem.Code,
                                    QualificationItemId = q.QualificationItem.ID,
                                    QualificationItemName = q.QualificationItem.Name,
                                    PointValue = q.PointValue,
                                    QualificationItemTypeId = q.QualificationItem.QualificationItemTypeId,
                                    lstQualificationConfigurationAttachmentModel = (q.QualificationConfigurationAttachments == null ? null : q.QualificationConfigurationAttachments.Select(e => new ViewModel.Qualification.QualificationConfigurationAttachmentModel
                                    {
                                        FileReferenceId = e.FileReferenceId,
                                        FileName = e.FileName,
                                        QualificationSupplierDataId = e.QualificationSupplierDataId

                                    }).ToList()),
                                    lstQualificationSupplierProjectModel = (q.QualificationSupplierProjects == null ? null : q.QualificationSupplierProjects.Select(e => new QualificationSupplierProjectModel
                                    {
                                        ContractName = e.ContractName,
                                        OwnerName = e.OwnerName,
                                        ContractValue = e.ContractValue,
                                        Description = e.Description,
                                        EmailAddress = e.EmailAddress,
                                        EndDateStr = (e.EndDate != null ? e.EndDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : string.Empty),
                                        StartDateStr = (e.StartDate != null ? e.StartDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : string.Empty),
                                        PhoneNumber = e.PhoneNumber
                                    }).ToList()),
                                    QualificationTypeId = q.Tender.QualificationTypeId.HasValue ? q.Tender.QualificationTypeId.Value : 0,
                                    QualificationSubCategoryId = q.QualificationItem.SubCategoryId,
                                    QualificationCategoryId = q.QualificationItem.QualificationSubCategory.QualificationCategoryId,
                                    TenderIdStr = Util.Encrypt(q.TenderId),
                                    SupplierValueString = q.QualificationItem.QualificationItemTypeId == (int)Enums.QualificationItemType.Select ?
                                      q.QualificationLookup.Name
                                    : q.SupplierValue.ToString(),
                                    InsuranceProvider = q.InsuranceProvider,
                                    InsuranceCoverage = q.LevelOfCoverage,
                                    InsuranceCoverageEndDateStr = (q.CoverageExpireDate != null ? q.CoverageExpireDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : string.Empty),
                                    Weight = q.Weight,
                                    TenderStatusId = q.Tender.TenderStatusId
                                }).OrderBy(q => q.ItemCode).ToListAsync();



            if (List != null && List.Count > 0)
            {

                QualificationTypeId = (List != null && List.Count > 0 ? List.Select(a => a.QualificationTypeId).FirstOrDefault() : 0);

                var yearlyList = _context.QualificationSupplierDataYearly.Where(y => y.TenderId == qualificationId && y.SupplierSelectedCr == supplierId && y.IsActive == true)
                    .Include(y => y.QualificationItem).AsEnumerable().GroupBy(y => y.QualificationItemId)
                    .Select(x => new QualificationSupplierDataYearlyViewModel
                    {
                        ItemCode = x.FirstOrDefault().QualificationItem.Code,
                        CurrentYear = x.Where(xx => xx.QualificationYearId == (int)Enums.QualificationYear.CurrentYear).FirstOrDefault().SupplierValue,
                        Year1 = (QualificationTypeId != (int)Enums.PreQualificationType.Small ? x.Where(xx => xx.QualificationYearId == (int)Enums.QualificationYear.SecondYear).FirstOrDefault().SupplierValue : 0.0M),
                        Year2 = (QualificationTypeId != (int)Enums.PreQualificationType.Small ? x.Where(xx => xx.QualificationYearId == (int)Enums.QualificationYear.ThirdYear).FirstOrDefault().SupplierValue : 0.0M),
                        Name = x.FirstOrDefault().QualificationItem.Name,
                    }).OrderBy(a => a.ItemCode)
                    .ToList();

                supplierData.lstQualificationSupplierTechDataModel = (List != null && List.Count > 0 ? List.Where(l => l.QualificationCategoryId == (int)Enums.QualificationItemCategory.Technical).ToList() : null);
                supplierData.lstQualificationSupplierFinancialDataModel = (List != null && List.Count > 0 ? List.Where(l => l.QualificationCategoryId == (int)Enums.QualificationItemCategory.Financial).ToList() : null);
                supplierData.QualificationTypeId = (List != null && List.Count > 0 ? List.FirstOrDefault().QualificationTypeId : 0);
                supplierData.SupplierCR = supplierId;
                supplierData.TenderIdStr = Util.Encrypt(qualificationId);
                supplierData.TenderStatusId = List.Count > 0 ? List.FirstOrDefault().TenderStatusId : 0;
                supplierData.lstQualificationSupplierProjectModel = (List != null && List.Count > 0 ? List.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfProjectsImplementedLastThreeYears).FirstOrDefault()?.lstQualificationSupplierProjectModel : null);


                supplierData.QualificationSupplierDataYearly = yearlyList;
            }

            return supplierData;
        }
    }
}