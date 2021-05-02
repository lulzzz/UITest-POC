using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("PrePlanningStatus", Schema = "Lookups")]
    public class PrePlanningStatus
    {
        public PrePlanningStatus()
        {

        }
        public PrePlanningStatus(int id, string nameEn, string nameAr)
        {
            StatusId = id;
            NameAr = nameAr;
            NameEn = nameEn;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StatusId { get; private set; }

        [StringLength(100)]
        public string NameAr { get; private set; }

        [StringLength(100)]
        public string NameEn { get; private set; }
    }
}

