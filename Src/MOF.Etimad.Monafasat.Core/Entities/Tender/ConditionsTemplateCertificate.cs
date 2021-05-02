using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("ConditionsTemplateCertificate", Schema = "Tender")]
    public class ConditionsTemplateCertificate : AuditableEntity
    {
        #region Fields====================================================
        [Key]
        public int Id { get; private set; }

        [ForeignKey(nameof(Certificate))]
        public int CerificateId { get; private set; }
        public VendorCertificates Certificate { private set; get; }

        [ForeignKey(nameof(ConditionsTemplate))]
        public int ConditionsTemplateId { get; private set; }
        public TenderConditionsTemplate ConditionsTemplate { private set; get; }


        #endregion

        #region NotMapped


        #endregion

        #region Constructors====================================================

        public ConditionsTemplateCertificate(int cerificateId)
        {
            CerificateId = cerificateId;
            EntityCreated();
        }

        public ConditionsTemplateCertificate()
        {

        }
        #endregion

        #region Methods====================================================
        public void Delete()
        {
            EntityDeleted();
        }
        public void SetAddedState()
        {
            Id = 0;
            EntityCreated();
        }
        public void SetActive()
        {
            IsActive = true;
            EntityUpdated();
        }
        #endregion

    }
}
