using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("RequestsRejectionType", Schema = "LookUps")]
    public class RequestsRejectionType
    {
        #region Fields====================================================
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RequestTypeId { get; private set; }

        [StringLength(500)]
        public string NameAr { get; private set; }

        [StringLength(500)]
        public string NameEn { get; set; }

        #endregion

        #region Constructors====================================================

        public RequestsRejectionType()
        {

        }

        public RequestsRejectionType(string nameEn, string nameAr, bool isActive)
        {
            NameAr = nameAr;
            NameEn = nameEn;

        }
        #endregion

        #region Methods====================================================



        #endregion
    }
}
