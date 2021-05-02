using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    public class SupplierBankGuaranteeAttachment : SupplierAttachment
    {

        public bool? IsBankGuaranteeValid { get; private set; }
        [Required]
        [StringLength(500)]
        public string GuaranteeNumber { get; private set; }
        [Required]
        public int BankId { get; private set; }
        [ForeignKey("BankId")]
        public virtual Bank Bank { get; private set; }
        [Required]
        public Decimal Amount { get; private set; }
        public DateTime? GuaranteeStartDate { get; private set; }
        public DateTime? GuaranteeEndDate { get; private set; }
        public int? GuaranteeDays { get; private set; }

        public SupplierBankGuaranteeAttachment()
        {

        }

        public SupplierBankGuaranteeAttachment(int attachmentId, int offerId, bool? isBankGuaranteeValid, string guaranteeNumber, int bankId, Decimal amount, DateTime? guaranteeStartDate, DateTime? guaranteeEndDate, int? guaranteeDays)
        {
            IsBankGuaranteeValid = isBankGuaranteeValid;
            GuaranteeNumber = guaranteeNumber;
            BankId = bankId;
            Amount = amount;
            GuaranteeStartDate = guaranteeStartDate;
            GuaranteeEndDate = guaranteeEndDate;
            GuaranteeDays = guaranteeDays;

            if (attachmentId == 0)
            {
                EntityCreated();
            }
            else
            {
                EntityUpdated();
            }

        }

    }
}
