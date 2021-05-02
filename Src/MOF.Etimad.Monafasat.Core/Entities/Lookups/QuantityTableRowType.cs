using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("QuantityTableRowType", Schema = "Lookups")]
    public class QuantityTableRowType
    {
        public QuantityTableRowType()
        {

        }
        public QuantityTableRowType(int id, string nameEn, string nameAr)
        {
            QuantityTableRowTypeId = id;
            NameAr = nameAr;
            NameEn = nameEn;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuantityTableRowTypeId { get; private set; }

        [StringLength(100)]
        public string NameAr { get; private set; }

        [StringLength(100)]
        public string NameEn { get; private set; }
    }
}

