using MOF.Etimad.Monafasat.ViewModel.Qualification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class PreQualificationApplyDocumentsModel
    {
        public int TenderId { get; set; }
        public int SupplierPreQualificationDocumentId { get; set; }
        public string SupplierPreQualificationDocumentIdString { get; set; }
        public string SupplierId { get; set; }
        public int PreQualificationId { get; set; }
        public string PreQualificationIdString { get; set; }
        public int? PreQualificationResult { get; set; } = 0;
        public int? PreQualificationResult2 { get; set; } = 0;
        public string RejectionReason { get; set; }

        [Display(Name = "Atachementes", ResourceType = typeof(Resources.SharedResources.DisplayInputs))]
        public string AttachmentRefrences { get; set; }

        public string OffersCheckingDateHijri { get; set; }

        public DateTime? OfferCheckingDate { get; set; }

        public bool EditMode { get; set; }
        public SupplierPreQualificationDocumentModel SupplierPreQualificationDocumentModel { get; set; }
        public List<PreQualificationSupplierAttachmentModel> preQualificationSupplierAttachmentModels { get; set; }
        public List<QualificationSupplierDataYearlyViewModel> lstQualificationSupplierDataYearlyViewModel { get; set; }
        public List<QualificationAttachmentModel> InsuranceAttachmentModels { get; set; }
        public List<QualificationAttachmentModel> EnvironmentalHealthSafetyStandardsAttachmentModels { get; set; }
        public List<QualificationAttachmentModel> QualityAssuranceStandardsAttachmentModels { get; set; }
        public List<QualificationSupplierProjectModel> lstQualificationSupplierProjectModel { get; set; }
        public List<QualificationSupplierDataModel> lstQualificationSupplierDataModel { get; set; }
        public List<QualificationSupplierDataModel> lstQualificationSupplierTechDataModel { get; set; }
        public List<QualificationSupplierDataModel> lstQualificationSupplierFinancialDataModel { get; set; }

        public int QualificationTypeId { get; set; }
        public int? QualificationStatusId { get; set; }

        public int SaudiEmployeesNumber { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal CashEquivalents { get; set; }  // النقد وما يعادله 

        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal AccountsReceivable { get; set; }  // حسابات مستحقه القبض

        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal CurrentLiabilities { get; set; }  //  او  الالتزامات او المطلوبات المتداولة

        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal CurrentAssets { get; set; }   //   و الموجودات   و الاصول المتداولة


        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal TotalAssets { get; set; }   // مجموع الموجودات

        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal TotalLiabilities { get; set; } // مجموع المطلوبات

        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal TotalRevenue { get; set; }  // مجموع الايرادات

        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal NetProfit { get; set; }  // صافي الارباح

        //[Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public string FileReferenceForQualityAssuranceStandards { get; set; }

        //[Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public string FileReferenceForEnvironmentalHealthSafetyStandards { get; set; }

        public string FileReferenceForInsurance { get; set; }



        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal CashEquivalents1 { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal AccountsReceivable1 { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal CurrentLiabilities1 { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal CurrentAssets1 { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal TotalAssets1 { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal TotalRevenue1 { get; set; } // مجموع الايرادات
        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal TotalLiabilities1 { get; set; } // مجموع المطلوبات

        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal NetProfit1 { get; set; }  // صافي الارباح



        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal CashEquivalents2 { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal AccountsReceivable2 { get; set; }


        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal CurrentLiabilities2 { get; set; }


        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal CurrentAssets2 { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal TotalAssets2 { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal TotalRevenue2 { get; set; } // مجموع الايرادات
        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal NetProfit2 { get; set; }  // صافي الارباح
        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal TotalLiabilities2 { get; set; }  // مجموع المطلوبات


        public int QualityAssuranceStandards { get; set; }
        public int EnvironmentalHealthSafetyStandards { get; set; }
    }
}
