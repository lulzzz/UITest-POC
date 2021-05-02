using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderTowFilesFinancialDetailsDetails
    {
        public string OfferIdString { get; set; }
        public string CombinedIdString { get; set; }
        public bool IsFinaintialOfferLetterAttached { get; set; }
        public string FinantialOfferLetterNumber { get; set; }
        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FinantialOfferLetterDate { get; set; }
        public bool IsBankGuaranteeAttached { get; set; }
        public IList<SupplierBankGuaranteeModel> BankGuaranteeFiles { get; set; } = new List<SupplierBankGuaranteeModel>();
        public bool IsFinantialOfferLetterCopyAttached { get; set; }
    }
}
