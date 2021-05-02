using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel.Qualification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class QualificationSupplierDataModel
    {
        public int TenderStatusId { get; set; }
        public int QualificationConfigurationId { get; set; }
        public int TenderId { get; set; }
        public int QualificationSupplierDataId { get; set; }
        public string TenderIdStr { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal SupplierValue { get; set; }
        public string SupplierValueString { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal SupplierValue1 { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal SupplierValue2 { get; set; }
        public int ItemCode { get; set; }
        public int QualificationItemId { get; set; }
        public int QualificationSubCategoryId { get; set; }
        public int QualificationCategoryId { get; set; }
        public string QualificationItemName { get; set; }
        public int QualificationTypeId { get; set; }
        public int? QualificationStatusId { get; set; }
        public int QualificationItemCode { get; set; }
        public bool IsConfigure { get; set; }
        public string SupplierSelectedCr { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal Weight { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal Min { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal Max { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public decimal Value { get; set; }
        public int QualificationItemTypeId { get; set; }
        public int SelectListTypeId { get; set; }

        [Range(-1, Int32.MaxValue, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        [Required(ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Required")]
        public int QualificationLookupId { get; set; }

        public string OffersCheckingDateHijri { get; set; }

        public DateTime? OfferCheckingDate { get; set; }

        public string FileReferenceForQualityAssuranceStandards { get; set; }
        public string FileReferenceForEnvironmentalHealthSafetyStandards { get; set; }
        public string FileReferenceForInsurance { get; set; }
        public decimal PointValue { get; set; }
        public List<QualificationSupplierDataYearlyViewModel> lstQualificationSupplierDataYearlyViewModel { get; set; }
        public List<QualificationConfigurationAttachmentModel> lstQualificationConfigurationAttachmentModel { get; set; }
        public List<QualificationSupplierProjectModel> lstQualificationSupplierProjectModel { get; set; }

        #region insurance

        public string InsuranceProvider { get; set; }
        public decimal InsuranceCoverage { get; set; }

        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? InsuranceCoverageEndDate { get; set; }

        public string InsuranceCoverageEndDateStr { get; set; }

        public string InsuranceCoverageEndDateHijri { get; set; }
        #endregion unsurance
    }
}
