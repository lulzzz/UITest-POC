using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static MOF.Etimad.Monafasat.Core.Enums;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("PreQualificationRequest", Schema = "Tender")]
    public partial class PreQualificationRequest : AuditableEntity
    {
        #region Fields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PreQualificationRequestId { get; private set; }

        public int SuplierId { get; private set; }
        public int TenderId { get; private set; }
        public int StatusId { get; private set; }
        //public int? AcceptanceStatusId { get; set; }
        //public int? OfferTechnicalEvaluationStatusId { get; set; }
        //[ForeignKey("OfferStatusId")]
        //public OfferStatus Status { get; private set; }
        //public bool IsManuallyApplied { get; private set; }
        ////  public string test1111111 { get; private set; }
        //[StringLength(20)]
        //public string CommericalRegisterNo { get; private set; }
    
        //public string Notes { get; private set; }

        /////////////////////////
        //public bool? IsChamberJoiningAttached { get; private set; }
        //public bool? IsChamberJoiningValid { get; private set; }
        //public bool? IsCommercialRegisterAttached { get; private set; }
        //public bool? IsCommercialRegisterValid { get; private set; }
        //public bool? IsOfferCopyAttached { get; private set; }

        //public bool? IsOfferLetterAttached { get; private set; }

        //[StringLength(500)]
        //public string OfferLetterNumber { get; private set; }

        //[StringLength(1024)]
        //public string JustificationOfRecommendation { get; private set; } //مبررات التوصية
        //public DateTime? OfferLetterDate { get; private set; }
        //public bool? IsPurchaseBillAttached { get; private set; }

        //public bool? IsSaudizationAttached { get; private set; }
        //public bool? IsSaudizationValidDate { get; private set; }

        //public bool? IsBankGuaranteeAttached { get; private set; }
        //public bool? IsSpecificationAttached { get; private set; }
        //public bool? IsSpecificationValidDate { get; private set; }

        //public bool? IsSocialInsuranceAttached { get; private set; }
        //public bool? IsSocialInsuranceValidDate { get; private set; }
        //public bool? IsVisitationAttached { get; private set; }

        //public bool IsZakatAttached { get; private set; }
        //public bool IsZakatValidDate { get; private set; }
        //public bool? IsOpened { get; private set; }
        //public decimal? TotalOfferAwardingValue { get; private set; }
        //public decimal? PartialOfferAwardingValue { get; private set; }

        #endregion



        #region  Navigation Properties

        [ForeignKey("SuplierId")]
        public Supplier Supplier { get; private set; }

        [ForeignKey("TenderId")]
        public Tender Qualification { get; private set; }


        #endregion
    }
}
