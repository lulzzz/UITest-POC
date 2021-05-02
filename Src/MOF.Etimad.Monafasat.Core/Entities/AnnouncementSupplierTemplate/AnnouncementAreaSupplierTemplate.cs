using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("AnnouncementAreaSupplierTemplate", Schema = "AnnouncementTemplate")]

    public class AnnouncementAreaSupplierTemplate : AuditableEntity
    {
        [Key]
        public int Id { get; private set; }

        [ForeignKey(nameof(Area))]
        public int AreaId { get; private set; }
        public Area Area { private set; get; }

        [ForeignKey(nameof(AnnouncementSupplierTemplate))]
        public int AnnouncementId { get; private set; }
        public AnnouncementSupplierTemplate AnnouncementSupplierTemplate { private set; get; }

        public AnnouncementAreaSupplierTemplate(int areaId)
        {
            AreaId = areaId;
            EntityCreated();
        }

        public AnnouncementAreaSupplierTemplate()
        {

        }

        public void Delete()
        {
            EntityDeleted();
        }
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        public void SetActive()
        {
            IsActive = true;
            EntityUpdated();
        }
    }
}
