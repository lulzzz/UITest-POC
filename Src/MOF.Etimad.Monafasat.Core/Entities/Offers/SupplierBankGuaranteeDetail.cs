using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    public class SupplierBankGuaranteeDetail : AuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BankGuaranteeId { get; private set; }
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
        public int? OfferId { get; set; }
        [ForeignKey("OfferId")]
        public Offer Offer { get; set; }


        public SupplierBankGuaranteeDetail()
        {

        }

        public SupplierBankGuaranteeDetail(int attachmentId, int offerId, bool? isBankGuaranteeValid, string guaranteeNumber, int bankId, Decimal amount, DateTime? guaranteeStartDate, DateTime? guaranteeEndDate, int? guaranteeDays)
        {
            IsBankGuaranteeValid = isBankGuaranteeValid;
            OfferId = offerId;
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

        public void Delete()
        {
            EntityDeleted();
        }
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
    }
}
