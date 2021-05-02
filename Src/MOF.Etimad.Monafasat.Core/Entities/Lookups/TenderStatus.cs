using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderStatus", Schema = "LookUps")]
    public class TenderStatus
    {
        #region Fields====================================================
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TenderStatusId { get; set; }

        [StringLength(100)]
        public string NameAr { get; set; }

        [StringLength(100)]
        public string NameEn { get; set; }

        #endregion

        #region NotMapped


        #endregion

        #region Constructors====================================================

        public TenderStatus()
        {

        }

        #endregion

        #region Methods====================================================



        #endregion
    }
}