using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class PostQualificationApplyDocumentsModel : PreQualificationSavingModel
    {
        public string postQualificationIdString { get; set; }
        public string PostQualificationTenderIdString { get; set; }
        public int? PostQualificationTenderId { get; set; }
        public List<string> CommercialNumbers { get; set; } = new List<string>();
        //[Required(ErrorMessageResourceType = typeof(Resources.QualificationResources.ErrorMessages), ErrorMessageResourceName = "EnterQualificationEnquiryDate")]
        public string LastEnqueriesDateString { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Resources.QualificationResources.ErrorMessages), ErrorMessageResourceName = "EnterQualificationDocumentsApplying")]
        public string OffersCheckingDateString { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Resources.QualificationResources.ErrorMessages), ErrorMessageResourceName = "EnterQualificationDocumentsEvaluationgDate")]
        public string LastOfferPresentationDateString { get; set; }
        public bool HasOldPostQualification { get; set; }



        //public int QualificationTypeId { get; set; }
        //public decimal TenderPointsToPass { get; set; }
        //public decimal TechnicalAdministrativeCapacity { get; set; }
        //public decimal FinancialCapacity { get; set; }
        //public int QualificationTypeId { get; set; }
        //public QualificationSmallEvaluation QualificationSmallEvaluation { get; set; }
        //public QualificationMediumEvaluation QualificationMediumEvaluation { get; set; }
        //public QualificationLargeEvaluation QualificationLargeEvaluation { get; set; }
    }
}
