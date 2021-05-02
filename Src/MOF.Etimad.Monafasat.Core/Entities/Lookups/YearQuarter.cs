using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("YearQuarter", Schema = "Lookups")]
    public class YearQuarter
    {
        public YearQuarter()
        {

        }
        public YearQuarter(int id, string nameEn, string nameAr)
        {
            YearQuarterId = id;
            NameAr = nameAr;
            NameEn = nameEn;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int YearQuarterId { get; private set; }

        [StringLength(100)]
        public string NameAr { get; private set; }

        [StringLength(100)]
        public string NameEn { get; private set; }
    }
}

