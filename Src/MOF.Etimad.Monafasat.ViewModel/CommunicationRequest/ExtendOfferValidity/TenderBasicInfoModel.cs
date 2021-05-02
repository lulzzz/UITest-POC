using System;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderBasicInfoModel
    {
        //[Display(Name = "إسم المنافسة")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderName")]
        public string TenderName { get; set; }
        //[Display(Name = "رقم المنافسة")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderNumber")]
        public string TenderNumber { get; set; }
        //[Display(Name = "الرقم المرجعى المنافسة")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderRefrenceNumber")]
        public string TenderRefrenceNumber { get; set; }
        //[Display(Name = "حالة المنافسة")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderStatus")]
        public int TenderStatusId { get; set; }
        //[Display(Name = "القيمة التقديرية")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "EvaluationValue")]
        public Decimal? EstimatedValue { get; set; }
        //[Display(Name = "القيمة التقديرية (كتابة)")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "EvaluationValueWritten")]
        public string EstimatedValueText { get; set; }
        //[Display(Name = "عدد العروض")]
        [Display(ResourceType = typeof(Resources.OfferResources.DisplayInputs), Name = "OffersNumber")]
        public int OffersNumber { get; set; }
        public int? OfferPresentationWay { get; set; }

    }
}
