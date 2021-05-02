using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities.QualificationEvaluation
{

    [Table("QualificationSupplierProject", Schema = "Qualification")]
    public partial class QualificationSupplierProject : AuditableEntity
    {

        public QualificationSupplierProject()
        {

        }
        public QualificationSupplierProject(int id, int tenderId, decimal contractValue, string contractName, string description, string ownerName, string phoneNumber, string emailAddress, string supplierSelectedCr, DateTime? startDate, DateTime? enddate)
        {
            this.ID = id;
            this.TenderId = tenderId;
            this.ContractValue = contractValue;
            this.ContractName = contractName;
            this.Description = description;
            this.OwnerName = ownerName;
            this.PhoneNumber = phoneNumber;
            this.EmailAddress = emailAddress;
            this.SupplierSelectedCr = supplierSelectedCr;
            this.StartDate = startDate;
            this.EndDate = enddate;
            EntityCreated();
        }


        #region Fields
        [Key]
        public int ID { get; private set; }
        public int TenderId { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public string ContractName { get; private set; }
        public string Description { get; private set; }
        public string PerformanceEvaluation { get; private set; }


        public decimal ContractValue { get; private set; }
        public string OwnerName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string EmailAddress { get; private set; }

        public int QualificationSupplierDataId { get; set; }

        [ForeignKey(nameof(QualificationSupplierDataId))]
        public virtual QualificationSupplierData QualificationSupplierData { get; private set; }



        [StringLength(20)]
        public string SupplierSelectedCr { get; private set; }

        [ForeignKey(nameof(TenderId))]
        public virtual Tender Tender { get; private set; }

        [ForeignKey(nameof(SupplierSelectedCr))]
        public virtual Supplier Supplier { get; private set; }

        #endregion

        public void DeActive()
        {
            this.IsActive = false;
            EntityUpdated();
        }


    }
}
