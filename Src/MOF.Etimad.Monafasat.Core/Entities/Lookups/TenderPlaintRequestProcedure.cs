using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderPlaintRequestProcedure", Schema = "LookUps")]
    public class TenderPlaintRequestProcedure
    {
        #region Fields====================================================
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; private set; }

        [StringLength(1024)]
        public string Name { get; private set; }

        #endregion

        #region NotMapped


        #endregion
    }

}