using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("VersionType", Schema = "Lookups")]
    public class VersionType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(100)]
        public string NameEnglish { get; set; }

        [StringLength(100)]
        public string NameArabic { get; set; }
    }
}
