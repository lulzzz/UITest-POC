using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderConditoinsStatus", Schema = "LookUps")]
    public class TenderConditoinsStatus
    {
        #region Fields====================================================
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TenderConditoinsStatusId { get; private set; }

        [StringLength(500)]
        public string NameAr { get; private set; }

        [StringLength(500)]
        public string NameEn { get; set; }

        #endregion

        #region Constructors====================================================

        public TenderConditoinsStatus()
        {

        }

        public TenderConditoinsStatus(string nameEn, string nameAr, bool isActive)
        {
            NameAr = nameAr;
            NameEn = nameEn;

        }
        #endregion

        #region Methods====================================================



        #endregion
    }
}
