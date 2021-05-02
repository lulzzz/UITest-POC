using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
// this could replace TenderRevisionSearchCriteria !
namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderRevisionSearchCriteria : SearchCriteria
    {
        #region Fields ========
        public int TenderId { get; set; }
        public int? BranchId { get; set; }
        public string AgencyCode { get; set; }
        public string TenderName { get; set; }
        public string TenderNumber { get; set; }
        //request date
        public DateTime? RequestDateFrom { get; set; }
        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yy}")]
        public DateTime? RequestDateFromS { get; set; }
        public DateTime? RequestDateTo { get; set; }
        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yy}")]
        public DateTime? RequestDateToS { get; set; }
        //respopnse date
        public DateTime? ResponseDateFrom { get; set; }
        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yy}")]
        public DateTime? ResponseDateFromS { get; set; }
        public DateTime? ResponseDateTo { get; set; }
        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yy}")]
        public DateTime? ResponseDateToS { get; set; }
        public List<int> TenderStatusIds { get; set; }
        #endregion
    }
}
