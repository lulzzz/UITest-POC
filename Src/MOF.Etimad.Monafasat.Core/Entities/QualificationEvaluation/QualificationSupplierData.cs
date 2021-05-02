using MOF.Etimad.Monafasat.Core.Entities.QualificationEvaluation;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MOF.Etimad.Monafasat.Core.Entities
{

    [Table("QualificationSupplierData", Schema = "Qualification")]
    public partial class QualificationSupplierData : AuditableEntity
    {

        #region Ctor

        public QualificationSupplierData()
        {

        }

        public QualificationSupplierData(int id, int qualificationConfigurationId, int tenderId, decimal supplierValue, decimal pointValue, int qualificationItemId, int qualificationCategoryId, decimal weight, string supplierSelectedCr,
            int qualificationSubCategoryId, int? lookupId = null, DateTime? coverageExpireDate = null, decimal levelOfCoverage = 0, string insuranceProvider = null)
        {
            this.ID = id;
            this.QualificationConfigurationId = qualificationConfigurationId;
            this.TenderId = tenderId;
            this.SupplierValue = (qualificationItemId == (int)Enums.QualificationEvaluationItems.PercentageOfSaudiEmployees ? supplierValue * 100 : supplierValue);
            this.PointValue = pointValue;
            this.QualificationItemId = qualificationItemId;
            this.QualificationCategoryId = qualificationCategoryId;
            this.Weight = weight;
            this.SupplierSelectedCr = supplierSelectedCr;
            this.QualificationSubCategoryId = qualificationSubCategoryId;
            this.QualificationLookupId = lookupId == -99 ? null : lookupId;
            this.CoverageExpireDate = coverageExpireDate;
            this.LevelOfCoverage = levelOfCoverage;
            this.InsuranceProvider = insuranceProvider;
            EntityCreated();
        }
        #endregion


        [Key]
        public int ID { get; private set; }

        public int QualificationConfigurationId { get; private set; }

        public int TenderId { get; private set; }

        public decimal SupplierValue { get; private set; }

        public int? QualificationItemId { get; private set; }

        public decimal PointValue { get; private set; }


        [StringLength(20)]
        public string SupplierSelectedCr { get; private set; }


        public decimal Weight { get; private set; }


        [ForeignKey(nameof(QualificationConfigurationId))]
        public virtual QualificationConfiguration QualificationConfiguration { get; private set; }

        [ForeignKey(nameof(QualificationItemId))]
        public virtual QualificationItem QualificationItem { get; private set; }

        [ForeignKey(nameof(TenderId))]
        public virtual Tender Tender { get; private set; }


        [ForeignKey(nameof(SupplierSelectedCr))]
        public virtual Supplier Supplier { get; private set; }


        public int? QualificationLookupId { get; private set; }

        [ForeignKey(nameof(QualificationLookupId))]
        public virtual QualificationLookup QualificationLookup { get; private set; }


        public string InsuranceProvider { get; private set; }


        public decimal LevelOfCoverage { get; private set; }

        public DateTime? CoverageExpireDate { get; private set; }

        #region UnMapped Properties

        [NotMapped]
        public int QualificationSubCategoryId { get; private set; }

        [NotMapped]
        public int QualificationCategoryId { get; private set; }

        public virtual List<QualificationConfigurationAttachment> QualificationConfigurationAttachments { get; private set; } = new List<QualificationConfigurationAttachment>();

        public virtual List<QualificationSupplierProject> QualificationSupplierProjects { get; private set; } = new List<QualificationSupplierProject>();

        #endregion UnMapped Properties


        #region Method


        public void InsertSupplierProject(List<QualificationSupplierProject> qualificationSupplierProjects)
        {
            this.QualificationSupplierProjects = qualificationSupplierProjects;
        }

        public void DeActive()
        {
            this.IsActive = false;
            EntityUpdated();
        }


        public void InsertQualificationConfigurationAttachments(List<QualificationConfigurationAttachment> qualificationConfigurationAttachments)
        {
            this.QualificationConfigurationAttachments = qualificationConfigurationAttachments;
        }

        #endregion




    }
}
