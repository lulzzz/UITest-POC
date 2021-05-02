using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("VendorCertificate", Schema = "LookUps")]
    public class VendorCertificates
    {
        #region Fields====================================================
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VendorCertificateId { get; private set; }

        [StringLength(1024)]
        public string NameEn { get; private set; }

        [StringLength(1024)]
        public string NameAr { get; private set; }

        public List<ConditionsTemplateCertificate> ConditionsTemplateCertificates { private set; get; }
        #endregion
        #region Constractore

        #endregion
    }
}