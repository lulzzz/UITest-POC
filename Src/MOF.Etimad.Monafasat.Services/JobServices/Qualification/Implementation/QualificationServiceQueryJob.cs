using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Qualification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services.JobServices.Qualification
{
    public class QualificationServiceQueryJob : IQualificationServiceQueryJob
    {
        private readonly IAppDbContext _context;

        public QualificationServiceQueryJob(IAppDbContext context)
        {
            _context = context;
        }


        public async Task<List<QualificationSupplierDto>> GetActiveQualificationSuppliers()
        {
            var supplierList = await _context.SupplierPreQualificationDocument
                .Where(supplier => supplier.IsActive == true
                        && supplier.StatusId == (int)Enums.QualificationStatus.Received
                        && supplier.PreQualification.IsActive == true
                        && supplier.PreQualification.TenderStatusId == (int)Enums.TenderStatus.Approved
                        && supplier.PreQualification.OffersCheckingDate >= DateTime.Now)
                .Select(s => new QualificationSupplierDto
                {
                    QualificationId = s.PreQualificationId,
                    SupplierCr = s.SupplierId
                }).ToListAsync();

            return supplierList;
        }



        public async Task<PreQualificationApplyDocumentsModel> GetQualificationSupplierData(int qualificationId, string supplierId)
        {
            var model = new PreQualificationApplyDocumentsModel()
            {
                PreQualificationResult = 0,
                PreQualificationIdString = Util.Encrypt(qualificationId)
            };

            List<QualificationSupplierDataModel> lstQualificationSupplierDataModel = await GetQualificationSupplierDataModel(qualificationId, supplierId);

            model = await PrepareQualificationData(model, lstQualificationSupplierDataModel);
            model.EditMode = true;
            return model;
        }

        private async Task<PreQualificationApplyDocumentsModel> PrepareQualificationData(PreQualificationApplyDocumentsModel model, List<QualificationSupplierDataModel> lstQualificationSupplierDataModel)
        {
            model.lstQualificationSupplierTechDataModel = lstQualificationSupplierDataModel.Where(l => l.QualificationCategoryId == (int)Enums.QualificationItemCategory.Technical).ToList();
            model.lstQualificationSupplierFinancialDataModel = lstQualificationSupplierDataModel.Where(l => l.QualificationCategoryId == (int)Enums.QualificationItemCategory.Financial).ToList();
            model.lstQualificationSupplierDataYearlyViewModel = lstQualificationSupplierDataModel.FirstOrDefault().lstQualificationSupplierDataYearlyViewModel;
            model.lstQualificationSupplierProjectModel = lstQualificationSupplierDataModel.FirstOrDefault().lstQualificationSupplierProjectModel;

            // bind data
            model = await bindFinancialPropertyAsync(model);
            model.FileReferenceForQualityAssuranceStandards = model.lstQualificationSupplierTechDataModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.QualityAssuranceStandards).FirstOrDefault()?.FileReferenceForQualityAssuranceStandards;
            model.FileReferenceForEnvironmentalHealthSafetyStandards = model.lstQualificationSupplierTechDataModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.EnvironmentalHealthSafetyStandards).FirstOrDefault()?.FileReferenceForEnvironmentalHealthSafetyStandards;
            model.FileReferenceForInsurance = model.lstQualificationSupplierTechDataModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.InsuranceOfGeneralCommercialResponsibility ||
            a.QualificationItemId == (int)Enums.QualificationEvaluationItems.InsuranceOfProfessionalCompensation ||
            a.QualificationItemId == (int)Enums.QualificationEvaluationItems.LiabilityInsurance).FirstOrDefault()?.FileReferenceForInsurance;




            if (lstQualificationSupplierDataModel != null && lstQualificationSupplierDataModel.Count > 0)
            {
                model.QualificationTypeId = lstQualificationSupplierDataModel.FirstOrDefault().QualificationTypeId;
                model.QualificationStatusId = lstQualificationSupplierDataModel.FirstOrDefault().QualificationStatusId;
                model.SupplierId = lstQualificationSupplierDataModel.FirstOrDefault().SupplierSelectedCr;
                model.OfferCheckingDate = lstQualificationSupplierDataModel.FirstOrDefault().OfferCheckingDate != null ? lstQualificationSupplierDataModel.FirstOrDefault().OfferCheckingDate : null;
                model.OffersCheckingDateHijri = lstQualificationSupplierDataModel.FirstOrDefault().OffersCheckingDateHijri;
            }

            return model;
        }

        private async Task<PreQualificationApplyDocumentsModel> bindFinancialPropertyAsync(PreQualificationApplyDocumentsModel model)
        {

            if (model.lstQualificationSupplierFinancialDataModel.FirstOrDefault().QualificationTypeId == (int)Enums.PreQualificationType.Small)
            {
                model.CashEquivalents = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.CashEquivalents && a.QualificationYearId == (int)Enums.QualificationYear.CurrentYear).FirstOrDefault().SupplierValue;
                model.AccountsReceivable = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.AccountsReceivable && a.QualificationYearId == (int)Enums.QualificationYear.CurrentYear).FirstOrDefault().SupplierValue;
                model.CurrentLiabilities = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.CurrentLiabilities && a.QualificationYearId == (int)Enums.QualificationYear.CurrentYear).FirstOrDefault().SupplierValue;
            }
            else if (model.lstQualificationSupplierFinancialDataModel.FirstOrDefault().QualificationTypeId == (int)Enums.PreQualificationType.Medium ||
                model.lstQualificationSupplierFinancialDataModel.FirstOrDefault().QualificationTypeId == (int)Enums.PreQualificationType.Large)
            {

                model.CurrentAssets = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.CurrentAssets && a.QualificationYearId == (int)Enums.QualificationYear.CurrentYear).FirstOrDefault().SupplierValue;
                model.CashEquivalents = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.CashEquivalents && a.QualificationYearId == (int)Enums.QualificationYear.CurrentYear).FirstOrDefault().SupplierValue;
                model.AccountsReceivable = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.AccountsReceivable && a.QualificationYearId == (int)Enums.QualificationYear.CurrentYear).FirstOrDefault().SupplierValue;
                model.CurrentLiabilities = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.CurrentLiabilities && a.QualificationYearId == (int)Enums.QualificationYear.CurrentYear).FirstOrDefault().SupplierValue;

                model.CurrentAssets1 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.CurrentAssets && a.QualificationYearId == (int)Enums.QualificationYear.SecondYear).FirstOrDefault().SupplierValue;
                model.CashEquivalents1 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.CashEquivalents && a.QualificationYearId == (int)Enums.QualificationYear.SecondYear).FirstOrDefault().SupplierValue;
                model.AccountsReceivable1 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.AccountsReceivable && a.QualificationYearId == (int)Enums.QualificationYear.SecondYear).FirstOrDefault().SupplierValue;
                model.CurrentLiabilities1 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.CurrentLiabilities && a.QualificationYearId == (int)Enums.QualificationYear.SecondYear).FirstOrDefault().SupplierValue;



                model.CurrentAssets2 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.CurrentAssets && a.QualificationYearId == (int)Enums.QualificationYear.ThirdYear).FirstOrDefault().SupplierValue;
                model.CashEquivalents2 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.CashEquivalents && a.QualificationYearId == (int)Enums.QualificationYear.ThirdYear).FirstOrDefault().SupplierValue;
                model.AccountsReceivable2 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.AccountsReceivable && a.QualificationYearId == (int)Enums.QualificationYear.ThirdYear).FirstOrDefault().SupplierValue;
                model.CurrentLiabilities2 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.CurrentLiabilities && a.QualificationYearId == (int)Enums.QualificationYear.ThirdYear).FirstOrDefault().SupplierValue;


                if (model.lstQualificationSupplierFinancialDataModel.FirstOrDefault().QualificationTypeId == (int)Enums.PreQualificationType.Large)
                {
                    model.NetProfit = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.NetProfit && a.QualificationYearId == (int)Enums.QualificationYear.CurrentYear).FirstOrDefault().SupplierValue;
                    model.NetProfit1 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.NetProfit && a.QualificationYearId == (int)Enums.QualificationYear.SecondYear).FirstOrDefault().SupplierValue;
                    model.NetProfit2 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.NetProfit && a.QualificationYearId == (int)Enums.QualificationYear.ThirdYear).FirstOrDefault().SupplierValue;



                    model.TotalRevenue = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalRevenue && a.QualificationYearId == (int)Enums.QualificationYear.CurrentYear).FirstOrDefault().SupplierValue;
                    model.TotalRevenue1 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalRevenue && a.QualificationYearId == (int)Enums.QualificationYear.SecondYear).FirstOrDefault().SupplierValue;
                    model.TotalRevenue2 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalRevenue && a.QualificationYearId == (int)Enums.QualificationYear.ThirdYear).FirstOrDefault().SupplierValue;


                    model.TotalLiabilities = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalLiabilities && a.QualificationYearId == (int)Enums.QualificationYear.CurrentYear).FirstOrDefault().SupplierValue;
                    model.TotalLiabilities1 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalLiabilities && a.QualificationYearId == (int)Enums.QualificationYear.SecondYear).FirstOrDefault().SupplierValue;
                    model.TotalLiabilities2 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalLiabilities && a.QualificationYearId == (int)Enums.QualificationYear.ThirdYear).FirstOrDefault().SupplierValue;



                    model.TotalAssets = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalAssets && a.QualificationYearId == (int)Enums.QualificationYear.CurrentYear).FirstOrDefault().SupplierValue;
                    model.TotalAssets1 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalAssets && a.QualificationYearId == (int)Enums.QualificationYear.SecondYear).FirstOrDefault().SupplierValue;
                    model.TotalAssets2 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalAssets && a.QualificationYearId == (int)Enums.QualificationYear.ThirdYear).FirstOrDefault().SupplierValue;

                }
            }

            return model;
        }

        private async Task<List<QualificationSupplierDataModel>> GetQualificationSupplierDataModel(int qualificationId, string supplierId)
        {
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
    }
}
