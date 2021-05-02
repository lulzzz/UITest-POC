using System;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AttachmentModel
    {
        public int AttachmentId { get; set; }
        public string FileName { get; set; }

        public string FileNetId { get; set; }

        public string Extension { get; set; }

        public int Size { get; set; }
        public int type { get; set; }

        //public BlobModel AttachmentBlob { get; set; }

    }
    public class BankGuaranteeAttachmentModel : AttachmentModel
    {

        public bool IsBankGuaranteeAttached { get; private set; }

        public int status { get; set; }

        public string GuaranteeNumber { get; set; }

        public int BankId { get; set; }

        public string BankName { get; set; }

        public Decimal Amount { get; set; }
        public DateTime? GuaranteeStartDate { get; set; }
        public DateTime? GuaranteeEndDate { get; set; }
        public int? GuaranteeDays { get; set; }


    }


    public class SpecificationAttachmentModel : AttachmentModel
    {

        public bool IsBankGuaranteeAttached { get; private set; }

        public int status { get; set; }

        public string GuaranteeNumber { get; set; }

        public int BankId { get; set; }

        public string BankName { get; set; }

        public Decimal Amount { get; set; }
        public DateTime? GuaranteeStartDate { get; set; }
        public DateTime? GuaranteeEndDate { get; set; }
        public int? GuaranteeDays { get; set; }


    }



}
