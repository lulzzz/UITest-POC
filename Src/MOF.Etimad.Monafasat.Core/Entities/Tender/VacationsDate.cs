using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("VacationsDate", Schema = "Tender")]
    public class VacationsDate
    {
        #region Fields

        [Key]
        public int VacationId { get; private set; }

        [StringLength(1024)]
        public string VacationName { get; private set; }
        public DateTime? VacationDate { get; private set; }

        #endregion


        public VacationsDate()
        {

        }
    }
}
