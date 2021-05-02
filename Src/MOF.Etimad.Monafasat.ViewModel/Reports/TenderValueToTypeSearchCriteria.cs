using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using System;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderValueToTypeSearchCriteria : SearchCriteria
    {
        public string AgencyCode { get; set; }
        public int? ActivityId { get; set; }
        public int? AgencyBeanchId { get; set; }
        public DateTime? FromCreatedDate { get; set; }
        public DateTime? ToCreatedDate { get; set; }
        public decimal? FromAverageSpending { get; set; }
        public decimal? ToAverageSpending { get; set; }

        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yy}")]
        public DateTime? FromCreatedDateString { get; set; }
        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yy}")]
        public DateTime? ToCreatedDateString { get; set; }
        public int? ConditionsBookletPrice { get; set; }
        public int? TenderPrice { get; set; }
        public int IntervalPeriod { get; set; }//TODO must be enum
        public int TenderId { get; set; }
        public int TenderStatusId { get; set; }
        public int BranchId { get; set; }
        public string FinancailYear { get; set; }
        public int? SpendingCategoryId { get; set; }
    }
}
