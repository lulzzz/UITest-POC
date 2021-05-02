using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("EnquiryReplyStatus", Schema = "LookUps")]
    public class EnquiryReplyStatus
    {
        #region Fields====================================================
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; private set; }

        [StringLength(100)]
        public string NameAr { get; private set; }

        [StringLength(100)]
        public string NameEn { get; private set; }

        #endregion

        #region NotMapped


        #endregion
    }

}