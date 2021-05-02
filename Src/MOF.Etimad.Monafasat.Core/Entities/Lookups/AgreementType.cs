using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("AgreementType", Schema = "LookUps")]
    public class AgreementType
    {
        #region Fields====================================================
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AgreementTypeId { get; private set; }

        [StringLength(500)]
        public string NameAr { get; private set; }

        [StringLength(500)]
        public string NameEn { get; set; }

        #endregion

        #region Constructors====================================================

        public AgreementType()
        {

        }

        public AgreementType(string nameEn, string nameAr, bool isActive)
        {
            NameAr = nameAr;
            NameEn = nameEn;

        }
        #endregion

        #region Methods====================================================



        #endregion
    }
}
