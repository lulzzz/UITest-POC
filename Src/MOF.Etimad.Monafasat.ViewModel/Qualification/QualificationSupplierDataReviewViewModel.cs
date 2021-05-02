using MOF.Etimad.Monafasat.ViewModel.Qualification;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class QualificationSupplierDataReviewViewModel
    {
        public List<QualificationSupplierDataModel> lstQualificationSupplierTechDataModel { get; set; }
        public List<QualificationSupplierDataModel> lstQualificationSupplierFinancialDataModel { get; set; }
        public List<QualificationSupplierDataYearlyViewModel> QualificationSupplierDataYearly { get; set; }
        public List<QualificationSupplierProjectModel> lstQualificationSupplierProjectModel { get; set; }
        public int QualificationTypeId { get; set; }
        public decimal SaudiEmployeesNumber { get; set; }
        public string TenderIdStr { get; set; }
        public string SupplierCR { get; set; }
        public int TenderStatusId { get; set; }
    }
}
