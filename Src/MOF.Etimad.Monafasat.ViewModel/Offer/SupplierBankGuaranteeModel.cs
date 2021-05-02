using System;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SupplierBankGuaranteeModel : SupplierAttachmentModel
    {
        public int BankGuaranteeId { get; set; }
        public bool? IsBankGuaranteeValid { get; set; } = true;
        public string GuaranteeNumber { get; set; }
        public string IsBankGuaranteeValidString { get; set; }
        public int? BankId { get; set; }
        public string BankName { get; set; }

        //public BankModel Bank { get; set; }
        public Decimal Amount { get; set; }
        public DateTime? GuaranteeStartDate { get; set; }
        public DateTime? GuaranteeEndDate { get; set; }
        public string GuaranteeStartDateString { get; set; }
        public string GuaranteeEndDateString { get; set; }
        public string GuaranteeStartDateDisplay { get; set; }
        public string GuaranteeEndDateDisplay { get; set; }
        public int? GuaranteeDays { get; set; }
    }
}
