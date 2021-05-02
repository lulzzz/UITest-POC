using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("AnnouncementTemplateListType", Schema = "LookUps")]
    public class AnnouncementTemplateListType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AnnouncementTemplateSuppliersListTypeId { get; private set; }
        [StringLength(100)]
        public string NameAr { get; private set; }
        [StringLength(100)]
        public string NameEn { get; private set; }
    }
}
