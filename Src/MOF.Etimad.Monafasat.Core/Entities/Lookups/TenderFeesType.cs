using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderFeesType", Schema = "Lookups")]
    public class TenderFeesType
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TenderFeesTypeId { get; set; }

        [StringLength(100)]
        public string NameEnglish { get; set; }

        [StringLength(100)]
        public string NameArabic { get; set; }

    }
}
