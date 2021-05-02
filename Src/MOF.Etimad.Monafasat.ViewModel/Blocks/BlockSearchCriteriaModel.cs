using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using System;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class BlockSearchCriteriaModel : SearchCriteria
    {
        [StringLength(20, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less20")]
        public string CommercialRegistrationNo { get; set; }
        [StringLength(200, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less200")]
        public string CommercialSupplierName { get; set; }
        [StringLength(20, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less20")]
        public string ResolutionNumber { get; set; }
        public int BlockTypeId { get; set; }
        public int BlockStatusId { get; set; }
        public int IsDeletedId { get; set; }

        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FromDate { get; set; }

        //[ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        //public DateTime? LastEnqueriesDate { get; set; }
        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ToDate { get; set; }
        public bool? IsOldBlock { get; set; }
        [StringLength(20, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less20")]
        public string AgencyCode { get; set; }
        [StringLength(200, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less200")]
        public string AgencyName { get; set; }
        public int TenderTypeId { get; set; }
        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yy}")]
        public DateTime? FromDateString { get; set; }
        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yy}")]
        public DateTime? ToDateString { get; set; }
    }
}
