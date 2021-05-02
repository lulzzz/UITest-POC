using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("Bank", Schema = "LookUps")]
    public class Bank
    {
        #region Fields====================================================
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int BankId { get; private set; }

        [StringLength(1024)]
        public string NameEn { get; private set; }

        [StringLength(1024)]
        public string NameAr { get; private set; }

        #endregion

        #region NotMapped


        #endregion
    }

}