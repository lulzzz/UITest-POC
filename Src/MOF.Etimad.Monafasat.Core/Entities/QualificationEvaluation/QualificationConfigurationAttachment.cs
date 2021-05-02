using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MOF.Etimad.Monafasat.Core.Entities
{


    [Table("QualificationConfigurationAttachment", Schema = "Qualification")]
    public partial class QualificationConfigurationAttachment : AuditableEntity
    {
        #region Fields
        [Key]
        public int ID { get; private set; }

        public string FileName { get; private set; }


        public int QualificationSupplierDataId { get; private set; }

        public string FileReferenceId { get; set; }

        [ForeignKey(nameof(QualificationSupplierDataId))]
        public virtual QualificationSupplierData QualificationSupplierData { get; private set; }
        #endregion Fields

        #region methods
        public QualificationConfigurationAttachment()
        {

        }
        public QualificationConfigurationAttachment(string fileName, string fileNetReferenceId, int qualificationSupplierDataId)
        {
            FileName = fileName;
            FileReferenceId = fileNetReferenceId;
            this.QualificationSupplierDataId = qualificationSupplierDataId;
            EntityCreated();
        }

        public QualificationConfigurationAttachment(int id, string fileName, string fileNetReferenceId)
        {
            this.ID = id;
            FileName = fileName;
            FileReferenceId = fileNetReferenceId;
            EntityCreated();
        }

        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        public void SetActive()
        {
            IsActive = true;
            EntityUpdated();
        }
        internal void Delete()
        {
            EntityDeleted();
        }
        #endregion methods


    }
}
