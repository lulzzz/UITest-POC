using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("VerificationCode", Schema = "Verification")]
    public class VerificationCode : AuditableEntity
    {
        [Key]
        public int VerificationCodeId { get; private set; }

        [StringLength(1024)]
        public string VerificationCodeNo { get; private set; }
        public int CodeTypeId { get; private set; }

        [ForeignKey(nameof(VerificationType))]
        public int VerificationTypeId { get; private set; }
        public VerificationType VerificationType { get; set; }

        public int UserId { get; private set; }
        public DateTime ExpiredDate { get; private set; }

        public VerificationCode()
        {

        }

        public VerificationCode(string verificationCode, int userId, int verificationTypeId, int codeTypeId)
        {
            VerificationCodeNo = verificationCode;
            VerificationTypeId = verificationTypeId;
            CodeTypeId = codeTypeId;

            UserId = userId;
            ExpiredDate = DateTime.Now.AddMinutes(5);
            EntityCreated();
        }
    }
}
