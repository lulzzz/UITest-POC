using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("City", Schema = "Lookups")]
    public class City
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CityID { get; set; }

        [StringLength(100)]
        public string NameEnglish { get; set; }

        [StringLength(100)]
        public string NameArabic { get; set; }

    }
}
